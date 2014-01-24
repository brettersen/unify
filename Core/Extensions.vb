Imports System.Runtime.CompilerServices

Module Extensions

    <Extension()>
    Public Function Demote(Of T)(ByVal itemCollection As Collection(Of T), ByVal itemIndex As Integer) As Boolean
        Dim item As T
        If itemIndex < itemCollection.Count - 1 Then
            item = itemCollection(itemIndex)
            itemCollection.RemoveAt(itemIndex)
            itemCollection.Insert(itemIndex + 1, item)
            Return True
        Else
            Return False
        End If
    End Function

    <Extension()>
    Public Function Promote(Of T)(ByVal itemCollection As Collection(Of T), ByVal itemIndex As Integer) As Boolean
        Dim item As T
        If itemIndex > 0 Then
            item = itemCollection(itemIndex)
            itemCollection.RemoveAt(itemIndex)
            itemCollection.Insert(itemIndex - 1, item)
            Return True
        Else
            Return False
        End If
    End Function

End Module
