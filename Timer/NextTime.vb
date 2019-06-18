Imports Newtonsoft.Json
Public Class NextTimeForm
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim w As Integer = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width
        Me.StartPosition = FormStartPosition.Manual
        If Main.Left + Me.Width > w Then Me.Left = w - Me.Width + 5 Else Me.Left = Main.Left
        Me.Top = Main.Top + Main.Height + 2
        Dim term = Main.CheckTerm()
        Dim TimeTable = JsonConvert.DeserializeObject(Of RootTimeTable)(My.Settings.TimeTable)
        For i As Integer = 0 To TimeTable.Timetable.Count - 1
            If TimeTable.Timetable(i).Week = Weekday(Today, FirstDayOfWeek.Monday) And TimeTable.Timetable(i).Term = term Then
                ShowNextTimeLabel.Text = TimeTable.Timetable(i).LessonName
                RoomLabel.Text = TimeTable.Timetable(i).Room
            End If
        Next
        Me.Text = "次の時間 (Week : " & Weekday(Today, FirstDayOfWeek.Monday) & ",Term : " & term & ")"
    End Sub
End Class
Public Class RootTimeTable
    Public Property Timetable As TimeTable()
End Class

Public Class TimeTable
    Public Property Id As Integer
    Public Property Week As Integer
    Public Property Term As Integer
    Public Property LessonName As String
    Public Property Room As String
    Public Property Teachers As Teachers()
End Class

Public Class Teachers
    Public Property FirstName As String
    Public Property FamilyName As String
End Class