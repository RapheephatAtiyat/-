Public Class Form1
    Public Class Product
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
        LoadTableData()
    End Sub
    Private Sub InsertData_Click(sender As Object, e As EventArgs) Handles InsertData.Click
        If Dropdown_ID.SelectedIndex = -1 Or String.IsNullOrWhiteSpace(NameP.Text) Or String.IsNullOrWhiteSpace(Price_P.Text) Or String.IsNullOrWhiteSpace(AmountP.Text) Then
            MsgBox("กรุณากรอกข้อมูลสินค้าให้ครบถ้วน", MsgBoxStyle.Exclamation, "เตือน")
        Else
            Dim selected As String = Dropdown_ID.SelectedItem.ToString()
            Dim isExist As Product = products.FirstOrDefault(Function(p) p.ID = selected)
            If isExist IsNot Nothing Then
                isExist.Amount += Double.Parse(AmountP.Text)
            Else
                Dim raw As New Product With {
                    .ID = selected,
                    .Name = NameP.Text,
                    .Price = Double.Parse(Price_P.Text),
                    .Amount = Double.Parse(AmountP.Text)
                }
                products.Add(raw)
            End If
            LoadTableData()
        End If
    End Sub
    Private Sub LoadTableData()
        DataGridView1.Rows.Clear()
        For Each product In products
            DataGridView1.Rows.Add(product.ID, product.Name, product.Price, product.Amount, product.Total)
        Next
    End Sub

    Private Sub Dropdown_ID_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Dropdown_ID.SelectedIndexChanged
        If Dropdown_ID.SelectedIndex <> -1 Then
            Dim selected As String = Dropdown_ID.SelectedItem.ToString()
            Select Case selected
                Case "001"
                    NameP.Text = "ปลาเปรี้ยว"
                    Price_P.Text = "50"
                Case "002"
                    NameP.Text = "ผัดเผ็ดหมา"
                    Price_P.Text = "100.50"
                Case "003"
                    NameP.Text = "ไก่ต้ม"
                    Price_P.Text = "20"
                Case Else
                    MsgBox("How!!!!????", MsgBoxStyle.Exclamation, "เตือน")
            End Select
        End If
    End Sub
    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        If DataGridView1.SelectedCells.Count > 0 Then
            Dim index As Integer = DataGridView1.SelectedCells(0).RowIndex
            If index >= 0 Then
                Dim Data As DataGridViewRow = DataGridView1.Rows(index)
                Dim Amount As Double = Convert.ToDouble(Data.Cells(4).Value)
                Dim Price As Double = Convert.ToDouble(Data.Cells(3).Value)
                Dim Total As Double = Price * Amount
                Dim VAT As Double = Total * 0.07
                Dim NetPrice As Double = Total * 1.07
                TotalL.Text = Total.ToString("N2")
                VATL.Text = VAT.ToString("N2")
                NetPriceL.Text = NetPrice.ToString("N2")
            End If
        End If
    End Sub
End Class
