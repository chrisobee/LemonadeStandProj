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
        Player player = new Player();
        Random rand = new Random();
        Menu menu = new Menu();
        Store store = new Store();
        List<Day> days;
        int currentDay;
        
        //Constructor

        //Member Methods

        // O- Open/closed principle because this method for choosing the player's name allows for more functionality when multiplayer is added
        // The source code won't be changed to add more players; however, the new functionality can be added on
        public void ChoosePlayerName()
        {
            Console.WriteLine("Choose the name of the player");
            player.name = Console.ReadLine();
        }

        public void RunGame()
        {
            ChoosePlayerName();
            days = new List<Day>();
            int finalDay = 7;
            for (int i = 1; i <= 7; i++)
            {
                if(player.wallet.Money <= 0)
                {
                    break;
                }
                else
                {
                    currentDay = i;
                    InitializeDay(currentDay);
                    days[currentDay - 1].GenerateAmountOfCustomers();
                    menu.DisplayGameMenu(player, store, days, currentDay);
                    SalePhase(days[currentDay - 1]);
                }
            }
            Console.WriteLine($"GAME OVER\nYou made it to the {currentDay} day.\nYou made ${player.totalProfits}");
        }
        public void InitializeDay(int currentDay)
        {
            days.Add(new Day());
            Console.WriteLine($"It is {player.name}'s turn.");
            Console.WriteLine($"It is Day: {currentDay}");
            Console.WriteLine($"The Weather Condition is: {days[currentDay - 1].weather.condition}");
            Console.WriteLine($"The Temperature is: {days[currentDay - 1].weather.temp}F");
            Console.WriteLine($"Your current fundage is: ${player.wallet.Money}");
            Console.ReadLine();
        }

        public void SalePhase(Day day)
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
                    CheckForCustomerPurchase(day, customer);
                }
                firstPitcherCheck = false;
            }
            DisplayEndOfDay(day);
        }

        public void CheckForCustomerPurchase(Day day, Customer customer)
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

        public void DisplayEndOfDay(Day day)
        {
            Console.WriteLine($"You sold {day.cupsSold} cups and made ${day.profits}\nAll of your Ice Cubes have melted!");
            Console.WriteLine("--------------------------------------");
            Console.ReadLine();
            Console.Clear();
            player.inventory.iceCubes.Clear();
            
        }


    }
}
