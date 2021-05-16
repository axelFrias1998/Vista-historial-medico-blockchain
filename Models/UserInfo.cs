using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class UserInfo 
    {
        public UserInfo ()
        {
           // AspNetUsers = new HashSet<AspNetUser>();
           // AspNetUserLogins = new HashSet<AspNetUserLogin>();
        }

        [JsonInclude]
        [MaxLength(100, ErrorMessage = "El Apellido de usuario debe tener como máximo 100 caracteres")]
        [MinLength(1, ErrorMessage = "El Apellido de usuario debe tener al menos 1 caracter")]
        public string Apellido { get; set; }
        
        [JsonInclude]
        [EmailAddress(ErrorMessage = "Debe ingresar un email válido")]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        [JsonInclude]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare("Password")]
        [JsonInclude]
        public string ConfirmPassword { get; set; }
        
        [JsonInclude]
        [MaxLength(100, ErrorMessage = "El Nombre de usuario debe tener como máximo 100 caracteres")]
        [MinLength(1, ErrorMessage = "El Nombre de usuario debe tener al menos 1 caracter")]
        public string Nombre { get; set; }
        
        [JsonInclude]
        public string PhoneNumber { get; set; }
        
        [JsonInclude]
        public string UserName { get; set; } 
    }
}