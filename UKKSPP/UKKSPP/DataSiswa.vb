Public Class DataSiswa
    Sub awal()
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        ComboBox1.Enabled = False
        ComboBox2.Enabled = False
        RichTextBox1.Enabled = False
        TextBox3.Enabled = False
        Button6.Enabled = True
        Button7.Enabled = False
        Button8.Enabled = False
        Button9.Enabled = False
        Button10.Enabled = False
    End Sub
    Sub tampildata()
        Call Koneksi()
        Da = New SqlClient.SqlDataAdapter("select * from siswa", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "siswa")
        DataGridView1.DataSource = Ds.Tables("siswa")
        DataGridView1.ReadOnly = True
    End Sub
    Sub bersih()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        ComboBox1.Text = ""
        ComboBox2.Text = ""
        RichTextBox1.Text = ""
    End Sub
    Sub tampilcombo()
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from kelas", Conn)
        Dr = Cmd.ExecuteReader
        While Dr.Read
            ComboBox1.Items.Add(Dr("NamaKelas"))
        End While
    End Sub
    Private Sub DataSiswa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label5.Text = Today.ToString("dd/MMM/yyyy")
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from petugas where Username='" & Form1.TextBox1.Text & "'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            Label2.Text = Dr.Item("Nama_petugas")
        End If
        Call awal()
        Call tampildata()
        Call tampilcombo()
        Label16.Visible = False
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label6.Text = TimeOfDay.ToString("T")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        ComboBox1.Enabled = True
        ComboBox2.Enabled = True
        RichTextBox1.Enabled = True
        TextBox3.Enabled = True
        Button6.Enabled = False
        Button7.Enabled = False
        Button8.Enabled = False
        Button9.Enabled = True
        Button10.Enabled = True
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from siswa where Nisn='" & TextBox1.Text & "' or NoTelp='" & TextBox3.Text & "'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            MsgBox("Nisn dan Notlp tidak boleh sama")
        ElseIf TextBox1.TextLength < 10 Then
            MsgBox("nisn kurang")
        ElseIf TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Or RichTextBox1.Text = "" Or TextBox3.Text = "" Then
            MsgBox("data harus lengkap")
        Else
            Call Koneksi()
            Dim i As String = "insert into siswa values('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & ComboBox1.Text & "', '" & RichTextBox1.Text & "', '" & TextBox3.Text & "','" & ComboBox2.Text & "')"
            Cmd = New SqlClient.SqlCommand(i, Conn)
            Cmd.ExecuteNonQuery()
            MsgBox("data berhasil disimpan")
            Call tampildata()
            Call bersih()
            Call awal()
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBox1.Enabled = False
        TextBox2.Enabled = True
        ComboBox1.Enabled = True
        ComboBox2.Enabled = True
        RichTextBox1.Enabled = True
        TextBox3.Enabled = True
        Button6.Enabled = False
        Button7.Enabled = True
        Button8.Enabled = True
        Button9.Enabled = False
        Button10.Enabled = True
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index

        TextBox1.Text = DataGridView1.Item(0, i).Value
        TextBox2.Text = DataGridView1.Item(1, i).Value
        ComboBox1.Text = DataGridView1.Item(2, i).Value
        RichTextBox1.Text = DataGridView1.Item(3, i).Value
        TextBox3.Text = DataGridView1.Item(4, i).Value
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        If TextBox1.TextLength < 10 Then
            MsgBox("nisn kurang")
        ElseIf TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Or RichTextBox1.Text = "" Or TextBox3.Text = "" Then
            MsgBox("data harus lengkap")
        Else
            Call Koneksi()
            Dim u As String = "update siswa set Nama='" & TextBox2.Text & "', Kelas='" & ComboBox1.Text & "', Alamat='" & RichTextBox1.Text & "', NoTelp='" & TextBox3.Text & "', Status='" & ComboBox2.Text & "' where Nisn='" & TextBox1.Text & "' "
            Cmd = New SqlClient.SqlCommand(u, Conn)
            Cmd.ExecuteNonQuery()
            MsgBox("data berhasil diubah")
            Call tampildata()
            Call awal()
            Cmd = New SqlClient.SqlCommand("select * from pembayaran where Nisn='" & TextBox1.Text & "' and JmlhBulan='" & Label16.Text & "'", Conn)
            Dr = Cmd.ExecuteReader
            Dr.Read()
            If Not Dr.HasRows Then
                MsgBox("Lunasi Pembayaran untuk mengambil ijazah")
                Call bersih()
            Else
                MsgBox("silahkan mengambil ijazah")
                Call bersih()
            End If
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If MessageBox.Show("hapus data?", "info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            Call Koneksi()
            Cmd = New SqlClient.SqlCommand("delete from siswa where Nisn='" & TextBox1.Text & "'", Conn)
            Cmd.ExecuteNonQuery()
            MsgBox("data berhasil dihapus")
            Call tampildata()
            Call bersih()
            Call awal()
        End If
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Call bersih()
        Call awal()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        MainForm.Show()
        Me.Hide()
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from siswa where Nisn like '%" & TextBox4.Text & "%' or Nama like '%" & TextBox4.Text & "%'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            Call Koneksi()
            Da = New SqlClient.SqlDataAdapter("select * from siswa where Nisn like '%" & TextBox4.Text & "%' or Nama like '%" & TextBox4.Text & "%'", Conn)
            Ds = New DataSet
            Da.Fill(Ds, "ketemu")
            DataGridView1.DataSource = Ds.Tables("ketemu")
            DataGridView1.ReadOnly = True
        ElseIf TextBox4.Text = "" Then
            Call tampildata()
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
        Form1.Show()
        Form1.TextBox1.Text = ""
        Form1.TextBox2.Text = ""
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DataPetugas.Show()
        Me.Hide()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        DataKelas.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Pembayaran.Show()
        Me.Hide()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        RiwayatPembayaran.Show()
        Me.Close()
    End Sub
End Class