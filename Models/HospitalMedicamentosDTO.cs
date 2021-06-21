using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public class HospitalMedicamentosDTO
    {
        [JsonInclude]
        public int id { get; set; }

        [JsonInclude]
        public string descripcion { get; set; }
        
        [JsonInclude]
        public string nombreMedicamento { get; set; }
        
        
        [JsonInclude]
        public string indicaciones { get; set; }

        [JsonInclude]
        public string viaAdministracion { get; set; }

        [JsonInclude]
        public string grupoMedicamentos { get; set; }

        [JsonInclude]
        public string precauciones { get; set; }
        
        [JsonInclude]
        public string efectosSecundarios { get; set; }
    }
}
