using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EconomyViewerAPI.DAL.Entities;
public class Server
{
    [Key]
    public string Name { get; set; } = string.Empty;
    public List<Item> Items { get; set; } = new();

    public Server() { }
    public Server(string name, List<Item> items)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Items = items ?? throw new ArgumentNullException(nameof(items));
    }
}
