using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class CreateCosultaDTO
    {
        
        [JsonInclude]
        public string pacienteId { get; set; }

        [JsonInclude]
        public string doctorId{ get; set; }

        [JsonInclude]
        public string hospitalId{ get; set; }

        [JsonInclude]
        public string consultaMedica { get; set; }

        [JsonInclude]
        public string genNodeId { get; set; }

    }
}