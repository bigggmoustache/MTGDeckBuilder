using MTGDeckBuilder.Services.DeckServices;
using MTGDeckBuilder.DataModels;

namespace MTGDeckBuilder.ViewModels.Deck
{
    public class IndexViewModel
    {
        public PaginatedList<MTGDeckBuilder.DataModels.Deck>? Decks { get; set; }
        public string? Filter { get; set; }
        public SortOption? Sort { get; set; }
    }
}
