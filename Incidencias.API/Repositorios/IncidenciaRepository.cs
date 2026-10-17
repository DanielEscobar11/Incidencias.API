using Incidencias.API.Data;
using Incidencias.API.Interfaces;
using Incidencias.API.Models;

namespace Incidencias.API.Repositorios
{
    public class IncidenciaRepository : BaseRepository<Incidencia>, IIncidenciasRepository
    {
        public IncidenciaRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
