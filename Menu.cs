using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand_3DayStarter
{
    class Menu
    {
        public void DisplayGameMenu(Player currentPlayer, Store store, List<Day> days, int currentDay, List<Player> allPlayers)
        {
            bool dayStarted = false;

            do
            {
                Console.Clear();
                Console.WriteLine($"Type in your choice\n" +
                                  $"1) View Day Info\n" +
                                  $"2) View Inventory\n" +
                                  $"3) Buy Cups\n" +
                                  $"4) Buy Lemons\n" +
                                  $"5) Buy Sugar Cubes\n" +
                                  $"6) Buy Ice Cubes\n" +
                                  $"7) Change Recipe/Price per Cup\n" +
                                  $"8) Save Game\n" +
                                  $"9) Start Day\n");
                                 
                int userInput = UserInterface.CheckMenuInput();

                switch (userInput)
                {
                    case 1:
                        DisplayDayStats(currentPlayer, days, currentDay);
                        Console.ReadLine();
                        break;
                    case 2:
                        currentPlayer.DisplayInventory();
                        Console.ReadLine();
                        break;
                    case 3:
                        store.SellCups(currentPlayer);
                        Console.WriteLine($"${currentPlayer.wallet.Money} Left");
                        Console.ReadLine();
                        break;
                    case 4:
                        store.SellLemons(currentPlayer);
                        Console.WriteLine($"${currentPlayer.wallet.Money} Left");
                        Console.ReadLine();
                        break;
                    case 5:
                        store.SellSugarCubes(currentPlayer);
                        Console.WriteLine($"${currentPlayer.wallet.Money} Left");
                        Console.ReadLine();
                        break;
                    case 6:
                        store.SellIceCubes(currentPlayer);
                        Console.WriteLine($"${currentPlayer.wallet.Money} Left");
                        Console.ReadLine();
                        break;
                    case 7:
                        currentPlayer.ChangeRecipe();
                        break;
                    case 8:
                        SaveGame(allPlayers, days);
                        break;
                    case 9:
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

        public void SaveGame(List<Player> players, List<Day> days)
        {
            int daysSize = days.Count();
            SaveData saveData = new SaveData();
            foreach(Player player in players)
            {
                PlayerData data = new PlayerData()
                {
                    Name = player.name,
                    Money = player.wallet.Money,
                    CurrentDay = player.loadedCurrentDay,
                    NumberOfCups = player.inventory.cups.Count(),
                    NumberOfLemons = player.inventory.lemons.Count(),
                    NumberOfSugarCubes = player.inventory.sugarCubes.Count(),
                    NumberOfIceCubes = player.inventory.iceCubes.Count()
                };
                saveData.playerData.Add(data);
            }
            string json = JsonConvert.SerializeObject(saveData);
            string path = @"C:\Users\chris\source\repos\LemonadeStand_3DayStarter\SaveFile.txt";
            File.WriteAllText(path, json);
        }
    }
}
