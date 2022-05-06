using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer
{
    public class Empleado
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo nombre es requerido.")]
        //[MaxLength(50, ErrorMessage = "La cantidad maxima de caracteres es 50.")]
        //[MinLength(3, ErrorMessage = "La cantidad minima de caracteres es 3.")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El campo {0} debe ser una cadena con una longitud mínima de {2} y una longitud máxima de {1}.")]
        public string Nombre { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string DNI { get; set; }
        //[MinLength(1, ErrorMessage = "El campo {0} debe ser un tipo matriz con una longitud mínima de {1}.")]
        //[MaxLength(3, ErrorMessage = "El campo {0} debe ser un tipo matriz con una longitud maxima de {1}.")]
        //[Required]
        //public List<Numero> Numeros { get; set; }

    }

    public class Numero
    {
        [Required]
        public string Pais { get; set; }
        [Required]
        public string Celular { get; set; }
    }
}
