using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AprobacionActivos.Models
{
    [Table("APROBACIONES")]
    public class Aprobacion
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


        [JsonPropertyName("EMAIL_APROBADOR")]
        public string EMAIL_APROBADOR { get; set; }


        [JsonPropertyName("TIPO_APROBACION_ID")]
        public int TIPO_APROBACION_ID { get; set; }

        [JsonPropertyName("TIPO_APROBACION")]
        [ForeignKey("TIPO_APROBACION_ID")]
        public TipoAprobacion TIPO_APROBACION { get; set; }


        [JsonPropertyName("APROBADO")]
        public int? APROBADO { get; set; }


        [JsonPropertyName("COMENTARIOS")]
        public string COMENTARIOS { get; set; }
    }
}
