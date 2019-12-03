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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnData = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnFrag = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.btnHazard = New System.Windows.Forms.Button()
        Me.btnStop = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
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
        Me.txtSamples.Location = New System.Drawing.Point(99, 20)
        Me.txtSamples.Name = "txtSamples"
        Me.txtSamples.Size = New System.Drawing.Size(90, 21)
        Me.txtSamples.TabIndex = 7
        Me.txtSamples.Text = "30"
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
        Me.GroupBox2.Text = "Monte Carlo"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnData)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 100)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(220, 71)
        Me.GroupBox3.TabIndex = 14
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Read Failure Data"
        '
        'btnData
        '
        Me.btnData.BackColor = System.Drawing.SystemColors.Highlight
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
        Me.GroupBox4.Controls.Add(Me.btnFrag)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 188)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(218, 85)
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
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.btnHazard)
        Me.GroupBox5.Location = New System.Drawing.Point(12, 296)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(218, 78)
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
        Me.btnHazard.Text = "from MDB File"
        Me.btnHazard.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.btnStop.Location = New System.Drawing.Point(27, 494)
        Me.btnStop.Name = "Button1"
        Me.btnStop.Size = New System.Drawing.Size(166, 23)
        Me.btnStop.TabIndex = 17
        Me.btnStop.Text = "Finish"
        Me.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.btnStop.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(245, 533)
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
End Class
