using Incidencias.API.Data;
using Incidencias.API.Dto;
using Incidencias.API.Interfaces;
using Incidencias.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Incidencias.API.Repositorios
{
    public class NotaIncidenciaRepository : BaseRepository<NotaIncidencia>, INotaIncidenciaRepository
    {
        public NotaIncidenciaRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<PaginacionResult<NotaIncidencia>> GetNotasByIncidenciaIdAsync(int incidenciaId, int skip, int take)
        {
            var total = await _dbSet.CountAsync(c => c.IncidenciaId == incidenciaId);
            var notas = await _dbSet.Where(c => c.IncidenciaId == incidenciaId)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
            return new PaginacionResult<NotaIncidencia>()
            {
                Result = notas,
                Total = total
            };
        }
    }
}
