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
        public class Lead
        {
            public int LeadId { get; set; }

            public String  id { get; set; }

            public Guid guid { get; set; }

            public string Tema { get; set; }// subject
            public string FirstName { get; set; } // fistname
            public string LastName { get; set; }// lastname
            
            public string Apellido_Materno { get; set; }//new_apellidomaterno
            public string Celular { get; set; } //mobilephone
            public DateTime Fecha_de_Nacimiento { get; set; } // Fec
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }

        }
    }
}