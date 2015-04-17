Public Module TimeHelper
   ' Rounded time used to retrieve games in the API challenge. We use the 
   '  time from at least 10 minutes ago because anything sooner keeps returning 404s for some reason.

   Private ReadOnly _EPOCH_DATETIME As New DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)
   Public ReadOnly Property EPOCH_DATETIME() As DateTime
      Get
         Return _EPOCH_DATETIME
      End Get
   End Property

   Private ReadOnly _FINAL_EPOCH As Integer = 1428918000
   Public ReadOnly Property FINAL_EPOCH() As Integer
      Get
         Return _FINAL_EPOCH
      End Get
   End Property

   Public ReadOnly Property FIRST_URF_DATETIME() As DateTime
      Get
         Return EpochToDateTime(1427865900)
      End Get
   End Property

   Public PreviousRoundedEpochTime As Integer
   Public PreviousRoundedEpochDateTime As DateTime

   Public Sub UpdateLastRoundedDateTime()
      PreviousRoundedEpochTime = DateTimeToEpoch(GetOlderRoundedDateTime(DateTime.UtcNow))
      PreviousRoundedEpochDateTime = GetOlderRoundedDateTime(DateTime.UtcNow)
   End Sub

   ' Flag the API call if the time is newer
   Public ReadOnly Property CanUpdate(ByVal givenDT As DateTime) As Boolean
      Get
         Return Not DateTimeEquals(givenDT, PreviousRoundedEpochDateTime)
      End Get
   End Property

   ' Returns a DateTime rounded down to the nearest 5 minute interval (e.g. 11:05) from the given DateTime,
   '  10 minutes ago
   Public Function GetOlderRoundedDateTime(ByVal curDT As DateTime) As DateTime
      ' Subtract the given time's minutes modulo 5 (to get remainder) and seconds.
      Dim e As New TimeSpan(0, 0, 10, 0, 0)
      Return curDT.Subtract(New TimeSpan(0, 0, curDT.Minute Mod 5, curDT.Second, curDT.Millisecond)).Subtract(e)
   End Function

   Public Function DateTimeToEpoch(ByVal curDT As DateTime) As Integer
      Return CInt((curDT - EPOCH_DATETIME).TotalSeconds)
   End Function

   Public Function EpochToDateTime(ByVal unix As Integer) As DateTime
      Return EPOCH_DATETIME.AddSeconds(unix)
   End Function

   ' Soft equality function because the DateTime class's equals method compares by ticks, which was giving
   '  us weird results.
   Public Function DateTimeEquals(ByVal a As DateTime, ByVal b As DateTime) As Boolean
      Return a.Year = b.Year AndAlso a.Month = b.Month AndAlso a.Day = b.Day AndAlso a.Hour = b.Hour AndAlso a.Minute = b.Minute AndAlso a.Second = b.Second
   End Function

   <System.Runtime.CompilerServices.Extension()>
   Function Average(query As IEnumerable(Of TimeSpan)) As TimeSpan
      Dim totalTime As New TimeSpan(0, 0, 0)

      For Each t As TimeSpan In query
         totalTime += t
      Next

      Return New TimeSpan(CLng(CSng(totalTime.Ticks) / query.Count))
   End Function
End Module
