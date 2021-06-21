using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public class ServicesCatalog
    {
        
        [JsonInclude]
        public int Id { get; set; }
        
        [JsonInclude]
        public string Type { get; set; }
        [JsonInclude]
        public bool IsPublic { get; set; }
        
    }
}
