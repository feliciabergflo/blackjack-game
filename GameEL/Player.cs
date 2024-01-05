using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameEL
{
    /// <summary>
    /// Represents a player in a blackjack game. 
    /// </summary>
    public class Player : IPlayer
    {
        #region PROPERTIES
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the player's hand. One-to-one relationship.
        /// </summary>
        public Hand Hand { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player has finished their turn or game.
        /// </summary>
        public bool IsFinished { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is the dealer.
        /// </summary>
        public bool IsDealer { get; set; }

        /// <summary>
        /// Gets or sets the total number of wins by the player.
        /// </summary>
        public int TotalWins { get; set; }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Default consuctor that initializes a new instance of the Player class.
        /// </summary>
        public Player()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Player class with the specified properties.  
        /// </summary>
        /// <param name="name">The player's name.</param>
        /// <param name="isDealer">A value indicating if the player is the dealer.</param>
        public Player(string name, bool isDealer)
        {
            Name = name;
            Hand = new Hand(this);
            IsDealer = isDealer;
            IsFinished = false;
            TotalWins = 0;
        }
        #endregion

        #region METHODS
        /// Resets the player's state, including their hand.
        public void Reset()
        {
            Hand.Clear();
            IsFinished = false;
        }

        /// <summary>
        /// Increments the player's total wins count. 
        /// </summary>
        public void IncrementTotalWins()
        {
            TotalWins++;
        }

        /// <summary>
        /// Returns a string representation of the player. 
        /// </summary>
        public override string ToString()
        {
            return $"{Name} (Hand: {Hand.TotalHandValue}  Wins: {TotalWins})";
        }
        #endregion
    }
}
