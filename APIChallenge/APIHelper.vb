Imports RiotSharp

Public Class APIHelper
   Private Const API_KEY_FILE As String = "riot_key"
   Public Const API_DELAY As Integer = 1250
   Public Const API_FULL_DELAY As Integer = 12500

   Private Shared API_KEY As String = ""
   Private Shared CDN_URL As String = ""


   Private Shared Sub API_LOAD_FILE()
      Using reader As New IO.StreamReader(APIHelper.API_KEY_FILE)
         APIHelper.API_KEY = reader.ReadLine().Trim
      End Using
   End Sub

   Public Shared Function API_GET_URF_MATCHES(ByVal arg1 As String) As IEnumerable(Of Integer)
      Dim request As Net.WebRequest = Net.WebRequest.Create("https://na.api.pvp.net/api/lol/na/v4.1/game/ids?beginDate=" & arg1 & "&api_key=" & API_KEY)
      Dim response As Net.WebResponse
      Try
         response = request.GetResponse()
      Catch ex As Net.WebException
         Console.WriteLine("Error : " & ex.Message)
         'CurrentStatusLabel.Text = "Error : " & ex.Message
         Console.WriteLine("URL: " & request.RequestUri.ToString)
         Return Nothing
      End Try

      Dim reader As New IO.StreamReader(response.GetResponseStream())
      Dim result As String = reader.ReadToEnd

      Dim gameList As New List(Of Integer)
      For Each i As String In result.Substring(1, result.Length - 2).Split(","c)
         If i.Length > 0 Then
            gameList.Add(CInt(i))
         End If
      Next

      reader.Close()
      response.Close()
      Return gameList
   End Function

   Public Shared Function API_GET_MATCH_INFO(ByVal arg1 As Integer) As MatchEndpoint.MatchDetail
      Dim api = RiotApi.GetInstance(API_KEY)
      Try
         Dim match = api.GetMatch(RiotSharp.Region.na, arg1, False)
         Return match
      Catch ex As RiotSharpException
         Console.WriteLine("Riot Sharp Error: " & ex.Message)
         Return Nothing
      End Try
   End Function

   Public Shared Sub API_INIT()
      If API_KEY = "" Or CDN_URL = "" Then
         API_LOAD_FILE()
      End If
   End Sub

   Private Shared Sub API_STATIC_LOAD_CDN_URL()
      Dim api = StaticRiotApi.GetInstance(API_KEY)
      Dim realm = api.GetRealm(RiotSharp.Region.na)
      CDN_URL = realm.Cdn & "/" & realm.Dd & "/img/champion/"
   End Sub

   Public Shared Champions As Dictionary(Of Integer, StaticDataEndpoint.ChampionStatic)
   Public Shared ChampionsRaw As StaticDataEndpoint.ChampionListStatic

   Public Shared Function GetChampName(ByVal n As Integer) As String
      Return Champions(n).Name
   End Function

   Public Shared Function GetChampID(ByVal name As String) As Integer
      Dim q = From c In Champions
              Where c.Value.Name = name
              Select c.Key
      Return q(0)
   End Function

   Public Shared Sub API_STATIC_LOAD_CHAMPION_INFO()
      If Champions IsNot Nothing Then
         Return
      End If

      If CDN_URL = Nothing Or CDN_URL = "" Then
         API_STATIC_LOAD_CDN_URL()
      End If

      Champions = New Dictionary(Of Integer, StaticDataEndpoint.ChampionStatic)

      Dim api = StaticRiotApi.GetInstance(API_KEY)

      Try
         Dim curTime = DateTime.Now
         Dim championQuery = api.GetChampions(RiotSharp.Region.na, StaticDataEndpoint.ChampionData.all, )
         ChampionsRaw = championQuery
         Dim sorted = (From p In championQuery.Champions()
                      Order By p.Value.Name Ascending
                      Select p.Value).ToList()
         For Each c In sorted
            Champions.Add(c.Id, c)

            ' Workaround hack for serialization error (issue #124 in BenFradet/RiotSharp)
            For Each s In c.Spells
               With s
                  .Range = Nothing
                  .Vars = Nothing
               End With
            Next
         Next

      Catch ex As RiotSharpException
         Console.WriteLine("Riot Sharp Error: " & ex.Message)
      End Try
   End Sub

   ' NOT a cached function! Will probably break if there's an error (?)
   Public Shared Function API_GET_IMAGE_FOR(ByVal champID As Integer) As Bitmap
      Return New Bitmap(New IO.MemoryStream(New System.Net.WebClient().DownloadData(CDN_URL & Champions(champID).Image.Full)))
   End Function
End Class

Module APIExtension
   <System.Runtime.CompilerServices.Extension()>
   Function Sort(query As IEnumerable(Of StaticDataEndpoint.ChampionStatic)) As IEnumerable(Of StaticDataEndpoint.ChampionStatic)
      Return From ret In query
             Order By ret.Name Descending
             Select ret
   End Function
End Module
