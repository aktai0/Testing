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
      CacheTypeNames.Add("Matches", New MatchCache)
      CacheTypeNames.Add("Data", New DataCache)
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

<Serializable()>
Class MatchCache
   Inherits EasyCache

   Public WithEvents MatchList As New _MatchList

   Public Overrides ReadOnly Property CACHE_FILE_NAME() As String
      Get
         Return "MatchCache.bin"
      End Get
   End Property

   Public Overrides ReadOnly Property CACHE_LIMIT() As Integer
      Get
         Return 500
      End Get
   End Property

   <NonSerialized>
   Private _CacheChanged As Boolean = False
   Public Overrides ReadOnly Property CacheChanged As Boolean
      Get
         Return _CacheChanged
      End Get
   End Property

   'Shared LastURFAPICall As DateTime

   ' Removes earlier items until the cache is at the cache limit
   Public Sub Trim()
      If MatchList.Count > CACHE_LIMIT Then
         Dim amount As Integer = MatchList.Count - CACHE_LIMIT

         MatchList.RemoveRange(0, amount)
         ' LoadedIndex doesn't necessarily keep up with Me.Count, so we need to check for negatives
         MatchList._LoadedIndex -= amount
         MatchList.LoadedIndex = Math.Max(MatchList.LoadedIndex, -1)

         MatchList.ForceCountChangedRefresh()
      End If
   End Sub
End Class

' List class that allows events to be raised when 
<Serializable>
Class _MatchList
   Inherits List(Of Match)

   Friend _LoadedIndex As Integer = -1
   Property LoadedIndex() As Integer
      Get
         Return _LoadedIndex
      End Get
      Set(value As Integer)
         If value <> _LoadedIndex Then
            _LoadedIndex = value
            RaiseEvent LoadChanged()
         End If
      End Set
   End Property

   ' Denotes the last successful time of API call for URF matches
   Private _LastURFAPICall As DateTime
   Property LastURFAPICall() As DateTime
      Get
         Return _LastURFAPICall
      End Get
      Set(ByVal value As DateTime)
         _LastURFAPICall = value
      End Set
   End Property

   <NonSerialized>
   Public Event CountChanged()

   <NonSerialized>
   Public Event LoadChanged()

   Private Function AlreadyHas(ByVal item As Match) As Boolean
      Return Me.Contains(item)
   End Function

   Public Shadows Sub Add(ByVal item As Match)
      If Me.AlreadyHas(item) Then
         Return
      End If
      MyBase.Add(item)
      'Me.Trim()
      RaiseEvent CountChanged()
      RaiseEvent LoadChanged()
   End Sub

   ' Add item without raising any events or trimming
   Public Shadows Sub QuietAdd(ByVal item As Match)
      If Me.AlreadyHas(item) Then
         Return
      End If
      MyBase.Add(item)
   End Sub

   Public Shadows Sub AddRange(ByVal collection As IEnumerable(Of Match))
      Throw New NotSupportedException("AddRange is not implemented for MatchCache")

      For Each i As Match In collection
         If Me.AlreadyHas(i) Then
            Continue For
         Else
            MyBase.Add(i)
         End If
      Next

      RaiseEvent CountChanged()
   End Sub

   Public Shadows Sub Clear()
      MyBase.Clear()
      RaiseEvent CountChanged()
   End Sub

   Public Shadows Sub Insert(index As Integer, item As Match)
      If Me.AlreadyHas(item) Then
         Return
      End If
      MyBase.Insert(index, item)
      RaiseEvent CountChanged()
   End Sub

   Public Shadows Sub Remove(item As Match)
      MyBase.Remove(item)
      RaiseEvent CountChanged()
   End Sub

   Public Shadows Sub RemoveAll(item As Predicate(Of Match))
      MyBase.RemoveAll(item)
      RaiseEvent CountChanged()
   End Sub

   Public Shadows Sub RemoveRange(index As Integer, count As Integer)
      MyBase.RemoveRange(index, count)
      RaiseEvent CountChanged()
   End Sub

   Public Sub ForceCountChangedRefresh()
      RaiseEvent CountChanged()
   End Sub

   Public Sub ForceLoadChangedRefresh()
      RaiseEvent LoadChanged()
   End Sub
End Class

<Serializable()>
Class Match
   Private MatchID As Integer
   Private InfoLoaded As Boolean = False
   Private MatchInfo As MatchEndpoint.MatchDetail

   Public Function GetMatchID() As Integer
      Return MatchID
   End Function

   Public Function GetMatchInfo() As MatchEndpoint.MatchDetail
      Return MatchInfo
   End Function

   Public Sub SetMatchInfo(ByVal info As MatchEndpoint.MatchDetail)
      MatchInfo = info
      If MatchInfo IsNot Nothing Then
         InfoLoaded = True
      End If
   End Sub

   Public Function IsLoaded() As Boolean
      Return InfoLoaded
   End Function

   Sub New(ByVal i As Integer)
      MatchID = i
   End Sub

   Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing Then
         Return False
      End If
      Dim oMatch As Match
      Try
         oMatch = CType(obj, Match)
      Catch ex As Exception
         Return False
      End Try
      Return Me.MatchID = oMatch.MatchID
   End Function

   Public Overrides Function ToString() As String
      Return MatchID.ToString & " (" & If(InfoLoaded, "Loaded", "Not Loaded") & ")"
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
         Dim MatchCache = RetrieveCache(Of MatchCache)()
         Return Not (LoadIndex = MatchCache.MatchList.LoadedIndex AndAlso FirstMatchID = MatchCache.MatchList(0).GetMatchID AndAlso LastMatchID = MatchCache.MatchList(MatchCache.MatchList.Count - 1).GetMatchID)
      End Get
   End Property

   <NonSerialized>
   Private _CacheChanged As Boolean = False
   ' Always save by default.
   Overrides ReadOnly Property CacheChanged() As Boolean
      Get
         Return _CacheChanged
      End Get
   End Property

   Overrides Sub RebuildCacheAsync()
      AsyncBackgroundWorker = New System.ComponentModel.BackgroundWorker()
      AddHandler AsyncBackgroundWorker.DoWork, AddressOf AsyncBackgroundWorker_DoWork

      AsyncBackgroundWorker.RunWorkerAsync()
   End Sub

   <NonSerialized>
   Private AsyncBackgroundWorker As System.ComponentModel.BackgroundWorker

   Private Sub AsyncBackgroundWorker_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs)
      _CacheChanged = True
      Console.WriteLine("Cache changed")
   End Sub

   Private Function SelectFirstChamp(ByVal name As String) As IEnumerable(Of String)

   End Function

   Sub New()
   End Sub

   Overrides Sub FinishedLoading()
   End Sub

End Class