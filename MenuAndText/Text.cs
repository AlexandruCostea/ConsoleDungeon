using System;
using System.Collections.Generic;
using System.Text;
using ConsoleDungeon.CharacterAndMobs;
using ConsoleDungeon.Locations;

namespace ConsoleDungeon.MenuAndText
{
    class Text
    {
        public static void JobText(Character.Jobs job, int partOfTheJob, int productivity, int rank){
            switch(job)
            {
                case Character.Jobs.Fishing: {
                        switch(partOfTheJob)
                        {
                            case 1: { Console.WriteLine("You went to the lake"); Console.WriteLine("in order to catch some fish."); }break;
                            case 2: { Console.WriteLine("You caught " + productivity + " fish.");
                                      Console.WriteLine("You gained " + productivity * 3 * (int)job * rank + " gold and " + productivity * 2 * (int)job * rank );
                                      Console.WriteLine("experience points by selling them.");
                                }break;
                            case 3: { Console.WriteLine("You lost " + productivity + " energy points."); }break;
                        }
                    }break;
                case Character.Jobs.Blacksmith:{
                        switch(partOfTheJob)
                        {
                            case 1: { Console.WriteLine("You went to the forge"); Console.WriteLine("and began crafting armour."); }break;
                            case 2: { Console.WriteLine("You created " + productivity + " chest plates.");
                                      Console.WriteLine("You sold them for " + productivity * 3 * (int)job * rank + " gold. You");
                                      Console.WriteLine("also gained " + productivity * 2 * (int)job * rank + " experience points.");
                                }break;
                            case 3: { Console.WriteLine("You lost " + productivity + " energy points."); }break;
                        }
                    }break;
                case Character.Jobs.Guard: {
                        switch(partOfTheJob)
                        {
                            case 1: { Console.WriteLine("You have to guard"); Console.WriteLine("a random dude tonight."); }break;
                            case 2: { Console.WriteLine("Nobody tried to attack the dude."); }break;
                            case 3: { Console.WriteLine("An assassin is aproaching the"); Console.WriteLine("dude. Beat him."); }break;
                            case 4: { Console.WriteLine("You lost " + productivity + " energy points."); }break;
                        }
                    }break;
                case Character.Jobs.Banker: {
                        switch(partOfTheJob)
                        {
                            case 1: { Console.WriteLine("You got ready for another day of"); Console.WriteLine("getting paid for doing nothing."); }break;
                            case 2: { Console.WriteLine("You lost another day at a desk.");
                                    Console.WriteLine("You have been paid " + productivity * 3 * (int)job * rank + " gold,"); Console.WriteLine("and gained " + productivity * 2 * (int)job * rank + " exp points.");
                                }break;
                            case 3: { Console.WriteLine("You lost " + productivity + " energy points."); }break;
                        }
                    }break;
            }
        }
        public static void TrainingText(Character.TrainingOptions choice, int partOfTRaining, int gains) {
            switch(choice)
            {
                case Character.TrainingOptions.Gym: {
                        switch(partOfTRaining)
                        {
                            case 1: { Console.WriteLine("You went to the gym."); }break;
                            case 2: { Console.WriteLine("You gained " + gains + " strength points."); }break;
                            case 3: { Console.WriteLine("You lost " + gains + " energy points."); }break;
                        }                
                    }break;
                case Character.TrainingOptions.HighGravityRoom: { 
                        switch(partOfTRaining)
                        {
                            case 1: { Console.WriteLine("You chose a real training method."); }break;
                            case 2: { Console.WriteLine("You gained " + gains + " strength points."); } break;
                            case 3: { Console.WriteLine("You lost " + gains + " energy points."); } break;
                        }
                    }break;
                case Character.TrainingOptions.HyperbolicTimeChamber: { 
                        switch(partOfTRaining)
                        {
                            case 1: { Console.WriteLine("You can use this only once more!"); }break;
                            case 2: { Console.WriteLine("You can't use this chamber again!"); Console.WriteLine(" If you try to enter by force,"); Console.WriteLine("the chamber will dissappear!"); }break;
                        }
                    }break;
            }
        }
        public static void DungeonBattleText(int mobType, int partOfBattle){
            switch(partOfBattle)
            {
                case 1: {
                        if (mobType != 5)  //Boss battle
                        {
                            if(mobType != 4)
                               Console.WriteLine("A " + (Mobs.MobSpecies)mobType + " approaches you.");
                            else
                            {
                                Random randomNUmber = new Random();
                                if (randomNUmber.Next(1, 2) == 1)
                                    Console.WriteLine("A Reanimated Dragon aproaches you.");
                                else Console.WriteLine("A Dragon apeoaches you.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You managed to defeat all the mobs");
                            Console.WriteLine("and got to the boss chamber.");
                            if (Dungeon.dungeonFloor == 20)
                                Console.WriteLine("Enemy: The Skeleton King");
                            if (Dungeon.dungeonFloor == 40)
                                Console.WriteLine("Enemy: Evil Spirit");
                            if (Dungeon.dungeonFloor == 60)
                                Console.WriteLine("Enemy: The Reaper");
                            if (Dungeon.dungeonFloor == 80)
                                Console.WriteLine("Enemy: Unknown Colossal Creature");
                            if (Dungeon.dungeonFloor == 100)
                                Console.WriteLine("Enemy: The Warlock");
                        }
                    }break;
            }
        }
    }
}
