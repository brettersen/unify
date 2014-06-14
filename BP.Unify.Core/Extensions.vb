Imports System.Runtime.CompilerServices

Public Module Extensions

    <Extension()>
    Public Function Demote(Of T)(ByVal items As List(Of T), ByVal itemIndex As Integer) As Boolean
        Dim item As T
        If itemIndex < items.Count - 1 Then
            item = items(itemIndex)
            items.RemoveAt(itemIndex)
            items.Insert(itemIndex + 1, item)
            Return True
        Else
            Return False
        End If
    End Function

    <Extension()>
    Public Function Promote(Of T)(ByVal items As List(Of T), ByVal itemIndex As Integer) As Boolean
        Dim item As T
        If itemIndex > 0 Then
            item = items(itemIndex)
            items.RemoveAt(itemIndex)
            items.Insert(itemIndex - 1, item)
            Return True
        Else
            Return False
        End If
    End Function

End Module


