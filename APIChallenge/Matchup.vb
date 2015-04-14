Imports RiotSharp

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

   ' Compute comprehensive matchup data for a given champion.
   Public Shared Function GetMatchupDataFor(ByVal champName As String) As List(Of Matchup)
      Dim MatchupList As New List(Of Matchup)

      Dim champID As Integer = APIHelper.GetChampID(champName)
      Dim now As DateTime = DateTime.Now
      Dim q = From match In CacheManager.RetrieveCache(Of MatchCache).MatchList, p In match.GetMatchInfo.Participants,
               match2 In CacheManager.RetrieveCache(Of MatchCache).MatchList, p2 In match2.GetMatchInfo.Participants
               Where match.GetMatchID = match2.GetMatchID AndAlso p.ParticipantId <> p2.ParticipantId AndAlso p.Timeline.Lane = p2.Timeline.Lane AndAlso p.ChampionId = champID AndAlso p.TeamId <> p2.TeamId
               Select match.GetMatchID, p.TeamId, BlueWon = match.GetMatchInfo().Teams(0).Winner, BlueTeamID = match.GetMatchInfo().Teams(0).TeamId, OtherChamp = p2.ChampionId, p.Timeline.Lane
      For Each item In q
         Dim wonGame As Boolean = False
         If item.TeamId = item.BlueTeamID Then
            If item.BlueWon Then
               wonGame = True
            Else
            End If
         ElseIf Not item.BlueWon Then
            wonGame = True
         End If

         'If item.GetMatchID = 1791704542 Then
         '   Console.WriteLine("Here")
         'End If

         Dim setSecondEnemy As Boolean = False
         Dim result As Matchup = Nothing
         For Each m In MatchupList
            result = m.SetEnemy2IfSameMatch(item.GetMatchID, item.OtherChamp, item.TeamId)
            If result IsNot Nothing Then
               setSecondEnemy = True
               Exit For
            End If
         Next
         If setSecondEnemy Then
            MatchupList.Add(result)
            Continue For
         End If

         Dim q2 = From match In CacheManager.RetrieveCache(Of MatchCache).MatchList, p In match.GetMatchInfo.Participants
                  Where match.GetMatchID = item.GetMatchID AndAlso p.ChampionId <> champID AndAlso p.TeamId = item.TeamId AndAlso p.Timeline.Lane = item.Lane
                  Select p.ChampionId
         Dim allyChamp = 0
         If q2.Count > 0 Then
            allyChamp = q2(0)
         End If

         MatchupList.Add(New Matchup(item.GetMatchID, champID, allyChamp, item.OtherChamp, 0, item.Lane, wonGame, item.TeamId))
      Next
      Console.WriteLine("That took: " & DateTime.Now.Subtract(now).ToString & " to complete")
      Return MatchupList
   End Function
End Class