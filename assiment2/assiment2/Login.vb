Imports System.Data
Imports System.Data.SqlClient
Public Class FrmLogin

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        Dim con As SqlConnection = New SqlConnection("Data Source=DESKTOP-0R7GUUA;Initial Catalog=CoffeeShop;Integrated Security=True")
        Dim cmd As SqlCommand = New SqlCommand("Select* from LoggingDetails where username='" + txtUsername.Text + "' and userPassword='" + txtPassword.Text + "'", con)
        Dim sda As SqlDataAdapter = New SqlDataAdapter(cmd)
        Dim dt As DataTable = New DataTable()
        sda.Fill(dt)
        If (dt.Rows.Count > 0) Then
            frmFrom2.Show()
            Me.Hide()
        Else
            MsgBox("Invalid Username or Password!", vbExclamation, " Sorry!")
            txtUsername.Text = ""
            txtPassword.Text = ""

        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        Dim iReset As DialogResult
        iReset = MessageBox.Show("Do you want to reset ?", "Login", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If (iReset = DialogResult.Yes) Then
            txtUsername.Text = ""
            txtPassword.Text = ""
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Dim iExit As DialogResult
        iExit = MessageBox.Show("Do you want to exit ?", "Login", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
        If (iExit = DialogResult.Yes) Then
            Application.Exit()
        End If
    End Sub

    Private Sub chkShow_CheckedChanged(sender As Object, e As EventArgs) Handles chkShow.CheckedChanged
        If (chkShow.Checked = True) Then
            txtPassword.PasswordChar = String.Empty
            chkShow.Image = My.Resources.eyeopen
        ElseIf (chkShow.Checked = False) Then
            txtPassword.PasswordChar = "."
            chkShow.Image = My.Resources.eyeclose

        End If
    End Sub
End Class
