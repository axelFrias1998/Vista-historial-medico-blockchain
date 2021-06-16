using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class CosultaMedica
    {
        
        [JsonInclude]
        public string dolencia { get; set; }

        [JsonInclude]
        public string relacionPasada { get; set; }

        [JsonInclude]
        public string hallazgos { get; set; }

        [JsonInclude]
        public string pruebasDiagnosticas { get; set; }

        [JsonInclude]
        public string resumen { get; set; }

        [JsonInclude]
        public string problemas { get; set; }

        [JsonInclude]
        public string diferenciales { get; set; }

        [JsonInclude]
        public string razonamiento { get; set; }

        [JsonInclude]
        public string pruebas { get; set; }

        [JsonInclude]
        public string planMedicamentos { get; set; }

        [JsonInclude]
        public string planTerapeutico { get; set; }

        [JsonInclude]
        public string educacion { get; set; }

        [JsonInclude]
        public string seguimiento { get; set; }

    }
}