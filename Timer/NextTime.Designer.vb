<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NextTimeForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NextTimeForm))
        Me.RoomLabel = New System.Windows.Forms.Label()
        Me.ShowNextTimeLabel = New System.Windows.Forms.Label()
        Me.NextTimeisLabel = New System.Windows.Forms.Label()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.SuspendLayout()
        '
        'RoomLabel
        '
        Me.RoomLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.RoomLabel.Font = New System.Drawing.Font("UD デジタル 教科書体 NP-B", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RoomLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.RoomLabel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.RoomLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.RoomLabel.Location = New System.Drawing.Point(12, 92)
        Me.RoomLabel.Name = "RoomLabel"
        Me.RoomLabel.Size = New System.Drawing.Size(230, 29)
        Me.RoomLabel.TabIndex = 8
        Me.RoomLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ShowNextTimeLabel
        '
        Me.ShowNextTimeLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.ShowNextTimeLabel.Font = New System.Drawing.Font("UD デジタル 教科書体 NK-R", 15.75!)
        Me.ShowNextTimeLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.ShowNextTimeLabel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.ShowNextTimeLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.ShowNextTimeLabel.Location = New System.Drawing.Point(5, 31)
        Me.ShowNextTimeLabel.Name = "ShowNextTimeLabel"
        Me.ShowNextTimeLabel.Size = New System.Drawing.Size(234, 61)
        Me.ShowNextTimeLabel.TabIndex = 7
        Me.ShowNextTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'NextTimeisLabel
        '
        Me.NextTimeisLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.NextTimeisLabel.Font = New System.Drawing.Font("UD デジタル 教科書体 NK-B", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.NextTimeisLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.NextTimeisLabel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.NextTimeisLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.NextTimeisLabel.Location = New System.Drawing.Point(5, 8)
        Me.NextTimeisLabel.Name = "NextTimeisLabel"
        Me.NextTimeisLabel.Size = New System.Drawing.Size(91, 23)
        Me.NextTimeisLabel.TabIndex = 6
        Me.NextTimeisLabel.Text = "次の時間"
        Me.NextTimeisLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ImageList1
        '
        Me.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit
        Me.ImageList1.ImageSize = New System.Drawing.Size(16, 16)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        '
        'NextTimeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(239, 131)
        Me.Controls.Add(Me.RoomLabel)
        Me.Controls.Add(Me.ShowNextTimeLabel)
        Me.Controls.Add(Me.NextTimeisLabel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "NextTimeForm"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "次の時間"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RoomLabel As Label
    Friend WithEvents ShowNextTimeLabel As Label
    Friend WithEvents NextTimeisLabel As Label
    Friend WithEvents ImageList1 As ImageList
End Class
