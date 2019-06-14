<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.TimerLabel = New System.Windows.Forms.Label()
        Me.TimerBar = New System.Windows.Forms.ProgressBar()
        Me.ClassTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TimerMenuStrip = New System.Windows.Forms.MenuStrip()
        Me.FuncMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModeMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClassMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NowTimeMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CountupMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KitchenMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CountdownMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PomodoroMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BatteryMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TopmostMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowBarMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowsecMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SizeMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Size1MenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Size2MenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Size3MenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Size4MenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NextTimeMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FuncSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdvancedMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeFontMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimerHeightMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TimerHeightToolStripTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.BarHeightMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BarHeightToolStripTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.SpaceWidthToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SpaceWidthToolStripTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.LeftSpaceMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LeftSpaceToolStripTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.RightSpaceMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RightSpaceToolStripTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.MarginMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MarginToolStripTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.TitleShowTimerMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PerfectTransparentMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdvancedSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ImportMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AdvancedSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.AdvancedOffMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SpecialTimeMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Term5MenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Term6MenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransparentMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PositionSaveMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LockStartMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AutorunMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CanSizeChangeMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SizeSaveMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeBatteryMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowNextTermMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IDPassMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IDPassSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ShowNotifyMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowPopUpMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NTPTimeMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeviationMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeviationToolStripTextBox = New System.Windows.Forms.ToolStripTextBox()
        Me.SettingSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.AdvancedOnMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowAboutMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WebTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SelectFontDialog = New System.Windows.Forms.FontDialog()
        Me.CountupTimer = New System.Windows.Forms.Timer(Me.components)
        Me.KitchenTimer = New System.Windows.Forms.Timer(Me.components)
        Me.NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.NotifyRightMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ModeMenuItemN = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClassMenuItemN = New System.Windows.Forms.ToolStripMenuItem()
        Me.NowTimeMenuStripN = New System.Windows.Forms.ToolStripMenuItem()
        Me.CountupMenuItemN = New System.Windows.Forms.ToolStripMenuItem()
        Me.KitchenMenuItemN = New System.Windows.Forms.ToolStripMenuItem()
        Me.CountdownMenuItemN = New System.Windows.Forms.ToolStripMenuItem()
        Me.PomodoroMenuItemN = New System.Windows.Forms.ToolStripMenuItem()
        Me.BatteryMenuItemN = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeSecMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ChangeLockMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MovePadMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NextTimeMenuItemN = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MinimizeMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RightSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ExitMenuItemN = New System.Windows.Forms.ToolStripMenuItem()
        Me.CountdownTimer = New System.Windows.Forms.Timer(Me.components)
        Me.BatteryTimer = New System.Windows.Forms.Timer(Me.components)
        Me.SaveFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.OpenFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.BatteryMonitorTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TimeTimer = New System.Windows.Forms.Timer(Me.components)
        Me.TimerMenuStrip.SuspendLayout()
        Me.NotifyRightMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'TimerLabel
        '
        resources.ApplyResources(Me.TimerLabel, "TimerLabel")
        Me.TimerLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.TimerLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.TimerLabel.Name = "TimerLabel"
        '
        'TimerBar
        '
        resources.ApplyResources(Me.TimerBar, "TimerBar")
        Me.TimerBar.Name = "TimerBar"
        '
        'ClassTimer
        '
        Me.ClassTimer.Enabled = True
        '
        'TimerMenuStrip
        '
        Me.TimerMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FuncMenuItem, Me.AdvancedMenuItem, Me.SettingMenuItem, Me.HelpMenuItem, Me.AboutMenuItem})
        Me.TimerMenuStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow
        resources.ApplyResources(Me.TimerMenuStrip, "TimerMenuStrip")
        Me.TimerMenuStrip.Name = "TimerMenuStrip"
        '
        'FuncMenuItem
        '
        Me.FuncMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ModeMenuItem, Me.TopmostMenuItem, Me.ShowBarMenuItem, Me.ShowsecMenuItem, Me.SizeMenuItem, Me.NextTimeMenuItem, Me.FuncSeparator1, Me.ExitMenuItem})
        Me.FuncMenuItem.Name = "FuncMenuItem"
        resources.ApplyResources(Me.FuncMenuItem, "FuncMenuItem")
        '
        'ModeMenuItem
        '
        Me.ModeMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClassMenuItem, Me.NowTimeMenuItem, Me.CountupMenuItem, Me.KitchenMenuItem, Me.CountdownMenuItem, Me.PomodoroMenuItem, Me.BatteryMenuItem})
        Me.ModeMenuItem.Name = "ModeMenuItem"
        resources.ApplyResources(Me.ModeMenuItem, "ModeMenuItem")
        '
        'ClassMenuItem
        '
        Me.ClassMenuItem.Checked = True
        Me.ClassMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ClassMenuItem.Name = "ClassMenuItem"
        resources.ApplyResources(Me.ClassMenuItem, "ClassMenuItem")
        '
        'NowTimeMenuItem
        '
        Me.NowTimeMenuItem.Name = "NowTimeMenuItem"
        resources.ApplyResources(Me.NowTimeMenuItem, "NowTimeMenuItem")
        '
        'CountupMenuItem
        '
        Me.CountupMenuItem.Name = "CountupMenuItem"
        resources.ApplyResources(Me.CountupMenuItem, "CountupMenuItem")
        '
        'KitchenMenuItem
        '
        Me.KitchenMenuItem.Name = "KitchenMenuItem"
        resources.ApplyResources(Me.KitchenMenuItem, "KitchenMenuItem")
        '
        'CountdownMenuItem
        '
        Me.CountdownMenuItem.Name = "CountdownMenuItem"
        resources.ApplyResources(Me.CountdownMenuItem, "CountdownMenuItem")
        '
        'PomodoroMenuItem
        '
        Me.PomodoroMenuItem.Name = "PomodoroMenuItem"
        resources.ApplyResources(Me.PomodoroMenuItem, "PomodoroMenuItem")
        '
        'BatteryMenuItem
        '
        Me.BatteryMenuItem.Name = "BatteryMenuItem"
        resources.ApplyResources(Me.BatteryMenuItem, "BatteryMenuItem")
        '
        'TopmostMenuItem
        '
        Me.TopmostMenuItem.Checked = True
        Me.TopmostMenuItem.CheckOnClick = True
        Me.TopmostMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TopmostMenuItem.Name = "TopmostMenuItem"
        resources.ApplyResources(Me.TopmostMenuItem, "TopmostMenuItem")
        '
        'ShowBarMenuItem
        '
        Me.ShowBarMenuItem.Checked = True
        Me.ShowBarMenuItem.CheckOnClick = True
        Me.ShowBarMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShowBarMenuItem.Name = "ShowBarMenuItem"
        resources.ApplyResources(Me.ShowBarMenuItem, "ShowBarMenuItem")
        '
        'ShowsecMenuItem
        '
        Me.ShowsecMenuItem.CheckOnClick = True
        Me.ShowsecMenuItem.Name = "ShowsecMenuItem"
        resources.ApplyResources(Me.ShowsecMenuItem, "ShowsecMenuItem")
        '
        'SizeMenuItem
        '
        Me.SizeMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Size1MenuItem, Me.Size2MenuItem, Me.Size3MenuItem, Me.Size4MenuItem})
        Me.SizeMenuItem.Name = "SizeMenuItem"
        resources.ApplyResources(Me.SizeMenuItem, "SizeMenuItem")
        '
        'Size1MenuItem
        '
        Me.Size1MenuItem.Name = "Size1MenuItem"
        resources.ApplyResources(Me.Size1MenuItem, "Size1MenuItem")
        '
        'Size2MenuItem
        '
        Me.Size2MenuItem.Checked = True
        Me.Size2MenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Size2MenuItem.Name = "Size2MenuItem"
        resources.ApplyResources(Me.Size2MenuItem, "Size2MenuItem")
        '
        'Size3MenuItem
        '
        Me.Size3MenuItem.Name = "Size3MenuItem"
        resources.ApplyResources(Me.Size3MenuItem, "Size3MenuItem")
        '
        'Size4MenuItem
        '
        Me.Size4MenuItem.Name = "Size4MenuItem"
        resources.ApplyResources(Me.Size4MenuItem, "Size4MenuItem")
        '
        'NextTimeMenuItem
        '
        Me.NextTimeMenuItem.ForeColor = System.Drawing.Color.Gray
        Me.NextTimeMenuItem.Name = "NextTimeMenuItem"
        resources.ApplyResources(Me.NextTimeMenuItem, "NextTimeMenuItem")
        '
        'FuncSeparator1
        '
        Me.FuncSeparator1.Name = "FuncSeparator1"
        resources.ApplyResources(Me.FuncSeparator1, "FuncSeparator1")
        '
        'ExitMenuItem
        '
        Me.ExitMenuItem.Name = "ExitMenuItem"
        resources.ApplyResources(Me.ExitMenuItem, "ExitMenuItem")
        '
        'AdvancedMenuItem
        '
        Me.AdvancedMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChangeFontMenuItem, Me.TimerHeightMenuItem, Me.BarHeightMenuItem, Me.SpaceWidthToolStripMenuItem, Me.LeftSpaceMenuItem, Me.RightSpaceMenuItem, Me.MarginMenuItem, Me.TitleShowTimerMenuItem, Me.PerfectTransparentMenuItem, Me.AdvancedSeparator1, Me.ImportMenuItem, Me.ExportMenuItem, Me.AdvancedSeparator2, Me.AdvancedOffMenuItem})
        Me.AdvancedMenuItem.Name = "AdvancedMenuItem"
        resources.ApplyResources(Me.AdvancedMenuItem, "AdvancedMenuItem")
        '
        'ChangeFontMenuItem
        '
        Me.ChangeFontMenuItem.Name = "ChangeFontMenuItem"
        resources.ApplyResources(Me.ChangeFontMenuItem, "ChangeFontMenuItem")
        '
        'TimerHeightMenuItem
        '
        Me.TimerHeightMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.TimerHeightToolStripTextBox})
        Me.TimerHeightMenuItem.Name = "TimerHeightMenuItem"
        resources.ApplyResources(Me.TimerHeightMenuItem, "TimerHeightMenuItem")
        '
        'TimerHeightToolStripTextBox
        '
        Me.TimerHeightToolStripTextBox.Name = "TimerHeightToolStripTextBox"
        resources.ApplyResources(Me.TimerHeightToolStripTextBox, "TimerHeightToolStripTextBox")
        '
        'BarHeightMenuItem
        '
        Me.BarHeightMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BarHeightToolStripTextBox})
        Me.BarHeightMenuItem.Name = "BarHeightMenuItem"
        resources.ApplyResources(Me.BarHeightMenuItem, "BarHeightMenuItem")
        '
        'BarHeightToolStripTextBox
        '
        Me.BarHeightToolStripTextBox.Name = "BarHeightToolStripTextBox"
        resources.ApplyResources(Me.BarHeightToolStripTextBox, "BarHeightToolStripTextBox")
        '
        'SpaceWidthToolStripMenuItem
        '
        Me.SpaceWidthToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SpaceWidthToolStripTextBox})
        Me.SpaceWidthToolStripMenuItem.Name = "SpaceWidthToolStripMenuItem"
        resources.ApplyResources(Me.SpaceWidthToolStripMenuItem, "SpaceWidthToolStripMenuItem")
        '
        'SpaceWidthToolStripTextBox
        '
        Me.SpaceWidthToolStripTextBox.Name = "SpaceWidthToolStripTextBox"
        resources.ApplyResources(Me.SpaceWidthToolStripTextBox, "SpaceWidthToolStripTextBox")
        '
        'LeftSpaceMenuItem
        '
        Me.LeftSpaceMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LeftSpaceToolStripTextBox})
        Me.LeftSpaceMenuItem.Name = "LeftSpaceMenuItem"
        resources.ApplyResources(Me.LeftSpaceMenuItem, "LeftSpaceMenuItem")
        '
        'LeftSpaceToolStripTextBox
        '
        Me.LeftSpaceToolStripTextBox.Name = "LeftSpaceToolStripTextBox"
        resources.ApplyResources(Me.LeftSpaceToolStripTextBox, "LeftSpaceToolStripTextBox")
        '
        'RightSpaceMenuItem
        '
        Me.RightSpaceMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RightSpaceToolStripTextBox})
        Me.RightSpaceMenuItem.Name = "RightSpaceMenuItem"
        resources.ApplyResources(Me.RightSpaceMenuItem, "RightSpaceMenuItem")
        '
        'RightSpaceToolStripTextBox
        '
        Me.RightSpaceToolStripTextBox.Name = "RightSpaceToolStripTextBox"
        resources.ApplyResources(Me.RightSpaceToolStripTextBox, "RightSpaceToolStripTextBox")
        '
        'MarginMenuItem
        '
        Me.MarginMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MarginToolStripTextBox})
        Me.MarginMenuItem.Name = "MarginMenuItem"
        resources.ApplyResources(Me.MarginMenuItem, "MarginMenuItem")
        '
        'MarginToolStripTextBox
        '
        Me.MarginToolStripTextBox.Name = "MarginToolStripTextBox"
        resources.ApplyResources(Me.MarginToolStripTextBox, "MarginToolStripTextBox")
        '
        'TitleShowTimerMenuItem
        '
        Me.TitleShowTimerMenuItem.CheckOnClick = True
        Me.TitleShowTimerMenuItem.Name = "TitleShowTimerMenuItem"
        resources.ApplyResources(Me.TitleShowTimerMenuItem, "TitleShowTimerMenuItem")
        '
        'PerfectTransparentMenuItem
        '
        Me.PerfectTransparentMenuItem.CheckOnClick = True
        Me.PerfectTransparentMenuItem.Name = "PerfectTransparentMenuItem"
        resources.ApplyResources(Me.PerfectTransparentMenuItem, "PerfectTransparentMenuItem")
        '
        'AdvancedSeparator1
        '
        Me.AdvancedSeparator1.Name = "AdvancedSeparator1"
        resources.ApplyResources(Me.AdvancedSeparator1, "AdvancedSeparator1")
        '
        'ImportMenuItem
        '
        Me.ImportMenuItem.Name = "ImportMenuItem"
        resources.ApplyResources(Me.ImportMenuItem, "ImportMenuItem")
        '
        'ExportMenuItem
        '
        Me.ExportMenuItem.Name = "ExportMenuItem"
        resources.ApplyResources(Me.ExportMenuItem, "ExportMenuItem")
        '
        'AdvancedSeparator2
        '
        Me.AdvancedSeparator2.Name = "AdvancedSeparator2"
        resources.ApplyResources(Me.AdvancedSeparator2, "AdvancedSeparator2")
        '
        'AdvancedOffMenuItem
        '
        Me.AdvancedOffMenuItem.Name = "AdvancedOffMenuItem"
        resources.ApplyResources(Me.AdvancedOffMenuItem, "AdvancedOffMenuItem")
        '
        'SettingMenuItem
        '
        Me.SettingMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SpecialTimeMenuItem, Me.TransparentMenuItem, Me.PositionSaveMenuItem, Me.LockStartMenuItem, Me.AutorunMenuItem, Me.CanSizeChangeMenuItem, Me.SizeSaveMenuItem, Me.ChangeBatteryMenuItem, Me.ShowNextTermMenuItem, Me.NTPTimeMenuItem, Me.DeviationMenuItem, Me.SettingSeparator1, Me.AdvancedOnMenuItem})
        Me.SettingMenuItem.Name = "SettingMenuItem"
        resources.ApplyResources(Me.SettingMenuItem, "SettingMenuItem")
        '
        'SpecialTimeMenuItem
        '
        Me.SpecialTimeMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Term5MenuItem, Me.Term6MenuItem})
        Me.SpecialTimeMenuItem.Name = "SpecialTimeMenuItem"
        resources.ApplyResources(Me.SpecialTimeMenuItem, "SpecialTimeMenuItem")
        '
        'Term5MenuItem
        '
        Me.Term5MenuItem.CheckOnClick = True
        Me.Term5MenuItem.Name = "Term5MenuItem"
        resources.ApplyResources(Me.Term5MenuItem, "Term5MenuItem")
        '
        'Term6MenuItem
        '
        Me.Term6MenuItem.CheckOnClick = True
        resources.ApplyResources(Me.Term6MenuItem, "Term6MenuItem")
        Me.Term6MenuItem.Name = "Term6MenuItem"
        '
        'TransparentMenuItem
        '
        Me.TransparentMenuItem.AutoToolTip = True
        Me.TransparentMenuItem.Checked = True
        Me.TransparentMenuItem.CheckOnClick = True
        Me.TransparentMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.TransparentMenuItem.Name = "TransparentMenuItem"
        resources.ApplyResources(Me.TransparentMenuItem, "TransparentMenuItem")
        '
        'PositionSaveMenuItem
        '
        Me.PositionSaveMenuItem.CheckOnClick = True
        Me.PositionSaveMenuItem.Name = "PositionSaveMenuItem"
        resources.ApplyResources(Me.PositionSaveMenuItem, "PositionSaveMenuItem")
        '
        'LockStartMenuItem
        '
        Me.LockStartMenuItem.CheckOnClick = True
        Me.LockStartMenuItem.Name = "LockStartMenuItem"
        resources.ApplyResources(Me.LockStartMenuItem, "LockStartMenuItem")
        '
        'AutorunMenuItem
        '
        Me.AutorunMenuItem.CheckOnClick = True
        Me.AutorunMenuItem.Name = "AutorunMenuItem"
        resources.ApplyResources(Me.AutorunMenuItem, "AutorunMenuItem")
        '
        'CanSizeChangeMenuItem
        '
        Me.CanSizeChangeMenuItem.CheckOnClick = True
        Me.CanSizeChangeMenuItem.Name = "CanSizeChangeMenuItem"
        resources.ApplyResources(Me.CanSizeChangeMenuItem, "CanSizeChangeMenuItem")
        '
        'SizeSaveMenuItem
        '
        Me.SizeSaveMenuItem.CheckOnClick = True
        Me.SizeSaveMenuItem.Name = "SizeSaveMenuItem"
        resources.ApplyResources(Me.SizeSaveMenuItem, "SizeSaveMenuItem")
        '
        'ChangeBatteryMenuItem
        '
        Me.ChangeBatteryMenuItem.CheckOnClick = True
        Me.ChangeBatteryMenuItem.Name = "ChangeBatteryMenuItem"
        resources.ApplyResources(Me.ChangeBatteryMenuItem, "ChangeBatteryMenuItem")
        '
        'ShowNextTermMenuItem
        '
        Me.ShowNextTermMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.IDPassMenuItem, Me.IDPassSeparator, Me.ShowNotifyMenuItem, Me.ShowPopUpMenuItem})
        Me.ShowNextTermMenuItem.Name = "ShowNextTermMenuItem"
        resources.ApplyResources(Me.ShowNextTermMenuItem, "ShowNextTermMenuItem")
        '
        'IDPassMenuItem
        '
        Me.IDPassMenuItem.Name = "IDPassMenuItem"
        resources.ApplyResources(Me.IDPassMenuItem, "IDPassMenuItem")
        '
        'IDPassSeparator
        '
        Me.IDPassSeparator.Name = "IDPassSeparator"
        resources.ApplyResources(Me.IDPassSeparator, "IDPassSeparator")
        '
        'ShowNotifyMenuItem
        '
        Me.ShowNotifyMenuItem.CheckOnClick = True
        Me.ShowNotifyMenuItem.Name = "ShowNotifyMenuItem"
        resources.ApplyResources(Me.ShowNotifyMenuItem, "ShowNotifyMenuItem")
        '
        'ShowPopUpMenuItem
        '
        Me.ShowPopUpMenuItem.CheckOnClick = True
        Me.ShowPopUpMenuItem.Name = "ShowPopUpMenuItem"
        resources.ApplyResources(Me.ShowPopUpMenuItem, "ShowPopUpMenuItem")
        '
        'NTPTimeMenuItem
        '
        Me.NTPTimeMenuItem.Name = "NTPTimeMenuItem"
        resources.ApplyResources(Me.NTPTimeMenuItem, "NTPTimeMenuItem")
        '
        'DeviationMenuItem
        '
        Me.DeviationMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeviationToolStripTextBox})
        Me.DeviationMenuItem.Name = "DeviationMenuItem"
        resources.ApplyResources(Me.DeviationMenuItem, "DeviationMenuItem")
        '
        'DeviationToolStripTextBox
        '
        Me.DeviationToolStripTextBox.Name = "DeviationToolStripTextBox"
        resources.ApplyResources(Me.DeviationToolStripTextBox, "DeviationToolStripTextBox")
        '
        'SettingSeparator1
        '
        Me.SettingSeparator1.Name = "SettingSeparator1"
        resources.ApplyResources(Me.SettingSeparator1, "SettingSeparator1")
        '
        'AdvancedOnMenuItem
        '
        Me.AdvancedOnMenuItem.Name = "AdvancedOnMenuItem"
        resources.ApplyResources(Me.AdvancedOnMenuItem, "AdvancedOnMenuItem")
        '
        'HelpMenuItem
        '
        Me.HelpMenuItem.Name = "HelpMenuItem"
        resources.ApplyResources(Me.HelpMenuItem, "HelpMenuItem")
        '
        'AboutMenuItem
        '
        Me.AboutMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ShowAboutMenuItem})
        Me.AboutMenuItem.Name = "AboutMenuItem"
        resources.ApplyResources(Me.AboutMenuItem, "AboutMenuItem")
        '
        'ShowAboutMenuItem
        '
        Me.ShowAboutMenuItem.Name = "ShowAboutMenuItem"
        resources.ApplyResources(Me.ShowAboutMenuItem, "ShowAboutMenuItem")
        '
        'WebTimer
        '
        Me.WebTimer.Interval = 1000
        '
        'SelectFontDialog
        '
        Me.SelectFontDialog.ShowEffects = False
        '
        'CountupTimer
        '
        '
        'KitchenTimer
        '
        '
        'NotifyIcon
        '
        Me.NotifyIcon.ContextMenuStrip = Me.NotifyRightMenuStrip
        resources.ApplyResources(Me.NotifyIcon, "NotifyIcon")
        '
        'NotifyRightMenuStrip
        '
        Me.NotifyRightMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ModeMenuItemN, Me.ChangeSecMenuItem, Me.ChangeLockMenuItem, Me.MovePadMenuItem, Me.NextTimeMenuItemN, Me.ShowMenuItem, Me.MinimizeMenuItem, Me.RightSeparator, Me.ExitMenuItemN})
        Me.NotifyRightMenuStrip.Name = "ContextMenuStrip1"
        resources.ApplyResources(Me.NotifyRightMenuStrip, "NotifyRightMenuStrip")
        '
        'ModeMenuItemN
        '
        Me.ModeMenuItemN.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ClassMenuItemN, Me.NowTimeMenuStripN, Me.CountupMenuItemN, Me.KitchenMenuItemN, Me.CountdownMenuItemN, Me.PomodoroMenuItemN, Me.BatteryMenuItemN})
        Me.ModeMenuItemN.Name = "ModeMenuItemN"
        resources.ApplyResources(Me.ModeMenuItemN, "ModeMenuItemN")
        '
        'ClassMenuItemN
        '
        Me.ClassMenuItemN.Checked = True
        Me.ClassMenuItemN.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ClassMenuItemN.Name = "ClassMenuItemN"
        resources.ApplyResources(Me.ClassMenuItemN, "ClassMenuItemN")
        '
        'NowTimeMenuStripN
        '
        Me.NowTimeMenuStripN.Name = "NowTimeMenuStripN"
        resources.ApplyResources(Me.NowTimeMenuStripN, "NowTimeMenuStripN")
        '
        'CountupMenuItemN
        '
        Me.CountupMenuItemN.Name = "CountupMenuItemN"
        resources.ApplyResources(Me.CountupMenuItemN, "CountupMenuItemN")
        '
        'KitchenMenuItemN
        '
        Me.KitchenMenuItemN.Name = "KitchenMenuItemN"
        resources.ApplyResources(Me.KitchenMenuItemN, "KitchenMenuItemN")
        '
        'CountdownMenuItemN
        '
        Me.CountdownMenuItemN.Name = "CountdownMenuItemN"
        resources.ApplyResources(Me.CountdownMenuItemN, "CountdownMenuItemN")
        '
        'PomodoroMenuItemN
        '
        Me.PomodoroMenuItemN.Name = "PomodoroMenuItemN"
        resources.ApplyResources(Me.PomodoroMenuItemN, "PomodoroMenuItemN")
        '
        'BatteryMenuItemN
        '
        Me.BatteryMenuItemN.Name = "BatteryMenuItemN"
        resources.ApplyResources(Me.BatteryMenuItemN, "BatteryMenuItemN")
        '
        'ChangeSecMenuItem
        '
        Me.ChangeSecMenuItem.Name = "ChangeSecMenuItem"
        resources.ApplyResources(Me.ChangeSecMenuItem, "ChangeSecMenuItem")
        '
        'ChangeLockMenuItem
        '
        Me.ChangeLockMenuItem.Name = "ChangeLockMenuItem"
        resources.ApplyResources(Me.ChangeLockMenuItem, "ChangeLockMenuItem")
        '
        'MovePadMenuItem
        '
        Me.MovePadMenuItem.Name = "MovePadMenuItem"
        resources.ApplyResources(Me.MovePadMenuItem, "MovePadMenuItem")
        '
        'NextTimeMenuItemN
        '
        Me.NextTimeMenuItemN.ForeColor = System.Drawing.Color.Gray
        Me.NextTimeMenuItemN.Name = "NextTimeMenuItemN"
        resources.ApplyResources(Me.NextTimeMenuItemN, "NextTimeMenuItemN")
        '
        'ShowMenuItem
        '
        Me.ShowMenuItem.Name = "ShowMenuItem"
        resources.ApplyResources(Me.ShowMenuItem, "ShowMenuItem")
        '
        'MinimizeMenuItem
        '
        Me.MinimizeMenuItem.Name = "MinimizeMenuItem"
        resources.ApplyResources(Me.MinimizeMenuItem, "MinimizeMenuItem")
        '
        'RightSeparator
        '
        Me.RightSeparator.Name = "RightSeparator"
        resources.ApplyResources(Me.RightSeparator, "RightSeparator")
        '
        'ExitMenuItemN
        '
        Me.ExitMenuItemN.Name = "ExitMenuItemN"
        resources.ApplyResources(Me.ExitMenuItemN, "ExitMenuItemN")
        '
        'CountdownTimer
        '
        '
        'BatteryTimer
        '
        '
        'SaveFileDialog
        '
        resources.ApplyResources(Me.SaveFileDialog, "SaveFileDialog")
        '
        'OpenFileDialog
        '
        Me.OpenFileDialog.FileName = "OpenFileDialog1"
        resources.ApplyResources(Me.OpenFileDialog, "OpenFileDialog")
        '
        'BatteryMonitorTimer
        '
        Me.BatteryMonitorTimer.Interval = 250
        '
        'TimeTimer
        '
        Me.TimeTimer.Interval = 25
        '
        'Main
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TimerMenuStrip)
        Me.Controls.Add(Me.TimerBar)
        Me.Controls.Add(Me.TimerLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.TimerMenuStrip
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Main"
        Me.TimerMenuStrip.ResumeLayout(False)
        Me.TimerMenuStrip.PerformLayout()
        Me.NotifyRightMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TimerLabel As Label
    Friend WithEvents TimerBar As ProgressBar
    Friend WithEvents ClassTimer As System.Windows.Forms.Timer
    Friend WithEvents TimerMenuStrip As MenuStrip
    Friend WithEvents FuncMenuItem As ToolStripMenuItem
    Friend WithEvents ShowsecMenuItem As ToolStripMenuItem
    Friend WithEvents ShowBarMenuItem As ToolStripMenuItem
    Friend WithEvents SettingMenuItem As ToolStripMenuItem
    Friend WithEvents TransparentMenuItem As ToolStripMenuItem
    Friend WithEvents TopmostMenuItem As ToolStripMenuItem
    Friend WithEvents ShowNextTermMenuItem As ToolStripMenuItem
    Friend WithEvents HelpMenuItem As ToolStripMenuItem
    Friend WithEvents AboutMenuItem As ToolStripMenuItem
    Friend WithEvents SizeMenuItem As ToolStripMenuItem
    Friend WithEvents Size2MenuItem As ToolStripMenuItem
    Friend WithEvents Size4MenuItem As ToolStripMenuItem
    Friend WithEvents CanSizeChangeMenuItem As ToolStripMenuItem
    Friend WithEvents Size3MenuItem As ToolStripMenuItem
    Friend WithEvents Size1MenuItem As ToolStripMenuItem
    Friend WithEvents PositionSaveMenuItem As ToolStripMenuItem
    Friend WithEvents LockStartMenuItem As ToolStripMenuItem
    Friend WithEvents AutorunMenuItem As ToolStripMenuItem
    Friend WithEvents ShowAboutMenuItem As ToolStripMenuItem
    Friend WithEvents IDPassMenuItem As ToolStripMenuItem
    Friend WithEvents WebTimer As System.Windows.Forms.Timer
    Friend WithEvents NextTimeMenuItem As ToolStripMenuItem
    Friend WithEvents FuncSeparator1 As ToolStripSeparator
    Friend WithEvents ExitMenuItem As ToolStripMenuItem
    Friend WithEvents SizeSaveMenuItem As ToolStripMenuItem
    Friend WithEvents SettingSeparator1 As ToolStripSeparator
    Friend WithEvents AdvancedOnMenuItem As ToolStripMenuItem
    Friend WithEvents AdvancedMenuItem As ToolStripMenuItem
    Friend WithEvents ChangeFontMenuItem As ToolStripMenuItem
    Friend WithEvents TimerHeightMenuItem As ToolStripMenuItem
    Friend WithEvents TimerHeightToolStripTextBox As ToolStripTextBox
    Friend WithEvents AdvancedOffMenuItem As ToolStripMenuItem
    Friend WithEvents BarHeightMenuItem As ToolStripMenuItem
    Friend WithEvents BarHeightToolStripTextBox As ToolStripTextBox
    Friend WithEvents AdvancedSeparator2 As ToolStripSeparator
    Friend WithEvents TitleShowTimerMenuItem As ToolStripMenuItem
    Friend WithEvents SelectFontDialog As FontDialog
    Friend WithEvents SpaceWidthToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LeftSpaceMenuItem As ToolStripMenuItem
    Friend WithEvents RightSpaceMenuItem As ToolStripMenuItem
    Friend WithEvents SpaceWidthToolStripTextBox As ToolStripTextBox
    Friend WithEvents LeftSpaceToolStripTextBox As ToolStripTextBox
    Friend WithEvents RightSpaceToolStripTextBox As ToolStripTextBox
    Friend WithEvents MarginMenuItem As ToolStripMenuItem
    Friend WithEvents MarginToolStripTextBox As ToolStripTextBox
    Friend WithEvents ModeMenuItem As ToolStripMenuItem
    Friend WithEvents ClassMenuItem As ToolStripMenuItem
    Friend WithEvents CountupMenuItem As ToolStripMenuItem
    Friend WithEvents CountdownMenuItem As ToolStripMenuItem
    Friend WithEvents KitchenMenuItem As ToolStripMenuItem
    Friend WithEvents SpecialTimeMenuItem As ToolStripMenuItem
    Friend WithEvents CountupTimer As System.Windows.Forms.Timer
    Friend WithEvents PomodoroMenuItem As ToolStripMenuItem
    Friend WithEvents KitchenTimer As System.Windows.Forms.Timer
    Friend WithEvents NotifyIcon As NotifyIcon
    Friend WithEvents NTPTimeMenuItem As ToolStripMenuItem
    Friend WithEvents IDPassSeparator As ToolStripSeparator
    Friend WithEvents ShowNotifyMenuItem As ToolStripMenuItem
    Friend WithEvents ShowPopUpMenuItem As ToolStripMenuItem
    Friend WithEvents CountdownTimer As System.Windows.Forms.Timer
    Friend WithEvents NotifyRightMenuStrip As ContextMenuStrip
    Friend WithEvents ShowMenuItem As ToolStripMenuItem
    Friend WithEvents MinimizeMenuItem As ToolStripMenuItem
    Friend WithEvents RightSeparator As ToolStripSeparator
    Friend WithEvents ExitMenuItemN As ToolStripMenuItem
    Friend WithEvents ChangeLockMenuItem As ToolStripMenuItem
    Friend WithEvents ChangeSecMenuItem As ToolStripMenuItem
    Friend WithEvents NextTimeMenuItemN As ToolStripMenuItem
    Friend WithEvents BatteryMenuItem As ToolStripMenuItem
    Friend WithEvents BatteryTimer As System.Windows.Forms.Timer
    Friend WithEvents ModeMenuItemN As ToolStripMenuItem
    Friend WithEvents ClassMenuItemN As ToolStripMenuItem
    Friend WithEvents CountupMenuItemN As ToolStripMenuItem
    Friend WithEvents KitchenMenuItemN As ToolStripMenuItem
    Friend WithEvents CountdownMenuItemN As ToolStripMenuItem
    Friend WithEvents PomodoroMenuItemN As ToolStripMenuItem
    Friend WithEvents BatteryMenuItemN As ToolStripMenuItem
    Friend WithEvents AdvancedSeparator1 As ToolStripSeparator
    Friend WithEvents ExportMenuItem As ToolStripMenuItem
    Friend WithEvents ImportMenuItem As ToolStripMenuItem
    Friend WithEvents SaveFileDialog As SaveFileDialog
    Friend WithEvents OpenFileDialog As OpenFileDialog
    Friend WithEvents DeviationMenuItem As ToolStripMenuItem
    Friend WithEvents DeviationToolStripTextBox As ToolStripTextBox
    Friend WithEvents Term5MenuItem As ToolStripMenuItem
    Friend WithEvents Term6MenuItem As ToolStripMenuItem
    Friend WithEvents ChangeBatteryMenuItem As ToolStripMenuItem
    Friend WithEvents BatteryMonitorTimer As System.Windows.Forms.Timer
    Friend WithEvents MovePadMenuItem As ToolStripMenuItem
    Friend WithEvents NowTimeMenuItem As ToolStripMenuItem
    Friend WithEvents TimeTimer As System.Windows.Forms.Timer
    Friend WithEvents NowTimeMenuStripN As ToolStripMenuItem
    Friend WithEvents PerfectTransparentMenuItem As ToolStripMenuItem
End Class
