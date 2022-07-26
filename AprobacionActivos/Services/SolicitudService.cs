using AprobacionActivos.DTOs;
using AprobacionActivos.Enums;
using AprobacionActivos.Infraestructure;
using AprobacionActivos.Interfaces;
using AprobacionActivos.Models;
using AprobacionActivos.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AprobacionActivos.Services
{
    public class SolicitudService : SolicitudInterface
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IConfiguration configuration;

        public SolicitudService(
            ApplicationDbContext dbContext,
            IConfiguration configuration
        )
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<Solicitud>> GetSolicitudes()
        {
            return await dbContext.solicitudes.ToListAsync();
        }

        public async Task<ObjectResponse> GetSolicitudesLaboratorio()
        {
            ObjectResponse result = new ObjectResponse();
            try
            {
                var solicitudesList = (
                    from s in dbContext.solicitudes
                    join au in dbContext.actiovosUaos on s.ACTIVO_ID equals au.ID
                    join ts in dbContext.trackingSolicitudes on s.ID equals ts.SOLICITUD_ID
                    join a in dbContext.aprobaciones on s.ID equals a.SOLICITUD_ID into leftjoinaprobaciones
                    from lja in leftjoinaprobaciones.DefaultIfEmpty()
                    select new SolicitudesGetDTO()
                    {
                        ID = s.ID,
                        NOMBRE_LABORATORIO = au.NOMBRE_LABORATORIO,
                        CODIGO = s.CODIGO,
                        NOMBRES = s.NOMBRES,
                        APELLIDOS = s.APELLIDOS,
                        EMAIL = s.EMAIL,
                        ESTADO_TRACKING = ts.ESTADO,
                        APROBADO = lja == null ? null : lja.APROBADO
                    }                    
                ).ToList().Where( x => x.APROBADO == null).Where(x => x.ESTADO_TRACKING == "Pendiente respuesta laboratorio");


                await dbContext.SaveChangesAsync();
                result.success = true;
                result.data = solicitudesList;
                return result;
            }
            catch (Exception e)
            {
                result.success = false;
                result.reasons.Add("La solicitudes de laboratorio no pudieron ser consultadas.");
                return result;
            }
        }

        public async Task<ObjectResponse> GetSolicitudesPorteria()
        {
            ObjectResponse result = new ObjectResponse();
            try
            {
                var idAprobacionPorteria = dbContext.tipoAprobaciones.Where(y => y.NOMBRE_APROBACION == "APROBACION PORTERIA").FirstOrDefault().ID;
                var solicitudesList = (
                    from s in dbContext.solicitudes
                    join au in dbContext.actiovosUaos on s.ACTIVO_ID equals au.ID
                    join ts in dbContext.trackingSolicitudes on s.ID equals ts.SOLICITUD_ID
                    join a in dbContext.aprobaciones.Where(x => x.TIPO_APROBACION_ID == idAprobacionPorteria) on s.ID equals a.SOLICITUD_ID into leftjoinaprobaciones 
                    from t in leftjoinaprobaciones.DefaultIfEmpty()
                    join ta in dbContext.tipoAprobaciones on t.TIPO_APROBACION_ID equals ta.ID into leftjointipoaprobaciones
                    from ljta in leftjointipoaprobaciones.DefaultIfEmpty()
                    select new SolicitudesGetDTO()
                    {
                        ID = s.ID,
                        NOMBRE_LABORATORIO = au.NOMBRE_LABORATORIO,
                        CODIGO = s.CODIGO,
                        NOMBRES = s.NOMBRES,
                        APELLIDOS = s.APELLIDOS,
                        EMAIL = s.EMAIL,
                        ESTADO_TRACKING = ts.ESTADO,
                        APROBADO = t == null ? null : t.APROBADO
                    }
                ).ToList().Where(x => x.APROBADO == null).Where(x => x.ESTADO_TRACKING == "Pendiente respuesta porteria");


                await dbContext.SaveChangesAsync();
                result.success = true;
                result.data = solicitudesList;
                return result;
            }
            catch (Exception e)
            {
                result.success = false;
                result.reasons.Add("La solicitudes de porteria no pudieron ser consultadas.");
                return result;
            }
        }

        public async Task<ObjectResponse> SaveSolicitud(Solicitud solicitud)
        {
            ObjectResponse result = new ObjectResponse();
            try
            {
                //Creación Solicitud
                var solicitudCreated = await dbContext.solicitudes.AddAsync(solicitud);

                //Estado solicitud creada
                await dbContext.trackingSolicitudes.AddAsync(new TrackingSolicitud() { SOLICITUD = solicitudCreated.Entity, ESTADO = EstadosTrackingEnums.SolicitudCreada });

                //Estado solicitud pendiente respuesta laboratorio
                await dbContext.trackingSolicitudes.AddAsync(new TrackingSolicitud() { SOLICITUD = solicitudCreated.Entity, ESTADO = EstadosTrackingEnums.PendienteLaboratorio });

                await dbContext.SaveChangesAsync();
                result.success = true;
                result.data = solicitudCreated.Entity;
                return result;
            }
            catch (Exception e)
            {
                result.success = false;
                result.reasons.Add("La solicitud no pudo ser creada.");
                return result;
            }
        }

        public async Task<ObjectResponse> DeleteSolicitud(int id)
        {
            ObjectResponse result = new ObjectResponse();
            try
            {
                Solicitud solicitudFound = await dbContext.solicitudes.FirstOrDefaultAsync(x => x.ID == id);
                if (solicitudFound == null)
                {
                    result.success = false;
                    result.reasons.Add("La solicitud no existe.");
                    return result;
                }

                dbContext.solicitudes.Remove(solicitudFound);
                await dbContext.SaveChangesAsync();
                result.success = true;
                return result;
            }
            catch (Exception e)
            {
                result.success = false;
                result.reasons.Add("La solicitud no pudo ser creada.");
                return result;
            }
        }
    }
}
