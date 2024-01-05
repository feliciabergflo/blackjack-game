using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace GameEL
{
    /// <summary>
    /// Represents a playing card with a specific value and suite. 
    /// </summary>
    public class Card
    {
        #region PROPERTIES
        /// <summary>
        /// Gets and sets the Id of the card. 
        /// </summary>
        [Key]
        public int CardId { get; set; }

        /// <summary>
        /// Gets and sets the value of the card.
        /// </summary>
        public CardValue Value { get; set; }

        /// <summary>
        /// Gets and sets the suite of the card. 
        /// </summary>
        public CardSuite Suite { get; set; }

        /// <summary>
        /// Foreign key to represent the one-to-many relationship with Deck.
        /// </summary>
        public int? DeckId { get; set; }
        public Deck Deck { get; set; }

        /// <summary>
        /// Foreign key to represent the one-to-many relationship with Hand.
        /// </summary>
        public int? HandId { get; set; }
        public Hand Hand { get; set; }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Default consuctor that initializes a new instance of the Card class.
        /// </summary>
        public Card()
        {
        }

        /// <summary>
        /// Initializes a new instance of the Card class with the specified value and suite.
        /// </summary>
        /// <param name="value">The value of the card.</param>
        /// <param name="suite">The suite of the card.</param>
        public Card(CardValue value, CardSuite suite, Deck deck)
        {
            Value = value;
            Suite = suite;
            Deck = deck;
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Assigns a Hand to the Card.
        /// </summary>
        /// <param name="hand">The hand to assign the card to.</param>
        public void AssignToHand(Hand hand)
        {
            Hand = hand;
            HandId = hand.HandId;
            DeckId = null;
        }

        /// <summary>
        /// Generates the filename of the associated PNG file for the card.
        /// </summary>
        /// <returns>The PNG filename representing the card.</returns>
        public string GetPngFileName()
        {
            string suiteStr = SuiteString();
            string valueStr = ValueString();

            return $"{suiteStr}{valueStr}.png";
        }

        /// <summary>
        /// Converts the card's suite to a string representation. 
        /// </summary>
        /// <returns>A string representation of the card's suite.</returns>
        public string SuiteString()
        {
            switch (Suite)
            {
                case CardSuite.Hearts:
                    return "h";
                case CardSuite.Diamonds:
                    return "d";
                case CardSuite.Clubs:
                    return "c";
                case CardSuite.Spades:
                    return "s";
                default:
                    throw new InvalidOperationException("Invalid card suite.");
            }
        }

        /// <summary>
        /// Converts the card's value to an string representation.
        /// </summary>
        /// <returns>A astring representation of the card's value.</returns>
        public string ValueString()
        {
            switch (Value)
            {
                case CardValue.Two:
                    return "2";
                case CardValue.Three:
                    return "3";
                case CardValue.Four:
                    return "4";
                case CardValue.Five:
                    return "5";
                case CardValue.Six:
                    return "6";
                case CardValue.Seven:
                    return "7";
                case CardValue.Eight:
                    return "8";
                case CardValue.Nine:
                    return "9";
                case CardValue.Ten:
                    return "10";
                case CardValue.Jack:
                    return "j";
                case CardValue.Queen:
                    return "q";
                case CardValue.King:
                    return "k";
                case CardValue.Ace:
                    return "1";
                default:
                    throw new InvalidOperationException("Invalid card value.");
            }
        }

        /// <summary>
        /// Converts the card's value to an integer representation.
        /// </summary>
        /// <returns>An integer representing the card's value.</returns>
        public int ValueInt()
        {
            switch (Value)
            {
                case CardValue.Two:
                    return 2;
                case CardValue.Three:
                    return 3;
                case CardValue.Four:
                    return 4;
                case CardValue.Five:
                    return 5;
                case CardValue.Six:
                    return 6;
                case CardValue.Seven:
                    return 7;
                case CardValue.Eight:
                    return 8;
                case CardValue.Nine:
                    return 9;
                case CardValue.Ten:
                case CardValue.Jack:
                case CardValue.Queen:
                case CardValue.King:
                    return 10;
                case CardValue.Ace:
                    return 11;
                default:
                    throw new InvalidOperationException("Invalid card value.");
            }
        }
        #endregion
    }
}
