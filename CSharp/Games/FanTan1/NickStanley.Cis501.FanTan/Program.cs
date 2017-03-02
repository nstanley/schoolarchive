using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NickStanley.Cis501.FanTan
{   
    class Program
    {             
        static void Main(string[] args)
        {
            //Formatting
            Console.Title = "Fan Tan";
            Console.ForegroundColor = ConsoleColor.White;
            //Variables
            String name;
            bool GameOn = true;
            Table tabel = new Table();
            int chosenCard = -1;

            #region Build Deck
            card[] deck = new card[52];
            char tmp = (char)3;
            int k = 0;
            for (int i = 0; i < 4; ++i)
            {
                for (int j = 1; j < 14; ++j)
                {
                    deck[k].num = j;
                    deck[k].suit = tmp;
                    ++k;
                }
                ++tmp;
            }
            #endregion

            int numPlayers = 0;
            int masterCount = 0;

            #region Set up
            Console.Write("Enter your name: ");
            name = Console.ReadLine();
            while (numPlayers < 3 || numPlayers > 5)
            {
                Console.Write("Input number of Computer Players (3-5): ");
                try
                {
                    numPlayers = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Number of computer players will equal 3");
                    Console.ForegroundColor = ConsoleColor.White;
                    numPlayers = 3;
                }
            }
            ++numPlayers; //add human
            #endregion

            Hand[] player = new Hand[numPlayers];
            for (int i = 0; i < numPlayers; ++i)
            {
                if (i == 0)
                    player[i] = new Hand(name);
                else
                    player[i] = new Hand("Computer_" + (i - 1).ToString());
            }
                  
            #region Deal Cards
            for (int i = 0; i < 52; ++i)
            {
                Random r = new Random();
                int tmp1 = r.Next(0, 52);
                while (deck[tmp1].num == 0)
                    tmp1 = r.Next(0, 52);
                card c = deck[tmp1];
                deck[tmp1].num = 0;
                player[masterCount].AddToHand(c);
                if (masterCount == numPlayers-1)
                    masterCount = 0;
                else
                    ++masterCount;
            }
            #endregion

            masterCount = 0;
            //Start Game
            while (GameOn)
            {
                chosenCard = player[masterCount].PrintHand(masterCount, tabel);
                if (chosenCard == -1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("No available cards to play");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Press any key to continue . . .");
                    Console.ReadLine();
                    
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(player[masterCount].Name + " picked ");
                    player[masterCount].Crd(chosenCard).print();
                    Console.ForegroundColor = ConsoleColor.White;
                    tabel.PlayCard(player[masterCount].Play(chosenCard));
                }
                if (player[masterCount].HandEmpty())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(player[masterCount].Name + " Wins!");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Would you like to play again [Y/N]?");
                    string response = Console.ReadLine();
                    if (response == "Y" || response == "y")
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        player = new Hand[numPlayers];
                        tabel = new Table();
                        for (int i = 0; i < numPlayers; ++i)
                        {
                            if (i == 0)
                                player[i] = new Hand(name);
                            else
                                player[i] = new Hand("Computer_" + (i - 1).ToString());
                        }

                        #region Build Deck Again
                        deck = new card[52];
                        tmp = (char)3;
                        k = 0;
                        for (int i = 0; i < 4; ++i)
                        {
                            for (int j = 1; j < 14; ++j)
                            {
                                deck[k].num = j;
                                deck[k].suit = tmp;
                                ++k;
                            }
                            ++tmp;
                        }
                        #endregion

                        #region Deal Cards Again
                        for (int i = 0; i < 52; ++i)
                        {
                            Random r = new Random();
                            int tmp1 = r.Next(0, 52);
                            while (deck[tmp1].num == 0)
                                tmp1 = r.Next(0, 52);
                            card c = deck[tmp1];
                            deck[tmp1].num = 0;
                            player[masterCount].AddToHand(c);
                            if (masterCount == numPlayers - 1)
                                masterCount = 0;
                            else
                                ++masterCount;
                        }
                        #endregion

                        masterCount = 0;
                    }
                    else
                        break;
                }
                if (masterCount == numPlayers-1)
                    masterCount = 0;
                else
                    ++masterCount;
            }
            Console.WriteLine("Thanks For Playing!\nPress any key to continue . . .");
            Console.ReadLine();
        }
    }
}
