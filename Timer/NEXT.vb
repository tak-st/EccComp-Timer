Imports Newtonsoft.Json
Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim w As Integer = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width
        Me.StartPosition = FormStartPosition.Manual
        If Form1.Left + Me.Width > w Then Me.Left = w - Me.Width + 5 Else Me.Left = Form1.Left
        Me.Top = Form1.Top + Form1.Height + 2
        Dim j1 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 10, 45, 0)
        Dim j2 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 12, 30, 0)
        Dim j3 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 15, 0, 0)
        Dim j4 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 16, 45, 0)
        Dim y1 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 9, 15, 0)
        Dim y2 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 11, 0, 0)
        Dim y3 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 13, 30, 0)
        Dim y4 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 15, 15, 0)
        Dim jikan = DateTime.Now()
        Dim term = 0
        Application.DoEvents()
        If jikan < y1 Then
            term = 1
        Else
            If jikan < j1 Then
                term = 2
            Else
                If jikan < y2 Then
                    term = 2
                Else
                    If jikan < j2 Then
                        term = 3
                    Else
                        If jikan < y3 Then
                            term = 3
                        Else
                            If jikan < j3 Then
                                term = 4
                            Else

                                If jikan < y4 Then
                                    term = 4
                                Else

                                    If jikan < j4 Then
                                        term = 5
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If

        Dim TimeTable = JsonConvert.DeserializeObject(Of RootTimeTable)(My.Settings.TIME)
        For i As Integer = 0 To TimeTable.timetable.Count - 1
            If TimeTable.timetable(i).week = Weekday(Today, FirstDayOfWeek.Monday) And TimeTable.timetable(i).term = term Then
                Label3.Text = TimeTable.timetable(i).lesson_name
                Label4.Text = TimeTable.timetable(i).room
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