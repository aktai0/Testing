Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports RiotSharp

<Serializable()>
Class MatchCache
   Inherits List(Of Match)
   Const CACHE_FILE_NAME As String = "MatchCache.bin"
   Public Const CACHE_LIMIT As Integer = 100
   'Shared LastURFAPICall As DateTime

   Private _LoadedIndex As Integer = -1
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
      Me.Trim()
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

   ' Removes earlier items until the cache is at the cache limit
   Public Sub Trim()
      If Me.Count > CACHE_LIMIT Then
         Dim amount As Integer = Me.Count - CACHE_LIMIT

         Me.RemoveRange(0, amount)
         ' LoadedIndex doesn't necessarily keep up with Me.Count, so we need to check for negatives
         Me._LoadedIndex -= amount
         Me.LoadedIndex = Math.Max(Me.LoadedIndex, -1)

         RaiseEvent CountChanged()
      End If
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

   ' Loads a MatchCache object with all the matches in the cache file
   Shared Sub LoadCacheFileInto(ByRef givenCache As MatchCache)
      If File.Exists(CACHE_FILE_NAME) Then
         Dim TestFileStream As Stream = File.OpenRead(CACHE_FILE_NAME)
         Dim deserializer As New BinaryFormatter
         givenCache = CType(deserializer.Deserialize(TestFileStream), MatchCache)
         TestFileStream.Close()
      Else
      End If
   End Sub

   Shared Sub StoreCacheFile(ByRef givenCache As MatchCache)
      Dim TestFileStream As Stream = File.Create(CACHE_FILE_NAME)
      Dim serializer As New BinaryFormatter
      serializer.Serialize(TestFileStream, givenCache)
      TestFileStream.Close()
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