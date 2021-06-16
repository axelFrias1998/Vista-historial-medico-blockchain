using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class PlanMedicamento
    {
        
        [JsonInclude]
        public string init { get; set; }

        [JsonInclude]
        public string nombreMedicamento { get; set; }

        [JsonInclude]
        public string indicaciones { get; set; }

        [JsonInclude]
        public string finish { get; set; }


    }
}