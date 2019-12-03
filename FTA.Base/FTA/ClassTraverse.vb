Public Class ClassTraverse

    Dim FT As ClassFtData

    Public Sub New(FtData As ClassFtData)
        FT = FtData     ' Class 생성시 외부에서 읽은 FT 연결
    End Sub

    Public Sub TraverseFT1(Top As String)

        ' Program : Top에 대한 Event Index 가져오기 

        Message.Add("Traverse FT -----")

        ' Program : Top Index로부터 시작하여 FT 전체를 Traverse하기 
        '   Traverse하는 Gate 및 Event 출력하기 : Message.Add (Gate/Event 이름) 이용

        Message.Show()  ' Message 출력하기
    End Sub

    Public Sub TraverseFT2(Top As String)

        ' Program : Top에 대한 Event Index 가져오기 

        Message.Add("Traverse FT -----")

        ' Program : Top Index로부터 시작하여 FT 전체를 Traverse하기 
        '   한번 방문한 Gate는 다시 방문하지 않기 (이를 위해서는 Array 선언하여 이용하는 기법 필요) 
        '   Traverse하는 Gate 및 Event 출력하기 : Message.Add (Gate/Event 이름) 이용

        Message.Show()  ' Message 출력하기

    End Sub


    ' 같은 Gate, Event는 나타나지 않는다고 가정하고 Top Event 값 구하기
    Public Sub Calcualte_Top(Top As String)

        ' Program : Top에 대한 Event Index 가져오기 

        ' Program : Top Index로부터 시작하여 FT 전체를 Traverse하기 
        '   한번 방문한 Gate는 다시 방문하지 않기 (이를 위해서는 Array 선언하여 이용하는 기법 필요) 

        Message.Add("Calculate Top -----")

        ' Program : Top Index로부터 시작하여 FT 전체를 Traverse하면서 Gate 값 구하기 
        '   한번 방문한 Gate는 다시 방문하지 않기 (이를 위해서는 Array 선언하여 이용하는 기법 필요) 
        '   계산된 값 출력

        Message.Show()

    End Sub

End Class
