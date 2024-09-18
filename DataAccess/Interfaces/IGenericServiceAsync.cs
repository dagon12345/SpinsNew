using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IGenericServiceAsync<TEntity, TDto> : IReadServiceAsync<TEntity, TDto>
        where TEntity: class
        where TDto: class
    {
        Task AddAsync(TDto dto);
        Task DeleteAsync(int id);
        Task UpdateAsync(TDto dto);
    }
}
