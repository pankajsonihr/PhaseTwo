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
    class Player
    {
        protected int _strength;
        protected int _defence;
        protected int _intelligence;
        protected int _vitality;
        protected int _luck;
        protected int _weaponUse;
        protected int _dodge;
        protected const int MaxHealth = 100;
        protected int _health = MaxHealth;
        protected Inventory _inventory;
        float goldAmount;
        public Player(int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) { }
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

        /// <summary>
        /// Forwards True/False if the players inventory contains the specified item
        /// </summary>
        /// <param name="item"> The inventory item to check on </param>
        /// <returns> Bool value whether the item was found </returns>
        public bool PlayerContain(InventoryItem item) => _inventory.InventoryContain(item);

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
        public Player(Location start, Inventory inventory)
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
    class CharacterGenerator
    {
        public CharacterGenerator(Player player,string file)
        {
            Console.WriteLine("Welcome to hero selection menu. We have varities of heroes.\n1 For Import a hero of your choice.\n2 Select a hero we have.");
            int? selection = GetSelection(1, 2, "Our selection menu not getting what do you want to do?.");
            if (selection == 1)
            {
                ImportHero(player,file);
            }
            else if(selection == 2)
            {
                SelectHeroPreset(player);
            }

        }
        void SelectHeroPreset(Player player)
        {
            (int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) localData;
            Console.WriteLine("Which hero you would like to choose?\n 1 For Knight\n2 For Wizard\n3 For ValKery");
            int selection = GetSelection(1, 3, "Our selection menu not getting what do you want to do?.");
            Player forShow;
            switch (selection)
            {
                case 1:
                    Console.WriteLine("You have chosen Knight. It has the following preset powers");
                    forShow = new Knight();
                    forShow.Show();
                    Console.WriteLine($"Would you like to customise Knight according to your self or you want the preset powers?\n1 For Customisation\n2 For Default powers");
                    break;
                case 2:
                    Console.WriteLine("You have chosen Wizard. It has the following preset powers");
                    forShow = new Wizard();
                    forShow.Show();
                    Console.WriteLine($"Would you like to customise Wizard according to your self or you want the preset powers?\n1 For Customisation\n2 For Default powers");
                    break;
                case 3:
                    Console.WriteLine("You have chosen Valkery. It has the following preset powers");
                    forShow = new ValKery();
                    forShow.Show();
                    Console.WriteLine($"Would you like to customise Valkery according to your self or you want the preset powers?\n1 For Customisation\n2 For Default powers");
                    break;
                    default: //There is no default as we have filtered all values already
                    break;
            }
            int selectionForCustomisation = GetSelection(1, 2, "Our customisation menu not getting what do you want to do?.");
            if (selectionForCustomisation == 1)
            {
                localData=Customisation(player);
                player = selection switch
                {
                    1 => new Knight(localData.strength, localData.defence, localData.intelligence, localData.vitality, localData.luck, localData.weaponUse, localData.dodge),
                    2 => new Wizard(localData.strength, localData.defence, localData.intelligence, localData.vitality, localData.luck, localData.weaponUse, localData.dodge),
                    3 => new ValKery(localData.strength, localData.defence, localData.intelligence, localData.vitality, localData.luck, localData.weaponUse, localData.dodge)
                };
            }
            else if(selectionForCustomisation == 2)
            {
                player = selection switch
                {
                    1 => new Knight(),
                    2 => new Wizard(),
                    3 => new ValKery()
                };
            }

            
        }
        (int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) Customisation(Player player)
        {
            int localStrength;
            int localDefence=0;
            int localIntelligence = 0;
            int localVitality = 0;
            int localLuck = 0;
            int localWeaponUse = 0;
            int localDodge = 0;
            int maxPower=100;
            Console.WriteLine($"Please input your desired strength You can choose power upto: {maxPower} powers");
            localStrength = GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.");
            maxPower-=localStrength;
            Console.WriteLine($"Your strength is set to {localStrength}.");
            if (maxPower == 0)
            {
                return (localStrength,localDefence,localIntelligence,localVitality,localLuck,localWeaponUse,localDodge);
            }
            Console.WriteLine($"Please input your desired defence You can choose power upto: {maxPower} powers");
            localDefence = GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.");
            maxPower -= localDefence;
            Console.WriteLine($"Your defence is set to {localDefence}.");
            if (maxPower == 0)
            {
                return (localStrength, localDefence, localIntelligence, localVitality, localLuck, localWeaponUse, localDodge);
            }
            Console.WriteLine($"Please input your desired intelligence You can choose power upto: {maxPower} powers");
            localIntelligence = GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.");
            maxPower -= localIntelligence;
            Console.WriteLine($"Your intelligence is set to {localIntelligence}.");
            if (maxPower == 0)
            {
                return (localStrength, localDefence, localIntelligence, localVitality, localLuck, localWeaponUse, localDodge);
            }
            Console.WriteLine($"Please input your desired vitality You can choose power upto: {maxPower} powers");
            localVitality = GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.");
            maxPower -= localVitality;
            Console.WriteLine($"Your vitality is set to {localVitality}.");
            if (maxPower == 0)
            {
                return (localStrength, localDefence, localIntelligence, localVitality, localLuck, localWeaponUse, localDodge);
            }
            Console.WriteLine($"Please input your desired luck You can choose power upto: {maxPower} powers");
            localLuck = GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.");
            maxPower -= localLuck;
            Console.WriteLine($"Your luck is set to {localLuck}.");
            if (maxPower == 0)
            {
                return (localStrength, localDefence, localIntelligence, localVitality, localLuck, localWeaponUse, localDodge);
            }
            Console.WriteLine($"Please input your desired weapon use You can choose power upto: {maxPower} powers");
            localWeaponUse = GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.");
            maxPower -= localWeaponUse;
            Console.WriteLine($"Your weapon use is set to {localWeaponUse}.");
            if (maxPower == 0)
            {
                return (localStrength, localDefence, localIntelligence, localVitality, localLuck, localWeaponUse, localDodge);
            }
            Console.WriteLine($"Please input your desired dodge You can choose power upto: {maxPower} powers");
            localDodge = GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.");
            maxPower -= localDodge;
            Console.WriteLine($"Your dodge is set to {localDodge}.");
            if (maxPower == 0)
            {
                return (localStrength, localDefence, localIntelligence, localVitality, localLuck, localWeaponUse, localDodge);
            }

            return (localStrength, localDefence, localIntelligence, localVitality, localLuck, localWeaponUse, localDodge);
        }
        void ImportHero(Player player,string file)
        {
            int sum;
            string text = System.IO.File.ReadAllText(file);
            ImportPlayerData importValues = JsonSerializer.Deserialize<ImportPlayerData>(text);
            if (importValues.Character == 1)
            {
                sum=importValues.Strength+importValues.Defence+importValues.Intelligence+importValues.Vitality+importValues.Luck+importValues.WeaponUse+importValues.Dodge;
                if(sum>100 || sum < 0)
                {
                    Console.WriteLine("You have imported more power than allowed power");
                }
                else
                {
                    player = new Knight(importValues.Strength,importValues.Defence,importValues.Intelligence,importValues.Vitality,importValues.Luck,importValues.WeaponUse,importValues.Dodge);
                    Console.WriteLine("Showing your imported powers for Knight");
                    player.Show();
                }
               
            }
            else if (importValues.Character == 2)
            {
                sum = importValues.Strength + importValues.Defence + importValues.Intelligence + importValues.Vitality + importValues.Luck + importValues.WeaponUse + importValues.Dodge;
                if (sum > 100 || sum < 0)
                {
                    Console.WriteLine("You have imported more power than allowed power");
                }
                else
                {
                    player = new Wizard(importValues.Strength, importValues.Defence, importValues.Intelligence, importValues.Vitality, importValues.Luck, importValues.WeaponUse, importValues.Dodge);
                    Console.WriteLine("Showing your imported powers for Wizard");
                    player.Show();
                }
            }
            else if (importValues.Character == 3)
            {
                sum = importValues.Strength + importValues.Defence + importValues.Intelligence + importValues.Vitality + importValues.Luck + importValues.WeaponUse + importValues.Dodge;
                if (sum > 100 || sum < 0)
                {
                    Console.WriteLine("You have imported more power than allowed power");
                }
                else
                {
                    player = new ValKery(importValues.Strength, importValues.Defence, importValues.Intelligence, importValues.Vitality, importValues.Luck, importValues.WeaponUse, importValues.Dodge);
                    Console.WriteLine("Showing your imported powers for ValKery");
                player.Show();
                }
            }
                Console.WriteLine("Your imported player info is not valid please consider character values 1,2,3 will select Knight,Wizard,ValKery simultaneously.");
            }
        }
        private  int GetSelection(int min, int max, string errorMsg)
        {
            int? selection = SanitizeInput(Console.ReadLine(), min, max);
            while (selection == null)
            {
                Console.WriteLine(errorMsg);
                selection = SanitizeInput(Console.ReadLine(), min, max);
            }
            return (int)selection; // 'selection' cannot be null here
        }
        public  int? SanitizeInput(string input, int min, int max)
        {
            int result;
            if (int.TryParse(input, out result) && result >= min && result <= max)
            {
                return result;
            }
            return null;
        }
    }
    public struct ImportPlayerData
    {
        public int Character { get; set; }
        public int Strength { get; set; }
        public int Defence { get; set; }
        public int Intelligence { get; set; }
        public int Vitality { get; set; }
        public int Luck { get; set; }
        public int WeaponUse { get; set; }
        public int Dodge { get; set; }
    }
    class Knight: Player
    {
        public Knight() : base(25,18,7,13,5,25,7) { }
        public Knight(int strength,int defence, int intelligence,int vitality,int luck, int weaponUse, int dodge) : base(strength,defence,intelligence,vitality,luck,weaponUse,dodge) { }
    }
    class Wizard:Player
    {
        public Wizard() : base(15, 13, 25, 11, 9, 14, 13) { }
        public Wizard(int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) : base(strength, defence, intelligence, vitality, luck, weaponUse, dodge) { }
    }
    class ValKery:Player
    {
        public ValKery() : base(30, 12, 9, 16, 8, 25, 9) { }
        public ValKery(int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) : base(strength, defence, intelligence, vitality, luck, weaponUse, dodge) { }
    }

    // Represents a location in the 2D game world, based on its row and column.
    public record Location(int Row, int Column);
}

