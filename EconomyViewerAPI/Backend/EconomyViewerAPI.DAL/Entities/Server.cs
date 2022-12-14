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
    public override bool Equals(object? obj)
    {
        return obj is Server server &&
            server.Items.Count == Items.Count &&
            server.Items.SequenceEqual(Items) &&
            server.Name == Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Items, Name);
    }
}
