using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NickStanley.Cis501.FanTan
{
    class Hand
    {
        card[] hand;
        bool[] valid;
        int index;
        string name;
        public string Name
        {
            get
            {
                return name;
            }
        }

        public card Crd(int a)
        {
            return hand[a];
        }
        public card Play(int c)
        {
            card retrun = hand[c];
            hand[c] = hand[index - 1];
            hand[index-1].num = 0;
            --index;
            return retrun;
        }
        public Hand(string n)
        {
            index = 0;
            hand = new card[13];
            valid = new bool[13];
            name = n;
        }

        public void AddToHand(card c)
        {
            hand[index] = c;
            ++index;
        }

        public void SortHand() //Not working properly :(
        {
            card temp;
            for (int i = 0; i < index; ++i)
            {
                for (int j = 0; j < index; ++j)
                {
                    if (hand[i].value > hand[j].value)
                    {
                        temp = hand[i];
                        hand[i] = hand[j];
                        hand[j] = temp;
                    }
                }
            }
        }

        public int PrintHand(int playerNum, Table t)
        {
            int choice = -1;
            //SortHand();
            Validity(t);
            if (playerNum == 0)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(name + "'s Hand: ");
            Console.Write("Hand:    ");
            for (int j = 0; j < index; ++j)
            {
                hand[j].print();             
                Console.Write(", ");
            }
            if (playerNum == 0)//Only do for human players
            {
                Console.Write("\nIndex:   ");
                for (int j = 0; j < index; ++j)
                {
                    if (j < 10)
                        Console.Write(" " + j.ToString() + "  ");
                    else
                        Console.Write(j.ToString() + "  ");
                }
                Console.Write("\nValid:   ");
                for (int j = 0; j < index; ++j)
                {
                    if (valid[j])
                        Console.Write(" *  ");
                    else
                        Console.Write("    ");
                }
                Console.WriteLine();
                for (int i = 0; i < index; ++i)
                {
                    if (valid[i])
                    {
                        choice = i;
                        break;
                    }
                    else
                        choice = -1;
                }
                if (choice != -1)
                {
                    do
                    {
                        Console.Write("Input Valid Index: ");
                        try
                        {
                            choice = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine(e.Message);
                            Console.WriteLine("Your turn will be auto played");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    } while (!valid[choice]);
                }
            }
            else
            {
                for (int i = 0; i < index; ++i)
                {
                    if (valid[i])
                    {
                        choice = i;
                        break;
                    }
                    else
                        choice = -1;
                }
                Console.Write("\nPress any key to continue . . .");
                Console.ReadLine();
            }
            Console.ForegroundColor = ConsoleColor.White;
            return choice;

        }

        public void Validity(Table t)
        {
            card c;
            for (int i = 0; i < index; ++i)
            {
                c = hand[i];
                switch ((int)c.suit)
                {
                    case 3: //Hearts
                        if (c.num == 1)
                        {
                            if (t.H[c.num].num != 0)
                            {
                                valid[i] = true;
                            }
                            else
                                valid[i] = false;
                        }
                        else if (c.num == 13)
                        {
                            if (t.H[c.num - 2].num != 0)
                            {
                                valid[i] = true;
                            }
                            else
                                valid[i] = false;
                        }
                        else if (t.H[c.num - 2].num != 0 || t.H[c.num].num != 0 || c.num == 7)
                            valid[i] = true;
                        else
                            valid[i] = false;
                        break;
                    case 4: //Diamonds
                        if (c.num == 1)
                        {
                            if (t.D[c.num].num != 0)
                            {
                                valid[i] = true;
                            }
                            else
                                valid[i] = false;
                        }
                        else if (c.num == 13)
                        {
                            if (t.D[c.num - 2].num != 0)
                            {
                                valid[i] = true;
                            }
                            else
                                valid[i] = false;
                        }
                        else if (t.D[c.num - 2].num != 0 || t.D[c.num].num != 0 || c.num == 7)
                            valid[i] = true;
                        else
                            valid[i] = false;
                        break;
                    case 5: //Clubs
                        if (c.num == 1)
                        {
                            if (t.C[c.num].num != 0)
                            {
                                valid[i] = true;
                            }
                            else
                                valid[i] = false;
                        }
                        else if (c.num == 13)
                        {
                            if (t.C[c.num - 2].num != 0)
                            {
                                valid[i] = true;
                            }
                            else
                                valid[i] = false;
                        }
                        else if (t.C[c.num - 2].num != 0 || t.C[c.num].num != 0 || c.num == 7)
                            valid[i] = true;
                        else
                            valid[i] = false;
                        break;
                    default: //Spades
                        if (c.num == 1)
                        {
                            if (t.S[c.num].num != 0)
                            {
                                valid[i] = true;
                            }
                            else
                                valid[i] = false;
                        }
                        else if (c.num == 13)
                        {
                            if (t.S[c.num - 2].num != 0)
                            {
                                valid[i] = true;
                            }
                            else
                                valid[i] = false;
                        }
                        else if (t.S[c.num - 2].num != 0 || t.S[c.num].num != 0 || c.num == 7)
                            valid[i] = true;
                        else
                            valid[i] = false;
                        break;

                }
            }
        }

        public bool HandEmpty()
        {
            for (int i = 0; i < index; ++i)
            {
                if (hand[i].num != 0)
                    return false;
            }
            //if gets throuh loop, all cards are played
            return true;
        }
    }
}




    