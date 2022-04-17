using System;
using System.Data;
using System.Data.SqlClient;
using EntityLayer;

namespace DataAccessLayer
{
    public class RepositoryEmpleado
    {
        public void Registrar(Empleado empleado)
        {
            //string cadenaConexión = "data source=(local); Database=DoucheBD; Trusted_Connection=true ";
            var csb = new SqlConnectionStringBuilder()
            {
                DataSource = "(local)",
                InitialCatalog = "DoucheBD",
                UserID = "sa",
                Password = "S0p0rte"
            };
            using (var sqlConnection = new SqlConnection(csb.ConnectionString))
            {
                var command = new SqlCommand("Insert_Empleado", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Nombre", empleado.Nombre));
                command.Parameters.Add(new SqlParameter("@ApellidoPaterno", empleado.ApellidoPaterno));
                command.Parameters.Add(new SqlParameter("@ApellidoMaterno", empleado.ApellidoMaterno));
                command.Parameters.Add(new SqlParameter("@FechaNacimiento", empleado.FechaNacimiento));
                command.Parameters.Add(new SqlParameter("@DNI", empleado.DNI));
                sqlConnection.Open();
                int registroAfectados = command.ExecuteNonQuery();

            }
        }
    }
}
