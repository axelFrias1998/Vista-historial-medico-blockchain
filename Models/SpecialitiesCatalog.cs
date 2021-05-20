using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class SpecialitiesCatalog
    {
        public SpecialitiesCatalog()
        {
            //Hospitals = new HashSet<Hospital>();
        }

        [JsonInclude]
        public int Id { get; set; }

        [JsonInclude]
        public string Type { get; set; }

        [JsonInclude]
        public string Nombre { get; set; }

        //public virtual ICollection<Hospital> Hospitals { get; set; }
    }
}
