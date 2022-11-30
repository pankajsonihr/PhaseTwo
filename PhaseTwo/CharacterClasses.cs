using System.Reflection.Emit;
using System.Xml.Linq;

namespace PhaseTwo
{

    /// <summary>
    /// The Knight hero class that determines which weapons the player can use.
    /// </summary>
    class Knight : Player
    {
        public Knight() : base(25, 18, 7, 13, 5, 25, 7) { _name = "Théoden Lord of the Mark"; _class = HeroClasses.Knight; _equipedItems = new EquipedItems(1, 8f, 6f); }
        public Knight(int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) : base(strength, defence, intelligence, vitality, luck, weaponUse, dodge) { _name = "Knight"; _class = HeroClasses.Knight; _equipedItems = new EquipedItems(1, 8f, 6f); }
    }

    /// <summary>
    /// The Wizard hero class that determines which weapons the player can use.
    /// </summary>
    class Wizard : Player
    {
        public Wizard() : base(15, 13, 25, 11, 9, 14, 13) { _name = "Saruman Lord of Isengard"; _class = HeroClasses.Wizard; _equipedItems = new EquipedItems(1, 6f, 8f); }
        public Wizard(int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) : base(strength, defence, intelligence, vitality, luck, weaponUse, dodge) { _name = "Wizard"; _class = HeroClasses.Wizard; _equipedItems = new EquipedItems(1, 6f, 8f); }
    }

    /// <summary>
    /// The ValKery hero class that determines which weapons the player can use.
    /// </summary>
    class ValKery : Player
    {
        public ValKery() : base(30, 12, 9, 16, 8, 25, 9) { _name = "Gimli Lord of the Glittering Caves"; _class = HeroClasses.ValKery; _equipedItems = new EquipedItems(2, 14f, 10f); }
        public ValKery(int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) : base(strength, defence, intelligence, vitality, luck, weaponUse, dodge) { _name = "ValKery"; _class = HeroClasses.ValKery; }
    }


    /// <summary>
    /// EquipedItems class derived from the Inventory class. A smaller inventory that only allows certain items to be added to it based on the AllowedItems struct.
    /// </summary>
    class EquipedItems : Inventory
    {
        /// <summary>
        /// Equipment inventory constructor. can have custom values
        /// </summary>
        /// <param name="maxCount"> The max count of items in the inventory </param>
        /// <param name="maxWeight"> The max weight of items in the inventory </param>
        /// <param name="maxVolume"> The max volume of items in the inventory </param>
        public EquipedItems(int maxCount, float maxWeight, float maxVolume) : base(maxCount, maxWeight, maxVolume) { }

        /// <summary>
        /// Adds an item to the equipment inventory if the item is allowed to be used by the player class.
        /// </summary>
        /// <param name="player"> The player equiping the item  </param>
        /// <param name="itemIndex"> The index of the item to equip </param>
        /// <returns></returns>
        public bool EquipItem(Player player, int itemIndex)
        {
            itemIndex--;
            InventoryItem item = player.GetItemAtIndex(itemIndex);
            System.Type itemType = item.GetType();

            HeroClasses playerClass = player.GetHeroClass();
            string playerName = player.GetName();

            if (AllowedItems.AllowedItemsDict[playerClass].Contains(itemType))
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

        /// <summary>
        /// Removes an item from the equipment inventory
        /// </summary>
        /// <param name="player"> The player to unequip the item from </param>
        /// <param name="itemIndex"> The index of the item in the equipment inventory </param>
        /// <returns></returns>
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

    /// <summary>
    /// A class to test the add/remove feature of the equipment inventory.
    /// </summary>
    static class EquipementTester
    {
        /// <summary>
        /// A menu to add equipment to the equipment inventory
        /// </summary>
        /// <param name="player"> The player to test on </param>
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

        /// <summary>
        /// A menu to remove equipment from the equipment inventory
        /// </summary>
        /// <param name="player"> The player to test on </param>
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

    /// <summary>
    /// The allowed items struct contains a dictionary that says which HeroClass can use which items.
    /// </summary>
    public struct AllowedItems
    {
        public static readonly Dictionary<HeroClasses, List<System.Type>> AllowedItemsDict = new Dictionary<HeroClasses, List<System.Type>>
        {
            { HeroClasses.Knight, new List<System.Type>(){ typeof(Sword) } },
            { HeroClasses.Wizard, new List<System.Type>(){ typeof(Staff) } },
            { HeroClasses.ValKery, new List<System.Type>(){ typeof(Axe), typeof(Shield) } }
        };
    }


    /// <summary>
    /// The HeroClasses Enum for display which class the player has selected.
    /// </summary>
    public enum HeroClasses { Knight, Wizard, ValKery };
}
