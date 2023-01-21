Public Class DataPetugas
    Sub awal()
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        Button6.Enabled = True
        Button7.Enabled = False
        Button8.Enabled = False
        Button9.Enabled = False
        Button10.Enabled = False
    End Sub
    Sub tampildata()
        Call Koneksi()
        Da = New SqlClient.SqlDataAdapter("select * from petugas", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "petugas")
        DataGridView1.DataSource = Ds.Tables("petugas")
        DataGridView1.ReadOnly = True

        DataGridView1.Columns("level").Visible = False
        DataGridView1.Columns("Password").Visible = False
    End Sub
    Sub auto()
        Dim angka As Integer
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select max(id) from petugas", Conn)
        Cmd.ExecuteNonQuery()
        If IsDBNull(Cmd.ExecuteScalar) Then
            angka = 1
        Else
            angka = Cmd.ExecuteScalar + 1
        End If
    End Sub
    Sub bersih()
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
    End Sub
    Private Sub DataPetugas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox1.Enabled = True
        TextBox2.Enabled = True
        TextBox3.Enabled = True
        Button6.Enabled = False
        Button7.Enabled = False
        Button8.Enabled = False
        Button9.Enabled = True
        Button10.Enabled = True
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from petugas where Username='" & TextBox1.Text & "'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            MsgBox("username Sama")
        ElseIf TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("data harus lengkap ")
        Else
            Call Koneksi()
            Call auto()
            Dim i As String = "insert into petugas(Username, Password, Nama_petugas, level) values ('" & TextBox1.Text & "', '" & TextBox2.Text & "', '" & TextBox3.Text & "', '" & TextBox5.Text & "')"
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
        TextBox3.Enabled = True
        Button6.Enabled = False
        Button7.Enabled = True
        Button8.Enabled = True
        Button9.Enabled = False
        Button10.Enabled = True
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index

        TextBox1.Text = DataGridView1.Item(1, i).Value
        TextBox2.Text = DataGridView1.Item(2, i).Value
        TextBox3.Text = DataGridView1.Item(3, i).Value
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("data harus lengkap")
        Else
            Call Koneksi()
            Dim u As String = "Update petugas set Username='" & TextBox1.Text & "', Password='" & TextBox2.Text & "', Nama_petugas='" & TextBox3.Text & "', level='" & TextBox5.Text & "' where id='" & DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value & "'"
            Cmd = New SqlClient.SqlCommand(u, Conn)
            Cmd.ExecuteNonQuery()
            MsgBox("data berhasil diubah")
            Call tampildata()
            Call bersih()
            Call awal()
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If MessageBox.Show("hapus data ?", "info", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            Call Koneksi()
            Cmd = New SqlClient.SqlCommand("delete from petugas where id='" & DataGridView1.Item(0, DataGridView1.CurrentRow.Index).Value & "'", Conn)
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

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        Call Koneksi()
        Cmd = New SqlClient.SqlCommand("select * from petugas where Nama_petugas like '%" & TextBox4.Text & "%'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            Call Koneksi()
            Da = New SqlClient.SqlDataAdapter("select * from petugas where Nama_petugas like'%" & TextBox4.Text & "%'", Conn)
            Ds = New DataSet
            Da.Fill(Ds, "ketemu")
            DataGridView1.DataSource = Ds.Tables("ketemu")
            DataGridView1.ReadOnly = True
        End If
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        MainForm.Show()
        Me.Hide()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        DataSiswa.Show()
        Me.Close()
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