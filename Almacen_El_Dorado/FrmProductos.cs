using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Almacen_El_Dorado
{
    public partial class FrmProductos : Form
    {
        // ========== LISTA TEMPORAL DE PRODUCTOS (SIN BD) ==========
        List<Producto> listaProductos = new List<Producto>();

        // Variables para controlar edición
        bool editando = false;
        int idActual = 0;

        // ========== CONSTRUCTOR ==========
        public FrmProductos()
        {
            InitializeComponent();

            // Configurar la tabla
            ConfigurarDataGridView();

            // Cargar categorías en el ComboBox
            CargarCategorias();

            // Cargar productos de ejemplo
            CargarProductosEjemplo();

            // Mostrar productos en la tabla
            MostrarProductos();
        }

        // ========== CLASE PRODUCTO ==========
        class Producto
        {
            public int Id { get; set; }
            public string Codigo { get; set; }
            public string Nombre { get; set; }
            public decimal Precio { get; set; }
            public int Stock { get; set; }
            public string Categoria { get; set; }
        }

        // ========== CARGAR CATEGORÍAS EN EL COMBOBOX ==========
        private void CargarCategorias()
        {
            cmbCategoria.Items.Clear();
            cmbCategoria.Items.Add("Electrónica");
            cmbCategoria.Items.Add("Ropa");
            cmbCategoria.Items.Add("Alimentos");
            cmbCategoria.Items.Add("Hogar");
            cmbCategoria.Items.Add("Papelería");
            cmbCategoria.Items.Add("Herramientas");

            cmbCategoria.SelectedIndex = 0;
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        // ========== CONFIGURAR LA TABLA (DataGridView) ==========
        private void ConfigurarDataGridView()
        {
            // Limpiar columnas existentes
            dgvProductos.Columns.Clear();

            // Crear columnas
            dgvProductos.Columns.Add("Id", "ID");
            dgvProductos.Columns.Add("Codigo", "Código");
            dgvProductos.Columns.Add("Nombre", "Nombre");
            dgvProductos.Columns.Add("Precio", "Precio");
            dgvProductos.Columns.Add("Stock", "Stock");
            dgvProductos.Columns.Add("Categoria", "Categoría");

            // Configurar ancho de columnas
            dgvProductos.Columns["Id"].Width = 50;
            dgvProductos.Columns["Codigo"].Width = 100;
            dgvProductos.Columns["Nombre"].Width = 150;
            dgvProductos.Columns["Precio"].Width = 80;
            dgvProductos.Columns["Stock"].Width = 60;
            dgvProductos.Columns["Categoria"].Width = 100;

            // Configurar selección
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.MultiSelect = false;
            dgvProductos.AllowUserToAddRows = false;

            // Evento para seleccionar fila
            dgvProductos.CellClick += dgvProductos_CellClick;
        }

        // ========== CARGAR PRODUCTOS DE EJEMPLO ==========
        private void CargarProductosEjemplo()
        {
            listaProductos.Clear();

            listaProductos.Add(new Producto
            {
                Id = 1,
                Codigo = "PROD001",
                Nombre = "Laptop HP",
                Precio = 5000.00m,
                Stock = 10,
                Categoria = "Electrónica"
            });

            listaProductos.Add(new Producto
            {
                Id = 2,
                Codigo = "PROD002",
                Nombre = "Mouse USB",
                Precio = 150.00m,
                Stock = 50,
                Categoria = "Electrónica"
            });

            listaProductos.Add(new Producto
            {
                Id = 3,
                Codigo = "PROD003",
                Nombre = "Camisa Polo",
                Precio = 250.00m,
                Stock = 30,
                Categoria = "Ropa"
            });

            listaProductos.Add(new Producto
            {
                Id = 4,
                Codigo = "PROD004",
                Nombre = "Teclado USB",
                Precio = 300.00m,
                Stock = 8,
                Categoria = "Electrónica"
            });

            listaProductos.Add(new Producto
            {
                Id = 5,
                Codigo = "PROD005",
                Nombre = "Silla Gamer",
                Precio = 1200.00m,
                Stock = 3,
                Categoria = "Hogar"
            });
        }

        // ========== MOSTRAR PRODUCTOS EN EL DataGridView ==========
        private void MostrarProductos(string filtro = "")
        {
            // Limpiar la tabla
            dgvProductos.Rows.Clear();

            int fila = 0;

            // Recorrer todos los productos
            foreach (Producto p in listaProductos)
            {
                // Si hay filtro, verificar si coincide
                if (filtro != "")
                {
                    if (!p.Nombre.ToLower().Contains(filtro.ToLower()) &&
                        !p.Codigo.ToLower().Contains(filtro.ToLower()))
                    {
                        continue; // Saltar este producto
                    }
                }

                // Agregar fila
                dgvProductos.Rows.Add();
                dgvProductos.Rows[fila].Cells[0].Value = p.Id;
                dgvProductos.Rows[fila].Cells[1].Value = p.Codigo;
                dgvProductos.Rows[fila].Cells[2].Value = p.Nombre;
                dgvProductos.Rows[fila].Cells[3].Value = p.Precio.ToString("0.00");
                dgvProductos.Rows[fila].Cells[4].Value = p.Stock;
                dgvProductos.Rows[fila].Cells[5].Value = p.Categoria;

                // Cambiar color si stock bajo (menos de 5)
                if (p.Stock <= 5)
                {
                    dgvProductos.Rows[fila].DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                }
                else if (p.Stock <= 10)
                {
                    dgvProductos.Rows[fila].DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                }

                fila++;
            }

            // Mostrar mensaje si no hay resultados
            if (fila == 0 && filtro != "")
            {
                MessageBox.Show("No se encontraron productos con: " + filtro, "Búsqueda",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                MostrarProductos(); // Recargar todos
                txtBuscar.Text = "";
            }
        }

        // ========== LIMPIAR CAMPOS DEL FORMULARIO ==========
        private void LimpiarCampos()
        {
            txtCodigo.Text = "";
            txtNombre.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            cmbCategoria.SelectedIndex = 0;

            editando = false;
            idActual = 0;

            txtCodigo.Focus();
        }

        // ========== VALIDAR QUE LOS CAMPOS ESTÉN LLENOS ==========
        private bool ValidarCampos()
        {
            if (txtCodigo.Text == "")
            {
                MessageBox.Show("❌ Ingrese el código del producto", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return false;
            }

            if (txtNombre.Text == "")
            {
                MessageBox.Show("❌ Ingrese el nombre del producto", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (txtPrecio.Text == "")
            {
                MessageBox.Show("❌ Ingrese el precio del producto", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return false;
            }

            // Validar que precio sea número
            decimal precio;
            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("❌ El precio debe ser un número válido\nEjemplo: 99.99", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return false;
            }

            // Validar que precio no sea negativo
            if (precio < 0)
            {
                MessageBox.Show("❌ El precio no puede ser negativo", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return false;
            }

            // Validar que stock sea número (si está lleno)
            if (txtStock.Text != "")
            {
                int stock;
                if (!int.TryParse(txtStock.Text, out stock))
                {
                    MessageBox.Show("❌ La cantidad debe ser un número válido", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStock.Focus();
                    return false;
                }

                if (stock < 0)
                {
                    MessageBox.Show("❌ El stock no puede ser negativo", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtStock.Focus();
                    return false;
                }
            }

            return true;
        }

        // ========== BOTÓN NUEVO ==========
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // ========== BOTÓN GUARDAR ==========
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validar campos
            if (!ValidarCampos())
                return;

            if (!editando)
            {
                // ===== AGREGAR NUEVO PRODUCTO =====

                // Verificar que el código no exista ya
                foreach (Producto p in listaProductos)
                {
                    if (p.Codigo == txtCodigo.Text)
                    {
                        MessageBox.Show("❌ Ya existe un producto con el código: " + txtCodigo.Text,
                            "Código duplicado",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCodigo.Focus();
                        return;
                    }
                }

                // Calcular nuevo ID
                int nuevoId = 1;
                if (listaProductos.Count > 0)
                {
                    nuevoId = listaProductos[listaProductos.Count - 1].Id + 1;
                }

                // Crear nuevo producto
                Producto nuevo = new Producto();
                nuevo.Id = nuevoId;
                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Precio = decimal.Parse(txtPrecio.Text);
                nuevo.Stock = txtStock.Text == "" ? 0 : int.Parse(txtStock.Text);
                nuevo.Categoria = cmbCategoria.SelectedItem.ToString();

                // Agregar a la lista
                listaProductos.Add(nuevo);

                MessageBox.Show("✅ Producto guardado correctamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // ===== EDITAR PRODUCTO EXISTENTE =====

                // Verificar que el código no exista en otro producto
                foreach (Producto p in listaProductos)
                {
                    if (p.Codigo == txtCodigo.Text && p.Id != idActual)
                    {
                        MessageBox.Show("❌ Ya existe otro producto con el código: " + txtCodigo.Text,
                            "Código duplicado",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCodigo.Focus();
                        return;
                    }
                }

                // Buscar y actualizar producto
                for (int i = 0; i < listaProductos.Count; i++)
                {
                    if (listaProductos[i].Id == idActual)
                    {
                        listaProductos[i].Codigo = txtCodigo.Text;
                        listaProductos[i].Nombre = txtNombre.Text;
                        listaProductos[i].Precio = decimal.Parse(txtPrecio.Text);
                        listaProductos[i].Stock = txtStock.Text == "" ? 0 : int.Parse(txtStock.Text);
                        listaProductos[i].Categoria = cmbCategoria.SelectedItem.ToString();
                        break;
                    }
                }

                MessageBox.Show("✅ Producto actualizado correctamente", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                editando = false;
                idActual = 0;
            }

            // Actualizar la tabla
            MostrarProductos();

            // Limpiar campos
            LimpiarCampos();
        }

        // ========== BOTÓN EDITAR ==========
        private void btnEditar_Click(object sender, EventArgs e)
        {
            // Verificar que haya una fila seleccionada
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("⚠️ Seleccione un producto para editar\n\nHaga clic en la fila del producto que desea editar.",
                    "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener ID del producto seleccionado
            int id = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells[0].Value);

            // Buscar el producto
            foreach (Producto p in listaProductos)
            {
                if (p.Id == id)
                {
                    // Cargar datos en los campos
                    txtCodigo.Text = p.Codigo;
                    txtNombre.Text = p.Nombre;
                    txtPrecio.Text = p.Precio.ToString();
                    txtStock.Text = p.Stock.ToString();
                    cmbCategoria.SelectedItem = p.Categoria;

                    editando = true;
                    idActual = p.Id;
                    break;
                }
            }

            txtCodigo.Focus();
        }

        // ========== BOTÓN ELIMINAR ==========
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar que haya una fila seleccionada
            if (dgvProductos.SelectedRows.Count == 0)
            {
                MessageBox.Show("⚠️ Seleccione un producto para eliminar\n\nHaga clic en la fila del producto que desea eliminar.",
                    "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtener nombre del producto para mostrar en mensaje
            string nombreProducto = dgvProductos.SelectedRows[0].Cells[2].Value.ToString();

            // Confirmar eliminación
            DialogResult respuesta = MessageBox.Show($"¿Está seguro de eliminar el producto?\n\n📦 {nombreProducto}\n\nEsta acción no se puede deshacer.",
                "Confirmar eliminación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                // Obtener ID
                int id = Convert.ToInt32(dgvProductos.SelectedRows[0].Cells[0].Value);

                // Buscar y eliminar
                Producto productoEliminar = null;
                foreach (Producto p in listaProductos)
                {
                    if (p.Id == id)
                    {
                        productoEliminar = p;
                        break;
                    }
                }

                if (productoEliminar != null)
                {
                    listaProductos.Remove(productoEliminar);
                    MessageBox.Show($"✅ Producto eliminado correctamente\n\n📦 {nombreProducto}",
                        "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Actualizar tabla
                    MostrarProductos();
                    LimpiarCampos();
                }
            }
        }

        // ========== BOTÓN LIMPIAR ==========
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            MostrarProductos(); // Refrescar tabla por si había filtro
            txtBuscar.Text = ""; // Limpiar búsqueda también

            // Quitar selección del DataGridView
            dgvProductos.ClearSelection();
        }

        // ========== BOTÓN BUSCAR ==========
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string textoBuscar = txtBuscar.Text.Trim();
            if (textoBuscar == "")
            {
                MostrarProductos();
            }
            else
            {
                MostrarProductos(textoBuscar);
            }
        }

        // ========== PRESIONAR ENTER EN BÚSQUEDA ==========
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            // Búsqueda automática mientras escribe (opcional)
            // Si quieres búsqueda automática, descomenta la línea de abajo
            // MostrarProductos(txtBuscar.Text);
        }

        // ========== CLICK EN DATAGRIDVIEW ==========
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Evitar que seleccione el encabezado
            if (e.RowIndex >= 0)
            {
                dgvProductos.Rows[e.RowIndex].Selected = true;
            }
        }

        // ========== EVENTO PARA CUANDO SE SELECCIONA UNA CATEGORÍA ==========
        private void cmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            // No necesita código, solo está aquí para evitar error
        }

        // ========== EVENTOS DE TEXTO (no necesitan código) ==========
        private void txtCodigo_TextChanged(object sender, EventArgs e) { }
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void txtPrecio_TextChanged(object sender, EventArgs e) { }
        private void txtStock_TextChanged(object sender, EventArgs e) { }
        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmPrincipal principal = new FrmPrincipal();
            principal.Show();
        }
    }
}