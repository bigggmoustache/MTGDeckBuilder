using System.ComponentModel.DataAnnotations;

namespace MTGDeckBuilder.DataModels
{
    public class Deck
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedById { get; set; }
        public string CreatedDate { get; set; }
        public string? LastModified { get; set; }
        public string? Color { get; set; }
        public bool? White { get; set; }
        public bool? Black { get; set; }
        public bool? Red { get; set; }
        public bool? Blue { get; set; }
        public bool? Green { get; set; }
        public bool? Colorless { get; set; }
        public List<CardDeck> Cards { get; set; }

    }
}
