using MTGDeckBuilder.DataModels;

namespace MTGDeckBuilder.ViewModels.Deck
{
    public class ReadDeckViewModel
    {
        public int DeckId { get; set; }
        public string DeckName { get; set;}
        public MTGDeckBuilder.DataModels.Deck? Deck { get; set; }
        public List<Card> CardsInDeck { get; set; }
        public List<int> CardQuantities { get; set; }
        public string Error { get; set; }
    }
}
