using AprobacionActivos.DTOs;
using AprobacionActivos.Entities;
using AprobacionActivos.Enums;
using AprobacionActivos.Infraestructure;
using AprobacionActivos.Interfaces;
using AprobacionActivos.Models;
using AprobacionActivos.Shared;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprobacionActivos.Services
{
    public class AprobacionService : AprobacionInterface
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public AprobacionService(
            ApplicationDbContext dbContext,
            IMapper mapper
        )
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        //public async Task<ObjectResponse> SaveAprobacionLaboratorio(AprobacionPostDTO aprobacion)
        //{
        //    ObjectResponse result = new ObjectResponse();
        //    try
        //    {

        //        //Se busca la solicitud
        //        var solicitudFound = await dbContext.solicitudes.FirstOrDefaultAsync(x => x.ID == aprobacion.SOLICITUD_ID);

        //        if (solicitudFound == null)
        //        {
        //            result.success = false;
        //            result.reasons.Add("La solicitud no existe.");
        //            return result;
        //        }

        //        //Validar si la solicitud ya tiene esa aprobación
        //        var aprobacionFound = await dbContext.aprobaciones
        //            .Where(x => x.SOLICITUD_ID == aprobacion.SOLICITUD_ID)
        //            .Where(x => x.TIPO_APROBACION.NOMBRE_APROBACION == TipoAprobacionesEnums.AprobacionLaboratorio)
        //            .FirstOrDefaultAsync();

        //        if (aprobacionFound != null)
        //        {
        //            result.success = false;
        //            result.reasons.Add("La solicitud ya tiene una aprobación de laboratorio");
        //            return result;
        //        }

        //        //Validar el estado de la solicitud anterior
        //        var trackingFound = await dbContext.trackingSolicitudes.Where(x => x.ESTADO == EstadosTrackingEnums.PendienteLaboratorio).FirstOrDefaultAsync();

        //        if (trackingFound == null)
        //        {
        //            result.success = false;
        //            result.reasons.Add("La solicitud no tiene el estado correspondiente de pendiente laboratorio.");
        //            return result;
        //        }

        //        //Validar que el estado de la solictud no haya sido cancelada
        //        var trackingFoundCanceled = await dbContext.trackingSolicitudes.Where(x => x.ESTADO == EstadosTrackingEnums.SolicitudCancelada).FirstOrDefaultAsync();

        //        if (trackingFoundCanceled == null)
        //        {
        //            result.success = false;
        //            result.reasons.Add("La solicitud esta cancelada");
        //            return result;
        //        }

        //        //Crear aprobación
        //        Aprobacion aprobacionToCreate = mapper.Map<Aprobacion>(aprobacion);
        //        aprobacionToCreate.TIPO_APROBACION = await dbContext.tipoAprobaciones.FirstOrDefaultAsync(x => x.NOMBRE_APROBACION == TipoAprobacionesEnums.AprobacionLaboratorio);
        //        var aprobacionCreated = await dbContext.aprobaciones.AddAsync(aprobacionToCreate);

        //        //Si la aprobación no es exitosa
        //        if(aprobacion.APROBADO == 0)
        //        {
        //            //Estado Solicitud Cancelada
        //            await dbContext.trackingSolicitudes.AddAsync(new TrackingSolicitud() { SOLICITUD = solicitudFound, ESTADO = EstadosTrackingEnums.SolicitudCancelada });
        //        }
        //        else
        //        {
        //            //Estado actual
        //            await dbContext.trackingSolicitudes.AddAsync(new TrackingSolicitud() { SOLICITUD = solicitudFound, ESTADO = EstadosTrackingEnums.RespuestaLaboratorio });

        //            //Estado siguiente
        //            await dbContext.trackingSolicitudes.AddAsync(new TrackingSolicitud() { SOLICITUD = solicitudFound, ESTADO = EstadosTrackingEnums.PendientePorteria });
        //        }

        //        await dbContext.SaveChangesAsync();
        //        result.success = true;
        //        result.data = aprobacionCreated.Entity;
        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        result.success = false;
        //        result.reasons.Add("La solicitud no pudo ser creada.");
        //        return result;
        //    }
        //}

        public async Task<ObjectResponse> SaveAprobacion(AprobacionBase aprobacion)
        {
            ObjectResponse result = new ObjectResponse();
            try
            {

                //Se busca la solicitud
                var solicitudFound = await dbContext.solicitudes
                    .FirstOrDefaultAsync(x => x.ID == aprobacion.AprobacionDTO.SOLICITUD_ID);

                if (solicitudFound == null)
                {
                    result.success = false;
                    result.reasons.Add("La solicitud no existe.");
                    return result;
                }

                //Validar si la solicitud ya tiene esa aprobación
                var aprobacionFound = await dbContext.aprobaciones
                    .Where(x => x.SOLICITUD_ID == aprobacion.AprobacionDTO.SOLICITUD_ID)
                    .Where(x => x.TIPO_APROBACION.NOMBRE_APROBACION == aprobacion.TipoAprobacion)
                    .FirstOrDefaultAsync();

                if (aprobacionFound != null)
                {
                    result.success = false;
                    result.reasons.Add($"La solicitud ya tiene una aprobación de {aprobacion.MensajeAprobacion} ");
                    return result;
                }

                //Validar el estado de la solicitud anterior
                var trackingFound = await dbContext.trackingSolicitudes
                    .Where(x => x.SOLICITUD_ID == aprobacion.AprobacionDTO.SOLICITUD_ID)
                    .Where(x => x.ESTADO == aprobacion.EstadoTrackingAnterior)
                    .FirstOrDefaultAsync();

                if (trackingFound == null)
                {
                    result.success = false;
                    result.reasons.Add("La solicitud no tiene el estado correspondiente de pendiente .");
                    return result;
                }

                //Validar que el estado de la solictud no haya sido cancelada
                var trackingFoundCanceled = await dbContext.trackingSolicitudes
                    .Where(x => x.SOLICITUD_ID == aprobacion.AprobacionDTO.SOLICITUD_ID)
                    .Where(x => x.ESTADO == EstadosTrackingEnums.SolicitudCancelada)
                    .FirstOrDefaultAsync();

                if (trackingFoundCanceled != null)
                {
                    result.success = false;
                    result.reasons.Add("La solicitud esta cancelada");
                    return result;
                }

                //Crear aprobación
                Aprobacion aprobacionToCreate = mapper.Map<Aprobacion>(aprobacion.AprobacionDTO);
                aprobacionToCreate.TIPO_APROBACION = await dbContext.tipoAprobaciones.FirstOrDefaultAsync(x => x.NOMBRE_APROBACION == aprobacion.TipoAprobacion);
                var aprobacionCreated = await dbContext.aprobaciones.AddAsync(aprobacionToCreate);

                //Si la aprobación no es exitosa
                if (aprobacion.AprobacionDTO.APROBADO == 0)
                {
                    //Estado Solicitud Cancelada
                    await dbContext.trackingSolicitudes.AddAsync(new TrackingSolicitud() { SOLICITUD = solicitudFound, ESTADO = EstadosTrackingEnums.SolicitudCancelada });
                }
                else
                {
                    //Estado actual
                    await dbContext.trackingSolicitudes.AddAsync(new TrackingSolicitud() { SOLICITUD = solicitudFound, ESTADO = aprobacion.EstadoTrackingActual });

                    //Estado siguiente
                    await dbContext.trackingSolicitudes.AddAsync(new TrackingSolicitud() { SOLICITUD = solicitudFound, ESTADO = aprobacion.EstadoTrackingSiguiente });
                }

                await dbContext.SaveChangesAsync();
                result.success = true;
                result.data = aprobacionCreated.Entity;
                return result;
            }
            catch (Exception e)
            {
                result.success = false;
                result.reasons.Add("La aprobación no pudo ser completada.");
                return result;
            }
        }
    }
}
