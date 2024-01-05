using GameBLL;
using GameEL;
using Moq;

namespace MSTestBLL
{
    /// <summary>
    /// Unit tests for the GameManager class, using MSTest.
    /// </summary>
    [TestClass]
    public class GameManagerTests
    {
        /// <summary>
        /// Verifies that the SwitchToNextPlayer method moves to the next player when the 
        /// current player is finished and not the last player in the list.
        /// </summary>
        [TestMethod]
        public void SwitchToNextPlayer_WhenNotLastPlayer_ShouldMoveToNextPlayer()
        {
            // Arrange: Set up scenario with finished current player who is not the last player
            var playerManagerMock = new Mock<PlayerManager>();
            var gameManager = new GameManager(playerManagerMock.Object, numOfDecks: 1);

            var player1 = new Player("Player1", isDealer: false);
            var player2 = new Player("Player2", isDealer: false);
            gameManager.CurrentPlayer = player1;
            player1.IsFinished = true;

            playerManagerMock.Setup(m => m.Players).Returns(new List<Player> { player1, player2 });

            // Act: Call the SwitchToNextPlayer method
            gameManager.SwitchToNextPlayer();

            // Assert: Verify that the method moved to the next player
            Assert.AreEqual(player2, gameManager.CurrentPlayer, "SwitchToNextPlayer did not move to the next player.");
        }

        /// <summary>
        /// Verifies that the CheckIfRoundOver method returns true if all players 
        /// and dealer are finished. 
        /// </summary>
        [TestMethod]
        public void CheckIfRoundOver_WhenAllPlayersAndDealerFinished_ShouldReturnTrue()
        {
            // Arrange: Set up scenario with all players and dealer finished
            var playerManagerMock = new Mock<PlayerManager>();
            var gameManager = new GameManager(playerManagerMock.Object, numOfDecks: 1);

            var player1 = new Player("Player1", isDealer: false);
            var player2 = new Player("Player2", isDealer: false);
            var dealer = new Player("Dealer", isDealer: true);

            player1.IsFinished = true;
            player2.IsFinished = true;
            dealer.IsFinished = true;

            playerManagerMock.Setup(m => m.Players).Returns(new List<Player> { player1, player2 });
            playerManagerMock.Setup(m => m.Dealer).Returns(dealer);

            // Act: Call the CheckIfRoundOver method
            bool result = gameManager.CheckIfRoundOver();

            // Assert: Verify that the result is true
            Assert.IsTrue(result, "CheckIfRoundOver did not return true when all players and dealer finished.");
        }
    }
}