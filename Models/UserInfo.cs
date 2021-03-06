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
        [Required(ErrorMessage = "El Apellido es un campo requerido")]
        [MaxLength(100, ErrorMessage = "El Apellido de usuario debe tener como máximo 100 caracteres")]
        [MinLength(1, ErrorMessage = "El Apellido de usuario debe tener al menos 1 caracter")]
        public string Apellido { get; set; }
        
        [JsonInclude]
        [Required(ErrorMessage = "El Email es un campo requerido")]
        [EmailAddress(ErrorMessage = "Debe ingresar un email válido")]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        [JsonInclude]
        [Required(ErrorMessage = "El Password es un campo requerido")]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare("Password")]
        [JsonInclude]
        [Required(ErrorMessage = "La confirmación de Password es un campo requerido")]
        public string ConfirmPassword { get; set; }
        
        [JsonInclude]
        [Required(ErrorMessage = "El Nombre es un campo requerido")]
        [MaxLength(100, ErrorMessage = "El Nombre de usuario debe tener como máximo 100 caracteres")]
        [MinLength(1, ErrorMessage = "El Nombre de usuario debe tener al menos 1 caracter")]
        public string Nombre { get; set; }
        
        [JsonInclude]
        [Required(ErrorMessage = "El Teléfono es un campo requerido")]
        public string PhoneNumber { get; set; }
        
        [JsonInclude]
        [Required(ErrorMessage = "El Username es un campo requerido")]
        public string Username { get; set; } 

        public string SelectedUsuario {get;set;}
    }
}