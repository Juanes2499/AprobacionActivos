using AprobacionActivos.Models;
using AprobacionActivos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprobacionActivos.Interfaces
{
    public interface SolicitudInterface
    {
        Task<IEnumerable<Solicitud>> GetSolicitudes();
        Task<ObjectResponse> GetSolicitudesLaboratorio();
        Task<ObjectResponse> GetSolicitudesPorteria();
        Task<ObjectResponse> SaveSolicitud(Solicitud solicitud);
        Task<ObjectResponse> DeleteSolicitud(int id);
    }
}
