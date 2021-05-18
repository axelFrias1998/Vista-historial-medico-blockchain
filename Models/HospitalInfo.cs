using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Vista_historial_medico_blockchain.Models
{
    public class HospitalInfo
    {

        [JsonInclude]
        [MaxLength(150, ErrorMessage = "El Nombre de usuario debe tener como máximo 150 caracteres")]
        [MinLength(3, ErrorMessage = "El Nombre de usuario debe tener al menos 3 caracteres")]
        public string Name { get; set; }

        [JsonInclude]
        [MaxLength(20, ErrorMessage = "El Teléfono de usuario debe tener como máximo 20 caracteres")]
        [MinLength(0, ErrorMessage = "El Teléfono de usuario debe tener al menos 1 caracter")]
        public string PhoneNumber { get; set; }

        [JsonInclude]
        public int ServiceCatalogId { get; set; }

        [JsonInclude]
       public string AdminId { get; set; }
    }
}