using DataAccessLayer;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApplicationEjemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly RepositoryEmpleado repository;

        public EmpleadoController(RepositoryEmpleado repository)
        {
            this.repository = repository;
        }

        [HttpGet("{id}")]
        public Empleado ObtenerEmpleado(int id)
        {
            var empleado = repository.GetEmpleadoById(id);
            return empleado;
        }

        [HttpGet]
        public ActionResult<List<Empleado>> ObtenerEmpleados()
        {
            var empleados = repository.GetEmpleados();
            return empleados;
        }

        [HttpPost]
        public ActionResult<Empleado> Create([FromBody] Empleado empleado)
        {
            var newempleado = repository.Registrar(empleado);
            return CreatedAtAction(nameof(ObtenerEmpleado), new { id = newempleado.Id }, newempleado);
        }

        [HttpPut("{id}")]
        public ActionResult<Empleado> Update(int id, [FromBody] Empleado empleado)
        {
            var empleadoExist = repository.GetEmpleadoById(id);
            Empleado empleadoResponse = null;
            if (empleadoExist == null)
            {
                return NotFound($"No existe un empleado con ID = {id}");
            }
            if (id == empleado.Id)
            {
                empleadoResponse = repository.Update(empleado);
            }
            return empleadoResponse;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var empleado = repository.GetEmpleadoById(id);
            if (empleado == null)
            {
                return NotFound($"No existe un empleado con ID = {id}");
            }

            repository.DeleteEmpleado(id);
            return NoContent();
        }


    }
}
