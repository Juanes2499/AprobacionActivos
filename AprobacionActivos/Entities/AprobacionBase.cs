using AprobacionActivos.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprobacionActivos.Entities
{
    public class AprobacionBase
    {
        public AprobacionPostDTO AprobacionDTO { get; set; } = new AprobacionPostDTO();
        public string MensajeAprobacion { get; set; }
        public string TipoAprobacion { get; set; }
        public string EstadoTrackingAnterior { get; set; }
        public string EstadoTrackingActual { get; set; }
        public string EstadoTrackingSiguiente { get; set; }
    }
}
