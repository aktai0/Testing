Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports RiotSharp

<Serializable()>
Module CacheManager
   Private CacheTypeNames As New Dictionary(Of String, EasyCache)

   Private _CacheList As New Dictionary(Of String, EasyCache)
   Public Property CacheList() As Dictionary(Of String, EasyCache)
      Get
         Return _CacheList
      End Get
      Set(ByVal value As Dictionary(Of String, EasyCache))
         _CacheList = value
      End Set
   End Property

   Public Function RetrieveCache(Of T As EasyCache)() As T
      For Each cache As EasyCache In CacheList.Values
         If TypeOf (cache) Is T Then
            Return CType(cache, T)
         End If
      Next
      Return Nothing
   End Function

   ' We use two dicts because A) Dictionary entries are added as (Address, Address) pairs a type
   '  is not a primitive type and B) Deserialization creates a new object, so we would either need
   '  to replace the entry in the original dictionary (if we're only using one), use a different
   '  dictionary, or use a container class.
   Public Sub Init()
      CacheTypeNames.Add("Static", New StaticCache)
      CacheTypeNames.Add("Data", New DataCache)
      CacheTypeNames.Add("Matches", New MatchIDCache)
   End Sub

   Public Sub LoadAllCaches()
      For Each kvpair As KeyValuePair(Of String, EasyCache) In CacheTypeNames
         Dim loadedCache As EasyCache = LoadCacheFile(kvpair.Value.CACHE_FILE_NAME)
         ' If the cache file doesn't exist...
         If loadedCache Is Nothing Then
            loadedCache = kvpair.Value
         End If
         CacheList.Add(kvpair.Key, loadedCache)
         If loadedCache.ShouldRebuildCache Then
            loadedCache.RebuildCacheAsync()
         End If
         loadedCache.FinishedLoading()
      Next
   End Sub


   Private Function LoadCacheFile(ByVal cacheFileName As String) As EasyCache
      Dim newCache As EasyCache = Nothing
      If File.Exists(cacheFileName) Then
         Dim TestFileStream As Stream = File.OpenRead(cacheFileName)
         Dim deserializer As New BinaryFormatter
         Try
            newCache = CType(deserializer.Deserialize(TestFileStream), EasyCache)
         Catch ex As Exception
            MsgBox("There was an error with the cache. " & cacheFileName & " will be deleted. All cache data in that file will be lost. If you want to save a backup of it, do so NOW (before you exit this dialog). Error: " & ex.Message)
            TestFileStream.Close()
            File.Delete(cacheFileName)
            'MainWindow.Close()
         End Try
         TestFileStream.Close()
      End If
      Return newCache
   End Function

   Public Sub StoreAllCaches()
      For Each cache In CacheList.Values
         If cache.CacheChanged Then
            cache.StartedStoring()
            StoreCacheFile(cache.CACHE_FILE_NAME, cache)
         End If
      Next
   End Sub

   Public Sub StoreCacheFile(ByVal cacheFileName As String, ByRef givenCache As EasyCache)
      ' SyncLock to avoid corruption.
      SyncLock givenCache
         Dim TestFileStream As Stream = File.Create(cacheFileName)
         Dim serializer As New BinaryFormatter
         serializer.Serialize(TestFileStream, givenCache)
         TestFileStream.Close()
      End SyncLock
   End Sub
End Module

<Serializable()>
MustInherit Class EasyCache
   MustOverride ReadOnly Property CACHE_FILE_NAME() As String
   MustOverride ReadOnly Property CACHE_LIMIT() As Integer

   ' Always reuse old cache by default.
   Overridable ReadOnly Property ShouldRebuildCache() As Boolean
      Get
         Return False
      End Get
   End Property

   ' Always save by default.
   Overridable ReadOnly Property CacheChanged() As Boolean
      Get
         Return True
      End Get
   End Property

   Overridable Sub RebuildCache()
      Throw New InvalidOperationException
   End Sub

   Overridable Sub RebuildCacheAsync()
      Throw New InvalidOperationException
   End Sub

   Sub New()
   End Sub

   Overridable Sub FinishedLoading()
   End Sub

   Overridable Sub StartedStoring()
   End Sub

End Class

