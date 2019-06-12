Imports System.ComponentModel

Public Class IDInput

    Private Sub IDInput_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If IsNumeric(InputIDTextBox.Text) Then
            My.Settings.ID = InputIDTextBox.Text
            If IsNumeric(InputPassTextBox.Text) Then
                My.Settings.Password = InputPassTextBox.Text
                MessageBox.Show("保存しました", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("パスワードエラー。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("IDエラー。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles ShowPassButton.Click
        If sender.text = "*" Then
            InputPassTextBox.PasswordChar = ""
            sender.text = "a"
        Else
            InputPassTextBox.PasswordChar = "*"
            sender.text = "*"
        End If
    End Sub

    Private Sub IDInput_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class