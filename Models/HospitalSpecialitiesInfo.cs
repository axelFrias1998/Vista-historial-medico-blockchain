using System.Collections.Generic;

namespace Vista_historial_medico_blockchain.Models
{
    public class HospitalSpecialitiesInfo
    {
        public List<SpecialitiesCatalog> Specialities { get; set; }

        public string HospitalId { get; set; }
    }
}