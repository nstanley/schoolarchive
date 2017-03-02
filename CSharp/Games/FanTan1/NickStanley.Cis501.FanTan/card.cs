using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NickStanley.Cis501.FanTan
{
    public struct card
    {
        public int num;
        public char suit;

        public int value
        {
            get
            {
                int v = 10*(int)suit;
                v += num;
                return v;
            }
        }
        public void print()
        {
            Console.Write(suit);
            switch (num)
            {
                case 1:
                    Console.Write("A");
                    break;
                case 10:
                    Console.Write("T");
                    break;
                case 11:
                    Console.Write("J");
                    break;
                case 12:
                    Console.Write("Q");
                    break;
                case 13:
                    Console.Write("K");
                    break;
                default:
                    Console.Write(num.ToString());
                    break;
            }
        }
    }
}