<Serializable()>
Class StaticCache
   Inherits EasyCache

   Public Overrides ReadOnly Property CACHE_FILE_NAME As String
      Get
         Return "StaticCache.bin"
      End Get
   End Property

   Public Overrides ReadOnly Property CACHE_LIMIT As Integer
      Get
         Return 0
      End Get
   End Property

   <NonSerialized>
   Dim _CacheChanged As Boolean = False
   Public Overrides ReadOnly Property CacheChanged As Boolean
      Get
         Return _CacheChanged
      End Get
   End Property

   Public Overrides ReadOnly Property ShouldRebuildCache As Boolean
      Get
         Return Images.Count = 0 Or Champions.Count = 0
      End Get
   End Property

   Public Images As New Dictionary(Of Integer, Bitmap)

   Public Overrides Sub RebuildCache()
      Throw New InvalidOperationException
   End Sub

   Public Overrides Sub RebuildCacheAsync()
      APIHelper.API_INIT()
      AsynchronousStaticLoader = New System.ComponentModel.BackgroundWorker
      AddHandler AsynchronousStaticLoader.DoWork, AddressOf AsynchronousStaticLoader_DoWork
      AsynchronousStaticLoader.RunWorkerAsync()
   End Sub

   Public Overrides Sub FinishedLoading()
      MyBase.FinishedLoading()

      APIHelper.Champions = Champions
      APIHelper.ChampionsRaw = ChampionsRaw
   End Sub

   <NonSerialized>
   Private WithEvents AsynchronousStaticLoader As System.ComponentModel.BackgroundWorker

   Public Champions As New Dictionary(Of Integer, StaticDataEndpoint.ChampionStatic)
   Public ChampionsRaw As StaticDataEndpoint.ChampionListStatic

   Private Sub AsynchronousStaticLoader_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
      Console.Write("Fetching static champion data...")
      APIHelper.API_STATIC_LOAD_CHAMPION_INFO()
      Champions = APIHelper.Champions
      ChampionsRaw = APIHelper.ChampionsRaw
      Console.WriteLine(" Done!")

      For Each champ As StaticDataEndpoint.ChampionStatic In APIHelper.Champions.Values
         Console.Write("Fetching image for " & champ.Name & "...")
         Images.Add(champ.Id, APIHelper.API_GET_IMAGE_FOR(champ.Id))
         Console.WriteLine(" Done!")
      Next

      _CacheChanged = True
   End Sub
End Class

