using System;
using System.Windows.Forms;
using Almacen_El_Dorado.Database;
using System.Data.SqlClient;

namespace Almacen_El_Dorado
{
    public partial class FrmProveedores : Form
    {
        // variables para controlar edicion
        bool editando = false;
        int idActual = 0;

        public FrmProveedores()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(1100, 700);
            ConfigurarDataGridView();
            CargarProveedores();
        }

        // configurar el data grid view
        private void ConfigurarDataGridView()
        {
            dgvProveedores.Columns.Clear();
            dgvProveedores.Columns.Add("IdProveedor", "ID");
            dgvProveedores.Columns.Add("Nombre", "Nombre");
            dgvProveedores.Columns.Add("Telefono", "Telefono");
            dgvProveedores.Columns.Add("Direccion", "Direccion");
            dgvProveedores.Columns.Add("Contacto", "Contacto");
            dgvProveedores.Columns.Add("Email", "Email");

            dgvProveedores.Columns["IdProveedor"].Width = 50;
            dgvProveedores.Columns["Nombre"].Width = 150;
            dgvProveedores.Columns["Telefono"].Width = 100;
            dgvProveedores.Columns["Direccion"].Width = 150;
            dgvProveedores.Columns["Contacto"].Width = 120;
            dgvProveedores.Columns["Email"].Width = 150;

            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedores.AllowUserToAddRows = false;
            dgvProveedores.CellClick += dgvProveedores_CellClick;
        }

        // metodo para cargar proveedores desde la base de datos
        private void CargarProveedores(string filtro = "")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string query;

                    if (filtro == "")
                    {
                        query = "SELECT IdProveedor, Nombre, Telefono, Direccion, Contacto, Email FROM Proveedores ORDER BY Nombre";
                    }
                    else
                    {
                        // buscar por nombre o codigo (id)
                        query = "SELECT IdProveedor, Nombre, Telefono, Direccion, Contacto, Email FROM Proveedores " +
                                "WHERE Nombre LIKE @filtro OR CAST(IdProveedor AS VARCHAR) LIKE @filtro ORDER BY Nombre";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    if (filtro != "")
                    {
                        cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    dgvProveedores.Rows.Clear();

                    while (reader.Read())
                    {
                        dgvProveedores.Rows.Add(
                            reader["IdProveedor"].ToString(),
                            reader["Nombre"].ToString(),
                            reader["Telefono"].ToString(),
                            reader["Direccion"].ToString(),
                            reader["Contacto"].ToString(),
                            reader["Email"].ToString()
                        );
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar proveedores: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // limpiar campos del formulario
        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtContacto.Text = "";
            txtEmail.Text = "";
            editando = false;
            idActual = 0;
            txtNombre.Focus();
        }

        // validar campos obligatorios
        private bool ValidarCampos()
        {
            if (txtNombre.Text == "")
            {
                MessageBox.Show("Ingrese el nombre del proveedor", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }
            return true;
        }

        // boton nuevo
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            dgvProveedores.ClearSelection();
        }

        // boton guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();

                    if (!editando)
                    {
                        // insertar nuevo proveedor
                        string query = "INSERT INTO Proveedores (Nombre, Telefono, Direccion, Contacto, Email) " +
                                       "VALUES (@nombre, @telefono, @direccion, @contacto, @email)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                        cmd.Parameters.AddWithValue("@contacto", txtContacto.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Proveedor guardado correctamente", "Exito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // actualizar proveedor existente
                        string query = "UPDATE Proveedores SET Nombre=@nombre, Telefono=@telefono, " +
                                       "Direccion=@direccion, Contacto=@contacto, Email=@email " +
                                       "WHERE IdProveedor=@id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", idActual);
                        cmd.Parameters.AddWithValue("@nombre", txtNombre.Text);
                        cmd.Parameters.AddWithValue("@telefono", txtTelefono.Text);
                        cmd.Parameters.AddWithValue("@direccion", txtDireccion.Text);
                        cmd.Parameters.AddWithValue("@contacto", txtContacto.Text);
                        cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Proveedor actualizado correctamente", "Exito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        editando = false;
                        idActual = 0;
                    }
                }

                CargarProveedores();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar proveedor: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // boton editar
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un proveedor para editar", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            idActual = Convert.ToInt32(dgvProveedores.SelectedRows[0].Cells[0].Value);
            txtNombre.Text = dgvProveedores.SelectedRows[0].Cells[1].Value.ToString();
            txtTelefono.Text = dgvProveedores.SelectedRows[0].Cells[2].Value.ToString();
            txtDireccion.Text = dgvProveedores.SelectedRows[0].Cells[3].Value.ToString();
            txtContacto.Text = dgvProveedores.SelectedRows[0].Cells[4].Value.ToString();
            txtEmail.Text = dgvProveedores.SelectedRows[0].Cells[5].Value.ToString();

            editando = true;
            txtNombre.Focus();
        }

        // boton eliminar
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un proveedor para eliminar", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvProveedores.SelectedRows[0].Cells[0].Value);
            string nombre = dgvProveedores.SelectedRows[0].Cells[1].Value.ToString();

            DialogResult respuesta = MessageBox.Show("Esta seguro de eliminar al proveedor: " + nombre,
                "Confirmar eliminacion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Proveedores WHERE IdProveedor = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Proveedor eliminado correctamente", "Exito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CargarProveedores();
                        LimpiarCampos();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar proveedor: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // boton limpiar
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            CargarProveedores();
            txtBuscar.Text = "";
            dgvProveedores.ClearSelection();
        }

        // boton buscar (busca por nombre o codigo/id)
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string textoBuscar = txtBuscar.Text.Trim();
            if (textoBuscar == "")
            {
                CargarProveedores();
            }
            else
            {
                CargarProveedores(textoBuscar);
            }
        }

        // buscar con enter
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar_Click(sender, e);
            }
        }

        // click en data grid view
        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvProveedores.Rows[e.RowIndex].Selected = true;
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
        private void lblTelefono_Click(object sender, EventArgs e) { }
        private void txtBuscar_TextChanged(object sender, EventArgs e) { }
        private void dgvProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void txtTelefono_TextChanged(object sender, EventArgs e) { }
        private void txtDireccion_TextChanged(object sender, EventArgs e) { }
        private void txtContacto_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void gbDatosProveedor_Enter(object sender, EventArgs e) { }
        private void FrmProveedores_Load(object sender, EventArgs e) { }
    }
}