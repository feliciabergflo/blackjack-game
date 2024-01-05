using GameBLL;
using GameEL;

namespace MSTestBLL
{
    /// <summary>
    /// Unit tests for the PlayerManager class, using MSTest.
    /// </summary>
    [TestClass]
    public class PlayerManagerTests
    {
        /// <summary>
        /// Verifies that the GetCurrentPlauer method returns the player who is not finished
        /// when there is a player whose finished and one whose not.
        /// </summary>
        [TestMethod]
        public void GetCurrentPlayer_WhenPlayerIsNotFinished_ReturnNotFinishedPlayer()
        {
            // Arrange: Set up scenario with one player whose finished and one whose not
            var playerManager = new PlayerManager();

            var player1 = new Player("Player1", isDealer: false);
            var player2 = new Player("Player2", isDealer: false);
            player1.IsFinished = true;
            player2.IsFinished = false;

            playerManager.Players.AddRange(new List<Player> { player1, player2 });

            // Act: Call the GetCurrentPlayer method
            var result = playerManager.GetCurrentPlayer();

            // Assert: Verify that the method returned the player whose not finished
            Assert.AreEqual(player2, result, "GetCurrentPlayer did not return the expected player.");
        }

        /// <summary>
        /// Verifies that the CheckIndex method returns false when the index is higher
        /// than the player count in the game. 
        /// </summary>
        [TestMethod]
        public void CheckIndex_WhenIndexIsHigherThanPlayerCount_ReturnFalse()
        {
            // Arrange: Set up scenario with 2 players in game
            var playerManager = new PlayerManager();

            var player1 = new Player("Player1", isDealer: false);
            var player2 = new Player("Player2", isDealer: false);

            playerManager.Players.AddRange(new List<Player> { player1, player2 });

            // Act: Call the CheckIndex method
            bool result = playerManager.CheckIndex(3);

            // Assert: Verify that the method returned false 
            Assert.AreEqual(false, result, "CheckIndex did not return false when the index is higher than the player count.");
        }
    }
}
