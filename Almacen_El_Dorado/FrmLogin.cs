using System;
using System.Windows.Forms;

namespace Almacen_El_Dorado
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();

            // Configurar la contraseña para que muestre *
            txtPassword.PasswordChar = '*';

            // Ocultar mensaje de error al inicio
            lblError.Text = "";
        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            // Limpiar mensaje de error cuando el usuario empieza a escribir
            lblError.Text = "";
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Limpiar mensaje de error cuando escribe la contraseña
            lblError.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Usuario y contraseña correctos
            string usuarioCorrecto = "admin";
            string passwordCorrecto = "1234";

            // Verificar si el usuario y contraseña son correctos
            if (txtUsuario.Text == usuarioCorrecto && txtPassword.Text == passwordCorrecto)
            {
                // Abrir el formulario principal
                FrmPrincipal principal = new FrmPrincipal();
                principal.Show();

                // Ocultar el login
                this.Hide();
            }
            else
            {
                // Mostrar mensaje de error
                lblError.Text = "❌ Usuario o contraseña incorrectos";
                lblError.ForeColor = System.Drawing.Color.Red;

                // Limpiar los campos
                txtUsuario.Text = "";
                txtPassword.Text = "";

                // Poner el foco en el campo usuario
                txtUsuario.Focus();
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            // Preguntar si realmente quiere salir
            DialogResult respuesta = MessageBox.Show("¿Está seguro de salir?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                // Cerrar toda la aplicación
                Application.Exit();
            }
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            // Centrar el formulario en la pantalla
            this.StartPosition = FormStartPosition.CenterScreen;

            // Poner el foco en el campo usuario
            txtUsuario.Focus();
        }
    }
}