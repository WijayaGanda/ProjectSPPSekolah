Public Class Laporan
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        AxCrystalReport1.SelectionFormula = "totext({pembayaran.TanggalBayar})='" & DateTimePicker1.Text & "'"
        AxCrystalReport1.ReportFileName = "LaporanPembayaranSiswa.rpt"
        AxCrystalReport1.WindowState = Crystal.WindowStateConstants.crptMaximized
        AxCrystalReport1.RetrieveDataFiles()
        AxCrystalReport1.Action = 1
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AxCrystalReport2.ReportFileName = "FullReport.rpt"
        AxCrystalReport2.WindowState = Crystal.WindowStateConstants.crptMaximized
        AxCrystalReport2.RetrieveDataFiles()
        AxCrystalReport2.Action = 1
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MainForm.Show()
        Me.Close()
    End Sub
End Class