Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Public Class Main
    Public Secsw, OSw, Oti, sSw, nsize, manual, Nsw, SOK, countti
    Public counttm As DateTime
    Private Sub noti()
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
        Dim ln = "", rm = ""
        Dim TimeTable = JsonConvert.DeserializeObject(Of RootTimeTable)(My.Settings.TimeTable)
        For i As Integer = 0 To TimeTable.timetable.Count - 1
            If TimeTable.timetable(i).week = Weekday(Today, FirstDayOfWeek.Monday) And TimeTable.timetable(i).term = term Then
                ln = TimeTable.timetable(i).lesson_name
                rm = TimeTable.timetable(i).room
            End If
        Next
        If ln = "" Then
            NotifyIcon.BalloonTipTitle = "お疲れ様です。"
            NotifyIcon.BalloonTipText = "次の時間は授業がないようです。"
            If ShowNotifyMenuItem.Checked Then NotifyIcon.ShowBalloonTip(1000)
            Me.Visible = False
        Else
            NotifyIcon.BalloonTipTitle = "お疲れ様です。"
            NotifyIcon.BalloonTipText = "次の時間は " & rm & " 教室で、 " & ln & " です。"
            If ShowNotifyMenuItem.Checked Then NotifyIcon.ShowBalloonTip(1000)
            If ShowPopUpMenuItem.Checked = False Then NextTimeForm.Show()
        End If
    End Sub
    Private Sub ntp()
        'Pingオブジェクトの作成
        Dim p As New System.Net.NetworkInformation.Ping()
        '"www.yahoo.com"にPingを送信する
        Try
            Dim reply As System.Net.NetworkInformation.PingReply = p.Send("www.yahoo.com")
            '結果を取得
            If reply.Status = System.Net.NetworkInformation.IPStatus.Success Then
                '           Console.WriteLine("Reply from {0}:bytes={1} time={2}ms TTL={3}",
                '           reply.Address, reply.Buffer.Length,
                '           reply.RoundtripTime, reply.Options.Ttl)
            Else
                MessageBox.Show("インターネットに接続する必要があります。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        Catch ex As System.Exception
            MessageBox.Show("インターネットに接続する必要があります。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        p.Dispose()
        If System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() Then
        Else
            MessageBox.Show("インターネットに接続する必要があります。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        ' UDP生成
        Dim objSck As System.Net.Sockets.UdpClient
        Dim ipAny As System.Net.IPEndPoint =
        New System.Net.IPEndPoint(
        System.Net.IPAddress.Any, 0)
        objSck = New System.Net.Sockets.UdpClient(ipAny)

        ' UDP送信
        Dim sdat As Byte() = New Byte(47) {}
        sdat(0) = &HB
        objSck.Send(sdat, sdat.GetLength(0),
        "ntp.nict.jp", 123)

        ' UDP受信
        Dim rdat As Byte() = objSck.Receive(ipAny)

        ' 1900年1月1日からの経過時間(日時分秒)
        Dim lngAllS As Long ' 1900年1月1日からの経過秒数
        Dim lngD As Long    ' 日
        Dim lngH As Long    ' 時
        Dim lngM As Long    ' 分
        Dim lngS As Long    ' 秒

        ' 1900年1月1日からの経過秒数
        lngAllS = CLng(
              rdat(40) * Math.Pow(2, (8 * 3)) +
              rdat(41) * Math.Pow(2, (8 * 2)) +
              rdat(42) * Math.Pow(2, (8 * 1)) +
              rdat(43))

        lngD = lngAllS \ (24 * 60 * 60)   ' 日
        lngS = lngAllS Mod (24 * 60 * 60) ' 残りの秒数
        lngH = lngS \ (60 * 60)           ' 時
        lngS = lngS Mod (60 * 60)         ' 残りの秒数
        lngM = lngS \ 60                  ' 分
        lngS = lngS Mod 60                ' 秒

        ' DateTime型への変換
        Dim dtTime As DateTime = "1900/01/01"
        dtTime = dtTime.AddDays(lngD)
        dtTime = dtTime.AddHours(lngH)
        dtTime = dtTime.AddMinutes(lngM)
        dtTime = dtTime.AddSeconds(lngS)
        ' グリニッジ標準時から日本時間への変更
        dtTime = dtTime.AddHours(9)

        ' 現在の日時表示
        Dim ts As TimeSpan = dtTime - Now
        Dim r = (Now - dtTime).ToString("hh\:mm\:ss")
        System.Diagnostics.Trace.WriteLine(dtTime)
        Try
            TimeOfDay = dtTime
            MessageBox.Show(r & " 補正しました", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As System.Security.SecurityException
            MessageBox.Show("時間補正機能を使用するには、" & vbNewLine & "このアプリケーションを管理者権限で起動する必要があります。" & vbNewLine & " (検出された時間のずれ : " & r & ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            r = MessageBox.Show("このソフト内でのみ修正することができます。" & vbNewLine & "修正を行いますか?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If r = vbYes Then
                DeviationToolStripTextBox.Text = Int(ts.TotalSeconds)
            End If
        End Try
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Term5MenuItem.Checked = My.Settings.Term5
        Term6MenuItem.Checked = My.Settings.Term6
        DeviationToolStripTextBox.Text = My.Settings.Deviation
        Me.TopMost = True
        nsize = 48
        Me.Height = Me.Height + 26
        TimerMenuStrip.Visible = True
        TimerLabel.Top = TimerLabel.Top + 26
        TimerBar.Top = TimerBar.Top + 26
        If My.Settings.SavePosX >= 0 Then
            Me.StartPosition = FormStartPosition.Manual
            Me.Left = My.Settings.SavePosX
            Me.Top = My.Settings.SavePosY
            PositionSaveMenuItem.Checked = True
        End If
        If My.Settings.SaveSizeX >= 0 Then
            Me.Width = My.Settings.SaveSizeX
            Me.Height = My.Settings.SaveSizeY
            SizeSaveMenuItem.Checked = True
        End If
        TopmostMenuItem.Checked = My.Settings.TopMost
        ShowBarMenuItem.Checked = My.Settings.ShowBar
        ShowsecMenuItem.Checked = My.Settings.ShowSec
        TransparentMenuItem.Checked = My.Settings.Transparent
        CanSizeChangeMenuItem.Checked = My.Settings.CanSizeChange
        If My.Settings.ID <> "-1" Then
            IDPassMenuItem.Checked = True
            NextTimeMenuItem.Visible = True
            NextTimeMenuItemN.Visible = True
            ShowNotifyMenuItem.Checked = My.Settings.Notify
            ShowPopUpMenuItem.Checked = My.Settings.PopUp
            WebTimer.Start()
        End If
        Call 常に最前面ToolStripMenuItem_Click(Me, e)
        Call 秒数表示ToolStripMenuItem_Click(Me, e)
        Call 固定時マウスオーバーで透明化ToolStripMenuItem_Click(Me, e)
        Call フォームのサイズ変更を可能にするToolStripMenuItem_Click(Me, e)

        Select Case My.Settings.SizeSet
            Case 0
                SettingSeparator1.Visible = False
                AdvancedOnMenuItem.Visible = False
                SizeMenuItem.Enabled = False
                AdvancedMenuItem.Visible = True
                If My.Settings.Padding < 0 Then
                    TimerLabel.Top = 26 - (My.Settings.Padding * -1)
                Else
                    TimerLabel.Padding = New Padding(0, My.Settings.Padding, 0, 0)
                End If
                TimerLabel.Font = My.Settings.SaveFont
                TimerLabel.Height = My.Settings.LabelHeight
                TimerBar.Height = My.Settings.BarHeight
                TimerBar.Top = TimerLabel.Top + TimerLabel.Height + My.Settings.TimerBarWidth
                TimerLabel.Left = My.Settings.LeftSpace
                TimerBar.Left = TimerLabel.Left
                TimerLabel.Width = Me.Width - TimerLabel.Left - My.Settings.RightSpace
                TimerBar.Width = TimerLabel.Width
            Case 1
                Call 小ToolStripMenuItem_Click(Me, e)
            Case 2
                Call コンパクトToolStripMenuItem_Click(Me, e)
            Case 3
                Call 中ToolStripMenuItem_Click(Me, e)
            Case 4
                Call ビッグToolStripMenuItem_Click(Me, e)
        End Select
        Call バー表示ToolStripMenuItem_Click(Me, e)
        manual = True
        Dim fileName As String = "AutoLoad.ast"
        If System.IO.File.Exists(fileName) Then
            Debug.Print("AutoLoad読み込み")
            Call autoload(fileName, e)
        End If
        If My.Settings.Lock Then
            Me.Height = Me.Height - 26
            Me.FormBorderStyle = FormBorderStyle.None
            TimerMenuStrip.Visible = False
            TimerLabel.Top = TimerLabel.Top - 26
            If Me.Width < 40 Then Me.Width = 40
            If Me.Height < 40 Then Me.Height = 40
            TimerBar.Top = TimerBar.Top - 26
            If Oti >= 0 Then Oti = Math.Max(80, Oti)
            OSw = True
            sSw = True
            LockStartMenuItem.Checked = True

        End If
        If My.Settings.BatteryMode Then
            Call バッテリー残量ToolStripMenuItem_Click(sender, e)
        End If
        AutorunMenuItem.Checked = My.Settings.StartUp
        ChangeBatteryMenuItem.Checked = My.Settings.ChangeBattery
        Call バッテリー充電停止時残りバッテリー表示に切り替えるToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub autoload(filename As String, e As EventArgs)
        Dim sr As New System.IO.StreamReader(filename,
            System.Text.Encoding.Default)
        '内容を一行ずつ読み込む
        Dim i = 0
        While sr.Peek() > -1
            i += 1
            Select Case i
                Case 1
                    Dim Namev = sr.ReadLine()
                    Dim Sizev = sr.ReadLine()
                    Dim Sizevs = CSng(Sizev)
                    Dim Stv As FontStyle = sr.ReadLine()
                    TimerLabel.Font = New System.Drawing.Font(Namev, Sizevs, Stv)
                Case 2
                    Dim n = sr.ReadLine()
                    If n <> -1 Then
                        Me.Left = n
                    End If
                Case 3
                    Dim n = sr.ReadLine()
                    If n <> -1 Then
                        Me.Top = n
                    End If
                Case 4
                    Dim n = sr.ReadLine()
                    If n <> -1 Then
                        Me.Width = n
                    End If
                Case 5
                    Dim n = sr.ReadLine()
                    If n <> -1 Then
                        Me.Height = n
                    End If
                Case 6
                    TimerHeightToolStripTextBox.Text = sr.ReadLine()
                    Call ToolStripTextBox1_LostFocus(TimerHeightToolStripTextBox, e)
                Case 7
                    BarHeightToolStripTextBox2.Text = sr.ReadLine()
                    Call ToolStripTextBox2_LostFocus(BarHeightToolStripTextBox2, e)
                Case 8
                    LeftSpaceToolStripTextBox.Text = sr.ReadLine()
                    Call ToolStripTextBox7_Leave(LeftSpaceToolStripTextBox, e)
                Case 9
                    RightSpaceToolStripTextBox.Text = sr.ReadLine()
                    Call ToolStripTextBox8_Leave(RightSpaceToolStripTextBox, e)
                Case 10
                    MarginToolStripTextBox.Text = sr.ReadLine()
                    Call ToolStripTextBox10_LostFocus(MarginToolStripTextBox, e)
                Case 11
                    SpaceWidthToolStripTextBox.Text = sr.ReadLine()
                    Call ToolStripTextBox6_LostFocus(SpaceWidthToolStripTextBox, e)
            End Select
        End While
        '閉じる
        sr.Close()
    End Sub

    Private Sub TimerLabel_DoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles TimerLabel.DoubleClick
        If e.Button = MouseButtons.Right Then
            If Me.FormBorderStyle <> FormBorderStyle.None Then
                Me.Height = Me.Height - 26
                Me.FormBorderStyle = FormBorderStyle.None
                TimerMenuStrip.Visible = False
                TimerLabel.Top = TimerLabel.Top - 26
                TimerBar.Top = TimerBar.Top - 26
                If Oti >= 0 Then Oti = Math.Max(80, Oti)
                OSw = True
                sSw = True
            Else
                Call フォームのサイズ変更を可能にするToolStripMenuItem_Click(Me, e)
                Me.Height = Me.Height + 26
                TimerMenuStrip.Visible = True
                TimerLabel.Top = TimerLabel.Top + 26
                TimerBar.Top = TimerBar.Top + 26
                sSw = False
            End If
        End If
        If e.Button = MouseButtons.Left Then
            If Secsw = False Then
                Secsw = True
                ShowsecMenuItem.Checked = True
            Else
                Secsw = False
                ShowsecMenuItem.Checked = False
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles ClassTimer.Tick
        Dim jikan
        Dim sabun = 0
        Dim infome = ""
        Dim term = 0
        Dim Ntu = False
        If IDPassMenuItem.Checked = True And SOK Then Ntu = True
        Dim j1 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 10, 45, 0)
        Dim j2 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 12, 30, 0)
        Dim j3 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 15, 0, 0)
        Dim j4 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 16, 45, 0)
        Dim j5 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 18, 30, 0)
        Dim j6 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 20, 0, 0)
        Dim y1 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 9, 15, 0)
        Dim y2 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 11, 0, 0)
        Dim y3 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 13, 30, 0)
        Dim y4 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 15, 15, 0)
        Dim y5 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 17, 0, 0)
        Dim tss As New TimeSpan(0, 0, 0, DeviationToolStripTextBox.Text)
        jikan = DateTime.Now() + tss
        Application.DoEvents()
        If OSw = True And Oti < 120 And Oti >= 0 Then
            Oti += 1
            If Oti > 30 And Oti < 80 Then
                Me.Opacity = ((Oti - 30) * 2) / 100
            End If
        Else
            OSw = False
            If Oti >= 0 Then Oti = 0
            Me.Opacity = "1"

        End If
        If jikan < y1 Then
            If Nsw And Ntu Then Nsw = False : Call noti()
            sabun = DateDiff("s", y1, jikan)
            TimerBar.Maximum = 900
            infome = "1時限目待ち時間"
        Else
            If jikan < j1 Then
                Nsw = True
                sabun = DateDiff("s", j1, jikan)
                TimerBar.Maximum = 5400
                infome = "1時限目"
            Else
                If jikan < y2 Then
                    If Nsw And Ntu Then Nsw = False : Call noti()
                    sabun = DateDiff("s", y2, jikan)
                    TimerBar.Maximum = 900
                    infome = "2時限目待ち時間"
                Else
                    If jikan < j2 Then
                        Nsw = True
                        sabun = DateDiff("s", j2, jikan)
                        TimerBar.Maximum = 5400
                        infome = "2時限目"
                    Else
                        If jikan < y3 Then
                            If Nsw And Ntu Then Nsw = False : Call noti()
                            sabun = DateDiff("s", y3, jikan)
                            TimerBar.Maximum = 3600
                            infome = "昼休み"
                        Else
                            If jikan < j3 Then
                                Nsw = True
                                sabun = DateDiff("s", j3, jikan)
                                TimerBar.Maximum = 5400
                                infome = "3時限目"
                            Else

                                If jikan < y4 Then
                                    If Nsw And Ntu Then Nsw = False : Call noti()
                                    sabun = DateDiff("s", y4, jikan)
                                    TimerBar.Maximum = 900
                                    infome = "4時限目待ち時間"
                                Else

                                    If jikan < j4 Then
                                        Nsw = True
                                        sabun = DateDiff("s", j4, jikan)
                                        TimerBar.Maximum = 5400
                                        infome = "4時限目"
                                    Else
                                        If Term5MenuItem.Checked Then
                                            If jikan < y5 Then
                                                If Nsw And Ntu Then Nsw = False : Call noti()
                                                sabun = DateDiff("s", y5, jikan)
                                                TimerBar.Maximum = 900
                                                infome = "5時限目待ち時間"
                                            Else
                                                If jikan < j5 Then
                                                    Nsw = True
                                                    sabun = DateDiff("s", j5, jikan)
                                                    TimerBar.Maximum = 5400
                                                    infome = "5時限目"
                                                End If
                                            End If
                                        End If
                                        If Term6MenuItem.Checked Then
                                            If jikan < j6 Then
                                                Nsw = True
                                                sabun = DateDiff("s", j5, jikan)
                                                TimerBar.Maximum = 5400
                                                infome = "6時限目"
                                            End If
                                        End If
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
            TimerLabel.Text = sabun
            TimerLabel.ForeColor = Color.Red
        Else
            If Secsw = True Then
                If sabun > 9999 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "9999" Else TimerLabel.Text = sabun
            Else
                If sabun > 5999 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "99:59" Else TimerLabel.Text = Format(Int(sabun / 60), "00") & ":" & Format(sabun Mod 60, "00")
            End If
            TimerLabel.ForeColor = Color.Black
        End If
        If TitleShowTimerMenuItem.Checked = True And sabun >= 61 Then Me.Text = Format(Int(sabun / 60), "00") & ":" & Format(sabun Mod 60, "00") Else If Me.Text <> "" Then Me.Text = ""
        If TitleShowTimerMenuItem.Checked = True And sabun < 61 Then Me.Text = sabun
        If (TimerBar.Maximum - sabun) < 0 Then TimerBar.Value = 0 Else TimerBar.Value = TimerBar.Maximum - sabun
        NotifyIcon.Text = infome & " : 残り " & TimerLabel.Text
    End Sub

    Private Sub バー表示ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowBarMenuItem.Click
        If Not ShowBarMenuItem.Checked Then
            TimerBar.Visible = False
            Me.Height = Me.Height - Int(TimerBar.Height * 1.5)

        Else
            TimerBar.Visible = True
            If manual Then Me.Height = Me.Height + Int(TimerBar.Height * 1.5)

        End If
    End Sub

    Private Sub 秒数表示ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowsecMenuItem.Click
        If ShowsecMenuItem.Checked Then
            Secsw = True
        Else
            Secsw = False
        End If
    End Sub

    Private Sub 固定時マウスオーバーで透明化ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TransparentMenuItem.Click
        If Not TransparentMenuItem.Checked Then
            Oti = -1
        Else
            Oti = 120
        End If
    End Sub

    Private Sub 常に最前面ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TopmostMenuItem.Click
        If Not TopmostMenuItem.Checked Then
            Me.TopMost = False
        Else
            Me.TopMost = True
        End If
    End Sub

    Private Sub ヘルプToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HelpMenuItem.Click
        MessageBox.Show("時間表示を左ダブルクリック：秒数表示切り替え" & vbNewLine & "時間表示を右ダブルクリック：タイマーの位置を固定、コンパクト化", Me.Text, MessageBoxButtons.OK)
    End Sub

    Private Sub 休み時間時次の授業情報表示ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowNextTermMenuItem.Click
        'MessageBox.Show("ツールとの連携が必要です。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub 小ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Size4MenuItem.Click
        TimerLabel.Font = New Font("UD デジタル 教科書体 NK-B", 24, FontStyle.Bold)
        Me.Width = 153
        Me.Height = 113
        TimerLabel.Width = 125
        TimerLabel.Height = 105
        TimerLabel.Left = 6
        TimerBar.Top = 59
        TimerBar.Left = 6
        TimerBar.Width = TimerLabel.Width
        TimerBar.Height = 9
        Size4MenuItem.Checked = True
        Size3MenuItem.Checked = False
        Size2MenuItem.Checked = False
        Size1MenuItem.Checked = False

    End Sub

    Private Sub 中ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Size2MenuItem.Click
        TimerLabel.Font = New Font("UD デジタル 教科書体 NK-B", 48, FontStyle.Bold)
        Me.Width = 267
        Me.Height = 159
        TimerLabel.Width = 234
        TimerLabel.Height = 91
        TimerLabel.Left = 12
        TimerBar.Left = 12
        TimerBar.Top = 94
        TimerBar.Width = TimerLabel.Width
        TimerBar.Height = 19
        Size4MenuItem.Checked = False
        Size3MenuItem.Checked = False
        Size2MenuItem.Checked = True
        Size1MenuItem.Checked = False
    End Sub

    Private Sub フォームのサイズ変更を可能にするToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CanSizeChangeMenuItem.Click
        If CanSizeChangeMenuItem.Checked Then
            Me.FormBorderStyle = FormBorderStyle.Sizable
        Else
            Me.FormBorderStyle = FormBorderStyle.FixedSingle
        End If
    End Sub

    Private Sub ビッグToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Size1MenuItem.Click
        TimerLabel.Font = New Font("UD デジタル 教科書体 NK-B", 64, FontStyle.Bold)
        Me.Width = 355
        Me.Height = 191
        TimerLabel.Width = 315
        TimerLabel.Height = 114
        TimerLabel.Left = 15
        TimerBar.Left = TimerLabel.Left
        TimerBar.Top = 118
        TimerBar.Width = TimerLabel.Width
        TimerBar.Height = 24
        Size4MenuItem.Checked = False
        Size3MenuItem.Checked = False
        Size2MenuItem.Checked = False
        Size1MenuItem.Checked = True
    End Sub
    Private Sub Windowsの起動時に自動的に起動するToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutorunMenuItem.Click
        If Not AutorunMenuItem.Checked Then
            Call SetCurrentVersionDelete()
        Else
            Call SetCurrentVersionRun()
        End If
    End Sub
    Public Shared Sub SetCurrentVersionRun()
        'Runキーを開く
        Dim regkey As Microsoft.Win32.RegistryKey =
        Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
        "Software\Microsoft\Windows\CurrentVersion\Run", True)
        '値の名前に製品名、値のデータに実行ファイルのパスを指定し、書き込む
        regkey.SetValue(Application.ProductName, Application.ExecutablePath)
        '閉じる
        regkey.Close()
    End Sub
    Public Shared Sub SetCurrentVersionDelete()
        Dim regkey As Microsoft.Win32.RegistryKey =
            Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        regkey.DeleteValue(Application.ProductName)
        '閉じる
        regkey.Close()

    End Sub

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowAboutMenuItem.Click
        Dim ver As System.Diagnostics.FileVersionInfo
        ver = System.Diagnostics.FileVersionInfo.GetVersionInfo(
        System.Reflection.Assembly.GetExecutingAssembly().Location)
        MessageBox.Show("授業時間タイマー（ECC Comp.)　v" & ver.FileVersion & vbNewLine & "© 2018-2019 Takuya Shintani", "about", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub アプリと連携ToolStripMenuItem_Click(sender As Object, e As EventArgs)
        MessageBox.Show("ツールのインストールが必要です。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub 学籍番号で連携ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IDPassMenuItem.Click
        If IDPassMenuItem.Checked Then
            IDPassMenuItem.Checked = False
            My.Settings.ID = "-1"
            NextTimeMenuItem.Visible = False
            NextTimeMenuItemN.Visible = False
            Nsw = False
        Else
            IDInput.ShowDialog()
            If My.Settings.ID = "-1" Then

            Else
                NextTimeMenuItem.Visible = True
                NextTimeMenuItem.ForeColor = Color.Gray
                NextTimeMenuItemN.Visible = True
                NextTimeMenuItemN.ForeColor = Color.Gray
                ShowNotifyMenuItem.Checked = True
                ShowPopUpMenuItem.Checked = True
                IDPassMenuItem.Checked = True
                WebTimer.Start()
                WebTimer.Interval = 1500
            End If
        End If
    End Sub

    Private Sub 次の授業NToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NextTimeMenuItem.Click
        If NextTimeMenuItem.ForeColor <> Color.Gray Then
            NextTimeForm.Show()
        Else
            Me.TopMost = False
            Dim r = MessageBox.Show("時間割の取得に失敗しています。" & vbNewLine & "インターネットの接続を確認してください。" & vbNewLine & vbNewLine & "前回取得したデータを使用しますか？", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error)
            If r = vbYes Then SOK = True : NextTimeMenuItem.ForeColor = Color.Black : NextTimeMenuItemN.ForeColor = Color.Black : NextTimeForm.Show() : WebTimer.Stop() : Exit Sub
            r = MessageBox.Show("今すぐ再試行しますか？", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error)
            If r = vbYes Then WebTimer.Start() : WebTimer.Interval = 1500
            Call 常に最前面ToolStripMenuItem_Click(Me, e)
        End If
    End Sub

    Private Sub WebTimer_Tick(sender As Object, e As EventArgs) Handles WebTimer.Tick
        WebTimer.Interval = 1800000
        'Pingオブジェクトの作成
        Dim p As New System.Net.NetworkInformation.Ping()
        '"www.yahoo.com"にPingを送信する
        On Error GoTo 1
        Dim reply As System.Net.NetworkInformation.PingReply = p.Send("www.yahoo.com")

        '結果を取得
        If reply.Status = System.Net.NetworkInformation.IPStatus.Success Then
            '           Console.WriteLine("Reply from {0}:bytes={1} time={2}ms TTL={3}",
            '           reply.Address, reply.Buffer.Length,
            '           reply.RoundtripTime, reply.Options.Ttl)
        Else
1:
            Exit Sub
        End If

        p.Dispose()
        If System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() Then
        Else
            Exit Sub
        End If
        Dim username = My.Settings.ID
        Dim password = My.Settings.Password

        Dim TokenUrl As String = "http://comp2.ecc.ac.jp/monster/v1/token/create"

        'UTF-8へのエンコーディング用
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("UTF-8")

        'POST送信する文字列を作成
        Dim postData As String = "username=" & username & "&pass=" & password
        Dim wc As New System.Net.WebClient()

        '文字コードとコンテントタイプの設定
        wc.Encoding = enc
        wc.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

        'Post送信してトークンデータ受信(Json形式)
        Dim TokenJson As String = wc.UploadString(TokenUrl, postData)
        wc.Dispose()

        '受信したデータを表示する(コメントアウト済み)
        'Console.WriteLine(TokenJson)

        '受信したJsonをデシリアライズ
        Dim Token = JsonConvert.DeserializeObject(Of Token)(TokenJson)

        '時間割テーブルを返してくれるURL(code?以下にcode=学籍番号&token=取得したトークン、をGETで)
        Dim TableURL As String = "http://comp2.ecc.ac.jp/monster/v1/timetable/find_by_code?code=" & username & "&token=" & Token.token

        'TableURLをたたいて時間割データを取得(Json形式)
        Dim req As WebRequest = WebRequest.Create(TableURL)
        Dim res As WebResponse = req.GetResponse()

        Dim st As Stream = res.GetResponseStream()
        Dim sr As StreamReader = New StreamReader(st, enc)

        Dim TableJson As String = sr.ReadToEnd()
        My.Settings.TimeTable = TableJson
        sr.Close()
        st.Close()
        SOK = True
        NextTimeMenuItem.Enabled = True
        NextTimeMenuItem.ForeColor = Color.Black
        NextTimeMenuItemN.Enabled = True
        NextTimeMenuItemN.ForeColor = Color.Black
        WebTimer.Stop()
    End Sub

    Private Sub 終了XToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitMenuItem.Click
        Me.Close()
    End Sub

    Private Sub フォントの変更ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeFontMenuItem.Click
        SelectFontDialog.Font = TimerLabel.Font
        SelectFontDialog.Color = TimerLabel.ForeColor
        If SelectFontDialog.ShowDialog <> DialogResult.Cancel Then
            TimerLabel.Font = SelectFontDialog.Font
            TimerLabel.ForeColor = SelectFontDialog.Color
            My.Settings.SaveFont = SelectFontDialog.Font
        End If
    End Sub

    Private Sub ToolStripTextBox1_LostFocus(sender As Object, e As EventArgs) Handles TimerHeightToolStripTextBox.LostFocus
        If IsNumeric(TimerHeightToolStripTextBox.Text) Then
            TimerLabel.Height = Val(TimerHeightToolStripTextBox.Text)
            'ProgressBar1.Top = Label1.Top + Label1.Height + Val(ToolStripTextBox6.Text)
        Else
            TimerHeightToolStripTextBox.Text = TimerLabel.Height
        End If
    End Sub

    Private Sub ToolStripTextBox6_LostFocus(sender As Object, e As EventArgs) Handles SpaceWidthToolStripTextBox.LostFocus
        If IsNumeric(sender.Text) Then
            TimerBar.Top = TimerLabel.Top + TimerLabel.Height + Val(sender.Text)
        Else
            sender.Text = TimerBar.Top - (TimerLabel.Top + TimerLabel.Height)
        End If
    End Sub


    Private Sub ToolStripTextBox7_Leave(sender As Object, e As EventArgs) Handles LeftSpaceToolStripTextBox.LostFocus
        If IsNumeric(sender.Text) Then
            TimerLabel.Left = Val(sender.Text)
            TimerBar.Left = Val(sender.Text)
        Else
            sender.Text = TimerLabel.Left
        End If
    End Sub

    Private Sub ToolStripTextBox8_Leave(sender As Object, e As EventArgs) Handles RightSpaceToolStripTextBox.LostFocus
        If IsNumeric(sender.Text) Then
            TimerLabel.Width = Me.Width - TimerLabel.Left - Val(sender.Text)
            TimerBar.Width = TimerLabel.Width
        Else
            sender.Text = Me.Width - TimerLabel.Left - TimerLabel.Width
        End If
    End Sub

    Private Sub 高度なデザイン設定をONにするToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedOnMenuItem.Click
        AdvancedMenuItem.Visible = True
        CanSizeChangeMenuItem.Checked = True
        Call フォームのサイズ変更を可能にするToolStripMenuItem_Click(sender, e)
        SizeSaveMenuItem.Checked = True
        SettingSeparator1.Visible = False
        My.Settings.SizeSet = 0
        AdvancedOnMenuItem.Visible = False
        SizeMenuItem.Enabled = False
        My.Settings.SaveFont = TimerLabel.Font
        My.Settings.LabelHeight = TimerLabel.Height
        My.Settings.BarHeight = TimerBar.Height
        My.Settings.TimerBarWidth = TimerBar.Top - (TimerLabel.Top + TimerLabel.Height)
        My.Settings.LeftSpace = TimerLabel.Left
        My.Settings.RightSpace = Me.Width - TimerLabel.Left - TimerLabel.Width
        My.Settings.Padding = 0
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles AdvancedOffMenuItem.Click

        SettingSeparator1.Visible = True
        AdvancedMenuItem.Visible = False
        My.Settings.SizeSet = 3
        Call 中ToolStripMenuItem_Click(sender, e)
        AdvancedOnMenuItem.Visible = True
        SizeMenuItem.Enabled = True
    End Sub

    Private Sub 高度ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedMenuItem.Click
        TimerHeightToolStripTextBox.Text = TimerLabel.Height
        BarHeightToolStripTextBox2.Text = TimerBar.Height
        SpaceWidthToolStripTextBox.Text = TimerBar.Top - (TimerLabel.Top + TimerLabel.Height)
        LeftSpaceToolStripTextBox.Text = TimerLabel.Left
        RightSpaceToolStripTextBox.Text = Me.Width - TimerLabel.Left - TimerLabel.Width
        If TimerLabel.Padding.Top = 0 Then MarginToolStripTextBox.Text = (26 - TimerLabel.Top) * -1 Else MarginToolStripTextBox.Text = TimerLabel.Padding.Top
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Me.FormBorderStyle = FormBorderStyle.None Then
            Call フォームのサイズ変更を可能にするToolStripMenuItem_Click(Me, e)
            Me.Height = Me.Height + 26
            TimerMenuStrip.Visible = True
            TimerLabel.Top = TimerLabel.Top + 26
            TimerBar.Top = TimerBar.Top + 26
            sSw = False
        End If
        If My.Settings.SizeSet <> 0 Then
            If Size4MenuItem.Checked Then My.Settings.SizeSet = 1
            If Size3MenuItem.Checked Then My.Settings.SizeSet = 2
            If Size2MenuItem.Checked Then My.Settings.SizeSet = 3
            If Size1MenuItem.Checked Then My.Settings.SizeSet = 4
        End If
        My.Settings.TopMost = TopmostMenuItem.Checked
        My.Settings.ShowBar = ShowBarMenuItem.Checked
        My.Settings.ShowSec = ShowsecMenuItem.Checked
        My.Settings.Transparent = TransparentMenuItem.Checked
        My.Settings.CanSizeChange = CanSizeChangeMenuItem.Checked
        If PositionSaveMenuItem.Checked = True Then
            My.Settings.SavePosX = Me.Left
            My.Settings.SavePosY = Me.Top
        Else
            My.Settings.SavePosX = -1
            My.Settings.SavePosY = -1
        End If
        If SizeSaveMenuItem.Checked = True Then
            My.Settings.SaveSizeX = Me.Width
            My.Settings.SaveSizeY = Me.Height
        Else
            My.Settings.SaveSizeX = -1
            My.Settings.SaveSizeY = -1
        End If
        My.Settings.Lock = LockStartMenuItem.Checked
        My.Settings.StartUp = AutorunMenuItem.Checked
        My.Settings.SaveFont = TimerLabel.Font
        My.Settings.LabelHeight = TimerLabel.Height
        My.Settings.BarHeight = TimerBar.Height
        My.Settings.TimerBarWidth = TimerBar.Top - (TimerLabel.Top + TimerLabel.Height)
        My.Settings.LeftSpace = TimerLabel.Left
        My.Settings.RightSpace = Me.Width - TimerLabel.Left - TimerLabel.Width
        My.Settings.Padding = Val(MarginToolStripTextBox.Text)
        My.Settings.Notify = ShowNotifyMenuItem.Checked
        My.Settings.PopUp = ShowPopUpMenuItem.Checked
        If BatteryMenuItem.Checked Then My.Settings.BatteryMode = True Else My.Settings.BatteryMode = False
        My.Settings.Deviation = DeviationToolStripTextBox.Text
        My.Settings.ChangeBattery = ChangeBatteryMenuItem.Checked
        My.Settings.Term5 = Term5MenuItem.Checked
        My.Settings.Term6 = Term6MenuItem.Checked
        'End
    End Sub
    Private Sub カウントアップタイマーToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CountupMenuItem.Click, CountupMenuItemN.Click
        countti = 0
        KitchenMenuItem.Checked = False
        ClassMenuItem.Checked = False
        CountdownMenuItem.Checked = False
        BatteryMenuItem.Checked = False
        CountupMenuItem.Checked = True
        CountdownTimer.Stop()
        ClassTimer.Stop()
        KitchenTimer.Stop()
        BatteryTimer.Stop()
        CountupTimer.Start()
    End Sub

    Private Sub 授業時間タイマーToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClassMenuItem.Click, ClassMenuItemN.Click
        KitchenMenuItem.Checked = False
        CountupMenuItem.Checked = False
        CountdownMenuItem.Checked = False
        BatteryMenuItem.Checked = False
        TimerBar.Style = ProgressBarStyle.Blocks
        ClassMenuItem.Checked = True
        CountdownTimer.Stop()
        CountupTimer.Stop()
        KitchenTimer.Stop()
        BatteryTimer.Stop()
        ClassTimer.Start()
    End Sub

    Private Sub バッテリー残量ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatteryMenuItem.Click, BatteryMenuItemN.Click
        ClassMenuItem.Checked = False
        KitchenMenuItem.Checked = False
        CountupMenuItem.Checked = False
        CountdownMenuItem.Checked = False
        TimerBar.Style = ProgressBarStyle.Blocks
        BatteryMenuItem.Checked = True
        TimerBar.Value = 0
        TimerBar.Maximum = 100
        CountdownTimer.Stop()
        CountupTimer.Stop()
        KitchenTimer.Stop()
        ClassTimer.Stop()
        BatteryTimer.Start()

    End Sub
    Private Sub キッチンタイマーToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KitchenMenuItem.Click, KitchenMenuItemN.Click
        Me.TopMost = False
        countti = InputBox("測る時間を入力してください（分）")
        Call 常に最前面ToolStripMenuItem_Click(Me, e)
        If IsNumeric(countti) = False Then Exit Sub
        ClassMenuItem.Checked = False
        CountupMenuItem.Checked = False
        CountdownMenuItem.Checked = False
        BatteryMenuItem.Checked = False
        TimerBar.Style = ProgressBarStyle.Blocks
        countti *= 600
        TimerBar.Value = 0
        TimerBar.Maximum = countti
        KitchenMenuItem.Checked = True
        CountdownTimer.Stop()
        CountupTimer.Stop()
        ClassTimer.Stop()
        BatteryTimer.Stop()
        KitchenTimer.Start()
    End Sub

    Private Sub カウントダウンタイマーToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CountdownMenuItem.Click, CountdownMenuItemN.Click
        Me.TopMost = False
        Dim h = InputBox("目標時間を入力してください（時）")
        If IsNumeric(h) = False Or h < 0 Or h > 23 Then MessageBox.Show("0-23の数字である必要があります", "", MessageBoxButtons.OK, MessageBoxIcon.Error) : Exit Sub
        Dim m = InputBox("目標時間を入力してください（分）")
        If IsNumeric(m) = False Or m < 0 Or m > 59 Then MessageBox.Show("0-59の数字である必要があります", "", MessageBoxButtons.OK, MessageBoxIcon.Error) : Exit Sub
        Call 常に最前面ToolStripMenuItem_Click(Me, e)
        counttm = New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, Int(h), Int(m), 0)
        ClassMenuItem.Checked = False
        CountupMenuItem.Checked = False
        KitchenMenuItem.Checked = False
        BatteryMenuItem.Checked = False
        TimerBar.Style = ProgressBarStyle.Blocks
        Dim sabun = DateDiff("s", Now, counttm)
        TimerBar.Value = 0
        TimerBar.Maximum = Math.Max(sabun, 1)
        CountdownMenuItem.Checked = True
        CountupTimer.Stop()
        ClassTimer.Stop()
        BatteryTimer.Stop()
        KitchenTimer.Stop()
        CountdownTimer.Start()
    End Sub
    Private Sub kitchenTimer_Tick(sender As Object, e As EventArgs) Handles KitchenTimer.Tick
        countti -= 1
        Application.DoEvents()
        If OSw = True And Oti < 120 And Oti >= 0 Then
            Oti += 1
            If Oti > 30 And Oti < 80 Then
                Me.Opacity = ((Oti - 30) * 2) / 100
            End If
        Else
            OSw = False
            If Oti >= 0 Then Oti = 0
            Me.Opacity = "1"

        End If
        If countti = 0 Then
            NotifyIcon.BalloonTipText = "タイマー終了"
            NotifyIcon.ShowBalloonTip(1000)
        End If
        If countti < 0 Then TimerBar.Value = 0 Else TimerBar.Value = TimerBar.Maximum - countti
        If countti <= 600 Then
            TimerLabel.Text = Math.Max(Int(countti / 10), 0)
            TimerLabel.ForeColor = Color.Red
        Else
            If Secsw = True Then
                If countti > 99990 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "9999" Else TimerLabel.Text = Int(countti / 10)
            Else
                If countti > 59990 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "99:59" Else TimerLabel.Text = Format(Int((countti) / 600), "00") & ":" & Format(Int((countti Mod 600) / 10), "00")
            End If
            TimerLabel.ForeColor = Color.Black
        End If
        If TitleShowTimerMenuItem.Checked = True Then Me.Text = Format(Int((countti) / 600), "00") & ":" & Format(Int((countti Mod 600) / 10), "00") Else If Me.Text <> "" Then Me.Text = ""
        If Secsw = False Then
            NotifyIcon.Text = "キッチンタイマー : 残り " & TimerLabel.Text & " / " & Format(Int(TimerBar.Maximum / 600), "00") & ":" & Format(TimerBar.Maximum Mod 600 / 10, "00")
        Else
            NotifyIcon.Text = "キッチンタイマー : 残り " & TimerLabel.Text & " / " & TimerBar.Maximum / 10
        End If
    End Sub
    Private Sub CountupTimer_Tick(sender As Object, e As EventArgs) Handles CountupTimer.Tick
        countti += 1
        Application.DoEvents()
        If OSw = True And Oti < 120 And Oti >= 0 Then
            Oti += 1
            If Oti > 30 And Oti < 80 Then
                Me.Opacity = ((Oti - 30) * 2) / 100
            End If
        Else
            OSw = False
            If Oti >= 0 Then Oti = 0
            Me.Opacity = "1"

        End If
        TimerBar.Maximum = 10
        TimerBar.Value = Math.Min(Math.Max(0, (countti Mod 10) * 2.5), 10)
        'ProgressBar1.Style = ProgressBarStyle.Marquee
        If Secsw = True Then
            If countti > 99990 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "9999" Else TimerLabel.Text = Int(countti / 10)
        Else
            If countti > 59990 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "99:59" Else TimerLabel.Text = Format(Int((countti) / 600), "00") & ":" & Format(Int((countti Mod 600) / 10), "00")
        End If
        TimerLabel.ForeColor = Color.Black
        If TitleShowTimerMenuItem.Checked = True Then Me.Text = Format(Int((countti) / 600), "00") & ":" & Format(Int((countti Mod 600) / 10), "00") Else If Me.Text <> "" Then Me.Text = ""
        NotifyIcon.Text = "カウントアップタイマー : " & TimerLabel.Text
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles NotifyRightMenuStrip.Opening
        If Me.WindowState = FormWindowState.Minimized Then
            ShowMenuItem.Visible = True
            MinimizeMenuItem.Visible = False
        Else
            ShowMenuItem.Visible = False
            MinimizeMenuItem.Visible = True
        End If
    End Sub

    Private Sub 最小化するToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MinimizeMenuItem.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub 表示するToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowMenuItem.Click
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub 終了ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitMenuItemN.Click
        If Me.FormBorderStyle <> FormBorderStyle.None Then
        Else
            Call フォームのサイズ変更を可能にするToolStripMenuItem_Click(Me, e)
            Me.Height = Me.Height + 26
            TimerMenuStrip.Visible = True
            TimerLabel.Top = TimerLabel.Top + 26
            TimerBar.Top = TimerBar.Top + 26
            sSw = False
        End If
        Me.Close()
    End Sub

    Private Sub ネットワークでシステム時刻を補正ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NTPTimeMenuItem.Click
        Call ntp()
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ChangeLockMenuItem.Click
        If Me.FormBorderStyle <> FormBorderStyle.None Then
            Me.WindowState = FormWindowState.Normal
            Me.Height = Me.Height - 26
            Me.FormBorderStyle = FormBorderStyle.None
            TimerMenuStrip.Visible = False
            TimerLabel.Top = TimerLabel.Top - 26
            TimerBar.Top = TimerBar.Top - 26
            If Oti >= 0 Then Oti = Math.Max(80, Oti)
            OSw = True
            sSw = True
        Else
            Me.WindowState = FormWindowState.Normal
            Call フォームのサイズ変更を可能にするToolStripMenuItem_Click(Me, e)
            Me.Height = Me.Height + 26
            TimerMenuStrip.Visible = True
            TimerLabel.Top = TimerLabel.Top + 26
            TimerBar.Top = TimerBar.Top + 26
            sSw = False
        End If
    End Sub

    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles ChangeSecMenuItem.Click
        If Secsw = False Then
            Secsw = True
            ShowsecMenuItem.Checked = True
        Else
            Secsw = False
            ShowsecMenuItem.Checked = False
        End If
    End Sub

    Private Sub ToolStripMenuItem7_Click(sender As Object, e As EventArgs) Handles NextTimeMenuItemN.Click
        If NextTimeMenuItem.ForeColor <> Color.Gray Then
            If Me.WindowState = FormWindowState.Minimized Then
                Dim j1 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 10, 45, 0)
                Dim j2 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 12, 30, 0)
                Dim j3 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 15, 0, 0)
                Dim j4 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 16, 45, 0)
                Dim j5 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 18, 30, 0)
                Dim j6 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 20, 0, 0)
                Dim y1 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 9, 15, 0)
                Dim y2 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 11, 0, 0)
                Dim y3 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 13, 30, 0)
                Dim y4 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 15, 15, 0)
                Dim y5 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 17, 0, 0)
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
                                            Else
                                                If jikan < y5 Then
                                                    term = 5
                                                Else
                                                    If jikan < j5 Then
                                                        term = 6
                                                    Else
                                                        If jikan < j6 Then
                                                            term = 7
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                Dim ln = "", rm = ""
                Dim TimeTable = JsonConvert.DeserializeObject(Of RootTimeTable)(My.Settings.TimeTable)
                For i As Integer = 0 To TimeTable.timetable.Count - 1
                    If TimeTable.timetable(i).week = Weekday(Today, FirstDayOfWeek.Monday) And TimeTable.timetable(i).term = term Then
                        ln = TimeTable.timetable(i).lesson_name
                        rm = TimeTable.timetable(i).room
                    End If
                Next
                If ln = "" Then
                    NotifyIcon.BalloonTipTitle = ""
                    NotifyIcon.BalloonTipText = "次の時間は授業がないようです。"
                    If ShowNotifyMenuItem.Checked Then NotifyIcon.ShowBalloonTip(1000)
                    Me.Visible = False
                Else
                    NotifyIcon.BalloonTipTitle = ""
                    NotifyIcon.BalloonTipText = "次の時間は " & rm & " 教室で、 " & ln & " です。"
                    If ShowNotifyMenuItem.Checked Then NotifyIcon.ShowBalloonTip(1000)
                End If
            Else
                NextTimeForm.Show()
            End If
        Else
            Me.TopMost = False
            Dim r = MessageBox.Show("時間割の取得に失敗しています。" & vbNewLine & "インターネットの接続を確認してください。" & vbNewLine & vbNewLine & "前回取得したデータを使用しますか？", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error)
            If r = vbYes Then SOK = True : NextTimeMenuItem.ForeColor = Color.Black : NextTimeMenuItemN.ForeColor = Color.Black : NextTimeForm.Show() : WebTimer.Stop() : Exit Sub
            r = MessageBox.Show("今すぐ再試行しますか？", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error)
            If r = vbYes Then WebTimer.Start() : WebTimer.Interval = 1500
            Call 常に最前面ToolStripMenuItem_Click(Me, e)
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles BatteryTimer.Tick
        Application.DoEvents()
        If OSw = True And Oti < 120 And Oti >= 0 Then
            Oti += 1
            If Oti > 30 And Oti < 80 Then
                Me.Opacity = ((Oti - 30) * 2) / 100
            End If
        Else
            OSw = False
            If Oti >= 0 Then Oti = 0
            Me.Opacity = "1"

        End If
        'AC電源の状態
        Dim pls As PowerLineStatus = SystemInformation.PowerStatus.PowerLineStatus
        Dim blp As Single = SystemInformation.PowerStatus.BatteryLifePercent
        Dim blr As Integer = SystemInformation.PowerStatus.BatteryLifeRemaining
        If pls = PowerLineStatus.Unknown Then
            TimerLabel.Text = "???"
            TimerLabel.ForeColor = Color.Gray
        Else

            If Secsw = True Then
                'バッテリー残量（時間）
                If -1 < blr Then
                    TimerLabel.Text = Int(blr / 60) & "m"
                    TimerBar.Value = blp * 100
                    Select Case pls
                        Case PowerLineStatus.Offline
                            If blp > 0.2 Then TimerLabel.ForeColor = Color.Black Else TimerLabel.ForeColor = Color.Red
                            Exit Select
                        Case PowerLineStatus.Online
                            TimerLabel.ForeColor = Color.DarkGreen
                            Exit Select
                        Case PowerLineStatus.Unknown
                            TimerLabel.Text = "???"
                            TimerLabel.ForeColor = Color.Gray
                            Exit Select
                    End Select
                Else
                    'バッテリー残量（割合）
                    TimerLabel.Text = "--m"
                    TimerBar.Value = blp * 100

                    Select Case pls
                        Case PowerLineStatus.Offline
                            If blp > 0.2 Then TimerLabel.ForeColor = Color.Black Else TimerLabel.ForeColor = Color.Red
                            Exit Select
                        Case PowerLineStatus.Online
                            TimerLabel.ForeColor = Color.DarkGreen
                            Exit Select
                        Case PowerLineStatus.Unknown
                            TimerLabel.Text = "???"
                            TimerLabel.ForeColor = Color.Gray
                            Exit Select
                    End Select
                End If
            Else
                'バッテリー残量（割合）
                TimerLabel.Text = blp * 100 & "%"
                TimerBar.Value = blp * 100

                Select Case pls
                    Case PowerLineStatus.Offline
                        If blp > 0.2 Then TimerLabel.ForeColor = Color.Black Else TimerLabel.ForeColor = Color.Red
                        Exit Select
                    Case PowerLineStatus.Online
                        TimerLabel.ForeColor = Color.DarkGreen
                        Exit Select
                    Case PowerLineStatus.Unknown
                        TimerLabel.Text = "???"
                        TimerLabel.ForeColor = Color.Gray
                        Exit Select
                End Select
            End If

        End If

        Select Case pls
            Case PowerLineStatus.Offline
                If blr > -1 Then
                    NotifyIcon.Text = "バッテリー : " & blp * 100 & "% (残り時間 " & Int(blr / 60) & "分 " & blr Mod 60 & "秒)"
                Else
                    NotifyIcon.Text = "バッテリー : " & blp * 100 & "% (残り時間 計算中または不明)"
                End If
                Exit Select
            Case PowerLineStatus.Online
                NotifyIcon.Text = "バッテリー : " & blp * 100 & "% (充電中)"
                Exit Select
            Case PowerLineStatus.Unknown
                NotifyIcon.Text = "バッテリー : 状態不明"
                Exit Select
        End Select

        'バッテリーがフル充電された時の持ち時間（バッテリー駆動時間）
        'Dim bfl As Integer = SystemInformation.PowerStatus.BatteryFullLifetime
        'If -1 < bfl Then
        'Console.WriteLine("バッテリー駆動時間は、{0}秒です", bfl)
        'Else
        'Console.WriteLine("バッテリー駆動時間は、不明です")
        'End If
    End Sub

    Private Sub NotifyIcon1_Click(sender As Object, e As EventArgs) Handles NotifyIcon.DoubleClick
        NotifyIcon.BalloonTipText = NotifyIcon.Text
        NotifyIcon.ShowBalloonTip(1000)
    End Sub

    Private Sub NotifyIcon1_BalloonTipClicked(sender As Object, e As EventArgs) Handles NotifyIcon.BalloonTipClicked
        If Me.WindowState = FormWindowState.Minimized Then
            Call 表示するToolStripMenuItem_Click(Me, e)
        Else
            Call 最小化するToolStripMenuItem_Click(Me, e)
        End If
    End Sub

    Private Sub ToolStripMenuItem8_DropDownOpened(sender As Object, e As EventArgs) Handles ModeMenuItemN.DropDownOpened
        ClassMenuItemN.Checked = ClassMenuItem.Checked
        CountupMenuItemN.Checked = CountupMenuItem.Checked
        KitchenMenuItemN.Checked = KitchenMenuItem.Checked
        CountdownMenuItemN.Checked = CountdownMenuItem.Checked
        'ToolStripMenuItem13.Checked
        BatteryMenuItemN.Checked = BatteryMenuItem.Checked
    End Sub

    Private Sub 通知ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowNotifyMenuItem.Click

    End Sub

    Private Sub 高度な設定をエクスポートToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportMenuItem.Click
        Dim textFile As System.IO.StreamWriter
        SaveFileDialog.ShowDialog()
        If SaveFileDialog.FileName = "" Then Exit Sub
        textFile = New System.IO.StreamWriter(SaveFileDialog.FileName, False, System.Text.Encoding.Default)
        If PositionSaveMenuItem.Checked = True Then
            Dim r = MessageBox.Show("フォームの位置情報をファイルに加えますか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If r = vbYes Then
                My.Settings.SavePosX = Me.Left
                My.Settings.SavePosY = Me.Top
            Else
                My.Settings.SavePosX = -1
                My.Settings.SavePosY = -1
            End If
        End If
        If SizeSaveMenuItem.Checked = True Then
            Dim r = MessageBox.Show("フォームのサイズ情報をファイルに加えますか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If r = vbYes Then
                My.Settings.SaveSizeX = Me.Width
                My.Settings.SaveSizeY = Me.Height
            Else
                My.Settings.SaveSizeX = -1
                My.Settings.SaveSizeY = -1
            End If
        End If
        My.Settings.SaveFont = TimerLabel.Font
        My.Settings.LabelHeight = TimerLabel.Height
        My.Settings.BarHeight = TimerBar.Height
        My.Settings.TimerBarWidth = TimerBar.Top - (TimerLabel.Top + TimerLabel.Height)
        My.Settings.LeftSpace = TimerLabel.Left
        My.Settings.RightSpace = Me.Width - TimerLabel.Left - TimerLabel.Width
        My.Settings.Padding = Val(MarginToolStripTextBox.Text)
        textFile.WriteLine(My.Settings.SaveFont.Name)
        textFile.WriteLine(My.Settings.SaveFont.Size)
        textFile.WriteLine(My.Settings.SaveFont.Style)
        textFile.WriteLine(My.Settings.SavePosX)
        textFile.WriteLine(My.Settings.SavePosY)
        textFile.WriteLine(My.Settings.SaveSizeX)
        textFile.WriteLine(My.Settings.SaveSizeY)
        textFile.WriteLine(My.Settings.LabelHeight)
        textFile.WriteLine(My.Settings.BarHeight)
        textFile.WriteLine(My.Settings.LeftSpace)
        textFile.WriteLine(My.Settings.RightSpace)
        textFile.WriteLine(My.Settings.Padding)
        textFile.WriteLine(My.Settings.TimerBarWidth)
        textFile.Close()
        MessageBox.Show("エクスポートが完了しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub 高度な設定をインポートToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportMenuItem.Click
        OpenFileDialog.ShowDialog()
        If OpenFileDialog.FileName = "" Then Exit Sub
        Dim sr As New System.IO.StreamReader(OpenFileDialog.FileName,
            System.Text.Encoding.Default)
        '内容を一行ずつ読み込む
        Dim i = 0
        While sr.Peek() > -1
            i += 1
            Select Case i
                Case 1
                    Dim Namev = sr.ReadLine()
                    Dim Sizev = sr.ReadLine()
                    Dim Sizevs = CSng(Sizev)
                    Dim Stv As FontStyle = sr.ReadLine()
                    'If sr.ReadLine() Then
                    '    If sr.ReadLine() Then
                    '        Stv = System.Drawing.FontStyle.Bold And System.Drawing.FontStyle.Italic
                    '    Else
                    '        Stv = System.Drawing.FontStyle.Bold
                    '    End If
                    'Else
                    '    If sr.ReadLine() Then
                    '        Stv = System.Drawing.FontStyle.Italic
                    '    Else
                    '        Stv = ""
                    '    End If
                    'End If
                    TimerLabel.Font = New System.Drawing.Font(Namev, Sizevs, Stv)
                Case 2
                    Dim n = sr.ReadLine()
                    If n <> -1 Then
                        Dim r = MessageBox.Show("データに含まれている位置情報を読み込みますか？" & vbNewLine & "※エキスポート元と解像度が違う場合操作不能になる場合があります。", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        If r = vbYes Then
                            Me.Left = n
                        Else
                            sr.ReadLine()
                            i += 1
                        End If
                    End If
                Case 3
                    Dim n = sr.ReadLine()
                    If n <> -1 Then
                        Me.Top = n
                    End If
                Case 4
                    Dim n = sr.ReadLine()
                    If n <> -1 Then
                        Dim r = MessageBox.Show("データに含まれているサイズ情報を読み込みますか？" & vbNewLine & "※エキスポート元と解像度が違う場合操作不能になる場合があります。", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        If r = vbYes Then
                            Me.Width = n
                        Else
                            sr.ReadLine()
                            i += 1
                        End If
                    End If
                Case 5
                    Dim n = sr.ReadLine()
                    If n <> -1 Then
                        Me.Height = n
                    End If
                Case 6
                    TimerHeightToolStripTextBox.Text = sr.ReadLine()
                    Call ToolStripTextBox1_LostFocus(TimerHeightToolStripTextBox, e)
                Case 7
                    BarHeightToolStripTextBox2.Text = sr.ReadLine()
                    Call ToolStripTextBox2_LostFocus(BarHeightToolStripTextBox2, e)
                Case 8
                    LeftSpaceToolStripTextBox.Text = sr.ReadLine()
                    Call ToolStripTextBox7_Leave(LeftSpaceToolStripTextBox, e)
                Case 9
                    RightSpaceToolStripTextBox.Text = sr.ReadLine()
                    Call ToolStripTextBox8_Leave(RightSpaceToolStripTextBox, e)
                Case 10
                    MarginToolStripTextBox.Text = sr.ReadLine()
                    Call ToolStripTextBox10_LostFocus(MarginToolStripTextBox, e)
                Case 11
                    SpaceWidthToolStripTextBox.Text = sr.ReadLine()
                    Call ToolStripTextBox6_LostFocus(SpaceWidthToolStripTextBox, e)
            End Select
        End While
        '閉じる
        sr.Close()
        MessageBox.Show("インポートが完了しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub MenuStrip1_MouseHover(sender As Object, e As EventArgs) Handles TimerMenuStrip.MouseHover
        TimerMenuStrip.LayoutStyle = ToolStripLayoutStyle.Flow
    End Sub

    Private Sub MenuStrip1_MouseLeave(sender As Object, e As EventArgs) Handles TimerMenuStrip.MouseLeave
        TimerMenuStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow
    End Sub

    Private Sub ToolStripMenuItem5o_Click(sender As Object, e As EventArgs) Handles Term5MenuItem.CheckedChanged
        If Term5MenuItem.Checked Then
            Term6MenuItem.Enabled = True
        Else

            Term6MenuItem.Checked = False
            Term6MenuItem.Enabled = False

        End If
    End Sub

    Private Sub ToolStripMenuItem6o_Click(sender As Object, e As EventArgs) Handles Term6MenuItem.CheckedChanged

    End Sub

    Private Sub バッテリー充電停止時残りバッテリー表示に切り替えるToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeBatteryMenuItem.Click
        If ChangeBatteryMenuItem.Checked = True Then
            BatteryMonitorTimer.Start()
        Else
            BatteryMonitorTimer.Stop()
        End If
    End Sub

    Private Sub KanshiTimer_Tick(sender As Object, e As EventArgs) Handles BatteryMonitorTimer.Tick
        Dim pls As PowerLineStatus = SystemInformation.PowerStatus.PowerLineStatus
        Static plsm As PowerLineStatus
        If plsm <> pls Then
            Select Case pls
                Case PowerLineStatus.Offline
                    Call バッテリー残量ToolStripMenuItem_Click(sender, e)
                Case PowerLineStatus.Online
                    Call 授業時間タイマーToolStripMenuItem_Click(sender, e)
                Case PowerLineStatus.Unknown
            End Select
        End If
        plsm = pls
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles TimerLabel.Click

    End Sub

    Private Sub 設定ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingMenuItem.Click
        If Term5MenuItem.Checked = True And Term6MenuItem.Checked = True Then
            SpecialTimeMenuItem.Checked = True
        Else
            SpecialTimeMenuItem.Checked = False
        End If
        ShowNextTermMenuItem.Checked = IDPassMenuItem.Checked
        If DeviationToolStripTextBox.Text <> 0 Then DeviationMenuItem.Checked = True Else DeviationMenuItem.Checked = False
    End Sub

    Private Sub CountdownTimer_Tick(sender As Object, e As EventArgs) Handles CountdownTimer.Tick
        Dim j = New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, Hour(counttm), Minute(counttm), 0)
        Dim tss As New TimeSpan(0, 0, 0, DeviationToolStripTextBox.Text)
        Dim sabun = DateDiff("s", Now + tss, j)
        Application.DoEvents()
        If OSw = True And Oti < 120 And Oti >= 0 Then
            Oti += 1
            If Oti > 30 And Oti < 80 Then
                Me.Opacity = ((Oti - 30) * 2) / 100
            End If
        Else
            OSw = False
            If Oti >= 0 Then Oti = 0
            Me.Opacity = "1"

        End If
        If sabun < 0 Then TimerBar.Value = 0 Else TimerBar.Value = Math.Max(0, TimerBar.Maximum - sabun)
        If sabun = 0 Then
            NotifyIcon.BalloonTipTitle = ""
            NotifyIcon.BalloonTipText = "時間になりました"
            NotifyIcon.ShowBalloonTip(1000)
        End If
        If sabun < 60 Then
            TimerLabel.Text = Math.Max(Int(sabun), 0)
            TimerLabel.ForeColor = Color.Red
        Else
            If Secsw = True Then
                If sabun > 9999 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "9999" Else TimerLabel.Text = Int(sabun)
            Else
                If sabun > 5999 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "99:59" Else TimerLabel.Text = Format(Int(sabun / 60), "00") & ":" & Format(Int((sabun Mod 60)), "00")
            End If
            TimerLabel.ForeColor = Color.Black
        End If
        If TitleShowTimerMenuItem.Checked = True Then Me.Text = Format(Int(sabun / 60), "00") & ":" & Format(Int((sabun Mod 60)), "00") Else If Me.Text <> "" Then Me.Text = ""
        NotifyIcon.Text = "カウントダウンタイマー : 残り " & TimerLabel.Text & " (" & Format(Hour(counttm), "00") & ":" & Format(Minute(counttm), "00") & "まで)"
    End Sub
    Private Sub コンパクトToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Size3MenuItem.Click
        TimerLabel.Font = New Font("UD デジタル 教科書体 NK-B", 36, FontStyle.Bold)
        Me.Width = 210
        Me.Height = 133
        TimerLabel.Width = 178
        TimerLabel.Height = 98
        TimerLabel.Left = 9
        TimerBar.Left = 9
        TimerBar.Top = 73
        TimerBar.Width = TimerLabel.Width
        TimerBar.Height = 14
        Size4MenuItem.Checked = False
        Size3MenuItem.Checked = True
        Size2MenuItem.Checked = False
        Size1MenuItem.Checked = False
    End Sub
    Private Sub ProgressBar1_DoubleClick(sender As Object, e As EventArgs) Handles TimerBar.DoubleClick
        Me.Close()
    End Sub

    Private Sub Form1_MouseEnter(sender As Object, e As EventArgs) Handles TimerBar.MouseEnter, MyBase.MouseEnter, TimerLabel.MouseEnter
        If Oti < 40 And Me.FormBorderStyle = FormBorderStyle.None And Oti >= 0 Then
            OSw = True
            Oti = 0
            Me.Opacity = "0"
        End If
    End Sub

    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles MyBase.Closed
    End Sub

    Private Sub ToolStripTextBox2_LostFocus(sender As Object, e As EventArgs) Handles BarHeightToolStripTextBox2.LostFocus
        If IsNumeric(sender.Text) Then
            TimerBar.Height = Val(sender.Text)
        Else
            sender.Text = TimerBar.Height
        End If
    End Sub

    Private Sub ToolStripTextBox10_LostFocus(sender As Object, e As EventArgs) Handles MarginToolStripTextBox.LostFocus
        If IsNumeric(sender.Text) Then
            If Val(sender.Text) < 0 Then
                TimerLabel.Padding = New Padding(0, 0, 0, 0)
                TimerLabel.Top = 26 - (Val(sender.Text) * -1)
            Else
                TimerLabel.Top = 26
                TimerLabel.Padding = New Padding(0, Val(sender.Text), 0, 0)
            End If
        Else
            If TimerLabel.Padding.Top = 0 Then sender.Text = (26 - TimerLabel.Top) * -1 Else sender.Text = TimerLabel.Padding.Top
        End If
    End Sub

    Private Sub ToolStripTextBox3_TextChanged(sender As Object, e As EventArgs) Handles DeviationToolStripTextBox.TextChanged
        If Len(DeviationToolStripTextBox.Text) = 0 Or Not IsNumeric(DeviationToolStripTextBox.Text) Then DeviationToolStripTextBox.Text = "0"
    End Sub
End Class
'tokenのJsonファイルのデシリアライズ用クラス
Public Class Token
    'code, expireはいまのところ使い道なし(なんとなく残してる)
    Public Property code As String          'CD00001なら受信成功
    Public Property token As String
    Public Property expire As DateTimeOffset       'トークンの有効期限
End Class