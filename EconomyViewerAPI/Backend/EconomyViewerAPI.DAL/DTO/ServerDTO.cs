using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconomyViewerAPI.DAL.DTO;
public class ServerDTO
{
    public string Name { get; set; } = string.Empty;
    public List<ItemDTO> Items { get; set; } = new();
}
