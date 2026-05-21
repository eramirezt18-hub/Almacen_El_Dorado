using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Almacen_El_Dorado
{
    public partial class FrmPrincipal : Form
    {
        // Variable para guardar el usuario actual
        string usuarioActual = "admin";

        public FrmPrincipal()
        {
            InitializeComponent();

            // Configurar como contenedor MDI (para abrir formularios dentro)
            this.IsMdiContainer = true;

            // Maximizar ventana
            this.WindowState = FormWindowState.Maximized;

            // Buscar los labels en la StatusStrip y mostrar usuario
            MostrarUsuarioEnStatusStrip();

            // Iniciar el timer para la hora
            timer1.Start();
        }

        // Método para mostrar el usuario en la barra de estado
        private void MostrarUsuarioEnStatusStrip()
        {
            // Recorrer los elementos de la StatusStrip
            foreach (ToolStripItem item in statusStrip1.Items)
            {
                // Si es un ToolStripStatusLabel
                if (item is ToolStripStatusLabel)
                {
                    ToolStripStatusLabel lbl = item as ToolStripStatusLabel;

                    // Si el texto comienza con "Usuario:", actualizarlo
                    if (lbl.Text.Contains("Usuario:"))
                    {
                        lbl.Text = "👤 Usuario: " + usuarioActual;
                    }
                }
            }
        }

        // Evento: cuando el formulario se carga
        private void FrmPrincipal_Load(object sender, EventArgs e)
        {
            // Mostrar fecha actual en el título de la ventana (opcional)
            this.Text = "Almacén El Dorado - " + DateTime.Now.ToLongDateString();
        }

        // ========== TIMER: Actualiza la hora cada segundo ==========
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Recorrer los elementos de la StatusStrip
            foreach (ToolStripItem item in statusStrip1.Items)
            {
                // Si es un ToolStripStatusLabel
                if (item is ToolStripStatusLabel)
                {
                    ToolStripStatusLabel lbl = item as ToolStripStatusLabel;

                    // Si el texto NO contiene "Usuario:", es la hora
                    if (!lbl.Text.Contains("Usuario:"))
                    {
                        lbl.Text = "🕒 " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    }
                }
            }
        }

        // ========== MENÚ ARCHIVO ==========

        private void cambiarUsuarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Cerrar sesión y volver al login
            DialogResult respuesta = MessageBox.Show("¿Desea cerrar sesión?",
                "Cerrar sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                // Ocultar este formulario
                this.Hide();

                // Crear y mostrar el login
                FrmLogin login = new FrmLogin();
                login.Show();
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Confirmar salida
            DialogResult respuesta = MessageBox.Show("¿Está seguro de salir del sistema?",
                "Salir",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                Application.Exit(); // Cerrar toda la aplicación
            }
        }

        // ========== MENÚ MANTENIMIENTO ==========

        private void productosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abrir formulario de productos
            FrmProductos frm = new FrmProductos();
            frm.MdiParent = this; // Para que abra dentro del principal
            frm.Show();
        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abrir formulario de categorías
            FrmCategorias frm = new FrmCategorias();
            frm.MdiParent = this;
            frm.Show();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abrir formulario de proveedores
            FrmProveedores frm = new FrmProveedores();
            frm.MdiParent = this;
            frm.Show();
        }

        // ========== MENÚ INVENTARIO ==========

        private void entradaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abrir movimientos (entrada)
            FrmMovimientos frm = new FrmMovimientos();
            frm.MdiParent = this;
            frm.Show();
        }

        private void salidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abrir movimientos (salida)
            FrmMovimientos frm = new FrmMovimientos();
            frm.MdiParent = this;
            frm.Show();
        }

        private void historialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mostrar mensaje de que está en construcción
            MessageBox.Show("Módulo de historial en construcción\nPróximamente disponible.",
                "En desarrollo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ========== MENÚ CONSULTAS ==========

        private void stockActualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Abrir consulta de stock
            // FrmConsultaStock frm = new FrmConsultaStock();  // COMENTADO TEMPORALMENTE
            // frm.MdiParent = this;
            // frm.Show();

            // Temporal: mostrar mensaje
            MessageBox.Show("Módulo de consulta de stock en construcción",
                "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buscarProductoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mostrar mensaje de que está en construcción
            MessageBox.Show("Módulo de búsqueda avanzada en construcción\nPróximamente disponible.",
                "En desarrollo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ========== MENÚ AYUDA ==========

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Mostrar información del sistema
            MessageBox.Show("ALMACÉN EL DORADO\n" +
                "Sistema de Gestión de Inventario\n" +
                "Versión 1.0\n\n" +
                "Proyecto Final de Programación I\n" +
                "Universidad Mariano Gálvez de Guatemala\n" +
                "Campus Cobán\n\n" +
                "© 2024 - Todos los derechos reservados",
                "Acerca del sistema",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // ========== EVENTOS VACÍOS QUE NO NECESITAS (pero están por si acaso) ==========
        // Estos eventos no necesitan código, pero los dejo para que no tengas errores

        private void archivoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // No necesita código
        }

        private void mantenimientoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // No necesita código
        }

        private void inventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // No necesita código
        }

        private void cosultasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // No necesita código (tiene una "s" de más en "cosultas" pero lo dejamos igual)
        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // No necesita código
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // No necesita código
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // No necesita código
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // No necesita código
        }
    }
}