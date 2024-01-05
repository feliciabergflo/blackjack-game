using GameBLL;
using GameEL;

namespace BlackJackGame
{
    /// <summary>
    /// Provides the GUI for player interactions, game state display, and event handling. 
    /// </summary>
    public partial class MainForm : Form
    {
        #region FIELDS
        private PlayerManager playerManager;
        private GameManager gameManager;
        private Player dealer;
        private bool roundStarted = false;
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes the primary form for the Blackjack game.
        /// </summary>
        /// <param name="playerManager">The PlayerManager managing the players in the game.</param>
        /// <param name="gameManager">The GameManager controlling the game logic.</param>
        public MainForm(PlayerManager playerManager, GameManager gameManager)
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            this.playerManager = playerManager;
            this.gameManager = gameManager;
            this.dealer = playerManager.Dealer;

            gameManager.PlayerTurnStartedEvent += InformTurnStarted;
            gameManager.PlayerBustEvent += InformPlayerBust;
            gameManager.PlayerBlackjackEvent += InformPlayerBlackjack;
            gameManager.PlayerReached21Event += InformPlayerReached21;
            gameManager.RoundOverEvent += InformRoundOver;

            InitializeComponent();
            InitializeGUI();

        }
        #endregion

        #region METHODS
        #region GUI Methods
        /// <summary>
        /// Initializes the GUI components based on the current game state.
        /// </summary>
        private void InitializeGUI()
        {
            if (!roundStarted)
            {
                ClearGUI();
                UpdateListBox();
            }
        }

        /// <summary>
        /// Clears the GUI components.
        /// </summary>
        private void ClearGUI()
        {
            pnlDealerHand.Controls.Clear();
            pnlPlayerHand.Controls.Clear();

            lblPlayerWins.Text = "";
            lblDealerWins.Text = "";
        }

        /// <summary>
        /// Updates the displayed deck count on the GUI.
        /// </summary>
        private void UpdateDeckCount()
        {
            int deckCount = gameManager.GameDeck.DeckCount;
            lblDeckCount.Text = deckCount.ToString();
        }

        /// <summary>
        /// Updates the player list in the ListBox on the GUI. 
        /// </summary>
        private void UpdateListBox()
        {
            lstPlayers.Items.Clear();

            List<Player> players = playerManager.Players;
            foreach (Player player in players)
            {
                lstPlayers.Items.Add(player);
            }
        }

        /// <summary>
        /// Updates the player name labels on the GUI. 
        /// </summary>
        /// <param name="currentPlayer">The current player in turn.</param>
        private void UpdateNameLabels(Player currentPlayer)
        {
            string playerName = currentPlayer.Name;
            lblPlayerWinsLabel.Text = $"{playerName}'s Wins";
        }

        /// <summary>
        /// Updates the displayed win counts for players and daeler on the GUI. 
        /// </summary>
        /// <param name="currentPlayer">The current player in turn.</param>
        private void UpdateWinsLabels(Player currentPlayer)
        {
            lblPlayerWins.Text = currentPlayer.TotalWins.ToString();
            lblDealerWins.Text = dealer.TotalWins.ToString();
        }

        /// <summary>
        /// Updates the player's or dealer's hand panels on the GUI. 
        /// </summary>
        /// <param name="player">The player or dealer whose hand panel to update.</param>
        /// <param name="showFaceDownCard">A glaf indicating whether to show the face-down card.</param>
        private void UpdateHandPanels(Player player, bool showFaceDownCard)
        {
            // Get the pdf file name of the cards
            string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string pdfFolderName = Path.Combine(projectPath, "CardImages");
            string pdfFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, pdfFolderName);
            string pdfFileName = "";

            // Determine if player is dealer and gets it's GUI panel
            bool isDealer = player.IsDealer;
            Panel playerPanel = isDealer ? pnlDealerHand : pnlPlayerHand;

