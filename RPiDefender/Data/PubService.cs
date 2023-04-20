namespace RPiDefender.Data
{
    public class PubService
    {
        private List<Pub> _pubs;
        private readonly MenuScrapingService _menuScrapingService;

        public event EventHandler<EventArgs> PubsChanged;

        public IEnumerable<Pub> Pubs { get => _pubs; 
            private set
            {
                _pubs = value.ToList();
            }
        }

        public PubService(MenuScrapingService menuScrapingService)
        {
            _menuScrapingService = menuScrapingService;
            Pubs = new List<Pub>();
            foreach (var item in new[] { "BOUNTY-ROCK-CAFE-id3259", "Bistro-Paulus-6806", "MD-Original-1869-id2208" })
            {
                _ = AddRestaurant(item);
            }
        }

        public async Task AddRestaurant(string id)
        {
            var pubName = await _menuScrapingService.GetRestaurantName(id);
            if (pubName == null)
            {
                return;
            }
            _pubs.Add(new Pub() { Id = id, Name = pubName });
            PubsChanged?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveRestaurant(Pub restaurant)
        {
            _pubs.Remove(restaurant);
            PubsChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
