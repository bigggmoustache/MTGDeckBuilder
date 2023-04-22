using MTGDeckBuilder.DataModels;

namespace MTGDeckBuilder.Services.DeckServices
{
    public class DeckServices
    {
        public static Deck SetDeckColor(Deck deck, Card card)
        {
            if (card.ManaCost.ToLower().Contains("r")) { deck.Red = true; }
            if (card.ManaCost.ToLower().Contains("g")) { deck.Green = true; }
            if (card.ManaCost.ToLower().Contains("u")) { deck.Blue = true; }
            if (card.ManaCost.ToLower().Contains("w")) { deck.White = true; }
            if (card.ManaCost.ToLower().Contains("b")) { deck.Black = true; }
            else { deck.Colorless = true; }
            return deck;
        }
    }
}
