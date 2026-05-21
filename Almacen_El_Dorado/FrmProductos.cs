using System;
using System.Windows.Forms;
using Almacen_El_Dorado.Database;
using System.Data.SqlClient;

namespace Almacen_El_Dorado
{
    public partial class FrmProductos : Form
    {
        // variables para controlar edicion
        bool editando = false;
        int idActual = 0;

        // constructor
        public FrmProductos()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(1100, 700);
            ConfigurarDataGridView();
            CargarCategorias();
            CargarProductos();
        }

        // configurar el data grid view
        private void ConfigurarDataGridView()
        {
            dgvProductos.Columns.Clear();
            dgvProductos.Columns.Add("IdProducto", "ID");
            dgvProductos.Columns.Add("Codigo", "Codigo");
            dgvProductos.Columns.Add("Nombre", "Nombre");
            dgvProductos.Columns.Add("Precio", "Precio");
            dgvProductos.Columns.Add("Stock", "Stock");
            dgvProductos.Columns.Add("Categoria", "Categoria");

            dgvProductos.Columns["IdProducto"].Width = 50;
            dgvProductos.Columns["Codigo"].Width = 100;
            dgvProductos.Columns["Nombre"].Width = 150;
            dgvProductos.Columns["Precio"].Width = 80;
            dgvProductos.Columns["Stock"].Width = 60;
            dgvProductos.Columns["Categoria"].Width = 100;

            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.CellClick += dgvProductos_CellClick;
        }

        // cargar categorias en el combobox
        private void CargarCategorias()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string query = "SELECT IdCategoria, Nombre FROM Categorias ORDER BY Nombre";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    cmbCategoria.Items.Clear();
                    while (reader.Read())
                    {
                        string item = reader["IdCategoria"].ToString() + " - " + reader["Nombre"].ToString();
                        cmbCategoria.Items.Add(item);
                    }
                    reader.Close();

