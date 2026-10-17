using Incidencias.API.Dto;
using Incidencias.API.Models;
using System.Linq.Expressions;

namespace Incidencias.API.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseModel
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<PaginacionResult<TEntity>> GetAsync(int skip, int take);

        Task<TEntity> GetById(int id);

        Task<bool> Delete(int id);

        Task<bool> AnyById(int id);

        Task<TEntity> GetById(int id, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> Update(TEntity entity);
    }
}
