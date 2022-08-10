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
        return Ok(await _serverService.FillServersAsync());
    }
    [HttpGet("{name}")]
    public async Task<IActionResult> GetServer(string name)
    {
        return Ok(await _serverService.GetServer(name));
    }
    [HttpDelete("[action]/{name}")]
    public async Task<IActionResult> Delete(string name)
    {
        return await _serverService.DeleteServer(name) ? Ok(name) : NotFound(name);
    }
    [HttpGet("names")]
    public async Task<IActionResult> GetServerNames()
    {
        List<string> names = await _serverService.GetServerNames();
        return Ok(names);
    }
}
