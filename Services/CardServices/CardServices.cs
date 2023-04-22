using MTGDeckBuilder.DataModels;
using MTGDeckBuilder.DTO.SeearchDtos;

namespace MTGDeckBuilder.Services.CardServices
{
    public class CardServices
    {
        public Card CardFromDto(CardDto cardDto)
        {
            return new Card();
        }
    }
}

/*public int Id { get; set; }
public ICollection<Deck> Decks { get; set; }
public string Name { get; set; }
public Uri ImageUrl { get; set; }
public string Color { get; set; }
public string ManaCost { get; set; }
public string Power { get; set; }
public string Toughness { get; set; }*/