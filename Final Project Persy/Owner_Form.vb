﻿Public Class Owner_Form
    Dim invoice As String
    Dim nama_pembeli As String

    Sub Show_Detail()
        ds.Clear()
        da = New MySql.Data.MySqlClient.MySqlDataAdapter("SELECT
                                                            p.nama_produk AS nama_produk,
                                                            p.kategori AS kategori,
                                                            dt.jumlah AS jumlah,
                                                            p.harga AS harga,
                                                            dt.subtotal AS subtotal
                                                         FROM
                                                             tbl_transaksi t
                                                         JOIN
                                                             tbl_detailtransaksi dt ON t.no_invoice = dt.no_invoice
                                                         JOIN
                                                             tbl_produk p ON dt.id_produk = p.id_produk
                                                         WHERE dt.no_invoice='" & invoice & "'", conn)
        da.Fill(ds, "detail")
        dgvDetail.Rows.Clear()
        For i As Integer = 0 To ds.Tables("detail").Rows.Count - 1
            dgvDetail.Rows.Add(ds.Tables("detail").Rows(i).Item(0).ToString,
                                ds.Tables("detail").Rows(i).Item(1).ToString,
                                ds.Tables("detail").Rows(i).Item(2).ToString,
                                ds.Tables("detail").Rows(i).Item(3).ToString,
                                ds.Tables("detail").Rows(i).Item(4).ToString)
        Next

    End Sub

    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        PersyModule.Clicked_Color(btnHome)

        PersyModule.Restore_Color(btnReport)
        PersyModule.Restore_Color(btnProfile)
        PersyModule.Restore_Color(btnDetail)

        btnHome.Image = My.Resources.home
        btnReport.Image = My.Resources.Laporan
        btnDetail.Image = My.Resources.history_white
        btnProfile.Image = My.Resources.output_onlinepngtools__1_

        panelRiwayat.Visible = False
    End Sub

    Private Sub btnReport_Click(sender As Object, e As EventArgs) Handles btnReport.Click
        PersyModule.Clicked_Color(btnReport)

        PersyModule.Restore_Color(btnHome)
        PersyModule.Restore_Color(btnProfile)
        PersyModule.Restore_Color(btnDetail)

        btnReport.Image = My.Resources.report
        btnHome.Image = My.Resources.output_onlinepngtools
        btnDetail.Image = My.Resources.history_white
        btnProfile.Image = My.Resources.output_onlinepngtools__1_

        panelRiwayat.Visible = False
    End Sub

    Private Sub btnProfile_Click(sender As Object, e As EventArgs) Handles btnProfile.Click
        PersyModule.Clicked_Color(btnProfile)

        PersyModule.Restore_Color(btnHome)
        PersyModule.Restore_Color(btnReport)
        PersyModule.Restore_Color(btnDetail)

        btnReport.Image = My.Resources.Laporan
        btnHome.Image = My.Resources.output_onlinepngtools
        btnDetail.Image = My.Resources.history_white
        btnProfile.Image = My.Resources.user1

        panelRiwayat.Visible = False
    End Sub

    Private Sub btnDetail_Click(sender As Object, e As EventArgs) Handles btnDetail.Click
        PersyModule.Clicked_Color(btnDetail)

        PersyModule.Restore_Color(btnHome)
        PersyModule.Restore_Color(btnProfile)
        PersyModule.Restore_Color(btnReport)

        btnReport.Image = My.Resources.Laporan
        btnHome.Image = My.Resources.output_onlinepngtools
        btnDetail.Image = My.Resources.history
        btnProfile.Image = My.Resources.output_onlinepngtools__1_

        panelRiwayat.Visible = True
    End Sub
    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        PersyModule.Restore_Color(btnHome)
        PersyModule.Restore_Color(btnReport)
        PersyModule.Restore_Color(btnProfile)
        PersyModule.Restore_Color(btnDetail)
        PersyModule.HideErrorLogin()

        btnHome.Image = My.Resources.output_onlinepngtools
        btnReport.Image = My.Resources.Laporan
        btnDetail.Image = My.Resources.history_white
        btnProfile.Image = My.Resources.output_onlinepngtools__1_

        panelRiwayat.Visible = False

        Me.Hide()
        Login.Show()
    End Sub

    Private Sub shortenPage_Click(sender As Object, e As EventArgs) Handles shortenPage.Click
        extendPage.Visible = True
        shortenPage.Visible = False
        panelMenu.Visible = False
        panelMenu.Width = 46

        transitionFormP.ShowSync(panelMenu)
    End Sub

    Private Sub extendPage_Click(sender As Object, e As EventArgs) Handles extendPage.Click
        extendPage.Visible = False
        shortenPage.Visible = True
        panelMenu.Visible = False
        panelMenu.Width = 204

        transitionFormP.ShowSync(panelMenu)
    End Sub

    Private Sub tbxPemasukan_TextChanged(sender As Object, e As EventArgs) Handles tbxPemasukan.TextChanged

    End Sub

    Private Sub panelRiwayat_Paint(sender As Object, e As PaintEventArgs) Handles panelRiwayat.Paint
        PersyModule.Show_Transaction(dgvRiwayat)
        PersyModule.total_value(dgvRiwayat, tbxPemasukan)
    End Sub

    Private Sub btnClosePanel_Click(sender As Object, e As EventArgs) Handles btnClosePanel.Click
        panelDetail.Visible = False
    End Sub

    Private Sub dgvRiwayat_CellContentDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvRiwayat.CellContentDoubleClick
        Try
            If dgvRiwayat.SelectedRows.Count > 0 Then
                Dim selectedRow As DataGridViewRow = dgvRiwayat.SelectedRows(0)
                invoice = selectedRow.Cells("noinvoice").Value.ToString()
                nama_pembeli = selectedRow.Cells("pelanggan").Value.ToString()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Terjadi Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        panelDetail.Visible = True
    End Sub

    Private Sub panelDetail_Paint(sender As Object, e As PaintEventArgs) Handles panelDetail.Paint
        Show_Detail()
        tbxinvoice.Text = invoice
        tbxPelanggan.Text = nama_pembeli
        PersyModule.total_value(dgvDetail, tbxTotal)
    End Sub

    Private Sub Owner_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        connection()
    End Sub
End Class