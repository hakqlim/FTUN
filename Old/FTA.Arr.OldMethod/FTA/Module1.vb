Module Module1

    Public Sub ErrMsg(S As String)
        MessageBox.Show(S)
        End
    End Sub

    Public Sub Message(S As String)
        frmMessage.txtMessgae.Text = S
        frmMessage.Show()
    End Sub

End Module
