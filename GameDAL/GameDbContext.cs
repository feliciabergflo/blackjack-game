using GameEL;
using Microsoft.EntityFrameworkCore;

namespace GameDAL
{
    /// <summary>
    /// Represents the database context for the game application. 
    /// </summary>
    public class GameDbContext : DbContext
    {
        #region TABLES
        public DbSet<Player> Players { get; set; }
        public DbSet<Hand> Hands { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Card> Cards { get; set; }
        #endregion

        #region METHODS
        /// <summary>
        /// Configures the database connection and options.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source = (localdb)\MSSQLLocalDB; Database = GameDb; Integrated Security = True;");
        }

        /// <summary>
        /// Configures the relationship and ocnstraints between entities in the database.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder model)
        {
            // Configure the one-to-one relationship between Hand and Player
            model.Entity<Player>()
                .HasOne(p => p.Hand)
                .WithOne(h => h.Player)
                .HasForeignKey<Hand>(h => h.PlayerId);

            // Configure the one-to-many relationship between Hand and Card
            model.Entity<Hand>()
                .HasMany(h => h.Cards)
                .WithOne(c => c.Hand)
                .HasForeignKey(c => c.HandId);

            // Configure the one-to-many relationship between Deck and Card
            model.Entity<Deck>()
                .HasMany(d => d.Cards)
                .WithOne(c => c.Deck)
                .HasForeignKey(c => c.DeckId);

            // Configure the optional relationship for Card to Hand and Deck
            model.Entity<Card>()
                .Property(c => c.HandId)
                .IsRequired(false);

            model.Entity<Card>()
                .Property(c => c.DeckId)
                .IsRequired(false);


            base.OnModelCreating(model);
        }
        #endregion
    }
}
