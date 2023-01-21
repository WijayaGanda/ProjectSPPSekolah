Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.PasswordChar = "*"
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox2.PasswordChar = ""
        Else
            TextBox2.PasswordChar = "*"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("Data harus diisi")
        Else
            Call Koneksi()
            Cmd = New SqlClient.SqlCommand("select * from petugas where Username='" & TextBox1.Text & "' and Password='" & TextBox2.Text & "'", Conn)
            Dr = Cmd.ExecuteReader
            Dr.Read()
            If Not Dr.HasRows Then
                MsgBox("username atau passowrd salah")
            Else
                MainForm.Show()
                Me.Hide()
            End If
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        LoginSiswa.Show()
        Me.Hide()
    End Sub
End Class
