Public Class Form1
    Public Secsw
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.TopMost = True
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label1_DoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label1.DoubleClick
        If e.Button = MouseButtons.Right Then
            If Me.FormBorderStyle <> FormBorderStyle.None Then
                Me.FormBorderStyle = FormBorderStyle.None
            Else
                Me.FormBorderStyle = FormBorderStyle.Sizable
            End If
        End If
        If e.Button = MouseButtons.Left Then
            If Secsw = False Then
                Secsw = True
            Else
                Secsw = False
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim jikan, sabun
        Dim j1 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 10, 45, 0)
        Dim j2 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 12, 30, 0)
        Dim j3 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 15, 0, 0)
        Dim j4 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 16, 45, 0)
        Dim y1 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 9, 15, 0)
        Dim y2 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 11, 0, 0)
        Dim y3 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 13, 30, 0)
        Dim y4 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 15, 15, 0)
        jikan = DateTime.Now()
        Application.DoEvents()

        If jikan < y1 Then
            sabun = DateDiff("s", y1, jikan)
            ProgressBar1.Maximum = 900
        Else
            If jikan < j1 Then
                sabun = DateDiff("s", j1, jikan)
                ProgressBar1.Maximum = 5400
            Else
                If jikan < y2 Then
                    sabun = DateDiff("s", y2, jikan)
                    ProgressBar1.Maximum = 900
                Else
                    If jikan < j2 Then
                        sabun = DateDiff("s", j2, jikan)
                        ProgressBar1.Maximum = 5400
                    Else
                        If jikan < y3 Then
                            sabun = DateDiff("s", y3, jikan)
                            ProgressBar1.Maximum = 3600
                        Else
                            If jikan < j3 Then
                                sabun = DateDiff("s", j3, jikan)
                                ProgressBar1.Maximum = 5400
                            Else

                                If jikan < y4 Then
                                    sabun = DateDiff("s", y4, jikan)
                                    ProgressBar1.Maximum = 900
                                Else

                                    If jikan < j4 Then
                                        sabun = DateDiff("s", j4, jikan)
                                        ProgressBar1.Maximum = 5400
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        End If

        sabun = sabun * -1
        If sabun <= 60 Then
            Label1.Text = sabun
            Label1.ForeColor = Color.Red
        Else
            If Secsw = True Then
                Label1.Text = sabun
            Else
                Label1.Text = Format(Int(sabun / 60), "00") & ":" & Format(sabun Mod 60, "00")
            End If
            Label1.ForeColor = Color.Black
        End If
        ProgressBar1.Value = ProgressBar1.Maximum - sabun
    End Sub

    Private Sub ProgressBar1_Click(sender As Object, e As EventArgs) Handles ProgressBar1.Click

    End Sub

    Private Sub ProgressBar1_DoubleClick(sender As Object, e As EventArgs) Handles ProgressBar1.DoubleClick
        Me.Close()
    End Sub
End Class
