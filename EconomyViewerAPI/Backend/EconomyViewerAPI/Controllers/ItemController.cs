using EconomyViewerAPI.BLL.Services;
using EconomyViewerAPI.DAL.Entities;

using Microsoft.AspNetCore.Mvc;

namespace EconomyViewerAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private ItemService _itemService;

    public ItemController(ItemService itemService)
    {
        _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
    }

    [HttpGet("fromServer/{serverName}/{itemName}")]
    public async Task<IActionResult> GetItemByName(string serverName, string itemName)
    {
        return Ok(await _itemService.GetItem(serverName, itemName));
    }
    [HttpDelete("fromServer/{serverName}/{itemName}")]
    public async Task<IActionResult> DeleteItemByName(string serverName, string itemName)
    {
        return await _itemService.DeleteItem(serverName, itemName) ? Ok() : NotFound();
    }
}
