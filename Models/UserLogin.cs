using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class UserLogin
    {
        public UserLogin()
        {
            // AspNetUsers = new HashSet<AspNetUserLogin>();
            // AspNetUserLogins = new HashSet<AspNetUserToken>();
        }

        [JsonInclude]
        [Required(ErrorMessage = "El Username es un campo requerido")]
        public string Username { get; set; }

        [DataType(DataType.Password), Compare("Password")]
        [JsonInclude]
        [Required(ErrorMessage = "La confirmación de Password es un campo requerido")]
        public string Password { get; set; }
    }
}