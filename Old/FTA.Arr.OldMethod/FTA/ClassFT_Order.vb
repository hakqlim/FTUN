Public Class ClassFT_Order

    ' Gate Ordering -----------------------------------------------------
    Public Gate_Order_BU As New List(Of Integer)
    Dim BU_Occ() As Integer

    ' 주어진 TopName 에 대해 Gate 를 Bottom-Up 으로 Ordering
    Public Sub Find_BU_Gate_Order_FT(FT As ClassFtData, TopName As String)

        Dim topEvent As Integer
        topEvent = FT.GetIndexOfEvent(TopName)

        Gate_Order_BU.Clear()
        ReDim BU_Occ(ClassFtData.MxEvent)
        For i = 1 To FT.NoEvent
            BU_Occ(i) = 0
        Next

        Call Find_BU_Gate_Order(FT, topEvent)

    End Sub

    Private Sub Find_BU_Gate_Order(FT As ClassFtData, G As Integer)
        Dim i, e As Integer

        If (BU_Occ(G) > 0) Then
            Exit Sub
        End If

        BU_Occ(G) += 1      ' Occurrence 추가

        If (FT.XEvents(G).NoChild > 0) Then
            For i = 0 To FT.XEvents(G).NoChild - 1
                e = FT.XEvents(G).Child(i)
                Call Find_BU_Gate_Order(FT, e)
            Next

            Gate_Order_BU.Add(G)    ' Order에 추가
        Else

        End If
    End Sub

    ' BE Ordering -------------------------------------------------------
    Dim BE_Check() As Integer
    Dim nBE_Order As Integer
    Public BE_Order() As Integer

    ' 주어진 Top Event에 대해 Event Ordering
    Public Function Find_BE_Order(FT As ClassFtData, TopName As String) As Integer()
        ReDim BE_Check(ClassFtData.MxEvent)
        For i = 0 To FT.NoEvent
            BE_Check(i) = 0
        Next

        nBE_Order = 0
        Dim topEvent As Integer
        topEvent = FT.GetIndexOfEvent(TopName)

        ReDim BE_Order(FT.NoEvent)

        ' BE Ordering --> BE_Order()
        Call Give_BE_Order_DepthFirst(FT, topEvent)

        Return BE_Order
    End Function

    Private Sub Give_BE_Order_DepthFirst(FT As ClassFtData, T As Integer)
        Dim i, n, e As Integer
        If (BE_Check(T)) Then
            ' Exit Sub
        Else
            BE_Check(T) = True    ' 점검한 것을 나타냄
            n = FT.XEvents(T).NoChild
            For i = 0 To n - 1      ' 먼저 Chld Gate 아래부터 점검
                e = FT.XEvents(T).Child(i)
                If (FT.XEvents(e).NoChild > 0) Then
                    Call Give_BE_Order_DepthFirst(FT, e)
                End If
            Next

            For i = 0 To n - 1      ' Basic Event 점검 
                e = FT.XEvents(T).Child(i)
                If (FT.XEvents(e).NoChild = 0) Then
                    Call Add_BE_Order(e)
                End If
            Next
        End If
    End Sub

    Private Sub Add_BE_Order(ByVal e As Integer)
        nBE_Order += 1
        BE_Order(e) = nBE_Order
    End Sub

End Class
