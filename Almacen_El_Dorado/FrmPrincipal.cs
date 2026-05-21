using System;
using System.Windows.Forms;

namespace Almacen_El_Dorado
{
    public partial class FrmPrincipal : Form
    {
        // variable para guardar el usuario actual
        string usuarioActual = "AdminSistema";

        public FrmPrincipal()
        {
            InitializeComponent();

            // configurar ventana normal (no maximizada)
            this.WindowState = FormWindowState.Normal;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new System.Drawing.Size(1024, 768);

            // mostrar usuario en barra de estado
            MostrarUsuarioEnStatusStrip();

            // iniciar el timer para la hora
            timer1.Start();
        }

        // metodo para mostrar el usuario en la barra de estado
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

        // timer para actualizar la hora cada segundo
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

        // metodo para abrir formularios
        private void AbrirFormulario(Form formulario)
        {
            formulario.StartPosition = FormStartPosition.CenterScreen;
            formulario.WindowState = FormWindowState.Normal;
            formulario.Size = new System.Drawing.Size(1024, 768);
            formulario.Show();
            this.Hide();
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