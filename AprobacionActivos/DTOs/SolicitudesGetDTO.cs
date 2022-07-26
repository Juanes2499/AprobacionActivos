using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AprobacionActivos.DTOs
{
    public class SolicitudesGetDTO
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }

        [JsonPropertyName("NOMBRE_LABORATORIO")]
        public string NOMBRE_LABORATORIO { get; set; }

        [JsonPropertyName("CODIGO")]
        public int CODIGO { get; set; }

        [JsonPropertyName("NOMBRES")]
        public string NOMBRES { get; set; }

        [JsonPropertyName("APELLIDOS")]
        public string APELLIDOS { get; set; }

        [JsonPropertyName("EMAIL")]
        public string EMAIL { get; set; }

        [JsonPropertyName("ESTADO_TRACKING")]
        public string ESTADO_TRACKING { get; set; }

        [JsonPropertyName("APROBADO")]
        public int? APROBADO { get; set; }

    }
}
