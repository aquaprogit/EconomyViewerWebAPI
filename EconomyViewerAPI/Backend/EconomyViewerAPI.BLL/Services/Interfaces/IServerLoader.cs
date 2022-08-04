using EconomyViewerAPI.DAL.Entities;

namespace EconomyViewerAPI.BLL.Services.Interfaces;

public interface IServerLoader
{
    Task<List<Server>> GetAllServersAsync();
    Task<Server> GetServerByNameAsync(string name);
}