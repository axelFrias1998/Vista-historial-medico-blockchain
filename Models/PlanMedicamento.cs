using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class PlanMedicamento
    {
        [DataType(DataType.Date)]
        [JsonInclude]
        public DateTime Init { get; set; }
    
        [JsonInclude]
        public string NombreMedicamento { get; set; }

        [DataType(DataType.MultilineText)]
        [JsonInclude]
        public string Indicaciones { get; set; }

        [DataType(DataType.Date)]
        [JsonInclude]
        public DateTime Finish { get; set; }
    }
}      