Public Class ClassMonte

    Dim E_State() As Integer        ' Event State
    Dim E_Count() As Integer        ' Failure Count
    Dim E_Visited() As Integer      ' Visit 점검용 
    Dim NoSample As Integer

    Dim FT As ClassFtData       ' FT Data

    Public Sub Initialize(FT_In As ClassFtData, NoSample_In As Integer)

        ' 외부에서 가져오는 Data 설정
        FT = FT_In

        ' Array 설정
        Dim nEvent As Integer = FT.XEvents.Count
        ReDim E_State(nEvent)
        ReDim E_Count(nEvent)
        ReDim E_Visited(nEvent)

        ' # of Sample 설정
        NoSample = NoSample_In
    End Sub

    ' 주어진 Top 에 대해 Solve하기
    Public Sub SolveFaultTree(TopName As String)

        ' 몇번째 Event인지 찾기
        Dim TopEvent As Integer
        TopEvent = FT.GetIndexOfEvent(TopName)

        '  Solve 하기
        Dim v As Single = EvaluateTopEventProbability(TopEvent)  ' Gate Oredring 에 따라
        Dim S As String = TopName & " = " & v

        ' 출력
        Call PrintGateValue(S)
        Message.AddAndShow(S)

    End Sub

    Private Function EvaluateTopEventProbability(T As Integer) As Single
        Dim i, j, u As Integer
        Dim r, v As Single

        Dim nEvent As Integer = FT.XEvents.Count

        ' E_Count 초기화하기 
        For k = 2 To nEvent - 1
            E_Count(k) = 0
        Next

        ' Monte Carlo 계산
        For i = 1 To NoSample

            ' Visited 초기화
            For k = 2 To nEvent - 1
                E_Visited(k) = False
            Next

            ' Determine the State of BEs
            For j = 2 To nEvent - 1
                If (FT.XEvents(j).Child.Count = 0) Then
                    r = Rnd()
                    ' State of Event j
                    If (r <= FT.XEvents(j).Proba) Then
                        E_State(j) = True
                    Else
                        E_State(j) = False
                    End If
                End If
            Next

            ' Calculate the State of Top Event
            u = GetState(T)

        Next

        ' Evaluate the top event probability PT
        v = E_Count(T) / NoSample
        Return v

    End Function

    ' 각 Gate의 State 정하기 (Recursion)
    Private Function GetState(G As Integer) As Integer
        Dim Evt, u As Integer
        Dim n As Integer = FT.XEvents(G).Child.Count

        'return Sg for basic event 
        If (n = 0) Then
            Return E_State(G)
        End If

        ' For Gate
        ' if already processed
        If (E_Visited(G)) Then
            Return E_State(G)
        End If

        ' Visit 설정
        E_Visited(G) = True

        ' Calculate the state for each child
        For j = 0 To n - 1
            Evt = FT.XEvents(G).Child(j)
            E_State(Evt) = GetState(Evt)
        Next

        ' Determine the State of g
        u = DetermineState(G)
        E_State(G) = u          ' 각 Gate에 대해 State 저장

        If (u) Then
            E_Count(G) += 1     ' # of Failure
        End If

        Return u

    End Function

    ' OR, AND gate의 상태 결정
    Private Function DetermineState(G As Integer) As Integer
        Dim u, Evt As Integer
        Dim n As Integer = FT.XEvents(G).Child.Count

        If (FT.XEvents(G).Type = "+") Then
            u = False
            For j = 0 To n - 1
                Evt = FT.XEvents(G).Child(j)
                u = u Or E_State(Evt)
            Next j
        ElseIf (FT.XEvents(G).Type = "*") Then
            u = True
            For j = 0 To n - 1
                Evt = FT.XEvents(G).Child(j)
                u = u And E_State(Evt)
            Next j
        End If

        'E_State(G) = u
        Return u
    End Function

    ' 각 Gate에 대한 값 출력하기
    Private Sub PrintGateValue(ByRef S As String)
        Dim i As Integer
        Dim nEvent As Integer = FT.XEvents.Count

        S += vbCrLf & "Gate Probability --------------------"
        For i = 2 To nEvent - 1
            If (FT.XEvents(i).Child.Count > 0) Then ' Gate
                S += vbCrLf & FT.XEvents(i).Name & vbTab & Format(E_Count(i) / NoSample, "0.0000e-00")
            End If
        Next

    End Sub

End Class
