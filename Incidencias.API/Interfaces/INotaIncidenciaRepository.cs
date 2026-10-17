using Incidencias.API.Dto;
using Incidencias.API.Models;

namespace Incidencias.API.Interfaces
{
    public interface INotaIncidenciaRepository : IBaseRepository<NotaIncidencia>
    {
        Task<PaginacionResult<NotaIncidencia>> GetNotasByIncidenciaIdAsync(int incidenciaId, int skip, int take);
    }
}
