using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
//This Part is created by Siva Sai and Pankaj
namespace PhaseTwo
{
    interface ChooseAHero
    {
        Player HeroGenerator(string? file = null);
    }
    class CharacterGenerator:ChooseAHero
    {
        public Player HeroGenerator(string? file = null)
        {
            try
            {
                if (file == null)
                {
                    Console.WriteLine("Notice: No Player JSON file is available in parameter to take data.");
                    return SelectHeroPreset();
                }
                else
                {
                    if (ImportHero == null)
                    {
                        Console.WriteLine("Your imported File is not correct try again");
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
        Player SelectHeroPreset()
        {
            (int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) localData;
            Console.WriteLine("Which hero you would like to choose?\n1 For Knight\n2 For Wizard\n3 For ValKery");
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
        (int strength, int defence, int intelligence, int vitality, int luck, int weaponUse, int dodge) Customisation()
        {
            int maxPower = 100;
            int i = 0;
            int[] attributes = new int[7];
            string[] options = new string[] { "strength", "defence", "intelligence", "vitality","luck", "weaponUse", "dodge" };

            foreach (var item in options)
            {
                Console.WriteLine($"Please input your desired {item} You can choose power upto: {maxPower} powers");
                attributes[i]=GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.");
                maxPower -= attributes[i];
                if (maxPower == 0)
                {
                    return (attributes[0], attributes[1], attributes[2], attributes[3], attributes[4], attributes[5], attributes[6]);
                }
                i++;
            }
            #region
            //Console.WriteLine($"Please input your desired strength You can choose power upto: {maxPower} powers");
            //localStrength = GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.");
            //maxPower -= localStrength;
            //Console.WriteLine($"Your strength is set to {localStrength}.");
            //if (maxPower == 0)
            //{
            //    return (localStrength, localDefence, localIntelligence, localVitality, localLuck, localWeaponUse, localDodge);
            //}

            //Console.WriteLine($"Please input your desired defence You can choose power upto: {maxPower} powers");
            //localDefence = GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.");
            //maxPower -= localDefence;
            //Console.WriteLine($"Your defence is set to {localDefence}.");
            //if (maxPower == 0)
            //{
            //    return (localStrength, localDefence, localIntelligence, localVitality, localLuck, localWeaponUse, localDodge);
            //}

            //Console.WriteLine($"Please input your desired intelligence You can choose power upto: {maxPower} powers");
            //localIntelligence = GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.");
            //maxPower -= localIntelligence;
            //Console.WriteLine($"Your intelligence is set to {localIntelligence}.");
            //if (maxPower == 0)
            //{
            //    return (localStrength, localDefence, localIntelligence, localVitality, localLuck, localWeaponUse, localDodge);
            //}

            //Console.WriteLine($"Please input your desired vitality You can choose power upto: {maxPower} powers");
            //localVitality = GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.");
            //maxPower -= localVitality;
            //Console.WriteLine($"Your vitality is set to {localVitality}.");
            //if (maxPower == 0)
            //{
            //    return (localStrength, localDefence, localIntelligence, localVitality, localLuck, localWeaponUse, localDodge);
            //}

            //Console.WriteLine($"Please input your desired luck You can choose power upto: {maxPower} powers");
            //localLuck = GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.");
            //maxPower -= localLuck;
            //Console.WriteLine($"Your luck is set to {localLuck}.");
            //if (maxPower == 0)
            //{
            //   return (localStrength, localDefence, localIntelligence, localVitality, localLuck, localWeaponUse, localDodge);
            //}
            //Console.WriteLine($"Please input your desired weapon use You can choose power upto: {maxPower} powers");
            //localWeaponUse = GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.");
            //maxPower -= localWeaponUse;
            //Console.WriteLine($"Your weapon use is set to {localWeaponUse}.");
            //if (maxPower == 0)
            //{
            //    return (localStrength, localDefence, localIntelligence, localVitality, localLuck, localWeaponUse, localDodge);
            //}
            //Console.WriteLine($"Please input your desired dodge You can choose power upto: {maxPower} powers");
            //localDodge = GetSelection(0, maxPower, "Our customisation menu not getting what do you want to do?.");
            //maxPower -= localDodge;
            //Console.WriteLine($"Your dodge is set to {localDodge}.");
            //if (maxPower == 0)
            //{
            //    return (localStrength, localDefence, localIntelligence, localVitality, localLuck, localWeaponUse, localDodge);
            //}
            #endregion
            return (attributes[0], attributes[1], attributes[2], attributes[3], attributes[4], attributes[5], attributes[6]);
        }
        /// <summary>
        /// This method will parse the json file and give imported values to heroes 
        /// </summary>
        /// <param name="file">file location where json file is located</param>
        /// <returns>a hero type inherted from Player</returns>
        Player ImportHero(string file)
        {
            int sum;
            string text = System.IO.File.ReadAllText(file);
            ImportPlayerData importValues = JsonSerializer.Deserialize<ImportPlayerData>(text);
            sum = importValues.Strength + importValues.Defence + importValues.Intelligence + importValues.Vitality + importValues.Luck + importValues.WeaponUse + importValues.Dodge;
            if (importValues.Character == 1)
            {
                
                if (sum > 100)
                {
                    Console.WriteLine("You have imported more power than allowed power");
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
                    Console.WriteLine("You have imported more power than allowed power");
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
                    Console.WriteLine("You have imported more power than allowed power");
                }
                else
                {
                    return new ValKery(importValues.Strength, importValues.Defence, importValues.Intelligence, importValues.Vitality, importValues.Luck, importValues.WeaponUse, importValues.Dodge);
                }
            }
            else
            {
                Console.WriteLine("Your imported player info is not valid please consider character values 1,2,3 will select Knight,Wizard,ValKery simultaneously.");
            }
            return null;
        }
        /// <summary>
        /// this method take two integers, one is minimum and the other is maximum integer values entered by user it also have 
        /// a string error message that will be shown when input is not in the given range
        /// </summary>
        /// <param name="min">minimum allowed userinput int value</param>
        /// <param name="max">maximum allowed userinput int value</param>
        /// <param name="errorMsg">Error message to be shown when user input is not valid</param>
        /// <returns>returns the sanitized int input value</returns>
        private int GetSelection(int min, int max, string errorMsg)
        {
            int? selection = SanitizeInput(Console.ReadLine(), min, max);
            while (selection == null)
            {
                Console.WriteLine(errorMsg);
                selection = SanitizeInput(Console.ReadLine(), min, max);
            }
            return (int)selection; // 'selection' cannot be null here
        }
        /// <summary>
        /// it will convert the string input value into integer input value and also checks that value is b/w min and max int value we have given
        /// </summary>
        /// <param name="input">string input to convert int value</param>
        /// <param name="min">minimum allowed userinput int value</param>
        /// <param name="max">maximum allowed userinput int value</param>
        /// <returns>returns the filtered int values that we need for specific task</returns>
        public int? SanitizeInput(string input, int min, int max)
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
}
