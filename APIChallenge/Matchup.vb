Imports RiotSharp

<Serializable>
Public Class Matchup
   Public Const UNPOPULAR_CHAMPION_CUTOFF = 0.05 ' Cutoff the bottom 5%

   Public MatchID As Integer

   Public ChampionID As Integer
   Public AllyChampionID As Integer
   Public EnemyChampionID As Integer
   Public EnemyChampionID2 As Integer

   Public Team As Integer
   Public Lane As MatchEndpoint.Lane
   Public WonLane As Boolean

   Public Overrides Function Equals(obj As Object) As Boolean
      If obj Is Nothing Then
         Return False
      End If
      If Not TypeOf (obj) Is Matchup Then
         Return Nothing
      End If

      Dim other As Matchup = CType(obj, Matchup)
      Return Me.MatchID = other.MatchID AndAlso Me.ChampionID = other.ChampionID AndAlso Me.AllyChampionID = other.AllyChampionID AndAlso Me.EnemyChampionID = other.EnemyChampionID
   End Function

   Public Sub New(ByVal match As Integer, ByVal cID As Integer, ByVal acID As Integer, ByVal eID As Integer, ByVal eID2 As Integer, ByVal ln As MatchEndpoint.Lane, ByVal won As Boolean, ByVal tm As Integer)
      With Me
         .MatchID = match
         .ChampionID = cID
         .AllyChampionID = acID
         .EnemyChampionID = eID
         .EnemyChampionID2 = eID2
         .Lane = ln
         .WonLane = won
         .Team = tm
      End With
   End Sub

   Private Sub OrganizeEnemies()
      If EnemyChampionID > EnemyChampionID2 AndAlso EnemyChampionID2 <> 0 Then
         Dim t As Integer = EnemyChampionID
         EnemyChampionID = EnemyChampionID2
         EnemyChampionID2 = t
      End If
   End Sub

   Public Overrides Function ToString() As String
      Dim allyStr As String = ""
      If AllyChampionID <> 0 Then
         allyStr = " and " & APIHelper.GetChampName(AllyChampionID)
      End If

      Dim enemy2Str As String = ""
      If EnemyChampionID2 <> 0 Then
         enemy2Str = " and " & APIHelper.GetChampName(EnemyChampionID2)
      End If

      Return APIHelper.GetChampName(ChampionID) & allyStr & " vs " & APIHelper.GetChampName(EnemyChampionID) & enemy2Str & " in " & Lane.ToString & ": " & If(WonLane, "Won!", "Lost!") & " (Match: " & MatchID & ")"
   End Function

   ' Returns a copy of the Matchup with enemies reversed if there's a match, and also sets
   '  the enemy2 in this matchup
   Public Function SetEnemy2IfSameMatch(ByVal givenMatchID As Integer, ByVal e2 As Integer, ByVal tm As Integer) As Matchup
      If (givenMatchID = Me.MatchID AndAlso Me.Team = tm) Then
         EnemyChampionID2 = e2
         OrganizeEnemies()
         Return New Matchup(Me.MatchID, Me.ChampionID, Me.AllyChampionID, EnemyChampionID2, Me.EnemyChampionID, Me.Lane, Me.WonLane, Me.Team)
      End If
      Return Nothing
   End Function


   Public Shared Function GetWinRateOfChamp(ByVal champID As Integer) As ChampHistory
      If RetrieveCache(Of DataCache).GetMatchupDataFor(APIHelper.GetChampName(champID)) Is Nothing Then
         Return New ChampHistory(champID, 0, 0)
      End If
      ' Count how many matches the champion won in
      Dim winNum = (From matches In RetrieveCache(Of DataCache).GetMatchupDataFor(APIHelper.GetChampName(champID))
                    Where matches.WonLane
                    Select matches).Count()
      ' Count how many matches the champion was in
      Dim totalGames = RetrieveCache(Of DataCache).GetMatchupDataFor(APIHelper.GetChampName(champID)).Count
      Return New ChampHistory(champID, winNum, totalGames)
   End Function


   Public Shared Function GetWinRatesForAllChampions(ByVal filterUnpopular As Boolean, Optional ByVal sortMode As ListSortMode = ListSortMode.WinRate) As IEnumerable(Of ChampHistory)
      Dim champHistoryList As New List(Of ChampHistory)
      For Each champ In APIHelper.Champions.Keys
         champHistoryList.Add(Matchup.GetWinRateOfChamp(champ))
      Next

      Dim query As IEnumerable(Of ChampHistory)
      If filterUnpopular Then
         If sortMode = ListSortMode.WinRate Then
            query = From c In champHistoryList, names In APIHelper.Champions.Values
                        Where c.champID = names.Id AndAlso c.gamesPlayed / RetrieveCache(Of MatchIDCache).TotalMatchesLoaded > UNPOPULAR_CHAMPION_CUTOFF
                        Order By c.GetWinRate() Descending, c.gamesPlayed Descending, c.champName Ascending
                        Select c
         ElseIf sortMode = ListSortMode.LossRate Then
            query = From c In champHistoryList, names In APIHelper.Champions.Values
                        Where c.champID = names.Id AndAlso c.gamesPlayed / RetrieveCache(Of MatchIDCache).TotalMatchesLoaded > UNPOPULAR_CHAMPION_CUTOFF
                        Order By c.GetWinRate() Ascending, c.gamesPlayed Descending, c.champName Ascending
                        Select c
         Else
            query = From c In champHistoryList, names In APIHelper.Champions.Values
                        Where c.champID = names.Id AndAlso c.gamesPlayed / RetrieveCache(Of MatchIDCache).TotalMatchesLoaded > UNPOPULAR_CHAMPION_CUTOFF
                        Order By c.gamesPlayed Descending, c.GetWinRate Descending, c.champName Ascending
                        Select c
         End If
      Else
         If sortMode = ListSortMode.WinRate Then
            query = From c In champHistoryList, names In APIHelper.Champions.Values
                        Where c.champID = names.Id
                        Order By c.GetWinRate() Descending, c.gamesPlayed Descending, c.champName Ascending
                        Select c
         ElseIf sortMode = ListSortMode.LossRate Then
            query = From c In champHistoryList, names In APIHelper.Champions.Values
                        Where c.champID = names.Id
                        Order By c.GetWinRate() Ascending, c.gamesPlayed Descending, c.champName Ascending
                        Select c
         Else
            query = From c In champHistoryList, names In APIHelper.Champions.Values
                        Where c.champID = names.Id
                        Order By c.gamesPlayed Descending, c.GetWinRate Descending, c.champName Ascending
                        Select c
         End If
      End If
      Return query
   End Function
