using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AprobacionActivos.Models
{
    [Table("TRACKING_SOLICITUDES")]
    public class TrackingSolicitud
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("SOLICITUD_ID")]
        public int SOLICITUD_ID { get; set; }
        [JsonPropertyName("SOLICITUD")]
        [ForeignKey("SOLICITUD_ID")]
        public Solicitud SOLICITUD { get; set; }
        [JsonPropertyName("ESTADO")]
        public string ESTADO { get; set; }
        [JsonPropertyName("FECHA_CREACION")]
        public DateTime FECHA_CREACION { get; set; } = DateTime.Now;
        [JsonPropertyName("FECHA_ACTUALIZACION")]
        public DateTime FECHA_ACTUALIZACION { get; set; } = DateTime.Now;
    }
}
