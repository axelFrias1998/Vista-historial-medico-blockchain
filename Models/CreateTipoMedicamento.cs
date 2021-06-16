using System;

using System.Collections.Generic;

using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class CreateTipoMedicamento
    {

        [JsonInclude]
        public string Type { get; set; }      
    }
}