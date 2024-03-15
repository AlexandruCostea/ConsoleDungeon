using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using ConsoleDungeon.MenuAndText;
using ConsoleDungeon.CharacterAndMobs;

namespace ConsoleDungeon.Locations
{
    class Job
    {
        public static int promotion = 0;
        public static void Work(ref int currentDay, ref int currentGold, ref int expPoints, ref int energy, ref int jobRank, ref Character.Jobs job, ref int efficiency, ref int experience, ref int shifts, ref string jobName, int level, ref int hp, int fullHp, ref int strength, int defense, int dexterity, ref int lives, ref int reputation, ref bool fishingQuest, ref int fishingQProgress, int fishingQRank){
            Console.WriteLine("*==============*");
            Console.WriteLine("|| [1] Work   ||");
            Console.WriteLine("|| [2] Resign ||");
            Console.WriteLine("*==============*\n");
            Console.Write("Choose: ");
            int choice = Convertion.ToInt(Console.ReadLine(),2);
            if (choice == 0)
                Console.WriteLine("Nvm, you are too lazy :))");
            if (choice != 2 && choice!=0)
            {
                if (energy < 20 * (int)job + (jobRank - 1) * 10)
                    Console.WriteLine("You are too tired to work today. Go get some sleep");
                else
                {
                    currentDay++;
                    Program.daysLeft--;
                    Program.jobShifts++;
                    shifts++;
                    Text.JobText(job, 1, 0, jobRank);
                    Thread.Sleep(1000);
                    Random randomNumber = new Random();
                    if (job == Character.Jobs.Guard)
                    {
                        int enemyEncounterChance = randomNumber.Next(1, 10);
                        if (enemyEncounterChance < 7)
                        {
                            Text.JobText(job, 2, 0, jobRank);
                            int gainedGold = (randomNumber.Next(1, 10) + efficiency * 4) * 9;
                            expPoints += 20 * jobRank;
                            currentGold += gainedGold;
                            reputation += 10 * jobRank;
                            Console.WriteLine("You gained " + gainedGold + " gold, " + 20 * jobRank + " experience points and " + 10 * jobRank + " reputation points");
                        }
                        else
                        {
                            Text.JobText(job, 3, 0, jobRank);
                            Mobs assassin = new Mobs("Assassin", level);
                            assassin.Fight(ref energy, ref hp, fullHp, ref strength, defense, dexterity, assassin.strength, assassin.defense, ref assassin.hp, assassin.fullHp, assassin.raceNumber);
                            if(hp <= 0)
                            {
                                Console.WriteLine("You were killed before the other guards arrived.");
                                lives--;
                                Console.WriteLine("Lives left: " + lives);
                                hp = 10 * fullHp / 100;
                            }
                            else
                            {
                                Console.WriteLine("You did your job well.");
                                Console.WriteLine("Rewards: ");
                                int gainedGold = (randomNumber.Next(1, 10) + efficiency * 4) * 9 + 100 * randomNumber.Next(1, 2);
                                int gainedExp = randomNumber.Next(10, 16) * level;
                                int gainedStrength = level * 2;
                                int gainedReputation = level * jobRank * 5;
                                Console.WriteLine("Gold: " + gainedGold);
                                Console.WriteLine("Exp : " + gainedExp);
                                Console.WriteLine("Str : " + gainedStrength);
                                Console.WriteLine("Rep : " + gainedReputation);
                                currentGold += gainedGold;
                                expPoints += gainedExp;
                                strength += gainedStrength;
                                reputation += gainedReputation;
                                int chance = randomNumber.Next(1, 100);
                                if(chance <= 10 && CharacterInventory.numberOfItems < CharacterInventory.capacity)
                                {
                                    Console.WriteLine("You found a poisoned dagger.");
                                    CharacterInventory.items[6]++;
                                    CharacterInventory.numberOfItems++;
                                }
                                
                            }
                            Thread.Sleep(1500);
                        }
                        Text.JobText(job, 4, 60, jobRank);
                        energy -= 60;
                    }
                    else
                    {
                        int productivity = randomNumber.Next(7, 10) + efficiency * 4;
                        Text.JobText(job, 2, productivity, jobRank);
                        currentGold += productivity * 3 * (int)job * jobRank;
                        expPoints += productivity * 2 * (int)job * jobRank;
                        if (job == Character.Jobs.Banker)
                            currentGold += randomNumber.Next(3, 10) * 15;
                        energy -= 20 * (int)job + (jobRank - 1) * 10;
                        Text.JobText(job, 3, 20 * (int)job + (jobRank - 1) * 10, jobRank);
                        if(job == Character.Jobs.Fishing && fishingQuest == true)
                        {
                            fishingQProgress += productivity;
                            if(fishingQProgress >= fishingQRank * 20 && fishingQuest == true)
                            {
                                Console.WriteLine("Quest Completed!");
                                Console.WriteLine("Rewards:");
                                int questGold = fishingQRank * 120;
                                int fishingQExp = fishingQRank * 200;
                                Console.WriteLine("Gold:  " + questGold);
                                Console.WriteLine("Exp :  " + fishingQExp);
                                currentGold += questGold;
                                expPoints += fishingQExp;
                                fishingQuest = false;
                                Thread.Sleep(750);
                            }
                        }
                        Thread.Sleep(1500);
                    }
                    promotion++;
                    if (promotion == jobRank * 5 && jobRank < 5)
                    {
                        promotion = 0;
                        experience++;
                        jobRank++;
                        JobName.SetName(job, jobRank, ref jobName);
                    }
                }
            }
            else Resignation(ref job, jobRank, ref jobName);
        }
        public static void Warning(ref int warnings){
            warnings++;
            if (warnings < 3)
            {
                Console.WriteLine("You didn't complete all your shifts this week and you boss warned you about getting fired if that happens anymore");
                Console.WriteLine(3 - warnings + " warnings left until you are fired!");
            }
        }
        public static void Resignation(ref Character.Jobs job, int jobRank, ref string jobName) {
            Console.WriteLine("You resigned from your current job.");
            job = Character.Jobs.None;
            JobName.SetName(job, jobRank, ref jobName);
            Thread.Sleep(1000);
        }
    }
}
