
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
    public override Server? Find(int id)
    {
        return Table.IgnoreQueryFilters()
                    .Where(server => server.Id == id)
                    .Include(server => server.Items)
                    .FirstOrDefault();
    }
    public override async Task<Server?> FindAsync(int id)
    {
        return await Table.IgnoreQueryFilters()
            .Where(server => server.Id == id)
            .Include(server => server.Items)
            .FirstOrDefaultAsync();
    }
}
