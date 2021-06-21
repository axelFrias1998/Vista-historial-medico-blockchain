using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vista_historial_medico_blockchain.Models
{
    public class HospitalConsultasDTO
    {
        public DateTime DateStamp { get; set; }

        public string Paciente { get; set; }

        public string Doctor { get; set; }

    }
}