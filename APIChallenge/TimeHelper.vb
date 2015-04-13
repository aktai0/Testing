Public Class TimeHelper
   ' Rounded time used to retrieve games in the API challenge. We use the 
   '  time from at least 10 minutes ago because anything sooner keeps returning 404s for some reason.

   Private ReadOnly _UNIX_DATETIME As DateTime
   Public Shared ReadOnly Property EPOCH_DATETIME() As DateTime
      Get
         Return New DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
      End Get
   End Property

   Public Shared PreviousRoundedEpochTime As Integer
   Public Shared PreviousRoundedEpochDateTime As DateTime

   Public Shared Sub UpdateLastRoundedDateTime()
      PreviousRoundedEpochTime = DateTimeToEpoch(GetOlderRoundedDateTime(DateTime.UtcNow))
      PreviousRoundedEpochDateTime = GetOlderRoundedDateTime(DateTime.UtcNow)
   End Sub

   ' Flag the API call if the time is newer
   Public Shared ReadOnly Property CanUpdate(ByVal givenDT As DateTime) As Boolean
      Get
         Return Not DateTimeEquals(givenDT, PreviousRoundedEpochDateTime)
      End Get
   End Property

   ' Returns a DateTime rounded down to the nearest 5 minute interval (e.g. 11:05) from the given DateTime,
   '  10 minutes ago
   Public Shared Function GetOlderRoundedDateTime(ByVal curDT As DateTime) As DateTime
      ' Subtract the given time's minutes modulo 5 (to get remainder) and seconds.
      Dim e As New TimeSpan(0, 0, 10, 0, 0)
      Return curDT.Subtract(New TimeSpan(0, 0, curDT.Minute Mod 5, curDT.Second, curDT.Millisecond)).Subtract(e)
   End Function

   Public Shared Function DateTimeToEpoch(ByVal curDT As DateTime) As Integer
      Return CInt((curDT - EPOCH_DATETIME).TotalSeconds)
   End Function

   Public Shared Function EpochToDateTime(ByVal unix As Integer) As DateTime
      Return EPOCH_DATETIME.AddSeconds(unix)
   End Function

   ' Soft equality function because the DateTime class's equals method compares by ticks, which was giving
   '  us weird results.
   Public Shared Function DateTimeEquals(ByVal a As DateTime, ByVal b As DateTime) As Boolean
      Return a.Year = b.Year AndAlso a.Month = b.Month AndAlso a.Day = b.Day AndAlso a.Hour = b.Hour AndAlso a.Minute = b.Minute AndAlso a.Second = b.Second
   End Function

End Class
