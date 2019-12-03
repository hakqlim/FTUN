Public Class ClassTraverse

    Dim FT As ClassFtData

    Public Sub New(FtData As ClassFtData)
        FT = FtData     ' Class 생성시 외부에서 읽은 FT 연결
    End Sub

    Public Sub TraverseFT1(Top As String)

        Message.Add("Traverse FT -----")

        ' Program : Top에 대한 Event Index 가져오기 
        ' Program : Top Index로부터 시작하여 FT 전체를 Traverse하기 
        '   Traverse하는 Gate 및 Event 출력하기 : Message.Add (Gate/Event 이름) 이용
        Dim ix As Integer = FT.GetIndexOfEvent(Top)
        Call TraverseGate1(ix)

        Message.Show()  ' Message 출력하기
    End Sub

    Sub TraverseGate1(P As Integer)
        Dim x As Integer
        If (FT.XEvents(P).Child.Count = 0) Then ' Basic Event 
            Message.Add(FT.XEvents(P).Name)
            Return
        Else        ' Gate
            Message.Add(FT.XEvents(P).Name)
            'For i = 0 To FT.XEvents(P).Child.Count - 1
            '    x = FT.XEvents(P).Child(i)
            '    Call TraverseGate1(x)   ' Child에 대해 반복
            'Next

            For Each x In FT.XEvents(P).Child
                Call TraverseGate1(x)   ' Child에 대해 반복
            Next

        End If
    End Sub


    Public Sub TraverseFT2(Top As String)

        ' Program : Top에 대한 Event Index 가져오기 

        Message.Add("Traverse FT -----")

        ' Program : Top Index로부터 시작하여 FT 전체를 Traverse하기 
        '   한번 방문한 Gate는 다시 방문하지 않기 (이를 위해서는 Array 선언하여 이용하는 기법 필요) 
        '   Traverse하는 Gate 및 Event 출력하기 : Message.Add (Gate/Event 이름) 이용

        Message.Show()  ' Message 출력하기

    End Sub


    ' 같은 Gate, Event는 나타나지 않는다고 가정하고 Top Event 값 구하기
    Public Sub Calcualte_Top(Top As String)

        ' Program : Top에 대한 Event Index 가져오기 

        ' Program : Top Index로부터 시작하여 FT 전체를 Traverse하기 
        '   한번 방문한 Gate는 다시 방문하지 않기 (이를 위해서는 Array 선언하여 이용하는 기법 필요) 

        Message.Add("Calculate Top -----")

        ' Program : Top Index로부터 시작하여 FT 전체를 Traverse하면서 Gate 값 구하기 
        '   한번 방문한 Gate는 다시 방문하지 않기 (이를 위해서는 Array 선언하여 이용하는 기법 필요) 
        '   계산된 값 출력
        Dim ix As Integer = FT.GetIndexOfEvent(Top)
        Call CalculateGate1(ix)
        Message.Show()

    End Sub
    Function CalculateGate1(P As Integer) As Single
        Dim x As Integer
        If (FT.XEvents(P).Child.Count = 0) Then ' Basic Event 
            Message.Add(FT.XEvents(P).Name)
            Return FT.XEvents(P).Proba

        Else        ' Gate

            'For i = 0 To FT.XEvents(P).Child.Count - 1
            '    x = FT.XEvents(P).Child(i)
            '    Call TraverseGate1(x)   ' Child에 대해 반복
            'Next
            Dim V(FT.XEvents(P).Child.Count) As Single
            For i = 0 To FT.XEvents(P).Child.Count - 1
                x = FT.XEvents(P).Child(i)
                V(i) = CalculateGate1(x)   ' Child에 대해 반복
            Next
            Dim Vg As Single = CalORAND(V, FT.XEvents(P).Child.Count, FT.XEvents(P).Type)
            Message.Add(FT.XEvents(P).Name & "    " & Vg)
            Return Vg
        End If
    End Function

    Function CalORAND(V() As Single, n As Integer, t As String) As Single
        Dim Vg As Single
        If t = "+" Then
            Vg = 0
            For i = 0 To n - 1
                Vg = Vg + V(i) - Vg * V(i)
            Next
        Else
            Vg = 1
            For i = 0 To n - 1
                Vg = Vg * V(i)
            Next
        End If
        Return Vg
    End Function
End Class
