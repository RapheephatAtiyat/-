Public Class Form1
    Class Product
        Public Property ID As String
        Public Property Name As String
        Public Property Price As Double
        Public Property Amount As Double
        Public ReadOnly Property Total As Double
            Get
                Return Price * Amount
            End Get
        End Property
    End Class
    Private products As List(Of Product)
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        products = New List(Of Product)()
        LoadTable()
    End Sub

    Private Sub Shoutdown_Click(sender As Object, e As EventArgs) Handles Shoutdown.Click
        Dim res As MsgBoxResult = MsgBox("คุณต้องการปิดโปรแกรมหรือไม่", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "ปิดโปรแกรม")
        If res = MsgBoxResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub Dropdown_ID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Dropdown_ID.SelectedIndexChanged
        Dim selected As String = Dropdown_ID.SelectedItem.ToString()
        Dim list As New List(Of String) From {"001", "002", "003", "004", "005"}
        If list.Contains(selected) Then
            Select Case selected
                Case "001" : NameP.Text = "ดินสอ" : Price_P.Text = "10.75"
                Case "002" : NameP.Text = "ยางลบ" : Price_P.Text = "5.00"
                Case "003" : NameP.Text = "ไม้บรรทัด" : Price_P.Text = "15.55"
                Case "004" : NameP.Text = "สมุด" : Price_P.Text = "12.00"
                Case "005" : NameP.Text = "ปากกา" : Price_P.Text = "7.25"
            End Select
        End If
    End Sub

    Private Sub InsertData_Click(sender As Object, e As EventArgs) Handles InsertData.Click
        If Dropdown_ID.SelectedIndex <> -1 And Not String.IsNullOrWhiteSpace(AmountP.Text) Then
            Dim selected As String = Dropdown_ID.SelectedItem.ToString()
            Dim IsExits As Product = products.FirstOrDefault(Function(p) p.ID = selected)
            If IsExits IsNot Nothing Then
                IsExits.Amount += Double.Parse(AmountP.Text)
            Else
                Dim raw As New Product With {
                    .ID = selected,
                    .Name = NameP.Text,
                    .Price = Double.Parse(Price_P.Text),
                    .Amount = Double.Parse(AmountP.Text)
                }
                products.Add(raw)
            End If
            LoadTable()
        Else
            MsgBox("กรุณากรอกข้อมูลให้ครบถ้วน", MsgBoxStyle.Exclamation, "เตือน")
        End If
    End Sub
    Private Sub LoadTable()
        DataGridView1.Rows.Clear()
        For Each p In products
            DataGridView1.Rows.Add(p.ID, p.Name, p.Price, p.Amount, p.Total)
        Next
    End Sub

    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedCells.Count > 0 Then
            Dim index As Integer = DataGridView1.SelectedCells(0).RowIndex
            If index >= 0 Then
                Dim data As DataGridViewRow = DataGridView1.Rows(index)
                Dim Price As Double = Convert.ToDouble(data.Cells(2).Value)
                Dim Amount As Double = Convert.ToDouble(data.Cells(3).Value)
                Dim Total As Double = Price * Amount
                Dim VAT As Double = Total * 0.07
                Dim NetPrice As Double = Total * 1.07
                TotalL.Text = Total.ToString("N2")
                VATL.Text = VAT.ToString("N2")
                NetPriceL.Text = NetPrice.ToString("N2")
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim index As Integer = DataGridView1.SelectedCells(0).RowIndex
        If DataGridView1.SelectedCells.Count >= 0 Then
            Dim res As MsgBoxResult = MsgBox("คุณต้องการลบข้อมูลหรือไม่", MsgBoxStyle.Question Or MsgBoxStyle.YesNo, "ลบข้อมูล")
            If res = MsgBoxResult.Yes Then
                DataGridView1.Rows.RemoveAt(index)
            End If
        End If
    End Sub
End Class