                    if (cmbCategoria.Items.Count > 0)
                    {
                        cmbCategoria.SelectedIndex = 0;
                    }
                    cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorias: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // metodo para cargar productos desde la base de datos
        private void CargarProductos(string filtro = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string query;

                    if (filtro == "")
                    {
                        query = @"SELECT p.IdProducto, p.Codigo, p.Nombre, p.Precio, p.Stock, c.Nombre as Categoria 
                                 FROM Productos p 
                                 INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria 
                                 ORDER BY p.IdProducto";
                    }
                    else
                    {
                        query = @"SELECT p.IdProducto, p.Codigo, p.Nombre, p.Precio, p.Stock, c.Nombre as Categoria 
                                 FROM Productos p 
                                 INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria 
                                 WHERE p.Nombre LIKE @filtro OR p.Codigo LIKE @filtro 
                                 ORDER BY p.Nombre";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    if (filtro != "")
                    {
                        cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    dgvProductos.Rows.Clear();

                    while (reader.Read())
                    {
                        dgvProductos.Rows.Add(
                            reader["IdProducto"].ToString(),
                            reader["Codigo"].ToString(),
                            reader["Nombre"].ToString(),
                            Convert.ToDecimal(reader["Precio"]).ToString("0.00"),
                            reader["Stock"].ToString(),
                            reader["Categoria"].ToString()
                        );
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // limpiar campos del formulario
        private void LimpiarCampos()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            if (cmbCategoria.Items.Count > 0) cmbCategoria.SelectedIndex = 0;
            editando = false;
            idActual = 0;
            txtCodigo.Focus();
        }

        // validar campos obligatorios
        private bool ValidarCampos()
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("Ingrese el codigo del producto", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return false;
            }

            if (txtNombre.Text == "")
            {
                MessageBox.Show("Ingrese el nombre del producto", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (txtPrecio.Text == "")
            {
                MessageBox.Show("Ingrese el precio del producto", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return false;
            }

            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("El precio debe ser un numero valido", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return false;
            }

            if (precio < 0)
            {
                MessageBox.Show("El precio no puede ser negativo", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return false;
            }

            return true;
        }

        // boton nuevo
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            dgvProductos.ClearSelection();
        }

        // boton guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            // obtener id de categoria seleccionada
            int idCategoria = 0;
            if (cmbCategoria.SelectedIndex >= 0)
            {
                string seleccion = cmbCategoria.SelectedItem.ToString();
                idCategoria = Convert.ToInt32(seleccion.Split('-')[0].Trim());
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();

                    if (!editando)
                    {
                        // verificar si el codigo ya existe
                        string checkQuery = "SELECT COUNT(*) FROM Productos WHERE Codigo = @codigo";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                        checkCmd.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                        int existe = (int)checkCmd.ExecuteScalar();

                        if (existe > 0)
                        {
                            MessageBox.Show("Ya existe un producto con el codigo: " + txtCodigo.Text, "Duplicado",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCodigo.Focus();
                            return;
                        }

                        // insertar nuevo producto
                        string query = @"INSERT INTO Productos (Codigo, Nombre, Precio, Stock, IdCategoria) 
                                       VALUES (@codigo, @nombre, @precio, @stock, @idCategoria)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@precio", decimal.Parse(txtPrecio.Text));
                        cmd.Parameters.AddWithValue("@stock", txtStock.Text == "" ? 0 : int.Parse(txtStock.Text));
                        cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Producto guardado correctamente", "Exito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // verificar si el codigo existe en otro producto
                        string checkQuery = "SELECT COUNT(*) FROM Productos WHERE Codigo = @codigo AND IdProducto != @id";
                        SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                        checkCmd.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                        checkCmd.Parameters.AddWithValue("@id", idActual);
                        int existe = (int)checkCmd.ExecuteScalar();

                        if (existe > 0)
                        {
                            MessageBox.Show("Ya existe otro producto con el codigo: " + txtCodigo.Text, "Duplicado",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCodigo.Focus();
                            return;
                        }

                        // actualizar producto existente
                        string query = @"UPDATE Productos SET Codigo=@codigo, Nombre=@nombre, Precio=@precio, 
                                       Stock=@stock, IdCategoria=@idCategoria WHERE IdProducto=@id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", idActual);
                        cmd.Parameters.AddWithValue("@codigo", txtCodigo.Text);
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@precio", decimal.Parse(txtPrecio.Text));
                        cmd.Parameters.AddWithValue("@stock", txtStock.Text == "" ? 0 : int.Parse(txtStock.Text));
                        cmd.Parameters.AddWithValue("@idCategoria", idCategoria);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Producto actualizado correctamente", "Exito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        editando = false;
                        idActual = 0;
                    }
                }

                CargarProductos();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar producto: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // boton editar
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto para editar", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            idActual = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells[0].Value);
            txtCodigo.Text = dgvProductos.SelectedRows[0].Cells[1].Value.ToString();
            txtNombre.Text = dgvProductos.SelectedRows[0].Cells[2].Value.ToString();
            txtPrecio.Text = dgvProductos.SelectedRows[0].Cells[3].Value.ToString();
            txtStock.Text = dgvProductos.SelectedRows[0].Cells[4].Value.ToString();

            // seleccionar la categoria correcta en el combobox
            string categoriaProducto = dgvProductos.SelectedRows[0].Cells[5].Value.ToString();
            for (int i = 0; i < cmbCategoria.Items.Count; i++)
            {
                if (cmbCategoria.Items[i].ToString().Contains(categoriaProducto))
                {
                    cmbCategoria.SelectedIndex = i;
                    break;
                }
            }

            editando = true;
            txtCodigo.Focus();
        }

        // boton eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un producto para eliminar", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells[0].Value);
            string nombre = dgvProductos.SelectedRows[0].Cells[2].Value.ToString();

            DialogResult respuesta = MessageBox.Show("Esta seguro de eliminar el producto: " + nombre,
                "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Productos WHERE IdProducto = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Producto eliminado correctamente", "Exito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CargarProductos();
                        LimpiarCampos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar producto: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // boton limpiar
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarProductos();
            txtBuscar.Text = "";
            dgvProductos.ClearSelection();
        }

        // boton buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string textoBuscar = txtBuscar.Text.Trim();
            if (textoBuscar == "")
            {
                CargarProductos();
            }
            else
            {
                CargarProductos(textoBuscar);
            }
        }

        // click en data grid view
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvProductos.Rows[e.RowIndex].Selected = true;
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
        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtCodigo_TextChanged(object sender, EventArgs e) { }
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void txtPrecio_TextChanged(object sender, EventArgs e) { }
        private void txtStock_TextChanged(object sender, EventArgs e) { }
        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void txtBuscar_TextChanged(object sender, EventArgs e) { }
        private void FrmProductos_Load(object sender, EventArgs e) { }
    }
}