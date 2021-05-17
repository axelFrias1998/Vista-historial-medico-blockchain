using System;
using System.Text.Json.Serialization;

namespace Vista_historial_medico_blockchain.Models
{
    public class UserToken
    {

        [JsonInclude]
        public string Token { get; set; }

        [JsonInclude]
        public DateTime Expiration { get; set; }
    }
}