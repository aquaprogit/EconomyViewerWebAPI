using EconomyViewerAPI.BLL.Exceptions;
using EconomyViewerAPI.DAL.EF;
using EconomyViewerAPI.DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace EconomyViewerAPI.BLL.Services;
public class ItemService
{
    private ApplicationContext _context;

    public ItemService(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Item> GetItem(int serverId, string name)
    {
        Server server = await _context.Servers.Include(s => s.Items).FirstAsync(server => server.Id == serverId);
        return server.Items.FirstOrDefault(item => item.Header == name) ?? throw new ItemNotFoundException(name);
    }

    public async Task<bool> DeleteItem(int serverId, string itemName)
    {
        Server server = await _context.Servers.Include(s => s.Items).FirstAsync(server => server.Id == serverId);
        bool result = server.Items.Remove(server.Items.First(item => item.Header == itemName));
        await _context.SaveChangesAsync();
        return result;
    }
}
