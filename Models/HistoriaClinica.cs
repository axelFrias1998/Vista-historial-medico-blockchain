using System;
using System.Text.Json.Serialization;


namespace Vista_historial_medico_blockchain.Models
{
    public class HistoriaClinica
    {
        [JsonInclude]
        public DateTime FechaElaboracion { get; set; }

        [JsonInclude]
        public DateTime FechaNacimiento { get; set; }

        [JsonInclude]
        //1 = Mujer, 0 = hombre 
        public bool Sexo { get; set; }

        [JsonInclude]
        //Indirecto (1) o directo (0) 
        public bool Interrogatorio { get; set; }

        [JsonInclude]
        public string PadecimientosMadre { get; set; }
        
        [JsonInclude]
        public string PadecimientosPadre { get; set; }
        
        [JsonInclude]
        public string PadecimientosHermanos { get; set; }
        
        [JsonInclude]
        public string PadecimientosRamaMaterna { get; set; }
        
        [JsonInclude]
        public string PadecimientosRamaPaterna { get; set; }
        
        [JsonInclude]
        public string EsquemaVacunacion { get; set; }
        
        [JsonInclude]
        public string AntecedentesQuirurgicos { get; set; }
        
        [JsonInclude]
        public string AntecedentesTraumaticos { get; set; }
        
        [JsonInclude]
        public string AntecedentesAlergicos { get; set; }
        
        [JsonInclude]
        public string AntecedentesHospitalizaciones { get; set; }
    }
}