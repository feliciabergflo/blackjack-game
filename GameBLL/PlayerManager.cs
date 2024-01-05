using GameDAL;
using GameEL;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace GameBLL
{
    /// <summary>
    /// Manages players, the dealer, and all participant-related logic for a card game.
    /// </summary>
    public class PlayerManager
    {
        #region FIELDS
        private readonly PlayerRepository playerRepository;
        #endregion

        #region PROPERTIES
        public int PlayerCount
        {
            get { return Players.Count; }
        }

        public virtual List<Player> Players { get; set; }

        public virtual Player Dealer { get; set; }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the PlayerManager class. 
        /// </summary>
        public PlayerManager()
        {
            playerRepository = new PlayerRepository();
            Players = new List<Player>();
            LoadPlayersFromDb();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Loads players from saved data.
        /// </summary>
        public void LoadPlayersFromDb()
        {
            var playersDb = playerRepository.GetAllPlayers();

            if (playersDb.Any())
            {
                Players = playersDb;
            }
        }

        /// <summary>
        /// Creates the dealer if one does not exist and adds them to the game.
        /// </summary>
        /// <returns>True if the dealer was created; false if not.</returns>
        public Player CreateDealer()
        {
            Dealer = new Player("Dealer", isDealer: true);
            playerRepository.AddPlayer(Dealer);

            return Dealer;
        }

        /// <summary>
        /// Creates a new player with the specified name and adds them to the game. 
        /// </summary>
        /// <param name="name">The name of the player to create</param>
        /// <returns>True if the player was created; false if not.</returns>
        public Player CreatePlayer(string name)
        {
            Player newPlayer = new Player(name, isDealer: false);

            playerRepository.AddPlayer(newPlayer);
            Players.Add(newPlayer);

            return newPlayer;
        }

        /// <summary>
        /// Changes the name of a player and updates the database. 
        /// </summary>
        /// <param name="newName">The new name to assign to the player </param>
        /// <param name="index">The index of the player to change.</param>
        /// <returns>True if the player's name is successfully changed, otherwise false.</returns>
        public bool ChangePlayer(string newName, int index)
        {
            if (index < 0 || index >= Players.Count)
            {
                return false;
            }

            Player changedPlayer = GetAt(index);
            changedPlayer.Name = newName;
            playerRepository.UpdatePlayer(changedPlayer);

            return true;
        }

        /// <summary>
        /// Removes a player with the specified id.
        /// </summary>
        /// <param name="index">The index of the player to remove.</param>
        public bool RemovePlayer(int index)
        {
            if (index < 0 || index >= Players.Count)
            {
                return false;
            }

            Player playerToDelete = GetAt(index);
            playerRepository.DeletePlayer(playerToDelete);
            Players.RemoveAt(index);

            return true;
        }

        /// <summary>
        /// Clears the hands and resets the state of all players and the dealer.
        /// </summary>
        public void ClearAllHands()
        {
            foreach (var player in Players)
            {
                player.Reset();
                playerRepository.UpdatePlayer(player);
            }

            if (Dealer != null)
            {
                Dealer.Reset();
                playerRepository.UpdatePlayer(Dealer);
            }
        }

        /// <summary>
        /// Retrieves the current player who has not finished their turn. 
        /// </summary>
        public Player GetCurrentPlayer()
        {
            return Players.FirstOrDefault(player => !player.IsFinished);
        }

        /// <summary>
        /// Checks if the given index is valid for accessing a player.
        /// </summary>
        /// <param name="index">The index to check.</param>
        /// <returns>True if the index is valid; false otherwise.</returns>
        public bool CheckIndex(int index)
        {
            return index >= 0 && index < PlayerCount;
        }

        /// <summary>
        /// Retrieves the player at the specified index in the list of players.
        /// </summary>
        /// <param name="index">The index of the player to retrieve.</param>
        /// <returns>The player at the specified index.</returns>
        public Player GetAt(int index)
        {
            return Players[index];
        }
        #endregion
    }
}
