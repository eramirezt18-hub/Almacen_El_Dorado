using System.Data.SqlClient;

namespace Almacen_El_Dorado.Database
{
    public class Conexion
    {
        public static SqlConnection ObtenerConexion()
        {
            SqlConnection conn = new SqlConnection(DatabaseHelper.ConnectionString);
            conn.Open();
            return conn;
        }
    }
}