using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class IdentityRol
    {
        public IdentityRol()
        {
            //AspNetUsers = new HashSet<AspNetUser>();
        }

        [JsonInclude]
        public string id { get; set; }
        [JsonInclude]
        public string name { get; set; }
        [JsonInclude]
        public string normalizedName { get; set; }
        [JsonInclude]
        public string concurrencyStamp { get; set; }
    }
}