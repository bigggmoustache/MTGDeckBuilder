using Microsoft.EntityFrameworkCore;
using MTGDeckBuilder.DataModels;

namespace MTGDeckBuilder.Data
{
    public class MtgDeckDbContext : DbContext
    {
        public MtgDeckDbContext(DbContextOptions<MtgDeckDbContext> options) : base(options)
        {
        }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Card> Cards{ get; set; }
        public DbSet<CardDeck> CardDecks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardDeck>().HasKey(CD => new { CD.CardId, CD.DeckId });
        }
    }
}
