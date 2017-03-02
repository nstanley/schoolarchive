using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardLibrary;

namespace FanTan
{
    public class DisplayTable
    {
        public TableRow[] DisplayCards { get; private set; }

        public DisplayTable()
        {
            DisplayCards = new TableRow[4];
            DisplayCards[0] = new TableRow();
            DisplayCards[1] = new TableRow();
            DisplayCards[2] = new TableRow();
            DisplayCards[3] = new TableRow();
        }

        public void PutCardOnTable(Card c)
        {
            c.FaceUp = true;
            DisplayCards[(int)c.Suit].AppendCard(c);
        }

        public void ClearTable()
        {
            DisplayCards[0].ClearCards();
            DisplayCards[1].ClearCards();
            DisplayCards[2].ClearCards();
            DisplayCards[3].ClearCards();
        }

        public override string ToString()
        {
            string s = "\n\t " + DisplayCards[0].ToString() + "\n\t " +
                       DisplayCards[1].ToString() + "\n\t " +
                       DisplayCards[2].ToString() + "\n\t " +
                       DisplayCards[3].ToString() + "\n\t ";
            return s;
        }
    }
}
