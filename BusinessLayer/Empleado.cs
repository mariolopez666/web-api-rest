﻿using System;

namespace EntityLayer
{
    public class Empleado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string DNI { get; set; }
    }
}
