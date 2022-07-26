using AprobacionActivos.DTOs;
using AprobacionActivos.Infraestructure;
using AprobacionActivos.Interfaces;
using AprobacionActivos.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprobacionActivos.Services
{
    public class ActivoService : ActivoInterface
    {
        private readonly ApplicationDbContext dbContext;

        public ActivoService(
            ApplicationDbContext dbContext
        )
        {
            this.dbContext = dbContext;
        }


        public async Task<ObjectResponse> GetLaboratorios()
        {
            ObjectResponse result = new ObjectResponse();
            try
            {
                var laboratoriosList = await dbContext.actiovosUaos
                    .OrderBy(x => x.NOMBRE_LABORATORIO)
                    .Select(x => x.NOMBRE_LABORATORIO)
                    .Distinct()
                    .ToListAsync();


                await dbContext.SaveChangesAsync();
                result.success = true;
                result.data = laboratoriosList;
                return result;
            }
            catch (Exception e)
            {
                result.success = false;
                result.reasons.Add("Los laboratorios no pueden ser consultados.");
                return result;
            }
        }

        public async Task<ObjectResponse> GetItems(string nombreLaboratorio)
        {
            ObjectResponse result = new ObjectResponse();
            try
            {
                var itemsList = await dbContext.actiovosUaos
                    .Where(x => x.NOMBRE_LABORATORIO == nombreLaboratorio)
                    .OrderBy(x => x.ID)
                    .Select(x => new ItemGetDTO() { ID = x.ID, NOMBRE_ACTIVO = x.NOMBRE_ACTIVO })
                    .Distinct()
                    .ToListAsync();

                await dbContext.SaveChangesAsync();
                result.success = true;
                result.data = itemsList;
                return result;
            }
            catch (Exception e)
            {
                result.success = false;
                result.reasons.Add("Los items no pueden ser consultados.");
                return result;
            }
        }
    }
}
