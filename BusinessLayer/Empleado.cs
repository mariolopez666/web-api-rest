using System;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer
{
    public class Empleado
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido.")]
        [MaxLength(50, ErrorMessage = "La cantidad maxima de caracteres es 50.")]
        [MinLength(3, ErrorMessage = "La cantidad minima de caracteres es 3.")]
        public string Nombre { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string DNI { get; set; }
    }
}
