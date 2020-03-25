using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand_3DayStarter
{
    class Player
    {
        // member variables (HAS A)
        public string name;
        public double totalProfits;
        public int loadedCurrentDay = 1;
        public Inventory inventory;
        public Wallet wallet;
        public Recipe recipe = new Recipe();
        public Pitcher pitcher;

        // constructor (SPAWNER)
        public Player()
        {
            inventory = new Inventory();
            wallet = new Wallet();
        }

        // member methods (CAN DO)
        public void DisplayInventory()
        {
            Console.WriteLine($"You have {inventory.cups.Count} cups");
            Console.WriteLine($"You have {inventory.lemons.Count} lemons");
            Console.WriteLine($"You have {inventory.sugarCubes.Count} sugar cubes");
            Console.WriteLine($"You have {inventory.iceCubes.Count} ice cubes");
        }

        public void ChangeRecipe()
        {
            int userInput;
            bool finishedRecipe = false;

            while (!finishedRecipe)
            {
                Console.WriteLine($"Which would you like to change in the recipe?\n1) Price Per Cup\n2) Lemons\n3) Sugar Cubes\n4) Ice Cubes\n5) Finished Changing Recipe");
                userInput = UserInterface.CheckMenuInput();
                switch (userInput)
                {
                    case 1:
                        Console.WriteLine($"What would you like to change it to?\nCurrent Price per Cup: ${recipe.pricePerCup}");
                        recipe.pricePerCup = UserInterface.CheckDoubleInput();
                        Console.WriteLine($"New Price per Cup: ${recipe.pricePerCup}");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 2:
                        Console.WriteLine($"How many lemons per cup?\nCurrent Lemons per Cup: {recipe.amountOfLemons}");
                        recipe.amountOfLemons = UserInterface.CheckMenuInput();
                        Console.WriteLine($"New Lemons per Cup: {recipe.amountOfLemons}");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 3:
                        Console.WriteLine($"How many sugar cubes per cup?\nCurrent Sugar Cubes per Cup: {recipe.amountOfSugarCubes}");
                        recipe.amountOfSugarCubes = UserInterface.CheckMenuInput();
                        Console.WriteLine($"New Sugar Cubes per Cup: {recipe.amountOfSugarCubes}");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 4:
                        Console.WriteLine($"How many ice cubes per cup?\nCurrent Ice Cubes per Cup: {recipe.amountOfIceCubes}");
                        recipe.amountOfIceCubes = UserInterface.CheckMenuInput();
                        Console.WriteLine($"New Ice Cubes per Cup: {recipe.amountOfIceCubes}");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    default:
                        finishedRecipe = true;
                        break;

                }
            }
            
        }

        public Pitcher MakeNewPitcher()
        {
            pitcher = new Pitcher();
            inventory.RemoveCupsFromInventory(pitcher.cupsLeftInPitcher);
            inventory.RemoveLemonsFromInventory(recipe.amountOfLemons);
            inventory.RemoveSugarCubesFromInventory(recipe.amountOfSugarCubes);
            inventory.RemoveIceCubesFromInventory(recipe.amountOfIceCubes);
            return pitcher;
        }
    }
}
