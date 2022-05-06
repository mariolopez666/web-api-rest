using AutoMapper;
using DTO;
using EntityLayer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class RepositoryEmpleado
    {
        private string cadenaConexion = null;
        private readonly IMapper mapper;
        public RepositoryEmpleado(IConfiguration configuration, IMapper mapper)
        {
            cadenaConexion = configuration.GetConnectionString("DefaultConnection");
            this.mapper = mapper;
        }
        public Empleado Registrar(EmpleadoDto empleadoDto)
        {
            var empleado = mapper.Map<Empleado>(empleadoDto);
            using (var sqlConnection = new SqlConnection(cadenaConexion))
            {
                var command = new SqlCommand("Insert_Empleado", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Direction = ParameterDirection.Output });
                command.Parameters.Add(new SqlParameter("@Nombre", empleado.Nombre));
                command.Parameters.Add(new SqlParameter("@ApellidoPaterno", empleado.ApellidoPaterno));
                command.Parameters.Add(new SqlParameter("@ApellidoMaterno", empleado.ApellidoMaterno));
                command.Parameters.Add(new SqlParameter("@FechaNacimiento", empleado.FechaNacimiento));
                command.Parameters.Add(new SqlParameter("@DNI", empleado.DNI));
                sqlConnection.Open();
                int registroAfectados = command.ExecuteNonQuery();
                int newId = (int)command.Parameters[0].Value;
                var newEmpleado = GetEmpleadoById(newId);
                return newEmpleado;
            }
        }

        public Empleado Update(Empleado empleado)
        {
            using (var sqlConnection = new SqlConnection(cadenaConexion))
            {
                var command = new SqlCommand("Update_Empleado", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = empleado.Id });
                command.Parameters.Add(new SqlParameter("@Nombre", empleado.Nombre));
                command.Parameters.Add(new SqlParameter("@ApellidoPaterno", empleado.ApellidoPaterno));
                command.Parameters.Add(new SqlParameter("@ApellidoMaterno", empleado.ApellidoMaterno));
                command.Parameters.Add(new SqlParameter("@FechaNacimiento", empleado.FechaNacimiento));
                command.Parameters.Add(new SqlParameter("@DNI", empleado.DNI));
                sqlConnection.Open();
                int registroAfectados = command.ExecuteNonQuery();
                var newEmpleado = GetEmpleadoById(empleado.Id);
                return newEmpleado;
            }
        }

        public Empleado GetEmpleadoById(int Id)
        {
            Empleado empleado = null;
            using (var sqlConnection = new SqlConnection(cadenaConexion))
            {
                var command = new SqlCommand("GetEmpleadoById", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                sqlConnection.Open();
                SqlDataReader dr = command.ExecuteReader();
                
                if (dr.Read())
                {
                    empleado = new Empleado();
                    empleado.Id = (int)dr["Id"];
                    empleado.Nombre = dr["Nombre"].ToString();
                    empleado.ApellidoPaterno = dr["ApellidoPaterno"].ToString();
                    empleado.ApellidoMaterno = dr["ApellidoMaterno"].ToString();
                    empleado.FechaNacimiento = (DateTime)dr["FechaNacimiento"];
                    empleado.DNI = dr["DNI"].ToString();
                }
            }
            return empleado;
        }

        public List<Empleado> GetEmpleados()
        {
            List<Empleado> listaEmpleados = new List<Empleado>();
            using (var sqlConnection = new SqlConnection(cadenaConexion))
            {
                var command = new SqlCommand("GetEmpleados", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                sqlConnection.Open();
                SqlDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    var empleado = new Empleado();
                    empleado.Id = (int)dr["Id"];
                    empleado.Nombre = dr["Nombre"].ToString();
                    empleado.ApellidoPaterno = dr["ApellidoPaterno"].ToString();
                    empleado.ApellidoMaterno = dr["ApellidoMaterno"].ToString();
                    empleado.FechaNacimiento = (DateTime)dr["FechaNacimiento"];
                    empleado.DNI = dr["DNI"].ToString();
                    listaEmpleados.Add(empleado);
                }
            }
            return listaEmpleados;
        }
        public int DeleteEmpleado(int Id)
        {
            using (var sqlConnection = new SqlConnection(cadenaConexion))
            {
                var command = new SqlCommand("[Delete_Empleado]", sqlConnection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = Id });
                sqlConnection.Open();
                int ra = command.ExecuteNonQuery();
                return ra;
            }
        }
    }
}
