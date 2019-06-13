<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Movepad
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.UpButton = New System.Windows.Forms.Button()
        Me.LeftButton = New System.Windows.Forms.Button()
        Me.DownButton = New System.Windows.Forms.Button()
        Me.RightButton = New System.Windows.Forms.Button()
        Me.UnitButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'UpButton
        '
        Me.UpButton.Font = New System.Drawing.Font("UD デジタル 教科書体 NK-B", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.UpButton.Location = New System.Drawing.Point(41, 3)
        Me.UpButton.Name = "UpButton"
        Me.UpButton.Size = New System.Drawing.Size(48, 32)
        Me.UpButton.TabIndex = 0
        Me.UpButton.Text = "↑"
        Me.UpButton.UseVisualStyleBackColor = True
        '
        'LeftButton
        '
        Me.LeftButton.Font = New System.Drawing.Font("UD デジタル 教科書体 NK-B", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.LeftButton.Location = New System.Drawing.Point(3, 41)
        Me.LeftButton.Name = "LeftButton"
        Me.LeftButton.Size = New System.Drawing.Size(32, 48)
        Me.LeftButton.TabIndex = 1
        Me.LeftButton.Text = "←"
        Me.LeftButton.UseVisualStyleBackColor = True
        '
        'DownButton
        '
        Me.DownButton.Font = New System.Drawing.Font("UD デジタル 教科書体 NK-B", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.DownButton.Location = New System.Drawing.Point(41, 95)
        Me.DownButton.Name = "DownButton"
        Me.DownButton.Size = New System.Drawing.Size(48, 32)
        Me.DownButton.TabIndex = 2
        Me.DownButton.Text = "↓"
        Me.DownButton.UseVisualStyleBackColor = True
        '
        'RightButton
        '
        Me.RightButton.Font = New System.Drawing.Font("UD デジタル 教科書体 NK-B", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.RightButton.Location = New System.Drawing.Point(95, 41)
        Me.RightButton.Name = "RightButton"
        Me.RightButton.Size = New System.Drawing.Size(32, 48)
        Me.RightButton.TabIndex = 3
        Me.RightButton.Text = "→"
        Me.RightButton.UseVisualStyleBackColor = True
        '
        'UnitButton
        '
        Me.UnitButton.Font = New System.Drawing.Font("UD デジタル 教科書体 NK-B", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.UnitButton.Location = New System.Drawing.Point(41, 41)
        Me.UnitButton.Name = "UnitButton"
        Me.UnitButton.Size = New System.Drawing.Size(48, 48)
        Me.UnitButton.TabIndex = 4
        Me.UnitButton.Text = "1"
        Me.UnitButton.UseVisualStyleBackColor = True
        '
        'Movepad
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(129, 130)
        Me.Controls.Add(Me.UnitButton)
        Me.Controls.Add(Me.RightButton)
        Me.Controls.Add(Me.DownButton)
        Me.Controls.Add(Me.LeftButton)
        Me.Controls.Add(Me.UpButton)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Movepad"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Movepad"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents UpButton As Button
    Friend WithEvents LeftButton As Button
    Friend WithEvents DownButton As Button
    Friend WithEvents RightButton As Button
    Friend WithEvents UnitButton As Button
End Class
