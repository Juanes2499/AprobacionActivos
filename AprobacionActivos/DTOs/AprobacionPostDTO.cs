using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AprobacionActivos.DTOs
{
    public class AprobacionPostDTO
    {
        [JsonPropertyName("SOLICITUD_ID")]
        public int SOLICITUD_ID { get; set; }

        [JsonPropertyName("EMAIL_APROBADOR")]
        public string EMAIL_APROBADOR { get; set; }

        [JsonPropertyName("APROBADO")]
        public int APROBADO { get; set; }

        [JsonPropertyName("COMENTARIOS")]
        public string COMENTARIOS { get; set; }
    }
}
