using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhaseTwo
{
    /// <summary>
    /// The Base Merchant class, derived from the IInteractables interface.
    /// </summary>
    class Merchant : IInteractable
    {
        // The Inventory of the merchant.
        protected Inventory _inventory;
        // The location of the merchant on the map.
        public Location Location { get; set; }
        // The amount of gold the merchant has.
        public float goldAmount { get; protected set; }
        
        /// <summary>
        /// The Merchant constructor when specifying an inventory.
        /// </summary>
        /// <param name="location"> The starting location of the merchant </param>
        /// <param name="inventory"> The inventory to start the merchant with </param>
        public Merchant(Location location, Inventory inventory)
        {
            _inventory = inventory;
            goldAmount = 250;
            Location = location;
        }

        /// <summary>
        /// The merchant constructor when ONLY specifying a location.
        /// </summary>
        /// <param name="location"> The starting location of the merchant </param>
        public Merchant(Location location)
        {
            _inventory = new Inventory(30, 30, 30);
            _inventory.Add(new Sword());
            _inventory.Add(new Shield());
            _inventory.Add(new Map());
            _inventory.Add(new HealingPotion());

            goldAmount = 250;

            Location = location;
        }

        /// <summary>
        /// When trading, changes the amount of gold the merchant has.
        /// </summary>
        /// <param name="amount"> The amount to update the goldAmount of the merchant </param>
        /// <returns> A boolean whether the transfer was successfull </returns>
        public bool TradeGold(float amount)
        {
            if ((goldAmount + amount) >= 0)
            {
                goldAmount += amount;
                return true;
            }
            return false;
        }

        /// <summary>
        /// The method used when the PLAYER interacts with the MERCHANT, displays a menu of options to BUY/SELL/LEAVE.
        /// </summary>
        /// <param name="player"> The player instace to interact with </param>
        public void Interact(Player player)
        {
            bool interactMore = true;
            do
            {
                Console.WriteLine("\nYou approach the merchant. Before you can utter a word, he mutters \"Buying or selling? I've got all the odds and ends.\"");
                Console.WriteLine("\nWould do you want to do?");
                Console.WriteLine("1 - Buy");
                Console.WriteLine("2 - Sell");
                Console.WriteLine("3 - Leave\n");
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            SellMenu(player);
                            break;
                        case 2:
                            BuyMenu(player);
                            break;
                        case 3:
                            throw new System.Runtime.CompilerServices.SwitchExpressionException();
                        default:
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
                    Console.WriteLine("\nSee you next time... if the dark lord doesn't first.");
                    interactMore = false;
                }
                Console.ResetColor();
            } while (interactMore);
        }

        /// <summary>
        /// The method used when the PLAYER selects to BUY items from the Merchant i.e. the merchant is SELLing items.
        /// </summary>
        /// <param name="player"> The player instace to buy from </param>
        void SellMenu(Player player)
        {
            bool sellMore = true;
            do
            {
                (string items, int itemCount) indexedInventory = _inventory.GetIndexedInventory();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYour inventory is currently at {player.InventoryCurrentCount()}/{player.InventoryMaxCount()} items, {player.InventoryCurrentWeight()}/{player.InventoryMaxWeight()} weight," +
                                  $" {player.InventoryCurrentVolume()}/{player.InventoryMaxVolume()} volume, and {player.InventoryGetTotalValue()} total value. You have {player.GetGold()} gold coins.");
                Console.ResetColor();
                Console.WriteLine("\n\"Here are my wares...\" Says the merchant.");
                Console.WriteLine("\nEnter the item index of the item you would like to purchase");
                Console.WriteLine(indexedInventory.items);
                Console.WriteLine($"\t{indexedInventory.itemCount}\t-\tExit\n");
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice >= 1 & choice <= indexedInventory.itemCount - 1)
                    {
                        SellItem(player, choice);
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
                    Console.WriteLine("\n\"Hmm... Anything else?\" Says the merchant.");
                    sellMore = false;
                }
                Console.ResetColor();
            } while (sellMore);
        }
        /// <summary>
        /// The method used when the PLAYER selects and item to SELL to the merchant.
        /// </summary>
        /// <param name="player"> The player buying the item </param>
        /// <param name="itemIndex"> The index of the item in the merchants inventory </param>
        /// <returns></returns>
        bool SellItem(Player player, int itemIndex)
        {
            itemIndex = itemIndex - 1;
            InventoryItem item = _inventory.GetItemAtIndex(itemIndex);
            float itemValue = item.Value;

            if (player.GetGold() >= itemValue & player.AddItem(item))
            {
                item = _inventory.RemoveAt(itemIndex);

                //Remove gold from players inventory
                player.UpdateGold(itemValue * -1);
                //Add gold from this merchant
                TradeGold(itemValue);
                Console.WriteLine($"\nYou have purchased a {item} for {itemValue} gold coins.\n");
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nYou have {player.GetGold()} gold coins, you need {itemValue} gold coins to purchase the {item}.\n");
            }
            return false;
        }

        /// <summary>
        /// The method used when the player selects to SELL items to the Merchant i.e. the merchant is BUYing items.
        /// </summary>
        /// <param name="player"> The player instance selling the items </param>
        void BuyMenu(Player player)
        {
            bool buyMore = true;
            do
            {
                (string items, int itemCount) indexedInventory = player.GetIndexedInventory();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nYour inventory is currently at {player.InventoryCurrentCount()}/{player.InventoryMaxCount()} items, {player.InventoryCurrentWeight()}/{player.InventoryMaxWeight()} weight," +
                                  $" {player.InventoryCurrentVolume()}/{player.InventoryMaxVolume()} volume, and {player.InventoryGetTotalValue()} total value. You have {player.GetGold()} gold coins.");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\nThe merchants inventory is currently at {_inventory.CurrentCount}/{_inventory.MaxCount} items, {_inventory.CurrentWeight}/{_inventory.MaxWeight} weight," +
                                  $" {_inventory.CurrentVolume}/{_inventory.MaxVolume} volume, and {_inventory.TotalValue} total value. He has {goldAmount} gold coins.");
                Console.ResetColor();
                Console.WriteLine("\n\"Here are my wares...\" You say.");
                Console.WriteLine("\nEnter the item index of the item you would like to sell");
                Console.WriteLine(indexedInventory.items);
                Console.WriteLine($"\t{indexedInventory.itemCount}\t-\tExit\n");
                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice >= 1 & choice <= indexedInventory.itemCount - 1)
                    {
                        BuyItem(player, choice);
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
                    Console.WriteLine("\nHmm... Anything else?");
                    buyMore = false;
                }
                Console.ResetColor();
            } while (buyMore);
        }

        /// <summary>
        /// The method used when the player selects an item to SELL to the Merchant.
        /// </summary>
        /// <param name="player"> The player instance selling the item </param>
        /// <param name="itemIndex"> The index of the item in the players inventory </param>
        /// <returns></returns>
        bool BuyItem(Player player, int itemIndex)
        {
            itemIndex = itemIndex - 1;
            InventoryItem item = player.GetItemAtIndex(itemIndex);
            float itemValue = item.Value;

            if (player.GetGold() >= item.Value & _inventory.Add(item))  //Change here
            {
                player.RemoveItem(item);
                goldAmount = goldAmount - item.Value;
                player.UpdateGold(item.Value);
                Console.WriteLine($"You have successfuly sold {item.Name} and your current balance is {player.GetGold()}");
                return true;              
            }
            else
            {
                Console.WriteLine($"The Merchant doesnt have enough gold. The merchant balance is {goldAmount}.");
                return false;
            }
        }    
    }

    /// <summary>
    /// The PotionMerchant class, derived from the Merchant class
    /// </summary>
    class PotionMerchant : Merchant
    {
        /// <summary>
        /// The PotionMerchant constructor with ONLY starting location
        /// </summary>
        /// <param name="location"> The starting location of the PotionMerchant </param>
        public PotionMerchant(Location location) : base(location)
        {
            _inventory = new Inventory(30, 30, 30);
            _inventory.Add(new HealingPotion());
            _inventory.Add(new HealingPotion());

            goldAmount = 250;

            Location = location;
        }

        /// <summary>
        /// The PotionMerchant constructor to use when specifying the inventory
        /// </summary>
        /// <param name="location"> The starting location of the PotionMerchant </param>
        /// <param name="inventory"> The starting inventory of the PotionMerchant </param>
        public PotionMerchant(Location location, Inventory inventory) : base(location, inventory)
        {
            _inventory = inventory;
            goldAmount = 250;
            Location = location;
        }
    }

    /// <summary>
    /// The ArmorMerchant class, derived from the Merchant class
    /// </summary>
    class ArmorMerchant : Merchant
    {
        /// <summary>
        /// The ArmorMerchant constructor with ONLY starting location
        /// </summary>
        /// <param name="location"> The starting location of the ArmorMerchant </param>
        public ArmorMerchant(Location location) : base(location)
        {
            _inventory = new Inventory(30, 30, 30);
            _inventory.Add(new Shield());
            _inventory.Add(new Shield());

            goldAmount = 250;

            Location = location;
        }

        /// <summary>
        /// The ArmorMerchant constructor to use when specifying the inventory
        /// </summary>
        /// <param name="location"> The starting location of the ArmorMerchant </param>
        /// <param name="inventory"> The starting inventory of the ArmorMerchant </param>
        ArmorMerchant(Location location, Inventory inventory) : base(location, inventory)
        {
            _inventory = inventory;

            goldAmount = 250;

            Location = location;
        }
    }

    class WeaponMerchant : Merchant
    {
        /// <summary>
        /// The WeaponMerchant constructor with ONLY starting location
        /// </summary>
        /// <param name="location"> The starting location of the WeaponMerchant </param>
        public WeaponMerchant(Location location) : base(location)
        {
            _inventory = new Inventory(30, 30, 30);
            _inventory.Add(new Sword());
            _inventory.Add(new Sword());

            goldAmount = 250;

            Location = location;
        }

        /// <summary>
        /// The WeaponMerchant constructor to use when specifying the inventory
        /// </summary>
        /// <param name="location"> The starting location of the WeaponMerchant </param>
        /// <param name="inventory"> The starting inventory of the WeaponMerchant </param>
        public WeaponMerchant(Location location, Inventory inventory) : base(location, inventory)
        {
            _inventory = inventory;

            goldAmount = 250;

            Location = location;
        }
    }
}
