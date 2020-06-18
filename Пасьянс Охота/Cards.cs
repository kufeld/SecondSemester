using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Пасьянс_Охота
{
    public enum CardSuit
    {
        /// <summary>
        /// черви
        /// </summary>
        Hearts = 0,
        /// <summary>
        /// бубны
        /// </summary>
        Diamonds = 1,
        /// <summary>
        /// крести
        /// </summary>
        Clubs = 2,
        /// <summary>
        /// пики
        /// </summary>
        Spades = 3,
    }

    public enum CardValue
    {
        Six = 0,
        Seven = 1,
        Eight = 2, 
        Nine = 3,
        Ten = 4,
        Jack = 5,
        Queen = 7,
        King = 8,
        Ace = 9,
    }

    public class Card
    {
        public readonly CardValue cardValue;
        public readonly CardSuit cardSuit;
        public int stackNumber;

        public Card(CardValue cardValue, CardSuit cardSuit, int stackNumber = -1)
        {
            this.cardSuit = cardSuit;
            this.cardValue = cardValue;
            this.stackNumber = stackNumber;
        }

        public Card(int value, int suit, int stackNumber = -1)
        {
            this.cardSuit = (CardSuit)suit;
            this.cardValue = (CardValue)value;
            this.stackNumber = stackNumber;
        }

        public Card WithChangedStackNumber(int stackNumber)
        {
            this.stackNumber = stackNumber;
            return this;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(Card))
                return false;
            var secondCard = (Card) obj;
            return secondCard.cardSuit == cardSuit && secondCard.cardValue == cardValue;
        }

        public override int GetHashCode()
        {
            return 1;
        }

        public bool EqualsByValue(Card card) => card.cardValue == cardValue;
    }
}
