using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public class CreatedUserDTO
    {
        
        [JsonInclude]
        public string id { get; set; }
        [JsonInclude]
        public string nombre { get; set; }
        [JsonInclude]
        public string apellido { get; set; }

        [JsonInclude]
        public string userName { get; set; }
        
        [JsonInclude]
        public string email { get; set; }

        [JsonInclude]
        public string phoneNumber { get; set; }
    }
}
