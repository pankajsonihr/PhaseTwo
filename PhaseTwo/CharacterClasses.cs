using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PhaseTwo
{
    //abstract class Hero : Player
    //{
    //    protected EquipedItems _equipedItems;
    //    protected Hero() : base(30, 12, 9, 16, 8, 25, 9) { _name = "Gimli Lord of the Glittering Caves"; _class = HeroClasses.ValKery; _equipedItems = new EquipedItems(2, 14f, 10f); }
    //    protected Hero(int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) : base(strength, defence, intelligence, vitality, luck, weaponUse, dodge) { _name = "Hero"; _class = HeroClasses.ValKery; _equipedItems = new EquipedItems(1, 8f, 6f); }
    //}

    class Knight : Player
    {
        protected EquipedItems _equipedItems;
        public Knight() : base(25, 18, 7, 13, 5, 25, 7) { _name = "Théoden Lord of the Mark"; _class = HeroClasses.Knight; _equipedItems = new EquipedItems(1, 8f, 6f); }
        public Knight(int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) : base(strength, defence, intelligence, vitality, luck, weaponUse, dodge) { _name = "Knight"; _class = HeroClasses.Knight; _equipedItems = new EquipedItems(1, 8f, 6f); }
        public override bool EquipWeapon(int itemIndex)
        {
            var item = _inventory.GetItemAtIndex(itemIndex-1);
            if (item.GetType() == typeof(Sword)) 
            {
                if (_equipedItems.Add(item))
                {
                    _inventory.RemoveAt(itemIndex);
                    Console.WriteLine($"{_name} equiped {item}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"{_name} could not equip {item}. ");
                }
            }
            else
            {
                Console.WriteLine($"{_name} isn't capable of equiping {item}");
            }
            return false;
        }
        public override string GetEquippedWeapons()
        {
            return _equipedItems.ToString();
        }
    }
    class Wizard : Player
    {
        protected EquipedItems _equipedItems;
        public Wizard() : base(15, 13, 25, 11, 9, 14, 13) { _name = "Saruman Lord of Isengard"; _class = HeroClasses.Wizard; _equipedItems = new EquipedItems(1, 6f, 8f); }
        public Wizard(int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) : base(strength, defence, intelligence, vitality, luck, weaponUse, dodge) { _name = "Wizard"; _class = HeroClasses.Wizard; _equipedItems = new EquipedItems(1, 6f, 8f); }

        public override bool EquipWeapon(int itemIndex)
        {
            var item = _inventory.GetItemAtIndex(itemIndex-1);
            if (item.GetType() == typeof(Staff))
            {
                if (_equipedItems.Add(item))
                {
                    _inventory.RemoveAt(itemIndex);
                    Console.WriteLine($"{_name} equiped {item}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"{_name} could not equip {item}. ");
                }
            }
            else
            {
                Console.WriteLine($"{_name} isn't capable of equiping {item}");
            }
            return false;
        }

        public override string GetEquippedWeapons()
        {
            return _equipedItems.ToString();
        }
    }
    class ValKery : Player
    {
        protected EquipedItems _equipedItems;
        public ValKery() : base(30, 12, 9, 16, 8, 25, 9) { _name = "Gimli Lord of the Glittering Caves"; _class = HeroClasses.ValKery; _equipedItems = new EquipedItems(2, 14f, 10f); }
        public ValKery(int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) : base(strength, defence, intelligence, vitality, luck, weaponUse, dodge) { _name = "ValKery"; _class = HeroClasses.ValKery; }

        public override bool EquipWeapon(int itemIndex)
        {
            var item = _inventory.GetItemAtIndex(itemIndex - 1);
            if (item.GetType() == typeof(Sword) || item.GetType() == typeof(Shield))
            {
                if (_equipedItems.Add(item))
                {
                    _inventory.RemoveAt(itemIndex);
                    Console.WriteLine($"{_name} equiped {item}");
                    return true;
                }
                else
                {
                    Console.WriteLine($"{_name} could not equip {item}. ");
                }
            }
            else
            {
                Console.WriteLine($"{_name} isn't capable of equiping {item}");
            }
            return false;
        }

        public override string GetEquippedWeapons()
        {
            return _equipedItems.ToString();
        }
    }

    class EquipedItems : Inventory
    {
        public EquipedItems(int maxCount, float maxWeight, float maxVolume) : base(maxCount,maxWeight,maxVolume) { }

        public EquipedWeapon();

    }

    static class EquipementTester
    {
        public static void AddEquipment(Player player)
        {
            bool addmore = true;
            do
            {
                (string items, int itemCount) indexedInventory = player.GetIndexedInventory();
                Console.WriteLine("You current have these items equiped:\n");
                Console.WriteLine(player.GetEquippedWeapons());
                Console.WriteLine("Which item would you like to equip from your inventory?\n");
                Console.WriteLine(indexedInventory);
                Console.WriteLine($"\t{indexedInventory.itemCount}\t-\tExit\n");
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice >= 1 & choice <= indexedInventory.itemCount - 1)
                    {
                        player.EquipWeapon(choice);
                    }
                    else if (choice == indexedInventory.itemCount)
                    {
                        throw new System.Runtime.CompilerServices.SwitchExpressionException();
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That is an invalid selection.");
                }
                catch (System.Runtime.CompilerServices.SwitchExpressionException)
                {
                    Console.WriteLine("You close your inventory");
                    addmore = false;
                }
                Console.ResetColor();
            } while (addmore);
        }
    }

    public struct AllowedWeapons
    {
        public static readonly Dictionary<HeroClasses, List<InventoryItem>> AllowedWeaponsDict = new Dictionary<HeroClasses, List<InventoryItem>>
        {
            { HeroClasses.Knight, new List<InventoryItem>(){ new Sword() } },
            { HeroClasses.Wizard, new List<InventoryItem>(){ new Staff() } },
            { HeroClasses.ValKery, new List<InventoryItem>(){ new Axe(), new Shield() } }
        };
    }

    enum HeroClasses { Knight, Wizard, ValKery };
}
