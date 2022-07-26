using AprobacionActivos.DTOs;
using AprobacionActivos.Entities;
using AprobacionActivos.Enums;
using AprobacionActivos.Interfaces;
using AprobacionActivos.Models;
using AprobacionActivos.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprobacionActivos.Controllers
{
    [Route("/aprobaciones")]
    [ApiController]
    public class AprobacionController : Controller
    {
        private readonly AprobacionInterface aprobacionInterface;

        public AprobacionController(
            AprobacionInterface aprobacionInterface
        )
        {
            this.aprobacionInterface = aprobacionInterface;
        }

        [HttpPost("laboratorio")]
        public async Task<ActionResult<ObjectResponse>> SaveAprobacionLaboratorio([FromBody] AprobacionPostDTO aprobacion)
        {
            AprobacionBase aprobacionBase = new AprobacionBase() {
                AprobacionDTO = aprobacion,
                MensajeAprobacion = "laboratorio",
                TipoAprobacion = TipoAprobacionesEnums.AprobacionLaboratorio,
                EstadoTrackingAnterior = EstadosTrackingEnums.PendienteLaboratorio,
                EstadoTrackingActual = EstadosTrackingEnums.RespuestaLaboratorio,
                EstadoTrackingSiguiente = EstadosTrackingEnums.PendientePorteria
            };
            return await aprobacionInterface.SaveAprobacion(aprobacionBase);
        }

        [HttpPost("porteria")]
        public async Task<ActionResult<ObjectResponse>> SaveAprobacionPorteria([FromBody] AprobacionPostDTO aprobacion)
        {
            AprobacionBase aprobacionBase = new AprobacionBase()
            {
                AprobacionDTO = aprobacion,
                MensajeAprobacion = "porteria",
                TipoAprobacion = TipoAprobacionesEnums.AprobacionPorteria,
                EstadoTrackingAnterior = EstadosTrackingEnums.PendientePorteria,
                EstadoTrackingActual = EstadosTrackingEnums.RespuestaPorteria,
                EstadoTrackingSiguiente = EstadosTrackingEnums.SolicitudCerrada
            };
            return await aprobacionInterface.SaveAprobacion(aprobacionBase);
        }
    }
}
