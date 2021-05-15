using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class Hospital
    {
        public Hospital()
        {
            AspNetUsers = new HashSet<AspNetUser>();
        }

        [JsonInclude]
        public string Id { get; set; }
        [JsonInclude]
        public string Name { get; set; }
        [JsonInclude]
        public string PhoneNumber { get; set; }
        [JsonInclude]
        public DateTime RegisterDate { get; set; }
        [JsonInclude]
        public int ServiceCatalogId { get; set; }
        [JsonInclude]
        public string AdminId { get; set; }

        public virtual AspNetUser Admin { get; set; }
        public virtual ServicesCatalog ServiceCatalog { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
