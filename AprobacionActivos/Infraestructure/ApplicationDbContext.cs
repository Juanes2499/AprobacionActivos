using AprobacionActivos.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AprobacionActivos.Infraestructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Solicitud> solicitudes { get; set; }
        public DbSet<TrackingSolicitud> trackingSolicitudes { get; set; }
        public DbSet<TipoAprobacion> tipoAprobaciones { get; set; }
        public DbSet<Aprobacion> aprobaciones { get; set; }
        public DbSet<ActiovosUao> actiovosUaos { get; set; }
    }
}
