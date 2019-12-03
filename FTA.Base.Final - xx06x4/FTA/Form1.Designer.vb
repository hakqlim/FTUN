<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btnMonte = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSamples = New System.Windows.Forms.TextBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.btnMCS = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnData = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnFrag = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnHazard = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnMonte
        '
        Me.btnMonte.Location = New System.Drawing.Point(24, 47)
        Me.btnMonte.Name = "btnMonte"
        Me.btnMonte.Size = New System.Drawing.Size(166, 27)
        Me.btnMonte.TabIndex = 6
        Me.btnMonte.Text = "Monte Carlo"
        Me.btnMonte.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(31, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 12)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "# of Runs:"
        '
        'txtSamples
        '
        Me.txtSamples.Location = New System.Drawing.Point(99, 19)
        Me.txtSamples.Name = "txtSamples"
        Me.txtSamples.Size = New System.Drawing.Size(65, 21)
        Me.txtSamples.TabIndex = 7
        Me.txtSamples.Text = "30"
        '
        'SaveFileDialog1
        '
        '
        'btnMCS
        '
        Me.btnMCS.Location = New System.Drawing.Point(15, 19)
        Me.btnMCS.Name = "btnMCS"
        Me.btnMCS.Size = New System.Drawing.Size(174, 35)
        Me.btnMCS.TabIndex = 10
        Me.btnMCS.Text = "from RAW File"
        Me.btnMCS.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.GroupBox1.Controls.Add(Me.btnMCS)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 11)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(220, 72)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Read MCS"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(15, 20)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(149, 34)
        Me.Button4.TabIndex = 11
        Me.Button4.Text = "from FTP File"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtSamples)
        Me.GroupBox2.Controls.Add(Me.btnMonte)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 393)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(218, 86)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "CDF"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.GroupBox3.Controls.Add(Me.btnData)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 100)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(220, 71)
        Me.GroupBox3.TabIndex = 14
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Read Uncertainty Data"
        '
        'btnData
        '
        Me.btnData.BackColor = System.Drawing.SystemColors.ControlText
        Me.btnData.ForeColor = System.Drawing.SystemColors.Info
        Me.btnData.Location = New System.Drawing.Point(15, 19)
        Me.btnData.Name = "btnData"
        Me.btnData.Size = New System.Drawing.Size(174, 38)
        Me.btnData.TabIndex = 10
        Me.btnData.Text = "from MDB File"
        Me.btnData.UseVisualStyleBackColor = False
        '
        'GroupBox4
        '
        Me.GroupBox4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.GroupBox4.Controls.Add(Me.btnFrag)
        Me.GroupBox4.Controls.Add(Me.Button2)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 188)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(405, 85)
        Me.GroupBox4.TabIndex = 15
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Read Fragility Data"
        '
        'btnFrag
        '
        Me.btnFrag.BackColor = System.Drawing.SystemColors.ControlText
        Me.btnFrag.ForeColor = System.Drawing.SystemColors.Info
        Me.btnFrag.Location = New System.Drawing.Point(15, 35)
        Me.btnFrag.Name = "btnFrag"
        Me.btnFrag.Size = New System.Drawing.Size(174, 36)
        Me.btnFrag.TabIndex = 0
        Me.btnFrag.Text = "from MDB File"
        Me.btnFrag.UseVisualStyleBackColor = False
        '
        'Button2
        '
        Me.Button2.BackColor = System.Drawing.SystemColors.Info
        Me.Button2.Location = New System.Drawing.Point(232, 35)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(150, 36)
        Me.Button2.TabIndex = 19
        Me.Button2.Text = "from inp File"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.GroupBox5.Controls.Add(Me.btnHazard)
        Me.GroupBox5.Location = New System.Drawing.Point(12, 296)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(405, 78)
        Me.GroupBox5.TabIndex = 16
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Read Hazard Data"
        '
        'btnHazard
        '
        Me.btnHazard.BackColor = System.Drawing.SystemColors.Info
        Me.btnHazard.ForeColor = System.Drawing.SystemColors.InfoText
        Me.btnHazard.Location = New System.Drawing.Point(15, 21)
        Me.btnHazard.Name = "btnHazard"
        Me.btnHazard.Size = New System.Drawing.Size(174, 37)
        Me.btnHazard.TabIndex = 0
        Me.btnHazard.Text = "from inp File"
        Me.btnHazard.UseVisualStyleBackColor = False
        '
        'btnStop
        '
        Me.btnStop.BackColor = System.Drawing.SystemColors.HotTrack
        Me.btnStop.ForeColor = System.Drawing.SystemColors.Info
        Me.btnStop.Location = New System.Drawing.Point(12, 494)
        Me.btnStop.Name = "btnStop"
        Me.btnStop.Size = New System.Drawing.Size(397, 33)
        Me.btnStop.TabIndex = 17
        Me.btnStop.Text = "Finish"
        Me.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.btnStop.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.Desktop
        Me.Button1.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.Button1.Location = New System.Drawing.Point(244, 317)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(150, 37)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "from MDB File"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(15, 47)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(150, 27)
        Me.Button3.TabIndex = 20
        Me.Button3.Text = "Monte Carlo"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.TextBox2)
        Me.GroupBox6.Controls.Add(Me.Label1)
        Me.GroupBox6.Controls.Add(Me.Button3)
        Me.GroupBox6.Location = New System.Drawing.Point(229, 393)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(180, 85)
        Me.GroupBox6.TabIndex = 21
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "SEISMIC Only"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(91, 17)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(75, 21)
        Me.TextBox2.TabIndex = 23
        Me.TextBox2.Text = "1000"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(23, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 12)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "# of Runs:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(85, 14)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(80, 21)
        Me.TextBox1.TabIndex = 21
        Me.TextBox1.Text = "1000"
        '
        'GroupBox7
        '
        Me.GroupBox7.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.GroupBox7.Controls.Add(Me.Label3)
        Me.GroupBox7.Controls.Add(Me.TextBox3)
        Me.GroupBox7.Controls.Add(Me.Button5)
        Me.GroupBox7.Controls.Add(Me.Button4)
        Me.GroupBox7.Location = New System.Drawing.Point(238, 11)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(179, 160)
        Me.GroupBox7.TabIndex = 22
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Solve FT"
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(17, 108)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(147, 27)
        Me.Button5.TabIndex = 12
        Me.Button5.Text = "BDD"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(17, 81)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(147, 21)
        Me.TextBox3.TabIndex = 13
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 66)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 12)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Top Event"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(431, 533)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.GroupBox6)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnStop)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "Form1"
        Me.Text = "Uncertainty Analysis"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnMonte As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSamples As System.Windows.Forms.TextBox
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnMCS As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents btnData As Button
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents btnFrag As Button
    Friend WithEvents GroupBox5 As GroupBox
    Friend WithEvents btnHazard As Button
    Friend WithEvents btnStop As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents GroupBox6 As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button4 As Button
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents GroupBox7 As GroupBox
    Friend WithEvents Button5 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox3 As TextBox
End Class
