using GameEL;
using Microsoft.EntityFrameworkCore;

namespace GameDAL
{
    /// <summary>
    /// Repository class for managing decks in the database. 
    /// </summary>
    public class DeckRepository
    { 
        public DeckRepository() { }

        /// <summary>
        /// Adds a deck and it's cards to the database.
        /// </summary>
        /// <param name="gameDeck">The deck to be added.</param>
        public void AddDeck(Deck gameDeck)
        {
            if (gameDeck != null)
            {
                using (var context = new GameDbContext())
                {
                    context.Decks.Add(gameDeck);
                    context.SaveChanges();

                    foreach (var card in gameDeck.Cards)
                    {
                        card.DeckId = gameDeck.DeckId;
                    }

                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Updates a deck and it's cards in the database. 
        /// </summary>
        /// <param name="gameDeck">The deck to be updates.</param>
        public void UpdateDeck(Deck gameDeck)
        {
            if (gameDeck == null) throw new ArgumentNullException(nameof(gameDeck));

            using (var context = new GameDbContext())
            {
                var dbDeck = context.Decks
                    .Include(d => d.Cards)
                    .SingleOrDefault(d => d.DeckId == gameDeck.DeckId);

                if (dbDeck == null) throw new InvalidOperationException("Deck not found.");

                // Update deck properties
                context.Entry(dbDeck).CurrentValues.SetValues(gameDeck);
                context.SaveChanges();

                // Identify cards to remove
                var cardsToRemove = dbDeck.Cards
                    .Where(dbCard => gameDeck.Cards.All(gameCard => gameCard.CardId != dbCard.CardId))
                    .ToList();

                // Remove old cards
                foreach (var card in cardsToRemove)
                {
                    dbDeck.Cards.Remove(card);
                }
                context.SaveChanges();

                // Identify cards to add
                var cardsToAdd = gameDeck.Cards
                    .Where(gameCard => dbDeck.Cards.All(dbCard => dbCard.CardId != gameCard.CardId))
                    .ToList();

                // Add new cards
                foreach (var card in cardsToAdd)
                {
                    dbDeck.Cards.Add(new Card
                    {
                        Value = card.Value,
                        Suite = card.Suite,
                    });
                }
                context.SaveChanges();

                // Update the key id of all cards
                foreach (var card in gameDeck.Cards)
                {
                    var dbCard = dbDeck.Cards
                        .Where(c => c.Value == card.Value && c.Suite == card.Suite && !gameDeck.Cards.Any(gc => gc.CardId == c.CardId))
                        .FirstOrDefault();

                    if (dbCard != null && card.CardId == 0)
                    {
                        card.CardId = dbCard.CardId;
                    }
                }

                // Update values of common cards
                foreach (var dbCard in dbDeck.Cards)
                {
                    var gameCard = dbDeck.Cards
                        .FirstOrDefault(gc => gc.CardId == dbCard.CardId);

                    if (gameCard != null)
                    {
                        context.Entry(dbCard).CurrentValues.SetValues(gameCard);
                    }
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Retrieves all decks from the database.
        /// </summary>
        /// <returns>A list of all decks in the database.</returns>
        public List<Deck> GetAllDecks()
        {
            using (var context = new GameDbContext())
            {
                var decks = context.Decks
                    .Include(deck => deck.Cards)
                    .ToList();

                return decks;
            }
        }
    }
}
