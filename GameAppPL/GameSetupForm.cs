using GameBLL;
using GameDAL;
using GameEL;

namespace BlackJackGame
{
    /// <summary>
    /// Responsible for setting up the game, including player management initial game configuration.
    /// </summary>
    public partial class GameSetupForm : Form
    {
        #region FIELDS
        private PlayerManager playerManager;
        private GameManager gameManager;
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes playerManager and prepares the GUI. 
        /// </summary>
        public GameSetupForm()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            playerManager = new PlayerManager();

            InitializeComponent();
            ClearGUI();
            SetGUI();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Clears the GUI controls.
        /// </summary>
        private void ClearGUI()
        {
            txtNumDecks.Text = string.Empty;
            txtName.Text = string.Empty;
            lstPlayerNames.Items.Clear();
        }

        /// <summary>
        /// Sets the GUI with player names and number of decks. 
        /// </summary>
        private void SetGUI()
        {
            // Retrieve player list and populate list box
            List<Player> playerList = playerManager.Players;
            foreach (Player player in playerList)
            {
                lstPlayerNames.Items.Add(player.Name);
            }

            // Retrieve the decks from the database and display it 
            using (var context = new GameDbContext())
            {
                var numberOfDecks = (from deck in context.Decks
                                     select deck.NumberOfDecks).FirstOrDefault();

                if (numberOfDecks > 0)
                {
                    txtNumDecks.Text = numberOfDecks.ToString();
                    txtNumDecks.Enabled = false;
                }
            }
        }

        /// <summary>
        /// Checks the validity of user input.
        /// </summary>
        /// <param name="numDecks">Output parameter that received number of decks.</param>
        /// <returns>True if input is valid; otherwise, false.</returns>
        private bool CheckInput(out int numDecks)
        {
            bool playerListOK = false;
            bool numDecksOK = false;

            if (lstPlayerNames.Items != null || lstPlayerNames.Items.Count > 0)
            {
                playerListOK = true;
            }

            if (int.TryParse(txtNumDecks.Text, out numDecks))
            {
                numDecksOK = true;
            }

            return playerListOK && numDecksOK;
        }

        /// <summary>
        /// Adds a new player to the game, updating the player list.
        /// </summary>
        private void btnAddPlayer_Click(object sender, EventArgs e)
        {
            string playerName = txtName.Text.Trim();

            if (!string.IsNullOrEmpty(playerName))
            {
                Player newPlayer = playerManager.CreatePlayer(playerName);

                lstPlayerNames.Items.Add(newPlayer.Name);

                txtName.Clear();
            }
            else
            {
                MessageBox.Show("You must provide a name before adding a player.", "Error");
            }
        }

        /// <summary>
        /// Edits the selected player's name, updating the player list.
        /// </summary>
        private void btnEditPlayer_Click(object sender, EventArgs e)
        {
            if (lstPlayerNames.SelectedItems != null)
            {
                string newPlayerName = txtName.Text.Trim();
                int selectedIndex = lstPlayerNames.SelectedIndex;

                // Checks if a new name is provided
                if (string.IsNullOrEmpty(newPlayerName))
                {
                    MessageBox.Show("You must provide a new name.", "Error");
                    return;
                }

                bool changeOK = playerManager.ChangePlayer(newPlayerName, selectedIndex);
                lstPlayerNames.Items[selectedIndex] = newPlayerName;

                if (!changeOK)
                {
                    MessageBox.Show("Failed to change player name.", "Error");
                }

                txtName.Clear();
            }
            else
            {
                MessageBox.Show("You must select a player before changing the name.", "Error");
            }
        }

        /// <summary>
        /// Deletes the selected player or players from the game, updating the player list.
        /// </summary>
        private void btnDeletePlayer_Click(object sender, EventArgs e)
        {
            if (lstPlayerNames.SelectedItems != null && lstPlayerNames.SelectedItems.Count > 0)
            {
                var indicesToRemove = new List<int>();

                foreach (var selectedItem in lstPlayerNames.SelectedItems)
                {
                    int selectedIndex = lstPlayerNames.Items.IndexOf(selectedItem);
                    indicesToRemove.Add(selectedIndex);
                }

                foreach (int selectedIndex in indicesToRemove)
                {
                    lstPlayerNames.Items.RemoveAt(selectedIndex);
                    bool removalOK = playerManager.RemovePlayer(selectedIndex);

                    if (!removalOK)
                    {
                        MessageBox.Show("Failed to remove player.", "Error");
                    }
                }
            }
            else
            {
                MessageBox.Show("You must select one or more players before deleting.", "Error");
            }
        }

        /// <summary>
        /// Starts the game with the specified parameters.
        /// </summary>
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            if (!CheckInput(out int numDecks))
            {
                MessageBox.Show("Cannot read name or deck values.", "Error");
                return;
            }

            gameManager = new GameManager(playerManager, numDecks);
            MainForm mainForm = new MainForm(playerManager, gameManager);
            mainForm.Show();
            this.Hide();
        }
        #endregion
    }
}
