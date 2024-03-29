﻿namespace PhaseTwo
{
    /// <summary>
    /// Phase 1 Object Oriented Programming II - Group 8
    /// Authors:
    /// Nicholas St-Jacques - A00204011
    /// Rama Venkata Siva Sai Nuvvula - A00240120
    /// Pankaj - A00244692
    /// Ruchit
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            int packMaxItems = 10;
            float packMaxVolume = 20;
            float packMaxWeight = 30;
            //Creates the player inventory and adds a few items
            Inventory pack = new Inventory(packMaxItems, packMaxVolume, packMaxWeight);
            pack.Add(new Sword());
            pack.Add(new Map());
            Player player = CharacterGenerator.HeroGenerator();
            Console.WriteLine($"You have chosen: {player.GetName()} Below is your chosen powers for player");
            player.Show();

            //Creates the player with their starter inventory
            player.HeroAddLocationAndInventory(new Location(0, 0), pack);
            // Player player = new Player(new Location(0, 0), pack,ch.HeroGenerator(@""));


            //  Player? player = cg.HeroGenerator(@"D:\Cs\PhaseTwo\PhaseTwo\bin\Debug\net6.0\Player.json", new Location(0, 0), pack);
            //Creates the merchant
            //Merchant merch1 = new Merchant(new Location(0, 0));

            //Interact with the player
            //merch1.Interact(player);

            //Creates the other 3 merchants
            //PotionMerchant potionMerchant = new PotionMerchant(new Location(0, 0));
            //ArmorMerchant armorMerchant = new ArmorMerchant(new Location(0, 0));
            //WeaponMerchant weaponMerchant = new WeaponMerchant(new Location(0, 0));

            EquipementTester.AddEquipment(player);
            EquipementTester.RemoveEquipment(player);
        }
    }
}