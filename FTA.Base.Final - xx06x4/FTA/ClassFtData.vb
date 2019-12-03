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
        Call ReadFtBlock(strReader) ' Read FT Block
        Call ReadDataBlock(strReader)   ' Read Data Block

        strReader.Close()   ' File 닫기

    End Sub

    ' Program : FT Block 읽기 (아래 완성하기)
    Private Sub ReadFtBlock(strReader As System.IO.StreamReader)
        Dim aLine As String
        Dim sL() As String, Sep(1) As String
        Dim iGate, iC As Integer
        Sep(0) = " " : Sep(1) = vbTab

        Do While (strReader.Peek() <> -1)   ' 끝이 아니면 계속 읽기

            ' 한줄 읽기
            aLine = strReader.ReadLine

            If (aLine.Substring(0, 1) <> "*") Then  ' * 가 아니면
                If (aLine.ToUpper = "ENDTREE") Then Exit Do ' ENDTREE 까지 읽기

                ' Word 단위로 분리하여 SL() 에 저장
                sL = aLine.Split(Sep, StringSplitOptions.RemoveEmptyEntries)

                ' Name, Type, Child로 분리
                If (sL.Length > 2) Then     ' 적어도 Name, Type, Child 1개는 있어야 처리
                    iGate = GetIndexOfEvent(sL(0))  ' Event Index (New Event면 생성하고 이름 저장)
                    XEvents(iGate).Type = sL(1)          ' Type
                    For j = 2 To sL.Length - 1
                        iC = GetIndexOfEvent(sL(j))
                        XEvents(iGate).Child.Add(iC)        ' Child
                    Next
                End If
            End If
        Loop

    End Sub

    ' Program : Data Block 읽기 (아래 완성하기)
    Private Sub ReadDataBlock(strReader As System.IO.StreamReader)
        Dim aLine As String
        Dim sL() As String, Sep(1) As String
        Dim iE As Integer, pE As Single
        Sep(0) = " " : Sep(1) = vbTab

        Do While (strReader.Peek() <> -1)   ' IMPORT 까지 한줄씩 읽기
            aLine = strReader.ReadLine
            If (aLine.ToUpper = "IMPORT") Then Exit Do
        Loop

        ' Data 읽기
        Do While (strReader.Peek() <> -1)
            aLine = strReader.ReadLine   ' 한줄 읽기
            sL = aLine.Split(Sep, StringSplitOptions.RemoveEmptyEntries)     ' Word 단위로 분리하여 SL() 에 저장

            If (sL(0).ToUpper = "LIMIT") Then Exit Do ' LIMIT 까지 읽기

            ' Event에 대한 Data
            pE = Convert.ToSingle(sL(0))    ' Proba
            iE = GetIndexOfEvent(sL(1))     ' Event Index

            XEvents(iE).Proba = pE
            XEvents(iE).Type = "B"
        Loop

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

        Message.Add("-- FT ---- ")

        For i = 2 To XEvents.Count - 1
            n = XEvents(i).Child.Count
            If (n > 0) Then
                S = XEvents(i).Name & " | " & i.ToString & vbTab & XEvents(i).Type
                For j = 0 To n - 1
                    e = XEvents(i).Child(j)
                    If (e > 0) Then
                        S += vbTab & XEvents(e).Name & " | " & e
                    Else
                        S += vbTab & "-" & XEvents(-e).Name & " | " & e
                    End If
                Next
            Else
                S = XEvents(i).Name & " | " & i.ToString & vbTab & XEvents(i).Proba
            End If
            Message.Add(S)
        Next

        Message.Show()

    End Sub

End Class
