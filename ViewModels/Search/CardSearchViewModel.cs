using MTGDeckBuilder.DataModels;
using MTGDeckBuilder.DTO.SeearchDtos;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace MTGDeckBuilder.Models.Search
{
    public class CardSearchViewModel
    {
        public string? Query { get; set; }
        public string CardSelected { get; set; }
        public int CardQuantity { get; set; }
        public List<Card> CardList { get; set; }
        public string NameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public string Error { get; set; }

    }
}
