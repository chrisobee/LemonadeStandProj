using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand_3DayStarter
{
    class Menu
    {
        public void DisplayGameMenu(Player player, Store store, List<Day> days, int currentDay)
        {
            bool dayStarted = false;

            do
            {
                Console.Clear();
                Console.WriteLine($"Type in your choice\n1) View Day Info\n2) View Inventory\n3) Buy Cups\n4) Buy Lemons\n5) Buy Sugar Cubes\n6) Buy Ice Cubes\n7) Change Recipe/Price per Cup\n8) Start Day");
                int userInput = UserInterface.CheckMenuInput();

                switch (userInput)
                {
                    case 1:
                        DisplayDayStats(player, days, currentDay);
                        Console.ReadLine();
                        break;
                    case 2:
                        player.DisplayInventory();
                        Console.ReadLine();
                        break;
                    case 3:
                        store.SellCups(player);
                        Console.WriteLine($"${player.wallet.Money} Left");
                        Console.ReadLine();
                        break;
                    case 4:
                        store.SellLemons(player);
                        Console.WriteLine($"${player.wallet.Money} Left");
                        Console.ReadLine();
                        break;
                    case 5:
                        store.SellSugarCubes(player);
                        Console.WriteLine($"${player.wallet.Money} Left");
                        Console.ReadLine();
                        break;
                    case 6:
                        store.SellIceCubes(player);
                        Console.WriteLine($"${player.wallet.Money} Left");
                        Console.ReadLine();
                        break;
                    case 7:
                        player.ChangeRecipe();
                        break;
                    case 8:
                        dayStarted = true;
                        break;
                    default:
                        Console.WriteLine("Choose one of the options");
                        break;
                }
            }
            while (dayStarted == false);

        }

        public void DisplayDayStats(Player player, List<Day> days, int currentDay)
        {
            Console.WriteLine($"It is {player.name}'s turn");
            Console.WriteLine($"It is Day: {currentDay}");
            Console.WriteLine($"The Weather Condition is: {days[currentDay - 1].weather.condition}");
            Console.WriteLine($"The Temperature is: {days[currentDay - 1].weather.temp}F");
            Console.WriteLine($"Your current fundage is: ${player.wallet.Money}");
        }
    }
}
