namespace RPiDefender.Data
{
    public class RestaurantMenu
    {
        public string RestaurantName { get; set; }

        public IEnumerable<MenuItem> Items { get; set; }
    }
}
