using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class SpecialitiesCatalog
    {

        [JsonInclude]
        public string EspecialidadId { get; set; }

        [JsonInclude]
        public string Type { get; set; }

        [JsonInclude]
        public string Nombre { get; set; }

        //public virtual ICollection<Hospital> Hospitals { get; set; }
    }
}
