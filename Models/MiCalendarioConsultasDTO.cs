using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;


namespace Vista_historial_medico_blockchain.Models
{
    public class MiCalendarioConsultasDTO
    {
        public List<MisConsultasDTO> MisConsultas { get; set; }

        public List<PlanMedicamento> MisMedicamentos { get; set; }
    }
}