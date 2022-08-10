﻿
using AutoMapper;

using EconomyViewerAPI.BLL.Repos.Interfaces;
using EconomyViewerAPI.BLL.Services.Interfaces;
using EconomyViewerAPI.DAL.DTO;
using EconomyViewerAPI.DAL.EF;
using EconomyViewerAPI.DAL.Entities;

using Microsoft.EntityFrameworkCore;

namespace EconomyViewerAPI.BLL.Services;
public class ServerService
{
    private readonly IServerRepo _repo;
    private readonly IServerLoader _serverLoader;
    private readonly IMapper _mapper;
    public ServerService(IServerRepo repo, IServerLoader server, IMapper mapper)
    {
        _repo = repo;
        _serverLoader = server ?? throw new ArgumentNullException(nameof(server));
        _mapper = mapper;
    }
    public async Task<int> FillServersAsync()
    {
        List<Server> downloaded = await _serverLoader.GetAllServersAsync();
        int affected = 0;
        foreach (Server server in downloaded)
        {
            Server? alreadyAdded = _repo.Find(server.Name);
            if (alreadyAdded == null || server.Equals(alreadyAdded) == false)
            {
                if (alreadyAdded != null)
                {
                    _repo.Delete(alreadyAdded);
                }
                affected += await _repo.AddAsync(server);
            }
        }
        return affected;
    }
    public async Task<ServerDTO?> GetServer(string name)
    {
        return _mapper.Map<ServerDTO>(await _repo.FindAsync(name));
    }
    public async Task<List<string>> GetServerNames()
    {
        return await _repo.Table.Select(s => s.Name).ToListAsync();
    }
}
