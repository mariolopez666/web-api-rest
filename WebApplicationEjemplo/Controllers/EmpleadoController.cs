using AutoMapper;
using DataAccessLayer;
using DTO;
using EntityLayer;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebApplicationEjemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly RepositoryEmpleado repository;
        private readonly IMapper mapper;

        public EmpleadoController(RepositoryEmpleado repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("{traceId}/{id}")]
        public Empleado ObtenerEmpleado(string traceId, int id)
        {
            var empleado = repository.GetEmpleadoById(id);
            return empleado;
        }

        [HttpGet]
        public ActionResult<List<EmpleadoDto>> ObtenerEmpleados()
        {
            var empleados = mapper.Map<List<EmpleadoDto>>(repository.GetEmpleados()) ;
            return empleados;
        }

        [HttpPost]
        public ActionResult<Empleado> Create([FromBody] EmpleadoDto empleadoDto)
        {
            //throw new Exception();
            var newempleado = repository.Registrar(empleadoDto);
            return CreatedAtAction(nameof(ObtenerEmpleado), new { traceId = Guid.NewGuid().ToString(), id = newempleado.Id }, newempleado);
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
