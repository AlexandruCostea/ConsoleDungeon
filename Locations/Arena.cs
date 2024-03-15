using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ConsoleDungeon.CharacterAndMobs;

namespace ConsoleDungeon.Locations
{
    class Arena
    {
        public static void ArenaFight(ref int currentDay, int level, ref int energy, ref int hp, int fullHp, ref int strength, int defense, int dexterity, ref int exp, ref int gold, ref bool arenaQ, ref int arenaQProg, int arenaQRank){
            if (energy < 60 || gold < 20 * level)
            {
                if (energy < 60)
                    Console.WriteLine("You are too tired to fight.");
                else
                    Console.WriteLine("You don't have enough gold.");
            }
            else
            {
                gold -= 20 * level;
                Random randomNumber = new Random();
                Mobs enemy = new Mobs("ArenaMob", level);
                int x = randomNumber.Next(1, 3);
                Console.Write("Opponent: ");
                switch (x)
                {
                    case 1:
                        {
                            if (level <= 10)
                                Console.WriteLine("Bat");
                            else if (level > 10 && level <= 30)
                                Console.WriteLine("Golem");
                            else if (level > 30 && level <= 60)
                                Console.WriteLine("Zombie");
                            else if (level > 60 && level <= 80)
                                Console.WriteLine("Water Elemental");
                            else if (level > 80)
                                Console.WriteLine("Drake");
                        }
                        break;
                    case 2:
                        {
                            if (level <= 10)
                                Console.WriteLine("Wolf");
                            else if (level > 10 && level <= 30)
                                Console.WriteLine("Ghost");
                            else if (level > 30 && level <= 60)
                                Console.WriteLine("Murlock");
                            else if (level > 60 && level <= 80)
                                Console.WriteLine("Earth Elemental");
                            else if (level > 80)
                                Console.WriteLine("Warewolf");
                        }
                        break;
                    case 3:
                        {
                            if (level <= 10)
                                Console.WriteLine("Snake");
                            else if (level > 10 && level <= 30)
                                Console.WriteLine("Genie");
                            else if (level > 30 && level <= 60)
                                Console.WriteLine("Hellhound");
                            else if (level > 60 && level <= 80)
                                Console.WriteLine("Fire Elemental");
                            else if (level > 80)
                                Console.WriteLine("Archmage");
                        }
                        break;
                }
                Console.WriteLine("[1]Fight  [2]Quit");
                int choice = Convertion.ToInt(Console.ReadLine(), 2);
                if (choice == 1)
                {
                    energy -= 60;
                    currentDay++;
                    Program.daysLeft--;
                    enemy.Fight(ref energy, ref hp, fullHp, ref strength, defense, dexterity, enemy.strength, enemy.defense, ref enemy.hp, enemy.fullHp, 0);
                    if (hp <= 0)
                    {
                        hp = 1;
                        Console.WriteLine("You lost.");
                        Console.WriteLine("Rewards: ");
                        Console.WriteLine("10 experience points.");
                        exp += 10;
                    }
                    else
                    {
                        Console.WriteLine("You won!");
                        Console.WriteLine("Rewards: ");
                        arenaQProg++;
                        int gainedStrength = level * 5;
                        int gainedExp = level * 30;
                        Program.arenaKills++;
                        Program.mobKills++;
                        Console.WriteLine(gainedStrength + " Strength points");
                        Console.WriteLine(gainedExp + " Experience points");
                        strength += gainedStrength;
                        exp += gainedExp;
                        if(arenaQ == true && arenaQProg == arenaQRank * 3)
                        {
                            Console.WriteLine("Quest Completed!");
                            Console.WriteLine("*Rewards:");
                            int qGold = 200 * arenaQRank;
                            int qExp = 250 * arenaQRank;
                            Console.WriteLine("Gold :  " + qGold);
                            Console.WriteLine("Exp  :  " + qExp);
                            gold += qGold;
                            exp += qExp;
                            arenaQ = false;
                            Thread.Sleep(750);
                        }
                    }
                }
            }
            Thread.Sleep(1500);

        }
    }
}
