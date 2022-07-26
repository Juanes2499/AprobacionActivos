using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AprobacionActivos.Models
{
    [Table("TIPO_APROBACIONES")]
    public class TipoAprobacion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        [JsonPropertyName("ID")]
        public int ID { get; set; }

        [JsonPropertyName("NOMBRE_APROBACION")]
        public string NOMBRE_APROBACION { get; set; }

        #region Relaciones
        List<Aprobacion> aprobaciones { get; set; }
        #endregion
    }
}
