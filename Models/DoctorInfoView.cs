using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Vista_historial_medico_blockchain.Models{

    public partial class DoctorInfoView
    {


        [JsonInclude]

        public string Nombre { get; set; }

        [JsonInclude]
        public string Apellido { get; set; }

        [EmailAddress(ErrorMessage = "Debe ingresar un email válido")]
        [JsonInclude]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [JsonInclude]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare("Password")]
        [JsonInclude]
        public string ConfirmPassword { get; set; }

        [JsonInclude]
        public string PhoneNumber { get; set; }

        [JsonInclude]
        public string UserName { get; set; }

        [JsonInclude]
        public string AdminId { get; set; }

        [JsonInclude]
        public string EspecialidadId { get; set; }
    }
}