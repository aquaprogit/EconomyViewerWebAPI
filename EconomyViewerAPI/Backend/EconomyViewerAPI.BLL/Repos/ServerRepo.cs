
using EconomyViewerAPI.BLL.Repos.Base;
using EconomyViewerAPI.BLL.Repos.Interfaces;
using EconomyViewerAPI.DAL.EF;
using EconomyViewerAPI.DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace EconomyViewerAPI.BLL.Repos;
public class ServerRepo : BaseRepo<Server>, IServerRepo
{
    public ServerRepo(ApplicationContext context) : base(context) { }

    public override IEnumerable<Server> GetAll()
    {
        return Table.Include(server => server.Items)
                    .OrderBy(server => server.Name);
    }
    public Server? Find(string name)
    {
        return Table.IgnoreQueryFilters()
                    .Where(server => server.Name == name)
                    .Include(server => server.Items)
                    .FirstOrDefault();
    }
    public async Task<Server?> FindAsync(string name)
    {
        return await Table.IgnoreQueryFilters()
            .Where(server => server.Name == name)
            .Include(server => server.Items)
            .FirstOrDefaultAsync();
    }
}
