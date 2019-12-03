Public Class ClassTraverse

    Dim FT As ClassFtData

    Public Sub New(FtData As ClassFtData)
        FT = FtData     ' Class 생성시 FT 연결
    End Sub

    Public Sub TraverseFT1(Top As String)
        Dim ixTop As Integer = FT.GetIndexOfEvent(Top.ToUpper)

        Message.Add("Traverse FT 1 -----")
        Call TraverseGate1(ixTop)

        Message.Show()
    End Sub

    Private Sub TraverseGate1(P As Integer)
        Dim x As Integer
        If (FT.XEvents(P).Child.Count = 0) Then ' Basic Event 
            Message.Add(FT.XEvents(P).Name)
            Return
        Else        ' Gate
            Message.Add(FT.XEvents(P).Name)
            For i = 0 To FT.XEvents(P).Child.Count - 1
                x = FT.XEvents(P).Child(i)
                Call TraverseGate1(x)   ' Child에 대해 반복
            Next
        End If
    End Sub

    Public Sub TraverseFT2(Top As String)
        Dim ixTop As Integer = FT.GetIndexOfEvent(Top.ToUpper)

        Dim Occ(FT.XEvents.Count) As Integer    ' 다시 방문하지 않도록 점검하는 용도
        For i = 0 To FT.XEvents.Count - 1
            Occ(i) = 0
        Next

        Message.Add("Traverse FT 2 -----")
        Call TraverseGate2(ixTop, Occ)

        Message.Show()
    End Sub

    Private Sub TraverseGate2(P As Integer, Occ() As Integer)
        Dim x As Integer
        If (FT.XEvents(P).Child.Count = 0) Then     ' BE
            Message.Add(FT.XEvents(P).Name)
            Return
        Else        ' Gate
            Message.Add(FT.XEvents(P).Name)
            If (Occ(P) > 0) Then Return

            Occ(P) += 1
            For i = 0 To FT.XEvents(P).Child.Count - 1
                x = FT.XEvents(P).Child(i)
                Call TraverseGate2(x, Occ)
            Next
        End If
    End Sub


    Public Sub Calcualte_Top(Top As String)
        Dim ixTop As Integer = FT.GetIndexOfEvent(Top.ToUpper)

        Dim Occ(FT.XEvents.Count) As Integer    ' 다시 방문하지 않도록 점검하는 용도
        For i = 0 To FT.XEvents.Count - 1
            Occ(i) = 0
        Next

        Message.Add("Calculate Top -----")
        Dim V As Single = Calculate_Gate(ixTop, Occ)
        Message.Add("P=" & V)
        Message.Show()
    End Sub

    Private Function Calculate_Gate(P As Integer, Occ() As Integer) As Single
        Dim x As Integer
        If (FT.XEvents(P).Child.Count = 0) Then     ' BE
            Return FT.XEvents(P).Proba
        Else        ' Gate
            If (Occ(P) > 0) Then Return FT.XEvents(P).Proba
            Occ(P) += 1

            Dim V(FT.XEvents(P).Child.Count), V1 As Single
            For i = 0 To FT.XEvents(P).Child.Count - 1
                x = FT.XEvents(P).Child(i)
                V(i) = Calculate_Gate(x, Occ)   ' Child 값 임시 저장
            Next

            ' Gate 값 계산
            If (FT.XEvents(P).Type = "+") Then
                V1 = 0.0
                For i = 0 To FT.XEvents(P).Child.Count - 1
                    V1 = V1 + V(i) - V1 * V(i)
                Next
            ElseIf (FT.XEvents(P).Type = "*") Then
                V1 = 1.0
                For i = 0 To FT.XEvents(P).Child.Count - 1
                    V1 = V1 * V(i)
                Next
            End If
            FT.XEvents(P).Proba = V1    ' Gate 값 저장
            Message.Add(FT.XEvents(P).Name & " = " & Format(FT.XEvents(P).Proba, "0.0000e-00"))

            Return FT.XEvents(P).Proba
        End If
    End Function


End Class
