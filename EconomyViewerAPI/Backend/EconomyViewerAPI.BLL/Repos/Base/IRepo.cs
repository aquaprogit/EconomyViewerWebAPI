using Microsoft.EntityFrameworkCore;

namespace EconomyViewerAPI.BLL.Repos.Base;
public interface IRepo<T> : IDisposable where T : class
{
    DbSet<T> Table { get; }
    int Add(T entity, bool persist = true);
    Task<int> AddAsync(T entity, bool persist = true);

    int AddRange(IEnumerable<T> entities, bool persist = true);
    Task<int> AddRangeAsync(IEnumerable<T> entities, bool persist = true);

    int Update(T entity, bool persist = true);

    int UpdateRange(IEnumerable<T> entities, bool persist = true);

    int Delete(T entity, bool persist = true);

    int DeleteRange(IEnumerable<T> entities, bool persist = true);

    IEnumerable<T> GetAll();

    int SaveChanges();
    Task<int> SaveChangesAsync();
}
