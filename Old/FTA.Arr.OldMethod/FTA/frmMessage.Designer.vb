<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMessage
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
        Me.txtMessgae = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'txtMessgae
        '
        Me.txtMessgae.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtMessgae.Location = New System.Drawing.Point(0, 0)
        Me.txtMessgae.Multiline = True
        Me.txtMessgae.Name = "txtMessgae"
        Me.txtMessgae.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMessgae.Size = New System.Drawing.Size(284, 262)
        Me.txtMessgae.TabIndex = 0
        '
        'frmMessage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.txtMessgae)
        Me.Name = "frmMessage"
        Me.Text = "Message"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtMessgae As System.Windows.Forms.TextBox
End Class
