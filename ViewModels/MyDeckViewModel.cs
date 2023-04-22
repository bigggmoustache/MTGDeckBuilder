namespace MTGDeckBuilder.ViewModels
{
    public class MyDeckViewModel
    {
        public List<MyCardViewModel> Cards { get; set; }

        public List<MyCardViewModel> SourceCard { get; set; }
        public int DeckId { get; set; }
    }
}
