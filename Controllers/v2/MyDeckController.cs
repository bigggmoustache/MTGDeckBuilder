using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MTGDeckBuilder.Data;
using MTGDeckBuilder.DataModels;
using MTGDeckBuilder.ViewModels;

namespace MTGDeckBuilder.Controllers.v2
{
    [Route("mydeck/{id:int}")]
    public class MyDeckController : Controller
    {
        private readonly MtgDeckDbContext _context;

        public MyDeckController(MtgDeckDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var vm = new MyDeckViewModel();
            vm.DeckId = id;
            vm.Cards = new List<MyCardViewModel>();
            var cardDecks = await _context.CardDecks.Where(x=>x.DeckId == id).ToListAsync();
            foreach(var cardDeck in cardDecks)
            {
                vm.Cards.Add(_context.Cards.Select(x=>new MyCardViewModel
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl.ToString()
                }).Single(x => x.Id == cardDeck.CardId));
            }
            vm.SourceCard = await _context.Cards.Select(x => new MyCardViewModel
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl.ToString()
            }).ToListAsync();

            return View(vm);
        }

        [HttpPost("{cardId:int}")]
        public async Task<IActionResult> Add(int id, int cardId)
        {
            var item = new CardDeck { DeckId = id, CardId = cardId };
            await _context.CardDecks.AddAsync(item);
            await _context.SaveChangesAsync();
            var vm = new MyDeckViewModel();
            vm.DeckId = id;
            vm.Cards = new List<MyCardViewModel>();
            var cardDecks = await _context.CardDecks.Where(x => x.DeckId == id).ToListAsync();
            foreach (var cardDeck in cardDecks)
            {
                vm.Cards.Add(_context.Cards.Select(x => new MyCardViewModel
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl.ToString()
                }).Single(x => x.Id == cardDeck.CardId));
            }
            vm.SourceCard = await _context.Cards.Select(x => new MyCardViewModel
            {
                Id = x.Id,
                ImageUrl = x.ImageUrl.ToString()
            }).ToListAsync();

            return View(vm);
        }
    }
}
