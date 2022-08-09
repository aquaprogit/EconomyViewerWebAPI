
using EconomyViewerAPI.BLL.Repos.Interfaces;
using EconomyViewerAPI.BLL.Services.Interfaces;
using EconomyViewerAPI.DAL.EF;
using EconomyViewerAPI.DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace EconomyViewerAPI.BLL.Services;
public class ServerService
{
    private readonly IServerRepo _repo;
    private readonly IServerLoader _serverLoader;
    public ServerService(IServerRepo repo, IServerLoader server)
    {
        _repo = repo;
        _serverLoader = server ?? throw new ArgumentNullException(nameof(server));
    }
    public async Task<int> FillServersAsync()
    {
        List<Server> downloaded = await _serverLoader.GetAllServersAsync();
        foreach (Server server in downloaded)
        {
            Server? alreadyAdded = _repo.Find(server.Name);
            if (alreadyAdded == null || server.Equals(alreadyAdded) == false)
            {
                if (alreadyAdded != null)
                    _repo.Delete(alreadyAdded);
                await _repo.AddAsync(server);
            }
        }
        return await _repo.SaveChangesAsync();
    }
    public async Task<Server?> GetServer(string name)
    {
        return await _repo.Table.FirstOrDefaultAsync(server => server.Name == name);
    }
    public async Task<Server?> GetServer(int id)
    {
        return await _repo.FindAsync(id);
    }
    public async Task<List<string>> GetServerNames()
    {
        return await _repo.Table.Select(s => s.Name).ToListAsync();
    }
}
