using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#nullable disable

namespace Vista_historial_medico_blockchain.Models
{
    public partial class ConsultaMedica
    {
        [JsonInclude]
        /////////////SUBJETIVO
        //Dolencia principal, enfermedad actual, apropiada revisión por sistemas
        public string Dolencia { get; set; }

        [JsonInclude]
        //Partes apropiadas o significativas de la historia médica pasada
        public string RelacionPasada { get; set; }

        [JsonInclude]
        /////////////OBJETIVO
        //Hallazgos al examen físico
        public string Hallazgos { get; set; }

        [JsonInclude]
        //Pruebas diagnósticas
        public string PruebasDiagnosticas { get; set; }

        [JsonInclude]
        /////////ANALISIS
        //Resumen del paciente
        public string Resumen { get; set; }

        [JsonInclude]
        //Sus problemas
        public string Problemas { get; set; }

        [JsonInclude]
        //Posibles diferenciales
        public string Diferenciales { get; set; }

        [JsonInclude]
        //Razonamiento clínico
        public string Razonamiento { get; set; }

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