using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//This Part is created by Siva Sai and Pankaj
namespace PhaseTwo
{
    static class CharacterGenerator
    {
        public static Player HeroGenerator(string? file = null)
        {
            try
            {
                if (file == null)
                {
                    Console.WriteLine("Notice: No Player JSON file is available in parameter to take data.\n");
                    return SelectHeroPreset();
                }
                else
                {
                    if (ImportHero == null)
                    {
                        Console.WriteLine("Your imported File is not correct try again\n");
                        throw new System.Runtime.CompilerServices.SwitchExpressionException();
                    }
                    else
                    {
                        return ImportHero(file);
                    }
                }
            }
            catch (System.Runtime.CompilerServices.SwitchExpressionException)
            {
                //in case hero import is failed
                return SelectHeroPreset();
            }
        }
        static Player SelectHeroPreset()
        {
            (int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) localData;
            Console.WriteLine("Which hero you would like to choose?\n1 For Knight\n2 For Wizard\n3 For ValKery\n");
            int selection = ConsoleHelper.GetSelection(1, 3, "Our selection menu not getting what do you want to do?.\n");
            Player forShow;
            switch (selection)
            {
                case 1:
                    Console.WriteLine("\nYou have chosen Knight. It has the following preset powers\n");
                    forShow = new Knight();
                    forShow.Show();
                    Console.WriteLine($"\nWould you like to customise Knight according to your self or you want the preset powers?\n1 For Customisation\n2 For Default powers\n");
                    break;
                case 2:
                    Console.WriteLine("\nYou have chosen Wizard. It has the following preset powers\n");
                    forShow = new Wizard();
                    forShow.Show();
                    Console.WriteLine($"\nWould you like to customise Wizard according to your self or you want the preset powers?\n1 For Customisation\n2 For Default powers\n");
                    break;
                case 3:
                    Console.WriteLine("\nYou have chosen Valkery. It has the following preset powers\n");
                    forShow = new ValKery();
                    forShow.Show();
                    Console.WriteLine($"\nWould you like to customise Valkery according to your self or you want the preset powers?\n1 For Customisation\n2 For Default powers\n");
                    break;
                default: //There is no default as we have filtered all values already
                    break;
            }
            int selectionForCustomisation = ConsoleHelper.GetSelection(1, 2, "Our customisation menu not getting what do you want to do?.\n");
            if (selectionForCustomisation == 1)
            {
                localData = Customisation();
                return selection switch
                {
                    1 => new Knight(localData.strength, localData.defence, localData.intelligence, localData.vitality, localData.luck, localData.weaponUse, localData.dodge),
                    2 => new Wizard(localData.strength, localData.defence, localData.intelligence, localData.vitality, localData.luck, localData.weaponUse, localData.dodge),
                    3 => new ValKery(localData.strength, localData.defence, localData.intelligence, localData.vitality, localData.luck, localData.weaponUse, localData.dodge)
                };
            }
            else if (selectionForCustomisation == 2)
            {
                return selection switch
                {
                    1 => new Knight(),
                    2 => new Wizard(),
                    3 => new ValKery()
                };
            }
            return null;
        }
        /// <summary>
        /// this method will take all types of powers from user input and store them as tuple data type
        /// </summary>
        /// <returns>tuple variable having all user input powers for hero</returns>
        static  (int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) Customisation()
        {
            int maxPower = 100;
            int i = 0;
            int[] attributes = new int[7];
            string[] options = new string[] { "strength", "defence", "intelligence", "vitality","luck", "weaponUse", "dodge\n" };

            foreach (var item in options)
            {
                Console.WriteLine($"Please input your desired {item} You can choose power upto: {maxPower} powers\n");
                attributes[i]=ConsoleHelper.GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.\n");
                maxPower -= attributes[i];
                if (maxPower == 0)
                {
                    return (attributes[0], attributes[1], attributes[2], attributes[3], attributes[4], attributes[5], attributes[6]);
                }
                i++;
            }
            return (attributes[0], attributes[1], attributes[2], attributes[3], attributes[4], attributes[5], attributes[6]);
        }
        /// <summary>
        /// This method will parse the json file and give imported values to heroes 
        /// </summary>
        /// <param name="file">file location where json file is located</param>
        /// <returns>a hero type inherted from Player</returns>
        static Player ImportHero(string file)
        {
            int sum;
            string text = System.IO.File.ReadAllText(file);
            ImportPlayerData importValues = JsonSerializer.Deserialize<ImportPlayerData>(text);
            sum = importValues.Strength + importValues.Defence + importValues.Intelligence + importValues.Vitality + importValues.Luck + importValues.WeaponUse + importValues.Dodge;
            if (importValues.Character == 1)
            {
                
                if (sum > 100)
                {
                    Console.WriteLine("You have imported more power than allowed power\n");
                }
                else
                {
                    return new Knight(importValues.Strength, importValues.Defence, importValues.Intelligence, importValues.Vitality, importValues.Luck, importValues.WeaponUse, importValues.Dodge);
                }
            }
            else if (importValues.Character == 2)
            {
                if (sum > 100 || sum < 0)
                {
                    Console.WriteLine("You have imported more power than allowed power\n");
                }
                else
                {
                    return new Wizard(importValues.Strength, importValues.Defence, importValues.Intelligence, importValues.Vitality, importValues.Luck, importValues.WeaponUse, importValues.Dodge);
                }
            }
            else if (importValues.Character == 3)
            {
                if (sum > 100 || sum < 0)
                {
                    Console.WriteLine("You have imported more power than allowed power\n");
                }
                else
                {
                    return new ValKery(importValues.Strength, importValues.Defence, importValues.Intelligence, importValues.Vitality, importValues.Luck, importValues.WeaponUse, importValues.Dodge);
                }
            }
            else
            {
                Console.WriteLine("Your imported player info is not valid please consider character values 1,2,3 will select Knight,Wizard,ValKery simultaneously.\n");
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
}
