Module Module1

    ' Program Error 가 발생하는 것을 찾아서 Message를 써내고, Program termination 하기
    Public Sub ErrMsg(S As String)
        MessageBox.Show(S)
        End
    End Sub

    ' Message를 frmMessage라는 Form에 나타내기
    ' 용법 
    '   Message.Initialize : 초기화 
    '   Message.Add (Msg) : Message 추가하기
    '   Message.Show : Message가 포함된 Form 나타내기
    '   Message.AddAndShow (Msg) : Message 추가하고 Form으로 나타내기

    Public Message As New ClassMessage

    Public Class ClassMessage

        Dim Msg As String = ""

        Public Sub Initialize()
            Msg = ""
        End Sub

        Public Sub Add(ByVal S As String)
            If Msg = "" Then
                Msg = S
            Else
                Msg += vbCrLf & S
            End If
        End Sub

        Public Sub Show()
            If (Msg = "") Then Exit Sub
            frmMessage.txtMsg.Text = Msg
            frmMessage.Show()
            frmMessage.BringToFront()
        End Sub

        Public Sub AddAndShow(ByVal S As String)
            Call Add(S)
            Call Show()
        End Sub
    End Class


End Module
