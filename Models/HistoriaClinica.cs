using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Vista_historial_medico_blockchain.Models
{
    public class HistoriaClinica
    {
        [DataType(DataType.Date)]
        [JsonInclude]
        public DateTime FechaElaboracion { get; set; }

        [DataType(DataType.Date)]
        [JsonInclude]
        public DateTime FechaNacimiento { get; set; }

        [JsonInclude]
        //1 = Mujer, 0 = hombre 
        public bool Sexo { get; set; }

        [JsonInclude]
        //Indirecto (1) o directo (0) 
        public bool Interrogatorio { get; set; }

        [DataType(DataType.MultilineText)]
        [JsonInclude]
        public string PadecimientosMadre { get; set; }
        
        [DataType(DataType.MultilineText)]
        [JsonInclude]
        public string PadecimientosPadre { get; set; }
        
        [DataType(DataType.MultilineText)]
        [JsonInclude]
        public string PadecimientosHermanos { get; set; }
        
        [DataType(DataType.MultilineText)]
        [JsonInclude]
        public string PadecimientosRamaMaterna { get; set; }
        
        [DataType(DataType.MultilineText)]
        [JsonInclude]
        public string PadecimientosRamaPaterna { get; set; }
        
        [DataType(DataType.MultilineText)]
        [JsonInclude]
        public string EsquemaVacunacion { get; set; }
        
        [DataType(DataType.MultilineText)]
        [JsonInclude]
        public string AntecedentesQuirurgicos { get; set; }
        
        [DataType(DataType.MultilineText)]
        [JsonInclude]
        public string AntecedentesTraumaticos { get; set; }

        [DataType(DataType.MultilineText)]       
        [JsonInclude]
        public string AntecedentesAlergicos { get; set; }
        
        [DataType(DataType.MultilineText)]
        [JsonInclude]
        public string AntecedentesHospitalizaciones { get; set; }
    }
}