using HtmlAgilityPack;
using Microsoft.AspNetCore.Http.Features;
using RPiDefender.Pages;
using System.Text;

namespace RPiDefender.Data
{
    public class MenuScrapingService
    {
        public async Task<IEnumerable<RestaurantMenu>> GetMenu(string[] menuIds)
        {
            StringBuilder condition = new StringBuilder();
            foreach (var menuId in menuIds)
            {
                if (condition.Length != 0)
                {
                    condition.Append(" | ");
                }

                condition.Append($"//div[@class='restaurace {menuId}']");
            }

            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = await web.LoadFromWebAsync("https://www.olomouc.cz/poledni-menu");
            var restaurants = document.DocumentNode.SelectNodes(condition.ToString());

            var result = new List<RestaurantMenu>();

            foreach (var restaurant in restaurants)
            {
                var menu = new RestaurantMenu();

                menu.RestaurantName = restaurant.SelectSingleNode("div[@class='nazev-restaurace']/div/h3/a").InnerText.Replace("&nbsp;", " ");

                var tableNode = restaurant.SelectNodes("table/tr");

                var menuItems = new List<MenuItem>();

                foreach (var row in tableNode)
                {
                    var menuItem = new MenuItem();
                    var allItems = row.SelectNodes("td");

                    menuItem.Order = allItems.ToList()[0].InnerText.Replace("&nbsp;", " ");
                    menuItem.Name = allItems.ToList()[1].InnerText.Replace("&nbsp;", " ");
                    menuItem.Price = allItems.ToList()[2].InnerText.Replace("&nbsp;", " ");

                    menuItems.Add(menuItem);
                }

                menu.Items = menuItems;

                result.Add(menu);
            }

            return result;
        }

        public async Task<string?> GetRestaurantName(string id)
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = await web.LoadFromWebAsync("https://www.olomouc.cz/poledni-menu");
            var restaurant = document.DocumentNode.SelectSingleNode($"//div[@class='restaurace {id}']");

            if (restaurant == null)
                return null;

            return restaurant.SelectSingleNode("div[@class='nazev-restaurace']/div/h3/a").InnerText.Replace("&nbsp;", " ");
        }
    }
}