<Serializable>
Class MatchIDCache
   Inherits EasyCache

   Public Overrides ReadOnly Property CACHE_FILE_NAME As String
      Get
         Return "MatchIDCache.bin"
      End Get
   End Property

   Public Overrides ReadOnly Property CACHE_LIMIT As Integer
      Get
         Return 0
      End Get
   End Property

   Private FirstMatchID As Integer
   Private LastMatchID As Integer
   Private Count As Integer

   Public Overrides ReadOnly Property CacheChanged As Boolean
      Get
         If MatchIDQueue.Count > 0 Then
            Return Count <> MatchIDQueue.Count OrElse FirstMatchID <> MatchIDQueue.Peek() OrElse LastMatchID <> MatchIDQueue.Last()
         Else
            Return Count <> MatchIDQueue.Count
         End If
      End Get
   End Property

   Public Overrides Sub StartedStoring()
      If MatchIDQueue.Count > 0 Then
         FirstMatchID = MatchIDQueue.Peek
         LastMatchID = MatchIDQueue.Last
      Else
         FirstMatchID = 0
         LastMatchID = 0
      End If
      Count = MatchIDQueue.Count
   End Sub

   Dim _TotalMatchesLoaded As Integer = 0
   Public Property TotalMatchesLoaded As Integer
      Get
         Return _TotalMatchesLoaded
      End Get
      Private Set(value As Integer)
         _TotalMatchesLoaded = value
      End Set
   End Property

   Dim _NextURFBucketTimeToLoad As DateTime = FIRST_URF_DATETIME
   Public Property NextURFBucketTimeToLoad As DateTime
      Get
         Return _NextURFBucketTimeToLoad
      End Get
      Private Set(value As DateTime)
         _NextURFBucketTimeToLoad = value
      End Set
   End Property

   Private _TotallMatchIDs As Integer = 0
   Public Property TotalMatchIDs As Integer
      Get
         Return _TotallMatchIDs
      End Get
      Private Set(value As Integer)
         _TotallMatchIDs = value
      End Set
   End Property

   Public ReadOnly Property HasMatchesPendingLoad As Boolean
      Get
         Return MatchIDQueue.Count > 0
      End Get
   End Property

   Public ReadOnly Property NumMatchesPendingLoad As Integer
      Get
         Return MatchIDQueue.Count
      End Get
   End Property

   ' Queue  
   Private MatchIDQueue As New Queue(Of Integer)

   Public Sub PopFirstMatchIfEquals(ByVal matchID As Integer)
      If matchID = MatchIDQueue.Peek Then
         MatchIDQueue.Dequeue()
      End If
   End Sub

   Private Sub IncrementTime()
      NextURFBucketTimeToLoad = NextURFBucketTimeToLoad.Add(New TimeSpan(0, 5, 0))
   End Sub

   Public ReadOnly Property HasMatchIDsAvailable() As Boolean
      Get
         Return DateTimeToEpoch(NextURFBucketTimeToLoad) <= FINAL_EPOCH
      End Get
   End Property

   ' Call me in a BackgroundWorker! Preferably in conjunction with sleep!
   Public Sub LoadMatchIDs()
      ErrorPending = False
      Dim matches = APIHelper.API_GET_URF_MATCHES(DateTimeToEpoch(NextURFBucketTimeToLoad).ToString)

      If matches Is Nothing Then
         ErrorPending = True
         Return
      End If

      SyncLock MatchIDQueue
         For Each m In matches
            MatchIDQueue.Enqueue(m)
            TotalMatchIDs += 1
         Next
      End SyncLock
      IncrementTime()
   End Sub

   Public Sub LoadMatchesAsync(ByVal numMatches As Integer)
      ErrorPending = False
      If NumMatchesPendingLoad < numMatches Then
         ErrorPending = True
         Return
      End If


      Dim list As New List(Of System.ComponentModel.BackgroundWorker)

      For i = 1 To numMatches
         Dim matchLoaderBackgroundWorker As New System.ComponentModel.BackgroundWorker()
         AddHandler matchLoaderBackgroundWorker.DoWork, AddressOf MatchLoadingBackgroundWorker_DoWork
         AddHandler matchLoaderBackgroundWorker.RunWorkerCompleted, AddressOf MatchLoadingBackgroundWorker_Completed

         Dim container As New MatchupContainer
         container.MatchID = MatchIDQueue.Dequeue

         matchLoaderBackgroundWorker.RunWorkerAsync(container)
         list.Add(matchLoaderBackgroundWorker)
      Next

      While True
         Dim oneStillBusy As Boolean = False
         For Each bg In list
            If bg.IsBusy Then
               oneStillBusy = True
               Exit For
            End If
         Next
         If Not oneStillBusy Then
            Exit While
         End If
         Threading.Thread.Sleep(100)
      End While
   End Sub

   Private Sub MatchLoadingBackgroundWorker_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
      'Dim matchupList = sender.
      Dim container As MatchupContainer = CType(e.Argument, MatchupContainer)
      e.Result = container
      container.LoadMatch()
   End Sub

   Private Sub MatchLoadingBackgroundWorker_Completed(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
      Dim container As MatchupContainer = CType(e.Result, MatchupContainer)
      If container Is Nothing OrElse container.Matchups Is Nothing Then
         ErrorPending = True
         Return
      End If

      RetrieveCache(Of DataCache)().AddMatchupData(container.Matchups)
      TotalMatchesLoaded += 1
   End Sub


   Private Class MatchupContainer
      Public Matchups As IEnumerable(Of Matchup) = Nothing
      Public MatchID As Integer = 0

      Public Sub LoadMatch()
         Dim matchDetail As MatchEndpoint.MatchDetail = APIHelper.API_GET_MATCH_INFO(MatchID, True)

         If matchDetail Is Nothing Then
            Return
         End If

         Dim participantsByLane = From p In matchDetail.Participants
                                  Group p By Lane = p.Timeline.Lane Into groups = Group
                                  Select Lane, groups
         Dim winningTeam = If(matchDetail.Teams(0).Winner, matchDetail.Teams(0).TeamId, matchDetail.Teams(1).TeamId)
         Dim matchupList As New List(Of Matchup)

         For Each item In participantsByLane
            For Each i In item.groups
               Dim myChampion As Integer = i.ChampionId
               Dim myEnemy As Integer = 0
               Dim wonLane As Boolean = i.TeamId = winningTeam
               Dim newMatchup As Matchup

               Dim numEnemies As Integer = 0

               ' Iterate through every other laner
               For Each a In item.groups
                  ' Participant on the opposite team is an enemy, add all of them as new matchups
                  If i.TeamId <> a.TeamId Then
                     numEnemies += 1
                     myEnemy = a.ChampionId

                     ' We create a matchup for each champion in the lane (allies and enemies). 4 matchups if 2v2
                     newMatchup = New Matchup(MatchID, myChampion, myEnemy, item.Lane, wonLane, i.TeamId)
                     matchupList.Add(newMatchup)
                  End If
               Next

               ' Xv0 Lane
               If numEnemies = 0 Then
                  matchupList.Add(New Matchup(MatchID, myChampion, 0, item.Lane, wonLane, i.TeamId))
               End If
            Next
         Next

         Matchups = matchupList
         Return
      End Sub
   End Class

   <NonSerialized>
   Public ErrorPending As Boolean = False

   ' Call me in a BackgroundWorker! Preferably in conjunction with sleep!
   Public Sub LoadFirstMatchIntoMatchups()
      ErrorPending = False
      Dim firstMatchID As Integer = MatchIDQueue.Peek
      Dim matchups = LoadMatch(MatchIDQueue.Peek)

      If matchups Is Nothing Then
         ErrorPending = True
         Return
      End If

      RetrieveCache(Of DataCache)().AddMatchupData(matchups)
      TotalMatchesLoaded += 1

      ' The first entry shouldn't change in a different thread, but better safe than sorry.
      SyncLock MatchIDQueue
         PopFirstMatchIfEquals(firstMatchID)
      End SyncLock
   End Sub

   <NonSerialized>
   Public LoadFast As Boolean = False

   Private Function LoadMatch(ByVal matchID As Integer) As IEnumerable(Of Matchup)
      Dim matchDetail As MatchEndpoint.MatchDetail = APIHelper.API_GET_MATCH_INFO(matchID, LoadFast)

      If matchDetail Is Nothing Then
         Return Nothing
      End If

      Dim participantsByLane = From p In matchDetail.Participants
                               Group p By Lane = p.Timeline.Lane Into groups = Group
                               Select Lane, groups
      Dim winningTeam = If(matchDetail.Teams(0).Winner, matchDetail.Teams(0).TeamId, matchDetail.Teams(1).TeamId)
      Dim matchupList As New List(Of Matchup)

      For Each item In participantsByLane
         For Each i In item.groups
            Dim myChampion As Integer = i.ChampionId
            Dim myEnemy As Integer = 0
            Dim wonLane As Boolean = i.TeamId = winningTeam
            Dim newMatchup As Matchup

            Dim numEnemies As Integer = 0

            ' Iterate through every other laner
            For Each a In item.groups
               ' Participant on the opposite team is an enemy, add all of them as new matchups
               If i.TeamId <> a.TeamId Then
                  numEnemies += 1
                  myEnemy = a.ChampionId

                  ' We create a matchup for each champion in the lane (allies and enemies). 4 matchups if 2v2
                  newMatchup = New Matchup(matchID, myChampion, myEnemy, item.Lane, wonLane, i.TeamId)
                  matchupList.Add(newMatchup)
               End If
            Next

            ' Xv0 Lane
            If numEnemies = 0 Then
               matchupList.Add(New Matchup(matchID, myChampion, 0, item.Lane, wonLane, i.TeamId))
            End If
         Next
      Next

      Return matchupList
   End Function
End Class

<Serializable>
Class DataCache
   Inherits EasyCache

   Public Overrides ReadOnly Property CACHE_FILE_NAME As String
      Get
         Return "DataCache.bin"
      End Get
   End Property

   Public Overrides ReadOnly Property CACHE_LIMIT As Integer
      Get
         Return 0
      End Get
   End Property

   Private FirstMatchID As Integer = 0
   Private LastMatchID As Integer = 0
   Private LoadIndex As Integer = -1

   Overrides ReadOnly Property ShouldRebuildCache() As Boolean
      Get
         Return False
      End Get
   End Property

   <NonSerialized>
   Private _CacheChanged As Boolean = False

   Private LoadedMatchupAdded As New Matchup(0, 0, 0, 0, False, 0)
   Private LatestMatchupAdded As New Matchup(0, 0, 0, 0, False, 0)

   ' Always save by default.
   Overrides ReadOnly Property CacheChanged() As Boolean
      Get
         Return Not LatestMatchupAdded.Equals(LoadedMatchupAdded)
      End Get
   End Property

   Public Overrides Sub StartedStoring()
      LoadedMatchupAdded = LatestMatchupAdded
   End Sub

   Sub New()
   End Sub

   Overrides Sub FinishedLoading()
      LoadedMatchupAdded = LatestMatchupAdded
   End Sub

   Private MatchupData As New Dictionary(Of String, List(Of Matchup))

   Public Sub AddMatchupData(ByRef matchups As IEnumerable(Of Matchup))
      SyncLock MatchupData
         For Each m In matchups
            If MatchupData.ContainsKey(APIHelper.GetChampName(m.ChampionID)) Then
               MatchupData(APIHelper.GetChampName(m.ChampionID)).Add(m)
            Else
               MatchupData.Add(APIHelper.GetChampName(m.ChampionID), New List(Of Matchup)({m}))
            End If
            LatestMatchupAdded = m
         Next
      End SyncLock
   End Sub

   ' Compute comprehensive matchup data for a given champion.
   Public Function GetMatchupDataFor(ByVal champName As String) As List(Of Matchup)
      If MatchupData.ContainsKey(champName) Then
         Return MatchupData(champName)
      End If
      Return Nothing
   End Function
End Class