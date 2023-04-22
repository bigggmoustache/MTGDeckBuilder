using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Project;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MTGDeckBuilder.Data;
using MTGDeckBuilder.DataModels;
using MTGDeckBuilder.Services.DeckServices;
using MTGDeckBuilder.ViewModels.Deck;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace MTGDeckBuilder.Controllers
{
    public class DeckController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private MtgDeckDbContext _mtgDbContext;
        private readonly IConfiguration _configuration;


        public DeckController(UserManager<IdentityUser> userManager, MtgDeckDbContext mtgDbContext
            , IConfiguration configuration)
        {
            _userManager = userManager;
            _mtgDbContext = mtgDbContext;
            _configuration = configuration;
        }

        [HttpGet]
        [Authorize]
        public IActionResult CreateDeck(CreateDeckViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeck(CreateDeckViewModel model, string? blank)
        {
            var user = await _userManager.GetUserAsync(User);

            Deck deck = new Deck();
            deck.Name = model.Name.ToUpper();
            deck.CreatedById = user.Id;
            deck.CreatedByName = user.UserName;
            deck.CreatedDate = DateTime.Now.ToString();
            _mtgDbContext.Add(deck);
            await _mtgDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(IndexViewModel model, string? filter, SortOption? sort, int? pageIndex)
        {
            List<bool> deckColorBools = new List<bool>();
            if (!String.IsNullOrEmpty(filter)) { filter.ToUpper(); };
            var decksIQ = _mtgDbContext.Decks.AsQueryable();


            if (!string.IsNullOrEmpty(filter))
            {
                decksIQ = decksIQ.Where(t => t.Name.Contains(filter));
            }
            
            if (sort is { } s)
            {
                decksIQ = s switch
                {
                    SortOption.NameDesc => decksIQ.OrderByDescending(t => t.Name),
                    SortOption.NameAsc => decksIQ.OrderBy(t => t.Name),
                    _ => decksIQ.OrderBy(t => t.Name)
                };
            }

            var pageSize = _configuration.GetValue("PageSize", 3);
            var paginatedList = await PaginatedList<Deck>.CreateAsync(decksIQ, pageIndex ?? 1, pageSize);

            model.Decks = paginatedList;
            
            model.Filter = filter;
            model.Sort = sort;
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ReadDeck(ReadDeckViewModel model, int deckId)
        {
            Deck deck = await _mtgDbContext.Decks.FirstOrDefaultAsync(d => d.Id == deckId);
            List<Card> cards = new List<Card>();
            List<int> cardQuantity = new List<int>();
            //c.CardDecks must be filtered by decks too 
            foreach (var cardDeck in _mtgDbContext.CardDecks.Where(cd => cd.DeckId == deckId))
            {
                var card = _mtgDbContext.Cards.FirstOrDefault(c => c.Id == cardDeck.CardId);
                cards.Add(card);
                cardQuantity.Add(cardDeck.Quantity);
            }

            model.CardsInDeck = cards;
            model.CardQuantities = cardQuantity;
            model.Deck = deck;
            return View(model);
        }
    }
}
