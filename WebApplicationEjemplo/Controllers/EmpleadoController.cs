using AutoMapper;
using DataAccessLayer;
using DTO;
using EntityLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebApplicationEjemplo.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
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
        /// <summary>
        /// Obtener empleado por Identificador.
        /// </summary>
        /// <param name="id">Identificador del empleado.</param>
        [HttpGet("{id}")]
        public ActionResult<Empleado> ObtenerEmpleado(int id)
        {
            var empleadoDto = mapper.Map<EmpleadoDto>(repository.GetEmpleadoById(id));
            if (empleadoDto == null)
            {
                return NotFound($"No existe un empleado con ID = {id}");
            }
            else
            {
                return Ok(empleadoDto);
            }
        }

        [HttpGet]
        public ActionResult<List<EmpleadoDto>> ObtenerEmpleados()
        {
            var empleados = mapper.Map<List<EmpleadoDto>>(repository.GetEmpleados()) ;
            return empleados;
        }
        
        /// <summary>
        /// Metodo para registrar un empleado.
        /// </summary>
        /// <param name="empleadoDto">Objeto empleado.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<Empleado> Create([FromBody] EmpleadoDto empleadoDto)
        {
            //if (empleadoDto.DNI.Length != 8)
            //{
            //    return BadRequest("El DNI debe ser de 8 digitos.");
            //}
            try
            {
                var newempleado = repository.Registrar(empleadoDto);
                return CreatedAtAction(nameof(ObtenerEmpleado), new { id = newempleado.Id }, newempleado);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error interno en el servidor.");
            }
          
        }

        /// <summary>
        /// Metodo para registrar un empleado.
        /// </summary>
        /// <param name="empleadoDto">Objeto empleado.</param>
        /// <returns></returns>
        [HttpPost("registrar")]
        [Consumes("multipart/form-data")]
        public ActionResult<Empleado> CreateFromForm([FromForm] EmpleadoDto empleadoDto)
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
