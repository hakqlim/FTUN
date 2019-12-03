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
        Me.SuspendLayout()
        '
        'btnReadFT
        '
        Me.btnReadFT.Location = New System.Drawing.Point(12, 27)
        Me.btnReadFT.Name = "btnReadFT"
        Me.btnReadFT.Size = New System.Drawing.Size(166, 38)
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
        Me.Button1.Location = New System.Drawing.Point(259, 27)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(166, 38)
        Me.Button1.TabIndex = 1
        Me.Button1.Text = "Print FT Data"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnOrder
        '
        Me.btnOrder.Location = New System.Drawing.Point(259, 135)
        Me.btnOrder.Name = "btnOrder"
        Me.btnOrder.Size = New System.Drawing.Size(166, 38)
        Me.btnOrder.TabIndex = 2
        Me.btnOrder.Text = "Find Bottom-Up Order"
        Me.btnOrder.UseVisualStyleBackColor = True
        '
        'txtTopEvent
        '
        Me.txtTopEvent.Location = New System.Drawing.Point(94, 101)
        Me.txtTopEvent.Name = "txtTopEvent"
        Me.txtTopEvent.Size = New System.Drawing.Size(161, 21)
        Me.txtTopEvent.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 12)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Top Event :"
        '
        'btnBDD
        '
        Me.btnBDD.Location = New System.Drawing.Point(259, 190)
        Me.btnBDD.Name = "btnBDD"
        Me.btnBDD.Size = New System.Drawing.Size(166, 38)
        Me.btnBDD.TabIndex = 5
        Me.btnBDD.Text = "Solve BDD"
        Me.btnBDD.UseVisualStyleBackColor = True
        '
        'btnMonte
        '
        Me.btnMonte.Location = New System.Drawing.Point(11, 179)
        Me.btnMonte.Name = "btnMonte"
        Me.btnMonte.Size = New System.Drawing.Size(166, 38)
        Me.btnMonte.TabIndex = 6
        Me.btnMonte.Text = "Monte Carlo"
        Me.btnMonte.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 155)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 12)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "# of Runs:"
        '
        'txtSamples
        '
        Me.txtSamples.Location = New System.Drawing.Point(87, 152)
        Me.txtSamples.Name = "txtSamples"
        Me.txtSamples.Size = New System.Drawing.Size(90, 21)
        Me.txtSamples.TabIndex = 7
        Me.txtSamples.Text = "10000"
        '
        'btnSaveBDD
        '
        Me.btnSaveBDD.Location = New System.Drawing.Point(259, 296)
        Me.btnSaveBDD.Name = "btnSaveBDD"
        Me.btnSaveBDD.Size = New System.Drawing.Size(166, 38)
        Me.btnSaveBDD.TabIndex = 9
        Me.btnSaveBDD.Text = "Save BDD on RAW File"
        Me.btnSaveBDD.UseVisualStyleBackColor = True
        '
        'btnMCS
        '
        Me.btnMCS.Location = New System.Drawing.Point(259, 353)
        Me.btnMCS.Name = "btnMCS"
        Me.btnMCS.Size = New System.Drawing.Size(166, 38)
        Me.btnMCS.TabIndex = 10
        Me.btnMCS.Text = "Calcualte MCS and Save on RAW File"
        Me.btnMCS.UseVisualStyleBackColor = True
        '
        'btnPrintITE
        '
        Me.btnPrintITE.Location = New System.Drawing.Point(259, 243)
        Me.btnPrintITE.Name = "btnPrintITE"
        Me.btnPrintITE.Size = New System.Drawing.Size(166, 38)
        Me.btnPrintITE.TabIndex = 11
        Me.btnPrintITE.Text = "Print ITE"
        Me.btnPrintITE.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(472, 454)
        Me.Controls.Add(Me.btnPrintITE)
        Me.Controls.Add(Me.btnMCS)
        Me.Controls.Add(Me.btnSaveBDD)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtSamples)
        Me.Controls.Add(Me.btnMonte)
        Me.Controls.Add(Me.btnBDD)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtTopEvent)
        Me.Controls.Add(Me.btnOrder)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnReadFT)
        Me.Name = "Form1"
        Me.Text = "Fault Tree Analysis"
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

End Class
