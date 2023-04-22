using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTGDeckBuilder.DataModels
{
    public class CardDeck
    {
        public int CardId { get; set; }
        public Card Card { get; set; }
        public int DeckId { get; set; }
        public Deck Deck { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
