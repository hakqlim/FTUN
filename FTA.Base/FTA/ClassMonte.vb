Public Class ClassMonte

    Dim FT As ClassFtData       ' FT Data
    Dim NoSample As Integer     ' # of Samples

    Dim E_State() As Integer        ' Gate/Event State  (FT.XEvents.Count 만큼 설정)
    Dim E_Count() As Integer        ' Failure Count     (FT.XEvents.Count 만큼 설정)
    Dim E_Visited() As Integer      ' Visit 점검용      (FT.XEvents.Count 만큼 설정)

    Public Sub New(FT_Data As ClassFtData, NoSample_In As Integer)

        ' 외부에서 가져오는 Data 설정
        FT = FT_Data

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

        ' Program : TopName에 대한 Index 가져오기 

        ' Program : Monte Carlo 방법으로 Top Index에 대한 값을 계산하기 



        ' Gate 들의 값을 Message에 저장 
        Call PrintGateValue()
        Message.Show()  ' Message 나타내기

    End Sub

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
