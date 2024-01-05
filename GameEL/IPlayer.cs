namespace GameEL
{
    /// <summary>
    /// Represents a player in a game. 
    /// </summary>
    public interface IPlayer
    {
        #region PROPERTIES
        /// <summary>
        /// Gets or sets the player's name. 
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indiciating if a player has finished their turn. 
        /// </summary>
        bool IsDealer { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the player is the dealer. 
        /// </summary>
        bool IsFinished { get; set; }

        /// <summary>
        /// Gets or sets the total number of wins by the player. 
        /// </summary>
        int TotalWins { get; set; }
        #endregion

        #region METHODS
        /// <summary>
        /// Resets the player's state. 
        /// </summary>
        void Reset();

        /// <summary>
        /// Increments the player's total wins count. 
        /// </summary>
        void IncrementTotalWins();

        /// <summary>
        /// Returns a string representation of the player. 
        /// </summary>
        string ToString();
        #endregion
    }
}