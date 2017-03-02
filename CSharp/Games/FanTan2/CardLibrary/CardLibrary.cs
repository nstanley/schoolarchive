using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardLibrary
{
    public enum SuitEnum { H, D, C, S };

    public class Card
    {
        public SuitEnum Suit { get; private set; }
        public int Rank { get; private set; }
        public bool FaceUp;
        public Card(int s, int r)
        {
            Suit = (SuitEnum)s;
            Rank = r;
            //set faceup/down
            FaceUp = true;
        }
        public override string ToString()
        {
            string s;
            if (FaceUp)
            {
                switch (Rank)
                {
                    case 1:
                        s = Suit.ToString() + "A";
                        break;
                    case 10:
                        s = Suit.ToString() + "T";
                        break;
                    case 11:
                        s = Suit.ToString() + "J";
                        break;
                    case 12:
                        s = Suit.ToString() + "Q";
                        break;
                    case 13:
                        s = Suit.ToString() + "K";
                        break;
                    default:
                        s = Suit.ToString() + Rank.ToString();
                        break;
                }
            }
            else
                s = "XX";
            return s;
        }
    }

    public class CardDeck
    {
        Card[] deck = new Card[52];
        int TopIndex;

        public CardDeck()
        {
            TopIndex = 51;
            int k = 0;
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 1; j < 14; ++j)
                {
                    deck[k] = new Card(i, j);
                    ++k;
                }
            }
        }

        public Card CardDraw()
        {
            TopIndex--;
            return deck[TopIndex + 1];
        }

        public void Shuffle()
        {
            //Knuth's Algortithm
            Random rand = new Random();
            for (int i = 51; i >= 0; i--)
            {
                int x = rand.Next(0, i);
                Card temp = deck[i];
                deck[i] = deck[x];
                deck[x] = temp;
            }
        }

        public void ReturnAllCards()
        {
            TopIndex = 51;
        }
    }


}
