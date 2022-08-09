using EconomyViewerAPI.BLL.Exceptions;
using EconomyViewerAPI.BLL.Repos.Interfaces;
using EconomyViewerAPI.DAL.EF;
using EconomyViewerAPI.DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace EconomyViewerAPI.BLL.Services;
public class ItemService
{
    private readonly IItemRepo _itemRepo;

    public ItemService(IItemRepo repo)
    {
        _itemRepo = repo;
    }

    public async Task<Item> GetItem(int serverId, string name)
    {
        return await _itemRepo.Table.FirstAsync(item => item.Header == name && item.Server.Id == serverId);
    }

    public async Task<bool> DeleteItem(int serverId, string itemName)
    {
        Item? item = await _itemRepo.Table.FirstOrDefaultAsync(item => item.Header == itemName && item.Server.Id == serverId);
        return item != null && _itemRepo.Delete(item) > 0;
    }
}
