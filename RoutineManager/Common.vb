Public Module Common

    Public Enum FormMode
        Adding
        Editing
        Viewing
    End Enum

    Public Sub LoadComboBox(dataSource As Object, displayMember As String, valueMember As String)

        Dim table As New DataTable
        Dim row As DataRow

        With table
            .Columns.Add("Value")
            .Columns.Add("Display")
            .Rows.Add(New Object() {Space(0), Space(0)})
        End With



        'With cboEntity
        '    .DataSource = ExemptionEntity.GetEntities()
        '    .DisplayMember = "EntityName"
        '    .ValueMember = "ExemptionEntityId"
        'End With

    End Sub

End Module
