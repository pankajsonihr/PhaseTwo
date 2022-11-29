using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PhaseTwo
{
    /// <summary>
    /// The base inventory item class, contains the Weight, Volume, item Value, its ENUM name, and its use
    /// </summary>
    class InventoryItem
    {
        // Item weight
        public float Weight { get; }
        // Item Volume
        public float Volume { get; }
        // Item Value
        public float Value { get; }
        // Name of the item, with itemName Enum
        public ItemName Name { get; }
        // The value of the use of the item
        public float Use;

        
        /// <summary>
        /// The inventory item constructor
        /// </summary>
        /// <param name="name"> The item name, using ItemName enum </param>
        /// <param name="weight"> The weight of the item </param>
        /// <param name="volume"> The volume of the item </param>
        /// <param name="value"> The value of the item </param>
        /// <param name="use"> The value of the 'use' of the item </param>
        public InventoryItem(ItemName name, float weight, float volume, float value, float use)
        {
            Name = name;
            Weight = weight;
            Volume = volume;
            Value = value;
            Use = use;
        }

        /// <summary>
        /// The Use of the item method. Virtual as not all item have a use.
        /// </summary>
        /// <returns> A boolean if the item was used succesfully </returns>
        public virtual bool UseItem()
        {
            return false;
        }
        /// <summary>
        /// This method gets the item name
        /// </summary>
        /// <returns> A string of the item name </returns>
        public override string ToString()
        {
            string ret = base.ToString();
            return ret.Substring(ret.IndexOf('.') + 1);
        }
    }

    /// <summary>
    /// The weapon class, derived from InventoryItem
    /// </summary>
    class Weapon : InventoryItem 
    {
        /// <summary>
        /// The weapon class constructor
        /// </summary>
        /// <param name="name"> The name of the item "Sword" </param>
        /// <param name="weight"> The weight of the sword </param>
        /// <param name="volume"> The volume of the sword </param>
        /// <param name="value"> The gold coin value of the sword </param>
        /// <param name="use"> The damage of the sword </param>
        public Weapon(ItemName name, float weight, float volume, float value, float use) : base(name,weight, volume, value, use) { }
    }

    /// <summary>
    /// The potion class, derived from the InventoryItem
    /// </summary>
    class Potion : InventoryItem
    {
        // The PotionEffect enum that represents the type of effect the potion uses
        public PotionEffect Effect { get; }

        
        /// <summary>
        /// The constructor of the potion class
        /// </summary>
        /// <param name="name"> The name of the item "Potion" </param>
        /// <param name="weight"> The weight of the potion </param>
        /// <param name="volume"> The volume of the potion </param>
        /// <param name="value"> The value of the potion in gold coins </param>
        /// <param name="use"> The value of the potion effect </param>
        /// <param name="effect"> The PotionEffect Enum </param>
        public Potion(ItemName name, float weight, float volume, float value, int use, PotionEffect effect) : base (name, weight, volume, value,use)
        {
            Effect = effect;
        }


        /// <summary>
        /// This method gets the item name
        /// </summary>
        /// <returns> A string representing the potion and its effect </returns>
        public override string ToString()
        {
            string ret = $"Potion of {Effect}";
            return ret;
        }
    }

    /// <summary>
    /// The healing potion class, derived from the potion class
    /// </summary>
    class HealingPotion : Potion
    {
        /// <summary>
        /// The HealingPotion constructor, with its default values
        /// </summary>
        public HealingPotion() : base(ItemName.HealingPotion,2.6f,4.8f,9.99f,10,PotionEffect.Healing) { }

        /// <summary>
        /// The method when using the healing potion
        /// </summary>
        /// <param name="player"> The player/hero to use the potion on </param>
        /// <returns> A boolean whether or not the item was used </returns>
        public bool UseItem(Player player)
        {
            Console.WriteLine($"You drank the {this.ToString()}, and gained {this.Use}");
            //player.IncreaseHealth(this.Use);
            return true;
        }


    }
    /// <summary>
    /// The RevengePotion class, derived from the Potion class
    /// </summary>
    class RevengePotion : Potion
    {
        /// <summary>
        /// The RevengerPotion constructor
        /// </summary>
        public RevengePotion(): base(ItemName.RevengePotion, 2.6f,4.8f,9.99f,-40,PotionEffect.Revenge) { }


        /// <summary>
        /// The method when using the item, returning damage taken to the damage sender
        /// </summary>
        /// <param name="merch"> The NPC to use the potion on </param>
        /// <returns> A boolean whether the potion was successfully used </returns>
        public bool UseItem(Merchant merch)
        {
            Console.WriteLine($"Will take a revenge from player by reducing health");
            return true;
        }
    }
    /// <summary>
    /// The PoisonPotion class, derived from the Potion class
    /// </summary>
    class PoisonPotion : Potion
    {
        /// <summary>
        /// The PoisonPotion constructor
        /// </summary>
        public PoisonPotion() : base(ItemName.PoisonPotion, 2.6f, 4.8f, 9.99f, 100, PotionEffect.Revenge) { }

        /// <summary>
        /// The method when using the PoisonPotion
        /// </summary>
        /// <param name="merch"> The NPC to use the item on </param>
        /// <returns> A boolean whether the potion was successfully used </returns>
        public bool UseItem(Merchant merch)
        {
            Console.WriteLine($"Will through on eyes of merchant to steal the stuff and reduce merchant's gold");
            return true;
        }
    }

    /// <summary>
    /// The Sword class, derived from the Weapon class
    /// </summary>
    class Sword : Weapon
    {        
        /// <summary>
        /// The Sword constructor, with default values
        /// </summary>
        public Sword() : base(ItemName.Sword, 3.8f, 5.6f, 14.99f, 40) { }

        /// <summary>
        /// The method when using the item
        /// </summary>
        /// <param name="merch"> The NPC to attack </param>
        /// <returns> A boolean whether the sword was successfully used </returns>
        public bool UseItem(Merchant merch)
        {
            Console.WriteLine($"Your sword accidentaly fell on {merch}");
            return true;
        }
    }

    class Staff : Weapon
    {
        /// <summary>
        /// The Staff constructor, with default values
        /// </summary>
        public Staff() : base(ItemName.Staff, 2.5f, 7f, 16.99f, 33) { }

        public bool UseItem(Merchant merch)
        {
            Console.WriteLine($"Your sword accidentaly fell on {merch}");
            return true;
        }
    }

    class Axe : Weapon
    {
        /// <summary>
        /// The Sword constructor, with default values
        /// </summary>
        public Axe() : base(ItemName.Axe, 3.8f, 5.6f, 14.99f, 40) { }

        /// <summary>
        /// The method when using the item
        /// </summary>
        /// <param name="merch"> The NPC to attack </param>
        /// <returns> A boolean whether the sword was successfully used </returns>
        public bool UseItem(Merchant merch)
        {
            Console.WriteLine($"Your axe accidentaly fell on {merch}");
            return true;
        }
    }

    /// <summary>
    /// The sword class, derived from the weapon class
    /// </summary>
    class Shield : Weapon
    { 
        /// <summary>
        /// The shield constructor, with default values
        /// </summary>
        public Shield() : base(ItemName.Shield, 3.8f, 5.6f, 14.99f, 40) { }

        /// <summary>
        /// The method to use when using the item
        /// </summary>
        /// <param name="merch"> The NPC to defend from </param>
        /// <returns> A boolean whether the shield successfully defended againts the NPC </returns>
        public bool UseItem(Merchant merch)
        {
            Console.WriteLine($"Accidently fell on your leg and you don't have one leg from now");
            return true;
        }
    }

    /// <summary>
    /// The Map class, derived from InventoryItem
    /// </summary>
    class Map : InventoryItem
    { 
        /// <summary>
        /// The Map constructor, with default values
        /// </summary>
        public Map() : base(ItemName.Map, 1, 0.5f, 5.5f,0) { }

        /// <summary>
        /// The method to use when using the item
        /// </summary>
        /// <returns> A boolean whether the map was successfully used </returns>
        public override bool UseItem()
        {
            Console.WriteLine($"Accidently fell on your leg and you don't have one leg from now");
            return true;
        }

    }

    /// <summary>
    /// The PotionEffect enums for generating potions.
    /// </summary>
    enum PotionEffect { Healing, Revenge, Poison }

    /// <summary>
    /// The ItemName enum for generating items.
    /// </summary>
    enum ItemName
    {
        Shield, HealingPotion, Map, Sword, RevengePotion, PoisonPotion, Staff, Axe
    }
}
