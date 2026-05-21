using System;
using System.Windows.Forms;
using Almacen_El_Dorado.Database;
using System.Data.SqlClient;

namespace Almacen_El_Dorado
{
    public partial class FrmCategorias : Form
    {
        public FrmCategorias()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(500, 450);
            CargarCategorias();
        }

        // metodo para cargar categorias desde la base de datos
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

                    lstCategorias.Items.Clear();
                    while (reader.Read())
                    {
                        string item = reader["IdCategoria"].ToString() + " - " + reader["Nombre"].ToString();
                        lstCategorias.Items.Add(item);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar categorias: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // evento del boton agregar categoria
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // validar que no este vacio
            if (txtCategoria.Text == "")
            {
                MessageBox.Show("Ingrese el nombre de la categoria", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoria.Focus();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();

                    // verificar si ya existe
                    string checkQuery = "SELECT COUNT(*) FROM Categorias WHERE Nombre = @nombre";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@nombre", txtCategoria.Text);
                    int existe = (int)checkCmd.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("La categoria '" + txtCategoria.Text + "' ya existe", "Duplicado",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCategoria.Text = "";
                        txtCategoria.Focus();
                        return;
                    }

                    // insertar nueva categoria
                    string insertQuery = "INSERT INTO Categorias (Nombre) VALUES (@nombre)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@nombre", txtCategoria.Text);
                    insertCmd.ExecuteNonQuery();

                    MessageBox.Show("Categoria agregada correctamente", "Exito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    txtCategoria.Text = "";
                    CargarCategorias();
                    txtCategoria.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al agregar categoria: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // evento del boton eliminar categoria
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // verificar que haya una categoria seleccionada
            if (lstCategorias.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una categoria para eliminar", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // obtener el id de la categoria seleccionada
            string seleccionado = lstCategorias.SelectedItem.ToString();
            int idCategoria = Convert.ToInt32(seleccionado.Split('-')[0].Trim());
            string nombreCategoria = seleccionado.Split('-')[1].Trim();

            // confirmar eliminacion
            DialogResult respuesta = MessageBox.Show("Esta seguro de eliminar la categoria: " + nombreCategoria,
                "Confirmar eliminacion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM Categorias WHERE IdCategoria = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@id", idCategoria);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Categoria eliminada correctamente", "Exito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CargarCategorias();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar categoria: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // evento para volver al menu principal
        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmPrincipal principal = new FrmPrincipal();
            principal.Show();
        }

        // eventos vacios
        private void lstCategorias_SelectedIndexChanged(object sender, EventArgs e) { }
        private void txtCategoria_TextChanged(object sender, EventArgs e) { }
        private void FrmCategorias_Load(object sender, EventArgs e) { }
    }
}