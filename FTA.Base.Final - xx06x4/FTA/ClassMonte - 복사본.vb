Public Class ClassMonte

    Dim FT As ClassFtData       ' FT Data
    Dim NoSample As Integer     ' # of Samples

    Dim E_State() As Integer        ' Gate/Event State  (FT.XEvents.Count 만큼 설정)
    Dim E_Count() As Integer        ' Failure Count     (FT.XEvents.Count 만큼 설정)

    Public Sub New(FT_Data As ClassFtData, NoSample_In As Integer)

        ' 외부에서 가져오는 Data 설정
        FT = FT_Data

        ' Array 설정
        Dim nEvent As Integer = FT.XEvents.Count
        ReDim E_State(nEvent)
        ReDim E_Count(nEvent)

        ' # of Sample 설정
        NoSample = NoSample_In
    End Sub

    ' 주어진 Top 에 대해 Solve하기
    Public Sub SolveFaultTree(TopName As String)

        ' Program : TopName에 대한 Index 가져오기 
        Dim TopIndex As Integer = FT.GetIndexOfEvent(TopName)

        ' Program : Monte Carlo 방법으로 Top Index에 대한 값을 계산하기 
        Dim V As Single = EvaluateTopEventProbability(TopIndex)

        Message.Add("V = " & V)

        ' Gate 들의 값을 Message에 저장 
        Call PrintGateValue()
        Message.Show()  ' Message 나타내기

    End Sub


    Private Function EvaluateTopEventProbability(T As Integer) As Single
        Dim r As Single
        Dim m As Integer = 0        ' # of Failure

        ' E_Count 초기화 
        For j = 2 To FT.XEvents.Count - 1
            E_Count(j) = 0
        Next

        'For number of samples n
        For i = 1 To NoSample

            ' E_State 초기화
            For j = 2 To FT.XEvents.Count - 1
                E_State(j) = 100        ' 계산된 것은 0, -1 로 변경됨
            Next

            '// determine the state of events
            'For each Ei  which is a basic event
            For j = 2 To FT.XEvents.Count - 1
                If (FT.XEvents(j).Child.Count = 0) Then
                    '// a random number r between 0 and 1
                    r = Rnd()
                    '// State of Ei
                    'if r ≤ Pi then Si = True else Si = False
                    If (r <= FT.XEvents(j).Proba) Then
                        E_State(j) = True
                    Else
                        E_State(j) = False
                    End If
                End If
            Next

            '// Calculate the state of top event
            Dim ST As Integer = GetState(T)
            If ST = True Then m = m + 1

        Next

        '// Evaluate the top event probability PT
        Dim PT As Single = m / NoSample
        Return PT

    End Function


    Private Function GetState(G As Integer) As Integer

        Dim Evt, u As Integer
        Dim n As Integer = FT.XEvents(G).Child.Count

        'return Sg for basic event 
        If (n = 0) Then
            Return E_State(G)
        End If

        ' For Gate
        ' if already processed
        If (E_State(G) <> 100) Then
            Return E_State(G)
        End If

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
    Private Sub PrintGateValue()
        Dim S As String
        Dim i As Integer
        Dim nEvent As Integer = FT.XEvents.Count

        Message.Add("Gate Probability --------------------")    ' 출력하기

        For i = 2 To nEvent - 1
            If (FT.XEvents(i).Child.Count > 0) Then ' Gate
                S = FT.XEvents(i).Name & vbTab & Format(E_Count(i) / NoSample, "0.0000e-00")
                Message.Add(S)  ' 출력하기
            End If
        Next

    End Sub

End Class
