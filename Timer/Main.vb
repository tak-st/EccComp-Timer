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
        Dim ln = 0, rm = 0
        Dim TimeTable = JsonConvert.DeserializeObject(Of RootTimeTable)(My.Settings.TIME)
        For i As Integer = 0 To TimeTable.timetable.Count - 1
            If TimeTable.timetable(i).week = Weekday(Today, FirstDayOfWeek.Monday) And TimeTable.timetable(i).term = term Then
                ln = TimeTable.timetable(i).lesson_name
                rm = TimeTable.timetable(i).room
            End If
        Next
        If ln = "" Then
            NotifyIcon1.BalloonTipTitle = "お疲れ様です。"
            NotifyIcon1.BalloonTipText = "次の時間は授業がないようです。"
            If 通知ToolStripMenuItem.Checked Then NotifyIcon1.ShowBalloonTip(1000)
            Me.Visible = False
        Else
            NotifyIcon1.BalloonTipTitle = "お疲れ様です。"
            NotifyIcon1.BalloonTipText = "次の時間は " & rm & " 教室で、 " & ln & " です。"
            If 通知ToolStripMenuItem.Checked Then NotifyIcon1.ShowBalloonTip(1000)
            If ポップアップ表示ToolStripMenuItem.Checked = False Then Form3.Show()
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
                ToolStripTextBox3.Text = Int(ts.TotalSeconds)
            End If
        End Try
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.TopMost = True
        nsize = 48
        Me.Height = Me.Height + 26
        MenuStrip1.Visible = True
        Label1.Top = Label1.Top + 26
        ProgressBar1.Top = ProgressBar1.Top + 26
        If My.Settings.posX >= 0 Then
            Me.StartPosition = FormStartPosition.Manual
            Me.Left = My.Settings.posX
            Me.Top = My.Settings.posY
            フォームの位置を記憶するToolStripMenuItem.Checked = True
        End If
        If My.Settings.sizeX >= 0 Then
            Me.Width = My.Settings.sizeX
            Me.Height = My.Settings.sizeY
            フォームのサイズを記憶するFToolStripMenuItem.Checked = True
        End If
        常に最前面ToolStripMenuItem.Checked = My.Settings.Top
        バー表示ToolStripMenuItem.Checked = My.Settings.Bar
        秒数表示ToolStripMenuItem.Checked = My.Settings.Second
        固定時マウスオーバーで透明化ToolStripMenuItem.Checked = My.Settings.Hide
        フォームのサイズ変更を可能にするToolStripMenuItem.Checked = My.Settings.SC
        If My.Settings.ID <> "-1" Then
            学籍番号で連携ToolStripMenuItem.Checked = True
            次の授業NToolStripMenuItem.Visible = True
            ToolStripMenuItem7.Visible = True
            通知ToolStripMenuItem.Checked = My.Settings.noti
            ポップアップ表示ToolStripMenuItem.Checked = My.Settings.popup
            WebTimer.Start()
        End If
        Call 常に最前面ToolStripMenuItem_Click(Me, e)
        Call 秒数表示ToolStripMenuItem_Click(Me, e)
        Call 固定時マウスオーバーで透明化ToolStripMenuItem_Click(Me, e)
        Call フォームのサイズ変更を可能にするToolStripMenuItem_Click(Me, e)

        Select Case My.Settings.Size
            Case 0
                ToolStripSeparator2.Visible = False
                高度な設定ToolStripMenuItem.Visible = False
                文字の大きさToolStripMenuItem.Enabled = False
                高度ToolStripMenuItem.Visible = True
                If My.Settings.pad < 0 Then
                    Label1.Top = 26 - (My.Settings.pad * -1)
                Else
                    Label1.Padding = New Padding(0, My.Settings.pad, 0, 0)
                End If
                Label1.Font = My.Settings.FontT
                Label1.Height = My.Settings.heightT
                ProgressBar1.Height = My.Settings.heightB
                ProgressBar1.Top = Label1.Top + Label1.Height + My.Settings.widthTB
                Label1.Left = My.Settings.LS
                ProgressBar1.Left = Label1.Left
                Label1.Width = Me.Width - Label1.Left - My.Settings.RS
                ProgressBar1.Width = Label1.Width
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
        If My.Settings.kotei Then
            Me.Height = Me.Height - 26
            Me.FormBorderStyle = FormBorderStyle.None
            MenuStrip1.Visible = False
            Label1.Top = Label1.Top - 26
            If Me.Width < 40 Then Me.Width = 40
            If Me.Height < 40 Then Me.Height = 40
            ProgressBar1.Top = ProgressBar1.Top - 26
            If Oti >= 0 Then Oti = Math.Max(80, Oti)
            OSw = True
            sSw = True
            起動時固定状態で開始するLToolStripMenuItem.Checked = True

        End If
        If My.Settings.bmode Then
            Call バッテリー残量ToolStripMenuItem_Click(sender, e)
        End If
        Windowsの起動時に自動的に起動するToolStripMenuItem.Checked = My.Settings.StartUp
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
                    Label1.Font = New System.Drawing.Font(Namev, Sizevs, Stv)
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
                    ToolStripTextBox1.Text = sr.ReadLine()
                    Call ToolStripTextBox1_LostFocus(ToolStripTextBox1, e)
                Case 7
                    ToolStripTextBox2.Text = sr.ReadLine()
                    Call ToolStripTextBox2_LostFocus(ToolStripTextBox2, e)
                Case 8
                    ToolStripTextBox7.Text = sr.ReadLine()
                    Call ToolStripTextBox7_Leave(ToolStripTextBox7, e)
                Case 9
                    ToolStripTextBox8.Text = sr.ReadLine()
                    Call ToolStripTextBox8_Leave(ToolStripTextBox8, e)
                Case 10
                    ToolStripTextBox10.Text = sr.ReadLine()
                    Call ToolStripTextBox10_LostFocus(ToolStripTextBox10, e)
                Case 11
                    ToolStripTextBox6.Text = sr.ReadLine()
                    Call ToolStripTextBox6_LostFocus(ToolStripTextBox6, e)
            End Select
        End While
        '閉じる
        sr.Close()
    End Sub

    Private Sub Label1_DoubleClick(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles Label1.DoubleClick
        If e.Button = MouseButtons.Right Then
            If Me.FormBorderStyle <> FormBorderStyle.None Then
                Me.Height = Me.Height - 26
                Me.FormBorderStyle = FormBorderStyle.None
                MenuStrip1.Visible = False
                Label1.Top = Label1.Top - 26
                ProgressBar1.Top = ProgressBar1.Top - 26
                If Oti >= 0 Then Oti = Math.Max(80, Oti)
                OSw = True
                sSw = True
            Else
                Call フォームのサイズ変更を可能にするToolStripMenuItem_Click(Me, e)
                Me.Height = Me.Height + 26
                MenuStrip1.Visible = True
                Label1.Top = Label1.Top + 26
                ProgressBar1.Top = ProgressBar1.Top + 26
                sSw = False
            End If
        End If
        If e.Button = MouseButtons.Left Then
            If Secsw = False Then
                Secsw = True
                秒数表示ToolStripMenuItem.Checked = True
            Else
                Secsw = False
                秒数表示ToolStripMenuItem.Checked = False
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim jikan
        Dim sabun = 0
        Dim infome = ""
        Dim term = 0
        Dim Ntu = False
        If 学籍番号で連携ToolStripMenuItem.Checked = True And SOK Then Ntu = True
        Dim j1 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 10, 45, 0)
        Dim j2 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 12, 30, 0)
        Dim j3 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 15, 0, 0)
        Dim j4 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 16, 45, 0)
        Dim j5 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 18, 30, 0)
        Dim y1 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 9, 15, 0)
        Dim y2 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 11, 0, 0)
        Dim y3 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 13, 30, 0)
        Dim y4 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 15, 15, 0)
        Dim y5 As New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, 17, 0, 0)
        Dim tss As New TimeSpan(0, 0, 0, ToolStripTextBox3.Text)
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
            ProgressBar1.Maximum = 900
            infome = "1時限目待ち時間"
        Else
            If jikan < j1 Then
                Nsw = True
                sabun = DateDiff("s", j1, jikan)
                ProgressBar1.Maximum = 5400
                infome = "1時限目"
            Else
                If jikan < y2 Then
                    If Nsw And Ntu Then Nsw = False : Call noti()
                    sabun = DateDiff("s", y2, jikan)
                    ProgressBar1.Maximum = 900
                    infome = "2時限目待ち時間"
                Else
                    If jikan < j2 Then
                        Nsw = True
                        sabun = DateDiff("s", j2, jikan)
                        ProgressBar1.Maximum = 5400
                        infome = "2時限目"
                    Else
                        If jikan < y3 Then
                            If Nsw And Ntu Then Nsw = False : Call noti()
                            sabun = DateDiff("s", y3, jikan)
                            ProgressBar1.Maximum = 3600
                            infome = "昼休み"
                        Else
                            If jikan < j3 Then
                                Nsw = True
                                sabun = DateDiff("s", j3, jikan)
                                ProgressBar1.Maximum = 5400
                                infome = "3時限目"
                            Else

                                If jikan < y4 Then
                                    If Nsw And Ntu Then Nsw = False : Call noti()
                                    sabun = DateDiff("s", y4, jikan)
                                    ProgressBar1.Maximum = 900
                                    infome = "4時限目待ち時間"
                                Else

                                    If jikan < j4 Then
                                        Nsw = True
                                        sabun = DateDiff("s", j4, jikan)
                                        ProgressBar1.Maximum = 5400
                                        infome = "4時限目"
                                    Else
                                        If ToolStripMenuItem4.Checked Then
                                            If jikan < y5 Then
                                                If Nsw And Ntu Then Nsw = False : Call noti()
                                                sabun = DateDiff("s", y5, jikan)
                                                ProgressBar1.Maximum = 900
                                                infome = "5時限目待ち時間"
                                            Else
                                                If jikan < j5 Then
                                                    Nsw = True
                                                    sabun = DateDiff("s", j5, jikan)
                                                    ProgressBar1.Maximum = 5400
                                                    infome = "5時限目"
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

        sabun = sabun * -1
        If sabun <= 60 Then
            Label1.Text = sabun
            Label1.ForeColor = Color.Red
        Else
            If Secsw = True Then
                If sabun > 9999 And フォームのサイズ変更を可能にするToolStripMenuItem.Checked = False Then Label1.Text = "9999" Else Label1.Text = sabun
            Else
                If sabun > 5999 And フォームのサイズ変更を可能にするToolStripMenuItem.Checked = False Then Label1.Text = "99:59" Else Label1.Text = Format(Int(sabun / 60), "00") & ":" & Format(sabun Mod 60, "00")
            End If
            Label1.ForeColor = Color.Black
        End If
        If タイトルにタイマー表示ToolStripMenuItem.Checked = True And sabun >= 61 Then Me.Text = Format(Int(sabun / 60), "00") & ":" & Format(sabun Mod 60, "00") Else If Me.Text <> "" Then Me.Text = ""
        If タイトルにタイマー表示ToolStripMenuItem.Checked = True And sabun < 61 Then Me.Text = sabun
        If (ProgressBar1.Maximum - sabun) < 0 Then ProgressBar1.Value = 0 Else ProgressBar1.Value = ProgressBar1.Maximum - sabun
        NotifyIcon1.Text = infome & " : 残り " & Label1.Text
    End Sub

    Private Sub バー表示ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles バー表示ToolStripMenuItem.Click
        If Not バー表示ToolStripMenuItem.Checked Then
            ProgressBar1.Visible = False
            Me.Height = Me.Height - Int(ProgressBar1.Height * 1.5)

        Else
            ProgressBar1.Visible = True
            If manual Then Me.Height = Me.Height + Int(ProgressBar1.Height * 1.5)

        End If
    End Sub

    Private Sub 秒数表示ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 秒数表示ToolStripMenuItem.Click
        If 秒数表示ToolStripMenuItem.Checked Then
            Secsw = True
        Else
            Secsw = False
        End If
    End Sub

    Private Sub 固定時マウスオーバーで透明化ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 固定時マウスオーバーで透明化ToolStripMenuItem.Click
        If Not 固定時マウスオーバーで透明化ToolStripMenuItem.Checked Then
            Oti = -1
        Else
            Oti = 120
        End If
    End Sub

    Private Sub 常に最前面ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 常に最前面ToolStripMenuItem.Click
        If Not 常に最前面ToolStripMenuItem.Checked Then
            Me.TopMost = False
        Else
            Me.TopMost = True
        End If
    End Sub

    Private Sub ヘルプToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ヘルプToolStripMenuItem.Click
        MessageBox.Show("時間表示を左ダブルクリック：秒数表示切り替え" & vbNewLine & "時間表示を右ダブルクリック：タイマーの位置を固定、コンパクト化", Me.Text, MessageBoxButtons.OK)
    End Sub

    Private Sub 休み時間時次の授業情報表示ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 休み時間時次の授業情報表示ToolStripMenuItem.Click
        'MessageBox.Show("ツールとの連携が必要です。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub 小ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 小ToolStripMenuItem.Click
        Label1.Font = New Font("UD デジタル 教科書体 NK-B", 24, FontStyle.Bold)
        Me.Width = 153
        Me.Height = 113
        Label1.Width = 125
        Label1.Height = 105
        Label1.Left = 6
        ProgressBar1.Top = 59
        ProgressBar1.Left = 6
        ProgressBar1.Width = Label1.Width
        ProgressBar1.Height = 9
        小ToolStripMenuItem.Checked = True
        コンパクトToolStripMenuItem.Checked = False
        中ToolStripMenuItem.Checked = False
        ビッグToolStripMenuItem.Checked = False

    End Sub

    Private Sub 中ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 中ToolStripMenuItem.Click
        Label1.Font = New Font("UD デジタル 教科書体 NK-B", 48, FontStyle.Bold)
        Me.Width = 267
        Me.Height = 159
        Label1.Width = 234
        Label1.Height = 91
        Label1.Left = 12
        ProgressBar1.Left = 12
        ProgressBar1.Top = 94
        ProgressBar1.Width = Label1.Width
        ProgressBar1.Height = 19
        小ToolStripMenuItem.Checked = False
        コンパクトToolStripMenuItem.Checked = False
        中ToolStripMenuItem.Checked = True
        ビッグToolStripMenuItem.Checked = False
    End Sub

    Private Sub フォームのサイズ変更を可能にするToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles フォームのサイズ変更を可能にするToolStripMenuItem.Click
        If フォームのサイズ変更を可能にするToolStripMenuItem.Checked Then
            Me.FormBorderStyle = FormBorderStyle.Sizable
        Else
            Me.FormBorderStyle = FormBorderStyle.FixedSingle
        End If
    End Sub

    Private Sub ビッグToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ビッグToolStripMenuItem.Click
        Label1.Font = New Font("UD デジタル 教科書体 NK-B", 64, FontStyle.Bold)
        Me.Width = 355
        Me.Height = 191
        Label1.Width = 315
        Label1.Height = 114
        Label1.Left = 15
        ProgressBar1.Left = Label1.Left
        ProgressBar1.Top = 118
        ProgressBar1.Width = Label1.Width
        ProgressBar1.Height = 24
        小ToolStripMenuItem.Checked = False
        コンパクトToolStripMenuItem.Checked = False
        中ToolStripMenuItem.Checked = False
        ビッグToolStripMenuItem.Checked = True
    End Sub
    Private Sub Windowsの起動時に自動的に起動するToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Windowsの起動時に自動的に起動するToolStripMenuItem.Click
        If Not Windowsの起動時に自動的に起動するToolStripMenuItem.Checked Then
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

    Private Sub AboutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AboutToolStripMenuItem.Click
        Dim ver As System.Diagnostics.FileVersionInfo
        ver = System.Diagnostics.FileVersionInfo.GetVersionInfo(
        System.Reflection.Assembly.GetExecutingAssembly().Location)
        MessageBox.Show("授業時間タイマー（ECC Comp.)　v" & ver.FileVersion & vbNewLine & "© 2018-2019 Takuya Shintani", "about", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub アプリと連携ToolStripMenuItem_Click(sender As Object, e As EventArgs)
        MessageBox.Show("ツールのインストールが必要です。", Me.Text, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub 学籍番号で連携ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 学籍番号で連携ToolStripMenuItem.Click
        If 学籍番号で連携ToolStripMenuItem.Checked Then
            学籍番号で連携ToolStripMenuItem.Checked = False
            My.Settings.ID = "-1"
            次の授業NToolStripMenuItem.Visible = False
            ToolStripMenuItem7.Visible = False
            Nsw = False
        Else
            IDInput.ShowDialog()
            If My.Settings.ID = "-1" Then

            Else
                次の授業NToolStripMenuItem.Visible = True
                次の授業NToolStripMenuItem.ForeColor = Color.Gray
                ToolStripMenuItem7.Visible = True
                ToolStripMenuItem7.ForeColor = Color.Gray
                通知ToolStripMenuItem.Checked = True
                ポップアップ表示ToolStripMenuItem.Checked = True
                学籍番号で連携ToolStripMenuItem.Checked = True
                WebTimer.Start()
                WebTimer.Interval = 1500
            End If
        End If
    End Sub

    Private Sub 次の授業NToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 次の授業NToolStripMenuItem.Click
        If 次の授業NToolStripMenuItem.ForeColor <> Color.Gray Then
            Form3.Show()
        Else
            Me.TopMost = False
            Dim r = MessageBox.Show("時間割の取得に失敗しています。" & vbNewLine & "インターネットの接続を確認してください。" & vbNewLine & vbNewLine & "前回取得したデータを使用しますか？", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error)
            If r = vbYes Then SOK = True : 次の授業NToolStripMenuItem.ForeColor = Color.Black : ToolStripMenuItem7.ForeColor = Color.Black : Form3.Show() : WebTimer.Stop() : Exit Sub
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
        Dim password = My.Settings.PASS

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
        My.Settings.TIME = TableJson
        sr.Close()
        st.Close()
        SOK = True
        次の授業NToolStripMenuItem.Enabled = True
        次の授業NToolStripMenuItem.ForeColor = Color.Black
        ToolStripMenuItem7.Enabled = True
        ToolStripMenuItem7.ForeColor = Color.Black
        WebTimer.Stop()
    End Sub

    Private Sub 終了XToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 終了XToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub フォントの変更ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles フォントの変更ToolStripMenuItem.Click
        FontDialog1.Font = Label1.Font
        FontDialog1.Color = Label1.ForeColor
        If FontDialog1.ShowDialog <> DialogResult.Cancel Then
            Label1.Font = FontDialog1.Font
            Label1.ForeColor = FontDialog1.Color
            My.Settings.FontT = FontDialog1.Font
        End If
    End Sub

    Private Sub ToolStripTextBox1_LostFocus(sender As Object, e As EventArgs) Handles ToolStripTextBox1.LostFocus
        If IsNumeric(ToolStripTextBox1.Text) Then
            Label1.Height = Val(ToolStripTextBox1.Text)
            'ProgressBar1.Top = Label1.Top + Label1.Height + Val(ToolStripTextBox6.Text)
        Else
            ToolStripTextBox1.Text = Label1.Height
        End If
    End Sub

    Private Sub ToolStripTextBox6_LostFocus(sender As Object, e As EventArgs) Handles ToolStripTextBox6.LostFocus
        If IsNumeric(sender.Text) Then
            ProgressBar1.Top = Label1.Top + Label1.Height + Val(sender.Text)
        Else
            sender.Text = ProgressBar1.Top - (Label1.Top + Label1.Height)
        End If
    End Sub


    Private Sub ToolStripTextBox7_Leave(sender As Object, e As EventArgs) Handles ToolStripTextBox7.LostFocus
        If IsNumeric(sender.Text) Then
            Label1.Left = Val(sender.Text)
            ProgressBar1.Left = Val(sender.Text)
        Else
            sender.Text = Label1.Left
        End If
    End Sub

    Private Sub ToolStripTextBox8_Leave(sender As Object, e As EventArgs) Handles ToolStripTextBox8.LostFocus
        If IsNumeric(sender.Text) Then
            Label1.Width = Me.Width - Label1.Left - Val(sender.Text)
            ProgressBar1.Width = Label1.Width
        Else
            sender.Text = Me.Width - Label1.Left - Label1.Width
        End If
    End Sub

    Private Sub 高度なデザイン設定をONにするToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 高度な設定ToolStripMenuItem.Click
        高度ToolStripMenuItem.Visible = True
        フォームのサイズ変更を可能にするToolStripMenuItem.Checked = True
        Call フォームのサイズ変更を可能にするToolStripMenuItem_Click(sender, e)
        フォームのサイズを記憶するFToolStripMenuItem.Checked = True
        ToolStripSeparator2.Visible = False
        My.Settings.Size = 0
        高度な設定ToolStripMenuItem.Visible = False
        文字の大きさToolStripMenuItem.Enabled = False
        My.Settings.FontT = Label1.Font
        My.Settings.heightT = Label1.Height
        My.Settings.heightB = ProgressBar1.Height
        My.Settings.widthTB = ProgressBar1.Top - (Label1.Top + Label1.Height)
        My.Settings.LS = Label1.Left
        My.Settings.RS = Me.Width - Label1.Left - Label1.Width
        My.Settings.pad = 0
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click

        ToolStripSeparator2.Visible = True
        高度ToolStripMenuItem.Visible = False
        My.Settings.Size = 3
        Call 中ToolStripMenuItem_Click(sender, e)
        高度な設定ToolStripMenuItem.Visible = True
        文字の大きさToolStripMenuItem.Enabled = True
    End Sub

    Private Sub 高度ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 高度ToolStripMenuItem.Click
        ToolStripTextBox1.Text = Label1.Height
        ToolStripTextBox2.Text = ProgressBar1.Height
        ToolStripTextBox6.Text = ProgressBar1.Top - (Label1.Top + Label1.Height)
        ToolStripTextBox7.Text = Label1.Left
        ToolStripTextBox8.Text = Me.Width - Label1.Left - Label1.Width
        If Label1.Padding.Top = 0 Then ToolStripTextBox10.Text = (26 - Label1.Top) * -1 Else ToolStripTextBox10.Text = Label1.Padding.Top
    End Sub
    Private Sub 文字の場所を上にずらすToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 文字の場所を上下にずらすToolStripMenuItem.Click

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If My.Settings.Size <> 0 Then
            If 小ToolStripMenuItem.Checked Then My.Settings.Size = 1
            If コンパクトToolStripMenuItem.Checked Then My.Settings.Size = 2
            If 中ToolStripMenuItem.Checked Then My.Settings.Size = 3
            If ビッグToolStripMenuItem.Checked Then My.Settings.Size = 4
        End If
        My.Settings.Top = 常に最前面ToolStripMenuItem.Checked
        My.Settings.Bar = バー表示ToolStripMenuItem.Checked
        My.Settings.Second = 秒数表示ToolStripMenuItem.Checked
        My.Settings.Hide = 固定時マウスオーバーで透明化ToolStripMenuItem.Checked
        My.Settings.SC = フォームのサイズ変更を可能にするToolStripMenuItem.Checked
        If フォームの位置を記憶するToolStripMenuItem.Checked = True Then
            My.Settings.posX = Me.Left
            My.Settings.posY = Me.Top
        Else
            My.Settings.posX = -1
            My.Settings.posY = -1
        End If
        If フォームのサイズを記憶するFToolStripMenuItem.Checked = True Then
            My.Settings.sizeX = Me.Width
            My.Settings.sizeY = Me.Height
        Else
            My.Settings.sizeX = -1
            My.Settings.sizeY = -1
        End If
        My.Settings.kotei = 起動時固定状態で開始するLToolStripMenuItem.Checked
        My.Settings.StartUp = Windowsの起動時に自動的に起動するToolStripMenuItem.Checked
        My.Settings.FontT = Label1.Font
        My.Settings.heightT = Label1.Height
        My.Settings.heightB = ProgressBar1.Height
        My.Settings.widthTB = ProgressBar1.Top - (Label1.Top + Label1.Height)
        My.Settings.LS = Label1.Left
        My.Settings.RS = Me.Width - Label1.Left - Label1.Width
        My.Settings.pad = Val(ToolStripTextBox10.Text)
        My.Settings.noti = 通知ToolStripMenuItem.Checked
        My.Settings.popup = ポップアップ表示ToolStripMenuItem.Checked
        If バッテリー残量ToolStripMenuItem.Checked Then My.Settings.bmode = True Else My.Settings.bmode = False
        'End
    End Sub
    Private Sub カウントアップタイマーToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles カウントアップタイマーToolStripMenuItem.Click, ToolStripMenuItem10.Click
        countti = 0
        キッチンタイマーToolStripMenuItem.Checked = False
        授業時間タイマーToolStripMenuItem.Checked = False
        カウントダウンタイマーToolStripMenuItem.Checked = False
        バッテリー残量ToolStripMenuItem.Checked = False
        カウントアップタイマーToolStripMenuItem.Checked = True
        CountdownTimer.Stop()
        Timer1.Stop()
        kitchenTimer.Stop()
        BatteryTimer.Stop()
        CountupTimer.Start()
    End Sub

    Private Sub 授業時間タイマーToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 授業時間タイマーToolStripMenuItem.Click, ToolStripMenuItem9.Click
        キッチンタイマーToolStripMenuItem.Checked = False
        カウントアップタイマーToolStripMenuItem.Checked = False
        カウントダウンタイマーToolStripMenuItem.Checked = False
        バッテリー残量ToolStripMenuItem.Checked = False
        ProgressBar1.Style = ProgressBarStyle.Blocks
        授業時間タイマーToolStripMenuItem.Checked = True
        CountdownTimer.Stop()
        CountupTimer.Stop()
        kitchenTimer.Stop()
        BatteryTimer.Stop()
        Timer1.Start()
    End Sub

    Private Sub バッテリー残量ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles バッテリー残量ToolStripMenuItem.Click, ToolStripMenuItem14.Click
        授業時間タイマーToolStripMenuItem.Checked = False
        キッチンタイマーToolStripMenuItem.Checked = False
        カウントアップタイマーToolStripMenuItem.Checked = False
        カウントダウンタイマーToolStripMenuItem.Checked = False
        ProgressBar1.Style = ProgressBarStyle.Blocks
        バッテリー残量ToolStripMenuItem.Checked = True
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = 100
        CountdownTimer.Stop()
        CountupTimer.Stop()
        kitchenTimer.Stop()
        Timer1.Stop()
        BatteryTimer.Start()

    End Sub
    Private Sub キッチンタイマーToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles キッチンタイマーToolStripMenuItem.Click, ToolStripMenuItem11.Click
        Me.TopMost = False
        countti = InputBox("測る時間を入力してください（分）")
        Call 常に最前面ToolStripMenuItem_Click(Me, e)
        If IsNumeric(countti) = False Then Exit Sub
        授業時間タイマーToolStripMenuItem.Checked = False
        カウントアップタイマーToolStripMenuItem.Checked = False
        カウントダウンタイマーToolStripMenuItem.Checked = False
        バッテリー残量ToolStripMenuItem.Checked = False
        ProgressBar1.Style = ProgressBarStyle.Blocks
        countti *= 600
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = countti
        キッチンタイマーToolStripMenuItem.Checked = True
        CountdownTimer.Stop()
        CountupTimer.Stop()
        Timer1.Stop()
        kitchenTimer.Start()
    End Sub

    Private Sub カウントダウンタイマーToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles カウントダウンタイマーToolStripMenuItem.Click, ToolStripMenuItem12.Click
        Me.TopMost = False
        Dim h = InputBox("目標時間を入力してください（時）")
        If IsNumeric(h) = False Or h < 0 Or h > 23 Then MessageBox.Show("0-23の数字である必要があります", "", MessageBoxButtons.OK, MessageBoxIcon.Error) : Exit Sub
        Dim m = InputBox("目標時間を入力してください（分）")
        If IsNumeric(m) = False Or m < 0 Or m > 59 Then MessageBox.Show("0-59の数字である必要があります", "", MessageBoxButtons.OK, MessageBoxIcon.Error) : Exit Sub
        Call 常に最前面ToolStripMenuItem_Click(Me, e)
        counttm = New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, Int(h), Int(m), 0)
        授業時間タイマーToolStripMenuItem.Checked = False
        カウントアップタイマーToolStripMenuItem.Checked = False
        キッチンタイマーToolStripMenuItem.Checked = False
        バッテリー残量ToolStripMenuItem.Checked = False
        ProgressBar1.Style = ProgressBarStyle.Blocks
        Dim sabun = DateDiff("s", Now, counttm)
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = Math.Max(sabun, 1)
        カウントダウンタイマーToolStripMenuItem.Checked = True
        CountupTimer.Stop()
        Timer1.Stop()
        BatteryTimer.Stop()
        kitchenTimer.Stop()
        CountdownTimer.Start()
    End Sub
    Private Sub kitchenTimer_Tick(sender As Object, e As EventArgs) Handles kitchenTimer.Tick
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
            NotifyIcon1.BalloonTipText = "タイマー終了"
            NotifyIcon1.ShowBalloonTip(1000)
        End If
        If countti < 0 Then ProgressBar1.Value = 0 Else ProgressBar1.Value = ProgressBar1.Maximum - countti
        If countti <= 600 Then
            Label1.Text = Math.Max(Int(countti / 10), 0)
            Label1.ForeColor = Color.Red
        Else
            If Secsw = True Then
                If countti > 99990 And フォームのサイズ変更を可能にするToolStripMenuItem.Checked = False Then Label1.Text = "9999" Else Label1.Text = Int(countti / 10)
            Else
                If countti > 59990 And フォームのサイズ変更を可能にするToolStripMenuItem.Checked = False Then Label1.Text = "99:59" Else Label1.Text = Format(Int((countti) / 600), "00") & ":" & Format(Int((countti Mod 600) / 10), "00")
            End If
            Label1.ForeColor = Color.Black
        End If
        If タイトルにタイマー表示ToolStripMenuItem.Checked = True Then Me.Text = Format(Int((countti) / 600), "00") & ":" & Format(Int((countti Mod 600) / 10), "00") Else If Me.Text <> "" Then Me.Text = ""
        If Secsw = False Then
            NotifyIcon1.Text = "キッチンタイマー : 残り " & Label1.Text & " / " & Format(Int(ProgressBar1.Maximum / 600), "00") & ":" & Format(ProgressBar1.Maximum Mod 600 / 10, "00")
        Else
            NotifyIcon1.Text = "キッチンタイマー : 残り " & Label1.Text & " / " & ProgressBar1.Maximum / 10
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
        ProgressBar1.Maximum = 10
        ProgressBar1.Value = Math.Min(Math.Max(0, (countti Mod 10) * 2.5), 10)
        'ProgressBar1.Style = ProgressBarStyle.Marquee
        If Secsw = True Then
            If countti > 99990 And フォームのサイズ変更を可能にするToolStripMenuItem.Checked = False Then Label1.Text = "9999" Else Label1.Text = Int(countti / 10)
        Else
            If countti > 59990 And フォームのサイズ変更を可能にするToolStripMenuItem.Checked = False Then Label1.Text = "99:59" Else Label1.Text = Format(Int((countti) / 600), "00") & ":" & Format(Int((countti Mod 600) / 10), "00")
        End If
        Label1.ForeColor = Color.Black
        If タイトルにタイマー表示ToolStripMenuItem.Checked = True Then Me.Text = Format(Int((countti) / 600), "00") & ":" & Format(Int((countti Mod 600) / 10), "00") Else If Me.Text <> "" Then Me.Text = ""
        NotifyIcon1.Text = "カウントアップタイマー : " & Label1.Text
    End Sub

    Private Sub ContextMenuStrip1_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles ContextMenuStrip1.Opening
        If Me.WindowState = FormWindowState.Minimized Then
            表示するToolStripMenuItem.Visible = True
            最小化するToolStripMenuItem.Visible = False
        Else
            表示するToolStripMenuItem.Visible = False
            最小化するToolStripMenuItem.Visible = True
        End If
    End Sub

    Private Sub 最小化するToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 最小化するToolStripMenuItem.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub 表示するToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 表示するToolStripMenuItem.Click
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub 終了ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 終了ToolStripMenuItem.Click
        If Me.FormBorderStyle <> FormBorderStyle.None Then
        Else
            Call フォームのサイズ変更を可能にするToolStripMenuItem_Click(Me, e)
            Me.Height = Me.Height + 26
            MenuStrip1.Visible = True
            Label1.Top = Label1.Top + 26
            ProgressBar1.Top = ProgressBar1.Top + 26
            sSw = False
        End If
        Me.Close()
    End Sub

    Private Sub ネットワークでシステム時刻を補正ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ネットワークでシステム時刻を補正ToolStripMenuItem.Click
        Call ntp()
    End Sub

    Private Sub ToolStripMenuItem5_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem5.Click
        If Me.FormBorderStyle <> FormBorderStyle.None Then
            Me.Height = Me.Height - 26
            Me.FormBorderStyle = FormBorderStyle.None
            MenuStrip1.Visible = False
            Label1.Top = Label1.Top - 26
            ProgressBar1.Top = ProgressBar1.Top - 26
            If Oti >= 0 Then Oti = Math.Max(80, Oti)
            OSw = True
            sSw = True
        Else
            Call フォームのサイズ変更を可能にするToolStripMenuItem_Click(Me, e)
            Me.Height = Me.Height + 26
            MenuStrip1.Visible = True
            Label1.Top = Label1.Top + 26
            ProgressBar1.Top = ProgressBar1.Top + 26
            sSw = False
        End If
    End Sub

    Private Sub ToolStripMenuItem6_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem6.Click
        If Secsw = False Then
            Secsw = True
            秒数表示ToolStripMenuItem.Checked = True
        Else
            Secsw = False
            秒数表示ToolStripMenuItem.Checked = False
        End If
    End Sub

    Private Sub ToolStripMenuItem7_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem7.Click
        If 次の授業NToolStripMenuItem.ForeColor <> Color.Gray Then
            If Me.WindowState = FormWindowState.Minimized Then
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
                Dim ln = 0, rm = 0
                Dim TimeTable = JsonConvert.DeserializeObject(Of RootTimeTable)(My.Settings.TIME)
                For i As Integer = 0 To TimeTable.timetable.Count - 1
                    If TimeTable.timetable(i).week = Weekday(Today, FirstDayOfWeek.Monday) And TimeTable.timetable(i).term = term Then
                        ln = TimeTable.timetable(i).lesson_name
                        rm = TimeTable.timetable(i).room
                    End If
                Next
                If ln = "" Then
                    NotifyIcon1.BalloonTipTitle = ""
                    NotifyIcon1.BalloonTipText = "次の時間は授業がないようです。"
                    If 通知ToolStripMenuItem.Checked Then NotifyIcon1.ShowBalloonTip(1000)
                    Me.Visible = False
                Else
                    NotifyIcon1.BalloonTipTitle = ""
                    NotifyIcon1.BalloonTipText = "次の時間は " & rm & " 教室で、 " & ln & " です。"
                    If 通知ToolStripMenuItem.Checked Then NotifyIcon1.ShowBalloonTip(1000)
                End If
            Else
                Form3.Show()
            End If
        Else
            Me.TopMost = False
            Dim r = MessageBox.Show("時間割の取得に失敗しています。" & vbNewLine & "インターネットの接続を確認してください。" & vbNewLine & vbNewLine & "前回取得したデータを使用しますか？", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Error)
            If r = vbYes Then SOK = True : 次の授業NToolStripMenuItem.ForeColor = Color.Black : ToolStripMenuItem7.ForeColor = Color.Black : Form3.Show() : WebTimer.Stop() : Exit Sub
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
            Label1.Text = "???"
            Label1.ForeColor = Color.Gray
        Else

            If Secsw = True Then
                'バッテリー残量（時間）
                If -1 < blr Then
                    Label1.Text = Int(blr / 60) & "m"
                    ProgressBar1.Value = blp * 100
                    Select Case pls
                        Case PowerLineStatus.Offline
                            If blp > 0.2 Then Label1.ForeColor = Color.Black Else Label1.ForeColor = Color.Red
                            Exit Select
                        Case PowerLineStatus.Online
                            Label1.ForeColor = Color.DarkGreen
                            Exit Select
                        Case PowerLineStatus.Unknown
                            Label1.Text = "???"
                            Label1.ForeColor = Color.Gray
                            Exit Select
                    End Select
                Else
                    'バッテリー残量（割合）
                    Label1.Text = "--m"
                    ProgressBar1.Value = blp * 100

                    Select Case pls
                        Case PowerLineStatus.Offline
                            If blp > 0.2 Then Label1.ForeColor = Color.Black Else Label1.ForeColor = Color.Red
                            Exit Select
                        Case PowerLineStatus.Online
                            Label1.ForeColor = Color.DarkGreen
                            Exit Select
                        Case PowerLineStatus.Unknown
                            Label1.Text = "???"
                            Label1.ForeColor = Color.Gray
                            Exit Select
                    End Select
                End If
            Else
                'バッテリー残量（割合）
                Label1.Text = blp * 100 & "%"
                ProgressBar1.Value = blp * 100

                Select Case pls
                    Case PowerLineStatus.Offline
                        If blp > 0.2 Then Label1.ForeColor = Color.Black Else Label1.ForeColor = Color.Red
                        Exit Select
                    Case PowerLineStatus.Online
                        Label1.ForeColor = Color.DarkGreen
                        Exit Select
                    Case PowerLineStatus.Unknown
                        Label1.Text = "???"
                        Label1.ForeColor = Color.Gray
                        Exit Select
                End Select
            End If

        End If

        Select Case pls
            Case PowerLineStatus.Offline
                If blr > -1 Then
                    NotifyIcon1.Text = "バッテリー : " & blp * 100 & "% (残り時間 " & Int(blr / 60) & "分 " & blr Mod 60 & "秒)"
                Else
                    NotifyIcon1.Text = "バッテリー : " & blp * 100 & "% (残り時間 計算中または不明)"
                End If
                Exit Select
            Case PowerLineStatus.Online
                NotifyIcon1.Text = "バッテリー : " & blp * 100 & "% (充電中)"
                Exit Select
            Case PowerLineStatus.Unknown
                NotifyIcon1.Text = "バッテリー : 状態不明"
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

    Private Sub NotifyIcon1_Click(sender As Object, e As EventArgs) Handles NotifyIcon1.DoubleClick
        NotifyIcon1.BalloonTipText = NotifyIcon1.Text
        NotifyIcon1.ShowBalloonTip(1000)
    End Sub

    Private Sub NotifyIcon1_BalloonTipClicked(sender As Object, e As EventArgs) Handles NotifyIcon1.BalloonTipClicked
        If Me.WindowState = FormWindowState.Minimized Then
            Call 表示するToolStripMenuItem_Click(Me, e)
        Else
            Call 最小化するToolStripMenuItem_Click(Me, e)
        End If
    End Sub

    Private Sub ToolStripMenuItem8_DropDownOpened(sender As Object, e As EventArgs) Handles ToolStripMenuItem8.DropDownOpened
        ToolStripMenuItem9.Checked = 授業時間タイマーToolStripMenuItem.Checked
        ToolStripMenuItem10.Checked = カウントアップタイマーToolStripMenuItem.Checked
        ToolStripMenuItem11.Checked = キッチンタイマーToolStripMenuItem.Checked
        ToolStripMenuItem12.Checked = カウントダウンタイマーToolStripMenuItem.Checked
        'ToolStripMenuItem13.Checked
        ToolStripMenuItem14.Checked = バッテリー残量ToolStripMenuItem.Checked
    End Sub

    Private Sub 通知ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 通知ToolStripMenuItem.Click

    End Sub

    Private Sub 高度な設定をエクスポートToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 高度な設定をエクスポートToolStripMenuItem.Click
        Dim textFile As System.IO.StreamWriter
        SaveFileDialog1.ShowDialog()
        If SaveFileDialog1.FileName = "" Then Exit Sub
        textFile = New System.IO.StreamWriter(SaveFileDialog1.FileName, False, System.Text.Encoding.Default)
        If フォームの位置を記憶するToolStripMenuItem.Checked = True Then
            Dim r = MessageBox.Show("フォームの位置情報をファイルに加えますか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If r = vbYes Then
                My.Settings.posX = Me.Left
                My.Settings.posY = Me.Top
            Else
                My.Settings.posX = -1
                My.Settings.posY = -1
            End If
        End If
        If フォームのサイズを記憶するFToolStripMenuItem.Checked = True Then
            Dim r = MessageBox.Show("フォームのサイズ情報をファイルに加えますか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If r = vbYes Then
                My.Settings.sizeX = Me.Width
                My.Settings.sizeY = Me.Height
            Else
                My.Settings.sizeX = -1
                My.Settings.sizeY = -1
            End If
        End If
        My.Settings.FontT = Label1.Font
        My.Settings.heightT = Label1.Height
        My.Settings.heightB = ProgressBar1.Height
        My.Settings.widthTB = ProgressBar1.Top - (Label1.Top + Label1.Height)
        My.Settings.LS = Label1.Left
        My.Settings.RS = Me.Width - Label1.Left - Label1.Width
        My.Settings.pad = Val(ToolStripTextBox10.Text)
        textFile.WriteLine(My.Settings.FontT.Name)
        textFile.WriteLine(My.Settings.FontT.Size)
        textFile.WriteLine(My.Settings.FontT.Style)
        'textFile.WriteLine(My.Settings.FontT.Italic)
        textFile.WriteLine(My.Settings.posX)
        textFile.WriteLine(My.Settings.posY)
        textFile.WriteLine(My.Settings.sizeX)
        textFile.WriteLine(My.Settings.sizeY)
        textFile.WriteLine(My.Settings.heightT)
        textFile.WriteLine(My.Settings.heightB)
        textFile.WriteLine(My.Settings.LS)
        textFile.WriteLine(My.Settings.RS)
        textFile.WriteLine(My.Settings.pad)
        textFile.WriteLine(My.Settings.widthTB)
        textFile.Close()
        MessageBox.Show("エクスポートが完了しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub 高度な設定をインポートToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles 高度な設定をインポートToolStripMenuItem.Click
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName = "" Then Exit Sub
        Dim sr As New System.IO.StreamReader(OpenFileDialog1.FileName,
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
                    Label1.Font = New System.Drawing.Font(Namev, Sizevs, Stv)
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
                    ToolStripTextBox1.Text = sr.ReadLine()
                    Call ToolStripTextBox1_LostFocus(ToolStripTextBox1, e)
                Case 7
                    ToolStripTextBox2.Text = sr.ReadLine()
                    Call ToolStripTextBox2_LostFocus(ToolStripTextBox2, e)
                Case 8
                    ToolStripTextBox7.Text = sr.ReadLine()
                    Call ToolStripTextBox7_Leave(ToolStripTextBox7, e)
                Case 9
                    ToolStripTextBox8.Text = sr.ReadLine()
                    Call ToolStripTextBox8_Leave(ToolStripTextBox8, e)
                Case 10
                    ToolStripTextBox10.Text = sr.ReadLine()
                    Call ToolStripTextBox10_LostFocus(ToolStripTextBox10, e)
                Case 11
                    ToolStripTextBox6.Text = sr.ReadLine()
                    Call ToolStripTextBox6_LostFocus(ToolStripTextBox6, e)
            End Select
        End While
        '閉じる
        sr.Close()
        MessageBox.Show("インポートが完了しました。", "", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub CountdownTimer_Tick(sender As Object, e As EventArgs) Handles CountdownTimer.Tick
        Dim j = New DateTime(Year(DateTime.Today), Month(DateTime.Today), DateTime.Today.Day, Hour(counttm), Minute(counttm), 0)
        Dim tss As New TimeSpan(0, 0, 0, ToolStripTextBox3.Text)
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
        If sabun < 0 Then ProgressBar1.Value = 0 Else ProgressBar1.Value = Math.Max(0, ProgressBar1.Maximum - sabun)
        If sabun = 0 Then
            NotifyIcon1.BalloonTipTitle = ""
            NotifyIcon1.BalloonTipText = "時間になりました"
            NotifyIcon1.ShowBalloonTip(1000)
        End If
        If sabun < 60 Then
            Label1.Text = Math.Max(Int(sabun), 0)
            Label1.ForeColor = Color.Red
        Else
            If Secsw = True Then
                If sabun > 9999 And フォームのサイズ変更を可能にするToolStripMenuItem.Checked = False Then Label1.Text = "9999" Else Label1.Text = Int(sabun)
            Else
                If sabun > 5999 And フォームのサイズ変更を可能にするToolStripMenuItem.Checked = False Then Label1.Text = "99:59" Else Label1.Text = Format(Int(sabun / 60), "00") & ":" & Format(Int((sabun Mod 60)), "00")
            End If
            Label1.ForeColor = Color.Black
        End If
        If タイトルにタイマー表示ToolStripMenuItem.Checked = True Then Me.Text = Format(Int(sabun / 60), "00") & ":" & Format(Int((sabun Mod 60)), "00") Else If Me.Text <> "" Then Me.Text = ""
        NotifyIcon1.Text = "カウントダウンタイマー : 残り " & Label1.Text & " (" & Format(Hour(counttm), "00") & ":" & Format(Minute(counttm), "00") & "まで)"
    End Sub
    Private Sub コンパクトToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles コンパクトToolStripMenuItem.Click
        Label1.Font = New Font("UD デジタル 教科書体 NK-B", 36, FontStyle.Bold)
        Me.Width = 210
        Me.Height = 133
        Label1.Width = 178
        Label1.Height = 98
        Label1.Left = 9
        ProgressBar1.Left = 9
        ProgressBar1.Top = 73
        ProgressBar1.Width = Label1.Width
        ProgressBar1.Height = 14
        小ToolStripMenuItem.Checked = False
        コンパクトToolStripMenuItem.Checked = True
        中ToolStripMenuItem.Checked = False
        ビッグToolStripMenuItem.Checked = False
    End Sub
    Private Sub ProgressBar1_DoubleClick(sender As Object, e As EventArgs) Handles ProgressBar1.DoubleClick
        Me.Close()
    End Sub

    Private Sub Form1_MouseEnter(sender As Object, e As EventArgs) Handles MyBase.MouseEnter, Label1.MouseEnter, ProgressBar1.MouseEnter
        If Oti < 40 And Me.FormBorderStyle = FormBorderStyle.None And Oti >= 0 Then
            OSw = True
            Oti = 0
            Me.Opacity = "0"
        End If
    End Sub

    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed
    End Sub

    Private Sub ToolStripTextBox2_LostFocus(sender As Object, e As EventArgs) Handles ToolStripTextBox2.LostFocus
        If IsNumeric(sender.Text) Then
            ProgressBar1.Height = Val(sender.Text)
        Else
            sender.Text = ProgressBar1.Height
        End If
    End Sub

    Private Sub ToolStripTextBox10_LostFocus(sender As Object, e As EventArgs) Handles ToolStripTextBox10.LostFocus
        If IsNumeric(sender.Text) Then
            If Val(sender.Text) < 0 Then
                Label1.Padding = New Padding(0, 0, 0, 0)
                Label1.Top = 26 - (Val(sender.Text) * -1)
            Else
                Label1.Top = 26
                Label1.Padding = New Padding(0, Val(sender.Text), 0, 0)
            End If
        Else
            If Label1.Padding.Top = 0 Then sender.Text = (26 - Label1.Top) * -1 Else sender.Text = Label1.Padding.Top
        End If
    End Sub

    Private Sub ToolStripTextBox3_TextChanged(sender As Object, e As EventArgs) Handles ToolStripTextBox3.TextChanged
        If Len(ToolStripTextBox3.Text) = 0 Or Not IsNumeric(ToolStripTextBox3.Text) Then ToolStripTextBox3.Text = "0"
    End Sub
End Class
'tokenのJsonファイルのデシリアライズ用クラス
Public Class Token
    'code, expireはいまのところ使い道なし(なんとなく残してる)
    Public Property code As String          'CD00001なら受信成功
    Public Property token As String
    Public Property expire As DateTimeOffset       'トークンの有効期限
End Class