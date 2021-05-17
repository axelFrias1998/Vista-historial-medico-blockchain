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
           // AspNetUsers = new HashSet<AspNetUser>();
           // AspNetUserLogins = new HashSet<AspNetUserLogin>();
        }

        [JsonInclude]
        public string UserName { get; set; } 

        [DataType(DataType.Password)]
        [JsonInclude]
        public string Password { get; set; }

        
    }
}