using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class Register
    {
        public Register()
        {
           // AspNetUsers = new HashSet<AspNetUser>();
           // AspNetUserLogins = new HashSet<AspNetUserLogin>();
        }

        [JsonInclude]
        public UserInfo UserInfo { get; set; }
        [JsonInclude]
        public string HospitalId { get; set; } 
    }
}