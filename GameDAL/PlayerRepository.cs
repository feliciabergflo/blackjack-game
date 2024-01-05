using GameEL;
using Microsoft.EntityFrameworkCore;

namespace GameDAL
{
    /// <summary>
    /// Repository class for managing players in the database. 
    /// </summary>
    public class PlayerRepository
    {
        public PlayerRepository() { }

        /// <summary>
        /// Adds a player and their cards to the database.
        /// </summary>
        /// <param name="player">The player to be added.</param>
        public void AddPlayer(Player player)
        {
            if (player != null)
            {
                using (var context = new GameDbContext())
                {
                    context.Players.Add(player);
                    context.SaveChanges();

                    player.Hand.PlayerId = player.PlayerId;
                    context.SaveChanges();

                    foreach (var card in player.Hand.Cards)
                    {
                        card.HandId = player.Hand.HandId;
                    }

                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Updates a player and it's cards in the database. 
        /// </summary>
        /// <param name="player">The player to be updated.</param>
        public void UpdatePlayer(Player player)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));

            using (var context = new GameDbContext())
            {
                var dbPlayer = context.Players
                    .Include(p => p.Hand)
                    .ThenInclude(h => h.Cards)
                    .SingleOrDefault(p => p.PlayerId == player.PlayerId);

                if (dbPlayer == null) throw new InvalidOperationException("Player not found.");

                // Update player properties
                context.Entry(dbPlayer).CurrentValues.SetValues(player);

                if (player.Hand != null)
                {
                    // Create new hand if it doesn't exist
                    if (dbPlayer.Hand == null)
                    {
                        dbPlayer.Hand = new Hand();
                    }
                    context.Entry(dbPlayer.Hand).CurrentValues.SetValues(player.Hand);

                    // Identify cards to remove
                    var cardsToRemove = dbPlayer.Hand.Cards
                        .Where(dbCard => player.Hand.Cards.All(pc => pc.CardId != dbCard.CardId))
                        .ToList();

                    // Remove old cards
                    foreach (var card in cardsToRemove)
                    {
                        dbPlayer.Hand.Cards.Remove(card);
                        context.Cards.Remove(card);
                    }

                    // Identify cards to add
                    var cardsToAdd = player.Hand.Cards
                        .Where(pc => dbPlayer.Hand.Cards.All(dbCard => dbCard.CardId != pc.CardId))
                        .ToList();

                    // Add new cards
                    foreach (var card in cardsToAdd)
                    {
                        dbPlayer.Hand.Cards.Add(new Card
                        {
                            CardId = card.CardId,
                            Value = card.Value,
                            Suite = card.Suite,
                        });
                    }
                    context.SaveChanges();

                    // Update the key id of all cards
                    foreach (var card in player.Hand.Cards)
                    {
                        var dbCard = dbPlayer.Hand.Cards
                            .FirstOrDefault(c => c.Value == card.Value && c.Suite == card.Suite);

                        if (dbCard != null && dbCard.CardId == 0)
                        {
                            card.CardId = dbCard.CardId;
                        }
                    }

                    // Update values of common cards
                    foreach (var trackedCard in dbPlayer.Hand.Cards)
                    {
                        var playerCard = player.Hand.Cards
                            .SingleOrDefault(pc => pc.CardId == trackedCard.CardId);

                        if (playerCard != null)
                        {
                            context.Entry(trackedCard).CurrentValues.SetValues(playerCard);
                        }
                    }
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a player from the database. 
        /// </summary>
        /// <param name="player">The player to be deleted.</param>
        public void DeletePlayer(Player player)
        {
            if (player != null)
            {
                using (var context = new GameDbContext())
                {
                    context.Players.Attach(player);
                    context.Players.Remove(player);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Retrieves all players (excluding the dealer) from the database.
        /// </summary>
        /// <returns>A list of all players in the database.</returns>
        public List<Player> GetAllPlayers()
        {
            using (var context = new GameDbContext())
            {
                var players = context.Players
                    .Where(player => !player.IsDealer)
                    .Include(player => player.Hand)
                    .ThenInclude(hand => hand.Cards)
                    .ToList();

                return players;
            }
        }

        /// <summary>
        /// Retrieves the dealer from the database.
        /// </summary>
        /// <returns>The dealer player.</returns>
        public Player GetDealer()
        {
            using (var context = new GameDbContext())
            {
                var dealer = context.Players
                    .Where(player => player.IsDealer)
                    .Include(player => player.Hand)
                    .ThenInclude(hand => hand.Cards)
                    .FirstOrDefault();

                return dealer;
            }
        }
    }
}