Public Class ClassFtData

    ' FT Structure 관련 -------------------------------------

    ' 하나의 Event에 대한 정보
    Public Class cEvent_Def
        Public Name As String
        Public Type As String
        Public Proba As Single
    End Class

    ' FT 전체 저장
    Public XEvents As List(Of cEvent_Def)
End Class
