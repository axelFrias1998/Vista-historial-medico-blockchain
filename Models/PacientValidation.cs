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

        [JsonInclude]
        [Required(ErrorMessage = "El Username es un campo requerido")]
        public string Username { get; set; }

        [DataType(DataType.Password), Compare("Password")]
        [JsonInclude]
        [Required(ErrorMessage = "La confirmación de Password es un campo requerido")]
        public string Password { get; set; }

        [Display(Name = "File")]
        [Required(ErrorMessage = "Elige tu archivo")]
        public IFormFile File { get; set; }

        [FileExtensions(Extensions = "gti")]
        public string FileName => File?.FileName;
    }
}