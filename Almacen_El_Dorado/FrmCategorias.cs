using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Almacen_El_Dorado
{
    public partial class FrmCategorias : Form
    {
        // Lista de categorias
        List<string> listaCategorias = new List<string>();

        // ========== CONSTRUCTOR ==========
        public FrmCategorias()
        {
            InitializeComponent();

            // Cargar categorias de ejemplo
            CargarCategoriasEjemplo();

            // Mostrar categorias en el ListBox
            MostrarCategorias();
        }

        // ========== CARGAR CATEGORIAS DE EJEMPLO ==========
        private void CargarCategoriasEjemplo()
        {
            listaCategorias.Clear();
            listaCategorias.Add("Electronica");
            listaCategorias.Add("Ropa");
            listaCategorias.Add("Alimentos");
            listaCategorias.Add("Hogar");
            listaCategorias.Add("Papeleria");
            listaCategorias.Add("Herramientas");
        }

        // ========== MOSTRAR CATEGORIAS EN EL LISTBOX ==========
        private void MostrarCategorias()
        {
            lstCategorias.Items.Clear();

            foreach (string categoria in listaCategorias)
            {
                lstCategorias.Items.Add(categoria);
            }
        }

        // ========== AGREGAR CATEGORIA ==========
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            // Validar que no este vacio
            if (txtCategoria.Text == "")
            {
                MessageBox.Show("Ingrese el nombre de la categoria",
                    "Validacion",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtCategoria.Focus();
                return;
            }

            // Validar que no exista ya
            foreach (string cat in listaCategorias)
            {
                if (cat.ToLower() == txtCategoria.Text.ToLower())
                {
                    MessageBox.Show("La categoria '" + txtCategoria.Text + "' ya existe",
                        "Categoria duplicada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    txtCategoria.Text = "";
                    txtCategoria.Focus();
                    return;
                }
            }

            // Guardar nombre para el mensaje
            string nombreCategoria = txtCategoria.Text;

            // Agregar nueva categoria
            listaCategorias.Add(txtCategoria.Text);

            // Actualizar lista
            MostrarCategorias();

            // Limpiar campo
            txtCategoria.Text = "";
            txtCategoria.Focus();

            MessageBox.Show("Categoria '" + nombreCategoria + "' agregada correctamente",
                "Exito",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ========== ELIMINAR CATEGORIA ==========
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            // Verificar que haya una categoria seleccionada
            if (lstCategorias.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una categoria para eliminar",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            // Obtener la categoria seleccionada
            string categoriaSeleccionada = lstCategorias.SelectedItem.ToString();

            // Confirmar eliminacion
            DialogResult respuesta = MessageBox.Show("Esta seguro de eliminar la categoria:\n\n" +
                categoriaSeleccionada + "\n\nEsta accion no se puede deshacer.",
                "Confirmar eliminacion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                // Eliminar de la lista
                listaCategorias.RemoveAt(lstCategorias.SelectedIndex);

                // Actualizar lista
                MostrarCategorias();

                MessageBox.Show("Categoria eliminada correctamente",
                    "Exito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        // ========== EVENTO CUANDO SE SELECCIONA UNA CATEGORIA ==========
        private void lstCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Este evento se ejecuta cuando se selecciona un item
            // No necesita codigo adicional, pero lo dejamos para referencia
        }

        // ========== EVENTO CUANDO CAMBIA EL TEXTO (no necesita codigo) ==========
        private void txtCategoria_TextChanged(object sender, EventArgs e)
        {
            // No necesita codigo
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmPrincipal principal = new FrmPrincipal();
            principal.Show();
        }
    }
}