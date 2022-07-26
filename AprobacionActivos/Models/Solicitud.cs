using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AprobacionActivos.Models
{
    [Table("SOLICITUDES")]
    public class Solicitud
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        [JsonPropertyName("ID")]
        public int ID { get; set; }
        [JsonPropertyName("NOMBRES")]
        public string NOMBRES { get; set; }
        [JsonPropertyName("APELLIDOS")]
        public string APELLIDOS { get; set; }
        [JsonPropertyName("EMAIL")]
        public string EMAIL { get; set; }
        [JsonPropertyName("CODIGO")]
        public int CODIGO { get; set; }
        [JsonPropertyName("COMENTARIOS")]
        public string COMENTARIOS { get; set; }
        [JsonPropertyName("ACTIVO_ID")]
        public int ACTIVO_ID { get; set; }

        #region Relations
        List<TrackingSolicitud> trackingSolicitudes { get; set; }
        List<Aprobacion> aprobaciones { get; set; }
        #endregion
    }
}
