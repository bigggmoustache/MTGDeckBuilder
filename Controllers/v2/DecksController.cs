using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MTGDeckBuilder.Data;
using MTGDeckBuilder.DataModels;

namespace MTGDeckBuilder.Controllers.v2
{
    [Route("v2/decks")]
    [ApiController]

    public class DecksController : Controller
    {
        private readonly MtgDeckDbContext _context;

        public DecksController(MtgDeckDbContext context)
        {
            _context = context;
        }

        // GET: Decks
        [HttpGet]
        public async Task<IActionResult> Index()
        {
              return _context.Decks != null ? 
                          View(await _context.Decks.ToListAsync()) :
                          Problem("Entity set 'MtgDeckDbContext.Decks'  is null.");
        }
        [HttpGet("Details/{id:int}")]
        // GET: Decks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Decks == null)
            {
                return NotFound();
            }

            var deck = await _context.Decks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deck == null)
            {
                return NotFound();
            }

            return View(deck);
        }
        [HttpGet("Create")]

        // GET: Decks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Decks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedByName,CreatedById,CreatedDate,LastModified,Color,White,Black,Red,Blue,Green,Colorless")] Deck deck)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deck);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deck);
        }

        // GET: Decks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Decks == null)
            {
                return NotFound();
            }

            var deck = await _context.Decks.FindAsync(id);
            if (deck == null)
            {
                return NotFound();
            }
            return View(deck);
        }

        // POST: Decks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedByName,CreatedById,CreatedDate,LastModified,Color,White,Black,Red,Blue,Green,Colorless")] Deck deck)
        {
            if (id != deck.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deck);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeckExists(deck.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(deck);
        }

        // GET: Decks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Decks == null)
            {
                return NotFound();
            }

            var deck = await _context.Decks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deck == null)
            {
                return NotFound();
            }

            return View(deck);
        }

        // POST: Decks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Decks == null)
            {
                return Problem("Entity set 'MtgDeckDbContext.Decks'  is null.");
            }
            var deck = await _context.Decks.FindAsync(id);
            if (deck != null)
            {
                _context.Decks.Remove(deck);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeckExists(int id)
        {
          return (_context.Decks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
