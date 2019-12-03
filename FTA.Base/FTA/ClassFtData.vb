Public Class ClassFtData

    ' FT Structure 관련 -------------------------------------

    ' 하나의 Event에 대한 정보
    Public Class cEvent_Def
        Public Name As String
        Public Type As String
        Public Proba As Single
        Public Child As List(Of Integer)

        Sub New()
            Child = New List(Of Integer)    ' cEvent_Def 생성시 Child List 초기화 
        End Sub
    End Class

    ' FT 전체 저장
    Public XEvents As List(Of cEvent_Def)

    ' -------------------------------------------------------------------
    ' File 읽기
    Public Sub ReadFT(FileName As String)

        ' File 열기 (using StreamReader)
        Dim strReader As System.IO.StreamReader = System.IO.File.OpenText(FileName)

        ' 초기화 (0 - False, 1 - True 용, 실제 FT Event는 2부터 저장되도록)
        Dim E1, E2 As New cEvent_Def
        E1.Name = "FALSE" : E1.Type = "B" : E1.Proba = 0.0 : E1.Child = New List(Of Integer)
        E2.Name = "TRUE" : E2.Type = "B" : E2.Proba = 1.0 : E2.Child = New List(Of Integer)

        XEvents = New List(Of cEvent_Def)   ' FT 전체 초기화 
        XEvents.Add(E1)
        XEvents.Add(E2)

        ' Program : FT File 읽기
        '   Program : Call ReadFtBlock(strReader) ' Read FT Block
        '   Program : Call ReadDataBlock(strReader)   ' Read Data Block

        strReader.Close()   ' File 닫기

    End Sub

    ' Program : FT Block 읽기 (아래 완성하기)
    Private Sub ReadFtBlock(strReader As System.IO.StreamReader)
        Dim aLine As String
        Dim sL() As String, Sep(1) As String

        Sep(0) = " " : Sep(1) = vbTab

        Do While (strReader.Peek() <> -1)   ' 끝이 아니면 계속 읽기

            aLine = strReader.ReadLine '    한줄 읽기

            ' Program :  *가 아니면 skip
            '   ENDTREE 까지 읽기 (ENDTREE를 만나면 Exit Do)
            '   Word 단위로 분리하여 SL() 에 저장
            '   첫번째 Word = Event Name, 두번째 Word = Type, 3번째 이상은 Child 들

        Loop
    End Sub

    ' Program : Data Block 읽기 (아래 완성하기)
    Private Sub ReadDataBlock(strReader As System.IO.StreamReader)
        Dim aLine As String
        Dim sL() As String, Sep(1) As String
        Sep(0) = " " : Sep(1) = vbTab

        ' Program : IMPORT 부터 Data Block 시작됨 : IMPORT까기 읽기 

        ' Program : 한줄씩 읽어서 Data 처리하기 
        '    LIMIT를 만나면 Exit 
        '   Word 단위로 분리하여 SL() 에 저장
        '   첫번째 Word = Event Proba , 두번째 Word = Name 

    End Sub

    ' 주어진 이름이 몇번째 XEvents()에 저장되어 있는지 Index Return 
    '   없으면 새로운 XEevent 생성하고 Index Return
    Public Function GetIndexOfEvent(EvtName As String) As Integer

        ' Negate인지 점검
        Dim Negate As Integer = 1
        If (EvtName.Substring(0, 1) = "-") Then
            Negate = -1
            EvtName = EvtName.Substring(1)
        End If

        ' 기존에 Xevents()에 저장된 것이면 찾아서 Index를 Return --
        For i = 2 To XEvents.Count - 1
            If (EvtName = XEvents(i).Name) Then
                Return i * Negate       ' Index 를 Return
            End If
        Next

        ' 없으면 새로운 Event 생성하고 Index를 Return --
        Dim E1 As New cEvent_Def    ' 생성
        E1.Name = EvtName
        'E1.Child = New List(Of Integer)    ‘ cEvent_Def 생성시 초기화 처리함 -> 여기서는 불필요
        XEvents.Add(E1)         ' XEvents에 추가
        Return ((XEvents.Count - 1) * Negate)   ' Index 를 Return

    End Function

    ' FT 내용 Print 
    Public Sub PrintFT()

        Dim S As String = ""        ' FT 내용을 기록할 String
        Dim e, n As Integer     ' e : 하나의 Child, n : Child 수
        Dim nEvent As Integer = XEvents.Count   ' Event 수

        S = "-- FT ---- "

        For i = 2 To nEvent - 1
            n = XEvents(i).Child.Count
            If (n > 0) Then
                S += vbCrLf & XEvents(i).Name & " | " & i.ToString & vbTab & XEvents(i).Type
                For j = 0 To n - 1
                    e = XEvents(i).Child(j)
                    If (e > 0) Then
                        S += vbTab & XEvents(e).Name & " | " & e
                    Else
                        S += vbTab & "-" & XEvents(-e).Name & " | " & e
                    End If
                Next
            Else
                S += vbCrLf & XEvents(i).Name & " | " & i.ToString & vbTab & XEvents(i).Proba
            End If
        Next

        Message.AddAndShow(S)

    End Sub

End Class
