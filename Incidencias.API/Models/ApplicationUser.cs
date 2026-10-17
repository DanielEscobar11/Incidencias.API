using Microsoft.AspNetCore.Identity;

namespace Incidencias.API.Models
{
    /// <summary>
    /// Esta tabla es para administrar los usarios de mi API
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {

        public ApplicationUser() { 

            Incidencias = new HashSet<Incidencia>();
            NotaIncidencias = new HashSet<NotaIncidencia>();
        }
        public HashSet<Incidencia> Incidencias { get; set; }

        public HashSet<NotaIncidencia> NotaIncidencias { get; set; }
    }

    public class ApplicationRole : IdentityRole<Guid>
    {
    }
}
