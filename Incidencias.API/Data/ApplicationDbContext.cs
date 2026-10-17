using Incidencias.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Incidencias.API.Data
{
    public class ApplicationDbContext : 
        IdentityDbContext<ApplicationUser, ApplicationRole ,Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : 
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //La lave primaria de mi Incidencia 
            builder.Entity<Incidencia>().HasKey(c => c.Id);
            //Tenemos configurado de una a muchos (Un usuario tiene muchas incidencias)
            builder.Entity<Incidencia>().HasOne(i => i.CreadoPor).WithMany(u => u.Incidencias).OnDelete(DeleteBehavior.Cascade);

            builder.Entity<NotaIncidencia>().HasKey(c => c.Id);
            builder.Entity<NotaIncidencia>().HasOne(c => c.CreadoPor).WithMany(u => u.NotaIncidencias).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<NotaIncidencia>().HasOne(c => c.Incidencia).WithMany(i => i.Notas).OnDelete(DeleteBehavior.NoAction);
        }

        public DbSet<Incidencia> Incidencias { get; set; }

        public DbSet<NotaIncidencia> NotaIncidencias { get; set; }
    }
}
