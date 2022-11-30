using System.Linq.Expressions;
using System.Text.Json;

namespace PhaseTwo
{
    /// <summary>
    /// The player class which holds the player properties
    /// MaxHealth
    /// _health
    /// _inventory
    /// goldAmount
    /// And various helper methods to access the inventory
    /// </summary>
    abstract class Player
    {
        protected string _name { get; set; }
        protected HeroClasses _class { get; set; }
        protected int _strength { get; }
        protected int _defence { get; }
        protected int _intelligence { get; }
        protected int _vitality { get; }
        protected int _luck { get; }
        protected int _weaponUse { get; }
        protected int _dodge { get; }

        protected const int MaxHealth = 100;
        protected int _health = MaxHealth;
        protected Inventory _inventory;
        protected EquipedItems _equipedItems;
        float goldAmount;
        public Player(int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge)
        {
            _strength = strength;
            _defence = defence;
            _intelligence = intelligence;
            _vitality = vitality;
            _luck = luck;
            _weaponUse = weaponUse;
            _dodge = dodge;
            goldAmount = 25;
        }
        /// <summary>
        /// Forwards the inventory's CURRENT weight
        /// </summary>
        /// <returns> A float representing the inventory's CURRENT weight of items </returns>
        public float InventoryCurrentWeight() => _inventory.CurrentWeight;

        /// <summary>
        /// Forwards the inventory's MAX weightt
        /// </summary>
        /// <returns> A float representing the inventory's MAX weight of items </returns>
        public float InventoryMaxWeight() => _inventory.MaxWeight;

        /// <summary>
        /// Forwards the inventory's CURRENT volumee
        /// </summary>
        /// <returns> A float representing the inventory's CURRENT volume of items </returns>
        public float InventoryCurrentVolume() => _inventory.CurrentVolume;

        /// <summary>
        /// Forwards the inventory's MAX volume
        /// </summary>
        /// <returns> A float representing the inventory's MAX volume of items </returns>
        public float InventoryMaxVolume() => _inventory.MaxVolume;

        /// <summary>
        /// Forwards the inventory's CURRENT count
        /// </summary>
        /// <returns> An interger representing the inventory's CURRENT count of items </returns>
        public int InventoryCurrentCount() => _inventory.CurrentCount;

        /// <summary>
        /// Forwards the inventory's MAX count
        /// </summary>
        /// <returns> A integer representing the inventory's MAX count of items </returns>
        public int InventoryMaxCount() => _inventory.MaxCount;

        /// <summary>
        /// Forwards a string containing the items name/index/value with a header for the buy/sell menu's/
        /// </summary>
        /// <returns> A string with item names, index in the inventory, and their respective values </returns>
        public (string items, int itemCount) GetIndexedInventory() => _inventory.GetIndexedInventory();

        public (string items, int itemCount) GetIndexedEquipment() => _equipedItems.GetIndexedInventory();
        
        /// <summary>
        /// Forwards the inventory's total value of the items contained within it.
        /// </summary>
        /// <returns> A float representing the gold coins amounting to the total value of items. </returns>
        public float InventoryGetTotalValue() => _inventory.GetTotalValue();

        /// <summary>
        /// Forwards the amount of gold the player has.
        /// </summary>
        /// <returns> A float value representing the amount of gold the player has. </returns>
        public float GetGold() => goldAmount;

        /// <summary>
        /// Forwards True/False if the players inventory contains the specified item
        /// </summary>
        /// <param name="item"> The inventory item to check on </param>
        /// <returns> Bool value whether the item was found </returns>
        public bool PlayerContain(InventoryItem item) => _inventory.InventoryContain(item);

        /// <summary>
        /// Forwards True/False if the player has an item equiped
        /// </summary>
        /// <param name="item"> The inventory item to check on </param>
        /// <returns> Bool value whether the item was found </returns>
        public bool IsItemEquipped(InventoryItem item) => _equipedItems.InventoryContain(item);

        /// <summary>
        /// Equip an item to the players _equippedItems inventory
        /// </summary>
        /// <param name="itemIndex"> The index of the item to equip </param>
        /// <returns> True/False if the item is in the _equippedItems inventory </returns>
        public bool EquipItem(int itemIndex) => _equipedItems.EquipItem(this, itemIndex);

