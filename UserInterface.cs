using System;
using System.Collections.Generic;
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

        public static bool CheckForEmptyPitcher(Pitcher pitcher)
        {
            bool needNewPitcher = false;
            if(pitcher.cupsLeftInPitcher == 0)
            {
                needNewPitcher = true;
            }
            return needNewPitcher;
        }

        public static bool CheckIfEnoughIngredients(List<Cup> cups, List<Lemon> lemons, List<SugarCube> sugarCubes, List<IceCube> iceCubes, Recipe recipe, Pitcher pitcher)
        {
            bool enoughIngredients = true;
            if(cups.Count < pitcher.cupsLeftInPitcher ||
               lemons.Count < recipe.amountOfLemons ||
               sugarCubes.Count < recipe.amountOfSugarCubes ||
               iceCubes.Count < (recipe.amountOfIceCubes * pitcher.cupsLeftInPitcher))
            {
                enoughIngredients = false;
            }

            return enoughIngredients;
        }


    }
}
