namespace EconomyViewerAPI.DAL.DTO;
public class ItemPutDTO
{
    public int Id { get; set; } = 0;
    public string Header { get; set; } = string.Empty;
    public int Count { get; set; } = 1;
    public int Price { get; set; } = 1;
    public string Mod { get; set; } = string.Empty;
    public string ServerName { get; set; } = string.Empty;
}
