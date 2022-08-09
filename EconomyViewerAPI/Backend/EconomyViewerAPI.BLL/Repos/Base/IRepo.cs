namespace EconomyViewerAPI.BLL.Repos.Base;
public interface IRepo<T> : IDisposable
{
    int Add(T entity, bool persist = true);
    int AddRange(IEnumerable<T> entities, bool persist = true);
    int Update(T entity, bool persist = true);
    int UpdateRange(IEnumerable<T> entities, bool persist = true);
    int Delete(int id, byte[] timeStamp, bool persist = true);
    int Delete(T entity, bool persist = true);
    int DeleteRange(IEnumerable<T> entities, bool persist = true);
    T? Find(int id);
    IEnumerable<T> GetAll();
    int SaveChanges();
}
