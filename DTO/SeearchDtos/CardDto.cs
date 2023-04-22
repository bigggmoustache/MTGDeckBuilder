namespace MTGDeckBuilder.DTO.SeearchDtos
{
    public class CardDto
    {
        public string CardId { get; set; }
        public string Name { get; set; }
        public  string Color { get; set; }
        public  string ManaCost { get; set; }
        public string Power { get; set; }
        public string Toughness { get; set; }
        public Uri ImageUrl { get; set; }

    }
}
