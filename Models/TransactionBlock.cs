using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Vista_historial_medico_blockchain.Models
{
    public class TransactionBlock
    {
        public string Id { get; set; }

        public DateTime TimeStamp { get; set; }

        
        public string Hash { get; set; }

        
        public string NextHash { get; set; }

        
        public string Type { get; set; }    

        
        public ConsultaMedica ConsultaMedica { get; set; }
    }
}