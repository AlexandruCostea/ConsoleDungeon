using ConsoleDungeon.MenuAndText;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleDungeon
{
    class Level
    {
        static int strPoints=0, defPoints=0, dexPoints=0, intPoints=0, repPoints=0;
        public static void LevelUp(ref int expPoints, ref int levelUp, ref int lvlUpPoints, ref int level, ref int fullHp, ref int hp, ref string healthBar){
            Console.WriteLine("Level Up!");
            while (expPoints >= levelUp && level<100)
            {
                lvlUpPoints += 10;
                level++;
                expPoints -= levelUp;
                levelUp += 25;
                fullHp += 25;
                hp = fullHp;
            }
            HealthBars.SetHealthBar(ref healthBar, hp, fullHp);
        }
        public static void PointAssignment( ref int strength, ref int defense, ref int dexterity, ref int intelligence, ref int reputation, ref int points){
            Console.WriteLine("Availible points:  " + points);
            Console.WriteLine("[1] Strength:      " + strPoints);
            Console.WriteLine("[2] Defense:       " + defPoints);
            Console.WriteLine("[3] Dexterity:     " + dexPoints);
            Console.WriteLine("[4] Intelligence:  " + intPoints);
            Console.WriteLine("[5] Reputation:    " + repPoints);
            Console.Write("Spend points(y/n)  ");
            if (Console.ReadLine() == "y")
            {
                Console.Write("Choose: ");
                int x = Convertion.ToInt(Console.ReadLine(), 2);
                if (x >= 1 && x <= 5)
                {
                    Console.Write("Enter the amount of points: ");
                    int y = Convertion.ToInt(Console.ReadLine(), 2);
                    if (y > points)
                    {
                        Console.WriteLine("Insufficient points!");
                        Thread.Sleep(500);
                    }
                    else
                    {
                        points -= y;
                        switch (x)
                        {
                            case 1:
                                {
                                    strength += y;
                                    strPoints += y;
                                }
                                break;
                            case 2:
                                {
                                    defense += y;
                                    defPoints += y;
                                }
                                break;
                            case 3:
                                {
                                    dexterity += y;
                                    dexPoints += y;
                                }
                                break;
                            case 4:
                                {
                                    intelligence += y;
                                    intPoints += y;
                                }
                                break;
                            case 5:
                                {
                                    reputation += y;
                                    repPoints += y;
                                }
                                break;
                        }
                        Console.WriteLine("Success!\n");
                    }
                }
                Thread.Sleep(750);
                PointAssignment(ref strength, ref defense, ref dexterity, ref intelligence, ref reputation, ref points);
            }
        }
    }
}
