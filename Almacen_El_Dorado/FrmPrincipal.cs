using System;
using System.Windows.Forms;

namespace Almacen_El_Dorado
{
    public partial class FrmPrincipal : Form
    {
        string usuarioActual = "admin";

        public FrmPrincipal()
        {
            InitializeComponent();

            // Configurar ventana normal (NO MDI)
            this.WindowState = FormWindowState.Maximized;

            MostrarUsuarioEnStatusStrip();
            timer1.Start();
        }

        private void MostrarUsuarioEnStatusStrip()
        {
            try
            {
                foreach (ToolStripItem item in statusStrip1.Items)
                {
                    if (item is ToolStripStatusLabel lbl)
                    {
                        if (lbl.Text.Contains("Usuario:"))
                        {
                            lbl.Text = "Usuario: " + usuarioActual;
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                foreach (ToolStripItem item in statusStrip1.Items)
                {
                    if (item is ToolStripStatusLabel lbl)
                    {
                        if (!lbl.Text.Contains("Usuario:"))
                        {
                            lbl.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        }
                    }
                }
            }
            catch (Exception) { }
        }

        // ========== METODO PARA ABRIR FORMULARIOS (SIMPLE) ==========
        private void AbrirFormulario(Form formulario)
        {
            formulario.StartPosition = FormStartPosition.CenterScreen;
            formulario.WindowState = FormWindowState.Maximized;
            formulario.Show();
            this.Hide(); // Ocultar el principal mientras el otro form esta abierto
        }

        // ========== MENU ARCHIVO ==========
        private void cambiarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea cerrar sesion?", "Cerrar sesion",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                new FrmLogin().Show();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro de salir?", "Salir",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // ========== MENU MANTENIMIENTO ==========
        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmProductos());
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmCategorias());
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmProveedores());
        }

        // ========== MENU INVENTARIO ==========
        private void entradaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmMovimientos());
        }

        private void salidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmMovimientos());
        }

        private void historialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Modulo en construccion", "Aviso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ========== MENU CONSULTAS ==========
        private void stockActualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormulario(new FrmConsultas());
        }

        private void buscarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Modulo en construccion", "Aviso",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ========== MENU AYUDA ==========
        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ALMACEN EL DORADO\nSistema de Gestion de Inventario\nVersion 1.0",
                "Acerca de", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ========== EVENTOS VACIOS ==========
        private void archivoToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void mantenimientoToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void cosultasToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e) { }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e) { }
        private void FrmPrincipal_Load(object sender, EventArgs e) { }
    }
}