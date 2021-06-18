using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class ConsultaMedica
    {
        [DataType(DataType.MultilineText)]
        [JsonInclude]
        /////////////SUBJETIVO
        //Dolencia principal, enfermedad actual, apropiada revisión por sistemas
        public string Dolencia { get; set; }

        [DataType(DataType.MultilineText)]
        [JsonInclude]
        //Partes apropiadas o significativas de la historia médica pasada
        public string RelacionPasada { get; set; }

        [DataType(DataType.MultilineText)]
        [JsonInclude]
        /////////////OBJETIVO
        //Hallazgos al examen físico
        public string Hallazgos { get; set; }

        [DataType(DataType.MultilineText)]
        [JsonInclude]
        //Pruebas diagnósticas
        public string PruebasDiagnosticas { get; set; }

        [DataType(DataType.MultilineText)]
        [JsonInclude]
        /////////ANALISIS
        //Resumen del paciente
        public string Resumen { get; set; }

        [DataType(DataType.MultilineText)]
        [JsonInclude]
        //Sus problemas
        public string Problemas { get; set; }

        [DataType(DataType.MultilineText)]
        [JsonInclude]
        //Posibles diferenciales
        public string Diferenciales { get; set; }

        [DataType(DataType.MultilineText)]
        [JsonInclude]
        //Razonamiento clínico
        public string Razonamiento { get; set; }

        [DataType(DataType.MultilineText)]
        [JsonInclude]
       //////////PLAN
       //Pruebas Dx
        public string Pruebas { get; set; }

        [DataType(DataType.MultilineText)]
        [JsonInclude]
        public List<PlanMedicamento> PlanMedicamentos { get; set; }
        
        [DataType(DataType.MultilineText)]
        [JsonInclude]
        public string PlanTerapeutico { get; set; }
        
        [DataType(DataType.MultilineText)]
        [JsonInclude]
        //Indicaciones o recomendaciones
        public string Educacion { get; set; }

        [DataType(DataType.MultilineText)]
        [JsonInclude]
        public string Seguimiento { get; set; }

        public HistoriaClinica HistoriaClinica { get; set; }
    }
}