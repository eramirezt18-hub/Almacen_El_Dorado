using System;
using System.Windows.Forms;
using Almacen_El_Dorado.Database;
using System.Data.SqlClient;

namespace Almacen_El_Dorado
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();

            // configurar la contraseña para que muestre asteriscos
            txtPassword.PasswordChar = '*';

            // ocultar mensaje de error al inicio
            lblError.Text = "";
        }

        // evento cuando el usuario cambia el texto
        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            // limpiar mensaje de error cuando el usuario empieza a escribir
            lblError.Text = "";
        }

        // evento cuando la contraseña cambia
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // limpiar mensaje de error cuando escribe la contraseña
            lblError.Text = "";
        }

        // evento del boton iniciar sesion
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // credenciales profesionales
            string usuarioCorrecto = "AdminSistema";
            string passwordCorrecto = "Inventario2024";

            // verificar si el usuario y contraseña son correctos
            if (txtUsuario.Text == usuarioCorrecto && txtPassword.Text == passwordCorrecto)
            {
                // abrir el formulario principal
                FrmPrincipal principal = new FrmPrincipal();
                principal.Show();

                // ocultar el login
                this.Hide();
            }
            else
            {
                // mostrar mensaje de error
                lblError.Text = "Usuario o contraseña incorrectos";
                lblError.ForeColor = System.Drawing.Color.Red;

                // limpiar los campos
                txtUsuario.Text = "";
                txtPassword.Text = "";

                // poner el foco en el campo usuario
                txtUsuario.Focus();
            }
        }

        // evento del boton salir
        private void btnSalir_Click(object sender, EventArgs e)
        {
            // preguntar si realmente quiere salir
            DialogResult respuesta = MessageBox.Show("Desea salir del sistema?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
            {
                // cerrar toda la aplicacion
                Application.Exit();
            }
        }

        // evento cuando el formulario se carga
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            // centrar el formulario en la pantalla
            this.StartPosition = FormStartPosition.CenterScreen;

            // poner el foco en el campo usuario
            txtUsuario.Focus();
        }

        // boton para probar conexion (opcional)
        private void btnProbarConexion_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString))
                {
                    conn.Open();
                    MessageBox.Show("Conexion exitosa a la base de datos!", "Exito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexion: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}