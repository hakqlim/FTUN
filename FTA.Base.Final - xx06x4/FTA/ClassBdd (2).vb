Public Class ClassBdd

    Class ite_def       ' ITE 하나 저장용
        Public x As Integer    ' Pivot Event, Index to xEvents ()
        Public l, r As Integer ' Pointer to left, right 또는 0, 1
        Public v As Double     ' ite 의 Probability
    End Class

    Dim ite As List(Of ite_def) ' ITE 저장용

    Dim FT As ClassFtData       ' FT Data (외부에서 읽은 FT를 FT 라는 변수로 연결하기)

    Dim BE_Order() As Integer           ' BE 의 Order    (FT.XEvents.Count 만큼 필요)
    Dim Gate_Order As List(Of Integer)  ' Gate의 Order  (FT.XEvents.Count 만큼 필요)
    Dim pEvent_to_ite() As Integer      ' 각 FT에 대해 계산된 ITE 결과 : ite 에 대한 pointer (FT.XEvents.Count 만큼 필요)
    Dim TopName As String

    '-------------------------------------------------------------------------
    Public Sub Initialize(FT_Data As ClassFtData)

        ' 외부에서 읽은 FT를 연결하기 
        FT = FT_Data

        ite = New List(Of ite_def)  ' 초기화하기

        '' ite 에서 0 -> False, 1 -> True 를 나타내도록 (아래는 ClassFtData에서 이미 설정되어 있음)
        'FT.XEvents(0).Proba = 0.0       ' False Event
        'FT.XEvents(1).Proba = 1.0       ' True Event

        ' Array 설정
        Dim nEvent As Integer = FT.XEvents.Count
        ReDim pEvent_to_ite(nEvent)

        ' ite 에서 0 -> False, 1 -> True 를 나타내도록
        Dim ite0, ite1 As New ite_def
        ite0.v = 0.0
        ite1.v = 1.0
        ite.Add(ite0)
        ite.Add(ite1)

    End Sub

    '-------------------------------------------------------------------------
    ' Program : BDD 기본 Operation 처리하기
    ' BDD Operation : F = g <op> h
    ' g = ite(x, gl, gr), h = ite (y, hl, hr)
    '     x=ite(g).x, gl=ite(g).l, gr=ite(g).r
    '     y=ite(h).x, hl=ite(h).l, hr=ite(h).r

    Private Function Bdd_Solve(op As String, g As Integer, h As Integer) As Integer
        Dim p As Integer

        ' Program : g = 0 or 1, h = 0 or 1 이면 : 1 + H = 1,  1 * H = H,  0 + H = H,  0 * H = 0 처리하기 
        If (g <= 1 Or h <= 1) Then
            p = Bdd_Solve_Simple(op, g, h)
            Return p
        End If

        ' Program : g = h : g + g -> g, g * g -> g 처리하기 
        If (g = h) Then     ' g + g, g * g -> g
            Return g
        End If

        ' 일반적인 경우
        ' G <op> H = ite (x, Gl <op> Hl, Gr <op> Hr) -> Bdd_Solve(op, G, H)
        '   g = ite(x, gl, gr), h = ite (y, hl, hr)
        '   x=ite(g).x, gl=ite(g).l, gr=ite(g).r
        '   y=ite(h).x, hl=ite(h).l, hr=ite(h).r

        Dim x, gl, gr, y, hl, hr As Integer
        x = ite(g).x
        gl = ite(g).l
        gr = ite(g).r

        y = ite(h).x
        hl = ite(h).l
        hr = ite(h).r

        ' Program : BDD 기본 Operation 처리하기 
        '   F = g <op> h
        '   g = ite(x, gx, gr), h = ite (y, hx, hr)

        Dim pl, pr As Integer

        If (BE_Order(x) < BE_Order(y)) Then  ' if x < y
            ' G <op> H = ite (x, Gl <op> H, Gr <op> H)
            pl = Bdd_Solve(op, gl, h)   ' -> Bdd_Solve(op, Gl, H)
            pr = Bdd_Solve(op, gr, h)   ' -> Bdd_Solve(op, Gr, H)

            p = Put_ite(x, pl, pr)      ' p <- ite (x, pl, pr)

        ElseIf (BE_Order(x) > BE_Order(y)) Then ' if y < x
            ' G <op> H = ite (x, Hl <op> G, Hr <op> G)
            pl = Bdd_Solve(op, hl, g)
            pr = Bdd_Solve(op, hr, g)

            p = Put_ite(y, pl, pr)

        Else ' x = y
            ' G <op> H = ite (x, pl, pr)
            ' pl = Gl <op> Hl   -> Bdd_Solve(op, gl, hl)
            ' pr = Gr <op> Hr   -> Bdd_Solve(op, gr, hr)
            pl = Bdd_Solve(op, gl, hl)
            pr = Bdd_Solve(op, gr, hr)

            p = Put_ite(x, pl, pr)      'p <- ite (x, pl, pr)
        End If

        ' Program : 결과 ite에 대한 index를 return하기 
        Return p

    End Function

    ' Program : 하나가 True / False 인 경우
    '   g = 0 or 1, h = 0 or 1 이면 : 1 + H = 1,  1 * H = H,  0 + H = H,  0 * H = 0 처리하기 
    Private Function Bdd_Solve_Simple(op As String, g As Integer, h As Integer) As Integer

        ' Program : g = 0 or 1, h = 0 or 1 이면 : 1 + H = 1,  1 * H = H,  0 + H = H,  0 * H = 0 처리하기 
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

        ' Program : 결과 ite에 대한 index를 return하기 
        Return p

    End Function

    ' ITE 에 저장 p = x * l + /x * r  : ite (x, l, r)
    '   여기서 x는 basic event,  l, r 은 다른 ite에 대한 pointer
    Private Function Put_ite(x As Integer, l As Integer, r As Integer) As Integer
        Dim v, vx, vl, vr As Double

        If (l = r) Then
            Return l
        End If

        ' 저장
        Dim a_ite As New ite_def
        a_ite.x = x
        a_ite.l = l
        a_ite.r = r

        ' 값 계산
        vx = FT.XEvents(x).Proba        ' x 의 값
        vl = ite(l).v                   ' Left 값
        vr = ite(r).v                   ' Right 값
        v = vx * vl + (1.0 - vx) * vr   ' ite의 값
        a_ite.v = v

        ' 새로운 ite 저장하기 
        ite.Add(a_ite)

        ' ite에 대한 index를 return하기
        Return ite.Count - 1
    End Function

    '-------------------------------------------------------------------------
    ' 주어진 Top 에 대해 Solve하기
    Public Sub SolveFaultTree(TopNameIn As String)

        ' Top Event Name 저장
        TopName = TopNameIn

        ' 몇번째 Event인지 찾기 -----------------------------
        Dim ixTop As Integer
        ixTop = FT.GetIndexOfEvent(TopName)

        ' Ordering -------------------------------------------
        Dim Find_Order As New ClassFT_Order(FT)

        ' Event Ordering
        Call Find_Order.Find_BE_Order(TopName)       ' BE Order 찾고
        BE_Order = Find_Order.BE_Order

        ' Gate Ordering
        Find_Order.Find_BU_Gate_Order_FT(TopName)    ' Order 찾고
        Gate_Order = Find_Order.Gate_Order_BU       ' Gate_Order 에 저장

        ' BDD 초기화하기 ----------------------------------------
        Dim nEvent As Integer = FT.XEvents.Count

        For i = 2 To nEvent - 1
            If (FT.XEvents(i).Child.Count > 0) Then
                pEvent_to_ite(i) = -1       ' 아직 Gate는 계산 안됨
            Else
                pEvent_to_ite(i) = Put_ite(i, 1, 0) ' BE는 ite 생성
            End If
        Next

        ' BDD Solve 하기 -----------------------------------------
        Dim p As Integer = Solve_Gates()  ' Gate Oredring 에 따라

        ' Top Event 값은
        Dim v As Single = ite(p).v
        MessageBox.Show(TopName & " = " & v)

    End Sub

    ' Bottom-up 순서로 Solve할 Gate 순서를 미리 정하두고 
    ' Gate들 Solve하기 --> ite에 대한 pointer를 return
    Private Function Solve_Gates() As Integer
        Dim i, g, p As Integer

        ' 미리 주어진 순서대로 풀기 
        For i = 0 To Gate_Order.Count - 1
            g = Gate_Order(i)       ' i-th Gate
            p = Solve_A_Gate(g)     ' Gate g에 대해 풀기
        Next

        Return p        ' 결과 ite return하기 
    End Function

    ' Gate g 에 대해 풀기
    '   Return 값은 별 의미 없음 (pEvent_to_ite(g) 에 저장하므로)
    Private Function Solve_A_Gate(g As Integer) As Integer

        Dim op As String
        Dim e, e_sign As Integer
        Dim p, p1 As Integer
        Dim i, n As Integer

        op = FT.XEvents(g).Type         ' Gate Type
        n = FT.XEvents(g).Child.Count   ' Child 수

        e = FT.XEvents(g).Child(0)      ' 첫번째 Child
        e_sign = Math.Sign(e)           ' Normal Event는 1, Negate는 -1
        e = Math.Abs(e)                 ' 우선 Abs(e) 에 대해 계산하고
        p = pEvent_to_ite(e)            ' e에 대한 ite 
        If (e_sign = -1) Then p = Bdd_Negate(p) ' Negate면 
        If (p < 0) Then ErrMsg("Program Error - p < " & e & "-th Gate")

        For i = 1 To n - 1              ' 나머지 child 에 대해
            e = FT.XEvents(g).Child(i)
            e_sign = Math.Sign(e)
            e = Math.Abs(e)
            p1 = pEvent_to_ite(e)       ' e에 대한 ite 
            If (e_sign = -1) Then p1 = Bdd_Negate(p1) ' Negate면 
            If (p1 < 0) Then ErrMsg("Program Error - p < " & e & "-th Gate")

            p = Bdd_Solve(op, p, p1)    ' BDD Solve하고
        Next

        pEvent_to_ite(g) = p            ' Gate g에 대한 ite 결과 저장
        'If (p < 0) Then ErrMsg("Program Error - p < " & e & "-th Gate")
        Return p                        ' ite에 대한 index 를 return

    End Function

    '  --------------------------------------------------------------------
    ' Program : BDD -> zBDD 변경하기
    Public Function MinBdd(f As Integer) As Integer
        ' BDD를 cut set minimization 수행
        ' 기본적으로 모든 negate를 1로 처리하고 minimization
        ' Cutoff로 truncation 동시에 진행

        ' Program : f 가 0, 1 이면 minimize 필요 없음 
        If (f = 0) Or (f = 1) Then Return (f)

        ' Program : f = ite (x, l, r) 
        '   l, r 을 각기 minimize 하고 
        '   l 에 있는 term 들중 r 에 있는 term 은 삭제 
        '   binary reduction 과 동일한 algorithm
        Dim x, l, r, p As Integer
        x = ite(f).x
        l = ite(f).l
        r = ite(f).r

        If (r = 1) Then '   f = x * F1 + 1 => f = 1 로 처리
            p = 1     ' Return (1)
        ElseIf (l = r) Then '   r * x + r => r
            p = MinBdd(r)   ' (r에 대해 recursive하게 계산)
        Else     '   그 외는
            '       F = x * F1 + F0
            '       F1' <- MinBDD (F1)	    ' 각기 Minimize 
            '       F0' <- MinBDD (F0)	' 각기 Minimize 
            '       F1' <- MinBddBy (F1', F0')	' 이는 F1' 을 F0' 으로 Minimize
            '       F'  <- x * F1' + F0'        '  Minimize 된 결과 저장
            r = MinBdd(r)   ' F0' <- MinBDD (F0)
            If (r = 1) Then Return (1) ' x * l + 1 -> 1
            l = MinBdd(l)       ' F1' <- MinBDD (F1)
            l = MinBddBy(l, r)  ' F1' <- MinBddBy (F1', F0')
            If (l = 0) Then     ' Negate가 있는 FT에서 non minimal cut set이 나타나는 것을 어느 정도 완화시켜 줌
                p = r           ' t = 0 + r => t = r 로 변경
            Else
                p = Put_ite(x, l, r)    ' F'  <- x * F1' + F0'
            End If
        End If

        ' Program : 결과 ite에 대한 index를 return하기 
        Return (p)


        '' BDD를 cut set minimization 수행
        '' 기본적으로 모든 negate를 1로 처리하고 minimization
        '' Cutoff로 truncation 동시에 진행

        'Dim l, r, p As Integer
        '' f 가 0, 1 이면 minimize 필요 없음 
        'If (f = 0) Or (f = 1) Then Return (f)

        '' f = ite (x, l, r) 
        '' l, r 을 각기 minimize 하고 
        '' l 에 있는 term 들중 r 에 있는 term 은 삭제 
        '' binary reduction 과 동일한 algorithm

        'l = ite(f).l
        'r = ite(f).r

        'If (r = 1) Then
        '    Return (1)          ' t = x * F1 + 1 => t = 1 로 변경
        'ElseIf (l = r) Then     ' r * x + r => r
        '    p = MinBdd(r)
        'Else
        '    r = MinBdd(r)
        '    If (r = 1) Then Return (1)
        '    l = MinBdd(l)
        '    l = MinBddBy(l, r)
        '    If (l = 0) Then    ' Negate가 있는 FT에서 non minimal cut set이 나타나는 것을 어느 정도 완화시켜 줌
        '        p = r           ' t = 0 + f => t = f 로 변경
        '    Else
        '        p = Put_ite(ite(f).x, l, r)
        '    End If
        'End If

        'Return (p)

    End Function

    ' Program : Delete Term Operation
    '   F = x * F1 + F0, G = y * G1 + G0
    '   F' <- F Θ G
    Private Function MinBddBy(f As Integer, g As Integer) As Integer

        ' Program : 단순화 과정 먼저 처리 (결과를 그대로 return)
        '   F = 0 -> 0, G = 1 -> 0, G = 0 -> F, F = G -> 0, F = 1 -> 1 

        ' Program : 기본적인 Delete Term Operation
        '   f = ite(x, f1, f2),  g = ite(y, g1, g2) 

        '   if x < y : F1, F0 를 G 로 Minimize
        '       F1' <- F1 Θ G   : F1' = MinBddBy (F1, G) : Recursion 이용
        '       F0' <- F0 Θ G
        '       F' <- x * F1' + F0'     ' F' = Put_ite(x, F1', F0') 로 처리 
        '   if x > y : F 를 G0 로 Minimize
        '           F는 G1에 의해서는 Minimize 할 필요 없음
        '       F' <- F Θ G0
        '   if x = y : F1 은 G1, G0 로 Minimize , F0 는 G0 로 Minimize
        '       F1' <- F1 Θ G1
        '       F1'' <- F1' Θ G0
        '       F0' <- F0 Θ G0
        '       F'  <- x * F1'' + F0'

        '   Program : 결과를 저장하고 return

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
            l = MinBddBy(ite(f).l, g)   ' F1’<- F1 Θ G
            r = MinBddBy(ite(f).r, g)   ' F0’<- F0 Θ G
            p = Put_ite(ite(f).x, l, r) ' F’<- x * F1’ + F0’(조합)
        ElseIf (BE_Order(ite(f).x) > BE_Order(ite(g).x)) Then   ' f.x > g.x
            p = MinBddBy(f, ite(g).r)   ' F’<- F Θ G0
            '  f - g.x * g.l 은 빠져 있는 이유 (ordering 에 의해)
        Else    ' f.x = g.x
            l = MinBddBy(ite(f).l, ite(g).l)    ' F1’<- F1 Θ G1
            l = MinBddBy(l, ite(g).r)   ' F1'' <- F1’Θ G0 ' 추가 : Coherent Bdd에서의 min_by 
            r = MinBddBy(ite(f).r, ite(g).r)    ' F0’<- F0 Θ G0
            p = Put_ite(ite(f).x, l, r)     ' F’<- x * F1'' + F0
        End If

        Return (p)
    End Function

    '  --------------------------------------------------------------------
    ' Program : Negate 처리
    Private Function Bdd_Negate(f As Integer) As Integer

        ' Program : Negate 처리
        '   F = x * F1 + /x * F0
        '   /F = x * /F1 + /x * /F0     -> ite (x, /F1, /F0)
        '   여기에 단순화 과정 추가 필요
        '       /0 -> 1, /1 -> 0

        Dim x, fl, fr As Integer
        x = ite(f).x
        fl = ite(f).l
        fr = ite(f).r

        Dim nfl As Integer
        If (fl = 0) Then
            nfl = 1
        ElseIf (fl = 1) Then
            nfl = 0
        Else
            nfl = Bdd_Negate(fl)
        End If

        Dim nfr As Integer
        If (fr = 0) Then
            nfr = 1
        ElseIf (fr = 1) Then
            nfr = 0
        Else
            nfr = Bdd_Negate(fr)
        End If

        ' 식 재조합
        Dim p As Integer
        p = Put_ite(x, nfl, nfr)

        ' Program : 변환된 결과를 ite에 저장하고 return
        Return p

    End Function


    ' 결과 확인용 --------------------------------------------------------
    ' 주어진 Gate에 대해 BDD Print 하기
    Public Sub PrintITE()

        ' 몇번째 Event인지 찾기
        Dim TopEvent As Integer = FT.GetIndexOfEvent(TopName)
        Dim p As Integer = pEvent_to_ite(TopEvent) ' Top 에 대한 ITE index

        Call PrintGateValue()   ' Gate 값 먼저 출력

        Dim S As String
        Dim i, x As Integer
        If (ite.Count <= 200) Then
            S = TopName & "=" & p & "-th ITE ------------"
            Message.Add(S)
            For i = 2 To ite.Count - 1
                x = ite(i).x
                S = i & " (" & FT.XEvents(x).Name & ", " & ite(i).l & ", " & ite(i).r & " )" & vbTab & Convert.ToSingle(ite(i).v)
                Message.Add(S)
            Next
        Else
            S = "Too Many ITEs to be printed --------"
            S += vbCrLf & "noite = " & ite.Count
            Message.Add(S)
        End If

        Message.Show()
    End Sub

    ' 모든 Gate의 값을 출력 (Bottom-Up Order로)
    Private Sub PrintGateValue()
        Dim S As String
        Dim p As Integer    ' Pointer to ITE

        Message.Add("Gate Value -----------------")
        For Each G In Gate_Order
            p = pEvent_to_ite(G)    ' Pointer to ITE
            S = vbCrLf & FT.XEvents(G).Name & vbTab & Convert.ToSingle(ite(p).v)
            Message.Add(S)
        Next
    End Sub

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
        Dim TopEvent As Integer = FT.GetIndexOfEvent(TopName)
        Dim p As Integer = pEvent_to_ite(TopEvent) ' Top 에 대한 ITE index

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

        ElseIf (p > 1) Then
            NoLst += 1
            vx = FT.XEvents(ite(p).x).Proba

            Lst(NoLst) = ite(p).x           ' Positive
            Call ExpandBDD(CS, ite(p).l, Lst, NoLst, v * vx)

            Lst(NoLst) = -1 * ite(p).x      ' Negative
            Call ExpandBDD(CS, ite(p).r, Lst, NoLst, v * (1.0 - vx))
        ElseIf (p = 0) Then
            ' Do Nothing
        End If
    End Sub

    ' BDD 를 zBDD 로 변환하고 RAW file에 저장하기
    Public Sub SaveMCS(RawFileName As String)

        Dim CS As New ClassRawFile

        ' 몇번째 Event인지 찾기
        Dim TopEvent As Integer = FT.GetIndexOfEvent(TopName)
        Dim p As Integer = pEvent_to_ite(TopEvent) ' Top 에 대한 ITE index

        ' MCS (zBDD) 로 변경하고
        p = MinBdd(p)
        pEvent_to_ite(TopEvent) = p

        ' Cut Set을 RAW file에 저장하는 부분 -----------------------
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

            'Lst(NoLst) = -1 * ite(p).x      ' zBDD에서 Negative 는 추가하지 않음 
            Call ExpandMCS(CS, ite(p).r, Lst, NoLst - 1, v)
        ElseIf (p = 0) Then
            ' Do Nothing
        End If

    End Sub

End Class
