using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ValueDto
    {
        public string Value { get; set; }
        public IFormFile Archivo { get; set; }
    }
}
