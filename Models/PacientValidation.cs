using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class PacientValidation
    {
        //public PacientValidation()
        //{
        //    
        //}

        [JsonInclude]
        [Required(ErrorMessage = "El Username es un campo requerido")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [JsonInclude]
        public string Password { get; set; }

        [Display(Name = "File")]
        [Required(ErrorMessage = "Elige tu archivo")]
        public IFormFile File { get; set; }
    }
}