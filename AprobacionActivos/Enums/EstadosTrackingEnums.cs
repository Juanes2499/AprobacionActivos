using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprobacionActivos.Enums
{
    public class EstadosTrackingEnums
    {
        public const string SolicitudCreada = "Solicitud creada";
        public const string PendienteLaboratorio = "Pendiente respuesta laboratorio";
        public const string RespuestaLaboratorio = "Respuesta laboratorio dada";
        public const string PendientePorteria = "Pendiente respuesta porteria";
        public const string RespuestaPorteria = "Respuesta porteria dada";
        public const string SolicitudCancelada = "Solicitud cancelada";
        public const string SolicitudCerrada = "Solicitud cerrada";
    }
}
