using HtmlAgilityPack;
using Microsoft.AspNetCore.Http.Features;

namespace RPiDefender.Data
{
    public class MenuScrapingService
    {
        public async Task<IEnumerable<RestaurantMenu>> GetMenu()
        {
            //class="restaurace BOUNTY-ROCK-CAFE-id3259"
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = await web.LoadFromWebAsync("https://www.olomouc.cz/poledni-menu");
            var restaurants = document.DocumentNode.SelectNodes("//div[@class='restaurace BOUNTY-ROCK-CAFE-id3259']");

            var result = new List<RestaurantMenu>();

            foreach (var restaurant in restaurants)
            {
                var menu = new RestaurantMenu();

                menu.RestaurantName = restaurant.SelectSingleNode("div[@class='nazev-restaurace']/div/h3/a").InnerText;

                var tableNode = restaurant.SelectNodes("table/tr");

                var menuItems = new List<MenuItem>();

                foreach (var row in tableNode)
                {
                    var menuItem = new MenuItem();
                    var allItems = row.SelectNodes("td");

                    menuItem.Order = allItems.ToList()[0].InnerText;
                    menuItem.Name = allItems.ToList()[1].InnerText;
                    menuItem.Price = allItems.ToList()[2].InnerText;

                    menuItems.Add(menuItem);
                }

                menu.Items = menuItems;

                result.Add(menu);
            }

            return result;
        }
    }
}
