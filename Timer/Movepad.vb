Public Class Movepad
    Private Sub UnitButton_Click(sender As Object, e As EventArgs) Handles UnitButton.Click
        Me.TopMost = False
        UnitButton.Text = InputBox("いくつ単位で移動させますか?")
        Me.TopMost = True
        If IsNumeric(UnitButton.Text) = False Then UnitButton.Text = 1
    End Sub

    Private Sub UpButton_Click(sender As Object, e As EventArgs) Handles UpButton.Click
        Main.Top = Main.Top - UnitButton.Text
    End Sub

    Private Sub DownButton_Click(sender As Object, e As EventArgs) Handles DownButton.Click

        Main.Top = Main.Top + UnitButton.Text
    End Sub

    Private Sub LeftButton_Click(sender As Object, e As EventArgs) Handles LeftButton.Click

        Main.Left = Main.Left - UnitButton.Text
    End Sub

    Private Sub RightButton_Click(sender As Object, e As EventArgs) Handles RightButton.Click
        Main.Left = Main.Left + UnitButton.Text
    End Sub
End Class