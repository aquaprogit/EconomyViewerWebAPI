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

    [HttpGet("fromServer/{serverId}/{itemName}")]
    public async Task<IActionResult> GetItemByName(int serverId, string itemName)
    {
        return Ok(await _itemService.GetItem(serverId, itemName));
    }
    [HttpDelete("fromServer/{serverId}/{itemName}")]
    public async Task<IActionResult> DeleteItemByName(int serverId, string itemName)
    {
        return Ok(await _itemService.DeleteItem(serverId, itemName));
    } 
}
