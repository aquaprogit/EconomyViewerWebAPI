using EconomyViewerAPI.BLL.Services;
using EconomyViewerAPI.DAL.DTO;
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
    [HttpPost("add/")]
    [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateItem([FromBody] ItemDTO item)
    {
        return await _itemService.InsertItem(item) ? Ok(item) : BadRequest("Item already exists in the table");
    }
    [HttpGet("fromServer/{serverName}/{itemName}")]
    [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetItemByName(string serverName, string itemName)
    {
        return Ok(await _itemService.GetItem(serverName, itemName));
    }
    [HttpDelete("fromServer/{serverName}/{itemName}")]
    [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteItemByName(string serverName, string itemName)
    {
        return await _itemService.DeleteItem(serverName, itemName) ? Ok() : NotFound();
    }
}
