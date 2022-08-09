using EconomyViewerAPI.BLL.Services;
using EconomyViewerAPI.DAL.Entities;

using Microsoft.AspNetCore.Mvc;

namespace EconomyViewerAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ServerController : ControllerBase
{
    private readonly ServerService _serverService;

    public ServerController(ServerService serverService)
    {
        _serverService = serverService ?? throw new ArgumentNullException(nameof(serverService));
    }
    [HttpGet("loadFromForum/")]
    public async Task<IActionResult> LoadFromForum()
    {
        await _serverService.FillServersAsync();
        return Ok();
    }
    [HttpGet("{name}")]
    public async Task<IActionResult> GetServer(string name)
    {
        Server? server = await _serverService.GetServer(name);
        return Ok(server);
    }
    [HttpGet("names")]
    public async Task<IActionResult> GetServerNames()
    {
        List<string> names = await _serverService.GetServerNames();
        return Ok(names);
    }
}
