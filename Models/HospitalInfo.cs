using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Vista_historial_medico_blockchain.Models
{
    public class HospitalInfo
    {

        
        [MaxLength(150, ErrorMessage = "El Nombre de usuario debe tener como máximo 150 caracteres")]
        [MinLength(3, ErrorMessage = "El Nombre de usuario debe tener al menos 3 caracteres")]
        public string Name { get; set; }

        
        [MaxLength(20, ErrorMessage = "El Teléfono de usuario debe tener como máximo 20 caracteres")]
        [MinLength(0, ErrorMessage = "El Teléfono de usuario debe tener al menos 1 caracter")]
        public string PhoneNumber { get; set; }

        
        public int ServiceCatalogId { get; set; }

        
       public string AdminId { get; set; }

       public virtual ServicesCatalog ServiceCatalog { get; set; }
    }
}