Public Class DataKelas
    Sub awal()
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        Button6.Enabled = True
        Button7.Enabled = False
        Button8.Enabled = False
        Button9.Enabled = False
        Button10.Enabled = False
    End Sub
    Sub bersih()
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub
    Sub tampildata()
        Call Koneksi()
        Da = New SqlClient.SqlDataAdapter("select * from kelas", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "kelas")
        DataGridView1.DataSource = Ds.Tables("kelas")
        DataGridView1.ReadOnly = True
    End Sub
    Sub auto()
        Dim angka As Integer
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select max(Id) from kelas", Conn)
        Cmd.ExecuteNonQuery()
        If IsDBNull(Cmd.ExecuteScalar) Then
            angka = 1
        Else
            angka = Cmd.ExecuteScalar + 1
        End If
    End Sub
    Private Sub DataKelas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label6.Text = TimeOfDay.ToString("T")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
        Form1.Show()
        Form1.TextBox1.Text = ""
        Form1.TextBox2.Text = ""
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Call bersih()
        Call awal()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        MainForm.Show()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        DataPetugas.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DataSiswa.Show()
        Me.Hide()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Pembayaran.Show()
        Me.Hide()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        Button6.Enabled = False
        Button7.Enabled = False
        Button8.Enabled = False
        Button9.Enabled = True
        Button10.Enabled = True
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from kelas where NamaKelas='" & TextBox1.Text & "'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            MsgBox("kelas sudah tersedia")
        ElseIf TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("data harus lengkap")
        Else
            Call Koneksi()
            Call auto()
            Dim i As String = "insert into kelas values('" & TextBox1.Text & "', '" & TextBox2.Text & "')"
            Cmd = New SqlClient.SqlCommand(i, Conn)
            Cmd.ExecuteNonQuery()
            MsgBox("data berhasil disimpan")
            Call tampildata()
            Call bersih()
            Call awal()
        End If
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        Button6.Enabled = False
        Button7.Enabled = True
        Button8.Enabled = True
        Button9.Enabled = False
        Button10.Enabled = True

        Dim i As Integer
        i = DataGridView1.CurrentRow.Index
        TextBox1.Text = DataGridView1.Item(1, i).Value
        TextBox2.Text = DataGridView1.Item(2, i).Value
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("data harus lengkap")
        Else
            Call Koneksi()
            Call auto()
            Dim u As String = "Update kelas set NamaKelas='" & TextBox1.Text & "', KompetensiKeahlian='" & TextBox2.Text & "' where Id='" & DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value & "'"
            Cmd = New SqlClient.SqlCommand(u, Conn)
            Cmd.ExecuteNonQuery()
            MsgBox("data berhasil diubah")
            Call tampildata()
            Call bersih()
            Call awal()
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If MessageBox.Show("hapus data?", "info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            Call Koneksi()
            Cmd = New SqlClient.SqlCommand("delete from kelas where Id='" & DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value & "'", Conn)
            Cmd.ExecuteNonQuery()
            Call tampildata()
            Call bersih()
            Call awal()
        End If
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        RiwayatPembayaran.Show()
        Me.Close()
    End Sub
End Class