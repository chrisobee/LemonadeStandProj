using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand_3DayStarter
{
    static class UserInterface
    {
        public static int GetNumberOfItems(string itemsToGet, double pricePerItem)
        {
            bool userInputIsAnInteger = false;
            int quantityOfItem = -1;

            while (!userInputIsAnInteger || quantityOfItem < 0)
            {
                Console.WriteLine("How many " + itemsToGet + "($" + pricePerItem + " per piece) would you like to buy?");
                Console.WriteLine("Please enter a positive integer (or 0 to cancel):");

                userInputIsAnInteger = Int32.TryParse(Console.ReadLine(), out quantityOfItem);
            }

            return quantityOfItem;
        }

        //S- Single Responsibility in this method because this method is used multiple times in the User Interface Class
        //This method just performs a int check from user input and does it efficiently.
        public static int CheckMenuInput()
        {
            bool correctInput = false;
            int userInput = 0;
            while (!correctInput)
            {
                correctInput = Int32.TryParse(Console.ReadLine(), out userInput);
                if (correctInput == false)
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            return userInput;
        }
        
        public static double CheckDoubleInput()
        {
            bool correctInput = false;
            double userInput = 0;
            while (!correctInput)
            {
                correctInput = Double.TryParse(Console.ReadLine(), out userInput);
                if(correctInput == false)
                {
                    Console.WriteLine("Invalid Input");
                }
            }
            return userInput;
        }

        public static bool CheckStringInput()
        {
            bool continueGame = false;
            bool correctInput = false;
            char tempCheck;
            do
            {
                tempCheck = char.Parse(Console.ReadLine().ToLower());
                switch (tempCheck)
                {
                    case 'y':
                        continueGame = true;
                        correctInput = false;
                        break;
                    case 'n':
                        continueGame = false;
                        correctInput = true;
                        break;
                    default:
                        correctInput = false;
                        break;
                }
            }
            while (correctInput == false);

            return continueGame;
        }

        public static bool CheckForEmptyPitcher(Pitcher pitcher)
        {
            bool needNewPitcher = false;
            if(pitcher.cupsLeftInPitcher == 0)
            {
                needNewPitcher = true;
            }
            return needNewPitcher;
        }

        public static bool CheckIfEnoughIngredients(Player player)
        {
            int cupsForOnePitcher = 10;
            bool enoughIngredients = true;
            if(player.inventory.cups.Count < cupsForOnePitcher ||
               player.inventory.lemons.Count < player.recipe.amountOfLemons ||
               player.inventory.sugarCubes.Count < player.recipe.amountOfSugarCubes ||
               player.inventory.iceCubes.Count < player.recipe.amountOfIceCubes)
            {
                enoughIngredients = false;
            }

            return enoughIngredients;
        }


    }
}
