using AprobacionActivos.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprobacionActivos.Interfaces
{
    public interface ActivoInterface
    {
        Task<ObjectResponse> GetLaboratorios();
        Task<ObjectResponse> GetItems(string nombreLaboratorio);
    }
}
