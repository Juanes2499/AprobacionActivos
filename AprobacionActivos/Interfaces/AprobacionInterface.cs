using AprobacionActivos.DTOs;
using AprobacionActivos.Entities;
using AprobacionActivos.Models;
using AprobacionActivos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprobacionActivos.Interfaces
{
    public interface AprobacionInterface
    {
        Task<ObjectResponse> SaveAprobacion(AprobacionBase aprobacion);
    }
}
