using AprobacionActivos.Interfaces;
using AprobacionActivos.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprobacionActivos.Controllers
{
    [Route("/activos")]
    [ApiController]
    public class ActivoController : Controller
    {
        private readonly ActivoInterface activoInterface;

        public ActivoController(
            ActivoInterface activoInterface    
        )
        {
            this.activoInterface = activoInterface;
        }

        [HttpGet("laboratorios")]
        public async Task<ActionResult<ObjectResponse>> GetLaboratorios()
        {
            return await activoInterface.GetLaboratorios();
        }

        [HttpGet("items")]
        public async Task<ActionResult<ObjectResponse>> GetItems([FromQuery(Name = "laboratorio")] string laboratorio)
        {
            return await activoInterface.GetItems(laboratorio);
        }
    }
}
