using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardLibrary;

namespace FanTan
{
    public class HandRow
    {
        public Card[] handRow { get; private set; }
        public TableRow tableRow { get; private set; }

        public HandRow(TableRow tr)
        {
            handRow = new Card[14];
            tableRow = tr;
        }

        public void AddCard(Card c)
        {
            handRow[c.Rank] = c;
        }

        public void ClearCards()
        {
            handRow = new Card[14];
        }

        public Card RemoveCard(int rank)
        {
            Card c = handRow[rank];
            handRow[rank] = null;
            return c;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < 14; ++i)
            {
                if (handRow[i] != null)
                    sb.Append(handRow[i].ToString()+",");
            }
            return sb.ToString();
        }
    }
}