        /// <summary>
        /// Unequip an item from the players _equippedItems inventory
        /// </summary>
        /// <param name="itemIndex"> The index of the item to unequip </param>
        /// <returns> True/False if the item was removed from the _equippedItems inventory </returns>
        public bool UnequipItem(int itemIndex) => _equipedItems.UnequipItem(this, itemIndex);

        /// <summary>
        /// Removes an item from the inventory at a given index
        /// </summary>
        /// <param name="itemIndex"> The index of the item to get </param>
        /// <returns> The item found at the index </returns>
        public InventoryItem RemoveItemFromInventoryAt(int itemIndex) => _inventory.RemoveAt(itemIndex);

        /// <summary>
        /// Gets the players name
        /// </summary>
        /// <returns> The players name </returns>
        public string GetName() => _name;

        /// <summary>
        /// Gets the class of the player
        /// </summary>
        /// <returns> HeroClasses </returns>
        public HeroClasses GetHeroClass() => _class;

        /// <summary>
        /// Updates the value of the players gold amount +/-
        /// player.UpdateGold(-5) to remove gold.
        /// player.UpdateGold(5) to add gold.
        /// </summary>
        /// <param name="value"> The value to increase/decrease the players gold amount </param>
        public bool UpdateGold(float value)
        {
            float playerGold = goldAmount;
            if ((goldAmount + value) >= 0)
            {
                goldAmount += value;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get all equiped items
        /// </summary>
        /// <returns> a string containing all items </returns>
        public string GetEquippedWeapons()
        {
            return _equipedItems.ToString();
        }

        /// <summary>
        /// Shows stats
        /// </summary>
        public void Show()
        {
            Console.WriteLine("Strength = " + _strength);
            Console.WriteLine("Defence = " + _defence);
            Console.WriteLine("Intelligence = " + _intelligence);
            Console.WriteLine("Vitality = " + _vitality);
            Console.WriteLine("Luck = " + _luck);
            Console.WriteLine("Weapon Use = " + _weaponUse);
            Console.WriteLine("Dodge = " + _dodge);
        }

        /// <summary>
        /// Gets the item at the given index
        /// </summary>
        /// /// <param name="itemIndex"> The index of the inventory item to get </param>
        /// <returns> An inventory item associated with the itemIndex </returns>
        public InventoryItem GetItemAtIndex(int itemIndex) => _inventory.GetItemAtIndex(itemIndex);

        /// <summary>
        /// Adds the given item to the players inventory
        /// </summary>
        /// <returns> A bool value whether the item was added </returns>
        public bool AddItem(InventoryItem items) => _inventory.Add(items);

        /// <summary>
        /// Forwarding method to remove an item from player inventory
        /// </summary>
        /// <param name="item"> the item in the inventoryItem </param>
        public void RemoveItem(InventoryItem item) => _inventory.Remove(item);

        /// <summary>
        /// Indicates whether the player currently has the catacomb map.
        /// </summary>
        /// <returns> A bool value whether the map is in the inventory </returns>
        public bool HasMap { get => _inventory.HasMap(); }

        /// <summary>
        /// Indicates whether the player currently has the sword.
        /// </summary>
        /// <returns> A bool value whether the sword is in the inventory </returns>
        public bool HasSword { get => _inventory.HasSword(); }

        /**************************************************************************************************
             * You do not need to alter anything below here but you are free to do
             * For example - while under the effects of a potion of invulnerability, the player cannot die
         *************************************************************************************************/

        // Indicates whether the player is alive or not.
        public bool IsAlive
        {
            get => _health > 0;
        }

        // Represents the distance the player can sense danger.
        // Diagonal adjacent squares have a range of 2 from the player.
        public int SenseRange { get; } = 1;

        // Creates a new player that starts at the given location, with inventory.
        public void HeroAddLocationAndInventory(Location start, Inventory inventory)
        {
            Location = start;
            _inventory = inventory;
            goldAmount = 25;
        }
        public Player(Location start)
        {
            Location = start;
            _inventory = new Inventory(10, 20, 30);
            goldAmount = 25;
        }

        // The player's current location.
        public Location Location { get; set; }

        // Explains why a player died.
        public string CauseOfDeath { get; private set; }

        public void Kill(string cause)
        {
            _health = 0;
            CauseOfDeath = cause;
        }
    }

    // Represents a location in the 2D game world, based on its row and column.
    public record Location(int Row, int Column);
}

