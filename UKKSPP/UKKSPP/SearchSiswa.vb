Public Class SearchSiswa
    Sub tampildata()
        Call Koneksi()
        Da = New SqlClient.SqlDataAdapter("select * from siswa where Nisn !='" & Pembayaran.DataGridView1.Item(2, Pembayaran.DataGridView1.CurrentRow.Index).Value & "'", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "siswa")
        DataGridView1.DataSource = Ds.Tables("siswa")
        DataGridView1.ReadOnly = True
    End Sub
    Private Sub SearchSiswa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call tampildata()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Cmd = New SqlClient.SqlCommand("Select * from siswa where Nama like '%" & TextBox1.Text & "%'", Conn)
        Dr = Cmd.ExecuteReader
        Dr.Read()
        If Dr.HasRows Then
            Call Koneksi()
            Da = New SqlClient.SqlDataAdapter("Select * from siswa where Nama like '%" & TextBox1.Text & "%'", Conn)
            Ds = New DataSet
            Da.Fill(Ds, "ketemu")
            DataGridView1.DataSource = Ds.Tables("ketemu")
            DataGridView1.ReadOnly = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = ""
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim i As Integer
        i = DataGridView1.CurrentRow.Index

        Pembayaran.TextBox2.Text = DataGridView1.Item(1, i).Value
        Call Koneksi()
        Da = New SqlClient.SqlDataAdapter("select * from siswa where Nisn !='" & Pembayaran.TextBox1.Text & "'", Conn)
        Ds = New DataSet
        Ds.Clear()
        Da.Fill(Ds, "siswa")
        DataGridView1.DataSource = Ds.Tables("siswa")
        DataGridView1.ReadOnly = True
        Me.Close()
    End Sub
End Class