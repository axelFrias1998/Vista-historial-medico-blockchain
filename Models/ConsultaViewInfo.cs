using System.Collections.Generic;

namespace Vista_historial_medico_blockchain.Models
{
    public class ConsultaViewInfo
    {
        public string GenNode { get; set; }

        public string PacientId { get; set; }

        public List<TransactionBlock> TransactionBlock { get; set; }
    }
}