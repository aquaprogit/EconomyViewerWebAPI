
using EconomyViewerAPI.BLL.Services.Interfaces;
using EconomyViewerAPI.DAL.EF;
using EconomyViewerAPI.DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace EconomyViewerAPI.BLL.Services;
public class ServerService
{
    private readonly ApplicationContext _context;
    private readonly IServerLoader _serverLoader;
    public ServerService(ApplicationContext context, IServerLoader server)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _serverLoader = server ?? throw new ArgumentNullException(nameof(server));
    }
    public async Task FillServersAsync()
    {
        List<Server> downloaded = await _serverLoader.GetAllServersAsync();
        foreach (Server server in downloaded)
        {
            _context.Servers.Add(server);
        }
        await _context.SaveChangesAsync();
    }
    public async Task<Server?> GetServer(string name)
    {
        return await _context.Servers.Include(s => s.Items).FirstOrDefaultAsync(server => server.Name == name);
    }
}
