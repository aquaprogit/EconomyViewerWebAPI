
using EconomyViewerAPI.BLL.Repos.Base;
using EconomyViewerAPI.DAL.Entities;

namespace EconomyViewerAPI.BLL.Repos.Interfaces;
public interface IItemRepo : IRepo<Item>
{
    Item? Find(int id);
    Task<Item?> FindAsync(int id);
}