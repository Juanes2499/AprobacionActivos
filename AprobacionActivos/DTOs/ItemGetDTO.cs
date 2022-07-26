using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AprobacionActivos.DTOs
{
    public class ItemGetDTO
    {
        [JsonPropertyName("ID")]
        public int ID { get; set; }

        [JsonPropertyName("NOMBRE_ACTIVO")]
        public string NOMBRE_ACTIVO { get; set; }
    }
}
