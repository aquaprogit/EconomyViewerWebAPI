
using EconomyViewerAPI.BLL.Repos.Base;
using EconomyViewerAPI.BLL.Repos.Interfaces;
using EconomyViewerAPI.DAL.EF;
using EconomyViewerAPI.DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace EconomyViewerAPI.BLL.Repos;
public class ItemRepo : BaseRepo<Item>, IItemRepo
{
    public ItemRepo(ApplicationContext context) : base(context) { }
}
