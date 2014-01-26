Public Class frmExemption

    Private _entryMode As FormEntryMode
    Private _exemption As SyncTaskExemption

    Public Sub New(ByVal entryMode As FormEntryMode, Optional ByVal exemption As SyncTaskExemption = Nothing)
        InitializeComponent()
        Me.EntryMode = entryMode
        Me.Exemption = exemption
        If Me.Exemption IsNot Nothing Then
            Populate()
        Else
            Me.Exemption = New SyncTaskExemption()
        End If
    End Sub

#Region "PROPERTIES"

    Public Property EntryMode As FormEntryMode
        Get
            Return _entryMode
        End Get
        Set(value As FormEntryMode)
            Select Case value
                Case FormEntryMode.Add
                    Me.Text = "Add Exemption"
                Case FormEntryMode.Edit
                    Me.Text = "Edit Exemption"
            End Select
            _entryMode = value
        End Set
    End Property

    Public Property Exemption As SyncTaskExemption
        Get
            Return _exemption
        End Get
        Set(value As SyncTaskExemption)
            _exemption = value
        End Set
    End Property

#End Region

#Region "METHODS"

    Private Sub Populate()

        With Me.Exemption
            cboEntity.SelectedValue = .Entity
            cboOperator.SelectedValue = .Operator
            txtValue.Text = .Value
        End With

    End Sub

    Private Function Scrape() As Boolean

        Dim value As String

        If cboEntity.SelectedIndex < 0 Then
            ShowError("An exemption entity must be selected.", "Add Exemption")
            Return False
        End If

        If cboOperator.SelectedIndex < 0 Then
            ShowError("An exemption operator must be selected.", "Add Exemption")
            Return False
        End If

        value = txtValue.Text.Trim()
        If value.Length = 0 Then
            ShowError("An exemption value must be provided.", "Add Exemption")
            Return False
        End If

        Me.Exemption = New SyncTaskExemption(CType(cboEntity.SelectedItem, KeyValuePair(Of ExemptionEntity, String)).Key, _
                                             CType(cboOperator.SelectedItem, KeyValuePair(Of ExemptionOperator, String)).Key, _
                                             txtValue.Text.Trim())

        Return True

    End Function

#End Region

#Region "EVENTS"

    Private Sub frmExemption_Load(sender As Object, e As EventArgs) Handles Me.Load
        With cboEntity
            .DataSource = New BindingSource(EXEMPTION_ENTITIES, Nothing)
            .DisplayMember = "Value"
            .ValueMember = "Key"
        End With
    End Sub

    Private Sub cboEntity_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEntity.SelectedIndexChanged
        Dim selectedEntity As ExemptionEntity

        If cboEntity.SelectedIndex > -1 Then
            selectedEntity = CType(cboEntity.SelectedItem, KeyValuePair(Of ExemptionEntity, String)).Key
            With cboOperator
                .DataSource = New BindingSource(GetExemptionOperators(selectedEntity), Nothing)
                .DisplayMember = "Value"
                .ValueMember = "Key"
            End With
        End If
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If Scrape() Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
    End Sub

#End Region

End Class