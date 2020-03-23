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
                InitializeDay(i);
                GenerateAmountOfCustomers(days[currentDay], days[currentDay].weather.condition, days[currentDay].weather.temp);
                menu.DisplayGameMenu(player, store);
            }
            

        }

        public void InitializeDay(int i)
        {
            currentDay = i;
            days.Add(new Day(currentDay));
            Console.WriteLine($"It is Day: {days[currentDay]}");
            Console.WriteLine($"The Weather Condition is: {days[currentDay].weather.condition}");
            Console.WriteLine($"The Temperature is: {days[currentDay].weather.temp}F");
            Console.WriteLine($"Your current fundage is: ${player.wallet.Money}");
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
        }


        public void SalePhase()
        {

        }


    }
}
