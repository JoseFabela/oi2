Imports System.IO
Imports System.Reflection.Emit

Public Class Form1

    Dim cantidadPersonas As Int64 = 0
    Dim RegistradosInt As Int64 = 0
    Dim arreglo() As Contacto

    Private length As Integer = 0


    Private nombreArchivo As String ' Agregamos una variable para guardar el nombre del archivo
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DataGridView1.Columns.Add("Nombre", "Nombre")
        DataGridView1.Columns.Add("Edad", "Edad")
        DataGridView1.Columns.Add("Telefono", "Teléfono")
        DataGridView1.Columns.Add("Correo", "Correo")

        'Generamos el nombre del archivo y lo guardamos en la variable correspondiente
        nombreArchivo = My.Computer.FileSystem.SpecialDirectories.Desktop & "\contactos.txt"
        ' Si el archivo no existe, lo creamos con un encabezado
        If Not My.Computer.FileSystem.FileExists(nombreArchivo) Then
            My.Computer.FileSystem.WriteAllText(nombreArchivo, "Lista de contactos:" & vbCrLf & vbCrLf, False)
        End If
    End Sub

    Private Sub btnRegistrar_Click(sender As Object, e As EventArgs) Handles btnRegistrar.Click
        If (RegistradosInt < cantidadPersonas) Then
            Dim nuevocontacto = New Contacto()

            nuevocontacto.FechaDenacimiento_ = dtpFecha.Value
            nuevocontacto.Nombre_ = txtNombre.Text
            nuevocontacto.telefono_ = txtTelefono.Text
            nuevocontacto.CorreoElc = txtCorreo.Text
            arreglo(RegistradosInt) = nuevocontacto
            RegistradosInt = RegistradosInt + 1
            Dim nuevaLinea As String = nuevocontacto.Nombre_ + ", " + nuevocontacto.Edad.ToString() + ", " + nuevocontacto.telefono_.ToString() + ", " + nuevocontacto.CorreoElc.ToString() + Environment.NewLine
            DataGridView1.Rows.Add(nuevocontacto.Nombre_, nuevocontacto.Edad.ToString(), nuevocontacto.telefono_.ToString(), nuevocontacto.CorreoElc.ToString())

            My.Computer.FileSystem.WriteAllText(nombreArchivo, nuevaLinea, True)






            txtCorreo.Clear()
            txtNombre.Clear()
            txtTelefono.Clear()
            dtpFecha.Update()





        Else
            MsgBox("Ya se registraron todos")
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) 

    End Sub

    Private Sub NUDCantidad_ValueChanged(sender As Object, e As EventArgs) Handles NUDCantidad.ValueChanged
        cantidadPersonas = NUDCantidad.Text
        RegistradosInt = 0
        ReDim arreglo(cantidadPersonas)
    End Sub

    Private Sub btnAbrir_Click(sender As Object, e As EventArgs) Handles btnAbrir.Click
        Dim lector As OpenFileDialog
        lector = New OpenFileDialog()
        lector.Filter = "Nombre del Archivo|*.txt"
        If lector.ShowDialog() = DialogResult.OK Then
            Dim abrir As StreamReader
            abrir = New StreamReader(lector.FileName)
            DataGridView1.Text = abrir.ReadToEnd()
            abrir.Close()
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim archivo As SaveFileDialog
        archivo = New SaveFileDialog()
        archivo.Filter = "Nombre del Archivo|*.txt"
        If archivo.ShowDialog() = DialogResult.OK Then
            Dim escribir As StreamWriter
            escribir = New StreamWriter(archivo.FileName)
            escribir.Write(DataGridView1.Text)
            escribir.Close()
        End If
    End Sub

    Private Sub DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub DataGridView1_KeyDown(sender As Object, e As KeyEventArgs) Handles DataGridView1.KeyDown
        If e.KeyCode = Keys.Delete Then
            ' Eliminar la fila seleccionada (si la hay)
            If DataGridView1.SelectedRows.Count > 0 Then
                DataGridView1.Rows.RemoveAt(DataGridView1.SelectedRows(0).Index)
            End If
        End If
    End Sub
End Class
