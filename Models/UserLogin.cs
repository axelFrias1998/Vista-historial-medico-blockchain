using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class UserLogin 
    {
        public UserLogin ()
        {
           // AspNetUsers = new HashSet<AspNetUserLogin>();
           // AspNetUserLogins = new HashSet<AspNetUserToken>();
        }
        
        [JsonInclude]
        public string Username { get; set; } 

        [DataType(DataType.Password)]
        [JsonInclude]
        /*[ContraseñaValidate(ErrorMessage = "Contraseña no valida")]*/
        public string Password { get; set; }             
    }
}