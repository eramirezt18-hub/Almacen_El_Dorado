using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Almacen_El_Dorado
{
    public partial class FrmConsultas : Form
    {
        // Lista de productos
        List<ProductoConsulta> listaProductos = new List<ProductoConsulta>();

        // ========== CONSTRUCTOR ==========
        public FrmConsultas()
        {
            InitializeComponent();

            // Configurar DataGridView
            ConfigurarDataGridView();

            // Cargar productos de ejemplo
            CargarProductosEjemplo();

            // Mostrar productos
            MostrarProductos();

            // Calcular y mostrar resumen
            CalcularResumen();

            // Seleccionar filtro por nombre por defecto
            rbNombre.Checked = true;
        }

        // ========== CLASE PRODUCTO ==========
        class ProductoConsulta
        {
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public string Categoria { get; set; }
            public int Stock { get; set; }
            public decimal Precio { get; set; }
        }

        // ========== CONFIGURAR DATAGRIDVIEW ==========
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

        // ========== CARGAR PRODUCTOS DE EJEMPLO ==========
        private void CargarProductosEjemplo()
        {
            listaProductos.Clear();

            listaProductos.Add(new ProductoConsulta
            {
                Codigo = "PROD001",
                Nombre = "Laptop HP",
                Categoria = "Electronica",
                Stock = 10,
                Precio = 5000.00m
            });

            listaProductos.Add(new ProductoConsulta
            {
                Codigo = "PROD002",
                Nombre = "Mouse USB",
                Categoria = "Electronica",
                Stock = 50,
                Precio = 150.00m
            });

            listaProductos.Add(new ProductoConsulta
            {
                Codigo = "PROD003",
                Nombre = "Camisa Polo",
                Categoria = "Ropa",
                Stock = 30,
                Precio = 250.00m
            });

            listaProductos.Add(new ProductoConsulta
            {
                Codigo = "PROD004",
                Nombre = "Teclado USB",
                Categoria = "Electronica",
                Stock = 8,
                Precio = 300.00m
            });

            listaProductos.Add(new ProductoConsulta
            {
                Codigo = "PROD005",
                Nombre = "Silla Gamer",
                Categoria = "Hogar",
                Stock = 3,
                Precio = 1200.00m
            });

            listaProductos.Add(new ProductoConsulta
            {
                Codigo = "PROD006",
                Nombre = "Monitor LED",
                Categoria = "Electronica",
                Stock = 2,
                Precio = 1800.00m
            });
        }

        // ========== MOSTRAR TODOS LOS PRODUCTOS ==========
        private void MostrarProductos()
        {
            dgvConsultas.Rows.Clear();

            int fila = 0;
            foreach (ProductoConsulta p in listaProductos)
            {
                dgvConsultas.Rows.Add();
                dgvConsultas.Rows[fila].Cells[0].Value = p.Codigo;
                dgvConsultas.Rows[fila].Cells[1].Value = p.Nombre;
                dgvConsultas.Rows[fila].Cells[2].Value = p.Categoria;
                dgvConsultas.Rows[fila].Cells[3].Value = p.Stock;
                dgvConsultas.Rows[fila].Cells[4].Value = p.Precio.ToString("0.00");
                dgvConsultas.Rows[fila].Cells[5].Value = (p.Stock * p.Precio).ToString("0.00");

                // Color rojo para stock bajo (menos de 5)
                if (p.Stock <= 5)
                {
                    dgvConsultas.Rows[fila].DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                }
                // Color amarillo para stock medio (entre 5 y 10)
                else if (p.Stock <= 10)
                {
                    dgvConsultas.Rows[fila].DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                }

                fila++;
            }
        }

        // ========== BUSCAR PRODUCTOS ==========
        private void BuscarProductos()
        {
            string textoBuscar = txtBuscar.Text.Trim().ToLower();

            if (textoBuscar == "")
            {
                MostrarProductos();
                CalcularResumen();
                return;
            }

            dgvConsultas.Rows.Clear();

            int fila = 0;
            int totalEncontrados = 0;
            decimal valorTotalEncontrados = 0;
            int stockBajoEncontrados = 0;

            foreach (ProductoConsulta p in listaProductos)
            {
                bool coincide = false;

                // Determinar por que campo filtrar
                if (rbNombre.Checked)
                {
                    coincide = p.Nombre.ToLower().Contains(textoBuscar);
                }
                else if (rbCodigo.Checked)
                {
                    coincide = p.Codigo.ToLower().Contains(textoBuscar);
                }
                else if (rbCategoria.Checked)
                {
                    coincide = p.Categoria.ToLower().Contains(textoBuscar);
                }

                if (coincide)
                {
                    dgvConsultas.Rows.Add();
                    dgvConsultas.Rows[fila].Cells[0].Value = p.Codigo;
                    dgvConsultas.Rows[fila].Cells[1].Value = p.Nombre;
                    dgvConsultas.Rows[fila].Cells[2].Value = p.Categoria;
                    dgvConsultas.Rows[fila].Cells[3].Value = p.Stock;
                    dgvConsultas.Rows[fila].Cells[4].Value = p.Precio.ToString("0.00");
                    dgvConsultas.Rows[fila].Cells[5].Value = (p.Stock * p.Precio).ToString("0.00");

                    if (p.Stock <= 5)
                    {
                        dgvConsultas.Rows[fila].DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                        stockBajoEncontrados++;
                    }
                    else if (p.Stock <= 10)
                    {
                        dgvConsultas.Rows[fila].DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                    }

                    totalEncontrados++;
                    valorTotalEncontrados += p.Stock * p.Precio;
                    fila++;
                }
            }

            // Actualizar resumen con los resultados de busqueda
            lblTotalProductos.Text = "Total Productos: " + totalEncontrados;
            lblValorTotal.Text = "Valor Total: Q. " + valorTotalEncontrados.ToString("0.00");
            lblStockBajo.Text = "Stock Bajo (<=5): " + stockBajoEncontrados;

            if (totalEncontrados == 0)
            {
                MessageBox.Show("No se encontraron productos con: " + textoBuscar,
                    "Busqueda sin resultados",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                MostrarProductos();
                CalcularResumen();
                txtBuscar.Text = "";
            }
        }

        // ========== CALCULAR RESUMEN DEL INVENTARIO ==========
        private void CalcularResumen()
        {
            int totalProductos = listaProductos.Count;
            decimal valorTotal = 0;
            int stockBajo = 0;
            List<string> categoriasUnicas = new List<string>();

            foreach (ProductoConsulta p in listaProductos)
            {
                valorTotal += p.Stock * p.Precio;

                if (p.Stock <= 5)
                {
                    stockBajo++;
                }

                // Agregar categoria si no existe en la lista
                if (!categoriasUnicas.Contains(p.Categoria))
                {
                    categoriasUnicas.Add(p.Categoria);
                }
            }

            lblTotalProductos.Text = "Total Productos: " + totalProductos;
            lblValorTotal.Text = "Valor Total: Q. " + valorTotal.ToString("0.00");
            lblStockBajo.Text = "Stock Bajo (<=5): " + stockBajo;
            lblCategorias.Text = "Categorias: " + categoriasUnicas.Count;
        }

        // ========== BOTON BUSCAR ==========
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarProductos();
        }

        // ========== BOTON LIMPIAR FILTROS ==========
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            rbNombre.Checked = true;
            MostrarProductos();
            CalcularResumen();
        }

        // ========== BUSCAR CON ENTER ==========
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar_Click(sender, e);
            }
        }

        // ========== EVENTOS DE RADIOBUTTON ==========
        private void rbNombre_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNombre.Checked && txtBuscar.Text != "")
            {
                BuscarProductos();
            }
        }

        private void rbCodigo_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCodigo.Checked && txtBuscar.Text != "")
            {
                BuscarProductos();
            }
        }

        private void rbCategoria_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCategoria.Checked && txtBuscar.Text != "")
            {
                BuscarProductos();
            }
        }

        // ========== EVENTOS VACIOS (no necesitan codigo) ==========
        private void dgvConsultas_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void gbFiltros_Enter(object sender, EventArgs e) { }
        private void txtBuscar_TextChanged(object sender, EventArgs e) { }
        private void gbResumen_Enter(object sender, EventArgs e) { }
        private void lblTotalProductos_Click(object sender, EventArgs e) { }
        private void lblValorTotal_Click(object sender, EventArgs e) { }
        private void lblStockBajo_Click(object sender, EventArgs e) { }
        private void lblCategorias_Click(object sender, EventArgs e) { }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmPrincipal principal = new FrmPrincipal();
            principal.Show();
        }
    }
}