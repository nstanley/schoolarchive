using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardLibrary;

namespace FanTan
{
    public class TableRow
    {
        public Card[] row { get; private set; }
        public int ValidPrePosition { get; private set; }
        public int ValidSucPosition { get; private set; }
        public int NumCards { get; private set; }

        public TableRow()
        {
            row = new Card[14];
            ValidPrePosition = 7;
            ValidSucPosition = 7;
            NumCards = 0;
        }

        public void AppendCard(Card c)
        {
            row[c.Rank] = c;
            if (c.Rank < 7)
                --ValidPrePosition;
            else if (c.Rank > 7)
                ++ValidSucPosition;
            else
            {
                --ValidPrePosition;
                ++ValidSucPosition;
            }
        }

        public void ClearCards()
        {
            row = new Card[14];
            ValidPrePosition = 7;
            ValidSucPosition = 7;
            NumCards = 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < 14; ++i)
            {
                if (row[i] == null)
                {
                    sb.Append("__,");
                }
                else
                {
                    sb.Append(row[i].ToString() + ",");
                }
            }

            return sb.ToString();
        }
    }
}
