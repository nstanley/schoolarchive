using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardLibrary;

namespace FanTan
{
    public class Player
    {
        public string Name { get; private set; }
        public HandRow[] hand { get; private set; }
        public int NumCards { get; protected set; }
        public Card[] properHand { get; private set; }
        public bool[] validIndecies { get; private set; }

        public Player(string name, DisplayTable dt)
        {
            Name = name;
            hand = new HandRow[4];
            hand[0] = new HandRow(dt.DisplayCards[0]);
            hand[1] = new HandRow(dt.DisplayCards[1]);
            hand[2] = new HandRow(dt.DisplayCards[2]);
            hand[3] = new HandRow(dt.DisplayCards[3]);
            validIndecies = new bool[13];
            NumCards = 0;
        }
        /// <summary>
        /// A proper hand is one such that the cards are in order, in a single row
        /// //For smoother valid index implementation
        /// </summary>
        public void BuildProperHand()
        {
            properHand = new Card[NumCards];
            int count = 0;
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 1; j < 14; ++j)
                {
                    if (hand[i].handRow[j] != null)
                    {
                        properHand[count] = hand[i].handRow[j];
                        ++count;
                    }
                }
            }
        }
        /// <summary>
        /// This function returns a boolean indicating whether the hand contains
        /// a valid card or not
        /// </summary>
        /// <returns></returns>
        public bool HasValidCard()
        {
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 1; j < 14; ++j)
                {
                    if (hand[i].handRow[j] != null)
                    {
                        int tmp = hand[i].handRow[j].Rank;
                        if (tmp == hand[i].tableRow.ValidPrePosition || tmp == hand[i].tableRow.ValidSucPosition)
                            return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Plays either the given card (for a human) or the first valid card in the 
        /// computers hand. Card is then removed from hand
        /// </summary>
        /// <param name="index">Index of the card to be played</param>
        /// <returns>Card played</returns>
        public Card PlayValidCard(int index)
        {
            if (this is ComputerPlayer)
            {
                for (int i = 0; i < 4; ++i)
                {
                    for (int j = 1; j < 14; ++j)
                    {
                        if (hand[i].handRow[j] != null)
                        {
                            int tmp = hand[i].handRow[j].Rank;
                            if (tmp == hand[i].tableRow.ValidPrePosition || tmp == hand[i].tableRow.ValidSucPosition)
                            {
                                Card temp = hand[i].handRow[j];
                                hand[i].RemoveCard(j);
                                --this.NumCards;
                                return temp;
                            }
                        }
                    }
                }
                return null; //Shouldn't happen
            }
            else
            {
                Card humanCard = properHand[index];
                hand[(int)humanCard.Suit].RemoveCard(humanCard.Rank);
                --this.NumCards;
                return humanCard;
            }
        }

        /// <summary>
        /// Adds a card to the player's hand
        /// </summary>
        /// <param name="c">Card dealt</param>
        public void Deal(Card c)
        {
            if (this is ComputerPlayer)
            {
                //Debug vs Release Mode
#if DEBUG
            c.FaceUp = true;
#else
                c.FaceUp = false;
#endif
            }
            else
                c.FaceUp = true;
            hand[(int)c.Suit].AddCard(c);
            ++this.NumCards;
        }

        /// <summary>
        /// Clears all cards from hand
        /// </summary>
        public void ReturnCards()
        {
            hand[1].ClearCards();
            hand[0].ClearCards();
            hand[3].ClearCards();
            hand[2].ClearCards();
            NumCards = 0;
        }

        public override string ToString()
        {
            string s = hand[0].ToString() + hand[1].ToString() + hand[2].ToString() + hand[3].ToString();
            return s;
        }

        /// <summary>
        /// String of spaces or asterisks to indicate validity at an index
        /// </summary>
        /// <returns>String to be printed</returns>
        public string PrintValidity() //Also sets up the valid index array
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < NumCards; ++i)
            {
                if (properHand[i] != null)
                {
                    if (properHand[i].Rank == hand[(int)properHand[i].Suit].tableRow.ValidSucPosition ||
                        properHand[i].Rank == hand[(int)properHand[i].Suit].tableRow.ValidPrePosition)
                    {
                        sb.Append(" * ");
                        validIndecies[i] = true;
                    }
                    else
                    {
                        sb.Append("   ");
                        validIndecies[i] = false;
                    }
                }
            }

            return sb.ToString();
        }
        
        public string PrintIndecies()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < NumCards; ++i)
            {
                if (i < 10)
                    sb.Append(" " + i.ToString() + " ");
                else
                    sb.Append(i.ToString() + " ");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Checks that the index is in face a valid index using an array
        /// created parallel to the properHand array
        /// </summary>
        /// <param name="index">Index of card to be tested</param>
        /// <returns>Validity of card</returns>
        public bool IsValidIndex(int index)
        {
            if (index == -1 || index >= NumCards) //Garbage input
                return false;
            else
                return validIndecies[index];
        }
    }

    public class HumanPlayer : Player
    {
        public HumanPlayer(string name, DisplayTable dt)
            : base(name, dt) {        }       
    }

    public class ComputerPlayer : Player
    {
        public ComputerPlayer(string name, DisplayTable dt)
            : base(name, dt) {         }
    }
}
