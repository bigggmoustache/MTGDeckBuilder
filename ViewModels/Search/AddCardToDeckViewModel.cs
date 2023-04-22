using MTGDeckBuilder.DataModels;

namespace MTGDeckBuilder.ViewModels.Search
{
    public class AddCardToDeckViewModel
    {
        public List<MTGDeckBuilder.DataModels.Deck> UserDecks { get; set; }
        public string DeckSelected { get; set; }
        public MTGDeckBuilder.DataModels.Deck Deck { get; set; }
        public string Quantity { get; set; }
        public string CardMultiverseId { get; set; }
    }
}
