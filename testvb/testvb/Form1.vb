Option Explicit On

Public Class Form1

    Private Declare Sub AddLongs_Pointer Lib "C:\Users\임학규\source\repos\test\Debug\test.dll" (ByRef FirstElement As Single, ByVal lElements As Integer)
    Private Declare Function AddLongs_SafeArray Lib "C:\Users\임학규\source\repos\test\Debug\test.dll" (ByRef FirstElement() As Single, ByRef lSum As Integer) As Single

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ArrayOfLongs() As Single
        Dim lSum As Single = 0.0F
        Dim k As Integer = 0

        ReDim ArrayOfLongs(100)
        Call AddLongs_Pointer(ArrayOfLongs(0), UBound(ArrayOfLongs) + 1)
        For k = 0 To UBound(ArrayOfLongs)
            lSum = ArrayOfLongs(k) + lSum
        Next
        MsgBox("Result with C array = " & Str$(lSum))

        k = AddLongs_SafeArray(ArrayOfLongs, lSum)
        'k = AddLongs_SafeArray(ArrayOfLongs(), lSum)
        If k = 0 Then
            MsgBox("Result with Safearray = " & Str$(lSum))
        Else
            MsgBox("Call with Safearray failed")
        End If
    End Sub
    '[출처] DLL에 VB배열 넘겨주기|작성자 aragagi
End Class
