Public Class Form1
    Class Product
        Public Property ID As String
        Public Property Name As String
        Public Property Price As Double
        Public Property Amount As Double
        Public ReadOnly Property Total As Double
            Get
                Return (Price * Amount).ToString("n2")
            End Get
        End Property
    End Class
    Private products As List(Of Product)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        products = New List(Of Product)()
        LoadTable()
    End Sub
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex <> -1 Then
            Dim selected As String = ComboBox1.SelectedItem.ToString()
            Select Case selected
                Case "001" : TBNAME.Text = "ดินสอ" : TBPrice.Text = "7.00"
                Case "002" : TBNAME.Text = "ปากกา" : TBPrice.Text = "12.00"
                Case "003" : TBNAME.Text = "ยางลบ" : TBPrice.Text = "5.75"
                Case "004" : TBNAME.Text = "ไม้บรรทัด" : TBPrice.Text = "10.25"
                Case "005" : TBNAME.Text = "สมุด" : TBPrice.Text = "14.50"
            End Select
        End If
    End Sub

    Private Sub Insert_Click(sender As Object, e As EventArgs) Handles Insert.Click
        If ComboBox1.SelectedIndex <> -1 And Not String.IsNullOrWhiteSpace(TBAmount.Text) And IsNumeric(TBAmount.Text) Then
            Dim slected As String = ComboBox1.SelectedItem.ToString()
            Dim Isexits = products.FirstOrDefault(Function(p) p.ID = slected)
            If Isexits IsNot Nothing Then
                Isexits.Amount += Double.Parse(TBAmount.Text)
            Else
                Dim raw = New Product With {
                    .ID = slected,
                    .Name = TBNAME.Text,
                    .Price = TBPrice.Text,
                    .Amount = TBAmount.Text
                }
                products.Add(raw)
            End If
            LoadTable()
        Else
            MsgBox("กรุณากรอกข้อมูลให้ครบถ้วนแและถูกต้อง", MsgBoxStyle.Exclamation, "เตือน")
        End If
    End Sub

    Private Sub LoadTable()
        DataGridView1.Rows.Clear()
        For Each p In products
            DataGridView1.Rows.Add(p.ID, p.Name, p.Price, p.Amount, p.Total)
        Next
    End Sub

    Private Sub Delete_Click(sender As Object, e As EventArgs) Handles Delete.Click
        If DataGridView1.SelectedCells.Count >= 0 Then
            Try
                Dim index = DataGridView1.SelectedCells(0).RowIndex
                Dim res As MsgBoxResult = MsgBox("คุณต้องการลบข้อมูลหรือไม่?", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "ลบข้อมูล")
                If res = MsgBoxResult.Yes Then
                    DataGridView1.Rows.RemoveAt(index)
                End If
            Catch ex As Exception

            End Try
            LoadTable()
        End If
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedCells.Count > 0 Then
            Dim index As Integer = DataGridView1.SelectedCells(0).RowIndex
            If index >= 0 Then
                Dim data As DataGridViewRow = DataGridView1.Rows(index)
                Dim Total As Double = Convert.ToDouble(data.Cells(4).Value)
                Debug.Print(Total)
                Dim VAT As Double = Total * 0.07
                Dim NetPrice As Double = Total * 1.07
                LBTotal.Text = Total.ToString("n2")
                LBVat.Text = VAT.ToString("n2")
                LBNetPrice.Text = NetPrice.ToString("n2")
            End If
        End If
    End Sub

    Private Sub Shutdown_Click(sender As Object, e As EventArgs) Handles Shutdown.Click
        Dim res As MsgBoxResult = MsgBox("คุณต้องการปิดโปรแกรมหรือไม่", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "ปิดโปรแกรม")
        If res = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub
End Class
