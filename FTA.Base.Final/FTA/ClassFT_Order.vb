Public Class ClassFT_Order

    Dim FT As ClassFtData   ' 외부에서 읽은 FT Data 연결용

    Public Gate_Order_BU As New List(Of Integer)    ' Gate Ordering 
    '   Gate_Order_BU : 여기에 저장된 순서대로 Solve하기 위한 것 (Bottom-Up Ordering)

    Public BE_Order() As Integer '  BE에 대한 Ordering (FT.XEvents.Count 만큼 생성 필요)
    '   BDD 에서 각 Event의 순서를 비교하기 위한 것
    '   Event x 에 대하서는 BE_Order(x)에 비교용 값이 저장됨

    Dim nBE_Order As Integer = 0    ' Internal Variable 
    Dim Occ() As Integer            ' Internal Variable

    ' 초기화 --------------------------------------
    Public Sub New(FT_Data As ClassFtData)
        FT = FT_Data
    End Sub

    ' Gate Ordering -----------------------------------------------------

    ' 주어진 TopName 에 대해 Gate 를 Bottom-Up 으로 Ordering
    Public Sub Find_BU_Gate_Order_FT(TopName As String)

        Dim topEvent As Integer = FT.GetIndexOfEvent(TopName)
        Dim nEvent As Integer = FT.XEvents.Count

        Gate_Order_BU.Clear()
        ReDim Occ(nEvent)
        For i = 1 To nEvent - 1
            Occ(i) = 0
        Next

        ' Program : Gate Ordering 
        Call Find_BU_Gate_Order(topEvent)

    End Sub

    ' Bottom Up으로 Gate 순서 찾기 (Traverse FT 참고)
    Private Sub Find_BU_Gate_Order(G As Integer)

        ' Program : Gate Ordering 
        '   만일 G가 방문한 event면 나가기
        G = Math.Abs(G)
        If (Occ(G) <> 0) Then
            Exit Sub
        End If

        '   G를 방문하였다는 것을 표시
        Occ(G) = 1

        '   G가 Gate라면
        If (FT.XEvents(G).Child.Count > 0) Then
            '       각 Child에 대해 방문하기 (Recursion)
            For i = 0 To FT.XEvents(G).Child.Count - 1
                Dim c As Integer
                c = FT.XEvents(G).Child(i)
                Call Find_BU_Gate_Order(c)
            Next
            '   G를 Gate_Order_BU 에 추가
            Gate_Order_BU.Add(G)
        End If

        '   주의 : Negate에 대비하는 것 필요

    End Sub

    ' BE Ordering -------------------------------------------------------
    ' 주어진 Top Event에 대해 Event Ordering
    Public Function Find_BE_Order(TopName As String) As Integer()

        Dim nEvent As Integer = FT.XEvents.Count
        ReDim Occ(nEvent)
        For i = 0 To nEvent - 1
            Occ(i) = 0
        Next

        nBE_Order = 0
        Dim topEvent As Integer = FT.GetIndexOfEvent(TopName)

        ReDim BE_Order(nEvent)

        ' Program : BE Ordering --> BE_Order()
        Call Give_BE_Order_DepthFirst(topEvent)

        Return BE_Order
    End Function

    ' Depth First로 Event Ordering 하기 
    Private Sub Give_BE_Order_DepthFirst(G As Integer)

        ' Program : Event Ordering 하기
        '   G가 방문한 것이면 Exit
        G = Math.Abs(G)
        If (Occ(G) <> 0) Then
            Exit Sub
        End If

        '   G가 방문한 것이라는 것을 표시
        Occ(G) = 1

        '   G가 Gate라면 
        If (FT.XEvents(G).Child.Count > 0) Then
            '       Child 들중 Gate부터 점검 (Recursion)
            For i = 0 To FT.XEvents(G).Child.Count - 1
                Dim c As Integer = FT.XEvents(G).Child(i)
                Call Give_BE_Order_DepthFirst(c)
            Next
        Else
            '       Child 들중 BE 들을 Order 순을 정하고, BE_Order에 저장하기  (Add_BE_Order 참조)
            Call Add_BE_Order(G)
        End If
        '   주의 : Negate에 대비하는 것 필요

    End Sub

    ' BE 의 Order 저장하기
    Private Sub Add_BE_Order(ByVal e As Integer)
        nBE_Order += 1
        BE_Order(e) = nBE_Order
    End Sub

End Class
