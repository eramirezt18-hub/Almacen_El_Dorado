using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Almacen_El_Dorado
{
    public partial class FrmMovimientos : Form
    {
        // ========== LISTAS TEMPORALES ==========
        List<Movimiento> listaMovimientos = new List<Movimiento>();
        List<ProductoSimple> listaProductos = new List<ProductoSimple>();

        // ========== CONSTRUCTOR ==========
        public FrmMovimientos()
        {
            InitializeComponent();

            // Configurar el NumericUpDown
            nudCantidad.Minimum = 1;
            nudCantidad.Maximum = 9999;
            nudCantidad.Value = 1;

            // Cargar productos de ejemplo
            CargarProductos();

            // Cargar productos en el ComboBox
            CargarComboProductos();

            // Cargar movimientos de ejemplo
            CargarMovimientosEjemplo();

            // Configurar DataGridView
            ConfigurarDataGridView();

            // Mostrar movimientos
            MostrarMovimientos();

            // Seleccionar Entrada por defecto
            rbEntrada.Checked = true;

            // Actualizar stock mostrado
            ActualizarStockMostrado();
        }

        // ========== CLASE PRODUCTO (SIMPLIFICADA) ==========
        class ProductoSimple
        {
            public int Id { get; set; }
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public int Stock { get; set; }
            public decimal Precio { get; set; }
        }

        // ========== CLASE MOVIMIENTO ==========
        class Movimiento
        {
            public int Id { get; set; }
            public DateTime Fecha { get; set; }
            public string Tipo { get; set; }
            public string ProductoNombre { get; set; }
            public int Cantidad { get; set; }
            public int ProductoId { get; set; }
        }

        // ========== CARGAR PRODUCTOS DE EJEMPLO ==========
        private void CargarProductos()
        {
            listaProductos.Clear();

            listaProductos.Add(new ProductoSimple
            {
                Id = 1,
                Codigo = "PROD001",
                Nombre = "Laptop HP",
                Stock = 10,
                Precio = 5000
            });

            listaProductos.Add(new ProductoSimple
            {
                Id = 2,
                Codigo = "PROD002",
                Nombre = "Mouse USB",
                Stock = 50,
                Precio = 150
            });

            listaProductos.Add(new ProductoSimple
            {
                Id = 3,
                Codigo = "PROD003",
                Nombre = "Camisa Polo",
                Stock = 30,
                Precio = 250
            });

            listaProductos.Add(new ProductoSimple
            {
                Id = 4,
                Codigo = "PROD004",
                Nombre = "Teclado USB",
                Stock = 8,
                Precio = 300
            });

            listaProductos.Add(new ProductoSimple
            {
                Id = 5,
                Codigo = "PROD005",
                Nombre = "Silla Gamer",
                Stock = 3,
                Precio = 1200
            });
        }

        // ========== CARGAR PRODUCTOS EN EL COMBOBOX ==========
        private void CargarComboProductos()
        {
            cmbProducto.Items.Clear();

            foreach (ProductoSimple p in listaProductos)
            {
                // Mostrar: ID - Nombre (Stock: X)
                cmbProducto.Items.Add($"{p.Id} - {p.Nombre} (Stock: {p.Stock})");
            }

            if (cmbProducto.Items.Count > 0)
            {
                cmbProducto.SelectedIndex = 0;
            }
        }

        // ========== CARGAR MOVIMIENTOS DE EJEMPLO ==========
        private void CargarMovimientosEjemplo()
        {
            listaMovimientos.Clear();

            listaMovimientos.Add(new Movimiento
            {
                Id = 1,
                Fecha = DateTime.Now.AddDays(-2),
                Tipo = "ENTRADA",
                ProductoNombre = "Laptop HP",
                Cantidad = 5,
                ProductoId = 1
            });

            listaMovimientos.Add(new Movimiento
            {
                Id = 2,
                Fecha = DateTime.Now.AddDays(-1),
                Tipo = "SALIDA",
                ProductoNombre = "Mouse USB",
                Cantidad = 3,
                ProductoId = 2
            });
        }

        // ========== CONFIGURAR DATAGRIDVIEW ==========
        private void ConfigurarDataGridView()
        {
            dgvMovimientos.Columns.Clear();

            dgvMovimientos.Columns.Add("Fecha", "Fecha");
            dgvMovimientos.Columns.Add("Tipo", "Tipo");
            dgvMovimientos.Columns.Add("Producto", "Producto");
            dgvMovimientos.Columns.Add("Cantidad", "Cantidad");

            dgvMovimientos.Columns["Fecha"].Width = 130;
            dgvMovimientos.Columns["Tipo"].Width = 80;
            dgvMovimientos.Columns["Producto"].Width = 150;
            dgvMovimientos.Columns["Cantidad"].Width = 80;

            dgvMovimientos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMovimientos.AllowUserToAddRows = false;
        }

        // ========== MOSTRAR MOVIMIENTOS ==========
        private void MostrarMovimientos()
        {
            dgvMovimientos.Rows.Clear();

            int fila = 0;
            foreach (Movimiento m in listaMovimientos)
            {
                dgvMovimientos.Rows.Add();
                dgvMovimientos.Rows[fila].Cells[0].Value = m.Fecha.ToString("dd/MM/yyyy HH:mm");
                dgvMovimientos.Rows[fila].Cells[1].Value = m.Tipo;
                dgvMovimientos.Rows[fila].Cells[2].Value = m.ProductoNombre;
                dgvMovimientos.Rows[fila].Cells[3].Value = m.Cantidad;

                // Color diferente para entradas y salidas
                if (m.Tipo == "ENTRADA")
                {
                    dgvMovimientos.Rows[fila].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                }
                else
                {
                    dgvMovimientos.Rows[fila].DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                }

                fila++;
            }
        }

        // ========== ACTUALIZAR STOCK MOSTRADO ==========
        private void ActualizarStockMostrado()
        {
            if (cmbProducto.SelectedIndex >= 0)
            {
                int id = ObtenerIdProductoSeleccionado();

                foreach (ProductoSimple p in listaProductos)
                {
                    if (p.Id == id)
                    {
                        lblStockActual.Text = $"📦 Stock actual: {p.Stock} unidades";

                        // Cambiar color si stock bajo
                        if (p.Stock <= 5)
                        {
                            lblStockActual.ForeColor = System.Drawing.Color.Red;
                        }
                        else
                        {
                            lblStockActual.ForeColor = System.Drawing.Color.Black;
                        }
                        break;
                    }
                }
            }
        }

        // ========== OBTENER ID DEL PRODUCTO SELECCIONADO ==========
        private int ObtenerIdProductoSeleccionado()
        {
            if (cmbProducto.SelectedIndex >= 0)
            {
                string texto = cmbProducto.SelectedItem.ToString();
                string idTexto = texto.Split('-')[0].Trim();
                return int.Parse(idTexto);
            }
            return 0;
        }

        // ========== OBTENER PRODUCTO POR ID ==========
        private ProductoSimple ObtenerProductoPorId(int id)
        {
            foreach (ProductoSimple p in listaProductos)
            {
                if (p.Id == id)
                    return p;
            }
            return null;
        }

        // ========== VALIDAR MOVIMIENTO ==========
        private bool ValidarMovimiento()
        {
            // Verificar que haya un producto seleccionado
            if (cmbProducto.SelectedIndex < 0)
            {
                MessageBox.Show(" Seleccione un producto", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Verificar cantidad
            int cantidad = (int)nudCantidad.Value;
            if (cantidad <= 0)
            {
                MessageBox.Show(" La cantidad debe ser mayor a 0", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Verificar stock si es salida
            if (rbSalida.Checked)
            {
                int idProducto = ObtenerIdProductoSeleccionado();
                ProductoSimple producto = ObtenerProductoPorId(idProducto);

                if (producto.Stock < cantidad)
                {
                    MessageBox.Show($" No hay suficiente stock\n\n" +
                        $"Producto: {producto.Nombre}\n" +
                        $"Stock actual: {producto.Stock}\n" +
                        $"Cantidad solicitada: {cantidad}",
                        "Stock insuficiente",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        // ========== REGISTRAR MOVIMIENTO ==========
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Validar
            if (!ValidarMovimiento())
                return;

            // Obtener datos
            int idProducto = ObtenerIdProductoSeleccionado();
            ProductoSimple producto = ObtenerProductoPorId(idProducto);
            int cantidad = (int)nudCantidad.Value;
            string tipo = rbEntrada.Checked ? "ENTRADA" : "SALIDA";

            // Actualizar stock del producto
            if (tipo == "ENTRADA")
            {
                producto.Stock += cantidad;
            }
            else
            {
                producto.Stock -= cantidad;
            }

            // Registrar movimiento
            Movimiento nuevoMov = new Movimiento();
            nuevoMov.Id = listaMovimientos.Count + 1;
            nuevoMov.Fecha = DateTime.Now;
            nuevoMov.Tipo = tipo;
            nuevoMov.ProductoNombre = producto.Nombre;
            nuevoMov.Cantidad = cantidad;
            nuevoMov.ProductoId = producto.Id;

            listaMovimientos.Add(nuevoMov);

            // Actualizar ComboBox (para mostrar nuevo stock)
            CargarComboProductos();

            // Seleccionar el mismo producto
            for (int i = 0; i < cmbProducto.Items.Count; i++)
            {
                if (cmbProducto.Items[i].ToString().Contains(idProducto.ToString()))
                {
                    cmbProducto.SelectedIndex = i;
                    break;
                }
            }

            // Actualizar stock mostrado
            ActualizarStockMostrado();

            // Actualizar tabla de movimientos
            MostrarMovimientos();

            // Limpiar cantidad
            nudCantidad.Value = 1;

            // Mostrar mensaje de éxito
            MessageBox.Show($"✅ Movimiento registrado correctamente\n\n" +
                $" Producto: {producto.Nombre}\n" +
                $"Tipo: {tipo}\n" +
                $" Cantidad: {cantidad}\n" +
                $"Nuevo stock: {producto.Stock}",
                "Éxito",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ========== LIMPIAR CAMPOS ==========
        private void btnLimpiarMov_Click(object sender, EventArgs e)
        {
            nudCantidad.Value = 1;
            rbEntrada.Checked = true;

            if (cmbProducto.Items.Count > 0)
                cmbProducto.SelectedIndex = 0;

            ActualizarStockMostrado();
        }

        // ========== CUANDO CAMBIA EL PRODUCTO SELECCIONADO ==========
        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarStockMostrado();
        }

        // ========== CUANDO CAMBIA EL TIPO DE MOVIMIENTO ==========
        private void rbEntrada_CheckedChanged(object sender, EventArgs e)
        {
            if (rbEntrada.Checked)
            {
                btnRegistrar.BackColor = System.Drawing.Color.LightGreen;
                btnRegistrar.Text = " Registrar ENTRADA";
            }
        }

        private void rbSalida_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSalida.Checked)
            {
                btnRegistrar.BackColor = System.Drawing.Color.LightCoral;
                btnRegistrar.Text = " Registrar SALIDA";
            }
        }

        // ========== EVENTOS VACÍOS (no necesitan código) ==========
        private void dgvMovimientos_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void gbDatosMovimiento_Enter(object sender, EventArgs e) { }
        private void lblStockActual_Click(object sender, EventArgs e) { }
        private void nudCantidad_ValueChanged(object sender, EventArgs e) { }
        private void gbTipoMovimiento_Enter(object sender, EventArgs e) { }
        private void gbHistorial_Enter(object sender, EventArgs e) { }
    }
}