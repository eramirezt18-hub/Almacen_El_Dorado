using System;
using System.Windows.Forms;
using Almacen_El_Dorado.Database;
using System.Data.SqlClient;

namespace Almacen_El_Dorado
{
    public partial class FrmConsultas : Form
    {
        public FrmConsultas()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(1100, 700);
            ConfigurarDataGridView();
            CargarProductos();
            rbNombre.Checked = true;
        }

        // configurar data grid view
        private void ConfigurarDataGridView()
        {
            dgvConsultas.Columns.Clear();
            dgvConsultas.Columns.Add("Codigo", "Codigo");
            dgvConsultas.Columns.Add("Nombre", "Nombre");
            dgvConsultas.Columns.Add("Categoria", "Categoria");
            dgvConsultas.Columns.Add("Stock", "Stock");
            dgvConsultas.Columns.Add("Precio", "Precio");
            dgvConsultas.Columns.Add("ValorTotal", "Valor Total");

            dgvConsultas.Columns["Codigo"].Width = 100;
            dgvConsultas.Columns["Nombre"].Width = 150;
            dgvConsultas.Columns["Categoria"].Width = 100;
            dgvConsultas.Columns["Stock"].Width = 80;
            dgvConsultas.Columns["Precio"].Width = 100;
            dgvConsultas.Columns["ValorTotal"].Width = 100;

            dgvConsultas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvConsultas.AllowUserToAddRows = false;
        }

        // cargar productos desde la base de datos
        private void CargarProductos(string filtro = "", string tipoFiltro = "nombre")
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    string query;

                    if (filtro == "")
                    {
                        query = @"SELECT p.Codigo, p.Nombre, c.Nombre as Categoria, p.Stock, p.Precio 
                                 FROM Productos p 
                                 INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria 
                                 ORDER BY p.Nombre";
                    }
                    else
                    {
                        if (tipoFiltro == "nombre")
                        {
                            query = @"SELECT p.Codigo, p.Nombre, c.Nombre as Categoria, p.Stock, p.Precio 
                                     FROM Productos p 
                                     INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria 
                                     WHERE p.Nombre LIKE @filtro 
                                     ORDER BY p.Nombre";
                        }
                        else if (tipoFiltro == "codigo")
                        {
                            query = @"SELECT p.Codigo, p.Nombre, c.Nombre as Categoria, p.Stock, p.Precio 
                                     FROM Productos p 
                                     INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria 
                                     WHERE p.Codigo LIKE @filtro 
                                     ORDER BY p.Nombre";
                        }
                        else
                        {
                            query = @"SELECT p.Codigo, p.Nombre, c.Nombre as Categoria, p.Stock, p.Precio 
                                     FROM Productos p 
                                     INNER JOIN Categorias c ON p.IdCategoria = c.IdCategoria 
                                     WHERE c.Nombre LIKE @filtro 
                                     ORDER BY p.Nombre";
                        }
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);
                    if (filtro != "")
                    {
                        cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    dgvConsultas.Rows.Clear();

                    int totalProductos = 0;
                    decimal valorTotal = 0;
                    int stockBajo = 0;

                    while (reader.Read())
                    {
                        string codigo = reader["Codigo"].ToString();
                        string nombre = reader["Nombre"].ToString();
                        string categoria = reader["Categoria"].ToString();
                        int stock = Convert.ToInt32(reader["Stock"]);
                        decimal precio = Convert.ToDecimal(reader["Precio"]);
                        decimal valor = stock * precio;

                        dgvConsultas.Rows.Add(codigo, nombre, categoria, stock, precio.ToString("0.00"), valor.ToString("0.00"));

                        totalProductos++;
                        valorTotal += valor;
                        if (stock <= 5) stockBajo++;
                    }
                    reader.Close();

                    // mostrar resumen
                    lblTotalProductos.Text = "Total Productos: " + totalProductos;
                    lblValorTotal.Text = "Valor Total: Q. " + valorTotal.ToString("0.00");
                    lblStockBajo.Text = "Stock Bajo (<=5): " + stockBajo;

                    // contar categorias
                    string catQuery = "SELECT COUNT(DISTINCT IdCategoria) FROM Productos";
                    SqlCommand catCmd = new SqlCommand(catQuery, conn);
                    int totalCategorias = Convert.ToInt32(catCmd.ExecuteScalar());
                    lblCategorias.Text = "Categorias: " + totalCategorias;

                    if (filtro != "" && dgvConsultas.Rows.Count == 0)
                    {
                        MessageBox.Show("No se encontraron productos con: " + filtro, "Busqueda sin resultados",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarProductos();
                        txtBuscar.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                string tipo = "nombre";
                if (rbCodigo.Checked) tipo = "codigo";
                else if (rbCategoria.Checked) tipo = "categoria";
                CargarProductos(textoBuscar, tipo);
            }
        }

        // boton limpiar filtros
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            rbNombre.Checked = true;
            CargarProductos();
        }

        // buscar con enter
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar_Click(sender, e);
            }
        }

        // eventos de radiobutton
        private void rbNombre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNombre.Checked && txtBuscar.Text != "")
            {
                CargarProductos(txtBuscar.Text, "nombre");
            }
        }

        private void rbCodigo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCodigo.Checked && txtBuscar.Text != "")
            {
                CargarProductos(txtBuscar.Text, "codigo");
            }
        }

        private void rbCategoria_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCategoria.Checked && txtBuscar.Text != "")
            {
                CargarProductos(txtBuscar.Text, "categoria");
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
        private void dgvConsultas_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void gbFiltros_Enter(object sender, EventArgs e) { }
        private void txtBuscar_TextChanged(object sender, EventArgs e) { }
        private void gbResumen_Enter(object sender, EventArgs e) { }
        private void lblTotalProductos_Click(object sender, EventArgs e) { }
        private void lblValorTotal_Click(object sender, EventArgs e) { }
        private void lblStockBajo_Click(object sender, EventArgs e) { }
        private void lblCategorias_Click(object sender, EventArgs e) { }
        private void FrmConsultas_Load(object sender, EventArgs e) { }

        private void FrmConsultas_Load_1(object sender, EventArgs e)
        {

        }
    }
}