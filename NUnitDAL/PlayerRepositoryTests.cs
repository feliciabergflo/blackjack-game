using NUnit.Framework;
using GameDAL;
using GameEL;

namespace NUnitDAL
{
    /// <summary>
    /// Unit tests for the PlayerRepository class, using NUnit.
    /// </summary>
    [TestFixture]
    public class PlayerRepositoryTests
    {
        private PlayerRepository playerRepository;
        private Player testPlayer;

        /// <summary>
        /// Set up method that initialize the PlayerRepository and creates a test player.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            playerRepository = new PlayerRepository();
            testPlayer = new Player("Player", isDealer: false);
        }

        /// <summary>
        /// Verifies that the AddPlayer method correctly adds a player to the database.
        /// </summary>
        [Test]
        public void AddPlayer_ShouldAddPlayerToDatabase()
        {
            // Arrange: Get the initial player count
            int initialPlayerCount;
            using (var context = new GameDbContext())
            {
                initialPlayerCount = context.Players.Count();
            }

            // Act: Call the AddPlayer method
            playerRepository.AddPlayer(testPlayer);

            // Assert: Verify that a player has been added to the database
            using (var context = new GameDbContext())
            {
                int newPlayerCount = context.Players.Count();
                Assert.That(newPlayerCount, Is.EqualTo(initialPlayerCount + 1));
            }
        }

        /// <summary>
        /// Verifies that the DeletePlayer method correctly deletes a player from the database.
        /// </summary>
        [Test]
        public void DeletePlayer_ShouldDeletePlayerFromDatabase()
        {
            // Arrange
            using (var context = new GameDbContext())
            {
                context.Players.Add(testPlayer);
                context.SaveChanges();
            }

            int initialPlayerCount;
            using (var context = new GameDbContext())
            {
                initialPlayerCount = context.Players.Count();
            }

            // Act: Call the DeletePlayer method
            playerRepository.DeletePlayer(testPlayer);

            // Assert: Verify that a player has been deleted from the database
            using (var context = new GameDbContext())
            {
                int newPlayerCount = context.Players.Count();
                Assert.That(newPlayerCount, Is.EqualTo(initialPlayerCount - 1));
            }
        }

        /// <summary>
        /// Teardown method to clean up the test by removing the test player 
        /// from the database. 
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            using (var context = new GameDbContext())
            {
                var playerToRemove = context.Players.Find(testPlayer.PlayerId);
                if (playerToRemove != null)
                {
                    context.Players.Remove(playerToRemove);
                    context.SaveChanges();
                }
            }
        }
    }
}