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

   Public Sub StoreAllCaches()
      For Each cache In CacheList.Values
         If cache.CacheChanged Then
            StoreCacheFile(cache.CACHE_FILE_NAME, cache)
         End If
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

   Public Champions As Dictionary(Of Integer, StaticDataEndpoint.ChampionStatic)
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

   Public ReadOnly Property MatchIDCount As Integer
      Get
         Return MatchIDQueue.Count + TotalMatchesLoaded
      End Get
   End Property

   Public ReadOnly Property MatchesPendingLoad As Boolean
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
         Next
      End SyncLock
      IncrementTime()
   End Sub

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

   Private Function LoadMatch(ByVal matchID As Integer) As IEnumerable(Of Matchup)
      Dim matchDetail As MatchEndpoint.MatchDetail = APIHelper.API_GET_MATCH_INFO(matchID)

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
            Dim myAlly As Integer = 0
            Dim myEnemy As Integer = 0
            Dim myEnemy2 As Integer = 0
            Dim wonLane As Boolean = i.TeamId = winningTeam

            For Each a In item.groups
               ' Participant on the same time with a different champion is an ally
               If i.TeamId = a.TeamId AndAlso myChampion <> a.ChampionId Then
                  myAlly = a.ChampionId
               End If

               ' Participant on the opposite team is an enemy, add both of them
               If i.TeamId <> a.TeamId Then
                  If myEnemy <> 0 Then
                     myEnemy2 = a.ChampionId
                  Else
                     myEnemy = a.ChampionId
                  End If
               End If
            Next

            ' We create a matchup for each champion in the lane (allies and enemies). 4 matchups if 2v2
            Dim newMatchup As New Matchup(matchID, myChampion, myAlly, myEnemy, myEnemy2, item.Lane, wonLane, i.TeamId)

            matchupList.Add(newMatchup)
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

   Private LoadedMatchupAdded As New Matchup(0, 0, 0, 0, 0, 0, False, 0)
   Private LatestMatchupAdded As New Matchup(0, 0, 0, 0, 0, 0, False, 0)

   ' Always save by default.
   Overrides ReadOnly Property CacheChanged() As Boolean
      Get
         Return Not LatestMatchupAdded.Equals(LoadedMatchupAdded)
      End Get
   End Property

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

      'Dim MatchupList As New List(Of Matchup)

      'Dim champID As Integer = APIHelper.GetChampID(champName)
      'Dim now As DateTime = DateTime.Now
      'Dim allChampMatches = From match In CacheManager.RetrieveCache(Of MatchCache).MatchList, p In match.GetMatchInfo.Participants,
      '         match2 In CacheManager.RetrieveCache(Of MatchCache).MatchList, p2 In match2.GetMatchInfo.Participants
      '         Where match.GetMatchID = match2.GetMatchID AndAlso p.ParticipantId <> p2.ParticipantId AndAlso p.Timeline.Lane = p2.Timeline.Lane AndAlso p.ChampionId = champID AndAlso p.TeamId <> p2.TeamId
      '         Select match, match.GetMatchID, p.TeamId, BlueWon = match.GetMatchInfo().Teams(0).Winner, BlueTeamID = match.GetMatchInfo().Teams(0).TeamId, OtherChamp = p2.ChampionId, p.Timeline.Lane

      'Dim filteredMatches = From m In allChampMatches Select m.GetMatchID, m.TeamId, m.BlueTeamID, m.BlueWon, m.OtherChamp, m.Lane

      'For Each item In filteredMatches
      '   Dim wonGame As Boolean = False
      '   If item.TeamId = item.BlueTeamID Then
      '      If item.BlueWon Then
      '         wonGame = True
      '      Else
      '      End If
      '   ElseIf Not item.BlueWon Then
      '      wonGame = True
      '   End If

      '   Dim setSecondEnemy As Boolean = False
      '   Dim result As Matchup = Nothing
      '   For Each m In MatchupList
      '      result = m.SetEnemy2IfSameMatch(item.GetMatchID, item.OtherChamp, item.TeamId)
      '      If result IsNot Nothing Then
      '         setSecondEnemy = True
      '         Exit For
      '      End If
      '   Next
      '   If setSecondEnemy Then
      '      MatchupList.Add(result)
      '      Continue For
      '   End If

      '   Dim q2 = From match In CacheManager.RetrieveCache(Of MatchCache).MatchList, p In match.GetMatchInfo.Participants
      '            Where match.GetMatchID = item.GetMatchID AndAlso p.ChampionId <> champID AndAlso p.TeamId = item.TeamId AndAlso p.Timeline.Lane = item.Lane
      '            Select p.ChampionId
      '   Dim allyChamp = 0
      '   If q2.Count > 0 Then
      '      allyChamp = q2(0)
      '   End If

      '   MatchupList.Add(New Matchup(item.GetMatchID, champID, allyChamp, item.OtherChamp, 0, item.Lane, wonGame, item.TeamId))
      'Next
      ''Console.WriteLine("That took: " & DateTime.Now.Subtract(now).ToString & " to complete")

      'SyncLock MatchupData
      '   ' Check for race problem (if both threads check for the same champion, we can just return one).
      '   If MatchupData.ContainsKey(champName) Then
      '      Return MatchupData(champName)
      '   End If

      '   MatchupData.Add(champName, MatchupList)
      'End SyncLock
      'Return MatchupList
   End Function
End Class