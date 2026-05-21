using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Almacen_El_Dorado
{
    public partial class FrmProveedores : Form
    {
        // Lista de proveedores
        List<Proveedor> listaProveedores = new List<Proveedor>();

        // Variables para controlar edicion
        bool editando = false;
        int idActual = 0;

        // ========== CONSTRUCTOR ==========
        public FrmProveedores()
        {
            InitializeComponent();

            // Configurar DataGridView
            ConfigurarDataGridView();

            // Cargar proveedores de ejemplo
            CargarProveedoresEjemplo();

            // Mostrar proveedores
            MostrarProveedores();
        }

        // ========== CLASE PROVEEDOR ==========
        class Proveedor
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Telefono { get; set; }
            public string Direccion { get; set; }
            public string Contacto { get; set; }
            public string Email { get; set; }
        }

        // ========== CONFIGURAR DATAGRIDVIEW ==========
        private void ConfigurarDataGridView()
        {
            dgvProveedores.Columns.Clear();

            dgvProveedores.Columns.Add("Id", "ID");
            dgvProveedores.Columns.Add("Nombre", "Nombre");
            dgvProveedores.Columns.Add("Telefono", "Telefono");
            dgvProveedores.Columns.Add("Direccion", "Direccion");
            dgvProveedores.Columns.Add("Contacto", "Contacto");
            dgvProveedores.Columns.Add("Email", "Email");

            dgvProveedores.Columns["Id"].Width = 50;
            dgvProveedores.Columns["Nombre"].Width = 150;
            dgvProveedores.Columns["Telefono"].Width = 100;
            dgvProveedores.Columns["Direccion"].Width = 150;
            dgvProveedores.Columns["Contacto"].Width = 120;
            dgvProveedores.Columns["Email"].Width = 150;

            dgvProveedores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedores.MultiSelect = false;
            dgvProveedores.AllowUserToAddRows = false;
        }

        // ========== CARGAR PROVEEDORES DE EJEMPLO ==========
        private void CargarProveedoresEjemplo()
        {
            listaProveedores.Clear();

            listaProveedores.Add(new Proveedor
            {
                Id = 1,
                Nombre = "ElectroTech",
                Telefono = "12345678",
                Direccion = "Zona 1, Ciudad",
                Contacto = "Juan Perez",
                Email = "ventas@electrotech.com"
            });

            listaProveedores.Add(new Proveedor
            {
                Id = 2,
                Nombre = "Moda Express",
                Telefono = "87654321",
                Direccion = "Zona 4, Ciudad",
                Contacto = "Maria Lopez",
                Email = "info@modaexpress.com"
            });

            listaProveedores.Add(new Proveedor
            {
                Id = 3,
                Nombre = "Distribuidora de Alimentos",
                Telefono = "55555555",
                Direccion = "Zona 7, Ciudad",
                Contacto = "Carlos Ruiz",
                Email = "carlos@distalimentos.com"
            });
        }

        // ========== MOSTRAR PROVEEDORES ==========
        private void MostrarProveedores(string filtro = "")
        {
            dgvProveedores.Rows.Clear();

            int fila = 0;
            foreach (Proveedor p in listaProveedores)
            {
                // Aplicar filtro si existe
                if (filtro != "")
                {
                    if (!p.Nombre.ToLower().Contains(filtro.ToLower()) &&
                        !p.Telefono.Contains(filtro) &&
                        !p.Contacto.ToLower().Contains(filtro.ToLower()))
                    {
                        continue;
                    }
                }

                dgvProveedores.Rows.Add();
                dgvProveedores.Rows[fila].Cells[0].Value = p.Id;
                dgvProveedores.Rows[fila].Cells[1].Value = p.Nombre;
                dgvProveedores.Rows[fila].Cells[2].Value = p.Telefono;
                dgvProveedores.Rows[fila].Cells[3].Value = p.Direccion;
                dgvProveedores.Rows[fila].Cells[4].Value = p.Contacto;
                dgvProveedores.Rows[fila].Cells[5].Value = p.Email;

                fila++;
            }
        }

        // ========== LIMPIAR CAMPOS ==========
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

        // ========== VALIDAR CAMPOS ==========
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

        // ========== BOTON NUEVO ==========
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            dgvProveedores.ClearSelection();
        }

        // ========== BOTON GUARDAR ==========
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            if (!editando)
            {
                // Agregar nuevo proveedor
                int nuevoId = 1;
                if (listaProveedores.Count > 0)
                {
                    nuevoId = listaProveedores[listaProveedores.Count - 1].Id + 1;
                }

                Proveedor nuevo = new Proveedor();
                nuevo.Id = nuevoId;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Telefono = txtTelefono.Text;
                nuevo.Direccion = txtDireccion.Text;
                nuevo.Contacto = txtContacto.Text;
                nuevo.Email = txtEmail.Text;

                listaProveedores.Add(nuevo);

                MessageBox.Show("Proveedor guardado correctamente", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Editar proveedor existente
                for (int i = 0; i < listaProveedores.Count; i++)
                {
                    if (listaProveedores[i].Id == idActual)
                    {
                        listaProveedores[i].Nombre = txtNombre.Text;
                        listaProveedores[i].Telefono = txtTelefono.Text;
                        listaProveedores[i].Direccion = txtDireccion.Text;
                        listaProveedores[i].Contacto = txtContacto.Text;
                        listaProveedores[i].Email = txtEmail.Text;
                        break;
                    }
                }

                MessageBox.Show("Proveedor actualizado correctamente", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                editando = false;
                idActual = 0;
            }

            MostrarProveedores();
            LimpiarCampos();
        }

        // ========== BOTON EDITAR ==========
        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un proveedor para editar", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = Convert.ToInt32(dgvProveedores.SelectedRows[0].Cells[0].Value);

            foreach (Proveedor p in listaProveedores)
            {
                if (p.Id == id)
                {
                    txtNombre.Text = p.Nombre;
                    txtTelefono.Text = p.Telefono;
                    txtDireccion.Text = p.Direccion;
                    txtContacto.Text = p.Contacto;
                    txtEmail.Text = p.Email;

                    editando = true;
                    idActual = p.Id;
                    break;
                }
            }

            txtNombre.Focus();
        }

        // ========== BOTON ELIMINAR ==========
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProveedores.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un proveedor para eliminar", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombreProveedor = dgvProveedores.SelectedRows[0].Cells[1].Value.ToString();

            DialogResult respuesta = MessageBox.Show("Esta seguro de eliminar al proveedor:\n\n" +
                nombreProveedor + "\n\nEsta accion no se puede deshacer.",
                "Confirmar eliminacion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                int id = Convert.ToInt32(dgvProveedores.SelectedRows[0].Cells[0].Value);

                Proveedor eliminar = null;
                foreach (Proveedor p in listaProveedores)
                {
                    if (p.Id == id)
                    {
                        eliminar = p;
                        break;
                    }
                }

                if (eliminar != null)
                {
                    listaProveedores.Remove(eliminar);
                    MessageBox.Show("Proveedor eliminado correctamente", "Exito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    MostrarProveedores();
                    LimpiarCampos();
                }
            }
        }

        // ========== BOTON LIMPIAR ==========
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            MostrarProveedores();
            txtBuscar.Text = "";
            dgvProveedores.ClearSelection();
        }

        // ========== BOTON BUSCAR ==========
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string textoBuscar = txtBuscar.Text.Trim();
            if (textoBuscar == "")
            {
                MostrarProveedores();
            }
            else
            {
                MostrarProveedores(textoBuscar);
            }
        }

        // ========== BUSCAR CON ENTER ==========
        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar_Click(sender, e);
            }
        }

        // ========== CLICK EN DATAGRIDVIEW ==========
        private void dgvProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvProveedores.Rows[e.RowIndex].Selected = true;
            }
        }

        // ========== EVENTOS VACIOS (no necesitan codigo) ==========
        private void lblTelefono_Click(object sender, EventArgs e) { }
        private void txtBuscar_TextChanged(object sender, EventArgs e) { }
        private void dgvProveedores_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void txtTelefono_TextChanged(object sender, EventArgs e) { }
        private void txtDireccion_TextChanged(object sender, EventArgs e) { }
        private void txtContacto_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void gbDatosProveedor_Enter(object sender, EventArgs e) { }
    }
}