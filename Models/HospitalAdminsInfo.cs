using System.Collections.Generic;

namespace Vista_historial_medico_blockchain.Models
{
    public class HospitalAdminsInfo
    {
        public List<HospitalAdminDTO> Admins { get; set; }

        public string HospitalId { get; set; }
    }
}