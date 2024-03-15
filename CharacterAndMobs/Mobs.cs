using ConsoleDungeon.Locations;
using ConsoleDungeon.MenuAndText;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace ConsoleDungeon.CharacterAndMobs
{
    class Mobs
    {
        public int strength, defense, hp, fullHp, dropRate,raceNumber;
        enum MobType{
            DungeonMob,
            ArenaMob,
            TournamentMob,
            Assassin
        }
        public enum MobSpecies{
            Skeleton=1,
            Draugr,
            Goul,
            ReanimatedDragon,
            Warlock
        }
        public Mobs(string mobType, int stage)
        {
            int chance = 0;
            Random randomNumber = new Random();
            Enum.TryParse(mobType, out MobType type);
            switch (type)
            {
                case MobType.DungeonMob: {
                        MobChance(ref chance, stage);
                        switch((MobSpecies)chance)
                        {
                            case MobSpecies.Skeleton: {
                                    strength = randomNumber.Next(10, 40) + 20 * stage;
                                    defense = randomNumber.Next(1, 10);
                                    fullHp = 100 + 10 * stage;
                                    hp = fullHp;
                                    dropRate = 70;
                                    raceNumber = 1;
                                }break;
                            case MobSpecies.Draugr: {
                                    strength = randomNumber.Next(10, 20)* 2 + 20 * stage;
                                    defense = randomNumber.Next(15, 30);
                                    fullHp = 100 + 13 * stage;
                                    hp = fullHp;
                                    dropRate = 35;
                                    raceNumber = 2;
                                }
                                break;
                            case MobSpecies.Goul: {
                                    strength = randomNumber.Next(10, 20) * 4 + 20 * stage;
                                    defense = randomNumber.Next(40, 60);
                                    fullHp = 300 + 15 * stage;
                                    hp = fullHp;
                                    dropRate = 20;
                                    raceNumber = 3;
                                }break;
                            case MobSpecies.ReanimatedDragon: {
                                    strength = randomNumber.Next(12, 20) * 5 + 20 * stage;
                                    defense = randomNumber.Next(80, 150);
                                    fullHp = 500 + 17 * stage;
                                    hp = fullHp;
                                    dropRate = 5;
                                    raceNumber = 4;
                                }break;
                            case MobSpecies.Warlock: {
                                    strength = stage * 22;
                                    defense = randomNumber.Next(stage, 2 * stage);
                                    fullHp = stage * 35;
                                    hp = fullHp;
                                    dropRate = 100;
                                    raceNumber = 5;
                                }break;
                        }
                    }break;
                case MobType.ArenaMob: {
                        strength = 35 * stage;
                        defense = stage * 3;
                        fullHp = stage * 40;
                        hp = fullHp;
                    }break;
                case MobType.TournamentMob: {
                        strength = 20 * stage;
                        defense = stage * 4;
                        fullHp = stage * 50;
                        hp = fullHp;
                    }break;
                case MobType.Assassin: {
                        strength = stage * 50;
                        defense = randomNumber.Next(stage * 10, stage * 20);
                        fullHp = stage * 65;
                        hp = fullHp;
                        dropRate = 10;
                        raceNumber = 0;
                    }break;
            }
        }
        public void Fight(ref int energy, ref int hp, int fullHp, ref int strength, int defense, int dexterity, int mobStrength, int mobDefense, ref int mobHp, int mobFullHp, int raceNumber){
            string heroHealthBar=" ", mobHealthBar=" ";
            int criticalHit, mobCriticalHit, dodge, mobDodge,damage,mobDamage;
            Random randomNumber = new Random();
            while (hp > 0 && mobHp > 0)
            {
                criticalHit = mobCriticalHit = dodge = mobDodge = 0;
                HealthBars.SetHealthBar(ref heroHealthBar, hp, fullHp);
                HealthBars.SetHealthBar(ref mobHealthBar, mobHp, mobFullHp);
                Console.WriteLine("Your Hp:       Enemy Hp:");
                Console.WriteLine(heroHealthBar + "   " + mobHealthBar);
                Console.WriteLine(hp + "/" + fullHp + "        " + mobHp + "/" + mobFullHp);
                Console.WriteLine("*====================*");
                Console.WriteLine("| [1] Basic Attack   |");
                Console.WriteLine("| [2] Special Attack |");
                Console.WriteLine("| [3] Use Potion     |");
                Console.WriteLine("*====================*");
                Console.Write("Choose: ");
                int option = Convertion.ToInt(Console.ReadLine(), 1);
                if(option == 3)
                {
                    Console.WriteLine("Potions: ");
                    CharacterInventory.ShowInventory(32, 37, ref CharacterInventory.empty);
                    Console.WriteLine("[0] Exit");
                    Console.Write("Choose: ");
                    int option2 = Convertion.ToInt(Console.ReadLine(), 2);
                    if (option2 < 32 || option2 > 37)
                        option2 = 0;
                    if(option2 != 0)
                    {
                        if (CharacterInventory.items[option2] > 0)
                        {
                            switch (option2)
                            {
                                case 32:
                                    {
                                        Console.WriteLine("Hp increased by 100."); CharacterInventory.items[32]--;
                                        CharacterInventory.numberOfItems--; hp += 100; if (hp > fullHp) hp = fullHp;
                                    }
                                    break;
                                case 33:
                                    {
                                        Console.WriteLine("Hp increased by 500."); CharacterInventory.items[33]--;
                                        CharacterInventory.numberOfItems--; hp += 500; if (hp > fullHp) hp = fullHp;
                                    }
                                    break;
                                case 34:
                                    {
                                        Console.WriteLine("Hp increased by 1300."); CharacterInventory.items[34]--;
                                        CharacterInventory.numberOfItems--; hp += 1300; if (hp > fullHp) hp = fullHp;
                                    }
                                    break;
                                case 35:
                                    {
                                        Console.WriteLine("Energy increased by 50."); CharacterInventory.items[35]--;
                                        CharacterInventory.numberOfItems--; energy += 50; if (energy > 300) energy = 300;
                                    }
                                    break;
                                case 36:
                                    {
                                        Console.WriteLine("Energy increased by 120."); CharacterInventory.items[36]--;
                                        CharacterInventory.numberOfItems--; energy += 120; if (energy > 300) energy = 300;
                                    }
                                    break;
                                case 37:
                                    {
                                        Console.WriteLine("Energy increased by 250."); CharacterInventory.items[37]--;
                                        CharacterInventory.numberOfItems--; energy += 250; if (energy > 300) energy = 300;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("You don't have that item."); 
                            option2 = 0;
                        }
                    }
                    if(option2==0)
                    {
                        Console.WriteLine("*====================*");
                        Console.WriteLine("| [1] Basic Attack   |");
                        Console.WriteLine("| [2] Special Attack |");
                        Console.WriteLine("*====================*");
                        Console.Write("Choose: ");
                        option = Convertion.ToInt(Console.ReadLine(), 1);
                        if (option > 2 || option < 1)
                            option = 1;
                    }
                }
                if (option != 3)
                {
                    if (option > 2 || option < 1)
                        option = 1;
                    if ((option == 2 && energy >= 40) || (raceNumber == 0 && option == 2 && energy >= 100))
                        energy -= 40;
                    else if ((option == 2 && energy < 40) || (option == 2 && energy < 100 && raceNumber == 0))
                    {
                        Console.WriteLine("You don't have enough energy for a special attack.");
                        option = 1;
                        Thread.Sleep(750);
                    }
                    if (randomNumber.Next(1, 100) <= 10 + 5 * dexterity / 100)
                        criticalHit = 1;
                    if (randomNumber.Next(1, 100) <= 15)
                        mobDodge = 1;
                    damage = strength + criticalHit * 30 * strength / 100 + strength * 20 * (option - 1) / 100;
                    if (mobDodge == 0)
                    {
                        damage -= mobDefense;
                        mobDefense -= 20 * mobDefense / 100;
                        mobHp -= damage;
                        if (criticalHit == 1)
                            Console.WriteLine("Critical hit!");
                        Console.WriteLine("You dealt " + damage + " damage!");
                    }
                    else Console.WriteLine("The enemy dodged the attack!");
                }
                Thread.Sleep(750);
                if(mobHp <= 0)
                {
                    Console.WriteLine("You win!");
                    if (Dungeon.dungeonFloor % 20 != 0)
                        Program.mobKills++;
                    else Program.bossKills++;
                    Thread.Sleep(750);
                }
                else
                {
                    if (randomNumber.Next(1, 100) <= 15)
                        mobCriticalHit = 1;
                    if (randomNumber.Next(1, 100) <= 20)
                        dodge = 1;
                    mobDamage = mobStrength + mobCriticalHit * 25 * mobStrength / 100;
                    if (dodge == 0)
                    {
                        mobDamage -= defense;
                        defense -= 20 * defense / 100;
                        hp -= mobDamage;
                        if (mobCriticalHit == 1)
                            Console.WriteLine("Your enemy got a critical hit!");
                        Console.WriteLine("The enemy dealt " + mobDamage + " damage!");
                    }
                    else Console.WriteLine("You dodged the attack!");
                }
                
            }
        }
        void MobChance(ref int chance, int stage){
            Random randomNumber = new Random();
            int pick = randomNumber.Next(1, 100);
            if (stage <= 19)
                chance = 1;
            else if (stage > 20 && stage <= 39)
            {
                if (pick <= 20)
                    chance = 1;
                else chance = 2;
            }
            else if (stage > 40 && stage <= 59)
            {
                if (pick <= 5)
                    chance = 1;
                else if (pick > 5 && pick <= 35)
                    chance = 2;
                else if (pick > 35)
                    chance = 3;
            }
            else if (stage > 60 && stage <= 79)
            {
                if (pick <= 10)
                    chance = 2;
                else if (pick > 10 && pick <= 80)
                    chance = 3;
                else if (pick > 80)
                    chance = 4;
            }
            else if (stage > 80 && stage <= 99)
            {
                if (pick <= 30)
                    chance = 3;
                else chance = 4;
            }
            else if (stage % 20 == 0)
                chance = 5;
        }
    }
}
