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
        public void ChoosePlayerName()
        {
            Console.WriteLine("Choose the name of the player");
            player.name = Console.ReadLine();
        }

        public void RunGame()
        {
            days = new List<Day>();
            for (int i = 1; i <= 7; i++)
            {
                currentDay = i;
                InitializeDay(currentDay);
                GenerateAmountOfCustomers(days[currentDay - 1], days[currentDay - 1].weather.condition, days[currentDay - 1].weather.temp);
                menu.DisplayGameMenu(player, store, days, currentDay);
                SalePhase(days[currentDay - 1]);
            }
        }
        public void InitializeDay(int currentDay)
        {
            days.Add(new Day());
            Console.WriteLine($"It is Day: {currentDay}");
            Console.WriteLine($"The Weather Condition is: {days[currentDay - 1].weather.condition}");
            Console.WriteLine($"The Temperature is: {days[currentDay - 1].weather.temp}F");
            Console.WriteLine($"Your current fundage is: ${player.wallet.Money}");
            Console.ReadLine();
        }

        public void GenerateAmountOfCustomers(Day day, string condition, int temp)
        {
            int numberOfCustomers;
            if (temp <= 70)
            {
                if (day.weather.goodWeatherConditions.Contains(condition))
                {
                    numberOfCustomers = 30;
                    GenerateCustomers(day, numberOfCustomers);
                }
                else if (day.weather.badWeatherConditions.Contains(condition))
                {
                    numberOfCustomers = 20;
                    GenerateCustomers(day, numberOfCustomers);
                }
                else
                {
                    numberOfCustomers = 10;
                    GenerateCustomers(day, numberOfCustomers);
                }
            }
            else if (temp <= 90)
            {
                if (day.weather.goodWeatherConditions.Contains(condition))
                {
                    numberOfCustomers = 40;
                    GenerateCustomers(day, numberOfCustomers);
                }
                else if (day.weather.badWeatherConditions.Contains(condition))
                {
                    numberOfCustomers = 30;
                    GenerateCustomers(day, numberOfCustomers);
                }
                else
                {
                    numberOfCustomers = 20;
                    GenerateCustomers(day, numberOfCustomers);
                }
            }
            else
            {
                if (day.weather.goodWeatherConditions.Contains(condition))
                {
                    numberOfCustomers = 50;
                    GenerateCustomers(day, numberOfCustomers);
                }
                else if (day.weather.badWeatherConditions.Contains(condition))
                {
                    numberOfCustomers = 40;
                    GenerateCustomers(day, numberOfCustomers);
                }
                else
                {
                    numberOfCustomers = 30;
                    GenerateCustomers(day, numberOfCustomers);
                }
            }
        }

        public void GenerateCustomers(Day day, int numberOfCustomers)
        {
            for (int i = 0; i <= numberOfCustomers; i++)
            {
                day.customers.Add(new Customer($"{i}"));
            }
            GenerateCustomerChances(day);
        }

        public void GenerateCustomerChances(Day day)
        {
            foreach (Customer customer in day.customers)
            {
                customer.Chances = day.GenerateLikelihood(day.weather.condition, day.weather.temp);
            }
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
