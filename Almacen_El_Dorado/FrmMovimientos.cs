using System;
using System.Windows.Forms;
using Almacen_El_Dorado.Database;
using System.Data.SqlClient;

namespace Almacen_El_Dorado
{
    public partial class FrmMovimientos : Form
    {
        public FrmMovimientos()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(1000, 650);
            ConfigurarDataGridView();
            CargarComboProductos();
            CargarMovimientos();
            rbEntrada.Checked = true;
        }

        // configurar data grid view
        private void ConfigurarDataGridView()
        {
            dgvMovimientos.Columns.Clear();
            dgvMovimientos.Columns.Add("Fecha", "Fecha");
            dgvMovimientos.Columns.Add("Tipo", "Tipo");
            dgvMovimientos.Columns.Add("Producto", "Producto");
            dgvMovimientos.Columns.Add("Cantidad", "Cantidad");

            dgvMovimientos.Columns["Fecha"].Width = 130;
            dgvMovimientos.Columns["Tipo"].Width = 80;
            dgvMovimientos.Columns["Producto"].Width = 250;
            dgvMovimientos.Columns["Cantidad"].Width = 80;

            dgvMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovimientos.AllowUserToAddRows = false;
        }

        // cargar productos en el combobox
        private void CargarComboProductos()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT IdProducto, Nombre, Stock FROM Productos ORDER BY Nombre";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cmbProducto.Items.Clear();
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string nombre = reader.GetString(1);
                        int stock = reader.GetInt32(2);
                        cmbProducto.Items.Add(id + " - " + nombre + " (Stock: " + stock + ")");
                    }
                    reader.Close();

                    if (cmbProducto.Items.Count > 0)
                    {
                        cmbProducto.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // cargar historial de movimientos
        private void CargarMovimientos()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string query = @"SELECT m.Fecha, m.Tipo, p.Nombre as Producto, m.Cantidad 
                                   FROM Movimientos m 
                                   INNER JOIN Productos p ON m.IdProducto = p.IdProducto 
                                   ORDER BY m.Fecha DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    dgvMovimientos.Rows.Clear();
                    while (reader.Read())
                    {
                        dgvMovimientos.Rows.Add(
                            Convert.ToDateTime(reader["Fecha"]).ToString("dd/MM/yyyy HH:mm"),
                            reader["Tipo"].ToString(),
                            reader["Producto"].ToString(),
                            reader["Cantidad"].ToString()
                        );
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar movimientos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // actualizar stock mostrado
        private void ActualizarStockMostrado()
        {
            if (cmbProducto.SelectedIndex >= 0)
            {
                string seleccion = cmbProducto.SelectedItem.ToString();
                int idProducto = Convert.ToInt32(seleccion.Split('-')[0].Trim());

                try
                {
                    using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                    {
                        conn.Open();
                        string query = "SELECT Stock FROM Productos WHERE IdProducto = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", idProducto);
                        int stock = Convert.ToInt32(cmd.ExecuteScalar());
                        lblStockActual.Text = "Stock actual: " + stock + " unidades";
                    }
                }
                catch (Exception ex)
                {
                    lblStockActual.Text = "Stock actual: Error";
                }
            }
        }

        // obtener id del producto seleccionado
        private int ObtenerIdProductoSeleccionado()
        {
            if (cmbProducto.SelectedIndex >= 0)
            {
                string seleccion = cmbProducto.SelectedItem.ToString();
                return Convert.ToInt32(seleccion.Split('-')[0].Trim());
            }
            return 0;
        }

        // validar movimiento
        private bool ValidarMovimiento()
        {
            if (cmbProducto.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un producto", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int cantidad = (int)nudCantidad.Value;
            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (rbSalida.Checked)
            {
                int idProducto = ObtenerIdProductoSeleccionado();
                try
                {
                    using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                    {
                        conn.Open();
                        string query = "SELECT Stock, Nombre FROM Productos WHERE IdProducto = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", idProducto);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            int stock = reader.GetInt32(0);
                            string nombre = reader.GetString(1);
                            reader.Close();

                            if (stock < cantidad)
                            {
                                MessageBox.Show("No hay suficiente stock\n\nProducto: " + nombre +
                                    "\nStock actual: " + stock + "\nCantidad solicitada: " + cantidad,
                                    "Stock insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al validar stock: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        // boton registrar movimiento
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!ValidarMovimiento()) return;

            int idProducto = ObtenerIdProductoSeleccionado();
            int cantidad = (int)nudCantidad.Value;
            string tipo = rbEntrada.Checked ? "ENTRADA" : "SALIDA";

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();

                    // iniciar transaccion
                    SqlTransaction transaccion = conn.BeginTransaction();

                    try
                    {
                        // actualizar stock del producto
                        string updateStock;
                        if (tipo == "ENTRADA")
                        {
                            updateStock = "UPDATE Productos SET Stock = Stock + @cantidad WHERE IdProducto = @id";
                        }
                        else
                        {
                            updateStock = "UPDATE Productos SET Stock = Stock - @cantidad WHERE IdProducto = @id";
                        }

                        SqlCommand cmdStock = new SqlCommand(updateStock, conn, transaccion);
                        cmdStock.Parameters.AddWithValue("@cantidad", cantidad);
                        cmdStock.Parameters.AddWithValue("@id", idProducto);
                        cmdStock.ExecuteNonQuery();

                        // registrar movimiento
                        string insertMov = "INSERT INTO Movimientos (Tipo, Cantidad, Fecha, IdProducto) " +
                                          "VALUES (@tipo, @cantidad, @fecha, @idProducto)";
                        SqlCommand cmdMov = new SqlCommand(insertMov, conn, transaccion);
                        cmdMov.Parameters.AddWithValue("@tipo", tipo);
                        cmdMov.Parameters.AddWithValue("@cantidad", cantidad);
                        cmdMov.Parameters.AddWithValue("@fecha", DateTime.Now);
                        cmdMov.Parameters.AddWithValue("@idProducto", idProducto);
                        cmdMov.ExecuteNonQuery();

                        transaccion.Commit();

                        MessageBox.Show("Movimiento registrado correctamente", "Exito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // refrescar datos
                        CargarComboProductos();
                        CargarMovimientos();
                        ActualizarStockMostrado();
                        nudCantidad.Value = 1;
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback();
                        MessageBox.Show("Error al registrar movimiento: " + ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexion: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // boton limpiar
        private void btnLimpiarMov_Click(object sender, EventArgs e)
        {
            nudCantidad.Value = 1;
            rbEntrada.Checked = true;
            if (cmbProducto.Items.Count > 0)
            {
                cmbProducto.SelectedIndex = 0;
            }
            ActualizarStockMostrado();
        }

        // cambiar producto seleccionado
        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarStockMostrado();
        }

        // cambiar color del boton segun tipo
        private void rbEntrada_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEntrada.Checked)
            {
                btnRegistrar.BackColor = System.Drawing.Color.LightGreen;
                btnRegistrar.Text = "Registrar ENTRADA";
            }
        }

        private void rbSalida_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSalida.Checked)
            {
                btnRegistrar.BackColor = System.Drawing.Color.LightCoral;
                btnRegistrar.Text = "Registrar SALIDA";
            }
        }

        // volver al menu principal
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmPrincipal principal = new FrmPrincipal();
            principal.Show();
        }

        // eventos vacios
        private void dgvMovimientos_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void gbDatosMovimiento_Enter(object sender, EventArgs e) { }
        private void lblStockActual_Click(object sender, EventArgs e) { }
        private void nudCantidad_ValueChanged(object sender, EventArgs e) { }
        private void gbTipoMovimiento_Enter(object sender, EventArgs e) { }
        private void gbHistorial_Enter(object sender, EventArgs e) { }
        private void FrmMovimientos_Load(object sender, EventArgs e) { }
    }
}