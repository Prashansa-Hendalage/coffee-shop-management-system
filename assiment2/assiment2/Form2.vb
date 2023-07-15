Imports System.Data.SqlClient
Imports System.Drawing.Printing
Public Class frmFrom2
    Dim constr As String = "Data Source=DESKTOP-0R7GUUA;Initial Catalog=CoffeeShop;Integrated Security=True"
    Dim con As SqlConnection()
    Dim cmd As SqlCommand
    Dim adapter = New SqlDataAdapter
    Dim dr As SqlDataReader
    Public RefNo As String


    Sub iExit()
        Dim iExit As DialogResult
        iExit = MessageBox.Show("Do you want to exit ?", "Coffe Bloom", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If (iExit = DialogResult.Yes) Then
            Application.Exit()
        End If
    End Sub


    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        iExit()
    End Sub

    Sub iClear()
        For Each cbo In {cboQAmericano, cboQCaféLatte, cboQCaféMochas, cboQCappliccno, cboQClassicMojito, cboQEspresso,
               cboQEspressoDoppo, cboQIcedAmericano, cboQIcedCaféMocha, cboQIcedLatte, cboQIrishCoffee, cboQTiramisuIcedLatte}
            cbo.Text = "0"
            cbo.Enabled = False
        Next
        For Each chk In {chkAmericano, chkcaféLatte, chkCaféMochas, chkCappliccno, chkClassicMojito, chkEspresso,
                       chkEspressoDoppo, chkIcedAmericano, chkIcedCaféMocha, chkIcedLatte, chkIrishCoffee, chkTiramisuIcedLatte}
            chk.Checked = False
        Next
        For Each lbl In {lblDisplayCostCBeverages, lblDisplayCostHBeverages, lblDisplayDiscount, lblDisplayTotal, lblDisplaySubTotal}
            lbl.Text = "                    "
        Next
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        getData()
        AutoID()
        Dim iReset As DialogResult
        iReset = MessageBox.Show("Do you want to reset ?", "Coffe Bloom", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        iClear()
        rtReceipt.Clear()
    End Sub

    Private Sub frmFrom2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getData()
        AutoID()
        lblDate.Text = DateTime.Now.ToLongDateString
        Timer1.Start()
        iClear()
        rtReceipt.Clear()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblTime.Text = DateTime.Now.ToLongTimeString
    End Sub

    Sub GenerateTotal()
        Dim EachCost As New Beverages
        EachCost.Americano = EachCost.pAmericano * Val(cboQAmericano.Text)
        EachCost.CaféLatte = EachCost.pCaféLatte * Val(cboQCaféLatte.Text)
        EachCost.CaféMochas = EachCost.pCaféMochas * Val(cboQCaféMochas.Text)
        EachCost.Cappliccno = EachCost.pCappliccno * Val(cboQCappliccno.Text)
        EachCost.ClassicMojito = EachCost.pClassicMojito * Val(cboQClassicMojito.Text)
        EachCost.Espresso = EachCost.pEspresso * Val(cboQEspresso.Text)
        EachCost.EspressoDoppo = EachCost.pEspressoDoppo * Val(cboQEspressoDoppo.Text)
        EachCost.IcedAmericano = EachCost.pIcedAmericano * Val(cboQIcedAmericano.Text)
        EachCost.IcedCaféMocha = EachCost.pIcedCaféMocha * Val(cboQIcedCaféMocha.Text)
        EachCost.IcedLatte = EachCost.pIcedLatte * Val(cboQIcedLatte.Text)
        EachCost.IrishCoffee = EachCost.pIrishCoffee * Val(cboQIrishCoffee.Text)
        EachCost.TiramisuIcedLatte = EachCost.pTiramisuIcedLatte * Val(cboQTiramisuIcedLatte.Text)

        Dim iCostHBeverages As Integer = EachCost.GetCostHotBeverages
        Dim iCostCBeverages As Integer = EachCost.GetCostColdBeverages
        Dim iHCtot As Integer = EachCost.GetHCtot

        lblDisplayCostHBeverages.Text = (iCostHBeverages)
        lblDisplayCostCBeverages.Text = (iCostCBeverages)
        lblDisplaySubTotal.Text = (iHCtot)

        'Discount
        Dim Discount, Total, ServiceCharge As Integer
        If (iHCtot > 25000) Then
            Discount = 3000
        ElseIf (iHCtot > 10000) Then
            Discount = 1000
        Else
            Discount = 0

        End If
        lblDisplayDiscount.Text = Discount

        If (iHCtot > 0) Then
            ServiceCharge = 200
        Else
            ServiceCharge = 0
        End If
        lblDisplayServiceCharge.Text = ServiceCharge
        Total = iHCtot + ServiceCharge - Discount
        lblDisplayTotal.Text = Total

    End Sub
    Private Sub btnTotal_Click(sender As Object, e As EventArgs) Handles btnTotal.Click
        GenerateTotal()

    End Sub

    'Only enable to enter value when user click on the checkboxes
    Private Sub chkEspresso_CheckedChanged(sender As Object, e As EventArgs) Handles chkEspresso.CheckedChanged
        If (chkEspresso.Checked = True) Then
            cboQEspresso.Text = ""
            cboQEspresso.Enabled = True
            cboQEspresso.Focus()

        Else
            cboQEspresso.Text = "0"
            cboQEspresso.Enabled = False
        End If
    End Sub

    Private Sub chkEspressoDoppo_CheckedChanged(sender As Object, e As EventArgs) Handles chkEspressoDoppo.CheckedChanged
        If (chkEspressoDoppo.Checked = True) Then
            cboQEspressoDoppo.Text = ""
            cboQEspressoDoppo.Enabled = True
            cboQEspressoDoppo.Focus()

        Else
            cboQEspressoDoppo.Text = "0"
            cboQEspressoDoppo.Enabled = False
        End If
    End Sub

    Private Sub chkAmericano_CheckedChanged(sender As Object, e As EventArgs) Handles chkAmericano.CheckedChanged
        If (chkAmericano.Checked = True) Then
            cboQAmericano.Text = ""
            cboQAmericano.Enabled = True
            cboQAmericano.Focus()

        Else
            cboQAmericano.Text = "0"
            cboQAmericano.Enabled = False
        End If
    End Sub
    Private Sub chkCappliccno_CheckedChanged(sender As Object, e As EventArgs) Handles chkCappliccno.CheckedChanged
        If (chkCappliccno.Checked = True) Then
            cboQCappliccno.Text = ""
            cboQCappliccno.Enabled = True
            cboQCappliccno.Focus()

        Else
            cboQCappliccno.Text = "0"
            cboQCappliccno.Enabled = False
        End If
    End Sub

    Private Sub chkCaféMochas_CheckedChanged(sender As Object, e As EventArgs) Handles chkCaféMochas.CheckedChanged
        If (chkCaféMochas.Checked = True) Then
            cboQCaféMochas.Text = ""
            cboQCaféMochas.Enabled = True
            cboQCaféMochas.Focus()

        Else
            cboQCaféMochas.Text = "0"
            cboQCaféMochas.Enabled = False
        End If
    End Sub

    Private Sub chkcaféLatte_CheckedChanged(sender As Object, e As EventArgs) Handles chkcaféLatte.CheckedChanged
        If (chkcaféLatte.Checked = True) Then
            cboQCaféLatte.Text = ""
            cboQCaféLatte.Enabled = True
            cboQCaféLatte.Focus()

        Else
            cboQCaféLatte.Text = "0"
            cboQCaféLatte.Enabled = False
        End If
    End Sub

    Private Sub chkIcedAmericano_CheckedChanged(sender As Object, e As EventArgs) Handles chkIcedAmericano.CheckedChanged
        If (chkIcedAmericano.Checked = True) Then
            cboQIcedAmericano.Text = ""
            cboQIcedAmericano.Enabled = True
            cboQIcedAmericano.Focus()

        Else
            cboQIcedAmericano.Text = "0"
            cboQIcedAmericano.Enabled = False
        End If
    End Sub

    Private Sub chkIcedCaféMocha_CheckedChanged(sender As Object, e As EventArgs) Handles chkIcedCaféMocha.CheckedChanged
        If (chkIcedCaféMocha.Checked = True) Then
            cboQIcedCaféMocha.Text = ""
            cboQIcedCaféMocha.Enabled = True
            cboQIcedCaféMocha.Focus()

        Else
            cboQIcedCaféMocha.Text = "0"
            cboQIcedCaféMocha.Enabled = False
        End If
    End Sub

    Private Sub chkIcedLatte_CheckedChanged(sender As Object, e As EventArgs) Handles chkIcedLatte.CheckedChanged
        If (chkIcedLatte.Checked = True) Then
            cboQIcedLatte.Text = ""
            cboQIcedLatte.Enabled = True
            cboQIcedLatte.Focus()

        Else
            cboQIcedLatte.Text = "0"
            cboQIcedLatte.Enabled = False
        End If
    End Sub

    Private Sub chkTiramisuIcedLatte_CheckedChanged(sender As Object, e As EventArgs) Handles chkTiramisuIcedLatte.CheckedChanged
        If (chkTiramisuIcedLatte.Checked = True) Then
            cboQTiramisuIcedLatte.Text = ""
            cboQTiramisuIcedLatte.Enabled = True
            cboQTiramisuIcedLatte.Focus()

        Else
            cboQTiramisuIcedLatte.Text = "0"
            cboQTiramisuIcedLatte.Enabled = False
        End If
    End Sub

    Private Sub chkIrishCoffee_CheckedChanged(sender As Object, e As EventArgs) Handles chkIrishCoffee.CheckedChanged
        If (chkIrishCoffee.Checked = True) Then
            cboQIrishCoffee.Text = ""
            cboQIrishCoffee.Enabled = True
            cboQIrishCoffee.Focus()

        Else
            cboQIrishCoffee.Text = "0"
            cboQIrishCoffee.Enabled = False
        End If
    End Sub

    Private Sub chkClassicMojito_CheckedChanged(sender As Object, e As EventArgs) Handles chkClassicMojito.CheckedChanged
        If (chkClassicMojito.Checked = True) Then
            cboQClassicMojito.Text = ""
            cboQClassicMojito.Enabled = True
            cboQClassicMojito.Focus()

        Else
            cboQClassicMojito.Text = "0"
            cboQClassicMojito.Enabled = False
        End If
    End Sub

    'enable to only add numbers
    Private Sub Numbers_Only(sender As Object, e As KeyPressEventArgs) Handles cboQTiramisuIcedLatte.KeyPress, cboQIrishCoffee.KeyPress, cboQIcedLatte.KeyPress,
        cboQIcedCaféMocha.KeyPress, cboQIcedAmericano.KeyPress, cboQEspressoDoppo.KeyPress, cboQEspresso.KeyPress, cboQClassicMojito.KeyPress,
        cboQCappliccno.KeyPress, cboQCaféMochas.KeyPress, cboQCaféLatte.KeyPress, cboQAmericano.KeyPress
        If Char.IsDigit(e.KeyChar) = False And Char.IsControl(e.KeyChar) = False Then
            e.Handled = True
            MsgBox("Please enter valid number", vbExclamation, " Error!")
        End If
    End Sub

    'Handle the error when user clicked botton but didnot enter any value
    Private Sub Enter_Number(sender As Object, e As EventArgs) Handles cboQTiramisuIcedLatte.Validated, cboQIrishCoffee.Validated,
        cboQIcedLatte.Validated, cboQIcedCaféMocha.Validated, cboQIcedAmericano.Validated, cboQEspressoDoppo.Validated, cboQEspresso.Validated,
        cboQClassicMojito.Validated, cboQCappliccno.Validated, cboQCaféMochas.Validated, cboQCaféLatte.Validated, cboQAmericano.Validated
        Dim cbo As ComboBox = sender
        If (cbo.Text = "") Then
            cbo.Text = "0"
        End If
    End Sub

    Sub getData()
        Using con = New SqlConnection(constr)
            con.Open()
            Dim sql As String = "SELECT* From Table_1"
            cmd = New SqlCommand(sql, con)
            adapter = New SqlDataAdapter(cmd)
            Dim ds As New DataSet
            adapter.Fill(ds)
            dgvSale.DataSource = ds.Tables(0)
            con.Close()
        End Using
    End Sub

    Sub AutoID()
        Using con = New SqlConnection(constr)

            con.Open()
            Dim sql As String = "SELECT TOP 1 Id FROM Table_1 ORDER BY Id DESC"
            cmd = New SqlCommand(sql, con)
            adapter = New SqlDataAdapter(cmd)
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection)

            If (dr.Read) = True Then
                Dim id As Integer
                id = (dr(0) + 1)
                RefNo = id.ToString("00000")
            ElseIf IsDBNull(dr) Then
                RefNo = ("00001")
            Else
                RefNo = ("00001")
            End If
            lblDisplayRef.Text = RefNo

            'Dim ds As New DataSet
            'adapter.Fill(ds)

            'If (ds.Tables(0).Rows.Count > 0) Then
            'Dim tmp_id = ds.Tables(0).Rows(0)("Ref_No").ToString().Substring(3, 7)
            'Dim new_id As Integer = CInt(tmp_id) + 1
            'lblDisplayRef.Text = "INV" & new_id.ToString("0000000")
            'Else
            'lblDisplayRef.Text = "INV0000001"
            'End If


            con.Close()

        End Using

    End Sub
    Sub Receipt()

        '---------------------Receipt------------------------
        rtReceipt.AppendText(vbTab + vbTab + "Coffee Bloom" + vbNewLine)
        rtReceipt.AppendText(vbTab + "210-110 High Street - Negombo" + vbNewLine)
        rtReceipt.AppendText(vbTab + "  " + lblDate.Text + vbNewLine)
        rtReceipt.AppendText(vbTab + vbTab + lblTime.Text + vbNewLine)
        rtReceipt.AppendText(vbTab + vbTab + "Ref.No:" + lblDisplayRef.Text + vbNewLine)
        rtReceipt.AppendText("------------------------------------------------------------------" + vbNewLine)
        rtReceipt.AppendText("-----Hot Beverages-----------------------------------------" + vbNewLine)
        rtReceipt.AppendText("Espresso" + vbTab + vbTab + vbTab + "Rs 350 X " + cboQEspresso.Text + vbNewLine)
        rtReceipt.AppendText("Espresso Doppo" + vbTab + vbTab + vbTab + "Rs 400 X " + cboQEspressoDoppo.Text + vbNewLine)
        rtReceipt.AppendText("Americano" + vbTab + vbTab + vbTab + "Rs 450 X " + cboQAmericano.Text + vbNewLine)
        rtReceipt.AppendText("Cappliccno" + vbTab + vbTab + vbTab + "Rs 480 X " + cboQCappliccno.Text + vbNewLine)
        rtReceipt.AppendText("CaféMochas" + vbTab + vbTab + vbTab + "Rs 550 X " + cboQCaféMochas.Text + vbNewLine)
        rtReceipt.AppendText("CaféLatte" + vbTab + vbTab + vbTab + "Rs 520 X " + cboQCaféLatte.Text + vbNewLine)
        rtReceipt.AppendText("-----Cold Beverages---------------------------------------" + vbNewLine)
        rtReceipt.AppendText("Iced Americano" + vbTab + vbTab + vbTab + "Rs 500 X " + cboQIcedAmericano.Text + vbNewLine)
        rtReceipt.AppendText("Iced Café Mocha" + vbTab + vbTab + vbTab + "Rs 550 X " + cboQIcedCaféMocha.Text + vbNewLine)
        rtReceipt.AppendText("Iced Latte" + vbTab + vbTab + vbTab + "Rs 520 X " + cboQIcedLatte.Text + vbNewLine)
        rtReceipt.AppendText("Tiramisu Iced Latte" + vbTab + vbTab + "Rs 550 X " + cboQTiramisuIcedLatte.Text + vbNewLine)
        rtReceipt.AppendText("Irish Coffee" + vbTab + vbTab + vbTab + "Rs 720 X " + cboQIrishCoffee.Text + vbNewLine)
        rtReceipt.AppendText("Classic Mojito" + vbTab + vbTab + vbTab + "Rs 520 X " + cboQClassicMojito.Text + vbNewLine)
        rtReceipt.AppendText("------------------------------------------------------------------" + vbNewLine)
        rtReceipt.AppendText("Sub total" + vbTab + vbTab + vbTab + "Rs " + lblDisplaySubTotal.Text + ".00" + vbNewLine)
        rtReceipt.AppendText("Discount" + vbTab + vbTab + vbTab + "Rs " + lblDisplayDiscount.Text + ".00" + vbNewLine)
        rtReceipt.AppendText("Service Charge" + vbTab + vbTab + vbTab + "Rs " + lblDisplayServiceCharge.Text + ".00" + vbNewLine)
        rtReceipt.AppendText("------------------------------------------------------------------" + vbNewLine)
        rtReceipt.AppendText("------------------------------------------------------------------" + vbNewLine)
        rtReceipt.AppendText("Total" + vbTab + vbTab + vbTab + vbTab + "Rs " + lblDisplayTotal.Text + ".00" + vbNewLine)
        rtReceipt.AppendText(vbNewLine)
        rtReceipt.AppendText(vbTab + vbTab + " Thank You!" + vbNewLine)
    End Sub

    Private Sub btnReceipt_Click(sender As Object, e As EventArgs) Handles btnReceipt.Click
        GenerateTotal()
        Receipt()
        Using con = New SqlConnection(constr)
            con.Open()
            AutoID()
            Dim sql As String = "INSERT INTO Table_1(Ref_No,
S_DateTime,Espresso,EspressoDoppo,Americano,Cappliccno,CaféMochas,
CaféLatte,IcedAmericano,IcedCaféMocha,IcedLatte,TiramisuIcedLatte,IrishCoffee,ClassicMojito,Discount,Total) 
VALUES(@Ref_No,@S_DateTime,@Espresso,@EspressoDoppo,@Americano,@Cappliccno,@CaféMochas,
@CaféLatte,@IcedAmericano,@IcedCaféMocha,@IcedLatte,@TiramisuIcedLatte,@IrishCoffee,@ClassicMojito,@Discount,@Total)"
            cmd = New SqlCommand(sql, con)
            cmd.Parameters.AddWithValue("Ref_No", lblDisplayRef.Text)
            cmd.Parameters.AddWithValue("S_DateTime", Now)
            cmd.Parameters.AddWithValue("Espresso", Val(cboQEspresso.Text))
            cmd.Parameters.AddWithValue("EspressoDoppo", Val(cboQEspressoDoppo.Text))
            cmd.Parameters.AddWithValue("Americano", Val(cboQAmericano.Text))
            cmd.Parameters.AddWithValue("Cappliccno", Val(cboQCappliccno.Text))
            cmd.Parameters.AddWithValue("CaféMochas", Val(cboQCaféMochas.Text))
            cmd.Parameters.AddWithValue("CaféLatte", Val(cboQCaféLatte.Text))
            cmd.Parameters.AddWithValue("IcedAmericano", Val(cboQIcedAmericano.Text))
            cmd.Parameters.AddWithValue("IcedCaféMocha", Val(cboQIcedCaféMocha.Text))
            cmd.Parameters.AddWithValue("IcedLatte", Val(cboQIcedLatte.Text))
            cmd.Parameters.AddWithValue("TiramisuIcedLatte", Val(cboQTiramisuIcedLatte.Text))
            cmd.Parameters.AddWithValue("IrishCoffee", Val(cboQIrishCoffee.Text))
            cmd.Parameters.AddWithValue("ClassicMojito", Val(cboQClassicMojito.Text))
            cmd.Parameters.AddWithValue("Discount", Val(lblDisplayDiscount.Text))
            cmd.Parameters.AddWithValue("Total", Val(lblDisplayTotal.Text))
            cmd.ExecuteNonQuery()
            getData()

            con.Close()
        End Using
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim sql As String = "SELECT * FROM Table_1 WHERE Ref_No='" & txtSearch.Text & "'"
        Using con = New SqlConnection(constr)
            Using cmd = New SqlCommand(sql, con)
                Using adapter = New SqlDataAdapter(cmd)
                    adapter.SelectCommand = cmd
                    Using dt As New DataTable()
                        adapter.Fill(dt)
                        dgvSale.DataSource = dt
                    End Using
                End Using
            End Using
        End Using

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Using con = New SqlConnection(constr)
            Dim iDelete As DialogResult
            iDelete = MessageBox.Show("Do you want to Delete it ?", "Coffe Bloom", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
            If (iDelete = DialogResult.Yes) Then
                cmd = New SqlCommand("DELETE FROM [dbo].[Table_1] WHERE [Ref_No]='" + txtSearch.Text + "'", con)
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End If
        End Using
    End Sub

    Private Sub btnIExit_Click(sender As Object, e As EventArgs) Handles btnIExit.Click
        iExit()

    End Sub

    Private Sub btniReset_Click(sender As Object, e As EventArgs) Handles btniRest.Click
        getData()
        txtSearch.Text = ""
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        Using con = New SqlConnection(constr)
            cmd = New SqlCommand("UPDATE [dbo].[Table_1] SET [Espresso]='" + cboQEspresso.Text + "',[EspressoDoppo]='" + cboQEspressoDoppo.Text + "',[Americano]='" + cboQAmericano.Text + "',
                [Cappliccno]='" + cboQCappliccno.Text + "',[CaféMochas]='" + cboQCaféMochas.Text + "',
                [CaféLatte]='" + cboQCaféLatte.Text + "',[IcedAmericano]='" + cboQIcedAmericano.Text + "',[IcedCaféMocha]='" + cboQIcedCaféMocha.Text + "',
                [IcedLatte]='" + cboQIcedLatte.Text + "',[TiramisuIcedLatte]='" + cboQTiramisuIcedLatte.Text + "',
                [IrishCoffee]='" + cboQIrishCoffee.Text + "',[ClassicMojito]='" + cboQClassicMojito.Text + "',[Discount]='" + lblDisplayDiscount.Text + "',
                [Total]='" + lblDisplayTotal.Text + "'
                   WHERE Ref_No='" & txtSearch.Text & "'", con)
            con.Open()
            TabPage1.Show()
            cmd.ExecuteNonQuery()
            con.Close()
        End Using
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Dim PrintDoc As New PrintDocument
        AddHandler PrintDoc.PrintPage, AddressOf Me.PrintText
        PrintDoc.Print()
        PrintPreviewDialog1.ShowDialog()
    End Sub
    Private Sub PrintText(ByVal sender As Object, ByVal ev As PrintPageEventArgs)
        ev.Graphics.DrawString(rtReceipt.Text, New Font("Arial", 22, FontStyle.Regular), Brushes.Black, New Point(50, 12))

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        e.Graphics.DrawString(rtReceipt.Text, New Font("Arial", 22, FontStyle.Regular), Brushes.Black, New Point(50, 12))
    End Sub
End Class