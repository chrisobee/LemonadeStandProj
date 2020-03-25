using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand_3DayStarter
{
    class Game
    {
        //Member Variables
        public SaveData loadedData;
        Random rand = new Random();
        Menu menu = new Menu();
        Store store = new Store();
        List<Day> days;
        bool onePlayerGame;
        int currentDay;
        
        //Constructor

        //Member Methods
        public void RunGame()
        {
            bool restartGameCheck = false;
            List<Player> players = new List<Player>();
            bool newGame = ChooseNewGameOrLoadGame();
            RunNewOrLoadedGame(players, newGame);

            do
            {
                days = new List<Day>();
                for (int i = players[0].loadedCurrentDay; i <= 7; i++)
                {
                    foreach(Player player in players)
                    {
                        if (player.wallet.Money <= 0)
                        {
                            Console.WriteLine($"{player.name} ran out of money!!!");
                            break;
                        }
                        else
                        {
                            currentDay = i;
                            player.loadedCurrentDay = currentDay;
                            InitializeDay(currentDay, player);
                            days[currentDay - 1].GenerateAmountOfCustomers(player);
                            menu.DisplayGameMenu(player, store, days, currentDay, players);
                            SalePhase(days[currentDay - 1], player);
                        }
                    }
                }
                DisplayEndOfGame(players);
                if (UserInterface.CheckStringInput())
                {
                    restartGameCheck = true;
                }
            }
            while (restartGameCheck == false);
        }

        public bool ChooseNewGameOrLoadGame()
        {
            int userInput;
            bool newGame = true;
            bool incorrectInput = true;

            Console.WriteLine("Would you like to:\n1) Start New Game\n2) Load Game");
            while (incorrectInput)
            {
                userInput = UserInterface.CheckMenuInput();
                switch (userInput)
                {
                    case 1:
                        newGame = true;
                        incorrectInput = false;
                        break;
                    case 2:
                        newGame = false;
                        incorrectInput = false;
                        break;
                    default:
                        incorrectInput = true;
                        Console.WriteLine("Invalid Input");
                        break;
                }
            }
            return newGame;
        }

        public void RunNewOrLoadedGame(List<Player> players, bool newGame)
        {
            if (newGame)
            {
                ChooseGameType(players);
            }
            else
            {
                LoadGame(players);
            }
        }

        public void LoadGame(List<Player> players)
        {
            loadedData = JsonConvert.DeserializeObject<SaveData>(File.ReadAllText(@"C:\Users\chris\source\repos\LemonadeStand_3DayStarter\SaveFile.txt"));
            foreach (PlayerData data in loadedData.playerData)
            {
                Player player = new Player() { name = data.Name };
                players.Add(player);
                player.loadedCurrentDay = data.CurrentDay;
                player.wallet.LoadedMoney(data.Money);
                player.inventory.AddCupsToInventory(data.NumberOfCups);
                player.inventory.AddLemonsToInventory(data.NumberOfLemons);
                player.inventory.AddSugarCubesToInventory(data.NumberOfSugarCubes);
                player.inventory.AddIceCubesToInventory(data.NumberOfIceCubes);
            }
        }

        // O- Open/closed principle because this method for choosing the player's name allows for more functionality when multiplayer is added
        // The source code won't be changed to add more players; however, the new functionality can be added on
        public string ChoosePlayerName()
        {
            string name;
            Console.WriteLine("Choose the name of the player");
            name = Console.ReadLine();
            return name;
        }

        public void ChooseGameType(List<Player> players)
        {
            int userInput;
            bool correctInput;

            Console.WriteLine("Choose what type of game you would like to play\n1) Mr.Solo\n2) Two-Player");
            userInput = UserInterface.CheckMenuInput();
            do
            {
                switch (userInput)
                {
                    case 1:
                        players.Add(new Player() { name = ChoosePlayerName() });
                        onePlayerGame = true;
                        correctInput = true;
                        break;
                    case 2:
                        players.Add(new Player() { name = ChoosePlayerName() });
                        players.Add(new Player() { name = ChoosePlayerName() });
                        onePlayerGame = false;
                        correctInput = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Input");
                        correctInput = false;
                        break;
                }
            }
            while (correctInput == false);
        }

        public void InitializeDay(int currentDay, Player player)
        {
            for (int i = 1; i <= player.loadedCurrentDay; i++)
            {
                days.Add(new Day());
            }
            Console.WriteLine($"It is {player.name}'s turn.");
            Console.WriteLine($"It is Day: {currentDay}");
            Console.WriteLine($"The Weather Condition is: {days[currentDay - 1].weather.condition}");
            Console.WriteLine($"The Temperature is: {days[currentDay - 1].weather.temp}F");
            Console.WriteLine($"Your current fundage is: ${player.wallet.Money}");
            Console.ReadLine();
        }

        public void SalePhase(Day day, Player player)
        {
            bool firstPitcherCheck = UserInterface.CheckIfEnoughIngredients(player);

            while (firstPitcherCheck)
            {
                player.pitcher = player.MakeNewPitcher();

                foreach (Customer customer in day.customers)
                {
                    if (UserInterface.CheckForEmptyPitcher(player.pitcher))
                    {
                        if (UserInterface.CheckIfEnoughIngredients(player))
                        {
                            player.pitcher = player.MakeNewPitcher();
                        }
                        else
                        {
                            break;
                        }
                    }
                    CheckForCustomerPurchase(day, customer, player);
                }
                firstPitcherCheck = false;
            }
            DisplayEndOfDay(day, player);
        }

        public void CheckForCustomerPurchase(Day day, Customer customer, Player player)
        {
            bool multipleCups = true;

            customer.ChangeChancesDueToPrice(player.recipe.pricePerCup, day.standardPricePerCup);
            do
            {
                int buyCheck = rand.Next(0, customer.Chances);

                if (UserInterface.CheckForEmptyPitcher(player.pitcher))
                {
                    multipleCups = false;
                    break;
                }
                else if (customer.BuyLemonade(buyCheck))
                {
                    player.pitcher.cupsLeftInPitcher--;
                    day.cupsSold++;
                    player.wallet.ReceiveMoneyFromCustomer(player.recipe.pricePerCup);
                    day.profits += player.recipe.pricePerCup;
                    player.totalProfits += day.profits;
                }
                else
                {
                    multipleCups = false;
                }
            }
            while (multipleCups == true);
            
        }

        public void DisplayEndOfDay(Day day, Player player)
        {
            Console.WriteLine($"You sold {day.cupsSold} cups and made ${day.profits}\nAll of your Ice Cubes have melted!");
            Console.WriteLine("--------------------------------------");
            Console.ReadLine();
            Console.Clear();
            player.inventory.iceCubes.Clear();
            
        }

        public void DisplayEndOfGame(List<Player> players)
        {
            if (onePlayerGame)
            {
                Console.WriteLine($"GAME OVER\n{players[0].name} made it to day {currentDay}.\nYou made ${players[0].totalProfits} total gross profit");
            }
            else
            {
                Console.WriteLine($"GAME OVER\n{players[0].name} made it to day {currentDay}.\nYou made ${players[0].totalProfits} total gross profit");
                Console.WriteLine($"GAME OVER\n{players[1].name} made it to day {currentDay}.\nYou made ${players[1].totalProfits} total gross profit");
            }

            Console.WriteLine("Would you like to play again?\nY for yes\nN for no");
            Console.ReadLine();
        }
    }
}
