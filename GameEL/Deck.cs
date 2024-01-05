using System.ComponentModel.DataAnnotations;

namespace GameEL
{
    /// <summary>
    /// Represents a deck of playing cards that is used in black jack games. 
    /// </summary>
    public class Deck
    {
        #region PROPERTIES
        /// <summary>
        /// Gets and sets the id of the deck.
        /// </summary>
        [Key]
        public int DeckId { get; set; }

        /// <summary>
        /// Gets and sets the number of decks used in this deck.
        /// </summary>
        public int NumberOfDecks { get; set; }

        /// <summary>
        /// Gets and sets the collection of cards.
        /// </summary>
        public ICollection<Card> Cards { get; set; }

        /// <summary>
        /// Gets the count of remaining cards in the deck. 
        /// </summary>
        public int DeckCount
        {
            get { return Cards.Count; }
        }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Default consuctor that initializes a new instance of the Deck class.
        /// </summary>
        public Deck() 
        {
        }

        /// <summary>
        /// Initializes a new instance of the Deck class with the specified number of decks. 
        /// </summary>
        /// <param name="numOfDecks">The number of decks to use in the deck.</param>
        public Deck(int numOfDecks)
        {
            NumberOfDecks = numOfDecks;
            InitializeDeck(numOfDecks);
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Initializes the deck with the specified number of decks, populating it with playing cards.
        /// </summary>
        /// <param name="numOfDecks">The number of decks to use in the deck.</param>
        public void InitializeDeck(int numOfDecks)
        {
            if (numOfDecks < 1)
            {
                throw new ArgumentException("Number of decks must be at least 1.");
            }

            Cards = new List<Card>();

            // Iterates through each card in each deck and adds it to list of cards
            for (int i = 0; i < numOfDecks; i++)
            {
                foreach (CardSuite suite in Enum.GetValues(typeof(CardSuite)))
                {
                    foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                    {
                        Cards.Add(new Card(value, suite, this));
                    }
                }
            }
        }

        /// <summary>
        /// Shuffles the deck of cards using the Fisher-Yates shuffle algorithm.
        /// </summary>
        public void Shuffle()
        {
            List<Card> cardList = Cards.ToList();

            Random random = new Random();
            int n = Cards.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                Card temp = cardList[i];
                cardList[i] = cardList[j];
                cardList[j] = temp;
            }

            Cards = cardList;
        }

        /// <summary>
        /// Draws the next card from the deck, repopulating and shuffling the deck if empty.
        /// </summary>
        /// <returns>The card drawn from the tyop of the deck.</returns>
        public Card DrawNextCard()
        {
            if (Cards.Count == 0)
            {
                InitializeDeck(NumberOfDecks);
                Shuffle();
            }

            // Draws from the top of the deck
            Card drawnCard = Cards.First();
            Cards.Remove(drawnCard);

            return drawnCard;
        }
        #endregion
    }
}
