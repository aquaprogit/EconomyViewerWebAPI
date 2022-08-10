
using EconomyViewerAPI.BLL.Repos.Base;
using EconomyViewerAPI.DAL.Entities;

namespace EconomyViewerAPI.BLL.Repos.Interfaces;
public interface IServerRepo : IRepo<Server>
{
    Server? Find(string name);
    Task<Server?> FindAsync(string name);
}
