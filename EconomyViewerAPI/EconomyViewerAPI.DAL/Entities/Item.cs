namespace EconomyViewerAPI.DAL.Entities;
public class Item
{
    private int _count = 1;
    private int _price = 1;

    public int Id { get; set; }
    public string Header { get; set; } = string.Empty;
    public int Count
    {
        get => _count;
        set {
            if (value > 0)
                _count = value;
        }
    }
    public int Price
    {
        get => _price;
        set {
            if (value > 0)
                _price = value;
        }
    }
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

    public static Item operator +(Item first, Item second)
    {
        return first.Equals(second) == false
            ? throw new ArgumentException("Items with different Headers, Mods and PriceForOne can not be added")
            : new Item(first.Header, first.Count + second.Count, first.Price + second.Price, first.Mod);
    }
    public static Item operator -(Item first, Item second)
    {
        return first.Equals(second) == false
            ? throw new ArgumentException("Items with different Headers, Mods and PriceForOne can not be substracted")
            : new Item(first.Header, first.Count - second.Count, first.Price - second.Price, first.Mod);
    }
}
