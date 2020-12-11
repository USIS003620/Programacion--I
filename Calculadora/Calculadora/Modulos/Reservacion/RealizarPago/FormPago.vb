﻿Public Class FormPago
    Dim objConexion As New db_conexion()
    Dim dataTable As New DataTable
    Dim posicion As Integer
    Dim accion As String = "nuevo"

    Private Sub FormPago_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        obtenerDatos()
    End Sub

    Sub obtenerDatos()
        dataTable = objConexion.insercionDatos().Tables("usuarios")
        dataTable.PrimaryKey = New DataColumn() {dataTable.Columns("idUsuario")}

        cboFactu.DataSource = objConexion.obtenerDatos().Tables("TipoFactu").DefaultView()
        cboFactu.DisplayMember = "TipoFactura"
        cboFactu.ValueMember = "TipoFactu.TipoFactura"

        'mostrarDatos()
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Close()
    End Sub

    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If btnAgregar.Text = "Nuevo" Then
            btnAgregar.Text = "Guardar"
            btnModificar.Text = "Cancelar"

            '    habdesh_controles(False)
            '    VentasBindingSource.AddNew()

            '    IdClienteComboBox.SelectedValue = 1 'Cliente por default Ventas a Publico
            '    IdTipofacturaComboBox.SelectedValue = 3 'Tipo de factura por default consumidor final 
            '    IdPagoComboBox.SelectedValue = 1 'Tipo de Pago por default efectivo
            '    Fecha_vtaDateTimePicker.Value = Date.Now
            'Else 'Guardar
            '    Try
            '        _idVta = Integer.Parse(lblIdVenta.Text)
            '        Me.Validate()
            '        VentasBindingSource.EndEdit()

            '        If _idVta > 0 Then 'Modificanco
            '            eliminarDetalle()
            '        Else 'Agregando Nuevas Facturas
            '            VentasTableAdapter.Connection.Open()
            '            Dim comando As New SqlCommand
            '            comando.Connection = VentasTableAdapter.Connection
            '            comando.CommandText = "SELECT ident_current('ventas') + 1 AS idVenta"
            '            _idVta = Integer.Parse(comando.ExecuteScalar().ToString())
            '            VentasTableAdapter.Connection.Close()
            '        End If

            '        Dim nfilas As Integer = DventasProductosDataGridView.Rows.Count
            '        Dim valores(nfilas, 3) As String
            '        Dim fila As New DataGridViewRow

            '        For i = 0 To nfilas - 1
            '            fila = DventasProductosDataGridView.Rows(i)

            '            valores(i, 0) = fila.Cells("idProducto").Value.ToString()
            '            valores(i, 1) = fila.Cells("cantidad").Value.ToString()
            '            valores(i, 2) = fila.Cells("precio").Value.ToString()
            '        Next
            '        TableAdapterManager.UpdateAll(Db_sistemaDataSet)

            '        For i = 0 To nfilas - 1
            '            DventasTableAdapter1.Insert(
            '                _idVta,
            '                valores(i, 0),
            '                valores(i, 1),
            '                valores(i, 2)
            '            )
            '        Next
            '        actualizarDs()
            '        VentasBindingSource.MoveLast()

            '        habdesh_controles(True)
            btnAgregar.Text = "Nuevo"
                btnModificar.Text = "Modificar"
            Catch ex As Exception
                MessageBox.Show("Error al intentar guardar: " + ex.Message,
                    "Registro de Facturas de Venta", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnAgregarProducto_Click(sender As Object, e As EventArgs) Handles btnAgregarProducto.Click
        Dim objBuscarProductos As New FormBuscarProducto
        objBuscarProductos.ShowDialog()
        If objBuscarProductos._idP > 0 Then
            'DventasProductosBindingSource.AddNew()
            'DventasProductosDataGridView.CurrentRow.Cells("idProducto").Value = objBuscarProductos._idP
            'DventasProductosDataGridView.CurrentRow.Cells("codigo").Value = objBuscarProductos._codigoProducto
            'DventasProductosDataGridView.CurrentRow.Cells("descripcion").Value = objBuscarProductos._nombreProducto
            'DventasProductosDataGridView.CurrentRow.Cells("marca").Value = ""
            'DventasProductosDataGridView.CurrentRow.Cells("medidas").Value = ""

            'DventasProductosDataGridView.CurrentRow.Cells("cantidad").Value = 1
            'DventasProductosDataGridView.CurrentRow.Cells("precio").Value = 0
        End If
    End Sub
    Private Sub totalizar()
        Try
            Dim fila As DataGridViewRow
            Dim nfilas As Integer = dataProductos.Rows.Count - 1
            Dim subtotal, sumas, iva, total As Double

            For i As Integer = 0 To nfilas
                fila = dataProductos.Rows(i)
                subtotal = Double.Parse(fila.Cells("cantidad").Value.ToString()) * Double.Parse(fila.Cells("precio").Value.ToString())

                fila.Cells("subtotal").Value = subtotal.ToString()
                sumas += subtotal
            Next
            iva = If(IdTipofacturaComboBox.SelectedValue = 4, sumas * 0.13, 0)
            total = sumas + iva

            lblRespuestaSuma.Text = "$ " + Math.Round(sumas, 2).ToString()
            lblRespuestaIva.Text = "$ " + Math.Round(iva, 2).ToString()
            lblRespuestaTotal.Text = "$ " + Math.Round(total, 2).ToString()

            'lblnregistros.Text = (VentasBindingSource.Position + 1) & " de " & VentasBindingSource.Count
        Catch ex As Exception
            'MessageBox.Show("Error " + ex.Message)
        End Try
    End Sub

    Private Sub btnQuitarProducto_Click(sender As Object, e As EventArgs) Handles btnQuitarProducto.Click
        Try
            dataProductos.Rows.Remove(dataProductos.CurrentRow)
            totalizar()
        Catch ex As Exception
            MessageBox.Show("Error al intentar quitar la fila: " + ex.Message)
        End Try
    End Sub

    Sub habdesh_controles()

    End Sub
End Class