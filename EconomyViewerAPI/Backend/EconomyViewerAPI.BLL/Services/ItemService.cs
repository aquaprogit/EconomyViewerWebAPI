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

    public async Task<bool> InsertItem(ItemDTO item)
    {
        Item mapped = _mapper.Map<Item>(item);
        int countOfSame = _itemRepo.Table.Where(i => i.Header == item.Header)
                                           .AsEnumerable()
                                           .Count(i => i.Equals(mapped));
        if (countOfSame == 0)
        {
            await _itemRepo.AddAsync(mapped);
            return true;
        }
        return false;
    }
    public async Task<ItemDTO> GetItem(string serverName, string name)
    {
        return _mapper.Map<ItemDTO>(await _itemRepo.Table.FirstAsync(item => item.Header == name && item.ServerName == serverName));
    }
    public void UpdateItem(ItemPutDTO item)
    {
        _itemRepo.Update(_mapper.Map<Item>(item));
    }
    public async Task<bool> DeleteItem(string serverName, string itemName)
    {
        Item? item = await _itemRepo.Table.FirstOrDefaultAsync(item => item.Header == itemName && item.ServerName == serverName);
        return item != null && _itemRepo.Delete(item) > 0;
    }
}
