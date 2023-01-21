Public Class RiwayatPembayaran
    Sub siswa()
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from siswa where Nisn='" & LoginSiswa.TextBox1.Text & "'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            Label2.Text = Dr.Item("Nama")
            Button1.Visible = False
            Button2.Visible = False
            Button3.Visible = False
            Button4.Visible = False
            Button6.Visible = False
            TextBox4.Visible = False
        End If
    End Sub
    Private Sub RiwayatPembayaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label5.Text = Today.ToString("dd/MMM/yyyy")
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from petugas where Username='" & Form1.TextBox1.Text & "'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            Label2.Text = Dr.Item("Nama_petugas")
            TextBox4.Visible = True
            TextBox1.Visible = False
            Label7.Visible = False
        End If
        TextBox2.Enabled = False
        Call siswa()
        TextBox3.Enabled = False
        Button6.Visible = False
        TextBox1.Text = LoginSiswa.TextBox1.Text
        TextBox1.Enabled = False
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label6.Text = TimeOfDay.ToString("T")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DataPetugas.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DataSiswa.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        DataKelas.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Pembayaran.Show()
        Me.Hide()
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from pembayaran where Nisn='" & TextBox1.Text & "' and Nisn='" & LoginSiswa.TextBox1.Text & "'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            ErrorProvider1.Clear()
            TextBox2.Text = Dr.Item("NamaSiswa")
            TextBox3.Text = Dr.Item("Kelas")
            Call Koneksi()
            Da = New SqlClient.SqlDataAdapter("select * from pembayaran where Nisn='" & TextBox1.Text & "'", Conn)
            Ds = New DataSet
            Ds.Clear()
            Da.Fill(Ds, "pembayaran")
            DataGridView1.DataSource = Ds.Tables("pembayaran")
        ElseIf TextBox1.Text <> LoginSiswa.TextBox1.Text Then
            ErrorProvider1.SetError(TextBox1, "Bukan Nisn Anda")
            TextBox2.Text = ""
            TextBox3.Text = ""
            DataGridView1.DataSource = Nothing
        ElseIf TextBox1.Text = "" Then
            ErrorProvider1.SetError(TextBox1, "Nisn Kosong")
            TextBox2.Text = ""
            TextBox3.Text = ""
            DataGridView1.DataSource = Nothing
        Else
            ErrorProvider1.SetError(TextBox1, "Siswa belum melakukan pembayaran")
            TextBox2.Text = ""
            TextBox3.Text = ""
            DataGridView1.DataSource = Nothing
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
        Form1.Show()
        Form1.TextBox1.Text = ""
        Form1.TextBox2.Text = ""
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If TextBox1.Text = "" Then
            MsgBox("data kosong")
        Else
            AxCrystalReport1.SelectionFormula = "totext({pembayaran.Nisn})='" & TextBox4.Text & "'"
            AxCrystalReport1.ReportFileName = "LaporanPembayaranSiswa.rpt"
            AxCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized
            AxCrystalReport1.RetrieveDataFiles()
            AxCrystalReport1.Action = 1
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from pembayaran where Nisn='" & TextBox4.Text & "'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            ErrorProvider1.Clear()
            TextBox2.Text = Dr.Item("NamaSiswa")
            TextBox3.Text = Dr.Item("Kelas")
            Call Koneksi()
            Da = New SqlClient.SqlDataAdapter("select * from pembayaran where Nisn='" & TextBox4.Text & "'", Conn)
            Ds = New DataSet
            Ds.Clear()
            Da.Fill(Ds, "pembayaran")
            DataGridView1.DataSource = Ds.Tables("pembayaran")
        ElseIf TextBox1.Text = "" Then
            ErrorProvider1.SetError(TextBox1, "Nisn Kosong")
            TextBox2.Text = ""
            TextBox3.Text = ""
            DataGridView1.DataSource = Nothing
        Else
            ErrorProvider1.SetError(TextBox1, "Siswa belum melakukan pembayaran")
            TextBox2.Text = ""
            TextBox3.Text = ""
            DataGridView1.DataSource = Nothing
        End If
    End Sub

    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If TextBox4.Text = "" Then
            MsgBox("data kosong")
        Else
            AxCrystalReport1.SelectionFormula = "totext({pembayaran.Nisn})='" & TextBox4.Text & "'"
            AxCrystalReport1.ReportFileName = "LaporanPembayaranSiswa.rpt"
            AxCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized
            AxCrystalReport1.RetrieveDataFiles()
            AxCrystalReport1.Action = 1
        End If
    End Sub
End Class