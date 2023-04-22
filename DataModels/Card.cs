using System.ComponentModel.DataAnnotations;

namespace MTGDeckBuilder.DataModels
{
    public class Card
    {
        [Key] 
        public int Id { get; set; }
        public string MultiverseId { get; set;  }
        public string Name { get; set; }
        public Uri ImageUrl { get; set; }
        public string? ManaCost { get; set; }
        public string? Power { get; set; }
        public string? Toughness { get; set; }
        public string Type { get; set; }
        public List<CardDeck> CardDecks { get; set; }

    }
}
