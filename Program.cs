using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using ConsoleDungeon.CharacterAndMobs;
using ConsoleDungeon.MenuAndText;
using ConsoleDungeon.Locations;
using System.Reflection.Emit;
using System.Threading;

namespace ConsoleDungeon
{
    class Program
    {
        enum Activity
        {
            Sleep=1,
            Study,
            Training,
            Work,
            Gambling,
            Arena,
            CommunityService,
            Dungeon,
            Tournament,
            PointAssignment,
            SeeInventory,
            EquipItem,
            UnequipItem,
            Shop,
            SellItems,
            QuestLog,
            Surrender = 0
        }
        public static int mobKills = 0, bossKills = 0, jobShifts = 0, arenaKills = 0, tournamentWins = 0;
        static int currentDay = 1, option;
        public static int daysLeft = 364;
        static int ev = 0;
        static void Main(string[] args)
        {
            Start();
            Play();
        }
        static void Start(){
            Console.SetWindowSize(37, 40);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n\n\n\n\n\n*===================================*");
            Console.WriteLine("|          *                 *      |");
            Console.WriteLine("|    *           ||                 |");
            Console.WriteLine("|               | ||   *            |");
            Console.WriteLine("|   *           | ||           *    |");
            Console.WriteLine("|               | ||          *     |");
            Console.WriteLine("| *         *   | ||          *     |");
            Console.WriteLine("|               | ||       *        |");
            Console.WriteLine("|               | ||                |");
            Console.WriteLine("|               | ||                |");
            Console.WriteLine("|               | ||                |");
            Console.WriteLine("|            ___|  | ___            |");
            Console.WriteLine("|           /___    ___/            |");
            Console.WriteLine("|               |  |                |");
            Console.WriteLine("|                ||                 |");
            Console.WriteLine("|                ||                 |");
            Console.WriteLine("*===================================*\n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("          Console Dungeon             ");
            Thread.Sleep(3000);
            Console.Clear();
        }
        static void Play(){
            currentDay = 1;
            mobKills = bossKills = jobShifts = arenaKills = tournamentWins = 0;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Character hero = new Character();
            Quests.QuestReset(ref hero.fishingQuest, ref hero.tournamentQuest, ref hero.gamblingQuest, ref hero.arenaQuest, ref hero.fishingQRank, ref hero.tournamentQRank, ref hero.gamblingQRank, ref hero.arenaQRank, ref hero.fishingQProgress, ref hero.tournamentQProgress, ref hero.gamblingQProgress, ref hero.arenaQProgress);
            Activity chosenActivity = new Activity();
            while (hero.alive && !hero.win)
            {
                if (currentDay == 31 || currentDay == 59 || currentDay == 90 || currentDay == 120 || currentDay == 151 || currentDay == 181 || currentDay == 212 || currentDay == 243 || currentDay == 273 || currentDay == 304 || currentDay == 334 || currentDay == 365)
                    Quests.QuestReset(ref hero.fishingQuest, ref hero.tournamentQuest, ref hero.gamblingQuest, ref hero.arenaQuest, ref hero.fishingQRank, ref hero.tournamentQRank, ref hero.gamblingQRank, ref hero.arenaQRank, ref hero.fishingQProgress, ref hero.tournamentQProgress, ref hero.gamblingQProgress, ref hero.arenaQProgress);
                if (Shop.lastRestockDay != currentDay)
                {
                    Shop.ItemsRestock();
                    Shop.lastRestockDay++;
                }
                if (BlackJack.lastDay != currentDay)
                {
                    BlackJack.dailyWin = 0;
                    BlackJack.lastDay++;
                }
                Menu.MainMenuPrint(currentDay, hero.name, hero.race, hero.level, hero.strength, hero.defense, hero.energy, hero.gold, hero.intelligence, hero.jobName, hero.dexterity, hero.reputation, hero.hp, hero.fullHp, hero.healthBar);
                option = Convertion.ToInt(Console.ReadLine(), 1);
                if (option > 16 || option < 0)
                    option = 1;
                chosenActivity = (Activity)option;
                if (currentDay % 10 == 0)
                    hero.studyInterest = hero.workInterest = hero.trainingInterest = 0;
                if (currentDay == hero.hiredDay + 7 && ((hero.currentJob == Character.Jobs.Guard && hero.shifts < 4) || ((hero.currentJob == Character.Jobs.Banker || hero.currentJob == Character.Jobs.Blacksmith) && hero.shifts < 3)))
                {
                    Job.Warning(ref hero.warnings);
                    hero.hiredDay += 7;
                    hero.shifts = 0;
                }
                if (hero.warnings == 3)
                    hero.Fired(ref hero.currentJob, ref hero.jobName);
                switch (chosenActivity)
                {
                    case Activity.Sleep: { hero.Sleep(ref hero.energy, ref currentDay, ref hero.efficiency); daysLeft--; } break;
                    case Activity.Study: { hero.Study(ref hero.energy, ref hero.intelligence, ref currentDay, ref hero.efficiency, ref hero.studyInterest); daysLeft--; } break;
                    case Activity.Training: { hero.Training(ref hero.energy, ref hero.strength, ref currentDay, ref hero.dexterity, ref hero.trainingInterest); daysLeft--; } break;
                    case Activity.CommunityService: { hero.CommunityWork(ref hero.energy, ref hero.reputation, ref currentDay, ref hero.workInterest); daysLeft--; } break;
                    case Activity.Work:
                        {
                            if (hero.currentJob == Character.Jobs.None)
                                hero.ChooseAJob(ref hero.currentJob, hero.strength, hero.intelligence, hero.reputation, hero.dexterity, hero.workExperience, ref hero.shifts, ref hero.warnings, ref hero.jobName, ref hero.hiredDay, currentDay);
                            else Job.Work(ref currentDay, ref hero.gold, ref hero.expPoints, ref hero.energy, ref hero.jobRank, ref hero.currentJob, ref hero.efficiency, ref hero.workExperience, ref hero.shifts, ref hero.jobName, hero.level, ref hero.hp, hero.fullHp, ref hero.strength, hero.defense, hero.dexterity, ref hero.lives, ref hero.reputation, ref hero.fishingQuest, ref hero.fishingQProgress, hero.fishingQRank);
                        }
                        break;
                    case Activity.PointAssignment: { Level.PointAssignment(ref hero.strength, ref hero.defense, ref hero.dexterity, ref hero.intelligence, ref hero.reputation, ref hero.points); } break;
                    case Activity.Dungeon: { Dungeon.Exploration(ref hero.energy, ref currentDay, ref Dungeon.dungeonFloor, ref hero.hp, hero.fullHp, ref hero.expPoints, ref hero.strength, hero.defense, hero.dexterity, ref hero.gold, ref hero.reputation, ref hero.lives, ref hero.win, ref hero.mainQRank, ref hero.mainQProgress); } break;
                    case Activity.SeeInventory:
                        {
                            CharacterInventory.ShowInventory(1, 38, ref CharacterInventory.empty);
                            Console.ReadLine();
                        }
                        break;
                    case Activity.EquipItem: { CharacterInventory.UseItem(ref hero.strength, ref hero.defense, ref hero.hp, ref hero.fullHp, ref hero.energy, ref hero.dexterity, ref hero.slot1, ref hero.slot2, ref hero.helmet, ref hero.chestplate, ref hero.leggins, ref hero.boots, ref hero.gloves, ref CharacterInventory.empty, hero.weaponMastery, hero.level); } break;
                    case Activity.UnequipItem: { CharacterInventory.UnequipItems(ref hero.strength, ref hero.defense, ref hero.slot1, ref hero.slot2, hero.dualWielding, ref hero.helmet, ref hero.chestplate, ref hero.leggins, ref hero.boots, ref hero.gloves); } break;
                    case Activity.Shop: { Shop.BuyItems(ref hero.gold, hero.currentJob); } break;
                    case Activity.SellItems: { Shop.SellItems(ref hero.gold); } break;
                    case Activity.Gambling:
                        {
                            Menu.BlackJackRules();
                            BlackJack.Play(ref hero.gold, ref hero.gamblingQuest, ref hero.gamblingQProgress, hero.gamblingQRank, ref hero.expPoints);
                        }
                        break;
                    case Activity.Arena:
                        {
                            Console.WriteLine("Price: " + (20 * hero.level));
                            Arena.ArenaFight(ref currentDay, hero.level, ref hero.energy, ref hero.hp, hero.fullHp, ref hero.strength, hero.defense, hero.dexterity, ref hero.expPoints, ref hero.gold, ref hero.arenaQuest, ref hero.arenaQProgress, hero.arenaQRank);
                        }
                        break;
                    case Activity.Tournament:
                        {
                            Menu.TournamentRules();
                            Tournament.Play(ref currentDay, hero.level, ref hero.energy, ref hero.hp, hero.fullHp, ref hero.strength, hero.defense, hero.dexterity, ref hero.expPoints, ref hero.gold, ref hero.tournamentQuest, ref hero.tournamentQProgress, hero.tournamentQRank);
                        }
                        break;
                    case Activity.QuestLog: { Menu.ShowQuests(hero.mainQuest, hero.fishingQuest, hero.tournamentQuest, hero.gamblingQuest, hero.arenaQuest, hero.mainQRank, hero.fishingQRank, hero.tournamentQRank, hero.gamblingQRank, hero.arenaQRank, hero.mainQProgress, hero.fishingQProgress, hero.tournamentQProgress, hero.gamblingQProgress, hero.arenaQProgress); } break;
                    case Activity.Surrender: { hero.alive = false; } break;
                }
                if (hero.expPoints >= hero.levelUp)
                {
                    if (hero.level < 100)
                        Level.LevelUp(ref hero.expPoints, ref hero.levelUp, ref hero.points, ref hero.level, ref hero.fullHp, ref hero.hp, ref hero.healthBar);
                    else hero.expPoints = 0;
                }
                HealthBars.SetHealthBar(ref hero.healthBar, hero.hp, hero.fullHp);
                if (hero.reputation >= 500 && ev == 0)
                {
                    Console.WriteLine("You received a gift from the king for being a model villager");
                    Console.WriteLine("You got 1 Poisoned Dagger");
                    CharacterInventory.numberOfItems++;
                    CharacterInventory.items[6]++;
                    ev++;
                }
                if (hero.reputation >= 1000 && ev == 1)
                {
                    Console.WriteLine("You received a gift from the king for being the best villager");
                    Console.WriteLine("You got 1 War Hammer");
                    CharacterInventory.numberOfItems++;
                    CharacterInventory.items[8]++;
                    ev++;
                }
                if (hero.reputation >= 10000 && ev == 2)
                {
                    Console.WriteLine("The village decided to make you the hero of the village");
                    if (CharacterInventory.items[10] == 0)
                    {
                        Console.WriteLine("You got 1 Excaliber");
                        CharacterInventory.numberOfItems++;
                        CharacterInventory.items[10]++;
                    }
                    else
                    {
                        Console.WriteLine("You got 1 Meteorite Chestplate");
                        CharacterInventory.numberOfItems++;
                        CharacterInventory.items[19]++;
                    }
                    ev++;
                }
                if (hero.lives == 0)
                    hero.alive = false;
                if (currentDay % 7 == 0)
                    Console.Clear();
                if (hero.strength > 999999999)
                    hero.strength = 999999999;
                if (hero.defense > 999999999)
                    hero.defense = 999999999;
                if (hero.dexterity > 999999999)
                    hero.dexterity = 999999999;
                if (hero.intelligence > 999999999)
                    hero.intelligence = 999999999;
                if (hero.reputation > 999999999)
                    hero.reputation = 999999999;
                if (hero.gold > 999999999)
                    hero.gold = 999999999;
                if (daysLeft <= 0)
                    hero.alive = false;
            }
            if (!hero.alive)
            {
                if (chosenActivity != Activity.Surrender)
                    Console.WriteLine("You died..");
                else
                    Console.WriteLine("You surrendered");
                FinalStats();
                Console.Write("New run(y/n) ");
                if (Console.ReadLine() == "y")
                {
                    Console.Clear();
                    Program.Play();
                }
            }
            if (hero.win == true)
            {
                Console.WriteLine("You won, gg!");
                FinalStats();
                Console.Write("New run(y/n) ");
                if (Console.ReadLine() == "y")
                {
                    Console.Clear();
                    Program.Play();
                }
            }
        }
        static void FinalStats(){
            Console.WriteLine("\n*Stats: *");
            Console.WriteLine("*~~~~~~~~~~~~~~~~~~~~~~*");
            Console.WriteLine("Days           : " + (currentDay-1));
            Console.WriteLine("Mob kills      : " + mobKills);
            Console.WriteLine("Boss kills     : " + bossKills);
            Console.WriteLine("Job shifts     : " + jobShifts);
            Console.WriteLine("Arena wins     : " + arenaKills);
            Console.WriteLine("Tournament wins: " + tournamentWins);
            Console.WriteLine("*~~~~~~~~~~~~~~~~~~~~~~*");
        }
    }
}
