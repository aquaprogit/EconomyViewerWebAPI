using System;

namespace EconomyViewerAPI.DAL.Entities;
public class Item
{
    public int Id { get; set; }
    public string Header { get; set; } = string.Empty;
    public int Count { get; set; }
    public int Price { get; set; }
    public string Mod { get; set; } = string.Empty;

    public int PriceForOne => Price / Count;
    public Item() { }

    public Item(string header, int count, int price, string mod)
    {
        Header = header ?? throw new ArgumentNullException(nameof(header));
        Count = count;
        Price = price;
        Mod = mod ?? throw new ArgumentNullException(nameof(mod));
    }
    public override string ToString()
    {
        return $"{Header} {Count} шт. - {Price}";
    }
    public override bool Equals(object? obj)
    {
        return obj is Item item &&
            item.Header == Header &&
            item.PriceForOne == PriceForOne &&
            item.Mod == Mod;
    }
    public override int GetHashCode()
    {
        return HashCode.Combine(Header, PriceForOne, Mod);
    }
}
