using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace PhaseTwo
{
    /// <summary>
    /// Inventory container class that holds a characters InventoryItems.
    /// Ability to add/remove items.
    /// </summary>
    class Inventory
    {
        // The Max Count of items the inventory can hold.
        public int MaxCount { get; }
        // The Max Volume of items the inventory can hold.
        public float MaxVolume { get; }
        // The Max Weight of items the inventory can hold.
        public float MaxWeight { get; }

        // The list to hold all the items in the inventory
        private List<InventoryItem> _items;

        // The Current Count of items the inventory can hold.
        public int CurrentCount { get; private set; }
        // The Current Volume of items the inventory can hold.
        public float CurrentVolume { get; private set; }
        // The Current Weight of items the inventory can hold.
        public float CurrentWeight { get; private set; }
        // The TotalValue of all items in the inventory
        public float TotalValue { get; private set; }
    

        /// <summary>
        /// The constructor class for the inventory
        /// </summary>
        /// <param name="maxCount"> The maximum count of items </param>
        /// <param name="maxVolume"> The maximum volume of items </param>
        /// <param name="maxWeight"> The maximum weight of items </param>
        public Inventory(int maxCount, float maxVolume, float maxWeight)
        {
            MaxVolume = maxVolume;
            MaxWeight = maxWeight;
            MaxCount = maxCount;
            _items = new List<InventoryItem> { };
        }

        /// <summary>
        /// Method to detect the map in the inventory.
        /// </summary>
        /// <returns> True - map in inventory | False - map NOT in inventory </returns>
        public bool HasMap() { return _items.OfType<Map>().Any(); }

        /// <summary>
        /// Method to detect the sword in the inventory.
        /// </summary>
        /// <returns> True - sword in inventory | False - sword NOT in inventory </returns>
        public bool HasSword() { return _items.OfType<Sword>().Any(); }


        /// <summary>
        /// Gets the item at the specified index in the inventory
        /// </summary>
        /// <param name="itemIndex"> The index of the item to get </param>
        /// <returns> The InventoryItem at the given itemIndex </returns>
        public InventoryItem GetItemAtIndex(int itemIndex) { return _items[itemIndex]; }

        /// <summary>
        /// This method Adds items to the inventory list
        /// </summary>
        /// <param name="item"> The InventoryItem to add to the inventory list </param>
        /// <returns> A boolean whether the item was added to the inventory </returns>
        public bool Add(InventoryItem item)
        {
            if(CurrentCount >= MaxCount) return false;
            if (CurrentVolume + item.Volume > MaxVolume) return false;
            if (CurrentWeight + item.Weight > MaxWeight) return false;

            _items.Add(item);
            CurrentVolume += item.Volume;
            CurrentWeight += item.Weight;
            CurrentCount++;
            TotalValue += item.Value;
            return true;
        }

        /// <summary>
        /// Gets the total value of all the items in the inventory
        /// </summary>
        /// <returns> A float representing the value of all items in the inventory </returns>
        public float GetTotalValue()
        {
            float value = 0;
            foreach(var item in _items)
            {
                value += item.Value;
            }
            return value;
        }

        /// <summary>
        /// Removes an item at the itemIndex from the inventory
        /// </summary>
        /// <param name="itemIndex"> The item index to remove from the inventory </param>
        /// <returns> The InventoryItem removed from the inventory </returns>
        public InventoryItem RemoveAt(int itemIndex)
        {
            InventoryItem item = _items[itemIndex];
            _items.RemoveAt(itemIndex);

            CurrentVolume -= item.Volume;
            CurrentWeight -= item.Weight;
            CurrentCount--;

            return item;
        }

        /// <summary>
        /// Removes the first specified item from the inventory
        /// </summary>
        /// <param name="item"> An instance of an InventoryItem to remove from the inventory </param>
        /// <returns> A boolean value representing whether the item was removed </returns>
        public bool Remove(InventoryItem item)
        {
            int index = FindItemIndex(item);
            if (index != -1)
            {
                _items.RemoveAt(index);
                CurrentVolume -= item.Volume;
                CurrentWeight -= item.Weight;
                CurrentCount--;
                return true;
            }
            else
            {
                Console.WriteLine("Inventory don't have the item");
            }
            return false;
        }

        /// <summary>
        /// This method finds the item index of an InventoryItem in the inventory
        /// </summary>
        /// <param name="item"> An instance of the desired InventoryItem </param>
        /// <returns> The index of the item in the inventory </returns>
        private int FindItemIndex(InventoryItem item)
        {
            for (int i = 0; i < _items.Count; ++i)
            {
                if (_items[i].Name == item.Name)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// This method finds if the inventory contains an instance of an item.
        /// </summary>
        /// <param name="item"> the InventoryItem to remove from the inventory </param>
        /// <returns> A boolean value representing whether the inventory contains an item </returns>
        public bool InventoryContain(InventoryItem item)
        {
            foreach (var inventorList in _items)
            {
                if (inventorList.Name == item.Name)
                {
                    return true;
                }
            }

            return false;
        }     

        /// <summary>
        /// The GetEnumerator method to allow using for loops on the inventory class
        /// </summary>
        /// <returns></returns>
        public IEnumerator<InventoryItem> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        /// <summary>
        /// The ToString method of the inventory class
        /// </summary>
        /// <returns> A string value representing the items in the inventory </returns>
        public override string ToString()
        {
            if (_items.Count == 0) return "{\n\tEmpty\n}";
            string ret = "{";
            foreach (InventoryItem item in _items)
            {
                ret += $"\n\t- {item.ToString()} - {item.Value} G";
            }
            ret += "\n}\n";
            return ret;
        }

        /// <summary>
        /// Gets the inventory with indexed items
        /// </summary>
        /// <returns> items - A string with index items, and their values | itemCount - An integer representing the Count of items in inventory </returns>
        public (string items, int itemCount) GetIndexedInventory()
        {
            string ret = "\nItem Index\t-\tItem Name\t-\tValue";
            int itemIndex = 1;
            foreach (InventoryItem item in _items)
            {
                ret += $"\n\t{itemIndex}\t-\t{item}\t\t-\t{item.Value} Gold coins";
                itemIndex++;
            }
            return (items: ret, itemCount: itemIndex);
        }
    }

    /// <summary>
    /// Tester class for the inventory
    /// </summary>
    static class InventoryTester
    {
        /// <summary>
        /// The tester method for adding equipment to the inventory
        /// </summary>
        /// <param name="inventory"> The inventory instance to test </param>
        static public void AddEquipment(Inventory inventory)
        {
            bool addMoreItems = true;
            do
            {
                Console.WriteLine($"Inventory is currently at {inventory.CurrentCount}/{inventory.MaxCount} items, {inventory.CurrentWeight}/{inventory.MaxWeight} weight," +
                                  $" {inventory.CurrentVolume}/{inventory.MaxVolume} volume, and {inventory.TotalValue} total value.");
                Console.WriteLine(inventory.ToString());
                Console.WriteLine("What do you want to add?");
                Console.WriteLine("1 - Sword");
                Console.WriteLine("2 - Map");
                Console.WriteLine("3 - Shield");
                Console.WriteLine("4 - Healing Potion");
                Console.WriteLine("5 - Next - Remove Items");

                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    InventoryItem newItem = choice switch
                    {
                        1 => new Sword(),
                        2 => new Map(),
                        3 => new Shield(),
                        4 => new HealingPotion()
                    };
                    if (!inventory.Add(newItem))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Could not fit this item into the inventory.");
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That is an invalid selection.");

                }
                catch (System.Runtime.CompilerServices.SwitchExpressionException)
                {
                    Console.WriteLine("Venturing Forth!");
                    addMoreItems = false;
                }
                Console.ResetColor();
            } while (addMoreItems);
        }

        /// <summary>
        /// The tester method for removing equipment to the inventory
        /// </summary>
        /// <param name="inventory"> The inventory instance to test </param>
        static public void RemoveEquipment(Inventory inventory)
        {
            bool remMoreItems = true;
            do
            {
                Console.WriteLine($"Inventory is currently at {inventory.CurrentCount}/{inventory.MaxCount} items, {inventory.CurrentWeight}/{inventory.MaxWeight} weight," +
                                  $" {inventory.CurrentVolume}/{inventory.MaxVolume} volume, and {inventory.TotalValue} total value."); 
                Console.WriteLine(inventory.ToString());
                Console.WriteLine("What do you want to Remove?");
                Console.WriteLine("1 - Sword");
                Console.WriteLine("2 - Map");
                Console.WriteLine("3 - Shield");
                Console.WriteLine("4 - Healing Potion");
                Console.WriteLine("5 - Gather your inventory and venture forth");

                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    InventoryItem remItem = choice switch
                    {
                        1 => new Sword(),
                        2 => new Map(),
                        3 => new Shield(),
                        4 => new HealingPotion()
                    };
                    if (!inventory.Remove(remItem))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Could not find this item into the inventory.");
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That is an invalid selection.");

                }
                catch (System.Runtime.CompilerServices.SwitchExpressionException)
                {
                    Console.WriteLine("Venturing Forth!");
                    remMoreItems = false;
                }
                Console.ResetColor();
            } while (remMoreItems);
        }
    }
}
