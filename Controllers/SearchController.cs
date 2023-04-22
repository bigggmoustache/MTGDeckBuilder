
using Microsoft.AspNetCore.Mvc;
using MTGDeckBuilder.Models.Search;
using MTGDeckBuilder.DTO.SeearchDtos;
using MtgApiManager;
using MtgApiManager.Lib.Service;
using System.ComponentModel;
using MTGDeckBuilder.Data;
using Microsoft.AspNetCore.Identity;
using MTGDeckBuilder.Services.DeckServices;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MTGDeckBuilder.ViewModels.Search;
using MTGDeckBuilder.DataModels;
using MTGDeckBuilder.Services.CardServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Security.Principal;
using MtgApiManager.Lib.Model;

namespace MTGDeckBuilder.Controllers
{
    public class SearchController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        public IMtgServiceProvider _mtgService;
        public readonly MtgDeckDbContext _mtgDbContext;
        public SearchController(IMtgServiceProvider mtgService, MtgDeckDbContext mtgDbContext,
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _mtgDbContext = mtgDbContext;
            _mtgService = mtgService;
        }

        public string? NameSort { get; set; }
        public string? DateSort { get; set; }
        public string? CurrentFilter { get; set; }
        public string? CurrentSort { get; set; }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CardSearch(CardSearchViewModel model)
        {
            
            if (model.Query != null)
            {
                string query = model.Query;
                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CardSearch(CardSearchViewModel model, string? sortOrder = null) 
        {
            if (model.Query != null)
            {
                ICardService service = _mtgService.GetCardService();
                var result = await service.Where(x => x.Name, model.Query).AllAsync();
                if (!result.IsSuccess)
                {
                    return NotFound();
                }

                List<Card> cardList = new List<Card>();
                for (int i = 0; i < result.Value.Count; i++)
                {
                    cardList.Add(new Card());
                    cardList[i].MultiverseId = result.Value[i].Id;
                    cardList[i].Name = result.Value[i].Name;
                    cardList[i].ImageUrl = result.Value[i].ImageUrl;
                    cardList[i].ManaCost = result.Value[i].ManaCost;
                    cardList[i].Power = result.Value[i].Power;
                    cardList[i].Toughness = result.Value[i].Toughness;
                    cardList[i].Type = result.Value[0].Type;
                }

                model.CardList = cardList;

                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCardToDb(Card userCard)
        {
            Card cardCheck = _mtgDbContext.Cards.FirstOrDefault(c => c.MultiverseId == userCard.MultiverseId);
            if (cardCheck.MultiverseId != userCard.MultiverseId)
            {
                _mtgDbContext.Cards.Add(userCard);
                await _mtgDbContext.SaveChangesAsync();
            }
            string emptyString = "";
            return await (AddCardToDeck(new AddCardToDeckViewModel { }, cardCheck.MultiverseId, emptyString));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddCardToDeck(AddCardToDeckViewModel model, string cardMultiverseId, string emptyString)
        {
            var userId = _userManager.GetUserId(User).ToString();
            var userDecks = _mtgDbContext.Decks.AsQueryable().Where(d => d.CreatedById == userId).ToList();
            model.UserDecks = userDecks;
            model.CardMultiverseId = cardMultiverseId;
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCardToDeck(AddCardToDeckViewModel model, string deckSelected)
        {
            //Get UserId, Query Service, Check Result
            var userId = _userManager.GetUserId(User).ToString();

            //Select deck
            Deck deck = await _mtgDbContext.Decks.FirstOrDefaultAsync(d => d.Name == deckSelected
                && d.CreatedById == userId);

            //creates card object
            Card card = _mtgDbContext.Cards.FirstOrDefault(c => c.MultiverseId == model.CardMultiverseId);

            //Card Deck object creation required to add contextual join information
            var cardDeck = new CardDeck();

            cardDeck = _mtgDbContext.CardDecks.FirstOrDefault(c => c.DeckId == deck.Id && c.CardId == card.Id);


            //check if join quantity == null, if so create join and increase quant
            if (cardDeck.Quantity == null || cardDeck.Quantity == 0)
            {
                cardDeck.Card = card;
                cardDeck.CardId = card.Id;
                cardDeck.Deck = deck;
                cardDeck.DeckId = deck.Id;
                cardDeck.Quantity = 1;
                _mtgDbContext.CardDecks.Add(cardDeck);
                //update deck color before 
                deck = DeckServices.SetDeckColor(deck, card);
            }

            //check if card exceeds limit of 4
            if ((cardDeck.Quantity < 4 ) || card.Type == "Land")
            {
                cardDeck.Quantity++;
            }

            await _mtgDbContext.SaveChangesAsync();

            return RedirectToAction("ReadDeck", "Deck", new { deckId = deck.Id });
        }
    }
}