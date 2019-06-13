Imports Newtonsoft.Json
Public Class NextTimeForm
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim w As Integer = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width
        Me.StartPosition = FormStartPosition.Manual
        If Main.Left + Me.Width > w Then Me.Left = w - Me.Width + 5 Else Me.Left = Main.Left
        Me.Top = Main.Top + Main.Height + 2
        Dim term = Main.CheckTerm()
        Dim TimeTable = JsonConvert.DeserializeObject(Of RootTimeTable)(My.Settings.TimeTable)
        For i As Integer = 0 To TimeTable.timetable.Count - 1
            If TimeTable.timetable(i).week = Weekday(Today, FirstDayOfWeek.Monday) And TimeTable.timetable(i).term = term Then
                ShowNextTimeLabel.Text = TimeTable.timetable(i).lesson_name
                RoomLabel.Text = TimeTable.timetable(i).room
            End If
        Next
        Me.Text = "次の時間 (Week : " & Weekday(Today, FirstDayOfWeek.Monday) & ",Term : " & term & ")"
    End Sub
End Class
Public Class RootTimeTable
    Public Property timetable As TimeTable()
End Class

Public Class TimeTable
    Public Property id As Integer
    Public Property week As Integer
    Public Property term As Integer
    Public Property lesson_name As String
    Public Property room As String
    Public Property teachers As Teachers()
End Class

Public Class Teachers
    Public Property first_name As String
    Public Property family_name As String
End Class