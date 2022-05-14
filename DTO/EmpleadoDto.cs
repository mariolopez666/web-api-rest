using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DTO
{
    /// <summary>
    /// Objeto con los datos del empleado.
    /// </summary>
    public class EmpleadoDto
    {
        /// <summary>
        /// Identificador del empleado
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre del Empleado.
        /// </summary>
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El campo {0} debe ser una cadena con una longitud mínima de {2} y una longitud máxima de {1}.")]
        [Required]
        public string Nombre { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        
        [Required(ErrorMessage = "El DNI es requerido.")]
        [RegularExpression(@"^\d{8}$", ErrorMessage = "El DNI no cumple con el formato.")]
        public string DNI { get; set; }
    }
}
