using GameDAL;
using GameEL;
using Microsoft.EntityFrameworkCore;

namespace NUnitDAL
{
    /// <summary>
    /// Unit tests for the DeckRepository class, using NUnit.
    /// </summary>
    [TestFixture]
    public class DeckRepositoryTests
    {
        private DeckRepository deckRepository;
        private Deck testDeck;

        /// <summary>
        /// Set up method that initialize the DeckRepository and create a test deck.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            deckRepository = new DeckRepository();
            testDeck = new Deck(1);
        }

        /// <summary>
        /// Verifies that the AddDeck method correctly adds a deck to the database.
        /// </summary>
        [Test]
        public void AddDeck_ShouldAddDeckToDatabase()
        {
            // Arrange: Get the initial deck count
            int initialDeckCount;
            using (var context = new GameDbContext())
            {
                initialDeckCount = context.Decks.Count();
            }

            // Act: Call the AddDeck method
            deckRepository.AddDeck(testDeck);

            // Assert:  Verify that a deck has been added to the database
            using (var context = new GameDbContext())
            {
                int newDeckCount = context.Decks.Count();
                Assert.That(newDeckCount, Is.EqualTo(initialDeckCount + 1));
            }
        }

        /// <summary>
        /// Verifies that the GetAllDecks method returns a list oft decks with non-null cards.
        /// </summary>
        [Test]
        public void GetAllDecks_ShouldReturnAllDecks()
        {
            // Arrange
            using (var context = new GameDbContext())
            {
                context.Decks.Add(testDeck);
                context.SaveChanges();
            }

            // Act
            List<Deck> result = deckRepository.GetAllDecks();

            // Assert
            Assert.That(result, Has.All.Property(nameof(Deck.Cards)).Not.Null);
        }

        /// <summary>
        /// Teardown method to clean up the test by removing the test deck and 
        /// associated cards from the database. 
        /// </summary>
        [TearDown] 
        public void Teardown()
        {
            using (var context = new GameDbContext())
            {
                var deckToRemove = context.Decks
                    .Include(d => d.Cards)
                    .FirstOrDefault(d => d.DeckId == testDeck.DeckId);

                if (deckToRemove != null)
                {
                    var associatedCards = deckToRemove.Cards?.ToList();

                    if (associatedCards != null && associatedCards.Any())
                    {
                        context.Cards.RemoveRange(associatedCards);
                        context.SaveChanges();
                    }

                    context.Decks.Remove(deckToRemove);
                    context.SaveChanges();
                }
            }
        }
    }
}
