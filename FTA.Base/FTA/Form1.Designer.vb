<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form은 Dispose를 재정의하여 구성 요소 목록을 정리합니다.
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

    'Windows Form 디자이너에 필요합니다.
    Private components As System.ComponentModel.IContainer

    '참고: 다음 프로시저는 Windows Form 디자이너에 필요합니다.
    '수정하려면 Windows Form 디자이너를 사용하십시오.  
    '코드 편집기를 사용하여 수정하지 마십시오.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnReadFT = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnOrder = New System.Windows.Forms.Button()
        Me.txtTopEvent = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnBDD = New System.Windows.Forms.Button()
        Me.btnMonte = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSamples = New System.Windows.Forms.TextBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.btnSaveBDD = New System.Windows.Forms.Button()
        Me.btnMCS = New System.Windows.Forms.Button()
        Me.btnPrintITE = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.btnTraverse2 = New System.Windows.Forms.Button()
        Me.btnCalcualteTop = New System.Windows.Forms.Button()
        Me.btnTraverse1 = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnReadFT
        '
        Me.btnReadFT.Location = New System.Drawing.Point(27, 25)
        Me.btnReadFT.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnReadFT.Name = "btnReadFT"
        Me.btnReadFT.Size = New System.Drawing.Size(190, 35)
        Me.btnReadFT.TabIndex = 0
        Me.btnReadFT.Text = "Read FT"
        Me.btnReadFT.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(298, 25)
        Me.Button1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(190, 35)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Print FT Data"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnOrder
        '
        Me.btnOrder.Location = New System.Drawing.Point(17, 32)
        Me.btnOrder.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnOrder.Name = "btnOrder"
        Me.btnOrder.Size = New System.Drawing.Size(190, 30)
        Me.btnOrder.TabIndex = 2
        Me.btnOrder.Text = "Find Bottom-Up Order"
        Me.btnOrder.UseVisualStyleBackColor = True
        '
        'txtTopEvent
        '
        Me.txtTopEvent.Location = New System.Drawing.Point(125, 105)
        Me.txtTopEvent.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtTopEvent.Name = "txtTopEvent"
        Me.txtTopEvent.Size = New System.Drawing.Size(183, 25)
        Me.txtTopEvent.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(39, 109)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 15)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Top Event :"
        '
        'btnBDD
        '
        Me.btnBDD.Location = New System.Drawing.Point(17, 79)
        Me.btnBDD.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnBDD.Name = "btnBDD"
        Me.btnBDD.Size = New System.Drawing.Size(190, 31)
        Me.btnBDD.TabIndex = 5
        Me.btnBDD.Text = "Solve BDD"
        Me.btnBDD.UseVisualStyleBackColor = True
        '
        'btnMonte
        '
        Me.btnMonte.Location = New System.Drawing.Point(27, 59)
        Me.btnMonte.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnMonte.Name = "btnMonte"
        Me.btnMonte.Size = New System.Drawing.Size(190, 34)
        Me.btnMonte.TabIndex = 6
        Me.btnMonte.Text = "Monte Carlo"
        Me.btnMonte.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(35, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 15)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "# of Runs:"
        '
        'txtSamples
        '
        Me.txtSamples.Location = New System.Drawing.Point(113, 25)
        Me.txtSamples.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.txtSamples.Name = "txtSamples"
        Me.txtSamples.Size = New System.Drawing.Size(102, 25)
        Me.txtSamples.TabIndex = 7
        Me.txtSamples.Text = "10000"
        '
        'btnSaveBDD
        '
        Me.btnSaveBDD.Location = New System.Drawing.Point(17, 179)
        Me.btnSaveBDD.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnSaveBDD.Name = "btnSaveBDD"
        Me.btnSaveBDD.Size = New System.Drawing.Size(190, 32)
        Me.btnSaveBDD.TabIndex = 9
        Me.btnSaveBDD.Text = "Save BDD on RAW File"
        Me.btnSaveBDD.UseVisualStyleBackColor = True
        '
        'btnMCS
        '
        Me.btnMCS.Location = New System.Drawing.Point(17, 230)
        Me.btnMCS.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnMCS.Name = "btnMCS"
        Me.btnMCS.Size = New System.Drawing.Size(190, 48)
        Me.btnMCS.TabIndex = 10
        Me.btnMCS.Text = "Calcualte MCS and Save on RAW File"
        Me.btnMCS.UseVisualStyleBackColor = True
        '
        'btnPrintITE
        '
        Me.btnPrintITE.Location = New System.Drawing.Point(17, 129)
        Me.btnPrintITE.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnPrintITE.Name = "btnPrintITE"
        Me.btnPrintITE.Size = New System.Drawing.Size(190, 32)
        Me.btnPrintITE.TabIndex = 11
        Me.btnPrintITE.Text = "Print ITE"
        Me.btnPrintITE.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnBDD)
        Me.GroupBox1.Controls.Add(Me.btnPrintITE)
        Me.GroupBox1.Controls.Add(Me.btnOrder)
        Me.GroupBox1.Controls.Add(Me.btnMCS)
        Me.GroupBox1.Controls.Add(Me.btnSaveBDD)
        Me.GroupBox1.Location = New System.Drawing.Point(293, 146)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox1.Size = New System.Drawing.Size(224, 290)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "BDD"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtSamples)
        Me.GroupBox2.Controls.Add(Me.btnMonte)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 376)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox2.Size = New System.Drawing.Size(249, 108)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Monte Carlo"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.btnTraverse2)
        Me.GroupBox3.Controls.Add(Me.btnCalcualteTop)
        Me.GroupBox3.Controls.Add(Me.btnTraverse1)
        Me.GroupBox3.Location = New System.Drawing.Point(11, 146)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.GroupBox3.Size = New System.Drawing.Size(249, 211)
        Me.GroupBox3.TabIndex = 14
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Traverse FT"
        '
        'btnTraverse2
        '
        Me.btnTraverse2.Location = New System.Drawing.Point(25, 75)
        Me.btnTraverse2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnTraverse2.Name = "btnTraverse2"
        Me.btnTraverse2.Size = New System.Drawing.Size(190, 38)
        Me.btnTraverse2.TabIndex = 9
        Me.btnTraverse2.Text = "Traverse FT 2"
        Me.btnTraverse2.UseVisualStyleBackColor = True
        '
        'btnCalcualteTop
        '
        Me.btnCalcualteTop.Location = New System.Drawing.Point(25, 129)
        Me.btnCalcualteTop.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnCalcualteTop.Name = "btnCalcualteTop"
        Me.btnCalcualteTop.Size = New System.Drawing.Size(190, 64)
        Me.btnCalcualteTop.TabIndex = 8
        Me.btnCalcualteTop.Text = "Calcualte Gate Value (No Duplicated Event Assumed)"
        Me.btnCalcualteTop.UseVisualStyleBackColor = True
        '
        'btnTraverse1
        '
        Me.btnTraverse1.Location = New System.Drawing.Point(25, 25)
        Me.btnTraverse1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnTraverse1.Name = "btnTraverse1"
        Me.btnTraverse1.Size = New System.Drawing.Size(190, 38)
        Me.btnTraverse1.TabIndex = 7
        Me.btnTraverse1.Text = "Traverse FT 1"
        Me.btnTraverse1.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnReadFT)
        Me.GroupBox4.Controls.Add(Me.Button1)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 16)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(505, 72)
        Me.GroupBox4.TabIndex = 15
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Read FT"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(527, 495)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.txtTopEvent)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Form1"
        Me.Text = "Fault Tree Analysis"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnReadFT As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnOrder As System.Windows.Forms.Button
    Friend WithEvents txtTopEvent As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnBDD As System.Windows.Forms.Button
    Friend WithEvents btnMonte As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSamples As System.Windows.Forms.TextBox
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents btnSaveBDD As System.Windows.Forms.Button
    Friend WithEvents btnMCS As System.Windows.Forms.Button
    Friend WithEvents btnPrintITE As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnTraverse1 As System.Windows.Forms.Button
    Friend WithEvents btnCalcualteTop As System.Windows.Forms.Button
    Friend WithEvents btnTraverse2 As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox

End Class
