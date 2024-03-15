using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using ConsoleDungeon.CharacterAndMobs;
using ConsoleDungeon.MenuAndText;

namespace ConsoleDungeon.Locations
{
    class Dungeon
    {
        public static int dungeonFloor = 1;
        static double averagePower;
        public static void Exploration(ref int energy, ref int currentDay, ref int dungeonFloor, ref int hp, int fullHp, ref int exp, ref int strength, int defense, int dexterity, ref int gold, ref int reputation, ref int lives, ref bool win, ref int mainQRank, ref int mainQProg){
            if (energy < 100)
                Console.WriteLine("You are too tired to explore the dungeon.");
            else
            {
                averagePower = FloorAverageStats(dungeonFloor);
                Menu.DungeonMenuPrint(dungeonFloor, averagePower);
                Console.Write("Choose: ");
                int choice = Convertion.ToInt(Console.ReadLine(), 2);
                if (choice == 1)
                {
                    energy -= 100;
                    currentDay++;
                    Program.daysLeft--;
                    Mobs mob = new Mobs("DungeonMob", dungeonFloor);
                    Text.DungeonBattleText(mob.raceNumber, 1);
                    Console.WriteLine("*===========*");
                    Console.WriteLine("| [1] Fight |");
                    Console.WriteLine("| [2] Run   |");
                    Console.WriteLine("*===========*\n");
                    Console.Write("Choose : ");
                    int option = Convertion.ToInt(Console.ReadLine(), 1);
                    if (option != 2)
                    {
                        mob.Fight(ref energy, ref hp, fullHp, ref strength, defense, dexterity, mob.strength, mob.defense, ref mob.hp, mob.fullHp, mob.raceNumber);
                        DungeonRewards(ref hp, fullHp, ref lives, ref gold, ref exp, ref reputation, ref strength, mob.raceNumber, mob.dropRate, ref dungeonFloor, ref win, ref mainQRank, ref mainQProg);
                    }
                    else
                    {
                        bool escape = Escape(dungeonFloor);
                        if (escape == true)
                            Console.WriteLine("You managed to get out unharmed.");
                        else
                        {
                            Console.WriteLine("You can't escape.");
                            Thread.Sleep(500);
                            mob.Fight(ref energy, ref hp, fullHp, ref strength, defense, dexterity, mob.strength, mob.defense, ref mob.hp, mob.fullHp, mob.raceNumber);
                            DungeonRewards(ref hp, fullHp, ref lives, ref gold, ref exp, ref reputation, ref strength, mob.raceNumber, mob.dropRate, ref dungeonFloor, ref win, ref mainQRank, ref mainQProg);
                        }
                    }
                }
                Thread.Sleep(2000);
            }
        }
        public static void DungeonRewards(ref int hp, int fullHp, ref int lives, ref int gold, ref int exp, ref int reputation, ref int strength, int raceNumber, int dropRate, ref int dungeonFloor, ref bool win, ref int mainQRank, ref int mainQProg)
        {
            if (hp <= 0)
            {
                Console.WriteLine("You have been killed.");
                lives--;
                Console.WriteLine("Lives left:  " + lives);
                hp = 10 * fullHp / 100;
            }
            else
            {
                Console.WriteLine("*Rewards:");
                mainQProg++;
                Random randomNumber = new Random();
                int gainedExp = dungeonFloor * 20 + randomNumber.Next(1,5) * dungeonFloor;
                int gainedGold = dungeonFloor * 20 + randomNumber.Next(1,5) * dungeonFloor;
                int gainedReputation = 0;
                int gainedStrength = dungeonFloor * 5 + randomNumber.Next(0, 3) * dungeonFloor;
                if (raceNumber == 5)
                {
                    Program.bossKills++;
                    gainedExp += dungeonFloor * 15;
                    gainedGold = 250 * dungeonFloor / 20;
                    gainedReputation = 100 * dungeonFloor / 20;
                    gainedStrength = 150 * dungeonFloor / 20;
                    mainQRank++;
                }
                else Program.mobKills++;
                Console.WriteLine("Exp Points :  " + gainedExp);
                Console.WriteLine("Str Points :  " + gainedStrength);
                Console.WriteLine("Gained Gold:  " + gainedGold);
                if(raceNumber == 5)
                {
                    Console.WriteLine("Quest Completed!\n");
                    Console.WriteLine("*Extra rewards:");
                    Console.WriteLine("Rep Points :  " + gainedReputation);
                }
                exp += gainedExp;
                gold += gainedGold;
                reputation += gainedReputation;
                strength += gainedStrength;
                int drop = randomNumber.Next(1, 100);
                if(drop <= dropRate)
                {
                    int random=1,item=0,item2=0;
                    switch((Mobs.MobSpecies)raceNumber)
                    {
                        case Mobs.MobSpecies.Skeleton: { Console.WriteLine("You found a bone."); item = 1; }break;
                        case Mobs.MobSpecies.Draugr: {
                                random = randomNumber.Next(1, 2);
                                switch(random)
                                {
                                    case 1: { Console.WriteLine("You found a Rusy Sword."); item = 4; } break;
                                    case 2: { Console.WriteLine("You found a Sharp Sword."); item = 5; } break;
                                }
                            } break;
                        case Mobs.MobSpecies.Goul: { Console.WriteLine("You found a Poisoned Dagger."); item = 6; } break;
                        case Mobs.MobSpecies.ReanimatedDragon: {
                                random = randomNumber.Next(1, 6);
                                switch(random)
                                {
                                    case 1: { Console.WriteLine("You found a Crossbow."); item = 7; } break;
                                    case 2: { Console.WriteLine("You found a pair of Cobalt Boots."); item = 26; } break;
                                    case 3: { Console.WriteLine("You found a Cobalt Chestplate."); item = 18; } break;
                                    case 4: { Console.WriteLine("You found a pair of Cobalt Gloves."); item = 30; } break;
                                    case 5: { Console.WriteLine("You found a Cobalt Helmet."); item = 14; } break;
                                    case 6: { Console.WriteLine("You found a pair of Cobalt Leggins."); item = 22; } break;
                                }
                            } break;
                        case Mobs.MobSpecies.Warlock: { 
                                switch(dungeonFloor)
                                {
                                    case 20: { Console.WriteLine("You found an Iron Chestplate in the chest."); item = 16; } break;
                                    case 40: { Console.WriteLine("You found 1 Poisoned Dagger and 1 Platinum Helmet in the chest."); item = 6; item2 = 13; } break;
                                    case 60: { lives++; 
                                            Console.WriteLine("You found a bottled soul in the chest.");
                                            Console.WriteLine("Your number of lives increased by 1.");
                                        } break;
                                    case 80: { Console.WriteLine("You War Axe and a Meteorite Helmet in the chest."); item = 9; item2 = 15; } break;
                                    case 100: { win = true; } break;
                                }
                            } break;
                    }
                    CharacterInventory.PickItem(ref CharacterInventory.numberOfItems, CharacterInventory.capacity, dungeonFloor, item, item2);
                }
                Thread.Sleep(1000);
                dungeonFloor++;
            }
        }
        static double FloorAverageStats(int dungeonFloor){
            double averagePower = 0;
            for(int i = 1; i <= 100; i++)
            {
                Mobs mob = new Mobs("DungeonMob", dungeonFloor);
                averagePower += mob.strength;
            }
            averagePower /= 100;
            return averagePower;
        }
        static bool Escape(int dungeonFloor){
            Random randomNumber = new Random();
            int chance = randomNumber.Next(1,100);
            if ((chance <= 60 && dungeonFloor < 20) || (chance <= 40 && dungeonFloor < 40) || (chance <= 20 && dungeonFloor < 60) || (chance <= 10 && dungeonFloor < 80) || chance <= 2)
                return true;
            else return false;
        }
    }
}
