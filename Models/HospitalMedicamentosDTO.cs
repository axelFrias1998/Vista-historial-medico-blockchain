using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public class HospitlaMedicamentosDTO
    {
        
        [JsonInclude]
        public string id { get; set; }

        [JsonInclude]
        public string descripcion { get; set; }
        
        [JsonInclude]
        public string indicaciones { get; set; }

        [JsonInclude]
        public string viaAdministracion { get; set; }

        [JsonInclude]
        public string grupoMedicamentosId { get; set; }

        }
}