            // Clear panel of existing cards
            playerPanel.Controls.Clear();

            // X-coordinates for positioning cards within the panel
            int cardX = 20;

            // Iterate through cards in player's hand and add to UI
            foreach (var card in player.Hand.Cards)
            {
                // Check if it's the dealer and showFaceDownCard is true
                if (card == player.Hand.LastCard && isDealer && !showFaceDownCard)
                {
                    pdfFileName = "b2fv.png";
                }
                else
                {
                    pdfFileName = card.GetPngFileName();
                }

                // Construct the full path to the PDF file
                string pdfFilePath = Path.Combine(pdfFolderPath, pdfFileName);

                // Check if the PDF file exists 
                if (File.Exists(pdfFilePath))
                {
                    PictureBox cardPictureBox = new PictureBox();
                    cardPictureBox.Size = new Size(55, 77);
                    cardPictureBox.BackColor = Color.White;
                    cardPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    cardPictureBox.Location = new Point(cardX, 18);
                    cardPictureBox.Image = Image.FromFile(pdfFilePath);

                    // Calculate the Y-coordinate to center the PictureBox vertically
                    int centerY = (playerPanel.Height - cardPictureBox.Height) / 2;
                    cardPictureBox.Location = new Point(cardX, centerY);
                    cardPictureBox.Image = Image.FromFile(pdfFilePath);

                    // Add the card PictureBox to the player's panel
                    playerPanel.Controls.Add(cardPictureBox);

                    // Increment the X-coordinate for the next card
                    cardX += 75;
                }
                else
                {
                    MessageBox.Show("Code path not found.", "Error");
                }
            }
        }

        /// <summary>
        /// Updates the states of button on the GUI based on current game state.
        /// </summary>
        private void UpdateButtonStates()
        {
            btnStartRound.Enabled = !roundStarted;
            btnHit.Enabled = roundStarted;
            btnStand.Enabled = roundStarted;
            btnShuffle.Enabled = roundStarted;
        }

        #endregion

        #region Event Handler Methods
        /// <summary>
        /// Initiates a new round or continues the current round.
        /// </summary>
        private void btnStartRound_Click(object sender, EventArgs e)
        {
            bool isNewGame = gameManager.CheckIfNewGame();

            if (isNewGame)
            {
                MessageBox.Show("New Round!");
                gameManager.StartRound();
            }
            else
            {
                MessageBox.Show("Continuing Round!");
                gameManager.ContinueRound();
            }

            roundStarted = true;

            UpdateNameLabels(gameManager.CurrentPlayer);
            UpdateWinsLabels(gameManager.CurrentPlayer);
            UpdateHandPanels(gameManager.CurrentPlayer, showFaceDownCard: true);
            UpdateHandPanels(playerManager.Dealer, showFaceDownCard: false);

            UpdateDeckCount();
            UpdateButtonStates();
            UpdateListBox();
        }

        /// <summary>
        /// Initiates a player hit during their turn.
        /// </summary>
        private void btnHit_Click(object sender, EventArgs e)
        {
            // Check if round is over
            if (gameManager.CheckIfRoundOver())
            {
                MessageBox.Show("Round over. Please start a new round.");
                return;
            }

            Player currentPlayer = gameManager.CurrentPlayer;
            gameManager.PlayerHit(currentPlayer);
            UpdateHandPanels(currentPlayer, showFaceDownCard: true);
            UpdateDeckCount();
            UpdateListBox();

            gameManager.CheckPlayerStatus();

            UpdateWinsLabels(currentPlayer);
        }

        /// <summary>
        /// Initiates a player stand during their turn. 
        /// </summary>
        private void btnStand_Click(object sender, EventArgs e)
        {
            // Checks if round is over
            if (gameManager.CheckIfRoundOver())
            {
                MessageBox.Show("Game over. Please start a new round.");
                return;
            }

            MessageBox.Show("Player " + gameManager.CurrentPlayer.Name + " stands.");
            gameManager.PlayerStand();
        }

        /// <summary>
        /// Shuffles the game deck if confirmed by the user. 
        /// </summary>
        private void btnShuffle_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to shuffle the deck?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    gameManager.GameDeck.Shuffle();
                    MessageBox.Show("Deck shuffled.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An exception occurred: " + ex.Message);
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Exits the application if confirmed by the user. 
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to exit?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
            else
            {
                return;
            }
        }
        #endregion

        #region Game Handler Methods
        /// <summary>
        /// Informs the player whose turn has started and updates the GUI. 
        /// </summary>
        /// <param name="currentPlayer">The player whose turn has started.</param>
        private void InformTurnStarted(Player currentPlayer)
        {
            MessageBox.Show(currentPlayer.Name + "'s turn started.");
            UpdateButtonStates();
            UpdateNameLabels(currentPlayer);
            UpdateWinsLabels(currentPlayer);
            UpdateListBox();
            UpdateHandPanels(currentPlayer, showFaceDownCard: true);
        }

        /// <summary>
        /// Informs that a player has busted and updates the GUI. 
        /// </summary>
        /// <param name="currentPlayer">The player who has busted.</param>
        private void InformPlayerBust(Player currentPlayer)
        {
            MessageBox.Show(currentPlayer.Name + " busts!");
        }

        /// <summary>
        /// Informs tha a player has achieved a blackjack and updates the GUI. 
        /// </summary>
        /// <param name="currentPlayer">The player who achieved a blackjack.</param>
        private void InformPlayerBlackjack(Player currentPlayer)
        {
            MessageBox.Show(currentPlayer.Name + " got a blackjack!");
            UpdateWinsLabels(currentPlayer);
            UpdateListBox();

        }

        /// <summary>
        /// Informs that a player has reached a hand value of 21 and updates the GUI. 
        /// </summary>
        /// <param name="currentPlayer">The player who reached 21.</param>
        private void InformPlayerReached21(Player currentPlayer)
        {
            MessageBox.Show(currentPlayer.Name + " reached 21!");
        }

        /// <summary>
        /// Informs that the round is over, initiates the dealer's turn, and updates the GUI. 
        /// </summary>
        private void InformRoundOver()
        {
            MessageBox.Show("Round over! Dealer's turn.");
            HandleDealerTurnGUI();

            UpdateListBox();
            UpdateWinsLabels(gameManager.CurrentPlayer);
            roundStarted = false;

            playerManager.ClearAllHands();

            UpdateButtonStates();
        }

        /// <summary>
        /// Handles the GUI updates during the dealer's turn.
        /// </summary>
        private void HandleDealerTurnGUI()
        {
            // Reveal dealer's face-down card
            UpdateHandPanels(dealer, showFaceDownCard: true);

            if (dealer.Hand.TotalHandValue < 17)
            {
                MessageBox.Show("Dealer's score is 16 or less, and the dealer will draw another card.");
                gameManager.PlayerHit(dealer);
                UpdateHandPanels(dealer, showFaceDownCard: true);
                UpdateDeckCount();
            }
            else
            {
                MessageBox.Show("Dealer's score is 17 or higher, and the dealer stands.");
            }

            playerManager.Dealer.IsFinished = true;

            // Call method that determines game outcome
            Player winner = gameManager.DetermineGameOutcome();
            btnHit.Enabled = false;
            btnStand.Enabled = false;
            InformWinner(winner);
        }

        /// <summary>
        /// Informs the winner of the round and updates the GUI. 
        /// </summary>
        /// <param name="winner">The player who won.</param>
        private void InformWinner(Player winner)
        {
            if (winner != null)
            {
                MessageBox.Show(winner.Name + " is the winner of this round!");
            }
            else
            {
                MessageBox.Show("It's a tie or everyone busted. There is no winner this round.");
            }
        }
        #endregion
        #endregion
    }
}
