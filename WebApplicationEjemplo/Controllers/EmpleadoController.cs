using DataAccessLayer;
using EntityLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationEjemplo.Models;

namespace WebApplicationEjemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        [HttpGet]
        public string Saludo()
        {
           
            return $"hola mundo";
        }

        [HttpGet("saludodos")]
        public string SaludoDos()
        {
            return "hola mundo dos";

        }

        [HttpGet("obtener-numero")]
        public int ObtenerNumero()
        {
            return 1000000;
        }

        [HttpGet("obtener-empleado")]
        public Empleado ObtenerEmpleado()
        {
            Empleado empleado = new Empleado();
            empleado.Id = 1;
            empleado.Nombre = "Jordan";
            empleado.ApellidoPaterno = "Jamanca";
            empleado.ApellidoMaterno = "De la Torre";
            empleado.DNI = "45321655";
            empleado.FechaNacimiento = DateTime.Today;

            return empleado;
        }

        [HttpGet("obtener-empleados")]
        public List<Empleado> ObtenerEmpleados()
        {
            Empleado empleadoJordan = new Empleado();
            empleadoJordan.Id = 1;
            empleadoJordan.Nombre = "Jordan";
            empleadoJordan.ApellidoPaterno = "Jamanca";
            empleadoJordan.ApellidoMaterno = "De la Torre";
            empleadoJordan.DNI = "45321655";
            empleadoJordan.FechaNacimiento = DateTime.Today;

            Empleado empleadoMarvin = new Empleado();
            empleadoMarvin.Id = 2;
            empleadoMarvin.Nombre = "Marvin";
            empleadoMarvin.ApellidoPaterno = "Meza";
            empleadoMarvin.ApellidoMaterno = "Odar";
            empleadoMarvin.DNI = "45696485";
            empleadoMarvin.FechaNacimiento = new DateTime(1989, 4, 30);

            List<Empleado> listaEmpleados = new List<Empleado>();
            listaEmpleados.Add(empleadoJordan);
            listaEmpleados.Add(empleadoMarvin);

            return listaEmpleados;
        }

        [HttpPost]
        public Empleado Create([FromBody] Empleado empleado)
        {
            var repositoryEmpleado = new RepositoryEmpleado();
            repositoryEmpleado.Registrar(empleado);
            return empleado;
        }

        [HttpPut("{id}")]
        public Empleado Update(int id, [FromBody] Empleado empleado)
        {
            empleado.Nombre = $"{empleado.Nombre} {empleado.Nombre}";
            return empleado;
        }

        [HttpDelete("{id}")]
        public int Delete(int id)
        {
            return id;
        }


    }
}
