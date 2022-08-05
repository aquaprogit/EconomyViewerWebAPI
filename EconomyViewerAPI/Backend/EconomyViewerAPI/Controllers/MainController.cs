using EconomyViewerAPI.BLL.Services;

using Microsoft.AspNetCore.Mvc;

namespace EconomyViewerAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class MainController : ControllerBase
{
    private readonly ServerService _serverService;

    public MainController(ServerService serverService)
    {
        _serverService = serverService ?? throw new ArgumentNullException(nameof(serverService));
    }
    [HttpGet("loadFromForum/")]
    public async Task<IActionResult> LoadFromForum()
    {
        try
        {
            await _serverService.FillServersAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
    [HttpGet("server/{name}")]
    public async Task<IActionResult> GetServer(string name)
    {
        try
        {
            var server = await _serverService.GetServer(name);
            return Ok(server);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
    [HttpGet("server/names")]
    public async Task<IActionResult> GetServerNames()
    {
        try
        {
            var names = await _serverService.GetServerNames();
            return Ok(names);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }
}
