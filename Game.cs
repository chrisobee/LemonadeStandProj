using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand_3DayStarter
{
    class Game
    {
        //Member Variables
        Player playerOne;
        Player playerTwo;
        Random rand = new Random();
        Menu menu = new Menu();
        Store store = new Store();
        List<Day> days;
        bool onePlayerGame;
        int currentDay;
        
        //Constructor

        //Member Methods

        // O- Open/closed principle because this method for choosing the player's name allows for more functionality when multiplayer is added
        // The source code won't be changed to add more players; however, the new functionality can be added on
        public void ChoosePlayerName(Player player)
        {
            Console.WriteLine("Choose the name of the player");
            player.name = Console.ReadLine();
        }

        public void RunGame()
        {
            bool restartGameCheck = false;
            List<Player> players = new List<Player>();
            ChooseGameType(players);
            do
            {
                
                days = new List<Day>();
                for (int i = 1; i <= 7; i++)
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
                            InitializeDay(currentDay, player);
                            days[currentDay - 1].GenerateAmountOfCustomers(player);
                            menu.DisplayGameMenu(player, store, days, currentDay, onePlayerGame, players);
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
                        playerOne = new Player();
                        ChoosePlayerName(playerOne);
                        players.Add(playerOne);
                        onePlayerGame = true;
                        correctInput = true;
                        break;
                    case 2:
                        playerOne = new Player();
                        ChoosePlayerName(playerOne);
                        playerTwo = new Player();
                        ChoosePlayerName(playerTwo);
                        players.Add(playerOne);
                        players.Add(playerTwo);
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

        public void DisplayEndOfGame(List<Player> players)
        {
            if (onePlayerGame)
            {
                Console.WriteLine($"GAME OVER\n{playerOne.name} made it to day {currentDay}.\nYou made ${playerOne.totalProfits} total gross profit");
            }
            else
            {
                Console.WriteLine($"GAME OVER\n{playerOne.name} made it to day {currentDay}.\nYou made ${playerOne.totalProfits} total gross profit");
                Console.WriteLine($"GAME OVER\n{playerTwo.name} made it to day {currentDay}.\nYou made ${playerTwo.totalProfits} total gross profit");
            }

            Console.WriteLine("Would you like to play again?\nY for yes\nN for no");
            Console.ReadLine();
        }
        public void InitializeDay(int currentDay, Player player)
        {
            days.Add(new Day());
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


    }
}
