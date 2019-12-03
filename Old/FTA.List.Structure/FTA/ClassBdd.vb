Public Class ClassBdd

    Structure ite_type
        Dim x As Integer    ' Pivot Event, Index to xEvents ()
        Dim l, r As Integer ' Pointer to left, right 또는 0, 1
        Dim v As Double     ' ite 의 Probability
    End Structure

    Dim ite() As ite_type   ' ite를 저장하기 위한 Array 들
    Dim noite As Integer    ' ite 의 수
    Dim mxite As Integer = 10000   ' 최대 할당할 수 있는 ite 수

    Dim FT As ClassFtData       ' FT Data
    Dim BE_Order() As Integer   ' BE 의 Order
    Dim Gate_Order As List(Of Integer)   ' Gate의 Order
    Dim pEvent_to_ite() As Integer    ' 각 FT에 대해 계산된 결과 : ite 에 대한 pointer
    Dim TopName As String

    '-------------------------------------------------------------------------
    Public Sub Initialize(FT_In As ClassFtData)

        ' 외부에서 가져오는 Data 설정
        FT = FT_In

        '' ite 에서 0 -> False, 1 -> True 를 나타내도록 (아래는 ClassFtData에서 이미 설정되어 있음)
        'FT.XEvents(0).Proba = 0.0       ' False Event
        'FT.XEvents(1).Proba = 1.0       ' True Event

        ' Array 설정
        Dim nEvent As Integer = FT.XEvents.Count
        ReDim pEvent_to_ite(nEvent)
        ReDim ite(mxite)

        ' ite 에서 0 -> False, 1 -> True 를 나타내도록
        noite = 1   ' FT에 대한 ite는 2부터 시작하도록
        ite(0).v = 0.0
        ite(1).v = 1.0

    End Sub


    '-------------------------------------------------------------------------
    ' BDD Operation : F = g <op> h
    ' g = ite(x, gx, gr), h = ite (y, hx, hr)
    Private Function Bdd_Solve(op As String, g As Integer, h As Integer) As Integer
        Dim p, l, r As Integer
        Dim bg, bh As ite_type

        If (g <= 1 Or h <= 1) Then
            p = Bdd_Solve_Simple(op, g, h)
            Return p
        End If
        If (g = h) Then     ' g + g, g * g -> g
            Return g
        End If

        bg = ite(g)
        bh = ite(h)

        If (bg.x = bh.x) Then   ' x = y
            l = Bdd_Solve(op, bg.l, bh.l)
            r = Bdd_Solve(op, bg.r, bh.r)
            If (l = r) Then     ' l + l, l * l -> l
                p = l
            Else
                p = Put_ite(bg.x, l, r)
            End If
        ElseIf (BE_Order(bg.x) <= BE_Order(bh.x)) Then   ' x < y
            l = Bdd_Solve(op, bg.l, h)
            r = Bdd_Solve(op, bg.r, h)
            If (l = r) Then     ' l + l, l * l -> l
                p = l
            Else
                p = Put_ite(bg.x, l, r)
            End If
        Else                                            ' x > y
            l = Bdd_Solve(op, bh.l, g)
            r = Bdd_Solve(op, bh.r, g)
            If (l = r) Then     ' l + l, l * l -> l
                p = l
            Else
                p = Put_ite(bh.x, l, r)
            End If
        End If

        Return p
    End Function

    ' 하나가 True / False 인 경우
    Private Function Bdd_Solve_Simple(op As String, g As Integer, h As Integer) As Integer
        Dim p As Integer
        If (op = "+") Then      ' OR Case
            If (g = 1) Then     ' 1 + H 
                p = 1
            ElseIf (g = 0) Then ' 0 + H
                p = h
            ElseIf (h = 1) Then ' G + 1
                p = 1
            ElseIf (h = 0) Then ' G + 0
                p = g
            End If
        Else                    ' AND Case
            If (g = 1) Then     ' 1 * H 
                p = h
            ElseIf (g = 0) Then ' 0 * H
                p = 0
            ElseIf (h = 1) Then ' G * 1
                p = g
            ElseIf (h = 0) Then ' G * 0
                p = 0
            End If
        End If
        Return p
    End Function

    ' ITE 에 저장 p = x * l + /x * r  : ite (x, l, r)
    '   여기서 x는 basic event,  l, r 은 다른 ite에 대한 pointer
    Private Function Put_ite(x As Integer, l As Integer, r As Integer) As Integer
        Dim v, vx, vl, vr As Double
        ' 저장
        noite += 1
        ite(noite).x = x
        ite(noite).l = l
        ite(noite).r = r

        ' 값 계산
        vx = FT.XEvents(x).Proba        ' x 의 값
        vl = ite(l).v                   ' Left 값
        vr = ite(r).v                   ' Right 값
        v = vx * vl + (1.0 - vx) * vr   ' ite의 값
        ite(noite).v = v

        Return noite
    End Function

    '-------------------------------------------------------------------------
    ' 주어진 Top 에 대해 Solve하기
    Public Sub SolveFaultTree(TopNameIn As String)

        ' Top Event Name 저장
        TopName = TopNameIn

        ' 몇번째 Event인지 찾기
        Dim TopEvent As Integer
        TopEvent = FT.GetIndexOfEvent(TopName)

        ' Ordering 
        Dim Find_Order As New ClassFT_Order

        ' Event Ordering
        Call Find_Order.Find_BE_Order(FT, TopName)       ' BE Order 찾고
        BE_Order = Find_Order.BE_Order

        ' Gate Ordering
        Find_Order.Find_BU_Gate_Order_FT(FT, TopName)    ' Order 찾고
        Gate_Order = Find_Order.Gate_Order_BU       ' Gate_Order 에 저장

        ' BDD 초기화하기 
        Dim nEvent As Integer = FT.XEvents.Count

        For i = 2 To nEvent - 1
            If (FT.XEvents(i).Child.Count > 0) Then
                pEvent_to_ite(i) = -1       ' 아직 Gate는 계산 안됨
            Else
                pEvent_to_ite(i) = Put_ite(i, 1, 0)
            End If
        Next

        ' BDD Solve 하기
        Dim p As Integer = Solve_Gates()  ' Gate Oredring 에 따라

        ' Top Event 값은
        Dim v As Single = ite(p).v
        MessageBox.Show(TopName & " = " & v)

    End Sub

    ' Bottom-up 순서로 Solve할 Gate 순서를 미리 정하기
    ' Gate들 Solve하기 --> ite에 대한 pointer를 return
    Private Function Solve_Gates() As Integer
        Dim i, g, p As Integer

        ' 미리 주어진 순서대로 풀기 
        For i = 0 To Gate_Order.Count - 1
            g = Gate_Order(i)       ' i-th Gate
            p = Solve_A_Gate(g)     ' Gate g에 대해 풀기
        Next

        Return p
    End Function

    ' Gate g 에 대해 풀기
    '   Return 값은 별 의미 없음 (pFT_ite(g) 에 저장하므로)
    Private Function Solve_A_Gate(g As Integer) As Integer
        Dim op As String
        Dim e As Integer
        Dim p, p1 As Integer
        Dim i, n As Integer

        op = FT.XEvents(g).Type         ' Gate Type
        n = FT.XEvents(g).Child.Count       ' Child 수

        e = FT.XEvents(g).Child(0)      ' 첫번째 Child
        p = pEvent_to_ite(e)                  ' e에 대한 ite 
        If (p < 0) Then ErrMsg("Program Error - p < " & e & "-th Gate")

        For i = 1 To n - 1              ' 나머지 child 에 대해
            e = FT.XEvents(g).Child(i)

            p1 = pEvent_to_ite(e)             ' e에 대한 ite 
            If (p1 < 0) Then ErrMsg("Program Error - p < " & e & "-th Gate")

            p = Bdd_Solve(op, p, p1)    ' BDD Solve하고
        Next

        pEvent_to_ite(g) = p                  ' Gate g에 대한 ite 결과 저장
        Return p

    End Function


    ' 결과 확인용 --------------------------------------------------------
    ' 주어진 Gate에 대해 BDD Print 하기
    Public Sub PrintITE()

        ' 몇번째 Event인지 찾기
        Dim TopEvent As Integer
        TopEvent = FT.GetIndexOfEvent(TopName)

        Dim i, p, x As Integer
        p = pEvent_to_ite(TopEvent) ' Top 에 대한 ITE index

        Dim S As String = ""

        Call PrintGateValue(S)

        If (noite <= 200) Then
            S += vbCrLf & TopName & "=" & p & "-th ITE ------------"
            For i = 2 To noite
                x = ite(i).x
                S += vbCrLf & i & " (" & FT.XEvents(x).Name & ", " & ite(i).l & ", " & ite(i).r & " )" & vbTab & Convert.ToSingle(ite(i).v)
            Next
        Else
            S += vbCrLf & "Too Many ITEs to be printed --------"
            S += vbCrLf & "noite = " & noite
        End If

        Message.AddAndShow(S)
    End Sub

    ' 모든 Gate의 값을 출력 (Bottom-Up Order로)
    Private Sub PrintGateValue(ByRef S As String)
        Dim p As Integer    ' Pointer to ITE

        S += vbCrLf & "Gate Value -----------------"
        For Each G In Gate_Order
            p = pEvent_to_ite(G)    ' Pointer to ITE
            S += vbCrLf & FT.XEvents(G).Name & vbTab & Convert.ToSingle(ite(p).v)
        Next
        S += vbCrLf
    End Sub

    ' ite 값 구하기 
    ' (이미 계산한 부분은 다시 하지 않도록 수정 필요)
    Private Function Proba_ite(p As Integer) As Double
        Dim x, l, r As Integer
        Dim v, vx, vl, vr As Double
        If (p = 0) Then
            v = 0.0
        ElseIf (p = 1) Then
            v = 1.0
        Else
            x = ite(p).x
            l = ite(p).l
            r = ite(p).r
            vx = FT.XEvents(x).Proba
            vl = Proba_ite(l)
            vr = Proba_ite(r)
            v = vx * vl + (1.0 - vx) * vr
        End If
        Return v
    End Function

    ' Count # of Cut Set
    Public Function NumberOfCutSet() As Long
        ' 몇번째 Event인지 찾기
        Dim TopEvent As Integer
        TopEvent = FT.GetIndexOfEvent(TopName)

        Dim p As Integer
        p = pEvent_to_ite(TopEvent) ' Top 에 대한 ITE index

        Dim n As Long = 0
        n = NumberOfCutSet(p)
        Return n
    End Function

    Private Function NumberOfCutSet(p As Integer) As Long
        Dim n, nl, nr As Long
        If (p = 1) Then
            n = 1
        ElseIf (p > 1) Then
            nl = NumberOfCutSet(ite(p).l)
            nr = NumberOfCutSet(ite(p).r)
            n = nl + nr
        ElseIf (p = 0) Then
            n = 0
        End If
        Return n
    End Function

    '  --------------------------------------------------------------------
    ' Save RawFile
    Dim NoSet As Integer
    Public Sub SaveBDD(RawFileName As String)

        Dim CS As New ClassRawFile

        ' 몇번째 Event인지 찾기
        Dim TopEvent As Integer
        TopEvent = FT.GetIndexOfEvent(TopName)

        Dim p As Integer
        p = pEvent_to_ite(TopEvent) ' Top 에 대한 ITE index

        ' Array 초기화
        Dim nEvent As Integer = FT.XEvents.Count
        ReDim CS.XData.EventName(nEvent)
        ReDim CS.XData.EventProb(nEvent)

        ' FT.XEvents (2 ~ nEvent-1) --> CS.XData (1 ~ nEvent-2)

        ' XData 
        CS.XData.BlockName = "DATA"
        CS.XData.NumOfEvent = nEvent - 2
        For i = 2 To nEvent - 1
            CS.XData.EventName(i - 1) = FT.XEvents(i).Name
            CS.XData.EventProb(i - 1) = FT.XEvents(i).Proba
        Next

        ' XCutSet
        CS.XCutSet.BlockName = TopName
        CS.XCutSet.NumOfCutSet = NumberOfCutSet()

        ' Array 초기화
        ReDim CS.XCutSet.XaCutSet(CS.XCutSet.NumOfCutSet)

        If (CS.XCutSet.NumOfCutSet > 10000) Then
            MsgBox("Too Many Cut Sets : " & CS.XCutSet.NumOfCutSet)
            Exit Sub
        End If

        Dim Lst(nEvent) As Integer, NoLst As Integer        ' Lst : 지나간 path 저장용
        NoLst = -1 : NoSet = 0
        Call ExpandBDD(CS, p, Lst, NoLst, 1.0)
        If (NoSet <> CS.XCutSet.NumOfCutSet) Then
            MsgBox("Error")
            Exit Sub
        End If

        ' Save on RawFile
        Call CS.SaveRawFile(RawFileName, TopName)

    End Sub

    ' ITE를 MCS로 전개하기
    ' Lst : 지나간 path 저장용
    Private Sub ExpandBDD(ByRef CS As ClassRawFile, p As Integer, Lst() As Integer, ByVal NoLst As Integer, v As Single)
        Dim i As Integer, vx As Double
        'Dim S As String
        If (p = 1) Then     ' 여기까지 더해진 Lst() -> XCutSet에 추가
            ''S = v.ToString

            NoSet += 1
            ReDim CS.XCutSet.XaCutSet(NoSet - 1).Elems(NoLst)       ' Array 초기화
            CS.XCutSet.XaCutSet(NoSet - 1).NoElem = NoLst + 1
            For i = 0 To NoLst
                CS.XCutSet.XaCutSet(NoSet - 1).Elems(i) = Math.Sign(Lst(i)) * (Math.Abs(Lst(i)) - 1)   ' XEvents (e) --> XData (e-1)
                ''S += vbTab & Math.Sign(Lst(i)).ToString & "|" & FT.XEvents(Math.Abs(Lst(i))).Name
            Next

            ''Debug.Print(S)

        ElseIf (p > 1) Then
            NoLst += 1
            vx = FT.XEvents(ite(p).x).Proba

            Lst(NoLst) = ite(p).x           ' Positive
            Call ExpandBDD(CS, ite(p).l, Lst, NoLst, v * vx)

            Lst(NoLst) = -1 * ite(p).x      ' Negative
            Call ExpandBDD(CS, ite(p).r, Lst, NoLst, v * (1.0 - vx))
            'ElseIf (p = 0) Then     ' Do Nothing
        End If
    End Sub


    '  --------------------------------------------------------------------
    ' BDD -> zBDD 변경하기
    Public Function MinBdd(f As Integer) As Integer
        ' BDD를 cut set minimization 수행
        ' 기본적으로 모든 negate를 1로 처리하고 minimization
        ' Cutoff로 truncation 동시에 진행

        Dim l, r, p As Integer
        ' f 가 0, 1 이면 minimize 필요 없음 
        If (f = 0) Or (f = 1) Then Return (f)

        ' f = ite (x, l, r) 
        ' l, r 을 각기 minimize 하고 
        ' l 에 있는 term 들중 r 에 있는 term 은 삭제 
        ' binary reduction 과 동일한 algorithm

        l = ite(f).l
        r = ite(f).r

        If (r = 1) Then
            Return (1)          ' t = x * F1 + 1 => t = 1 로 변경
        ElseIf (l = r) Then     ' r * x + r => r
            p = MinBdd(r)
        Else
            r = MinBdd(r)
            If (r = 1) Then Return (1)
            l = MinBdd(l)
            l = MinBddBy(l, r)
            If (l = 0) Then    ' Negate가 있는 FT에서 non minimal cut set이 나타나는 것을 어느 정도 완화시켜 줌
                p = r           ' t = 0 + f => t = f 로 변경
            Else
                p = Put_ite(ite(f).x, l, r)
            End If
        End If

        Return (p)

    End Function

    Private Function MinBddBy(f As Integer, g As Integer) As Integer
        Dim l, r, p As Integer

        If (f = 0) Then
            Return (0)
        ElseIf (g = 1) Then
            Return (0)
        ElseIf (g = 0) Then
            Return (f)
        ElseIf (f = g) Then     ' minimize f by f => 0 
            Return (0)
        ElseIf (f = 1) Then
            Return (1)
        End If

        ' f = ite(x, f1, f2),  g = ite(y, g1, g2) 
        If (BE_Order(ite(f).x) < BE_Order(ite(g).x)) Then       ' f.x < g.x
            l = MinBddBy(ite(f).l, g)
            r = MinBddBy(ite(f).r, g)
            p = Put_ite(ite(f).x, l, r)
        ElseIf (BE_Order(ite(f).x) > BE_Order(ite(g).x)) Then   ' f.x > g.x
            p = MinBddBy(f, ite(g).r)
            '  ? 확인 필요 : f - g.x * g.l 은 빠져 있는 이유 (ordering에 의해)
        Else    ' f.x = g.x
            l = MinBddBy(ite(f).l, ite(g).l)
            l = MinBddBy(l, ite(g).r)    ' 추가 : Coherent Bdd에서의 min_by 
            r = MinBddBy(ite(f).r, ite(g).r)
            p = Put_ite(ite(f).x, l, r)
        End If

        Return (p)
    End Function

    Public Sub SaveMCS(RawFileName As String)

        Dim CS As New ClassRawFile

        ' 몇번째 Event인지 찾기
        Dim TopEvent As Integer
        TopEvent = FT.GetIndexOfEvent(TopName)

        Dim p As Integer
        p = pEvent_to_ite(TopEvent) ' Top 에 대한 ITE index

        ' MCS 로 변경하고
        p = MinBdd(p)
        pEvent_to_ite(TopEvent) = p

        ' Array 초기화
        Dim nEvent As Integer = FT.XEvents.Count
        ReDim CS.XData.EventName(nEvent)
        ReDim CS.XData.EventProb(nEvent)

        ' FT.XEvents (2 ~ nEvent-1) --> CS.XData (1 ~ nEvent-2)

        ' XData 
        CS.XData.BlockName = "DATA"
        CS.XData.NumOfEvent = nEvent - 2
        For i = 2 To nEvent - 1
            CS.XData.EventName(i - 1) = FT.XEvents(i).Name
            CS.XData.EventProb(i - 1) = FT.XEvents(i).Proba
        Next

        ' XCutSet
        CS.XCutSet.BlockName = TopName
        CS.XCutSet.NumOfCutSet = NumberOfCutSet()

        ' Array 초기화
        ReDim CS.XCutSet.XaCutSet(CS.XCutSet.NumOfCutSet)

        If (CS.XCutSet.NumOfCutSet > 10000) Then
            MsgBox("Too Many Cut Sets : " & CS.XCutSet.NumOfCutSet)
            Exit Sub
        End If

        Dim Lst(nEvent) As Integer, NoLst As Integer        ' Lst() : 지나간 Path 저장용
        NoLst = -1 : NoSet = 0
        Call ExpandMCS(CS, p, Lst, NoLst, 1.0)
        If (NoSet <> CS.XCutSet.NumOfCutSet) Then
            MsgBox("Error")
            Exit Sub
        End If

        ' Save on RawFile
        Call CS.SaveRawFile(RawFileName, TopName)
    End Sub

    ' ITE를 MCS로 전개하기
    Private Sub ExpandMCS(ByRef CS As ClassRawFile, p As Integer, Lst() As Integer, ByVal NoLst As Integer, v As Single)
        Dim i As Integer, vx As Double
        'Dim S As String
        If (p = 1) Then     ' 여기까지 더해진 Lst() -> XCutSet에 추가
            ''S = v.ToString

            NoSet += 1
            ReDim CS.XCutSet.XaCutSet(NoSet - 1).Elems(NoLst)       ' Array 초기화
            CS.XCutSet.XaCutSet(NoSet - 1).NoElem = NoLst + 1
            For i = 0 To NoLst
                CS.XCutSet.XaCutSet(NoSet - 1).Elems(i) = Math.Sign(Lst(i)) * (Math.Abs(Lst(i)) - 1)   ' XEvents (e) --> XData (e-1)
                ''S += vbTab & Math.Sign(Lst(i)).ToString & "|" & FT.XEvents(Math.Abs(Lst(i))).Name
            Next

            ''Debug.Print(S)

        ElseIf (p > 1) Then
            NoLst += 1
            vx = FT.XEvents(ite(p).x).Proba

            Lst(NoLst) = ite(p).x           ' Positive
            Call ExpandMCS(CS, ite(p).l, Lst, NoLst, v * vx)

            'Lst(NoLst) = -1 * ite(p).x      ' Negative
            Call ExpandMCS(CS, ite(p).r, Lst, NoLst - 1, v)
            'ElseIf (p = 0) Then     ' Do Nothing
        End If
    End Sub

    '  --------------------------------------------------------------------
    ' Negate
    Private Function Bdd_Negate(f As Integer) As Integer
        Dim x, l, r, p As Integer
        x = ite(f).x
        l = ite(f).l
        r = ite(f).r

        ' f = ite (x, l, r)
        If (l = 0) Then
            l = 1
        ElseIf (l = 1) Then
            l = 0
        Else
            l = Bdd_Negate(l)
        End If

        If (r = 0) Then
            r = 1
        ElseIf r = 1 Then
            r = 0
        Else
            r = Bdd_Negate(r)
        End If

        p = Put_ite(x, l, r)
        Return (p)

    End Function

End Class
