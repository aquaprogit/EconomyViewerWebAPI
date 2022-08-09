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

    public virtual int Add(T entity, bool persist = true)
    {
        Table.Add(entity);
        return persist ? SaveChanges() : 0;
    }
    public virtual async Task<int> AddAsync(T entity, bool persist = true)
    {
        await Table.AddAsync(entity);
        return persist ? await SaveChangesAsync() : 0;
    }
    public virtual int AddRange(IEnumerable<T> entities, bool persist = true)
    {
        Table.AddRange(entities);
        return persist ? SaveChanges() : 0;
    }
    public virtual async Task<int> AddRangeAsync(IEnumerable<T> entities, bool persist = true)
    {
        await Table.AddRangeAsync(entities);
        return persist ? await SaveChangesAsync() : 0;
    }

    public virtual int Update(T entity, bool persist = true)
    {
        Table.Update(entity);
        return persist ? SaveChanges() : 0;
    }
    public virtual int UpdateRange(IEnumerable<T> entities, bool persist = true)
    {
        Table.UpdateRange(entities);
        return persist ? SaveChanges() : 0;
    }

    public virtual int Delete(T entity, bool persist = true)
    {
        Table.Remove(entity);
        return persist ? SaveChanges() : 0;
    }
    public virtual int DeleteRange(IEnumerable<T> entities, bool persist = true)
    {
        Table.RemoveRange(entities);
        return persist ? SaveChanges() : 0;
    }

    public virtual T? Find(int id)
    {
        return Table.Find(id);
    }
    public virtual async Task<T?> FindAsync(int id)
    {
        return await Table.FindAsync(id);
    }

    public virtual IEnumerable<T> GetAll()
    {
        return Table;
    }

    public virtual int SaveChanges()
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
    public virtual async Task<int> SaveChangesAsync()
    {
        try
        {
            return await Context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred updating the database", ex);
        }
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
