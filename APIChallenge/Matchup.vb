Imports RiotSharp

<Serializable>
Public Class Matchup
   Public MatchID As Integer

   Public ChampionID As Integer
   Public AllyChampionID As Integer
   Public EnemyChampionID As Integer
   Public EnemyChampionID2 As Integer

   Public Team As Integer
   Public Lane As MatchEndpoint.Lane
   Public WonLane As Boolean

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

End Class

<Serializable>
Public Class WinRateMatchup
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

   Public Shared Function GetWinRateDataFor(ByVal champID As Integer, ByRef currentMatchups As List(Of Matchup), Optional ByVal orderDesc As Boolean = True) As IEnumerable(Of WinRateMatchup)
      Dim winRates As New List(Of WinRateMatchup)

      Dim groupedMatches = From m In currentMatchups
              Group m By OtherChampID = m.EnemyChampionID Into groups = Group
              Select OtherChampID, groups

      For Each championGroup In groupedMatches
         Dim wonGames = Aggregate m In championGroup.groups
                        Where m.WonLane
                        Into Count()
         Dim totalGames = championGroup.groups.Count()

         winRates.Add(New WinRateMatchup(champID, championGroup.OtherChampID, wonGames, totalGames))
      Next

      If orderDesc Then
         Return (From w In winRates
                 Order By w.WinRate Descending, w.TotalGames Descending, APIHelper.GetChampName(w.EnemyChampionID) Ascending
                 Select w)
      Else
         Return (From w In winRates
                 Order By w.WinRate Ascending, w.TotalGames Descending, APIHelper.GetChampName(w.EnemyChampionID) Ascending
                 Select w)
      End If
   End Function
End Class