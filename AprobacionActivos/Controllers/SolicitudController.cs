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
    [Route("/solicitudes")]
    [ApiController]
    public class SolicitudController : Controller
    {
        private readonly SolicitudInterface solicitudInterface;

        public SolicitudController(
            SolicitudInterface solicitudInterface
        )
        {
            this.solicitudInterface = solicitudInterface;
        }

        [HttpGet]
        public async Task<IEnumerable<Solicitud>> GetSolicitudes()
        {
            return await solicitudInterface.GetSolicitudes();
        }

        [HttpGet("laboratorio")]
        public async Task<ActionResult<ObjectResponse>> GetSolicitudesLaboratorio()
        {
            return await solicitudInterface.GetSolicitudesLaboratorio();
        }

        [HttpGet("porteria")]
        public async Task<ActionResult<ObjectResponse>> GetSolicitudesPorteria()
        {
            return await solicitudInterface.GetSolicitudesPorteria();
        }

        [HttpPost]
        public async Task<ActionResult<ObjectResponse>> SaveSolicitud([FromBody] Solicitud solicitud)
        {
            return await solicitudInterface.SaveSolicitud(solicitud);
        }

        [HttpDelete("/{id:int}")]
        public async Task<ActionResult<ObjectResponse>> DeleteSolicitud(int id)
        {
            return await solicitudInterface.DeleteSolicitud(id);
        }
    }
}
