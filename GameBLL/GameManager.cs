using GameDAL;
using GameEL;

namespace GameBLL
{
    /// <summary>
    /// Manages the game logic, including player turns, round and determining game outcome.
    /// </summary>
    public class GameManager
    {
        #region FIELDS
        private readonly DeckRepository deckRepository;
        private readonly PlayerRepository playerRepository;
        private PlayerManager playerManager;
        private Player dealer;
        #endregion

        #region EVENTS
        public event PlayerTurnStartedEventHandler PlayerTurnStartedEvent;
        public event PlayerBustEventHandler PlayerBustEvent;
        public event PlayerBlackjackEventHandler PlayerBlackjackEvent;
        public event PlayerReached21EventHandler PlayerReached21Event;
        public event RoundOverEventHandler RoundOverEvent;
        #endregion

        #region DELEGATES
        public delegate void PlayerTurnStartedEventHandler(Player player);
        public delegate void PlayerBustEventHandler(Player player);
        public delegate void PlayerBlackjackEventHandler(Player player);
        public delegate void PlayerReached21EventHandler(Player player);
        public delegate void RoundOverEventHandler();
        #endregion

        #region PROPERTIES
        public Player CurrentPlayer { get; set; }
        public Deck GameDeck { get; set; }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the GameManager class. 
        /// </summary>
        /// <param name="playerManager">The PlayerManager managing the players in the game.</param>
        /// <param name="numOfDecks">The number of decks used in the game.</param>
        public GameManager(PlayerManager playerManager, int numOfDecks)
        {
            this.playerManager = playerManager;

            deckRepository = new DeckRepository();
            playerRepository = new PlayerRepository();

            LoadOrInitializeDeck(numOfDecks);
            LoadDealerFromDb();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Loads or initializes the game deck based on saved data or creates a new deck. 
        /// </summary>
        /// <param name="numOfDecks">The number of decks to use.</param>
        private void LoadOrInitializeDeck(int numOfDecks)
        {
            List<Deck> savedDecks = deckRepository.GetAllDecks();

            if (savedDecks != null && savedDecks.Any())
            {
                GameDeck = savedDecks.First();
            }
            else
            {
                GameDeck = new Deck(numOfDecks);
                deckRepository.AddDeck(GameDeck);
            }
        }

        /// <summary>
        /// Loads the dealer from the database or creates a new dealer.
        /// </summary>
        public void LoadDealerFromDb()
        {
            var dealerDb = playerRepository.GetDealer();

            if (dealerDb != null)
            {
                dealer = dealerDb;
            }
            else
            {
                dealer = playerManager.CreateDealer();
            }

            playerManager.Dealer = dealer;
        }

        /// <summary>
        /// Starts a new round by shuffling the deck, dealing cards to players, and setting the current player.
        /// </summary>
        public void StartRound()
        {
            ShuffleDeck();
            CurrentPlayer = playerManager.GetAt(0);

            // Deal 2 initial cards to players and dealer
            for (var i = 0; i < 2; i++)
            {
                foreach (var player in playerManager.Players)
                {
                    PlayerHit(player);
                }

                PlayerHit(dealer);
            }
        }

        /// <summary>
        /// Continues the round by updating the current player if the round is not over.
        /// </summary>
        public void ContinueRound()
        {
            if (!CheckIfRoundOver())
            {
                CurrentPlayer = playerManager.GetCurrentPlayer();
                CheckPlayerStatus();
            }
        }

        /// <summary>
        /// Shuffles the game deck and udpates the deck repository. 
        /// </summary>
        public void ShuffleDeck()
        {
            GameDeck.Shuffle();
            deckRepository.UpdateDeck(GameDeck);
        }

        /// <summary>
        /// Checks if the round is over by verifying the finishing status of all players and dealer.
        /// </summary>
        /// <returns>True if the round is over, otherwise false.</returns>
        public bool CheckIfRoundOver()
        {
            bool allPlayersFinished = playerManager.Players.All(player => player.IsFinished);
            bool dealerFinished = playerManager.Dealer.IsFinished;

            return allPlayersFinished && dealerFinished;
        }

        /// <summary>
        /// Checks if a new game has started by verifying if any player has cards in their hand.
        /// </summary>
        /// <returns>True if it's a new game, otherwise false.</returns>
        public bool CheckIfNewGame()
        {
            return !playerManager.Players.Any(player => player.Hand.Cards.Any());
        }

        /// <summary>
        /// Adds a card to the player's hand, and updates repositories.
        /// </summary>
        /// <param name="player">The player to perform the hit action.</param>
        public void PlayerHit(Player player)
        {
            player.Hand.AddCard(GameDeck.DrawNextCard());
            deckRepository.UpdateDeck(GameDeck);
            playerRepository.UpdatePlayer(player);
        }

        /// <summary>
        /// Initiates a player stand, sets the player as finished, and switches to the next player.
        /// </summary>
        public void PlayerStand()
        {
            CurrentPlayer.IsFinished = true;
            playerRepository.UpdatePlayer(CurrentPlayer);
            SwitchToNextPlayer();
        }

        /// <summary>
        /// Checks the status of the current player and performs actions based on the hand value. 
        /// </summary>
        public void CheckPlayerStatus()
        {
            if (CurrentPlayer.Hand.TotalHandValue > 21)
            {
                HandlePlayerBust();
            }
            else if (CurrentPlayer.Hand.TotalHandValue == 21 && CurrentPlayer.Hand.HandCount == 2)
            {
                HandlePlayerBlackjack();
            }
            else if (CurrentPlayer.Hand.TotalHandValue == 21)
            {
                HandlePlayerReached21();
            }
        }

        /// <summary>
        /// Handles the event of a player busting, updates player status, and switches to next player. 
        /// </summary>
        public void HandlePlayerBust()
        {
            CurrentPlayer.IsFinished = true;
            playerRepository.UpdatePlayer(CurrentPlayer);

            PlayerBustEvent?.Invoke(CurrentPlayer);
            SwitchToNextPlayer();
        }

        /// <summary>
        /// Handles the event of a player achieving a blackjack, updates player status, and switches to next player.
        /// </summary>
        public void HandlePlayerBlackjack()
        {
            CurrentPlayer.IsFinished = true;
            CurrentPlayer.IncrementTotalWins();
            playerRepository.UpdatePlayer(CurrentPlayer);

            PlayerBlackjackEvent?.Invoke(CurrentPlayer);
            SwitchToNextPlayer();
        }

        /// <summary>
        /// Handles the event of a player reaching a hand value of 21, updates player status, and switches to next player.
        /// </summary>
        public void HandlePlayerReached21()
        {
            CurrentPlayer.IsFinished = true;
            playerRepository.UpdatePlayer(CurrentPlayer);

            PlayerReached21Event?.Invoke(CurrentPlayer);
            SwitchToNextPlayer();
        }

        /// <summary>
        /// Switches to the next player or invokes the round over event if all players are finished.
        /// </summary>
        public void SwitchToNextPlayer()
        {
            if (CurrentPlayer.IsFinished)
            {
                int currentIndex = playerManager.Players.IndexOf(CurrentPlayer);

                if (currentIndex < playerManager.Players.Count - 1)
                {
                    // If there are more players in the list, move to the next player
                    CurrentPlayer = playerManager.Players[currentIndex + 1];
                    PlayerTurnStartedEvent?.Invoke(CurrentPlayer);
                    return;
                }

                // If the current player is the last in the list, invoke round over
                RoundOverEvent?.Invoke();
            }
        }

        /// <summary>
        /// Determines the winner of the game based on player and dealer scores. 
        /// </summary>
        /// <returns>The winning player or null in case of a tie.</returns>
        public Player DetermineGameOutcome()
        {
            Player winner = null;
            int dealerScore = playerManager.Dealer.Hand.TotalHandValue;
            int bestPlayerScore = 0;

            // Loops through each player and calculates the highest score
            foreach (var player in playerManager.Players)
            {
                int playerScore = player.Hand.TotalHandValue;

                if (playerScore <= 21 && playerScore > bestPlayerScore)
                {
                    bestPlayerScore = playerScore;
                    winner = player;
                }
            }

            // Compares the dealer's score
            if (dealerScore <= 21 && (winner == null || dealerScore > bestPlayerScore))
            {
                winner = playerManager.Dealer;
            }

            winner?.IncrementTotalWins();

            foreach (var player in playerManager.Players)
            {
                playerRepository.UpdatePlayer(player);
            }
            playerRepository.UpdatePlayer(playerManager.Dealer);

            return winner;
        }
        #endregion
    }
}