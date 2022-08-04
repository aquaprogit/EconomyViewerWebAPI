
using EconomyViewerAPI.BLL.Services.Interfaces;
using EconomyViewerAPI.DAL.Entities;

using HtmlAgilityPack;

using System.Text.RegularExpressions;

namespace EconomyViewerAPI.BLL.Services;
public class ServersParser : IServerLoader
{
    private static readonly Regex _itemPattern = new Regex(@"(?<Header>.+)\s(?<Count>[0-9]+) шт. - (?<Price>[0-9]+)$");
    private static readonly Regex _singleItemPattent = new Regex(@"(?<Header>.+)\W+(?<Price>\d+)");
    private Dictionary<string, string>? _serverToLink;
    private Dictionary<string, string> GetServerNamesToLinks()
    {
        if (_serverToLink != null)
            return _serverToLink;

        HtmlWeb web = new HtmlWeb();
        HtmlDocument serversPage = new HtmlDocument();
        serversPage.LoadHtml(web.Load(@"https://f.simpleminecraft.ru/index.php?/forum/49-jekonomika/").Text);
        List<HtmlNode>? links = serversPage.DocumentNode.SelectNodes("//a[@href]")
            .Where(node => {
                string innerText = node.InnerText.Trim();
                return innerText.StartsWith("Экономика") && innerText.Length > "Экономика".Length + 1;
            })
            .ToList();

        _serverToLink =
            new Dictionary<string, string>(links.Select(link =>
                new KeyValuePair<string, string>(link.InnerText.Trim().Split()[1], link.Attributes["href"].Value)));

        return _serverToLink;
    }
    public async Task<List<Server>> GetAllServersAsync()
    {
        List<Server> result = new();
        IEnumerable<string> names = GetServerNamesToLinks().Keys;
        foreach (string serverName in names)
        {
            Server server = await GetServerByNameAsync(serverName);
            result.Add(server);
        }
        return result;
    }

    public async Task<Server> GetServerByNameAsync(string serverName)
    {
        HtmlWeb web = new HtmlWeb();
        HtmlDocument page = new HtmlDocument();
        HtmlDocument htmlDocument = await web.LoadFromWebAsync(GetServerNamesToLinks()[serverName]);
        page.LoadHtml(htmlDocument.Text);

        HtmlNode? post = page.DocumentNode.SelectSingleNode(@"//div[@data-role=""commentContent""]");
        IEnumerable<string>? lines = post.InnerText.Split("\n").Select(l => l.Trim()).Where(l => l.Length > 0);

        Task<List<Item>> items = Task.Run(() => {
            List<Item> result = new List<Item>();
            string currentMod = "";
            foreach (string line in lines)
            {
                Item? item = ItemFromString(line, currentMod);
                if (item == null)
                    currentMod = line.TrimEnd(':', '*').Replace("&amp; ", "");
                else
                    result.Add(item);
            }
            return result;
        });
        return new Server(serverName, await items);
    }
    public static Item? ItemFromString(string self, string mod)
    {
        if (_itemPattern.IsMatch(self) == false && _singleItemPattent.IsMatch(self) == false)
            return null;

        Regex pattern = _itemPattern.IsMatch(self) ? _itemPattern : _singleItemPattent;
        GroupCollection groups = pattern.Match(self).Groups;

        string header = groups["Header"].Value.TrimStart(' ', '\t');
        int count = _itemPattern.IsMatch(self) ? Convert.ToInt32(groups["Count"].Value) : 1;
        int price = Convert.ToInt32(groups["Price"].Value);

        return new Item(header, count, price, mod);
    }

}
