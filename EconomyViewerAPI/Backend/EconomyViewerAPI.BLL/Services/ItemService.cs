using AutoMapper;

using EconomyViewerAPI.BLL.Exceptions;
using EconomyViewerAPI.BLL.Profiles;
using EconomyViewerAPI.BLL.Repos.Interfaces;
using EconomyViewerAPI.DAL.DTO;
using EconomyViewerAPI.DAL.EF;
using EconomyViewerAPI.DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace EconomyViewerAPI.BLL.Services;
public class ItemService
{
    private readonly IItemRepo _itemRepo;
    private readonly IMapper _mapper;

    public ItemService(IItemRepo repo, IMapper mapper)
    {
        _itemRepo = repo;
        _mapper = mapper;
    }

    public async Task<ItemDTO> GetItem(int serverId, string name)
    {
        return _mapper.Map<ItemDTO>(await _itemRepo.Table.FirstAsync(item => item.Header == name && item.Server.Id == serverId));
    }

    public async Task<bool> DeleteItem(int serverId, string itemName)
    {
        Item? item = await _itemRepo.Table.FirstOrDefaultAsync(item => item.Header == itemName && item.Server.Id == serverId);
        return item != null && _itemRepo.Delete(item) > 0;
    }
}
