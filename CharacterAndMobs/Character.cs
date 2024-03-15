using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Resources;
using System.Security;
using System.Text;
using System.Threading;
using ConsoleDungeon.MenuAndText;
using ConsoleDungeon.Locations;

namespace ConsoleDungeon.CharacterAndMobs
{
    class Character
    {
        public string name,race,jobName,healthBar;
        public int energy, strength, defense, gold, intelligence, dexterity, reputation = 10, slot1 = 0, slot2 = -1, efficiency = 0, dualWielding = 0, weaponMastery = 0, lives = 3, hp = 100, fullHp = 100, level = 1, jobRank, workExperience = 0, studyInterest = 0, workInterest = 0, trainingInterest = 0, shifts, warnings, maximumGains = 0, chamberUses = 0, expPoints = 0, levelUp = 100, points = 0, hiredDay;
        public int helmet = 0, chestplate = 0, leggins = 0, boots = 0, gloves = 0;
        public bool alive = true, mainQuest=true, fishingQuest = false, tournamentQuest = false, gamblingQuest = false, arenaQuest = false, win = false;
        public int mainQRank = 1, fishingQRank = 0, tournamentQRank = 0, gamblingQRank = 0, arenaQRank = 0;
        public int mainQProgress = 0, fishingQProgress = 0, tournamentQProgress = 0, gamblingQProgress = 0, arenaQProgress = 0;
        enum Race{
            Human=1, Orc, Elf
        }
        public enum Jobs{
            None, Fishing, Blacksmith, Guard, Banker
        }
        public enum TrainingOptions{
            Gym=1,
            HighGravityRoom,
            HyperbolicTimeChamber
        }
        public Jobs currentJob;
        public Character(){
            Console.WriteLine("Name (maximum 20 letters): ");
            name = Console.ReadLine();
            Random randomNumber = new Random();
            if (name.Length > 20)
                name = name.Substring(0, 20);
            Console.WriteLine("Race: ");
            Console.WriteLine(" 1]Human");
            Console.WriteLine(" Humans sleep and study better,");
            Console.WriteLine(" and they work more efficiently.\n");
            Console.WriteLine(" 2]Orc");
            Console.WriteLine(" Orcs are a powerful race that can");
            Console.WriteLine(" equip more weapons at the same time.\n");
            Console.WriteLine(" 3]Elf");
            Console.WriteLine(" Elves are the best weapon users,");
            Console.WriteLine(" so they deal more damage with them.\n");
            Console.WriteLine(" 4]Random");
            int raceNumber = Convertion.ToInt(Console.ReadLine(),2);
            if (raceNumber > 3 || raceNumber < 1)
                raceNumber = randomNumber.Next(1,3);
            Race choice = (Race)raceNumber;
            race = choice.ToString();
            jobName = "None";
            HealthBars.SetHealthBar(ref healthBar, hp, fullHp);
            for (int i = 1; i <= 38; i++)
                CharacterInventory.items[i] = 0;
            ItemName.Set(ref CharacterInventory.itemsName);
            switch(choice)
            {
                case Race.Human: {
                        energy = 70;
                        gold = 40;
                        strength = 20;
                        intelligence = 30;
                        efficiency = 1;
                        dexterity = 20;
                        defense = 10;
                        CharacterInventory.items[(int)CharacterInventory.Items.minHpPotion] = 1;
                        CharacterInventory.numberOfItems = 1;
                    }
                    break;
                case Race.Orc: {
                        energy = 20;
                        gold = 0;
                        strength = 30;
                        intelligence = 15;
                        slot2 = 0;
                        dualWielding = 1;
                        slot2 = 0;
                        dexterity = 15;
                        defense = 30;
                        CharacterInventory.items[(int)CharacterInventory.Items.rustySwords] = 1;
                        CharacterInventory.numberOfItems = 1;
                    }
                    break;
                case Race.Elf: {
                        energy = 50;
                        gold = 50;
                        strength = 10;
                        intelligence = 25;
                        weaponMastery = 1;
                        dexterity = 15;
                        defense = 15;
                        CharacterInventory.items[(int)CharacterInventory.Items.knives] = 1;
                        CharacterInventory.items[(int)CharacterInventory.Items.minEngPotion] = 1;
                        CharacterInventory.numberOfItems = 2;
                    }
                    break;
            }
        }
        public void Sleep(ref int currentEnergy, ref int currentDay, ref int efficiency){
            Random randomNumber = new Random();
            int gainedEnergy = randomNumber.Next(40,60)+efficiency*25;
            if(gainedEnergy+currentEnergy>300)
            {
                gainedEnergy = 300 - currentEnergy;
            }
            currentEnergy += gainedEnergy;
            if (gainedEnergy == 0)
                Console.WriteLine("You can't fall asleep");
            else
            {
                Console.WriteLine("You went to sleep.");
                currentDay++;
                Thread.Sleep(750);
                Console.WriteLine("Your energy has been increased by " + gainedEnergy);
            }
            Thread.Sleep(1000);
        }
        public void Study(ref int currentEnergy, ref int intelligence, ref int currentDay, ref int efficiency, ref int interest){
            Random randomNumber = new Random();
            if (currentEnergy < 50)
            {
                Console.WriteLine("You tried to read a book");
                Console.WriteLine("and fell asleep at the library.");
            }
            else
            {
                if (interest >= 2)
                    Console.WriteLine("You have no motivation to study.");
                else
                {
                    Console.WriteLine("Today you went to the library");
                    Console.WriteLine("to study.");
                    currentDay++;
                    interest++;
                    Thread.Sleep(750);
                    int gainedKnowledge = randomNumber.Next(1, 30) + randomNumber.Next(10, 20) * efficiency;
                    intelligence += gainedKnowledge;
                    currentEnergy -= 50;
                    Console.WriteLine("You gained " + gainedKnowledge + " intelligence points.");
                    Console.WriteLine("You lost 50 energy points.");
                }
            }
            Thread.Sleep(1000);
        }
        public void Training(ref int currentEnergy, ref int strength, ref int currentDay, ref int dexterity, ref int interest){
            Random randomNumber = new Random();
            Menu.TrainingMenuPrint();
            if (interest < 3)
            {
                Console.Write("Choose your training method: ");
                int choice = Convertion.ToInt(Console.ReadLine(), 2);
                if (choice == 1 || choice == 2)
                {
                    if (maximumGains >= 500)
                    {
                        Console.WriteLine("Your body reached full potential.");
                        Console.WriteLine("There's no point in training anymore.");
                    }
                    else
                    {
                        if (currentEnergy >= choice * 30 + (choice - 1) * 60 && interest < 3)
                        {
                            Text.TrainingText((TrainingOptions)choice, 1, 0);
                            currentDay++;
                            interest++;
                            Thread.Sleep(750);
                            int gainedStrength = randomNumber.Next(5, 15) + (choice - 1) * 30;
                            maximumGains += gainedStrength;
                            if (maximumGains > 500)
                                gainedStrength -= maximumGains - 500;
                            strength += gainedStrength;
                            Text.TrainingText((TrainingOptions)choice, 2, gainedStrength);
                            if (choice == 1)
                            {
                                int gainedDexterity = randomNumber.Next(5, 20);
                                Console.WriteLine("You gained " + gainedDexterity + " dexterity points.");
                                dexterity += gainedDexterity;
                            }
                            currentEnergy -= choice * 30 + (choice - 1) * 60;
                            Text.TrainingText((TrainingOptions)choice, 3, choice * 30 + (choice - 1) * 60);
                        }
                        else Console.WriteLine("You are too tired.");
                    }
                }
                else if (choice == 3)
                {
                    if (chamberUses < 2 && currentEnergy >= 200 && maximumGains < 800)
                    {
                        Console.WriteLine("You chose to train for one year");
                        Console.WriteLine("in one single day.");
                        Thread.Sleep(750);
                        currentEnergy -= 200;
                        int gainedStrength = (strength * 15) / 100;
                        maximumGains += gainedStrength;
                        if (maximumGains > 800)
                            gainedStrength -= maximumGains - 800;
                        strength += gainedStrength;
                        chamberUses++;
                        currentDay++;
                        Console.WriteLine("You gained " + gainedStrength + " strength points.");
                        Console.WriteLine("You lost 200 energy points.");
                        Text.TrainingText((TrainingOptions)choice, chamberUses, 0);
                        interest = 3;
                    }
                    else
                    {
                        if (chamberUses == 2)
                            Console.WriteLine("You are not allowed in again.");
                        else if (maximumGains >= 800)
                        {
                            Console.WriteLine("You got that strong that not even ");
                            Console.WriteLine("this could help you improve more.");
                        }
                        else Console.WriteLine("You are too tired.");
                    }
                }
                else if (choice < 1 || choice > 3) Console.WriteLine("Lazy..");
            }
            else
            {
                Console.WriteLine("Your previous sessions exhausted");
                Console.WriteLine(" you. Take a break for a few days.");
            }
            Thread.Sleep(1500);
        }
        public void CommunityWork(ref int currentEnergy, ref int reputation, ref int currentDay, ref int interest){
            Random randomNumber = new Random();
            if (currentEnergy < 100)
            {
                Console.WriteLine("You can't do community");
                Console.WriteLine("service because you are too tired.");
            }
            else
            {
                if (interest >= 1)
                {
                    Console.WriteLine("You have better stuff to do");
                    Console.WriteLine("than helping this shitty community.");
                }
                else
                {
                    Console.WriteLine("Today you worked with other");
                    Console.WriteLine("volunteers to clean the village.");
                    currentDay++;
                    interest++;
                    Thread.Sleep(750);
                    int gainedReputation = randomNumber.Next(30, 50);
                    reputation += gainedReputation;
                    currentEnergy -= 100;
                    Console.WriteLine("You gained " + gainedReputation + " rep points.");
                    Console.WriteLine("You lost 100 energy points.");
                }
            }
            Thread.Sleep(1000);
        }
        public void ChooseAJob(ref Character.Jobs job, int strength, int intelligence, int reputation, int dexterity, int experience, ref int shifts, ref int warnings, ref string jobName, ref int hiredDay, int currentDay){
            Console.WriteLine("You don't have a job yet.");
            Console.WriteLine("Try to get one!");
            Menu.JobMenuPrint();
            Console.Write("Choose your job: ");
            int choice = Convertion.ToInt(Console.ReadLine(),2);
            if (choice >= 1 && choice <= 4)
            {
                switch ((Jobs)choice)
                {
                    case Jobs.Fishing:
                        {
                            if (strength >= 40 && intelligence >= 20)
                                Hired(ref job, ref jobRank, choice, ref shifts, ref warnings, ref jobName, ref hiredDay, currentDay);
                            else
                                Denied((Jobs)choice, ref job, ref jobName);
                        }
                        break;
                    case Jobs.Guard:
                        {
                            if (strength >= 150 && reputation >= 70)
                                Hired(ref job, ref jobRank, choice, ref shifts, ref warnings, ref jobName, ref hiredDay, currentDay);
                            else
                                Denied((Jobs)choice, ref job, ref jobName);
                        }
                        break;
                    case Jobs.Blacksmith:
                        {
                            if (strength >= 60 && intelligence >= 40 && dexterity >= 60)
                                Hired(ref job, ref jobRank, choice, ref shifts, ref warnings, ref jobName, ref hiredDay, currentDay);
                            else
                                Denied((Jobs)choice, ref job, ref jobName);
                        }
                        break;
                    case Jobs.Banker:
                        {
                            if (intelligence >= 300 && experience >= 1)
                                Hired(ref job, ref jobRank, choice, ref shifts, ref warnings, ref jobName, ref hiredDay, currentDay);
                            else
                                Denied((Jobs)choice, ref job, ref jobName);
                        }
                        break;
                }
                Thread.Sleep(750);
            }
            else
            {
                job = Jobs.None;
                jobName = "None";
            }
        }
        static void Hired(ref Jobs job, ref int jobRank, int choice, ref int shifts, ref int warnings, ref string jobName, ref int hiredDay, int currentDay) {
            Console.WriteLine("You have been hired! Congrats");
            job = (Jobs)choice;
            hiredDay = currentDay;
            JobName.SetName(job,1,ref jobName);
            shifts = warnings = 0;
            Job.promotion = 0;
            jobRank = 1;
        }
        static void Denied(Jobs choice, ref Jobs job, ref string jobName){
            job = Jobs.None;
            jobName = "None";
            switch(choice)
            {
                case Jobs.Fishing: { Console.WriteLine("You need more strength to catch fish.");
                                     Console.WriteLine("You need at least 40 str points and");
                                     Console.WriteLine("20 intelligence points to become");
                                     Console.WriteLine(" a fisherman.");
                    }break;
                case Jobs.Guard: { Console.WriteLine("You won't be hired as a guard if you");
                                   Console.WriteLine("are weak or if you cannot be trusted.");
                                   Console.WriteLine("Get at least 150 str points and 70");
                                   Console.WriteLine("reputation points if you want");
                                   Console.WriteLine("to become a guard.");
                    }break;
                case Jobs.Blacksmith: { Console.WriteLine("No one wants a blacksmith that");
                                        Console.WriteLine(" can't build durable gear.");
                                        Console.WriteLine("If you want to become a blacksmith,");
                                        Console.WriteLine("get at least 60 str points, 40");
                                        Console.WriteLine("intelligence points and 60");
                                        Console.WriteLine("dexterity points.");
                    }break;
                case Jobs.Banker: { Console.WriteLine("Only the smartest villagers can");
                                    Console.WriteLine("become bankers");
                                    Console.WriteLine("You won't become a banker unless you");
                                    Console.WriteLine("have at least 300 intelligence");
                                    Console.WriteLine("points and work experience.");
                    }break;
            }
            Thread.Sleep(1000);
        }
        public void Fired(ref Jobs job, ref string jobName){
            job = Jobs.None;
            jobName = "None";
            Console.WriteLine("You have been fired due to your");
            Console.WriteLine("current absence during work days!");
        }
    }
}
