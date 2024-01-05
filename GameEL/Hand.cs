using System.ComponentModel.DataAnnotations;

namespace GameEL
{
    /// <summary>
    /// Reperesents a player's hand in a black jack game. 
    /// </summary>
    public class Hand
    {
        #region PROPERTIES
        /// <summary>
        /// Gets and sets the id of the hand.
        /// </summary>
        [Key]
        public int HandId { get; set; }

        /// <summary>
        /// Gets and sets the list of cards in the hand. 
        /// </summary>
        public ICollection<Card> Cards { get; set; }

        /// <summary>
        /// Gets and sets player who the hand belongs to.
        /// </summary>
        public Player Player { get; set; }
        public int PlayerId { get; set; }

        /// <summary>
        /// Gets the count of cards in the hand. 
        /// </summary>
        public int HandCount
        {
            get { return Cards.Count; }
        }

        /// <summary>
        /// Gets the last card in the collection of cards.
        /// </summary>
        public Card LastCard
        {
            get { return Cards.LastOrDefault(); }
        }

        /// <summary>
        /// Gets the calculated value of the hand based on the card values. 
        /// </summary>
        public int TotalHandValue
        {
            get { return CalculateTotalHandValue(); }
        }
        #endregion

        #region CONSTRUCTOR
        public Hand() 
        {
        }

        /// <summary>
        /// Initializes a new instance of the Hand class with an empty list of cards. 
        /// </summary>
        public Hand(Player player)
        {
            Player = player;
            Cards = new List<Card>();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Adds a card to the hand.
        /// </summary>
        /// <param name="card">The card to add.</param>
        public void AddCard(Card card)
        {
            card.AssignToHand(this);
            Cards.Add(card);
        }

        /// <summary>
        /// Clears the hand by removing all cards. 
        /// </summary>
        public void Clear()
        {
            Cards.Clear();
        }

        /// <summary>
        /// Calculates the total value of the hand basd on the card values and game-specific rules. 
        /// </summary>
        /// <returns>The total value of the hand.</returns>
        private int CalculateTotalHandValue()
        {
            int totalValue = 0;
            int numberOfAces = 0;

            foreach (Card card in Cards)
            {
                totalValue += card.ValueInt();

                if (card.Value == CardValue.Ace)
                {
                    numberOfAces++;
                }

                while (numberOfAces > 0 && totalValue > 21)
                {
                    totalValue -= 10;
                    numberOfAces--;
                }
            }

            return totalValue;
        }
        #endregion
    }
}
