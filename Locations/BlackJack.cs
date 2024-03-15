using System;
using System.Collections.Generic;
using System.Text;
using ConsoleDungeon.MenuAndText;
using System.Threading;

namespace ConsoleDungeon.Locations
{
    class BlackJack
    {
        public static int dailyWin = 0, lastDay = 0;
        static int[] hand = new int[15];
        static int[] dealer = new int[15];
        public static void Play(ref int gold, ref bool gamblingQ, ref int gamblingQProg, int gamblingQRank, ref int exp)
        {
            Random randomNumber = new Random();
            int choice = 0, total1, total2;
            int i, j, ist, jst, ok1, ok2;
            Console.WriteLine("Place a bet(y/n) ");
            if (Console.ReadLine() == "y")
            {
                if (gold < 100 || dailyWin >= 50000)
                {
                    if (gold < 100)
                        Console.WriteLine("You don't have enough money.");
                    else
                        Console.WriteLine("You are not allowed to play until tomorrow.");
                }
                else
                {
                    Console.Write("Place a bet: ");
                    choice = Convertion.ToInt(Console.ReadLine(), 1);
                    if (choice < 100 || choice > 25000)
                        Console.WriteLine("Unacceptable wager.");
                    else
                    {
                        ok1 = ok2 = 0;
                        hand[1] = randomNumber.Next(1, 10);
                        dealer[1] = randomNumber.Next(1, 10);
                        hand[2] = randomNumber.Next(1, 10);
                        dealer[2] = randomNumber.Next(1, 10);
                        total1 = hand[1] + hand[2];
                        total2 = dealer[1] + dealer[2];
                        ist = jst = 2;
                        Console.WriteLine("Your hand:\n");
                        for (i = 1; i <= ist; i++)
                            Console.Write(hand[i] + " ");
                        Console.WriteLine("\nYour total: " + total1);
                        Console.WriteLine("Dealer's hand:\n");
                        for (j = 1; j <= jst; j++)
                        {
                            if (j == 1)
                                Console.Write("? ");
                            else
                                Console.Write(dealer[j] + " ");
                        }
                        Console.WriteLine("\nDealer's total: " + (total2 - dealer[1]));
                        Console.WriteLine("Your turn:");
                        while (ok1 == 0)
                        {
                            Console.Write("Hit(y/n)");
                            if (Console.ReadLine() != "y")
                                ok1 = 1;
                            else
                            {
                                ist++;
                                hand[ist] = randomNumber.Next(1, 10);
                                total1 += hand[ist];
                                Console.WriteLine("Your hand:\n");
                                for (i = 1; i <= ist; i++)
                                    Console.Write(hand[i] + " ");
                                Console.WriteLine("\nYour total: " + total1);
                                if (total1 > 21)
                                {
                                    Console.WriteLine("BUST!\n");
                                    Thread.Sleep(750);
                                    ok1 = ok2 = 1;
                                }
                                else if (total1 == 21)
                                    ok1 = 1;
                            }
                        }
                        Console.WriteLine("Dealer's turn:");
                        while (ok2 == 0)
                        {
                            if (total2 <= total1 - hand[1] || total2 < 17)
                            {
                                jst++;
                                dealer[jst] = randomNumber.Next(1, 10);
                                total2 += dealer[jst];
                                Console.WriteLine("Dealer's hand:\n");
                                for (j = 1; j <= jst; j++)
                                {
                                    if (j == 1)
                                        Console.Write("? ");
                                    else
                                        Console.Write(dealer[j] + " ");
                                }
                                Console.WriteLine("\nDealer's total: " + (total2 - dealer[1]));
                                Thread.Sleep(1500);
                                if (total2 > 21)
                                {
                                    Console.WriteLine("BUST!\n");
                                    Thread.Sleep(750);
                                    ok2 = 1;
                                }
                                else if (total2 == 21)
                                    ok2 = 1;
                            }
                            else
                                ok2 = 1;
                        }
                        Console.WriteLine("Your hand:\n");
                        for (i = 1; i <= ist; i++)
                            Console.Write(hand[i] + " ");
                        Console.WriteLine("\nTotal: " + total1 + "\n");
                        Console.WriteLine("Dealer's hand:\n");
                        for (j = 1; j <= jst; j++)
                            Console.Write(dealer[j] + " ");
                        Console.WriteLine("\nDealer's total: " + total2);
                        Thread.Sleep(1000);
                        if ((total1 == 21 && total2 != 21) || (total2 > 21) || (total1 < 21 && total1 > total2))
                        {
                            Console.WriteLine("You win!\n");
                            gamblingQProg += choice;
                            gold += choice;
                            dailyWin += choice;
                            if (gamblingQ == true && ((gamblingQProg == 300 && gamblingQRank == 1) || (gamblingQProg == 700 && gamblingQRank == 2) || (gamblingQProg == 1000 && gamblingQRank == 3) || (gamblingQRank == 2500 && gamblingQRank == 4) || (gamblingQRank == 6000 && gamblingQRank == 5)))
                            {
                                Console.WriteLine("Quest Completed!");
                                Console.WriteLine("*Rewards:");
                                int qGold = 200 * gamblingQRank;
                                int qExp = 300 * gamblingQRank;
                                Console.WriteLine("Gold :  " + qGold);
                                Console.WriteLine("Exp  :  " + qExp);
                                gold += qGold;
                                exp += qExp;
                                gamblingQ = false;
                                Thread.Sleep(750);
                            }
                        }
                        if ((total2 == 21 && total1 != 21) || (total1 > 21) || (total2 < 21 && total2 > total1))
                        {
                            Console.WriteLine("You lose!\n");
                            gold -= choice;
                            dailyWin -= choice;
                        }
                        if (total1 == total2)
                            Console.WriteLine("Tie!\n");
                        Console.WriteLine("Session earnings: " + BlackJack.dailyWin + "\n");
                        Thread.Sleep(1500); 
                    }
                    Play(ref gold, ref gamblingQ, ref gamblingQProg, gamblingQRank, ref exp);
                }
            }
        }
    }
}
