namespace MTGDeckBuilder.ViewModels.Deck
{
    public class CreateDeckViewModel
    {
        public string Name { get; set; }
        public bool IsOverDeckLimit { get; set; } = false;
    }
}
