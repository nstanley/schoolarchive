using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CardLibrary;
namespace FanTan
{
    class Program
    {
        static List<Player> players;
        static List<Player> computerPlayers;
        static CardDeck Deck;
        static DisplayTable Table;
        static Random r;
        static HumanPlayer player;
        static int numComps;

        static void Main(string[] args)
        {            
            r = new Random();
            Deck = new CardDeck();
            Table = new DisplayTable();
            computerPlayers = new List<Player>();
            players = new List<Player>();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter your name: ");
            player = new HumanPlayer(Console.ReadLine(),Table);
            do
            {
                Console.Write("Enter number of Computer players (3-5): ");
                try
                {
                    numComps = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    numComps = 0;
                }
            } while (numComps < 3 || numComps > 5);
            //Create numComps computer players
            for (int i = 0; i < numComps; ++i)
            {
                ComputerPlayer cp = new ComputerPlayer("Computer_" + i, Table);
                computerPlayers.Add(cp);
            }
            Go();
            Console.WriteLine("\nThanks for playing!");
            Console.WriteLine("Press any key to continue . . . ");
            Console.ReadLine();
        }
        static void Go()
        {
            while (1 != 2)
            {
                /*/This is not working how I want it to right now
                Console.WriteLine("START RANDO");
                while (players.Count != numComps+1)
                {
                    int rando = r.Next(1,numComps+1);
                    Player p;
                    switch (rando)
                    {
                        case 1:
                            p = player;
                            break;
                        case 2:
                            p = computerPlayers[0];
                            break;
                        case 3:
                            p = computerPlayers[1];
                            break;
                        case 4:
                            p = computerPlayers[2];
                            break;
                        case 5:
                            p = computerPlayers[3];
                            break;
                        case 6:
                            p = computerPlayers[4];
                            break;
                        default:
                            p = null;
                            break;
                    }
                    //if player is not in list, add
                    if (!players.Contains(p))
                        players.Add(p);
                }
                Console.WriteLine("OUT OF RANDO");*/
                players.Add(player);
                for (int i = 0; i < numComps; ++i)
                {
                    players.Add(computerPlayers[i]);
                }
                PlayOneGame();
                Deck.ReturnAllCards();
                player.ReturnCards();
                for (int i = 0; i < numComps; ++i)
                {
                    computerPlayers[i].ReturnCards();
                }
                Table.ClearTable();
                string yn = "";
                do
                {
                    Console.Write("Play again [y/n]? ");
                    yn = Console.ReadLine();
                    if (yn != "")
                    {
                        if (yn[0] == 'N' || yn[0] == 'n')
                            return;
                    }
                } while (yn == "");
            }
        }
        static void PlayOneGame()
        {
            int turn = 0;
            int count = 0;
            Deck.Shuffle();
            Console.WriteLine();
            //Deal cards
            for (int i = 0; i < 52; ++i)
            {
                players[count].Deal(Deck.CardDraw());
                ++count;
                if (count == numComps + 1) //Rollover
                    count = 0;
            }
            while (true)
            {
                int indy = 0;
                Console.WriteLine(players[turn].Name + "'s Hand: ");
                Console.WriteLine("Hand:    " + players[turn].ToString());
                if (!players[turn].HasValidCard())
                {
                    //if player has no valid card
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(players[turn].Name + " has no valid move, and will pass \n");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Press any key to continue . . . ");
                    Console.ReadLine();
                    ++turn;
                }
                else if (players[turn] is HumanPlayer)
                {
                    players[turn].BuildProperHand();
                    Console.WriteLine("Index:   " + players[turn].PrintIndecies());
                    Console.WriteLine("Valid:   " + players[turn].PrintValidity());
                    Console.ForegroundColor = ConsoleColor.Green;                    
                    do 
                    {
                        
                        Console.Write("Input index of card to play: ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        try
                        {
                            indy = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            indy = -1;
                            Console.WriteLine(e.Message);
                        }

                    }while (!players[turn].IsValidIndex(indy));
                    Console.ForegroundColor = ConsoleColor.White;
                    Table.PutCardOnTable(players[turn].PlayValidCard(indy));
                    Console.WriteLine(Table.ToString());
                    ++turn;
                }
                else //Computer player with valid card
                {
                    Card cardPlayed = players[turn].PlayValidCard(0);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(players[turn].Name + " played " + cardPlayed.ToString());
                    Console.ForegroundColor = ConsoleColor.White;
                    Table.PutCardOnTable(cardPlayed);
                    Console.WriteLine(Table.ToString());
                    Console.WriteLine("Press any key to continue . . . ");
                    Console.ReadLine();
                    ++turn;
                }
                if (players[turn - 1].NumCards == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(players[turn - 1].Name + " Wins!");
                    Console.ForegroundColor = ConsoleColor.White; 
                    break;
                }
                if (turn == numComps + 1) //Turns go out of bounds, reset
                    turn = 0;
            }
            
        }
    }
}
