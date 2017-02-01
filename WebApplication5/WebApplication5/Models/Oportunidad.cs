using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebApplication5.Models
{
    
    namespace ContactManager.Models
    {
        public class Oportunidad
        {
            public int OportunidadId { get; set; }

            public string Tema { get; set; }

            public String id { get; set; }
            public Guid guid { get; set; }
          
            public string Nombre { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }

            public DateTime FechadeNacimiento { get; set; }

            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

        }
    }
}