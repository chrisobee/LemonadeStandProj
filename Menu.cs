using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand_3DayStarter
{
    class Menu
    {
        public void DisplayGameMenu(Player player, Store store)
        {
            bool dayStarted = false;

            do
            {
                Console.WriteLine($"Type in your choice\n1) View Inventory\n2) Buy Cups\n3) Buy Lemons\n4) Buy Sugar Cubes\n5) Buy Ice Cubes\n6) Change Recipe/Price per Cup\n7) Start Day");
                int userInput = UserInterface.CheckMenuInput();

                switch (userInput)
                {
                    case 1:
                        player.DisplayInventory();
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 2:
                        store.SellCups(player);
                        Console.WriteLine($"${player.wallet.Money} Left");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 3:
                        store.SellLemons(player);
                        Console.WriteLine($"${player.wallet.Money} Left");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 4:
                        store.SellSugarCubes(player);
                        Console.WriteLine($"${player.wallet.Money} Left");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 5:
                        store.SellIceCubes(player);
                        Console.WriteLine($"${player.wallet.Money} Left");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 6:
                        player.ChangeRecipe();
                        break;
                    case 7:
                        dayStarted = true;
                        break;
                    default:
                        Console.WriteLine("Choose one of the options");
                        continue;
                }
            }
            while (dayStarted == false);

        }
    }
}
