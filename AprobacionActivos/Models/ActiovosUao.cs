using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AprobacionActivos.Models
{
    [Table("ACTIVOS_UAO")]
    public class ActiovosUao
    {
        [Key, Column(Order = 0)]
        [JsonPropertyName("ID")]
        public int ID { get; set; }

        [JsonPropertyName("NOMBRE_ACTIVO")]
        public string NOMBRE_ACTIVO { get; set; }

        [JsonPropertyName("NOMBRE_LABORATORIO")]
        public string NOMBRE_LABORATORIO { get; set; }
    }
}
