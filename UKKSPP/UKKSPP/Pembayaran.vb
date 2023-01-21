Public Class Pembayaran
    Sub awal()
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False
        TextBox5.Enabled = False
        ComboBox1.Enabled = False
        Button6.Enabled = True
        Button7.Enabled = False
        Button8.Enabled = False
        Button9.Enabled = False
        Button10.Enabled = False
        CheckBox1.Enabled = True
        CheckBox2.Enabled = True
        CheckBox3.Enabled = True
        CheckBox4.Enabled = True
        CheckBox5.Enabled = True
        CheckBox6.Enabled = True
        CheckBox7.Enabled = True
        CheckBox8.Enabled = True
        CheckBox9.Enabled = True
        CheckBox10.Enabled = True
        CheckBox11.Enabled = True
        CheckBox12.Enabled = True
    End Sub
    Sub bersih()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        ComboBox1.Text = ""
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        CheckBox4.Checked = False
        CheckBox5.Checked = False
        CheckBox6.Checked = False
        CheckBox7.Checked = False
        CheckBox8.Checked = False
        CheckBox9.Checked = False
        CheckBox10.Checked = False
        CheckBox11.Checked = False
        CheckBox12.Checked = False
        CheckBox1.Enabled = True
        CheckBox2.Enabled = True
        CheckBox3.Enabled = True
        CheckBox4.Enabled = True
        CheckBox5.Enabled = True
        CheckBox6.Enabled = True
        CheckBox7.Enabled = True
        CheckBox8.Enabled = True
        CheckBox9.Enabled = True
        CheckBox10.Enabled = True
        CheckBox11.Enabled = True
        CheckBox12.Enabled = True
    End Sub
    Sub tampildata()
        Call Koneksi()
        Da = New SqlClient.SqlDataAdapter("select * from pembayaran", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "pembayaran")
        DataGridView1.DataSource = Ds.Tables("pembayaran")
    End Sub
    Sub auto()
        Dim angka As Integer
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select max(IDPembayaran) from pembayaran", Conn)
        Cmd.ExecuteNonQuery()
        If IsDBNull(Cmd.ExecuteScalar) Then
            angka = 1
        Else
            angka = Cmd.ExecuteScalar + 1
        End If
    End Sub
    Sub jumlah()
        Dim total As Decimal = 0
        For jml As Integer = 0 To DataGridView1.Rows.Count - 1
            total = total + DataGridView1.Rows(jml).Cells(20).Value
        Next
        Label18.Text = total
    End Sub
    Private Sub Pembayaran_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label5.Text = Today.ToString("dd/MMM/yyyy")
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from petugas where Username='" & Form1.TextBox1.Text & "'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            Label2.Text = Dr.Item("Nama_petugas")
        End If
        TextBox1.Enabled = False
        TextBox3.Enabled = False
        Call awal()
        Call tampildata()
        Me.WindowState = FormWindowState.Maximized
        Call jumlah()
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

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
        Form1.Show()
        Form1.TextBox1.Text = ""
        Form1.TextBox2.Text = ""
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim bj, bf, bm, ba, bmei, bjun, bjul, bag, bs, bok, bn, bd As String
        If CheckBox1.Checked = True Then
            bj = "Lunas"
        Else
            bj = "Belum Lunas"
        End If
        If CheckBox2.Checked = True Then
            bf = "Lunas"
        Else
            bf = "Belum Lunas"
        End If
        If CheckBox3.Checked = True Then
            bm = "Lunas"
        Else
            bm = "Belum Lunas"
        End If
        If CheckBox4.Checked = True Then
            ba = "Lunas"
        Else
            ba = "Belum Lunas"
        End If
        If CheckBox5.Checked = True Then
            bmei = "Lunas"
        Else
            bmei = "Belum Lunas"
        End If
        If CheckBox6.Checked = True Then
            bjun = "Lunas"
        Else
            bjun = "Belum Lunas"
        End If
        If CheckBox7.Checked = True Then
            bjul = "Lunas"
        Else
            bjul = "Belum Lunas"
        End If
        If CheckBox8.Checked = True Then
            bag = "Lunas"
        Else
            bag = "Belum Lunas"
        End If
        If CheckBox9.Checked = True Then
            bs = "Lunas"
        Else
            bs = "Belum Lunas"
        End If
        If CheckBox10.Checked = True Then
            bok = "Lunas"
        Else
            bok = "Belum Lunas"
        End If
        If CheckBox11.Checked = True Then
            bn = "Lunas"
        Else
            bn = "Belum Lunas"
        End If
        If CheckBox12.Checked = True Then
            bd = "Lunas"
        Else
            bd = "Belum Lunas"
        End If
        Dim tgl As Date
        tgl = Format(Now, "dd/MM/yyyy")
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from pembayaran where TahunBayar='" & ComboBox1.Text & "' and Nisn='" & TextBox1.Text & "'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            MsgBox("data sudah ada")
        ElseIf TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Or TextBox5.Text = "" Then
            MsgBox("data harus lengkap")
        Else
            Call Koneksi()
            Call auto()
            Cmd = New SqlClient.SqlCommand("insert into pembayaran(Petugas, Nisn, NamaSiswa, Kelas, TanggalBayar, JmlhBulan, Januari, Februari, Maret, April, Mei, Juni, Juli, Agustus, September, Oktober, November, Desember, TahunBayar, JumlahBayar) values
                                      ('" & Label2.Text & "', '" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox3.Text & "', '" & Format(Now, "yyyy/MM/dd") & "', '" & Convert.ToInt32(TextBox5.Text) & "', '" & bj & "', '" & bf & "', '" & bm & "', '" & ba & "', '" & bmei & "', '" & bjun & "', '" & bjul & "', '" & bag & "', '" & bs & "', '" & bok & "', '" & bn & "', '" & bd & "', '" & ComboBox1.Text & "', '" & TextBox4.Text & "')", Conn)
            Cmd.ExecuteNonQuery()
            MsgBox("data disimpan")
            Call tampildata()
            Call bersih()
            Call awal()
        End If
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from siswa where Nama='" & TextBox2.Text & "'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            TextBox1.Text = Dr.Item("Nisn")
            TextBox3.Text = Dr.Item("Kelas")
        End If
    End Sub

    Private Sub TextBox5_TextChanged(sender As Object, e As EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text = "" Then
            TextBox4.Text = ""
        Else
            TextBox4.Text = TextBox5.Text * 150000
        End If
    End Sub

    Private Sub TextBox5_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox5.KeyPress
        If Not ((e.KeyChar >= "" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then e.Handled = True
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Call bersih()
        Call awal()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        TextBox4.Enabled = True
        TextBox5.Enabled = True
        ComboBox1.Enabled = True
        Button6.Enabled = False
        Button7.Enabled = False
        Button8.Enabled = False
        Button9.Enabled = True
        Button10.Enabled = True
        Button13.Enabled = True
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = True
        TextBox5.Enabled = False
        ComboBox1.Enabled = True
        Button6.Enabled = False
        Button7.Enabled = True
        Button8.Enabled = True
        Button9.Enabled = False
        Button10.Enabled = True
        Button13.Enabled = False

        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        TextBox1.Text = DataGridView1.Item(2, i).Value
        TextBox2.Text = DataGridView1.Item(3, i).Value
        TextBox3.Text = DataGridView1.Item(4, i).Value
        Dim ket As String
        ket = "Lunas"
        If ket = DataGridView1.Item(7, i).Value Then
            CheckBox1.Checked = True
            CheckBox1.Enabled = False
        Else
            CheckBox1.Checked = False

        End If
        If ket = DataGridView1.Item(8, i).Value Then
            CheckBox2.Checked = True
            CheckBox2.Enabled = False
        Else
            CheckBox2.Checked = False
        End If
        If ket = DataGridView1.Item(9, i).Value Then
            CheckBox3.Checked = True
            CheckBox3.Enabled = False
        Else
            CheckBox3.Checked = False
        End If
        If ket = DataGridView1.Item(10, i).Value Then
            CheckBox4.Checked = True
            CheckBox4.Enabled = False
        Else
            CheckBox4.Checked = False
        End If
        If ket = DataGridView1.Item(11, i).Value Then
            CheckBox5.Checked = True
            CheckBox5.Enabled = False
        Else
            CheckBox5.Checked = False
        End If
        If ket = DataGridView1.Item(12, i).Value Then
            CheckBox6.Checked = True
            CheckBox6.Enabled = False
        Else
            CheckBox6.Checked = False
        End If
        If ket = DataGridView1.Item(13, i).Value Then
            CheckBox7.Checked = True
            CheckBox7.Enabled = False
        Else
            CheckBox7.Checked = False
        End If
        If ket = DataGridView1.Item(14, i).Value Then
            CheckBox8.Checked = True
            CheckBox8.Enabled = False
        Else
            CheckBox8.Checked = False
        End If
        If ket = DataGridView1.Item(15, i).Value Then
            CheckBox9.Checked = True
            CheckBox9.Enabled = False
        Else
            CheckBox9.Checked = False
        End If
        If ket = DataGridView1.Item(16, i).Value Then
            CheckBox10.Checked = True
            CheckBox10.Enabled = False
        Else
            CheckBox10.Checked = False
        End If
        If ket = DataGridView1.Item(17, i).Value Then
            CheckBox11.Checked = True
            CheckBox11.Enabled = False
        Else
            CheckBox11.Checked = False
        End If
        If ket = DataGridView1.Item(18, i).Value Then
            CheckBox12.Checked = True
            CheckBox12.Enabled = False
        Else
            CheckBox12.Checked = False
        End If
        TextBox5.Text = DataGridView1.Item(6, i).Value
        ComboBox1.Text = DataGridView1.Item(19, i).Value
        TextBox4.Text = DataGridView1.Item(20, i).Value
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim tgl As Date
        tgl = Format(Today, "dd/MM/yyyy")
        Dim bj, bf, bm, ba, bmei, bjun, bjul, bag, bs, bok, bn, bd As String
        If CheckBox1.Checked = True Then
            bj = "Lunas"

        Else
            bj = "Belum Lunas"
        End If
        If CheckBox2.Checked = True Then
            bf = "Lunas"

        Else
            bf = "Belum Lunas"
        End If
        If CheckBox3.Checked = True Then
            bm = "Lunas"

        Else
            bm = "Belum Lunas"
        End If
        If CheckBox4.Checked = True Then
            ba = "Lunas"

        Else
            ba = "Belum Lunas"
        End If
        If CheckBox5.Checked = True Then
            bmei = "Lunas"

        Else
            bmei = "Belum Lunas"
        End If
        If CheckBox6.Checked = True Then
            bjun = "Lunas"
        Else
            bjun = "Belum Lunas"
        End If
        If CheckBox7.Checked = True Then
            bjul = "Lunas"
        Else
            bjul = "Belum Lunas"
        End If
        If CheckBox8.Checked = True Then
            bag = "Lunas"
        Else
            bag = "Belum Lunas"
        End If
        If CheckBox9.Checked = True Then
            bs = "Lunas"
        Else
            bs = "Belum Lunas"
        End If
        If CheckBox10.Checked = True Then
            bok = "Lunas"
        Else
            bok = "Belum Lunas"
        End If
        If CheckBox11.Checked = True Then
            bn = "Lunas"
        Else
            bn = "Belum Lunas"
        End If
        If CheckBox12.Checked = True Then
            bd = "Lunas"
        Else
            bd = "Belum Lunas"
        End If
        Dim jml As Integer
        jml = 12
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from pembayaran where JmlhBulan='" & TextBox5.Text & "' and IDPembayaran='" & DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value & "'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            MsgBox("ubah jumlah bulan !")
        ElseIf jml = DataGridView1.Item(6, DataGridView1.CurrentRow.Index).Value Then
            MsgBox("sudah lunas")
        ElseIf TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or ComboBox1.Text = "" Or TextBox5.Text = "" Then
            MsgBox("data harus lengkap")
        Else
            Call Koneksi()
            Call auto()
            Cmd = New SqlClient.SqlCommand("Update pembayaran set Petugas='" & Label2.Text & "', Nisn='" & TextBox1.Text & "', NamaSiswa='" & TextBox2.Text & "', Kelas='" & TextBox3.Text & "', TanggalBayar='" & Format(Now, "yyyy/MM/dd") & "', JmlhBulan='" & Convert.ToInt32(TextBox5.Text) & "',
                                            Januari='" & bj & "', Februari='" & bf & "', Maret='" & bm & "', April='" & ba & "', Mei='" & bmei & "', Juni='" & bjun & "', Juli='" & bjul & "', Agustus='" & bag & "', September='" & bs & "', Oktober='" & bok & "', November='" & bn & "', Desember='" & bd & "', TahunBayar='" & ComboBox1.Text & "', JumlahBayar='" & TextBox4.Text & "' where IDPembayaran='" & DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value & "'", Conn)
            Cmd.ExecuteNonQuery()
            MsgBox("data diubah")
            Call tampildata()
            TextBox5.Text = ""
            TextBox4.Text = ""
            Call bersih()
            Call awal()
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If MessageBox.Show("hapus data?", "info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            Call Koneksi()
            Cmd = New SqlClient.SqlCommand("delete from pembayaran where IDPembayaran='" & DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value & "'", Conn)
            Cmd.ExecuteNonQuery()
            MsgBox("data dihapus")
            Call tampildata()
            Call bersih()
            Call awal()
        End If
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from pembayaran where Nisn like '%" & TextBox6.Text & "%' or NamaSiswa like '%" & TextBox6.Text & "%' or Kelas like '%" & TextBox6.Text & "%' or TanggalBayar='" & TextBox6.Text & "'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            Call Koneksi()
            Da = New SqlClient.SqlDataAdapter("select * from pembayaran where Nisn like '%" & TextBox6.Text & "%' or NamaSiswa like '%" & TextBox6.Text & "%' or Kelas like '%" & TextBox6.Text & "%'  or TanggalBayar='" & TextBox6.Text & "'", Conn)
            Ds = New DataSet
            Da.Fill(Ds, "ketemu")
            DataGridView1.DataSource = Ds.Tables("ketemu")
            DataGridView1.ReadOnly = True
        ElseIf TextBox6.Text = "" Then
            Call tampildata()
        Else
            Call tampildata()
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        RiwayatPembayaran.Show()
        Me.Close()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        MainForm.Show()
        Me.Close()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        SearchSiswa.Show()
        Call Koneksi()
        Da = New SqlClient.SqlDataAdapter("select * from siswa where Nisn !='" & DataGridView1.Item(2, DataGridView1.CurrentRow.Index).Value & "'", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "siswa")
        SearchSiswa.DataGridView1.DataSource = Ds.Tables("siswa")
        SearchSiswa.DataGridView1.ReadOnly = True
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox5.Text = 1
        Else
            TextBox5.Text = ""
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            TextBox5.Text = 2
        Else
            TextBox5.Text = 2 - 1
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            TextBox5.Text = 3
        Else
            TextBox5.Text = 3 - 1
        End If
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            TextBox5.Text = 4
        Else
            TextBox5.Text = 4 - 1
        End If
    End Sub

    Private Sub CheckBox5_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox5.CheckedChanged
        If CheckBox5.Checked = True Then
            TextBox5.Text = 5
        Else
            TextBox5.Text = 5 - 1
        End If
    End Sub

    Private Sub CheckBox6_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox6.CheckedChanged
        If CheckBox6.Checked = True Then
            TextBox5.Text = 6
        Else
            TextBox5.Text = 6 - 1
        End If
    End Sub

    Private Sub CheckBox7_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox7.CheckedChanged
        If CheckBox7.Checked = True Then
            TextBox5.Text = 7
        Else
            TextBox5.Text = 7 - 1
        End If
    End Sub

    Private Sub CheckBox8_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox8.CheckedChanged
        If CheckBox8.Checked = True Then
            TextBox5.Text = 8
        Else
            TextBox5.Text = 8 - 1
        End If
    End Sub

    Private Sub CheckBox9_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox9.CheckedChanged
        If CheckBox9.Checked = True Then
            TextBox5.Text = 9
        Else
            TextBox5.Text = 9 - 1
        End If
    End Sub

    Private Sub CheckBox10_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox10.CheckedChanged
        If CheckBox10.Checked = True Then
            TextBox5.Text = 10
        Else
            TextBox5.Text = 10 - 1
        End If
    End Sub

    Private Sub CheckBox11_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox11.CheckedChanged
        If CheckBox11.Checked = True Then
            TextBox5.Text = 11
        Else
            TextBox5.Text = 11 - 1
        End If
    End Sub

    Private Sub CheckBox12_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox12.CheckedChanged
        If CheckBox12.Checked = True Then
            TextBox5.Text = 12
        Else
            TextBox5.Text = 12 - 1
        End If
    End Sub
End Class