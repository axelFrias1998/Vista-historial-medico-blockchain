using System;
using System.Collections.Generic;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class Hospital
    {
        public Hospital()
        {
            AspNetUsers = new HashSet<AspNetUser>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegisterDate { get; set; }
        public int ServiceCatalogId { get; set; }
        public string AdminId { get; set; }

        public virtual AspNetUser Admin { get; set; }
        public virtual ServicesCatalog ServiceCatalog { get; set; }
        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
