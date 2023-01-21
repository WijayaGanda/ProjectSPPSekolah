Imports System.Data.SqlClient

Module Module1
    Public Ds As DataSet
    Public Cmd As SqlCommand
    Public Dr As SqlDataReader
    Public Da As SqlDataAdapter
    Public Conn As SqlConnection
    Public MyDB As String

    Public Sub Koneksi()
        MyDB = "Data Source=LAPTOP-V9TE686J\WIWI;initial catalog=db_spp;integrated security = true"
        Conn = New SqlConnection(MyDB)
        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If
    End Sub

End Module