End Class

<Serializable>
Public Class WinRateMatchup
   Public Const UNPOPULAR_MATCHUP_CUTOFF = 0.01 ' Cut off lowest 1% of matchups

   Public ChampionID As Integer
   Public EnemyChampionID As Integer

   Public WinNum As Integer
   Public TotalGames As Integer

   Public WinRate As Single

   Sub New(ByVal cID As Integer, ByVal eID As Integer, ByVal wins As Integer, ByVal gms As Integer)
      With Me
         .ChampionID = cID
         .EnemyChampionID = eID
         .WinNum = wins
         .TotalGames = gms
      End With

      WinRate = CSng(WinNum) / TotalGames
   End Sub

   Public Shared Function GetWinRateDataFor(ByVal champID As Integer, ByRef currentMatchups As List(Of Matchup), ByVal sortMode As ListSortMode, ByVal filterPopular As Boolean) As IEnumerable(Of WinRateMatchup)
      Dim winRates As New List(Of WinRateMatchup)

      Dim groupedMatches = From m In currentMatchups
              Group m By OtherChampID = m.EnemyChampionID Into groups = Group
              Select OtherChampID, groups

      For Each championGroup In groupedMatches
         Dim wonGames = Aggregate m In championGroup.groups
                        Where m.WonLane
                        Into Count()
         Dim totalGames = championGroup.groups.Count()

         ' If it's a mirror matchup, only display as half the number of games (for obvious reasons).
         If championGroup.OtherChampID = champID Then
            totalGames = CInt(totalGames / 2)
            wonGames = CInt(wonGames / 2)
         End If

         If filterPopular Then
            If totalGames / currentMatchups.Count <= UNPOPULAR_MATCHUP_CUTOFF Then
               Continue For
            End If
         End If

         winRates.Add(New WinRateMatchup(champID, championGroup.OtherChampID, wonGames, totalGames))
      Next

      If sortMode = ListSortMode.WinRate Then
         Return (From w In winRates
                 Where w.EnemyChampionID <> 0
                 Order By w.WinRate Descending, w.TotalGames Descending, APIHelper.GetChampName(w.EnemyChampionID) Ascending
                 Select w)
      ElseIf sortMode = ListSortMode.LossRate Then
         Return (From w In winRates
                 Where w.EnemyChampionID <> 0
                 Order By w.WinRate Ascending, w.TotalGames Descending, APIHelper.GetChampName(w.EnemyChampionID) Ascending
                 Select w)
      ElseIf sortMode = ListSortMode.Alphabetical Then
         Return (From w In winRates
                 Where w.EnemyChampionID <> 0
                 Order By APIHelper.GetChampName(w.EnemyChampionID) Ascending
                 Select w)
      Else
         Return (From w In winRates
                 Where w.EnemyChampionID <> 0
                 Order By w.TotalGames Descending, w.WinRate Descending, APIHelper.GetChampName(w.EnemyChampionID) Ascending
                 Select w)
      End If
   End Function
End Class

Public Class ChampHistory
   Public champID As Integer
   Public gamesWon As Integer
   Public gamesPlayed As Integer
   Public champName As String

   Private _WinRate As Single

   Public Function GetWinRate() As Single
      Return _WinRate
   End Function

   Sub New(ByVal ID As Integer, ByVal won As Integer, ByVal played As Integer)
      With Me
         .champID = ID
         .gamesWon = won
         .gamesPlayed = played
      End With
      champName = APIHelper.Champions(champID).Name
      _WinRate = CSng(gamesWon) / If(gamesPlayed > 0, gamesPlayed, 1)
   End Sub

   Public Overrides Function ToString() As String
      Return "Champion " & champName & " won " & gamesWon & "/" & gamesPlayed & " (" & GetWinRate() & ")"
   End Function
End Class

Public Enum ListSortMode
   WinRate
   LossRate
   Popularity
   Alphabetical
End Enum