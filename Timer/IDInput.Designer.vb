<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IdInput
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
        Me.InputIDTextBox = New System.Windows.Forms.TextBox()
        Me.IDLabel = New System.Windows.Forms.Label()
        Me.PassLabel = New System.Windows.Forms.Label()
        Me.InputPassTextBox = New System.Windows.Forms.TextBox()
        Me.OKButton = New System.Windows.Forms.Button()
        Me.ShowPassButton = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'InputIDTextBox
        '
        Me.InputIDTextBox.Font = New System.Drawing.Font("UD デジタル 教科書体 NP-B", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.InputIDTextBox.Location = New System.Drawing.Point(79, 16)
        Me.InputIDTextBox.Name = "InputIDTextBox"
        Me.InputIDTextBox.Size = New System.Drawing.Size(193, 23)
        Me.InputIDTextBox.TabIndex = 0
        '
        'IDLabel
        '
        Me.IDLabel.AutoSize = True
        Me.IDLabel.Font = New System.Drawing.Font("UD デジタル 教科書体 NK-B", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.IDLabel.Location = New System.Drawing.Point(13, 19)
        Me.IDLabel.Name = "IDLabel"
        Me.IDLabel.Size = New System.Drawing.Size(29, 16)
        Me.IDLabel.TabIndex = 1
        Me.IDLabel.Text = "ID"
        '
        'PassLabel
        '
        Me.PassLabel.AutoSize = True
        Me.PassLabel.Font = New System.Drawing.Font("UD デジタル 教科書体 NK-B", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PassLabel.Location = New System.Drawing.Point(13, 48)
        Me.PassLabel.Name = "PassLabel"
        Me.PassLabel.Size = New System.Drawing.Size(53, 16)
        Me.PassLabel.TabIndex = 3
        Me.PassLabel.Text = "PASS"
        '
        'InputPassTextBox
        '
        Me.InputPassTextBox.Font = New System.Drawing.Font("UD デジタル 教科書体 NP-B", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.InputPassTextBox.Location = New System.Drawing.Point(79, 45)
        Me.InputPassTextBox.Name = "InputPassTextBox"
        Me.InputPassTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.InputPassTextBox.Size = New System.Drawing.Size(163, 23)
        Me.InputPassTextBox.TabIndex = 2
        '
        'OKButton
        '
        Me.OKButton.Location = New System.Drawing.Point(172, 74)
        Me.OKButton.Name = "OKButton"
        Me.OKButton.Size = New System.Drawing.Size(100, 24)
        Me.OKButton.TabIndex = 4
        Me.OKButton.Text = "OK"
        Me.OKButton.UseVisualStyleBackColor = True
        '
        'ShowPassButton
        '
        Me.ShowPassButton.Font = New System.Drawing.Font("UD デジタル 教科書体 NK-B", 10.0!, System.Drawing.FontStyle.Bold)
        Me.ShowPassButton.Location = New System.Drawing.Point(248, 45)
        Me.ShowPassButton.Name = "ShowPassButton"
        Me.ShowPassButton.Size = New System.Drawing.Size(24, 23)
        Me.ShowPassButton.TabIndex = 5
        Me.ShowPassButton.Text = "*"
        Me.ShowPassButton.UseVisualStyleBackColor = True
        '
        'IDInput
        '
        Me.AcceptButton = Me.OKButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 105)
        Me.Controls.Add(Me.ShowPassButton)
        Me.Controls.Add(Me.OKButton)
        Me.Controls.Add(Me.PassLabel)
        Me.Controls.Add(Me.InputPassTextBox)
        Me.Controls.Add(Me.IDLabel)
        Me.Controls.Add(Me.InputIDTextBox)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(300, 144)
        Me.MinimizeBox = False
        Me.Name = "IDInput"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "学籍番号等を入力"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents InputIDTextBox As TextBox
    Friend WithEvents IDLabel As Label
    Friend WithEvents PassLabel As Label
    Friend WithEvents InputPassTextBox As TextBox
    Friend WithEvents OKButton As Button
    Friend WithEvents ShowPassButton As Button
End Class
