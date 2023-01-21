Public Class LoginSiswa
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("data tidak boleh kosong")
        Else
            Call Koneksi()
            Cmd = New SqlClient.SqlCommand("select * from siswa where Nisn='" & TextBox1.Text & "' and Nama='" & TextBox2.Text & "'", Conn)
            Dr = Cmd.ExecuteReader
            Dr.Read()
            If Dr.HasRows Then
                RiwayatPembayaran.Show()
                Me.Hide()
            Else
                MsgBox("nisn salah, hubungi petugas jika ada kesalahan")
            End If
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub
End Class