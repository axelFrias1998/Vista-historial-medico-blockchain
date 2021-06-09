using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class CreateSpeciality
    {
        public CreateSpeciality ()
        {
           // AspNetUsers = new HashSet<AspNetUser>();
           // AspNetUserLogins = new HashSet<AspNetUserLogin>();
        }

        [JsonInclude]
        [Required(ErrorMessage = "El Nombre es un campo requerido")]
        [MaxLength(150, ErrorMessage = "El Nombre de usuario debe tener como máximo 100 caracteres")]
        [MinLength(0, ErrorMessage = "El Nombre de usuario debe tener al menos 1 caracter")]
        public string Name { get; set; }
    }
}

