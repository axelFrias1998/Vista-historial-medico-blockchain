using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Vista_historial_medico_blockchain.Models;

namespace Vista_historial_medico_blockchain.Models
{
    public class HospitalInfo
    {

        [JsonInclude]
        public int hospitalId{ get;set;}

        [JsonInclude]
        [MaxLength(150, ErrorMessage = "El Nombre de usuario debe tener como máximo 150 caracteres")]
        [MinLength(3, ErrorMessage = "El Nombre de usuario debe tener al menos 3 caracteres")]
        public string Name { get; set; }

        [JsonInclude]
        [MaxLength(20, ErrorMessage = "El Teléfono de usuario debe tener como máximo 20 caracteres")]
        [MinLength(0, ErrorMessage = "El Teléfono de usuario debe tener al menos 1 caracter")]
        public string PhoneNumber { get; set; }

        [JsonInclude]
        public DateTime registerDate { get; set; }

        [JsonInclude]
        public UserInfo admins { get; set; }

        [JsonInclude]
        public SpecialitiesCatalog especialidades { get; set; }

        [JsonInclude]
        public ServicesCatalog servicesCatalog { get; set; }
    }
}