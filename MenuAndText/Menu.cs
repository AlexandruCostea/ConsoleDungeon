using ConsoleDungeon.CharacterAndMobs;
using ConsoleDungeon.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ConsoleDungeon.MenuAndText
{
    class Menu
    {
        static string day, name, race, lvl, power, defense, energy, gold, intelligence, job, dexterity, reputation, hpb, hpBar;
        public static void MainMenuPrint(int currentDay, string heroName, string heroRace, int level, int heroPower, int heroDefense, int heroEnergy, int heroGold, int heroIntelligence, string currentJob, int heroDexterity, int heroReputation, int hp, int fullHp, string HealthBar){
            MainMenuStatsSet(currentDay, heroName, heroRace, level, heroPower, heroDefense, heroEnergy, heroGold, heroIntelligence, currentJob, heroDexterity, heroReputation, hp, fullHp, HealthBar);
            Console.WriteLine("*===================================*");
            Console.WriteLine("| Day          :  " + day + "     |");
            Console.WriteLine("| Name         :  " + name + "     |");
            Console.WriteLine("| Race         :  " + race + "     |");
            Console.WriteLine("| Level        :  " + lvl + "     |");
            Console.WriteLine("| Hp           :  " + hpBar + "     |");
            Console.WriteLine("|                 " + hpb + "     |");
            Console.WriteLine("| Strength     :  " + power + "     |");
            Console.WriteLine("| Defense      :  " + defense + "     |");
            Console.WriteLine("| Job          :  " + job + "     |");
            Console.WriteLine("| Intelligence :  " + intelligence + "     |");
            Console.WriteLine("| Dexterity    :  " + dexterity + "     |");
            Console.WriteLine("| Reputation   :  " + reputation + "     |");
            Console.WriteLine("| Gold         :  " + gold + "     |");
            Console.WriteLine("| Energy       :  " + energy + "     |");
            Console.WriteLine("*===================================*");
            Console.WriteLine("|  Activities:                      |");
            Console.WriteLine("*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*");
            Console.WriteLine("| [1] Sleep  [2] Study  [3] Train   |");
            Console.WriteLine("| [4] Work   [5] Gamble [6] Arena   |");
            Console.WriteLine("| [7] Community Service             |");
            Console.WriteLine("| [8] Explore the Dungeon           |");
            Console.WriteLine("| [9] Participate in a tournament   |");
            Console.WriteLine("|-----------------------------------|");
            Console.WriteLine("|[10]Spend Points  [11]See Inventory|");
            Console.WriteLine("|[12]Equip Item    [13]Unequip Item |");
            Console.WriteLine("|[14]Buy Items     [15]Sell Items   |");
            Console.WriteLine("|[16]Monthly Quests                 |");
            Console.WriteLine("|-----------------------------------|");
            Console.WriteLine("|         [0] Surrender             |");
            Console.WriteLine("*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*");

        }
        static void MainMenuStatsSet(int currentDay, string heroName, string heroRace, int level, int heroPower, int heroDefense, int heroEnergy, int heroGold, int heroIntelligence, string currentJob, int heroDexterity, int heroReputation, int hp, int fullHp, string HealthBar)
        {
            day = currentDay.ToString();
            while (day.Length != 13)
                day += " ";
            name = heroName;
            while (name.Length != 13)
                name += " ";
            race = heroRace;
            while (race.Length != 13)
                race += " ";
            lvl = level.ToString();
            while (lvl.Length != 13)
                lvl += " ";
            hpBar = HealthBar;
            while (hpBar.Length != 13)
                hpBar += " ";
            hpb = hp.ToString();
            hpb += "/";
            hpb += fullHp.ToString();
            while (hpb.Length != 13)
                hpb += " ";
            job = currentJob;
            while (job.Length != 13)
                job += " ";
            SetStat(ref power, heroPower);
            SetStat(ref defense, heroDefense);
            SetStat(ref dexterity, heroDexterity);
            SetStat(ref intelligence, heroIntelligence);
            SetStat(ref gold, heroGold);
            SetStat(ref reputation, heroReputation);
            SetStat(ref energy, heroEnergy);
        }
        static void SetStat(ref string stat, int heroStat){
            if (heroStat < 1000)
            {
                stat = heroStat.ToString();
                while (stat.Length != 13)
                    stat += " ";
            }
            else if (heroStat >= 1000 && heroStat < 10000)
            {
                stat = (heroStat / 1000).ToString();
                stat += ".";
                stat += ((heroStat / 100) % 10).ToString();
                stat += "k";
                while (stat.Length != 13)
                    stat += " ";
            }
            else if (heroStat >= 10000 && heroStat < 1000000)
            {
                stat = (heroStat / 1000).ToString();
                stat += "k";
                while (stat.Length != 13)
                    stat += " ";
            }
            else if (heroStat >= 1000000)
            {
                stat = (heroStat / 1000000).ToString();
                stat += ".";
                stat += ((heroStat / 100000) % 10).ToString();
                stat += "m";
                while (stat.Length != 13)
                    stat += " ";
            }
        }
        public static void JobMenuPrint(){
            Console.WriteLine("*Possible Jobs:*");
            Console.WriteLine("*==================================*");
            Console.WriteLine("||                                ||");
            Console.WriteLine("||  [1] Fishing    [2] Blacksmith ||");
            Console.WriteLine("||                                ||");
            Console.WriteLine("||  [3] Guard      [4] Banker     ||");
            Console.WriteLine("||                                ||");
            Console.WriteLine("||  [0] Exit                      ||");
            Console.WriteLine("*==================================*\n\n");
        }
        public static void TrainingMenuPrint(){
            Console.WriteLine("*Training Options:*");
            Console.WriteLine("*===================================*");
            Console.WriteLine("||                                 ||");
            Console.WriteLine("||  [1]Gym   [2]Gravity Room       ||");
            Console.WriteLine("||                                 ||");
            Console.WriteLine("||  [3]Hyperbolic Time chamber     ||");
            Console.WriteLine("||                                 ||");
            Console.WriteLine("||  [0]Exit                        ||");
            Console.WriteLine("*===================================*\n\n");
        }
        public static void DungeonMenuPrint(int currentFloor, double averagePowerLevel){
            Console.WriteLine("*=============================*");
            Console.WriteLine("Current floor:  " + currentFloor);
            Console.WriteLine("Avg mob power:  " + averagePowerLevel);
            Console.WriteLine("*=============================*");
            Console.WriteLine("*~~~~~~~~~~~~~~~~~~~~~~~~~~~~*");
            Console.WriteLine("||[1] Explore the Dungeon   ||");
            Console.WriteLine("||[2] Exit                  ||");
            Console.WriteLine("*~~~~~~~~~~~~~~~~~~~~~~~~~~~~*\n");
        }
        public static void PotionShopPrint(){
            int i,number;
            bool empty = true;
            for (i = 1; i <= 7; i++)
                if (Shop.potionsOnSale[i] > 0)
                    empty = false;
            if(empty)
            {
                Console.WriteLine("There are no potions for sale.");
                Thread.Sleep(750);
            }
            else
            {
                Console.WriteLine("*=============================*");
                for (i = 1; i<= 7; i++)
                {
                    if(Shop.potionsOnSale[i] > 0)
                    {
                        Console.WriteLine("|[" + i + "] " + CharacterInventory.itemsName[i + 31] + ": " + Shop.potionsOnSale[i] + "  |");
                        Console.Write("|   Price: " + Shop.potionsPrice[i]);
                        number = Shop.potionsPrice[i].ToString().Length + 1;
                        while(number < 20)
                        {
                            number++;
                            Console.Write(" ");
                        }
                        Console.WriteLine("|");
                    }
                }
                Console.WriteLine("*=============================*");
            }
        }
        public static void WeaponShopPrint(){
            int i, number;
            bool empty = true;
            for (i = 1; i <= 10; i++)
                if (Shop.weaponsOnSale[i] > 0)
                    empty = false;
            if (empty)
            {
                Console.WriteLine("There are no weapons for sale.");
                Thread.Sleep(750);
            }
            else
            {
                Console.WriteLine("*===============================*");
                for (i = 1; i <= 10; i++)
                {
                    if (Shop.weaponsOnSale[i] > 0)
                    {
                        Console.Write("|[" + i);
                        if (i < 10)
                            Console.Write(" ");
                        Console.Write("] ");
                        Console.Write(CharacterInventory.itemsName[i + 1] + ": " + Shop.weaponsOnSale[i] + "  ");
                        if (i != 1)
                            Console.Write(" ");
                        Console.WriteLine("|");
                        Console.Write("|   Price:  " + Shop.weaponsPrice[i]);
                        number = Shop.weaponsPrice[i].ToString().Length;
                        while (number < 20)
                        {
                            number++;
                            Console.Write(" ");
                        }
                        Console.WriteLine("|");
                    }
                }
                Console.WriteLine("*===============================*");
            }
        }
        public static void ArmourShopPrint(Character.Jobs job){
            int i, number;
            bool empty = true;
            for (i = 1; i <= 20; i++)
                if (Shop.armourOnSale[i] > 0)
                    empty = false;
            if (empty)
            {
                Console.WriteLine("There are no armour pieces for sale.");
                Thread.Sleep(750);
            }
            else
            {
                Console.WriteLine("*==============================*");
                for (i = 1; i <= 20; i++)
                {
                    if (Shop.armourOnSale[i] > 0)
                    {
                        Console.Write("|[" + i);
                        if (i < 10)
                            Console.Write(" ");
                        Console.Write("] ");
                        switch(i)
                        {
                            case 1: { Console.Write(CharacterInventory.itemsName[12]); }break;
                            case 2: { Console.Write(CharacterInventory.itemsName[16]); } break;
                            case 3: { Console.Write(CharacterInventory.itemsName[20]); } break;
                            case 4: { Console.Write(CharacterInventory.itemsName[24]); } break;
                            case 5: { Console.Write(CharacterInventory.itemsName[28]); } break;
                            case 6: { Console.Write(CharacterInventory.itemsName[13]); } break;
                            case 7: { Console.Write(CharacterInventory.itemsName[17]); } break;
                            case 8: { Console.Write(CharacterInventory.itemsName[21]); } break;
                            case 9: { Console.Write(CharacterInventory.itemsName[25]); } break;
                            case 10: { Console.Write(CharacterInventory.itemsName[29]); } break;
                            case 11: { Console.Write(CharacterInventory.itemsName[14]); } break;
                            case 12: { Console.Write(CharacterInventory.itemsName[18]); } break;
                            case 13: { Console.Write(CharacterInventory.itemsName[22]); } break;
                            case 14: { Console.Write(CharacterInventory.itemsName[26]); } break;
                            case 15: { Console.Write(CharacterInventory.itemsName[30]); } break;
                            case 16: { Console.Write(CharacterInventory.itemsName[15]); } break;
                            case 17: { Console.Write(CharacterInventory.itemsName[19]); } break;
                            case 18: { Console.Write(CharacterInventory.itemsName[23]); } break;
                            case 19: { Console.Write(CharacterInventory.itemsName[27]); } break;
                            case 20: { Console.Write(CharacterInventory.itemsName[31]); } break;
                        }
                        Console.WriteLine(": " + Shop.armourOnSale[i] + "  |");
                        Console.Write("|   Price:  ");
                        if (job == Character.Jobs.Blacksmith)
                            Console.Write(Shop.armourPrice[i] - 20 * Shop.armourPrice[i] / 100);
                        else Console.Write(Shop.armourPrice[i]);
                        if (job != Character.Jobs.Blacksmith)
                            number = Shop.armourPrice[i].ToString().Length + 1;
                        else number = (Shop.armourPrice[i] - 20 * Shop.armourPrice[i] / 100).ToString().Length + 1;
                        while (number < 20)
                        {
                            number++;
                            Console.Write(" ");
                        }
                        Console.WriteLine("|");
                    }
                }
                Console.WriteLine("*==============================*");
            }
        }
        public static void BlackJackRules(){
            Console.WriteLine("Welcome to the casino!");
            Console.WriteLine("*Rules: ");
            Console.WriteLine("*===================================*");
            Console.WriteLine("| Bets can't be lower than 100 gold!|");
            Console.WriteLine("| Bets can't be bigger than         |");
            Console.WriteLine("| 25000 gold!                       |");
            Console.WriteLine("| You can't win more than 50000     |");
            Console.WriteLine("| gold per day!                     |");
            Console.WriteLine("*===================================*\n");
        }
        public static void TournamentRules(){
            Console.WriteLine("*~~~~~~~~~~Tournament Rules~~~~~~~~~*");
            Console.WriteLine("| In a tournament you are going to  |");
            Console.WriteLine("| face 4 different opponents        |");
            Console.WriteLine("| No rewards are given if you don't |");
            Console.WriteLine("| win all the battles.              |");
            Console.WriteLine("| Entry fee is 300 gold             |");
            Console.WriteLine("| You can't die in a tournament.    |");
            Console.WriteLine("|                                   |");
            Console.WriteLine("*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*");
        }
        public static void ShowQuests(bool mainQ, bool fishingQ, bool tournamentQ, bool gamblingQ, bool arenaQ, int mainQRank, int fishingQRank, int tournamentQRank, int gamblingQRank, int arenaQRank, int mainQProg, int fishingQProg, int tournamentQProg, int gamblingQProg, int arenaQProg){
            Console.WriteLine("QuestLog: ");
            Console.WriteLine("*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*");
            if(mainQ)
            {
                switch(mainQRank)
                {
                    case 1: { Console.WriteLine("|Beat Dungeon Floor 20      (" + mainQProg + "/20  )|"); } break;
                    case 2: { Console.WriteLine("|Beat Dungeon Floor 40      (" + mainQProg + "/40  )|"); } break;
                    case 3: { Console.WriteLine("|Beat Dungeon Floor 60      (" + mainQProg + "/60  )|"); } break;
                    case 4: { Console.WriteLine("|Beat Dungeon Floor 80      (" + mainQProg + "/80  )|"); } break;
                    case 5: { Console.WriteLine("|Beat Dungeon Floor 100     (" + mainQProg + "/100 )|"); } break;
                }
            }
            if(fishingQ)
            {
                switch(fishingQRank)
                {
                    case 1: { Console.WriteLine("|Catch 20 fish              (" + fishingQProg + "/20  )|"); } break;
                    case 2: { Console.WriteLine("|Catch 40 fish              (" + fishingQProg + "/40  )|"); } break;
                    case 3: { Console.WriteLine("|Catch 60 fish              (" + fishingQProg + "/60  )|"); } break;
                    case 4: { Console.WriteLine("|Catch 80 fish              (" + fishingQProg + "/80  )|"); } break;
                    case 5: { Console.WriteLine("|Catch 100 fish             (" + fishingQProg + "/100 )|"); } break;
                }
            }
            if(tournamentQ)
            {
                switch(tournamentQRank)
                {
                    case 1: { Console.WriteLine("|Win a tournament           (" + tournamentQProg + "/1   )|"); } break;
                    case 2: { Console.WriteLine("|Win a tournament           (" + tournamentQProg + "/1   )|"); } break;
                    case 3: { Console.WriteLine("|Win 2 tournaments          (" + tournamentQProg + "/2   )|"); } break;
                    case 4: { Console.WriteLine("|Win 4 tournaments          (" + tournamentQProg + "/4   )|"); } break;
                    case 5: { Console.WriteLine("|Win 6 tournaments          (" + tournamentQProg + "/6   )|"); } break;
                }
            }
            if(gamblingQ)
            {
                switch(gamblingQRank)
                {
                    case 1: { Console.WriteLine("|Win 300 gold at BlackJack  (" + gamblingQProg + "/300 )|"); } break;
                    case 2: { Console.WriteLine("|Win 700 gold at BlackJack  (" + gamblingQProg + "/700 )|"); } break;
                    case 3: { Console.WriteLine("|Win 1000 gold at BlackJack (" + gamblingQProg + "/1000)|"); } break;
                    case 4: { Console.WriteLine("|Win 2500 gold at BlackJack (" + gamblingQProg + "/2500)|"); } break;
                    case 5: { Console.WriteLine("|Win 6000 gold at BlackJack (" + gamblingQProg + "/6000)|"); } break;
                }
            }
            if(arenaQ)
            {
                switch(arenaQRank)
                {
                    case 1: { Console.WriteLine("|Win 3 battles in Arena     (" + arenaQProg + "/3   )|"); } break;
                    case 2: { Console.WriteLine("|Win 6 battles in Arena     (" + arenaQProg + "/6   )|"); } break;
                    case 3: { Console.WriteLine("|Win 9 battles in Arena     (" + arenaQProg + "/9   )|"); } break;
                    case 4: { Console.WriteLine("|Win 12 battles in Arena    (" + arenaQProg + "/12  )|"); } break;
                    case 5: { Console.WriteLine("|Win 15 battles in Arena    (" + arenaQProg + "/15  )|"); } break;
                }
            }
            Console.WriteLine("*~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~*");
            Console.ReadLine();
        }
    }
}
