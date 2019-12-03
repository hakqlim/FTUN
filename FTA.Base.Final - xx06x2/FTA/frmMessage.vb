Public Class frmMessage

    Private Sub frmMessage_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Message.Initialize()
    End Sub

End Class