using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ConsoleDungeon.CharacterAndMobs;
using ConsoleDungeon.MenuAndText;

namespace ConsoleDungeon.Locations
{
    class Tournament
    {
        static string[] mobs = { "Bandit", "Enchanted Bronze Armour", "Ghost", "Ben Dover", "Dinosaur", "Baby Dragon"};
        public static void Play(ref int currentDay, int level, ref int energy, ref int hp, int fullHp, ref int strength, int defense, int dexterity, ref int experience, ref int gold, ref bool tournamentQ, ref int tournamentQProg, int tournamentQRank){
            int i,potion;
            Random randomNUmber = new Random();
            bool empty = false;
            Console.Write("Enter a tournament(y/n) ");
            if(Console.ReadLine() == "y")
            {
                if (energy < 200 || gold < 300)
                {
                    if (energy < 200)
                        Console.WriteLine("You are too tired.");
                    else
                    {
                        Console.WriteLine("You can't afford to");
                        Console.WriteLine("enter a tournament.");
                    }
                }
                else
                {
                    gold -= 300;
                    currentDay++;
                    Program.daysLeft--;
                    for (i = 1; i <= 4; i++)
                    {
                        Mobs mob = new Mobs("TournamentMob", level);
                        Console.WriteLine("Enemy: " + mobs[randomNUmber.Next(0, 5)]);
                        mob.strength += level * 3 * (i - 1);
                        mob.defense += level * (i - 1);
                        mob.fullHp += level * (i - 1) * 2;
                        mob.hp = mob.fullHp;
                        Console.WriteLine("Strength: " + mob.strength + "  Hp: " + mob.fullHp);
                        Console.Write("Fight(y/n): ");
                        if (Console.ReadLine() == "y")
                        {
                            energy -= 50;
                            mob.Fight(ref energy, ref hp, fullHp, ref strength, defense, dexterity, mob.strength, mob.defense, ref mob.hp, mob.fullHp, 0);
                            if (hp <= 0)
                            {
                                hp = 1;
                                Console.WriteLine("You have been disqualified!");
                                i = 5;
                            }
                            else
                            {
                                if (i != 4)
                                {
                                    Console.WriteLine("You advanced to the next round!");
                                    Program.mobKills++;
                                    Console.WriteLine("Hp: " + hp + "/" + fullHp);
                                    Console.WriteLine(" ");
                                    Console.Write("Heal(y/n) ");
                                    while (Console.ReadLine() == "y")
                                    {
                                        CharacterInventory.ShowInventory(32, 34, ref empty);
                                        if (empty)
                                            Console.WriteLine("You don't have healing potions.");
                                        else
                                        {
                                            Console.Write("Choose: ");
                                            potion = Convertion.ToInt(Console.ReadLine(), 2);
                                            if (potion < 32 || potion > 34)
                                                potion = 32;
                                            if (CharacterInventory.items[potion] == 0)
                                                Console.WriteLine("You don't have that item.");
                                            else
                                            {
                                                CharacterInventory.numberOfItems--;
                                                switch (potion)
                                                {
                                                    case 32: { CharacterInventory.items[32]--; hp += 100; if (hp > fullHp) hp = fullHp; } break;
                                                    case 33: { CharacterInventory.items[33]--; hp += 500; if (hp > fullHp) hp = fullHp; } break;
                                                    case 34: { CharacterInventory.items[34]--; hp += 1300; if (hp > fullHp) hp = fullHp; } break;
                                                }
                                                Console.WriteLine("Hp: " + hp + "/" + fullHp);
                                            }
                                            Console.Write("Heal(y/n): ");
                                        }
                                    }
                                    Console.WriteLine(" ");
                                }
                                else
                                {
                                    Console.WriteLine("You won!");
                                    tournamentQProg++;
                                    Program.tournamentWins++;
                                    int gainedGold = 50 * level;
                                    int gainedExp = 40 * level;
                                    int gainedStrength = level * 15;
                                    Console.WriteLine("Rewards: ");
                                    Console.WriteLine("Gold: " + gainedGold);
                                    Console.WriteLine("Exp : " + gainedExp);
                                    Console.WriteLine("Str : " + gainedStrength);
                                    strength += gainedStrength;
                                    experience += gainedExp;
                                    gold += gainedGold;
                                    if(tournamentQ == true && ((tournamentQProg == 1 && (tournamentQRank == 1 || tournamentQRank == 2)) || (tournamentQProg == 2 && tournamentQRank == 3) || (tournamentQProg == 4 && tournamentQRank == 4) || (tournamentQProg == 6 && tournamentQRank == 5)))
                                    {
                                        Console.WriteLine("Quest Completed!");
                                        Console.WriteLine("*Rewards:");
                                        int qGold = 600 * tournamentQRank;
                                        int qExp = 500 * tournamentQRank;
                                        Console.WriteLine("Gold :  " + qGold);
                                        Console.WriteLine("Exp  :  " + qExp);
                                        gold += qGold;
                                        experience += qExp;
                                        tournamentQ = false;
                                        Thread.Sleep(750);
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("You quit the tournament.");
                            Console.WriteLine("No rewards gained.");
                            i = 5;
                        }
                    }
                }
            }
            Thread.Sleep(2000);
        }
    }
}
