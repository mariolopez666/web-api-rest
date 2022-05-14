using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplicationEjemplo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        //Mensaje cuando supera los 128 MB: Se superó el límite de longitud del cuerpo multiparte 134217728
        [DisableRequestSizeLimit]
        //Mensaje cuando el body supera los 70 MB: No se pudo leer el formulario de solicitud. Solicitud de cuerpo demasiado grande.
        //[RequestSizeLimit(73400320)] //hasta 128 MB en Linux
        //[RequestSizeLimit(73400320)]
        //Error al leer el formulario de solicitud. Se excedió el límite de longitud de cuerpo multiparte 157286400 (150mb)
        //MultipartBodyLengthLimit es para cada valor del formData
        //para que funcione se tiene que incluir el atributo DisableRequestSizeLimit para quitar el valor por defecto de 28mb
        [RequestFormLimits(MultipartBodyLengthLimit = 157286400)]
        public IActionResult Post([FromForm] ValueDto dto)
        {
            var files = Request.Form.Files;
            if (dto.Archivo is null)
            {
                return BadRequest(new { value = "sin archivo" });
            }
            return Ok(new
            {
                dto.Value,
                dto.Archivo.FileName,
                dto.Archivo.Length,
                KB = Math.Round(dto.Archivo.Length / 1024M, 2),
                MB = dto.Archivo.Length / 1024M / 1024M
            });
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
