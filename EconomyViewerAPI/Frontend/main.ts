class Item {
    Header: string;
    Count: number;
    Price: number;
    Mod: string;

    constructor(header: string, count: number, price: number, mod: string) {
        this.Header = header;
        this.Count = count;
        this.Price = price;
        this.Mod = mod;
    }
}