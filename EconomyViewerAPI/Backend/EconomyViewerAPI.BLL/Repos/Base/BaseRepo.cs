using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EconomyViewerAPI.DAL.EF;

using Microsoft.EntityFrameworkCore;

namespace EconomyViewerAPI.BLL.Repos.Base;
public abstract class BaseRepo<T> : IRepo<T> where T : class
{
    private readonly bool _disposeContext;
    private bool _disposed;

    public ApplicationContext Context { get; }
    public DbSet<T> Table { get; }

    protected BaseRepo(ApplicationContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        Table = Context.Set<T>();
        _disposeContext = false;
    }
    protected BaseRepo(DbContextOptions<ApplicationContext> options) : this(new ApplicationContext(options))
    {
        _disposeContext = true;
    }

    public int Add(T entity, bool persist = true)
    {
        throw new NotImplementedException();
    }

    public int AddRange(IEnumerable<T> entities, bool persist = true)
    {
        throw new NotImplementedException();
    }

    public int Delete(int id, byte[] timeStamp, bool persist = true)
    {
        throw new NotImplementedException();
    }

    public int Delete(T entity, bool persist = true)
    {
        throw new NotImplementedException();
    }

    public int DeleteRange(IEnumerable<T> entities, bool persist = true)
    {
        throw new NotImplementedException();
    }

    public T? Find(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public int SaveChanges()
    {
        try
        {
            return Context.SaveChanges();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred updating the database", ex);
        }
    }

    public int Update(T entity, bool persist = true)
    {
        throw new NotImplementedException();
    }

    public int UpdateRange(IEnumerable<T> entities, bool persist = true)
    {
        throw new NotImplementedException();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
            return;

        if (disposing && _disposeContext)
            Context.Dispose();

        _disposed = true;
    }
    ~BaseRepo()
    {
        Dispose(disposing: false);
    }
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
