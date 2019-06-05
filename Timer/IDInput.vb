Imports System.ComponentModel

Public Class IDInput

    Private Sub IDInput_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub IDInput_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If IsNumeric(TextBox1.Text) Then
            My.Settings.ID = TextBox1.Text
            If IsNumeric(TextBox2.Text) Then
                My.Settings.PASS = TextBox2.Text
                MessageBox.Show("保存しました", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("パスワードエラー。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("IDエラー。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If sender.text = "*" Then
            TextBox2.PasswordChar = ""
            sender.text = "a"
        Else
            TextBox2.PasswordChar = "*"
            sender.text = "*"
        End If
    End Sub
End Class