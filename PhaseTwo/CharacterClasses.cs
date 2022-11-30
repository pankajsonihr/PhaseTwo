using System.Reflection.Emit;
using System.Xml.Linq;

namespace PhaseTwo
{

    class Knight : Player
    {
        public Knight() : base(25, 18, 7, 13, 5, 25, 7) { _name = "Théoden Lord of the Mark"; _class = HeroClasses.Knight; _equipedItems = new EquipedItems(1, 8f, 6f); }
        public Knight(int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) : base(strength, defence, intelligence, vitality, luck, weaponUse, dodge) { _name = "Knight"; _class = HeroClasses.Knight; _equipedItems = new EquipedItems(1, 8f, 6f); }
    }
    class Wizard : Player
    {
        public Wizard() : base(15, 13, 25, 11, 9, 14, 13) { _name = "Saruman Lord of Isengard"; _class = HeroClasses.Wizard; _equipedItems = new EquipedItems(1, 6f, 8f); }
        public Wizard(int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) : base(strength, defence, intelligence, vitality, luck, weaponUse, dodge) { _name = "Wizard"; _class = HeroClasses.Wizard; _equipedItems = new EquipedItems(1, 6f, 8f); }
    }
    class ValKery : Player
    {
        public ValKery() : base(30, 12, 9, 16, 8, 25, 9) { _name = "Gimli Lord of the Glittering Caves"; _class = HeroClasses.ValKery; _equipedItems = new EquipedItems(2, 14f, 10f); }
        public ValKery(int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) : base(strength, defence, intelligence, vitality, luck, weaponUse, dodge) { _name = "ValKery"; _class = HeroClasses.ValKery; }
    }

    class EquipedItems : Inventory
    {
        public EquipedItems(int maxCount, float maxWeight, float maxVolume) : base(maxCount, maxWeight, maxVolume) { }

        public bool EquipItem(Player player, int itemIndex)
        {
            itemIndex--;
            InventoryItem item = player.GetItemAtIndex(itemIndex);
            System.Type itemType = item.GetType();

            HeroClasses playerClass = player.GetHeroClass();
            string playerName = player.GetName();
            List<System.Type> AllowedItemsList = AllowedItems.AllowedItemsDict[playerClass];

            if (AllowedItemsList.Contains(itemType))
            {
                if (Add(item))
                {
                    player.RemoveItemFromInventoryAt(itemIndex);
                    Console.WriteLine($"\n{playerName} equiped {item}\n");
                    return true;
                }
                else
                {
                    Console.WriteLine($"\n{playerName} could not equip {item}.\n");
                }
            }
            else
            {
                Console.WriteLine($"\n{playerName} isn't capable of equiping {item}\n");
            }
            return false;
        }

        public bool UnequipItem(Player player, int itemIndex)
        {
            itemIndex--;
            string playerName = player.GetName();
            InventoryItem item = GetItemAtIndex(itemIndex);

            if (InventoryContain(item))
            {
                RemoveAt(itemIndex);
                Console.WriteLine($"\n{playerName} unequiped {item}\n");
                return true;
            }
            else
            {
                Console.WriteLine($"\n{playerName} does not have {item} equipped.\n");
            }
            return false;
        }
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
                Console.WriteLine("Which item would you like to equip?\n");
                Console.WriteLine(indexedInventory);
                Console.WriteLine($"\t{indexedInventory.itemCount}\t-\tExit\n");
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice >= 1 & choice <= indexedInventory.itemCount - 1)
                    {
                        player.EquipItem(choice);
                    }
                    else if (choice == indexedInventory.itemCount)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That is an invalid selection.\n");
                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine("You close your inventory\n");
                    addmore = false;
                }
                Console.ResetColor();
            } while (addmore);
        }

        public static void RemoveEquipment(Player player)
        {
            bool remmore = true;
            do
            {
                (string items, int itemCount) indexedEquipment = player.GetIndexedEquipment();
                Console.WriteLine("You current have these items equipped:\n");
                Console.WriteLine(player.GetEquippedWeapons());
                Console.WriteLine("Which item would you like to unequip from your inventory?\n");
                Console.WriteLine(indexedEquipment);
                Console.WriteLine($"\t{indexedEquipment.itemCount}\t-\tExit\n");
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice >= 1 & choice <= indexedEquipment.itemCount - 1)
                    {
                        player.UnequipItem(choice);
                    }
                    else if (choice == indexedEquipment.itemCount)
                    {
                        throw new IndexOutOfRangeException();
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That is an invalid selection.\n");
                }
                catch (System.IndexOutOfRangeException)
                {
                    Console.WriteLine("You close your inventory\n");
                    remmore = false;
                }
                Console.ResetColor();
            } while (remmore);
        }
    }

    public struct AllowedItems
    {
        public static readonly Dictionary<HeroClasses, List<System.Type>> AllowedItemsDict = new Dictionary<HeroClasses, List<System.Type>>
        {
            { HeroClasses.Knight, new List<System.Type>(){ typeof(Sword) } },
            { HeroClasses.Wizard, new List<System.Type>(){ typeof(Staff) } },
            { HeroClasses.ValKery, new List<System.Type>(){ typeof(Axe), typeof(Shield) } }
        };
    }

    public enum HeroClasses { Knight, Wizard, ValKery };
}
