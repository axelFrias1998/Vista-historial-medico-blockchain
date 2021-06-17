using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class PlanMedicamento
    {
        [JsonInclude]
        public DateTime Init { get; set; }

        [JsonInclude]
        public string NombreMedicamento { get; set; }

        [JsonInclude]
        public string Indicaciones { get; set; }

        [JsonInclude]
        public DateTime Finish { get; set; }
    }
}      