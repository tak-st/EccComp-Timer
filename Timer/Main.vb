Imports System.Net
Imports System.IO
Imports Newtonsoft.Json
Public Class Main
    Public ShowSecSwitch, OpacitySwitch, OpacityTimer, LockSwitch, manual, NotifySwitch, TimeTableGetOK, countti
    Public counttm As DateTime
    Private Sub ShowNotify()
        '通知表示用の関数です。
        Dim term = CheckTerm()
        Dim lessonname = "", Room = ""
        '取得しているjsonから検索
        Dim TimeTable = JsonConvert.DeserializeObject(Of RootTimeTable)(My.Settings.TimeTable)
        For i As Integer = 0 To TimeTable.timetable.Count - 1
            If TimeTable.timetable(i).week = Weekday(Today, FirstDayOfWeek.Monday) And TimeTable.timetable(i).term = term Then
                lessonname = TimeTable.timetable(i).lesson_name
                Room = TimeTable.timetable(i).room
            End If
        Next

        '授業があるか?
        If lessonname = "" Then
            NotifyIcon.BalloonTipTitle = "お疲れ様です。"
            NotifyIcon.BalloonTipText = "次の時間は授業がないようです。"
            If ShowNotifyMenuItem.Checked Then NotifyIcon.ShowBalloonTip(1000)
            Me.Visible = False
        Else
            NotifyIcon.BalloonTipTitle = "お疲れ様です。"
            NotifyIcon.BalloonTipText = "次の時間は " & Room & " 教室で、 " & lessonname & " です。"
            If ShowNotifyMenuItem.Checked Then NotifyIcon.ShowBalloonTip(1000)
            If ShowPopUpMenuItem.Checked = False Then NextTimeForm.Show()
        End If
    End Sub
    Private Sub NtpTimeGet()

        'Pingオブジェクトの作成
        Dim p As New System.Net.NetworkInformation.Ping()

        '"www.yahoo.com"にPingを送信する
        Try
            Dim reply As System.Net.NetworkInformation.PingReply = p.Send("www.yahoo.com")
            '結果を取得
            If reply.Status = System.Net.NetworkInformation.IPStatus.Success Then
                '成功したら先に進む
            Else
                'Yahooにpingできなかった
                MessageBox.Show("インターネットに接続する必要があります。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        Catch ex As System.Exception
            'Yahooにpingできなかった
            MessageBox.Show("インターネットに接続する必要があります。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try

        p.Dispose()

        If System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() Then
            'つながっているみたいなら先に進む
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
        Dim DeviationTime = (Now - dtTime).ToString("hh\:mm\:ss")
        System.Diagnostics.Trace.WriteLine(dtTime)
        Try
            TimeOfDay = dtTime
            MessageBox.Show(DeviationTime & " 補正しました", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As System.Security.SecurityException
            MessageBox.Show("時間補正機能を使用するには、" & vbNewLine & "このアプリケーションを管理者権限で起動する必要があります。" & vbNewLine _
                            & " (検出された時間のずれ : " & DeviationTime & ")", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Dim Msgboxreturn = MessageBox.Show("このソフト内でのみ修正することができます。" & vbNewLine & "修正を行いますか?", "",
                                            MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If Msgboxreturn = vbYes Then
                'はい
                DeviationToolStripTextBox.Text = Int(ts.TotalSeconds)
            End If
        End Try
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '起動時の処理です
        '設定ファイルから各種データを読み込みます
        PerfectTransparentMenuItem.Checked = My.Settings.PerfectTrans
        Term5MenuItem.Checked = My.Settings.Term5
        Term6MenuItem.Checked = My.Settings.Term6
        DeviationToolStripTextBox.Text = My.Settings.Deviation
        Me.TopMost = True
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

        Call TopmostMenuItemMenuItem_Click(Me, e)
        Call ShowsecMenuItemMenuItem_Click(Me, e)
        Call TransparentMenuItemMenuItem_Click(Me, e)
        Call CanSizeChangeMenuItem_Click(Me, e)

        'サイズセット 0 = 高度な設定がON
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
                Call Size4MenuItem_Click(Me, e)
            Case 2
                Call Size3MenuItem_Click(Me, e)
            Case 3
                Call Size2MenuItem_Click(Me, e)
            Case 4
                Call Size1MenuItem_Click(Me, e)
        End Select

        Call ShowBarMenuItem_Click(Me, e)

        manual = True

        'AutoLoad.ast検索
        Dim fileName As String = "AutoLoad.ast"

        If System.IO.File.Exists(fileName) Then
            Debug.Print("AutoLoad読み込み")
            Call AutoLoad(fileName, e)
        End If

        If My.Settings.Lock Then
            Me.Height = Me.Height - 26
            Me.FormBorderStyle = FormBorderStyle.None
            TimerMenuStrip.Visible = False
            TimerLabel.Top = TimerLabel.Top - 26
            If Me.Width < 40 Then Me.Width = 40
            If Me.Height < 40 Then Me.Height = 40
            TimerBar.Top = TimerBar.Top - 26
            If OpacityTimer >= 0 Then OpacityTimer = Math.Max(80, OpacityTimer)
            OpacitySwitch = True
            LockSwitch = True
            LockStartMenuItem.Checked = True
        End If

        If My.Settings.BatteryMode Then
            Call BatteryMenuItem_Click(sender, e)
        End If

        AutorunMenuItem.Checked = My.Settings.StartUp
        ChangeBatteryMenuItem.Checked = My.Settings.ChangeBattery

        Call ChangeBatteryMenuItem_Click(sender, e)

    End Sub

    Private Sub AutoLoad(filename As String, e As EventArgs)
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
                    Call TimerHeightToolStripTextBox_LostFocus(TimerHeightToolStripTextBox, e)
                Case 7
                    BarHeightToolStripTextBox.Text = sr.ReadLine()
                    Call BarHeightToolStripTextBox_LostFocus(BarHeightToolStripTextBox, e)
                Case 8
                    LeftSpaceToolStripTextBox.Text = sr.ReadLine()
                    Call LeftSpaceToolStripTextBox_Leave(LeftSpaceToolStripTextBox, e)
                Case 9
                    RightSpaceToolStripTextBox.Text = sr.ReadLine()
                    Call RightSpaceToolStripTextBox_Leave(RightSpaceToolStripTextBox, e)
                Case 10
                    MarginToolStripTextBox.Text = sr.ReadLine()
                    Call MarginToolStripTextBox_LostFocus(MarginToolStripTextBox, e)
                Case 11
                    SpaceWidthToolStripTextBox.Text = sr.ReadLine()
                    Call SpaceWidthToolStripTextBox_LostFocus(SpaceWidthToolStripTextBox, e)
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
                If OpacityTimer >= 0 Then OpacityTimer = Math.Max(80, OpacityTimer)
                OpacitySwitch = True
                LockSwitch = True
            Else
                Call CanSizeChangeMenuItem_Click(Me, e)
                Me.Height = Me.Height + 26
                TimerMenuStrip.Visible = True
                TimerLabel.Top = TimerLabel.Top + 26
                TimerBar.Top = TimerBar.Top + 26
                LockSwitch = False
            End If
        End If
        If e.Button = MouseButtons.Left Then
            If ShowSecSwitch = False Then
                ShowSecSwitch = True
                ShowsecMenuItem.Checked = True
            Else
                ShowSecSwitch = False
                ShowsecMenuItem.Checked = False
            End If
        End If
    End Sub

    Private Sub ClassTimer_Tick(sender As Object, e As EventArgs) Handles ClassTimer.Tick
        Dim DiffTime = 0
        Dim TimeName = ""
        Dim term = 0
        Dim NotifyShowFlag = False
        If IDPassMenuItem.Checked = True And TimeTableGetOK Then NotifyShowFlag = True
        Dim ClassTime1 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 10, 45, 0)
        Dim ClassTime2 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 12, 30, 0)
        Dim ClassTime3 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 15, 0, 0)
        Dim ClassTime4 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 16, 45, 0)
        Dim ClassTime5 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 18, 30, 0)
        Dim ClassTime6 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 20, 0, 0)
        Dim BreakTime1 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 9, 15, 0)
        Dim BreakTime2 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 11, 0, 0)
        Dim BreakTime3 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 13, 30, 0)
        Dim BreakTime4 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 15, 15, 0)
        Dim BreakTime5 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 17, 0, 0)
        Dim Deviation As New TimeSpan(0, 0, 0, DeviationToolStripTextBox.Text)
        Dim NowTime As DateTime = DateTime.Now() + Deviation
        Application.DoEvents()
        If OpacitySwitch = True And OpacityTimer < 120 And OpacityTimer >= 0 Then
            OpacityTimer += 1
            If OpacityTimer > 30 And OpacityTimer < 80 Then
                Me.Opacity = ((OpacityTimer - 30) * 2) / 100
            End If
        Else
            OpacitySwitch = False
            If OpacityTimer >= 0 Then OpacityTimer = 0
            Me.Opacity = "1"

        End If

        Select Case NowTime
            Case Is < BreakTime1
                If NotifySwitch And NotifyShowFlag Then NotifySwitch = False : Call ShowNotify()
                DiffTime = DateDiff("s", BreakTime1, NowTime)
                TimerBar.Maximum = 900
                TimeName = "1時限目待ち時間"
            Case Is < ClassTime1
                NotifySwitch = True
                DiffTime = DateDiff("s", ClassTime1, NowTime)
                TimerBar.Maximum = 5400
                TimeName = "1時限目"
            Case Is < BreakTime2
                If NotifySwitch And NotifyShowFlag Then NotifySwitch = False : Call ShowNotify()
                DiffTime = DateDiff("s", BreakTime2, NowTime)
                TimerBar.Maximum = 900
                TimeName = "2時限目待ち時間"
            Case Is < ClassTime2
                NotifySwitch = True
                DiffTime = DateDiff("s", ClassTime2, NowTime)
                TimerBar.Maximum = 5400
                TimeName = "2時限目"
            Case Is < BreakTime3
                If NotifySwitch And NotifyShowFlag Then NotifySwitch = False : Call ShowNotify()
                DiffTime = DateDiff("s", BreakTime3, NowTime)
                TimerBar.Maximum = 3600
                TimeName = "昼休み"
            Case Is < ClassTime3
                NotifySwitch = True
                DiffTime = DateDiff("s", ClassTime3, NowTime)
                TimerBar.Maximum = 5400
                TimeName = "3時限目"

            Case Is < BreakTime4
                If NotifySwitch And NotifyShowFlag Then NotifySwitch = False : Call ShowNotify()
                DiffTime = DateDiff("s", BreakTime4, NowTime)
                TimerBar.Maximum = 900
                TimeName = "4時限目待ち時間"
            Case Is < ClassTime4
                NotifySwitch = True
                DiffTime = DateDiff("s", ClassTime4, NowTime)
                TimerBar.Maximum = 5400
                TimeName = "4時限目"
            Case Else
                If Term5MenuItem.Checked Then
                    Select Case NowTime
                        Case Is < BreakTime5
                            If NotifySwitch And NotifyShowFlag Then NotifySwitch = False : Call ShowNotify()
                            DiffTime = DateDiff("s", BreakTime5, NowTime)
                            TimerBar.Maximum = 900
                            TimeName = "5時限目待ち時間"
                        Case Is < ClassTime5
                            NotifySwitch = True
                            DiffTime = DateDiff("s", ClassTime5, NowTime)
                            TimerBar.Maximum = 5400
                            TimeName = "5時限目"
                        Case Else
                            If Term6MenuItem.Checked Then
                                If NowTime < ClassTime6 Then
                                    NotifySwitch = True
                                    DiffTime = DateDiff("s", ClassTime5, NowTime)
                                    TimerBar.Maximum = 5400
                                    TimeName = "6時限目"
                                End If
                            End If
                    End Select
                End If
        End Select

        DiffTime = DiffTime * -1

        If DiffTime <= 60 Then
            TimerLabel.Text = DiffTime
            TimerLabel.ForeColor = Color.Red
        Else
            If ShowSecSwitch = True Then
                If DiffTime > 9999 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "9999" Else TimerLabel.Text = DiffTime
            Else
                If DiffTime > 5999 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "99:59" Else TimerLabel.Text = Format(Int(DiffTime / 60), "00") & ":" & Format(DiffTime Mod 60, "00")
            End If
            TimerLabel.ForeColor = Color.Black
        End If

        If TitleShowTimerMenuItem.Checked = True And DiffTime >= 61 Then _
            Me.Text = Format(Int(DiffTime / 60), "00") & ":" & Format(DiffTime Mod 60, "00") Else If Me.Text <> "" Then Me.Text = ""

        If TitleShowTimerMenuItem.Checked = True And DiffTime < 61 Then Me.Text = DiffTime

        If (TimerBar.Maximum - DiffTime) < 0 Then TimerBar.Value = 0 Else _
            TimerBar.Value = TimerBar.Maximum - DiffTime

        NotifyIcon.Text = TimeName & " : 残り " & TimerLabel.Text
    End Sub

    Private Sub ShowBarMenuItem_Click(sender As Object, e As EventArgs) Handles ShowBarMenuItem.Click

        If Not ShowBarMenuItem.Checked Then
            TimerBar.Visible = False
            Me.Height = Me.Height - Int(TimerBar.Height * 1.5)

        Else
            TimerBar.Visible = True
            If manual Then Me.Height = Me.Height + Int(TimerBar.Height * 1.5)
        End If

    End Sub

    Private Sub ShowsecMenuItemMenuItem_Click(sender As Object, e As EventArgs) Handles ShowsecMenuItem.Click

        If ShowsecMenuItem.Checked Then
            ShowSecSwitch = True
        Else
            ShowSecSwitch = False
        End If

    End Sub

    Private Sub TransparentMenuItemMenuItem_Click(sender As Object, e As EventArgs) Handles TransparentMenuItem.Click

        If Not TransparentMenuItem.Checked Then
            OpacityTimer = -1
        Else
            OpacityTimer = 120
        End If

    End Sub

    Private Sub TopmostMenuItemMenuItem_Click(sender As Object, e As EventArgs) Handles TopmostMenuItem.Click

        If Not TopmostMenuItem.Checked Then
            Me.TopMost = False
        Else
            Me.TopMost = True
        End If

    End Sub

    Private Sub HelpMenuItemMenuItem_Click(sender As Object, e As EventArgs) Handles HelpMenuItem.Click
        MessageBox.Show("時間表示を左ダブルクリック：秒数表示切り替え" & vbNewLine & "時間表示を右ダブルクリック：タイマーの位置を固定、コンパクト化", Me.Text, MessageBoxButtons.OK)
    End Sub
    Private Sub Size4MenuItem_Click(sender As Object, e As EventArgs) Handles Size4MenuItem.Click

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

    Private Sub Size2MenuItem_Click(sender As Object, e As EventArgs) Handles Size2MenuItem.Click
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

    Private Sub CanSizeChangeMenuItem_Click(sender As Object, e As EventArgs) Handles CanSizeChangeMenuItem.Click

        If CanSizeChangeMenuItem.Checked Then
            Me.FormBorderStyle = FormBorderStyle.Sizable
        Else
            Me.FormBorderStyle = FormBorderStyle.FixedSingle
        End If

    End Sub

    Private Sub Size1MenuItem_Click(sender As Object, e As EventArgs) Handles Size1MenuItem.Click

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
    Private Sub AutorunMenuItemMenuItem_Click(sender As Object, e As EventArgs) Handles AutorunMenuItem.Click

        If Not AutorunMenuItem.Checked Then
            Call AutoRunDelete()
        Else
            Call AutoRunSet()
        End If

    End Sub
    Public Shared Sub AutoRunSet()
        'Runキーを開く
        Dim regkey As Microsoft.Win32.RegistryKey =
        Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
        "Software\Microsoft\Windows\CurrentVersion\Run", True)
        '値の名前に製品名、値のデータに実行ファイルのパスを指定し、書き込む
        regkey.SetValue(Application.ProductName, Application.ExecutablePath)
        '閉じる
        regkey.Close()
    End Sub
    Public Shared Sub AutoRunDelete()
        Dim regkey As Microsoft.Win32.RegistryKey =
            Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        regkey.DeleteValue(Application.ProductName)
        '閉じる
        regkey.Close()

    End Sub

    Private Sub ShowAboutMenuItem_Click(sender As Object, e As EventArgs) Handles ShowAboutMenuItem.Click
        'バージョンを取得して表示
        Dim ver As System.Diagnostics.FileVersionInfo
        ver = System.Diagnostics.FileVersionInfo.GetVersionInfo(
        System.Reflection.Assembly.GetExecutingAssembly().Location)
        MessageBox.Show("授業時間タイマー（ECC Comp.)　v" & ver.FileVersion & vbNewLine & "© 2018-2019 Takuya Shintani", "about", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    Private Sub IDPassMenuItemMenuItem_Click(sender As Object, e As EventArgs) Handles IDPassMenuItem.Click

        'My.Settings.ID = -1 は未設定

        If IDPassMenuItem.Checked Then
            IDPassMenuItem.Checked = False
            My.Settings.ID = "-1"
            NextTimeMenuItem.Visible = False
            NextTimeMenuItemN.Visible = False
            NotifySwitch = False
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

    Private Sub NextTimeMenuItem_Click(sender As Object, e As EventArgs) Handles NextTimeMenuItem.Click
        If NextTimeMenuItem.ForeColor <> Color.Gray Then
            NextTimeForm.Show()
        Else
            Me.TopMost = False
            Dim r = MessageBox.Show("時間割の取得に失敗しています。" & vbNewLine & "インターネットの接続を確認してください。" &
                                    vbNewLine & vbNewLine & "前回取得したデータを使用しますか？", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error)
            If r = vbYes Then TimeTableGetOK = True : NextTimeMenuItem.ForeColor = Color.Black : NextTimeMenuItemN.ForeColor = Color.Black : NextTimeForm.Show() : WebTimer.Stop() : Exit Sub
            r = MessageBox.Show("今すぐ再試行しますか？", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error)
            If r = vbYes Then WebTimer.Start() : WebTimer.Interval = 1500
            Call TopmostMenuItemMenuItem_Click(Me, e)
        End If
    End Sub

    Private Sub WebTimer_Tick(sender As Object, e As EventArgs) Handles WebTimer.Tick

        'ネットワークから時間割を取得しようとする

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
        TimeTableGetOK = True
        NextTimeMenuItem.Enabled = True
        NextTimeMenuItem.ForeColor = Color.Black
        NextTimeMenuItemN.Enabled = True
        NextTimeMenuItemN.ForeColor = Color.Black
        WebTimer.Stop()
    End Sub

    Private Sub ExitMenuItem_Click(sender As Object, e As EventArgs) Handles ExitMenuItem.Click
        Me.Close()
    End Sub

    Private Sub ChangeFontMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeFontMenuItem.Click

        SelectFontDialog.Font = TimerLabel.Font
        SelectFontDialog.Color = TimerLabel.ForeColor
        If SelectFontDialog.ShowDialog <> DialogResult.Cancel Then
            TimerLabel.Font = SelectFontDialog.Font
            TimerLabel.ForeColor = SelectFontDialog.Color
            My.Settings.SaveFont = SelectFontDialog.Font
        End If

    End Sub

    Private Sub TimerHeightToolStripTextBox_LostFocus(sender As Object, e As EventArgs) Handles TimerHeightToolStripTextBox.LostFocus

        If IsNumeric(TimerHeightToolStripTextBox.Text) Then
            TimerLabel.Height = Val(TimerHeightToolStripTextBox.Text)
        Else
            TimerHeightToolStripTextBox.Text = TimerLabel.Height
        End If

    End Sub

    Private Sub SpaceWidthToolStripTextBox_LostFocus(sender As Object, e As EventArgs) Handles SpaceWidthToolStripTextBox.LostFocus

        If IsNumeric(sender.Text) Then
            TimerBar.Top = TimerLabel.Top + TimerLabel.Height + Val(sender.Text)
        Else
            sender.Text = TimerBar.Top - (TimerLabel.Top + TimerLabel.Height)
        End If

    End Sub


    Private Sub LeftSpaceToolStripTextBox_Leave(sender As Object, e As EventArgs) Handles LeftSpaceToolStripTextBox.LostFocus

        If IsNumeric(sender.Text) Then
            TimerLabel.Left = Val(sender.Text)
            TimerBar.Left = Val(sender.Text)
        Else
            sender.Text = TimerLabel.Left
        End If

    End Sub

    Private Sub RightSpaceToolStripTextBox_Leave(sender As Object, e As EventArgs) Handles RightSpaceToolStripTextBox.LostFocus

        If IsNumeric(sender.Text) Then
            TimerLabel.Width = Me.Width - TimerLabel.Left - Val(sender.Text)
            TimerBar.Width = TimerLabel.Width
        Else
            sender.Text = Me.Width - TimerLabel.Left - TimerLabel.Width
        End If

    End Sub

    Private Sub AdvancedOnMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedOnMenuItem.Click

        '高度な設定をONにしたとき

        AdvancedMenuItem.Visible = True
        CanSizeChangeMenuItem.Checked = True
        Call CanSizeChangeMenuItem_Click(sender, e)
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

    Private Sub AdvancedOffMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedOffMenuItem.Click

        '高度な設定をOFFにしたとき

        SettingSeparator1.Visible = True
        AdvancedMenuItem.Visible = False
        My.Settings.SizeSet = 3
        Call Size2MenuItem_Click(sender, e)
        AdvancedOnMenuItem.Visible = True
        SizeMenuItem.Enabled = True

    End Sub

    Private Sub AdvancedMenuItem_Click(sender As Object, e As EventArgs) Handles AdvancedMenuItem.Click

        TimerHeightToolStripTextBox.Text = TimerLabel.Height
        BarHeightToolStripTextBox.Text = TimerBar.Height
        SpaceWidthToolStripTextBox.Text = TimerBar.Top - (TimerLabel.Top + TimerLabel.Height)
        LeftSpaceToolStripTextBox.Text = TimerLabel.Left
        RightSpaceToolStripTextBox.Text = Me.Width - TimerLabel.Left - TimerLabel.Width
        If TimerLabel.Padding.Top = 0 Then MarginToolStripTextBox.Text = (26 - TimerLabel.Top) * -1 Else MarginToolStripTextBox.Text = TimerLabel.Padding.Top

    End Sub

    Private Sub Main_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        '終了するとき、固定状態を解除し、設定を保存する。

        If Me.FormBorderStyle = FormBorderStyle.None Then
            Call CanSizeChangeMenuItem_Click(Me, e)
            Me.Height = Me.Height + 26
            TimerMenuStrip.Visible = True
            TimerLabel.Top = TimerLabel.Top + 26
            TimerBar.Top = TimerBar.Top + 26
            LockSwitch = False
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
        My.Settings.PerfectTrans = PerfectTransparentMenuItem.Checked
        'End
    End Sub
    Private Sub CountupMenuItem_Click(sender As Object, e As EventArgs) Handles CountupMenuItem.Click, CountupMenuItemN.Click

        'カウントアップ開始時の処理

        countti = 0
        KitchenMenuItem.Checked = False
        ClassMenuItem.Checked = False
        CountdownMenuItem.Checked = False
        BatteryMenuItem.Checked = False
        CountupMenuItem.Checked = True
        NowTimeMenuItem.Checked = False
        TimeTimer.Stop()
        CountdownTimer.Stop()
        ClassTimer.Stop()
        KitchenTimer.Stop()
        BatteryTimer.Stop()
        CountupTimer.Start()

    End Sub

    Private Sub ClassMenuItem_Click(sender As Object, e As EventArgs) Handles ClassMenuItem.Click, ClassMenuItemN.Click

        '授業タイマー開始時の処理

        TimerBar.Style = ProgressBarStyle.Blocks
        KitchenMenuItem.Checked = False
        CountupMenuItem.Checked = False
        CountdownMenuItem.Checked = False
        BatteryMenuItem.Checked = False
        ClassMenuItem.Checked = True
        NowTimeMenuItem.Checked = False
        TimeTimer.Stop()
        CountdownTimer.Stop()
        CountupTimer.Stop()
        KitchenTimer.Stop()
        BatteryTimer.Stop()
        ClassTimer.Start()

    End Sub

    Private Sub BatteryMenuItem_Click(sender As Object, e As EventArgs) Handles BatteryMenuItem.Click, BatteryMenuItemN.Click

        'バッテリータイマー開始時の処理

        TimerBar.Style = ProgressBarStyle.Blocks
        ClassMenuItem.Checked = False
        KitchenMenuItem.Checked = False
        CountupMenuItem.Checked = False
        CountdownMenuItem.Checked = False
        BatteryMenuItem.Checked = True
        NowTimeMenuItem.Checked = False
        TimerBar.Value = 0
        TimerBar.Maximum = 100
        TimeTimer.Stop()
        CountdownTimer.Stop()
        CountupTimer.Stop()
        KitchenTimer.Stop()
        ClassTimer.Stop()
        BatteryTimer.Start()

    End Sub
    Private Sub KitchenMenuItem_Click(sender As Object, e As EventArgs) Handles KitchenMenuItem.Click, KitchenMenuItemN.Click

        'キッチンタイマー開始時の処理

        Me.TopMost = False
        countti = InputBox("測る時間を入力してください（分）")
        Call TopmostMenuItemMenuItem_Click(Me, e)
        If IsNumeric(countti) = False Then Exit Sub
        ClassMenuItem.Checked = False
        CountupMenuItem.Checked = False
        CountdownMenuItem.Checked = False
        BatteryMenuItem.Checked = False
        NowTimeMenuItem.Checked = False
        TimerBar.Style = ProgressBarStyle.Blocks
        countti *= 600
        TimerBar.Value = 0
        TimerBar.Maximum = countti
        KitchenMenuItem.Checked = True
        TimeTimer.Stop()
        CountdownTimer.Stop()
        CountupTimer.Stop()
        ClassTimer.Stop()
        BatteryTimer.Stop()
        KitchenTimer.Start()

    End Sub

    Private Sub CountdownMenuItem_Click(sender As Object, e As EventArgs) Handles CountdownMenuItem.Click, CountdownMenuItemN.Click

        'カウントダウンタイマー開始時の処理

        Me.TopMost = False
        Dim TargetHour = InputBox("目標時間を入力してください（時）")
        If IsNumeric(TargetHour) = False Or TargetHour < 0 Or TargetHour > 23 Then MessageBox.Show("0-23の数字である必要があります", "", MessageBoxButtons.OK, MessageBoxIcon.Error) : Exit Sub
        Dim TargetMin = InputBox("目標時間を入力してください（分）")
        If IsNumeric(TargetMin) = False Or TargetMin < 0 Or TargetMin > 59 Then MessageBox.Show("0-59の数字である必要があります", "", MessageBoxButtons.OK, MessageBoxIcon.Error) : Exit Sub
        Call TopmostMenuItemMenuItem_Click(Me, e)
        counttm = New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, Int(TargetHour), Int(TargetMin), 0)
        ClassMenuItem.Checked = False
        CountupMenuItem.Checked = False
        KitchenMenuItem.Checked = False
        BatteryMenuItem.Checked = False
        CountdownMenuItem.Checked = True
        NowTimeMenuItem.Checked = False
        TimerBar.Style = ProgressBarStyle.Blocks
        Dim TimeDiff = DateDiff("s", Now, counttm)
        TimerBar.Value = 0
        TimerBar.Maximum = Math.Max(TimeDiff, 1)
        TimeTimer.Stop()
        CountupTimer.Stop()
        ClassTimer.Stop()
        BatteryTimer.Stop()
        KitchenTimer.Stop()
        CountdownTimer.Start()

    End Sub
    Private Sub KitchenTimer_Tick(sender As Object, e As EventArgs) Handles KitchenTimer.Tick

        countti -= 1
        Application.DoEvents()
        '透明化処理
        If OpacitySwitch = True And OpacityTimer < 120 And OpacityTimer >= 0 Then
            OpacityTimer += 1
            If OpacityTimer > 30 And OpacityTimer < 80 Then
                Me.Opacity = ((OpacityTimer - 30) * 2) / 100
            End If
        Else
            OpacitySwitch = False
            If OpacityTimer >= 0 Then OpacityTimer = 0
            Me.Opacity = "1"

        End If
        'タイマー終了で通知を送る
        If countti = 0 Then
            NotifyIcon.BalloonTipText = "タイマー終了"
            NotifyIcon.ShowBalloonTip(1000)
        End If
        'バーマイナスにならないように
        If countti < 0 Then TimerBar.Value = 0 Else TimerBar.Value = TimerBar.Maximum - countti
        '60秒以下で赤に変更
        If countti <= 600 Then
            TimerLabel.Text = Math.Max(Int(countti / 10), 0)
            TimerLabel.ForeColor = Color.Red
        Else
            '文字溢れ処理
            If ShowSecSwitch = True Then
                If countti > 99990 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "9999" Else TimerLabel.Text = Int(countti / 10)
            Else
                If countti > 59990 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "99:59" Else TimerLabel.Text = Format(Int((countti) / 600), "00") & ":" & Format(Int((countti Mod 600) / 10), "00")
            End If
            TimerLabel.ForeColor = Color.Black
        End If
        'タイトルタイマー
        If TitleShowTimerMenuItem.Checked = True Then Me.Text = Format(Int((countti) / 600), "00") & ":" & Format(Int((countti Mod 600) / 10), "00") Else If Me.Text <> "" Then Me.Text = ""
        '通知アイコンタイトル
        If ShowSecSwitch = False Then
            NotifyIcon.Text = "キッチンタイマー : 残り " & TimerLabel.Text & " / " & Format(Int(TimerBar.Maximum / 600), "00") & ":" & Format(TimerBar.Maximum Mod 600 / 10, "00")
        Else
            NotifyIcon.Text = "キッチンタイマー : 残り " & TimerLabel.Text & " / " & TimerBar.Maximum / 10
        End If

    End Sub
    Private Sub CountupTimer_Tick(sender As Object, e As EventArgs) Handles CountupTimer.Tick
        countti += 1
        Application.DoEvents()
        '透明化処理
        If OpacitySwitch = True And OpacityTimer < 120 And OpacityTimer >= 0 Then
            OpacityTimer += 1
            If OpacityTimer > 30 And OpacityTimer < 80 Then
                Me.Opacity = ((OpacityTimer - 30) * 2) / 100
            End If
        Else
            OpacitySwitch = False
            If OpacityTimer >= 0 Then OpacityTimer = 0
            Me.Opacity = "1"

        End If
        TimerBar.Maximum = 10
        TimerBar.Value = Math.Min(Math.Max(0, (countti Mod 10) * 2.5), 10)
        '文字溢れ処理
        If ShowSecSwitch = True Then
            If countti > 99990 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "9999" Else TimerLabel.Text = Int(countti / 10)
        Else
            If countti > 59990 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "99:59" Else TimerLabel.Text = Format(Int((countti) / 600), "00") & ":" & Format(Int((countti Mod 600) / 10), "00")
        End If
        TimerLabel.ForeColor = Color.Black
        'タイトルタイマー
        If TitleShowTimerMenuItem.Checked = True Then Me.Text = Format(Int((countti) / 600), "00") & ":" & Format(Int((countti Mod 600) / 10), "00") Else If Me.Text <> "" Then Me.Text = ""
        '通知アイコンタイトル
        NotifyIcon.Text = "カウントアップタイマー : " & TimerLabel.Text
    End Sub

    Private Sub NotifyRightMenuStrip_Opened(sender As Object, e As EventArgs) Handles NotifyRightMenuStrip.Opened

        ChangeSecMenuItem.Text = "表示切替"
        If ClassTimer.Enabled Or CountupTimer.Enabled Or CountdownTimer.Enabled Or KitchenMenuItem.Enabled Then ChangeSecMenuItem.Text = "秒数表示切替"
        If BatteryTimer.Enabled Then ChangeSecMenuItem.Text = "残り時間表示切替"
        If TimeTimer.Enabled Then ChangeSecMenuItem.Text = "日付表示切替"

        '状態に応じて表示すると最小化するを切り替える

        If Me.WindowState = FormWindowState.Minimized Then
            ShowMenuItem.Visible = True
            MinimizeMenuItem.Visible = False
        Else
            ShowMenuItem.Visible = False
            MinimizeMenuItem.Visible = True
        End If

    End Sub

    Private Sub MinimizeMenuItem_Click(sender As Object, e As EventArgs) Handles MinimizeMenuItem.Click

        Me.WindowState = FormWindowState.Minimized

    End Sub

    Private Sub ShowMenuItem_Click(sender As Object, e As EventArgs) Handles ShowMenuItem.Click

        Me.WindowState = FormWindowState.Normal

    End Sub

    Private Sub ExitMenuItemN_Click(sender As Object, e As EventArgs) Handles ExitMenuItemN.Click

        If Me.FormBorderStyle <> FormBorderStyle.None Then
        Else
            Call CanSizeChangeMenuItem_Click(Me, e)
            Me.Height = Me.Height + 26
            TimerMenuStrip.Visible = True
            TimerLabel.Top = TimerLabel.Top + 26
            TimerBar.Top = TimerBar.Top + 26
            LockSwitch = False
        End If
        Me.Close()

    End Sub

    Private Sub NTPTimeMenuItem_Click(sender As Object, e As EventArgs) Handles NTPTimeMenuItem.Click
        Call NtpTimeGet()
    End Sub

    Private Sub ChangeLockMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeLockMenuItem.Click

        If Me.FormBorderStyle <> FormBorderStyle.None Then
            Me.WindowState = FormWindowState.Normal
            Me.Height = Me.Height - 26
            Me.FormBorderStyle = FormBorderStyle.None
            TimerMenuStrip.Visible = False
            TimerLabel.Top = TimerLabel.Top - 26
            TimerBar.Top = TimerBar.Top - 26
            If OpacityTimer >= 0 Then OpacityTimer = Math.Max(80, OpacityTimer)
            OpacitySwitch = True
            LockSwitch = True
        Else
            Me.WindowState = FormWindowState.Normal
            Call CanSizeChangeMenuItem_Click(Me, e)
            Me.Height = Me.Height + 26
            TimerMenuStrip.Visible = True
            TimerLabel.Top = TimerLabel.Top + 26
            TimerBar.Top = TimerBar.Top + 26
            LockSwitch = False
        End If

    End Sub

    Private Sub ChangeSecMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeSecMenuItem.Click

        If ShowSecSwitch = False Then
            ShowSecSwitch = True
            ShowsecMenuItem.Checked = True
        Else
            ShowSecSwitch = False
            ShowsecMenuItem.Checked = False
        End If

    End Sub
    Public Function CheckTerm() As Integer

        Dim ClassTime1 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 10, 45, 0)
        Dim ClassTime2 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 12, 30, 0)
        Dim ClassTime3 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 15, 0, 0)
        Dim ClassTime4 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 16, 45, 0)
        Dim ClassTime5 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 18, 30, 0)
        Dim ClassTime6 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 20, 0, 0)
        Dim BreakTime1 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 9, 15, 0)
        Dim BreakTime2 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 11, 0, 0)
        Dim BreakTime3 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 13, 30, 0)
        Dim BreakTime4 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 15, 15, 0)
        Dim BreakTime5 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 17, 0, 0)
        Dim Deviation As New TimeSpan(0, 0, 0, DeviationToolStripTextBox.Text)
        Dim NowTime As DateTime = DateTime.Now() + Deviation

        Application.DoEvents()
        Select Case NowTime
            Case Is < BreakTime1
                Return 1
            Case Is < ClassTime1
                Return 2
            Case Is < BreakTime2
                Return 2
            Case Is < ClassTime2
                Return 3
            Case Is < BreakTime3
                Return 3
            Case Is < ClassTime3
                Return 4
            Case Is < BreakTime4
                Return 4
            Case Is < ClassTime4
                Return 5
            Case Is < BreakTime5
                Return 5
            Case Is < ClassTime5
                Return 6
            Case Is < ClassTime6
                Return 7
        End Select
        Return 0
    End Function
    Private Sub NextTimeMenuItemN_Click(sender As Object, e As EventArgs) Handles NextTimeMenuItemN.Click

        If NextTimeMenuItem.ForeColor <> Color.Gray Then
            If Me.WindowState = FormWindowState.Minimized Then
                Dim term = CheckTerm()
                Dim lessonname = "", Room = ""
                Dim TimeTable = JsonConvert.DeserializeObject(Of RootTimeTable)(My.Settings.TimeTable)
                For i As Integer = 0 To TimeTable.timetable.Count - 1
                    If TimeTable.timetable(i).week = Weekday(Today, FirstDayOfWeek.Monday) And TimeTable.timetable(i).term = term Then
                        lessonname = TimeTable.timetable(i).lesson_name
                        Room = TimeTable.timetable(i).room
                    End If
                Next
                If lessonname = "" Then
                    NotifyIcon.BalloonTipTitle = ""
                    NotifyIcon.BalloonTipText = "次の時間は授業がないようです。"
                    If ShowNotifyMenuItem.Checked Then NotifyIcon.ShowBalloonTip(1000)
                    Me.Visible = False
                Else
                    NotifyIcon.BalloonTipTitle = ""
                    NotifyIcon.BalloonTipText = "次の時間は " & Room & " 教室で、 " & lessonname & " です。"
                    If ShowNotifyMenuItem.Checked Then NotifyIcon.ShowBalloonTip(1000)
                End If
            Else
                NextTimeForm.Show()
            End If
        Else
            Me.TopMost = False
            Dim r = MessageBox.Show("時間割の取得に失敗しています。" & vbNewLine & "インターネットの接続を確認してください。" & vbNewLine & vbNewLine & "前回取得したデータを使用しますか？", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error)
            If r = vbYes Then TimeTableGetOK = True : NextTimeMenuItem.ForeColor = Color.Black : NextTimeMenuItemN.ForeColor = Color.Black : NextTimeForm.Show() : WebTimer.Stop() : Exit Sub
            r = MessageBox.Show("今すぐ再試行しますか？", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error)
            If r = vbYes Then WebTimer.Start() : WebTimer.Interval = 1500
            Call TopmostMenuItemMenuItem_Click(Me, e)
        End If

    End Sub

    Private Sub BatteryTimer_Tick(sender As Object, e As EventArgs) Handles BatteryTimer.Tick
        Application.DoEvents()
        If OpacitySwitch = True And OpacityTimer < 120 And OpacityTimer >= 0 Then
            OpacityTimer += 1
            If OpacityTimer > 30 And OpacityTimer < 80 Then
                Me.Opacity = ((OpacityTimer - 30) * 2) / 100
            End If
        Else
            OpacitySwitch = False
            If OpacityTimer >= 0 Then OpacityTimer = 0
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

            If ShowSecSwitch = True Then
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
    End Sub

    Private Sub NotifyIcon_Click(sender As Object, e As EventArgs) Handles NotifyIcon.DoubleClick
        NotifyIcon.BalloonTipTitle = ""
        NotifyIcon.BalloonTipText = NotifyIcon.Text
        NotifyIcon.ShowBalloonTip(1000)
    End Sub

    Private Sub NotifyIcon_BalloonTipClicked(sender As Object, e As EventArgs) Handles NotifyIcon.BalloonTipClicked
        If Me.WindowState = FormWindowState.Minimized Then
            Call ShowMenuItem_Click(Me, e)
        Else
            Call MinimizeMenuItem_Click(Me, e)
        End If
    End Sub

    Private Sub ModeMenuItemN_DropDownOpened(sender As Object, e As EventArgs) Handles ModeMenuItemN.DropDownOpened

        ClassMenuItemN.Checked = ClassMenuItem.Checked
        CountupMenuItemN.Checked = CountupMenuItem.Checked
        KitchenMenuItemN.Checked = KitchenMenuItem.Checked
        CountdownMenuItemN.Checked = CountdownMenuItem.Checked
        BatteryMenuItemN.Checked = BatteryMenuItem.Checked
        NowTimeMenuStripN.Checked = NowTimeMenuItem.Checked

    End Sub

    Private Sub ExportMenuItem_Click(sender As Object, e As EventArgs) Handles ExportMenuItem.Click
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

    Private Sub ImportMenuItem_Click(sender As Object, e As EventArgs) Handles ImportMenuItem.Click
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
                    Call TimerHeightToolStripTextBox_LostFocus(TimerHeightToolStripTextBox, e)
                Case 7
                    BarHeightToolStripTextBox.Text = sr.ReadLine()
                    Call BarHeightToolStripTextBox_LostFocus(BarHeightToolStripTextBox, e)
                Case 8
                    LeftSpaceToolStripTextBox.Text = sr.ReadLine()
                    Call LeftSpaceToolStripTextBox_Leave(LeftSpaceToolStripTextBox, e)
                Case 9
                    RightSpaceToolStripTextBox.Text = sr.ReadLine()
                    Call RightSpaceToolStripTextBox_Leave(RightSpaceToolStripTextBox, e)
                Case 10
                    MarginToolStripTextBox.Text = sr.ReadLine()
                    Call MarginToolStripTextBox_LostFocus(MarginToolStripTextBox, e)
                Case 11
                    SpaceWidthToolStripTextBox.Text = sr.ReadLine()
                    Call SpaceWidthToolStripTextBox_LostFocus(SpaceWidthToolStripTextBox, e)
            End Select
        End While
        '閉じる
        sr.Close()
        MessageBox.Show("インポートが完了しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub TimerMenuStrip_MouseHover(sender As Object, e As EventArgs) Handles TimerMenuStrip.MouseHover
        TimerMenuStrip.LayoutStyle = ToolStripLayoutStyle.Flow
    End Sub

    Private Sub TimerMenuStrip_MouseLeave(sender As Object, e As EventArgs) Handles TimerMenuStrip.MouseLeave
        TimerMenuStrip.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow
    End Sub

    Private Sub Term5MenuItem_Click(sender As Object, e As EventArgs) Handles Term5MenuItem.CheckedChanged
        If Term5MenuItem.Checked Then
            Term6MenuItem.Enabled = True
        Else

            Term6MenuItem.Checked = False
            Term6MenuItem.Enabled = False

        End If
    End Sub

    Private Sub ChangeBatteryMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeBatteryMenuItem.Click
        If ChangeBatteryMenuItem.Checked = True Then
            BatteryMonitorTimer.Start()
        Else
            BatteryMonitorTimer.Stop()
        End If
    End Sub

    Private Sub BatteryMonitorTimer_Tick(sender As Object, e As EventArgs) Handles BatteryMonitorTimer.Tick
        Dim pls As PowerLineStatus = SystemInformation.PowerStatus.PowerLineStatus
        Static plsm As PowerLineStatus
        If plsm <> pls Then
            Select Case pls
                Case PowerLineStatus.Offline
                    Call BatteryMenuItem_Click(sender, e)
                Case PowerLineStatus.Online
                    Call ClassMenuItem_Click(sender, e)
                Case PowerLineStatus.Unknown
            End Select
        End If
        plsm = pls
    End Sub

    Private Sub MovePadMenuItem_Click(sender As Object, e As EventArgs) Handles MovePadMenuItem.Click
        Movepad.Show()
    End Sub

    Private Sub NowTimeMenuItem_Click(sender As Object, e As EventArgs) Handles NowTimeMenuItem.Click, NowTimeMenuStripN.Click

        '現在時刻表示開始時の処理

        TimerLabel.ForeColor = Color.Black
        ClassMenuItem.Checked = False
        CountupMenuItem.Checked = False
        KitchenMenuItem.Checked = False
        BatteryMenuItem.Checked = False
        CountdownMenuItem.Checked = False
        NowTimeMenuItem.Checked = True
        TimerBar.Style = ProgressBarStyle.Blocks
        TimerBar.Maximum = 6000
        TimeTimer.Stop()
        CountupTimer.Stop()
        ClassTimer.Stop()
        BatteryTimer.Stop()
        KitchenTimer.Stop()
        TimerBar.Value = 0
        TimeTimer.Start()

    End Sub

    Private Sub TimeTimer_Tick(sender As Object, e As EventArgs) Handles TimeTimer.Tick

        If OpacitySwitch = True And OpacityTimer < 120 And OpacityTimer >= 0 Then
            OpacityTimer += 1
            If OpacityTimer > 30 And OpacityTimer < 80 Then
                Me.Opacity = ((OpacityTimer - 30) * 2) / 100
            End If
        Else
            OpacitySwitch = False
            If OpacityTimer >= 0 Then OpacityTimer = 0
            Me.Opacity = "1"
        End If

        Dim Deviation As New TimeSpan(0, 0, 0, DeviationToolStripTextBox.Text)
        Dim NowTime As DateTime = DateTime.Now() + Deviation
        Dim dtmNow As DateTime
        dtmNow = DateTime.Now() + Deviation
        If ShowSecSwitch = False Then
            TimerBar.Maximum = 6000
            TimerLabel.Text = Format(NowTime, "HH:mm")
            TimerBar.Value = Format(NowTime, "ss") * 100 + (dtmNow.Millisecond \ 10)
            NotifyIcon.Text = "現在時刻 : " & TimerLabel.Text & ":" & Format(NowTime, "ss") & "." & (dtmNow.Millisecond \ 10)
        Else
            TimerBar.Maximum = 86400
            TimerLabel.Text = Format(NowTime, "M/d")
            TimerBar.Value = Format(NowTime, "HH") * 3600 + Format(NowTime, "mm") * 60 + Format(NowTime, "ss")
            NotifyIcon.Text = "今日の日付 : " & TimerLabel.Text & " (" & Format((TimerBar.Value / TimerBar.Maximum) * 100, "0.00") & "%)"
        End If

    End Sub

    Private Sub ModeMenuItemN_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PerfectTransparentMenuItem_Click(sender As Object, e As EventArgs) Handles PerfectTransparentMenuItem.Click
        If sender.checked Then
            MessageBox.Show("固定状態を解除したいときは、通知領域のアイコン右クリックの" & vbNewLine & "固定状態解除で固定状態を解除してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub FuncMenuItem_DropDownOpened(sender As Object, e As EventArgs) Handles FuncMenuItem.DropDownOpened
        ShowsecMenuItem.Text = "表示切替"
        If ClassTimer.Enabled Or CountupTimer.Enabled Or CountdownTimer.Enabled Or KitchenMenuItem.Enabled Then ShowsecMenuItem.Text = "秒数表示"
        If BatteryTimer.Enabled Then ShowsecMenuItem.Text = "残り時間表示"
        If TimeTimer.Enabled Then ShowsecMenuItem.Text = "日付表示"

    End Sub

    Private Sub SettingMenuItem_Click(sender As Object, e As EventArgs) Handles SettingMenuItem.Click
        If Term5MenuItem.Checked = True Then
            SpecialTimeMenuItem.Checked = True
        Else
            SpecialTimeMenuItem.Checked = False
        End If
        ShowNextTermMenuItem.Checked = IDPassMenuItem.Checked
        If DeviationToolStripTextBox.Text <> 0 Then DeviationMenuItem.Checked = True Else DeviationMenuItem.Checked = False
    End Sub

    Private Sub CountdownTimer_Tick(sender As Object, e As EventArgs) Handles CountdownTimer.Tick
        Dim TargetTime = New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, Hour(counttm), Minute(counttm), 0)
        Dim Deviation As New TimeSpan(0, 0, 0, DeviationToolStripTextBox.Text)
        Dim TimeDiff = DateDiff("s", Now + Deviation, TargetTime)
        Application.DoEvents()
        If OpacitySwitch = True And OpacityTimer < 120 And OpacityTimer >= 0 Then
            OpacityTimer += 1
            If OpacityTimer > 30 And OpacityTimer < 80 Then
                Me.Opacity = ((OpacityTimer - 30) * 2) / 100
            End If
        Else
            OpacitySwitch = False
            If OpacityTimer >= 0 Then OpacityTimer = 0
            Me.Opacity = "1"

        End If
        If TimeDiff < 0 Then TimerBar.Value = 0 Else TimerBar.Value = Math.Max(0, TimerBar.Maximum - TimeDiff)
        If TimeDiff = 0 Then
            NotifyIcon.BalloonTipTitle = ""
            NotifyIcon.BalloonTipText = "時間になりました"
            NotifyIcon.ShowBalloonTip(1000)
        End If
        If TimeDiff < 60 Then
            TimerLabel.Text = Math.Max(Int(TimeDiff), 0)
            TimerLabel.ForeColor = Color.Red
        Else
            If ShowSecSwitch = True Then
                If TimeDiff > 9999 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "9999" Else TimerLabel.Text = Int(TimeDiff)
            Else
                If TimeDiff > 5999 And CanSizeChangeMenuItem.Checked = False Then TimerLabel.Text = "99:59" Else TimerLabel.Text = Format(Int(TimeDiff / 60), "00") & ":" & Format(Int((TimeDiff Mod 60)), "00")
            End If
            TimerLabel.ForeColor = Color.Black
        End If
        If TitleShowTimerMenuItem.Checked = True Then Me.Text = Format(Int(TimeDiff / 60), "00") & ":" & Format(Int((TimeDiff Mod 60)), "00") Else If Me.Text <> "" Then Me.Text = ""
        NotifyIcon.Text = "カウントダウンタイマー : 残り " & TimerLabel.Text & " (" & Format(Hour(counttm), "00") & ":" & Format(Minute(counttm), "00") & "まで)"
    End Sub
    Private Sub Size3MenuItem_Click(sender As Object, e As EventArgs) Handles Size3MenuItem.Click
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

    Private Sub Main_MouseEnter(sender As Object, e As EventArgs) Handles TimerBar.MouseEnter, MyBase.MouseEnter, TimerLabel.MouseEnter
        If PerfectTransparentMenuItem.Checked = True Then
            If OpacityTimer < 120 And Me.FormBorderStyle = FormBorderStyle.None And OpacityTimer >= 0 Then
                OpacitySwitch = True
                OpacityTimer = 0
                Me.Opacity = "0"
            End If
        Else
            If OpacityTimer < 40 And Me.FormBorderStyle = FormBorderStyle.None And OpacityTimer >= 0 Then
                OpacitySwitch = True
                OpacityTimer = 0
                Me.Opacity = "0"
            End If
        End If
    End Sub

    Private Sub BarHeightToolStripTextBox_LostFocus(sender As Object, e As EventArgs) Handles BarHeightToolStripTextBox.LostFocus
        If IsNumeric(sender.Text) Then
            TimerBar.Height = Val(sender.Text)
        Else
            sender.Text = TimerBar.Height
        End If
    End Sub

    Private Sub MarginToolStripTextBox_LostFocus(sender As Object, e As EventArgs) Handles MarginToolStripTextBox.LostFocus
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

    Private Sub DeviationToolStripTextBox_TextChanged(sender As Object, e As EventArgs) Handles DeviationToolStripTextBox.TextChanged
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