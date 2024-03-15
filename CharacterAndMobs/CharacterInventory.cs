using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ConsoleDungeon.Locations;
using ConsoleDungeon.MenuAndText;

namespace ConsoleDungeon.CharacterAndMobs
{
    class CharacterInventory
    {
        public enum Items
        {
            bones = 1, sticks, knives, rustySwords, sharpSwords, poisonedDaggers, crossBow, warHammer, warAxe, excaliber, tyrfing,
            ironHelmet, platinumHelmet, cobaltHelmet, meteorHelmet,
            ironChestPlate, platinumChestPlate, cobaltChestPlate, meteorChestPlate,
            ironLeggins, platinumLeggins, cobaltLeggins, meteorLeggins,
            ironBoots, platinumBoots, cobaltBoots, meteorBoots,
            ironGloves, platinumGloves, cobaltGloves, meteorGloves,
            minHpPotion, medHpPotion, maxHpPotion, minEngPotion, medEngPotion, maxEngPotion, allStatsPotion
        }
        public static int[] items = new int[45]; // 1 -> 11 weapons; 12 -> 31 armour; 32 -> 38 potions;
        public static int[] itemsStats = { 0, 1, 1, 5, 35, 70, 200, 500, 700, 1000, 1500, 2200, 5, 20, 50, 80, 25, 60, 90, 150, 10, 35, 60, 90, 5, 20, 40, 70, 5, 15, 35, 65 };
        public static string[] itemsName = new string[45];
        public static int capacity = 50, numberOfItems=0;
        public static bool empty = true;
        static int slot1Power = 0, slot2Power = 0, helmetPower = 0, chestplatePower = 0, legginsPower = 0, bootsPower = 0, glovesPower = 0;
        public static void ShowInventory(int start, int stop,ref bool empty)
        {
            empty = true;
            int i;
            for (i = start; i <= stop; i++)
                if(items[i]>0)
                   empty = false;
            if (!empty)
            {
                Console.WriteLine("*================================*");
                for (i = start; i <= stop; i++)
                {
                    if (items[i] > 0)
                    {
                        if (i < 10)
                            Console.Write("| [ " + i + "] " + itemsName[i] + ": ");
                        else
                            Console.Write("| [" + i + "] " + itemsName[i] + ": ");
                        if (items[i] < 10)
                            Console.WriteLine(items[i] + "   |");
                        else if (items[i] >= 10 && items[i] < 100)
                            Console.WriteLine(items[i] + "  |");
                        else if (items[i] >= 100)
                            Console.WriteLine(items[i] + " |");
                    }
                }
                Console.WriteLine("*================================*\n");
                Console.WriteLine("Items: " + numberOfItems + "/" + capacity);
            }
            else if (empty && start == 1 && stop == 38)
                 Console.WriteLine("You don't have any items.");
        }
        public static void PickItem(ref int numberOfItems, int capacity, int floor, int drop, int drop2){
            if(numberOfItems == capacity || (numberOfItems == capacity - 1 && (floor == 40 || floor == 80 )))
            {
                if(floor!=40 && floor!=80)
                {
                    Console.WriteLine("Your inventory is full!");
                    Console.WriteLine("Options:");
                    Console.WriteLine("*=====================================*");
                    Console.WriteLine("|[1] Don't pick the item              |");
                    Console.WriteLine("|[2] Drop an item from your inventory |");
                    Console.WriteLine("*=====================================*\n");
                    Console.Write("Choose: ");
                    int option = Convertion.ToInt(Console.ReadLine(),1);
                    if (option > 2 || option < 1)
                        option = 1;
                    if(option == 2)
                    {
                        ShowInventory(1,38, ref empty);
                        Console.Write("Choose what to drop: ");
                        int option2 = Convertion.ToInt(Console.ReadLine(), 2);
                        if(items[option2]!=0)
                        {
                            items[option2]--;
                            items[drop]++;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Your inventory is full!");
                    Console.WriteLine("Space used: " + numberOfItems + "/" + capacity);
                    Console.WriteLine("Options:");
                    Console.WriteLine("*======================================*");
                    Console.WriteLine("|[1] Don't pick the items               |");
                    Console.WriteLine("|[2] Drop something from your inventory |");
                    Console.WriteLine("|[3] Pick only one item                 |");
                    Console.WriteLine("*======================================*\n");
                    Console.Write("Choose: ");
                    int option = Convertion.ToInt(Console.ReadLine(), 1);
                    if (option > 3 || option < 1)
                        option = 1;
                    if (option == 2)
                    {
                        ShowInventory(1,38,ref empty);
                        Console.Write("Choose what to drop: ");
                        int option2 = Convertion.ToInt(Console.ReadLine(), 2);
                        if (items[option2] != 0 || (option2 == 0 && numberOfItems < capacity))
                        {
                            items[option2]--;
                            items[drop]++;
                            if(option2 == 0)
                            {
                                items[option2] = 0;
                                numberOfItems++;
                            }
                        }
                        Thread.Sleep(750);
                        ShowInventory(1,38,ref empty);
                        Console.Write("Choose what to drop: ");
                        option2 = Convertion.ToInt(Console.ReadLine(), 2);
                        if (items[option2] != 0 || (option2 == 0 && numberOfItems < capacity) )
                        {
                            items[option2]--;
                            items[drop2]++;
                            if (option2 == 0)
                            {
                                items[option2] = 0;
                                numberOfItems++;
                            }
                        }
                    }
                    else if (option == 3)
                    {
                        if (numberOfItems == capacity)
                            Console.WriteLine("You don't have enough space.");
                        else
                        {
                            if (floor == 40)
                            {
                                Console.WriteLine("[1] Poisoned Dagger");
                                Console.WriteLine("[2] Platinum Helmet");
                                Console.Write("Choose: ");
                                int choice = Convertion.ToInt(Console.ReadLine(), 1);
                                if (choice < 1 || choice > 2)
                                    choice = 1;
                                if (choice == 1) items[6]++;
                                else items[13]++;
                            }
                            else
                            {
                                Console.WriteLine("[1] War Axe");
                                Console.WriteLine("[2] Meteorite Helmet");
                                Console.Write("Choose: ");
                                int choice = Convertion.ToInt(Console.ReadLine(), 1);
                                if (choice < 1 || choice > 2)
                                    choice = 1;
                                if (choice == 1) items[9]++;
                                else items[15]++;
                            }
                            numberOfItems++;
                        }
                    }
                }
            }
            else
            {
                if(floor != 40 && floor != 80)
                {
                    numberOfItems++;
                    items[drop]++;
                }
                else
                {
                    numberOfItems += 2;
                    items[drop]++;
                    items[drop2]++;
                }
            }
        }
        public static void UseItem(ref int strength, ref int defense, ref int hp, ref int fullHp, ref int eng, ref int dexterity, ref int slot1, ref int slot2, ref int helmet, ref int chestplate, ref int leggins, ref int boots, ref int gloves ,ref  bool empty, int weaponMastery, int level){
            int choice,choice2 = 0;
            Console.WriteLine("*~~~~~~~~~~~~~~~~~~~~~~~~*");
            Console.WriteLine("| [1] Use item           |");
            Console.WriteLine("| [2] Change weapons     |");
            Console.WriteLine("| [3] Change armour      |");
            Console.WriteLine("| [0] Exit               |");
            Console.WriteLine("*~~~~~~~~~~~~~~~~~~~~~~~~*");
            Console.Write("Choose: ");
            choice = Convertion.ToInt(Console.ReadLine(), 2);
            if(choice == 1)
            {
                ShowInventory(32, 38, ref empty);
                if (!empty)
                {
                    Console.WriteLine("[0] Exit");
                    Console.Write("Choose a potion: ");
                    choice2 = Convertion.ToInt(Console.ReadLine(), 2);
                    if (choice2 < 32 || choice2 > 38)
                        choice2 = 0;
                    if (items[choice2] == 0)
                    {
                        if(choice2 != 0)
                           Console.WriteLine("Not enough items of that kind.");
                    }
                    else
                    {
                        switch ((Items)choice2)
                        {
                            case Items.minHpPotion: { hp += 100; if (hp > fullHp) hp = fullHp; items[32]--; } break;
                            case Items.medHpPotion: { hp += 500; if (hp > fullHp) hp = fullHp; items[33]--; } break;
                            case Items.maxHpPotion: { hp += 1300; if (hp > fullHp) hp = fullHp; items[34]--; } break;
                            case Items.minEngPotion: { eng += 50; if (eng > 300) eng = 300; items[35]--; } break;
                            case Items.medEngPotion: { eng += 120; if (eng > 300) eng = 300; items[36]--; } break;
                            case Items.maxEngPotion: { eng += 250; if (eng > 300) eng = 300; items[37]--; } break;
                            case Items.allStatsPotion:
                                {
                                    strength += 10;
                                    defense += 5;
                                    fullHp += 10;
                                    hp += 10;
                                    dexterity += 10;
                                    items[38]--;
                                }
                                break;
                        }
                    }
                }
                else Console.WriteLine("You don't have any potions.");
            }
            else if(choice == 2)
            {
                ShowInventory(1, 11, ref empty);
                if (!empty)
                {
                    Console.WriteLine("[0] Exit");
                    int ok = 0;
                    while (ok == 0)
                    {
                        Console.Write("Choose a weapon: ");
                        choice2 = Convertion.ToInt(Console.ReadLine(), 2);
                        if (choice2 >= 4 && choice2 <= 11 && items[choice2] > 0)
                        {
                            if (choice2 == 4 && level < 3)
                                Console.WriteLine("Required level: 3");
                            else if(choice2 == 4 && level >= 3) ok = 1;
                            if (choice2 == 5 && level < 7)
                                Console.WriteLine("Required level: 7");
                            else if(choice2 == 5 && level >= 7)ok = 1;
                            if (choice2 == 6 && level < 15)
                                Console.WriteLine("Required level: 15");
                            else if(choice2 == 6 && level >= 15) ok = 1;
                            if (choice2 == 7 && level < 30)
                                Console.WriteLine("Required level: 30");
                            else if (choice2 == 6 && level >= 30) ok = 1;
                            if (choice2 == 8 && level < 40)
                                Console.WriteLine("Required level: 40");
                            else if (choice2 == 6 && level >= 40) ok = 1;
                            if (choice2 == 9 && level < 45)
                                Console.WriteLine("Required level: 45");
                            else if (choice2 == 6 && level >= 45) ok = 1;
                            if ((choice2 == 10 || choice == 11) && level < 50)
                                Console.WriteLine("Required level: 50");
                            else if ((choice2 == 10 || choice == 11) && level >= 50) ok = 1;

                        }
                        else ok = 1;
                    }
                    if (choice2 < 1 || choice2 > 11)
                        choice2 = 0;
                    if (items[choice2] == 0)
                    {
                        if (choice2 != 0)
                            Console.WriteLine("You don't have that weapon in your inventory.");
                    }
                    else
                    {
                        Console.WriteLine("\nSlot1: " + itemsName[slot1]);
                        if (slot2 != -1)
                            Console.WriteLine("Slot2: " + itemsName[slot2]);
                        Console.Write("Choose slot: ");
                        int choice3 = Convertion.ToInt(Console.ReadLine(), 1);
                        if (choice3 < 1 || choice3 > 2)
                            choice3 = 1;
                        if (choice3 == 2 && slot2 == -1)
                        {
                            Console.WriteLine("That slot is locked.");
                            choice3 = 1;
                            Thread.Sleep(500);
                        }
                        if (choice3 == 1) EquipWeapon(ref slot1, ref strength, ref slot1Power, choice2, weaponMastery);
                        else
                        {
                            if (slot1 <= 6)
                            {
                                if (choice2 > 6)
                                {
                                    Console.WriteLine("That weapon is to heavy to hold it in a single hand.");
                                    Thread.Sleep(1000);
                                }
                                else
                                    EquipWeapon(ref slot2, ref strength, ref slot2Power, choice2, weaponMastery);
                            }
                            else
                            {
                                Console.WriteLine("Your first weapon is too heavy to hold it in a single hand.");
                                Thread.Sleep(1000);
                            }
                        }
                    }
                }
                else Console.WriteLine("You don't have any weapons.");
            }
            else if(choice == 3)
            {
                ShowInventory(12, 31, ref empty);
                if (!empty)
                {
                    Console.Write("Change armour(y/n) ");
                    while(Console.ReadLine() == "y" )
                    {
                        Console.WriteLine("[1] Helmet    : " + itemsName[helmet]);
                        Console.WriteLine("[2] Chestplate: " + itemsName[chestplate]);
                        Console.WriteLine("[3] Leggins   : " + itemsName[leggins]);
                        Console.WriteLine("[4] Boots     : " + itemsName[boots]);
                        Console.WriteLine("[5] Gloves    : " + itemsName[gloves]);
                        Console.WriteLine("[0] Exit");
                        Console.Write("Choose: ");
                        choice2 = Convertion.ToInt(Console.ReadLine(), 2);
                        if (choice2 < 0 || choice2 > 5)
                            choice2 = 0;
                        if(choice2 != 0)
                        {
                            int choice3 = 0;
                            bool empty1 = true;
                            int ok = 0;
                            switch (choice2)
                            {
                                case 1: {
                                        CharacterInventory.ShowInventory(12, 15, ref empty1);
                                        if (!empty1)
                                        {
                                            Console.Write("Choose a helmet: ");
                                            while (ok == 0)
                                            {
                                                choice3 = Convertion.ToInt(Console.ReadLine(), 2);
                                                if (choice3 == 12 && level < 5)
                                                    Console.WriteLine("Required level: 5");
                                                else if (choice3 == 12 && level >= 5) ok = 1;
                                                if (choice3 == 13 && level < 25)
                                                    Console.WriteLine("Required level: 25");
                                                else if (choice3 == 13 && level >= 25) ok = 1;
                                                if (choice3 == 14 && level < 50)
                                                    Console.WriteLine("Required level: 50");
                                                else if (choice3 == 14 && level >= 50) ok = 1;
                                                if (choice3 == 15 && level < 70)
                                                    Console.WriteLine("Required level: 70");
                                                else if (choice3 == 15 && level >= 70) ok = 1;
                                                if (choice3 < 12 || choice3 > 15) ok = 1;
                                            }
                                            if (choice3 < 12 || choice3 > 15)
                                                choice3 = 0;
                                            if (items[choice3] == 0 || choice3 == 0)
                                            {
                                                if (choice3 != 0)
                                                    Console.WriteLine("You don't have that item.");
                                            }
                                            else EquipArmour(choice2, choice3, ref defense, ref helmet, ref chestplate, ref leggins, ref boots, ref gloves, ref helmetPower);
                                        }
                                        else Console.WriteLine("You don't have any helmets.");
                                    }break;
                                case 2: {
                                        CharacterInventory.ShowInventory(16, 19, ref empty1);
                                        if (!empty1)
                                        {
                                            Console.Write("Choose a chestplate: ");
                                            while (ok == 0)
                                            {
                                                choice3 = Convertion.ToInt(Console.ReadLine(), 2);
                                                if (choice3 == 16 && level < 5)
                                                    Console.WriteLine("Required level: 5");
                                                else if (choice3 == 16 && level >= 5) ok = 1;
                                                if (choice3 == 17 && level < 25)
                                                    Console.WriteLine("Required level: 25");
                                                else if (choice3 == 17 && level >= 25) ok = 1;
                                                if (choice3 == 18 && level < 50)
                                                    Console.WriteLine("Required level: 50");
                                                else if (choice3 == 18 && level >= 50) ok = 1;
                                                if (choice3 == 19 && level < 70)
                                                    Console.WriteLine("Required level: 70");
                                                else if (choice3 == 19 && level >= 70) ok = 1;
                                                if (choice3 < 16 || choice3 > 19) ok = 1;
                                            }
                                            if (choice3 < 16 || choice3 > 19)
                                                choice3 = 0;
                                            if (items[choice3] == 0 || choice3 == 0)
                                            {
                                                if (choice3 != 0)
                                                    Console.WriteLine("You don't have that item.");
                                            }
                                            else EquipArmour(choice2, choice3, ref defense, ref helmet, ref chestplate, ref leggins, ref boots, ref gloves, ref chestplatePower);
                                        }
                                        else Console.WriteLine("You don't have any chestplates.");
                                    } break;
                                case 3: {
                                        CharacterInventory.ShowInventory(20, 23, ref empty1);
                                        if (!empty1)
                                        {
                                            Console.Write("Choose some leggins: ");
                                            while (ok == 0)
                                            {
                                                choice3 = Convertion.ToInt(Console.ReadLine(), 2);
                                                if (choice3 == 20 && level < 5)
                                                    Console.WriteLine("Required level: 5");
                                                else if (choice3 == 20 && level >= 5) ok = 1;
                                                if (choice3 == 21 && level < 25)
                                                    Console.WriteLine("Required level: 25");
                                                else if (choice3 == 21 && level >= 25) ok = 1;
                                                if (choice3 == 22 && level < 50)
                                                    Console.WriteLine("Required level: 50");
                                                else if (choice3 == 22 && level >= 50) ok = 1;
                                                if (choice3 == 23 && level < 70)
                                                    Console.WriteLine("Required level: 70");
                                                else if (choice3 == 23 && level >= 70) ok = 1;
                                                if (choice3 < 20 || choice3 > 23) ok = 1;
                                            }
                                            if (choice3 < 20 || choice3 > 23)
                                                choice3 = 0;
                                            if (items[choice3] == 0 || choice3 == 0)
                                            {
                                                if (choice3 != 0)
                                                    Console.WriteLine("You don't have that item.");
                                            }
                                            else EquipArmour(choice2, choice3, ref defense, ref helmet, ref chestplate, ref leggins, ref boots, ref gloves, ref legginsPower);
                                        }
                                        else Console.WriteLine("You don't have any leggins.");
                                    } break;
                                case 4: {
                                        CharacterInventory.ShowInventory(24, 27, ref empty1);
                                        if (!empty1)
                                        {
                                            Console.Write("Choose some boots: ");
                                            while (ok == 0)
                                            {
                                                choice3 = Convertion.ToInt(Console.ReadLine(), 2);
                                                if (choice3 == 24 && level < 5)
                                                    Console.WriteLine("Required level: 5");
                                                else if (choice3 == 24 && level >= 5) ok = 1;
                                                if (choice3 == 25 && level < 25)
                                                    Console.WriteLine("Required level: 25");
                                                else if (choice3 == 25 && level >= 25) ok = 1;
                                                if (choice3 == 26 && level < 50)
                                                    Console.WriteLine("Required level: 50");
                                                else if (choice3 == 26 && level >= 50) ok = 1;
                                                if (choice3 == 27 && level < 70)
                                                    Console.WriteLine("Required level: 70");
                                                else if (choice3 == 27 && level >= 70) ok = 1;
                                                if (choice3 < 24 || choice3 > 27) ok = 1;
                                            }
                                            if (choice3 < 24 || choice3 > 27)
                                                choice3 = 0;
                                            if (items[choice3] == 0 || choice3 == 0)
                                            {
                                                if (choice3 != 0)
                                                    Console.WriteLine("You don't have that item.");
                                            }
                                            else EquipArmour(choice2, choice3, ref defense, ref helmet, ref chestplate, ref leggins, ref boots, ref gloves, ref bootsPower);
                                        }
                                        else Console.WriteLine("You don't have any boots.");
                                    } break;
                                case 5: {
                                        CharacterInventory.ShowInventory(28, 31, ref empty1);
                                        if (!empty1)
                                        {
                                            Console.Write("Choose some gloves: ");
                                            while (ok == 0)
                                            {
                                                choice3 = Convertion.ToInt(Console.ReadLine(), 2);
                                                if (choice3 == 28 && level < 5)
                                                    Console.WriteLine("Required level: 5");
                                                else if (choice3 == 28 && level >= 5) ok = 1;
                                                if (choice3 == 29 && level < 25)
                                                    Console.WriteLine("Required level: 25");
                                                else if (choice3 == 29 && level >= 25) ok = 1;
                                                if (choice3 == 30 && level < 50)
                                                    Console.WriteLine("Required level: 50");
                                                else if (choice3 == 30 && level >= 50) ok = 1;
                                                if (choice3 == 31 && level < 70)
                                                    Console.WriteLine("Required level: 70");
                                                else if (choice3 == 31 && level >= 70) ok = 1;
                                                if (choice3 < 28 || choice3 > 31) ok = 1;
                                            }
                                            if (choice3 < 28 || choice3 > 31)
                                                choice3 = 0;
                                            if (items[choice3] == 0 || choice3 == 0)
                                            {
                                                if (choice3 != 0)
                                                    Console.WriteLine("You don't have that item.");
                                            }
                                            else EquipArmour(choice2, choice3, ref defense, ref helmet, ref chestplate, ref leggins, ref boots, ref gloves, ref glovesPower);
                                        }
                                        else Console.WriteLine("You don't have any gloves.");
                                    } break;
                            }
                        }
                        Thread.Sleep(500);
                        Console.Write("Change armour(y/n) ");
                    }
                }
                else Console.WriteLine("You don't have armour in your inventory.");
            }
            Thread.Sleep(750);
        }
        static void EquipWeapon(ref int slot, ref int strength, ref int slotPower, int choice, int weaponMastery){
            strength -= slotPower;
            items[slot]++;
            slot = choice;
            items[choice]--;
            slotPower = itemsStats[choice] + 15 * itemsStats[choice] / 100 * weaponMastery;
            strength += slotPower;
        }
        static void EquipArmour(int choice, int choice3, ref int defense, ref int helmet, ref int chestplate, ref int leggins, ref int boots, ref int gloves, ref int itemPower){
            defense -= itemPower;
            switch(choice)
            {
                case 1: {
                        items[helmet]++;
                        helmet = choice3;
                        helmetPower = itemsStats[choice3];
                        defense += helmetPower;
                    }break;
                case 2:
                    {
                        items[chestplate]++;
                        chestplate = choice3;
                        chestplatePower = itemsStats[choice3];
                        defense += chestplatePower;
                    }
                    break;
                case 3:
                    {
                        items[leggins]++;
                        leggins = choice3;
                        legginsPower = itemsStats[choice3];
                        defense += legginsPower;
                    }
                    break;
                case 4:
                    {
                        items[boots]++;
                        boots = choice3;
                        bootsPower = itemsStats[choice3];
                        defense += bootsPower;
                    }
                    break;
                case 5:
                    {
                        items[gloves]++;
                        gloves = choice3;
                        glovesPower = itemsStats[choice3];
                        defense += glovesPower;
                    }
                    break;
            }
            items[choice3]--;
        }
        public static void UnequipItems(ref int strength, ref int defense, ref int slot1, ref int slot2, int dualWielding, ref int helmet, ref int chestplate, ref int leggins, ref int boots, ref int gloves) {
            int choice = 0;
            Console.WriteLine("*Your Loadout: ");
            Console.WriteLine("[1] Helmet    : " + itemsName[helmet]);
            Console.WriteLine("[2] Chestplate: " + itemsName[chestplate]);
            Console.WriteLine("[3] Leggins   : " + itemsName[leggins]);
            Console.WriteLine("[4] Boots     : " + itemsName[boots]);
            Console.WriteLine("[5] Gloves    : " + itemsName[gloves]);
            Console.WriteLine("[6] Weapon1   : " + itemsName[slot1]);
            if (dualWielding == 1)
                Console.WriteLine("[7] Weapon2   : " + itemsName[slot2]);
            Console.WriteLine("Unequip item(y/n): ");
            while (Console.ReadLine() == "y")
            {
                Console.Write("Choose item (press 0 for none): ");
                choice = Convertion.ToInt(Console.ReadLine(), 2);
                if (((choice < 1 || choice > 7) && dualWielding == 1) || ((choice < 1 || choice > 6) && dualWielding == 0))
                    choice = 0;
                switch(choice)
                {
                    case 1: {
                            if (helmet == 0)
                                Console.WriteLine("You have no helmet eqquiped.");
                            else
                            {
                                items[helmet]++;
                                defense -= itemsStats[helmet];
                                helmet = CharacterInventory.helmetPower = 0;
                            }
                        }break;
                    case 2:
                        {
                            if (chestplate == 0)
                                Console.WriteLine("You have no chestplate eqquiped.");
                            else
                            {
                                items[chestplate]++;
                                defense -= itemsStats[chestplate];
                                chestplate = CharacterInventory.chestplatePower = 0;
                            }
                        }break;
                    case 3:
                        {
                            if (leggins == 0)
                                Console.WriteLine("You have no leggins eqquiped.");
                            else
                            {
                                items[leggins]++;
                                defense -= itemsStats[leggins];
                                leggins = CharacterInventory.legginsPower = 0;
                            }
                        }break;
                    case 4:
                        {
                            if (boots == 0)
                                Console.WriteLine("You have no boots eqquiped.");
                            else
                            {
                                items[boots]++;
                                defense -= itemsStats[boots];
                                boots = CharacterInventory.bootsPower = 0;
                            }
                        }break;
                    case 5:
                        {
                            if (gloves == 0)
                                Console.WriteLine("You have no gloves eqquiped.");
                            else
                            {
                                items[gloves]++;
                                defense -= itemsStats[gloves];
                                gloves = CharacterInventory.glovesPower = 0;
                            }
                        }break;
                    case 6:
                        {
                            if (slot1 == 0)
                                Console.WriteLine("You have no weapon eqquiped on the first slot.");
                            else
                            {
                                items[slot1]++;
                                strength -= CharacterInventory.slot1Power;
                                slot1 = CharacterInventory.slot1Power = 0;
                            }
                        }break;
                    case 7:
                        {
                            if (slot2 == 0)
                                Console.WriteLine("You have no weapon eqquiped on the second slot.");
                            else
                            {
                                items[slot2]++;
                                strength -= CharacterInventory.slot2Power;
                                slot2 = CharacterInventory.slot2Power = 0;
                            }
                        }break;
                }
                Thread.Sleep(500);
                Console.WriteLine("[1] Helmet    : " + itemsName[helmet]);
                Console.WriteLine("[2] Chestplate: " + itemsName[chestplate]);
                Console.WriteLine("[3] Leggins   : " + itemsName[leggins]);
                Console.WriteLine("[4] Boots     : " + itemsName[boots]);
                Console.WriteLine("[5] Gloves    : " + itemsName[gloves]);
                Console.WriteLine("[6] Weapon1   : " + itemsName[slot1]);
                if (dualWielding == 1)
                    Console.WriteLine("[7] Weapon2   : " + itemsName[slot2]);
                Console.WriteLine("Unequip item(y/n): ");
            }
        }
    }
}
