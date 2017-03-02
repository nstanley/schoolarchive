using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NickStanley.Cis501.FanTan
{
    class Table
    {
        card[] Hearts = new card[13];
        card[] Diamonds = new card[13];
        card[] Clubs = new card[13];
        card[] Spades = new card[13];
        public card[] H
        {
            get
            {
                return Hearts;
            }
        }
        public card[] D
        {
            get
            {
                return Diamonds;
            }
        }
        public card[] C
        {
            get
            {
                return Clubs;
            }
        }
        public card[] S
        {
            get
            {
                return Spades;
            }
        }
        public void PlayCard(card c)
        {
            switch ((int)c.suit)
            {
                case 3:
                    Hearts[c.num-1] = c;
                    break;
                case 4:
                    Diamonds[c.num-1] = c;
                    break;
                case 5:
                    Clubs[c.num-1] = c;
                    break;
                default:
                    Spades[c.num-1] = c;
                    break;
            }
            ShowTable();
        }

        public void ShowTable()
        {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write("\n\n         HRT:    ");
            for (int i = 0; i < 13; ++i)
            {
                if (Hearts[i].num == 0)
                    Console.Write("__");
                else
                    Hearts[i].print();
                Console.Write(", ");
            }
            Console.Write("\n         DMD:    ");
            for (int i = 0; i < 13; ++i)
            {
                if (Diamonds[i].num == 0)
                    Console.Write("__");
                else
                    Diamonds[i].print();
                Console.Write(", ");
            }
            Console.Write("\n         CLB:    ");
            for (int i = 0; i < 13; ++i)
            {
                if (Clubs[i].num == 0)
                    Console.Write("__");
                else
                    Clubs[i].print();
                Console.Write(", ");
            }
            Console.Write("\n         SPD:    ");
            for (int i = 0; i < 13; ++i)
            {
                if (Spades[i].num == 0)
                    Console.Write("__");
                else
                    Spades[i].print();
                Console.Write(", ");
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine("\n");            
        }

    }
}
