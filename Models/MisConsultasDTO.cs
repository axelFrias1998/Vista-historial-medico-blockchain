using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;


namespace Vista_historial_medico_blockchain.Models
{
    public class MisConsultasDTO
    {
        public string NombreDoctor { get; set; }

        public string NombreHospital { get; set; }

        public DateTime FechaConsulta { get; set; }
    }
}