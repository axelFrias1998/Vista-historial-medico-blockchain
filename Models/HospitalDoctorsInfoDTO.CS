using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models{

    public partial class HospitalDoctorsInfoDTO{

        [JsonInclude]
        public string DoctorId { get; set; }

        [JsonInclude]

        public string Nombre { get; set; }

        [JsonInclude]

        public string Apellido { get; set; }

        [JsonInclude]

        public string Email { get; set; }

        [JsonInclude]

        public string Numero { get; set; }

        [JsonInclude]
        public string Especialidad { get; set; }
    }
}