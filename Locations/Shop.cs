using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using ConsoleDungeon.CharacterAndMobs;
using ConsoleDungeon.MenuAndText;

namespace ConsoleDungeon.Locations
{
    class Shop
    {
        public static int lastRestockDay = 1;
                                     // same order as inventory
        public static int[] potionsPrice  = {0, 60, 150, 400, 45, 120, 330, 300};
        public static int[] potionsOnSale = {0, 3, 1, 0, 3, 2, 0, 0};
        public static int[] weaponsPrice  = {0, 10, 30, 100, 300, 800, 1750, 2500, 3300, 6000, 4000};
        public static int[] weaponsOnSale = {0, 10, 4, 2, 0, 0, 0, 0, 0, 0, 0};

                                    // ordered by armour sets
        public static int[] armourPrice  = {0, 120, 250, 100, 75, 60, 350, 500, 320, 200, 190, 750, 1000, 730, 400, 380, 1000, 1500, 1000, 700, 680};
        public static int[] armourOnSale = {0, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1};

        enum Shops { PotionShop = 1, WeaponShop, ArmourShop};

        public static void ItemsRestock(){
            Random randomNumber = new Random();
            int chance;
            potionsOnSale[1] = randomNumber.Next(2, 5);
            potionsOnSale[2] = randomNumber.Next(1, 3);
            chance = randomNumber.Next(1, 100);
            if (Dungeon.dungeonFloor >= 20 && chance <= 40) potionsOnSale[3] = randomNumber.Next(1, 2);
            else potionsOnSale[4] = 0;
            potionsOnSale[4] = randomNumber.Next(1, 4);
            potionsOnSale[5] = randomNumber.Next(0, 3);
            chance = randomNumber.Next(1, 100);
            if (Dungeon.dungeonFloor >= 20 && chance <= 35) potionsOnSale[6] = randomNumber.Next(1, 2);
            else potionsOnSale[6] = 0;
            chance = randomNumber.Next(1, 100);
            if (Dungeon.dungeonFloor >= 30 && chance <= 30) potionsOnSale[7] = 1;
            else potionsOnSale[7] = 0;
            weaponsOnSale[1] = 10;
            weaponsOnSale[2] = randomNumber.Next(1,5);
            weaponsOnSale[3] = randomNumber.Next(1,3);
            chance = randomNumber.Next(1, 100);
            if (chance <= 45) weaponsOnSale[4] = randomNumber.Next(1, 3);
            else weaponsOnSale[4] = 0;
            chance = randomNumber.Next(1, 100);
            if (chance <= 40) weaponsOnSale[5] = randomNumber.Next(1, 2);
            else weaponsOnSale[5] = 0;
            chance = randomNumber.Next(1, 100);
            if (chance <= 30) weaponsOnSale[6] = 1;
            else weaponsOnSale[6] = 0;
            chance = randomNumber.Next(1, 100);
            if (chance <= 20) weaponsOnSale[7] = 1;
            else weaponsOnSale[7] = 0;
            chance = randomNumber.Next(1, 100);
            if (chance <= 10) weaponsOnSale[8] = 1;
            else weaponsOnSale[8] = 0;
            chance = randomNumber.Next(1, 100);
            if (chance <= 5 && Dungeon.dungeonFloor >= 50 && CharacterInventory.items[10] == 0) weaponsOnSale[9] = 1;
            else weaponsOnSale[9] = 0;
            chance = randomNumber.Next(1, 100);
            if (chance <= 15 && Dungeon.dungeonFloor >= 50 && CharacterInventory.items[11] == 0) weaponsOnSale[10] = 1;
            else weaponsOnSale[10] = 0;
            for(int i = 1; i <= 4; i++)
            {
                for(int j = 1; j<=5; j++)
                {
                    chance = randomNumber.Next(1, 100);
                    switch(i)
                    {
                        case 1: { if (chance <= 70) armourOnSale[(i - 1) * 5 + j] = 1; else armourOnSale[(i - 1) * 5 + j] = 0; } break;
                        case 2: { if (chance <= 45) armourOnSale[(i - 1) * 5 + j] = 1; else armourOnSale[(i - 1) * 5 + j] = 0; } break;
                        case 3: { if (chance <= 25) armourOnSale[(i - 1) * 5 + j] = 1; else armourOnSale[(i - 1) * 5 + j] = 0; } break;
                        case 4: { if (chance <= 15) armourOnSale[(i - 1) * 5 + j] = 1; else armourOnSale[(i - 1) * 5 + j] = 0; } break;
                    }
                }
            }
 
        }
        public static void BuyItems(ref int gold, Character.Jobs job){
            int shop = 0, choice = 0, choice2 = 100, price = 0;
            Console.WriteLine("*===================*");
            Console.WriteLine("| [1] Potions Shop  |");
            Console.WriteLine("| [2] Weapons Shop  |");
            Console.WriteLine("| [3] Armour Shop   |");
            Console.WriteLine("| [0] Exit          |");
            Console.WriteLine("*===================*\n");
            Console.Write("Choose: ");
            shop = Convertion.ToInt(Console.ReadLine(), 2);
            if (shop < 1 || shop > 3)
                shop = 0;
            if (shop != 0)
            {
                switch ((Shops)shop)
                {
                    case Shops.PotionShop:{
                            Menu.PotionShopPrint();
                        }break;
                    case Shops.WeaponShop:{
                            Menu.WeaponShopPrint();
                        }break;
                    case Shops.ArmourShop:{
                            Menu.ArmourShopPrint(job);
                        }break;
                }
                Console.WriteLine("[0] Buy Nothing");
                Console.Write("Choose: ");
                choice = Convertion.ToInt(Console.ReadLine(), 2);
                switch((Shops)shop)
                {
                    case Shops.PotionShop: { if (choice < 1 || choice > 7) choice = 0; }break;
                    case Shops.WeaponShop: { if (choice < 1 || choice > 10) choice = 0; } break;
                    case Shops.ArmourShop: { if (choice < 1 || choice > 20) choice = 0; } break;
                }
                switch((Shops)shop)
                {
                     case Shops.PotionShop: {
                            if (potionsOnSale[choice] < 1)
                            {
                                if(choice != 0)
                                   Console.WriteLine("That item doesn't exist in this shop.");
                            }
                            else
                            {
                                while (choice2 > potionsOnSale[choice])
                                {
                                    Console.Write("Choose quantity: ");
                                    choice2 = Convertion.ToInt(Console.ReadLine(), 2);
                                    if (choice2 < 0)
                                        choice2 = 0;
                                    if (choice2 > potionsOnSale[choice])
                                        Console.WriteLine("Not enough items on stock.");
                                }
                                if (choice2 != 0)
                                {
                                    price = potionsPrice[choice] * choice2;
                                    Console.WriteLine("Price: " + price);
                                    Console.Write("Procced(y/n) ");
                                    if (Console.ReadLine() == "y" && gold >= price && CharacterInventory.numberOfItems + choice2 <= CharacterInventory.capacity)
                                    {
                                        switch (choice)
                                        {
                                            case 1: { CharacterInventory.items[32] += choice2; potionsOnSale[1] -= choice2; } break;
                                            case 2: { CharacterInventory.items[33] += choice2; potionsOnSale[2] -= choice2; } break;
                                            case 3: { CharacterInventory.items[34] += choice2; potionsOnSale[3] -= choice2; } break;
                                            case 4: { CharacterInventory.items[35] += choice2; potionsOnSale[4] -= choice2; } break;
                                            case 5: { CharacterInventory.items[36] += choice2; potionsOnSale[5] -= choice2; } break;
                                            case 6: { CharacterInventory.items[37] += choice2; potionsOnSale[6] -= choice2; } break;
                                            case 7: { CharacterInventory.items[38] += choice2; potionsOnSale[7] -= choice2; } break;
                                        }
                                        gold -= price;
                                        CharacterInventory.numberOfItems += choice2;
                                        Console.WriteLine("Your transaction was successfull.");
                                    }
                                    else
                                    {
                                        if (gold < price)
                                            Console.WriteLine("Not enough gold.");
                                        if (CharacterInventory.capacity < CharacterInventory.numberOfItems + choice2)
                                            Console.WriteLine("Not enough space in your inventory.");
                                    }
                                }
                            }
                        }break;
                    case Shops.WeaponShop: {
                            if (weaponsOnSale[choice] < 1)
                            {
                                if(choice !=0)
                                   Console.WriteLine("That weapon is not on stock.");
                            }
                            else
                            {
                                while (choice2 > weaponsOnSale[choice])
                                {
                                    Console.Write("Choose quantity: ");
                                    choice2 = Convertion.ToInt(Console.ReadLine(), 2);
                                    if (choice2 < 0)
                                        choice2 = 0;
                                    if (choice2 > weaponsOnSale[choice])
                                        Console.WriteLine("Not enough items on stock.");
                                }
                                if (choice2 != 0)
                                {
                                    price = weaponsPrice[choice] * choice2;
                                    Console.WriteLine("Price: " + price);
                                    Console.Write("Procced(y/n) ");
                                    if (Console.ReadLine() == "y" && gold >= price && CharacterInventory.numberOfItems + choice2 <= CharacterInventory.capacity)
                                    {
                                        switch (choice)
                                        {
                                            case 1: { CharacterInventory.items[2] += choice2; weaponsOnSale[1] -= choice2; } break;
                                            case 2: { CharacterInventory.items[3] += choice2; weaponsOnSale[2] -= choice2; } break;
                                            case 3: { CharacterInventory.items[4] += choice2; weaponsOnSale[3] -= choice2; } break;
                                            case 4: { CharacterInventory.items[5] += choice2; weaponsOnSale[4] -= choice2; } break;
                                            case 5: { CharacterInventory.items[6] += choice2; weaponsOnSale[5] -= choice2; } break;
                                            case 6: { CharacterInventory.items[7] += choice2; weaponsOnSale[6] -= choice2; } break;
                                            case 7: { CharacterInventory.items[8] += choice2; weaponsOnSale[7] -= choice2; } break;
                                            case 8: { CharacterInventory.items[9] += choice2; weaponsOnSale[8] -= choice2; } break;
                                            case 9: { CharacterInventory.items[10] += choice2; weaponsOnSale[9] -= choice2; } break;
                                            case 10:
                                                {
                                                    CharacterInventory.items[11] += choice2; weaponsOnSale[10] -= choice2; if (Program.daysLeft > 14) Program.daysLeft = 14;
                                                    Console.WriteLine("You have been cursed!");
                                                    Console.WriteLine(" You will die in the next 14 days!");
                                                }
                                                break;
                                        }
                                        gold -= price;
                                        CharacterInventory.numberOfItems += choice2;
                                        Console.WriteLine("Your transaction was successful.");
                                    }
                                    else
                                    {
                                        if (gold < price)
                                            Console.WriteLine("Not enough gold.");
                                        if (CharacterInventory.capacity < CharacterInventory.numberOfItems + choice2)
                                            Console.WriteLine("Not enough space in your inventory.");
                                    }
                                }
                            }
                        }break;
                    case Shops.ArmourShop: {
                            if (armourOnSale[choice] < 1)
                            {
                                if(choice != 0)
                                   Console.WriteLine("That item doesn't exist in this shop.");
                            }
                            else
                            {
                                while (choice2 > armourOnSale[choice])
                                {
                                    Console.Write("Choose quantity: ");
                                    choice2 = Convertion.ToInt(Console.ReadLine(), 2);
                                    if (choice2 < 0)
                                        choice2 = 0;
                                    if (choice2 > armourOnSale[choice])
                                        Console.WriteLine("Not enough items on stock.");
                                }
                                if (choice2 != 0)
                                {
                                    price = armourPrice[choice] * choice2;
                                    Console.WriteLine("Price: " + price);
                                    Console.Write("Procced(y/n) ");
                                    if (Console.ReadLine() == "y" && gold >= price && CharacterInventory.numberOfItems + choice2 <= CharacterInventory.capacity)
                                    {
                                        switch (choice)
                                        {
                                            case 1: { CharacterInventory.items[12] += choice2; armourOnSale[1] -= choice2; } break;
                                            case 2: { CharacterInventory.items[16] += choice2; armourOnSale[2] -= choice2; } break;
                                            case 3: { CharacterInventory.items[20] += choice2; armourOnSale[3] -= choice2; } break;
                                            case 4: { CharacterInventory.items[24] += choice2; armourOnSale[4] -= choice2; } break;
                                            case 5: { CharacterInventory.items[28] += choice2; armourOnSale[5] -= choice2; } break;
                                            case 6: { CharacterInventory.items[13] += choice2; armourOnSale[6] -= choice2; } break;
                                            case 7: { CharacterInventory.items[17] += choice2; armourOnSale[7] -= choice2; } break;
                                            case 8: { CharacterInventory.items[21] += choice2; armourOnSale[8] -= choice2; } break;
                                            case 9: { CharacterInventory.items[25] += choice2; armourOnSale[9] -= choice2; } break;
                                            case 10: { CharacterInventory.items[29] += choice2; armourOnSale[10] -= choice2; } break;
                                            case 11: { CharacterInventory.items[14] += choice2; armourOnSale[11] -= choice2; } break;
                                            case 12: { CharacterInventory.items[18] += choice2; armourOnSale[12] -= choice2; } break;
                                            case 13: { CharacterInventory.items[22] += choice2; armourOnSale[13] -= choice2; } break;
                                            case 14: { CharacterInventory.items[26] += choice2; armourOnSale[14] -= choice2; } break;
                                            case 15: { CharacterInventory.items[30] += choice2; armourOnSale[15] -= choice2; } break;
                                            case 16: { CharacterInventory.items[15] += choice2; armourOnSale[16] -= choice2; } break;
                                            case 17: { CharacterInventory.items[19] += choice2; armourOnSale[17] -= choice2; } break;
                                            case 18: { CharacterInventory.items[23] += choice2; armourOnSale[18] -= choice2; } break;
                                            case 19: { CharacterInventory.items[27] += choice2; armourOnSale[19] -= choice2; } break;
                                            case 20: { CharacterInventory.items[31] += choice2; armourOnSale[20] -= choice2; } break;
                                        }
                                        gold -= price;
                                        CharacterInventory.numberOfItems += choice2;
                                        Console.WriteLine("Your transaction was successful.");
                                    }
                                    else
                                    {
                                        if (gold < price)
                                            Console.WriteLine("Not enough gold.");
                                        if (CharacterInventory.capacity < CharacterInventory.numberOfItems + choice2)
                                            Console.WriteLine("Not enough space in your inventory.");
                                    }
                                }
                            }
                        }break;
                }
                Thread.Sleep(1000);
                BuyItems(ref gold, job);
            }
        }
        public static void SellItems(ref int gold)
        {
            bool empty = false;
            int choice = 0, quantity = 200, price = 0;
            CharacterInventory.ShowInventory(1, 38, ref empty);
            Console.WriteLine("[0] Exit");
            if(!empty)
            {
                Console.Write("Choose item: ");
                choice = Convertion.ToInt(Console.ReadLine(), 2);
                if (choice < 1 || choice > 38)
                    choice = 0;
                if (choice != 0)
                {
                    if (CharacterInventory.items[choice] == 0)
                        Console.WriteLine("You don't have that item");
                    else
                    {
                        Console.Write("Choose quantity: ");
                        while(quantity > CharacterInventory.items[choice])
                        {
                            quantity = Convertion.ToInt(Console.ReadLine(), 1);
                            if (quantity < 1)
                                quantity = 1;
                            if (quantity > CharacterInventory.items[choice])
                                Console.WriteLine("Not enough items.");
                        }
                        if (choice >= 1 && choice <= 11)
                        {
                            if (choice <= 2)
                                price = quantity * 2;
                            else price = quantity * weaponsPrice[choice - 1] / 2;
                        }
                        else if (choice >= 32 && choice <= 38)
                            price = quantity * potionsPrice[choice-31] / 2;
                        else if (choice >= 12 && choice <= 31)
                        {
                            if (choice == 12 || choice == 16 || choice == 20 || choice == 24 || choice == 28)
                            {
                                price = quantity * 3 * 25;
                                if (choice == 16)
                                    price = quantity * 4 * 25;
                            }
                            else if (choice == 13 || choice == 17 || choice == 21 || choice == 25 || choice == 29)
                            {
                                price = quantity * 3 * 55;
                                if (choice == 17)
                                    price = quantity * 4 * 55;
                            }
                            else if (choice == 14 || choice == 18 || choice == 22 || choice == 26 || choice == 30)
                            {
                                price = quantity * 3 * 100;
                                if (choice == 18)
                                    price = quantity * 4 * 100;
                            }
                            else if (choice == 15 || choice == 19 || choice == 23 || choice == 27 || choice == 31)
                            {
                                price = quantity * 3 * 200;
                                if (choice == 19)
                                    price = quantity * 4 * 200;
                            }
                        }
                        Console.WriteLine("Gain: " + price + " gold for " + quantity + " ");
                        Console.WriteLine(CharacterInventory.itemsName[choice]);
                        Console.Write("(y/n)");
                        if(Console.ReadLine() == "y")
                        {
                            CharacterInventory.items[choice] -= quantity;
                            CharacterInventory.numberOfItems -= quantity;
                            gold += price;
                        }
                    }
                    Thread.Sleep(750);
                    SellItems(ref gold);
                }
            }
        }

    }
}
