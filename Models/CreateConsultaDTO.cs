using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class CreateConsultaDTO
    {
        [JsonInclude]
        public string PacienteId { get; set; }

        [JsonInclude]
        public string DoctorId { get; set; }

        [JsonInclude]
        public string HospitalId { get; set; }

        [JsonInclude]
        public ConsultaMedica ConsultaMedica { get; set; }
        
        [JsonInclude]
        public string GenNodeId { get; set; }
    }
}