using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand_3DayStarter
{
    class Day
    {
        //Member Variables
        public Weather weather = new Weather();
        public List<Customer> customers;
        public double standardPricePerCup;
        public double profits;
        public int cupsSold;
        Random rand = new Random();

        //Constructor
        public Day()
        {
            weather.condition = weather.GenerateRandomCondition();
            weather.temp = weather.GenerateRandomTemp();
            customers = new List<Customer>();
        }
        //Member Methods
        public int GenerateLikelihood()
        {
            int likelihood;
            if (weather.temp <= 70)
            {
                //Making sure to take into account this standard price that changes due to weather conditions
                standardPricePerCup = .15;
                if (weather.goodWeatherConditions.Contains(weather.condition))
                {
                    likelihood = 10;
                }
                else if (weather.badWeatherConditions.Contains(weather.condition))
                {
                    likelihood = 15;
                }
                else
                {
                    likelihood = 20;
                }
            }
            else if (weather.temp <= 90)
            {
                //Making sure to take into account this standard price that changes due to weather conditions
                standardPricePerCup = .25;
                if (weather.goodWeatherConditions.Contains(weather.condition))
                {
                    likelihood = 7;
                }
                else if (weather.badWeatherConditions.Contains(weather.condition))
                {
                    likelihood = 10;
                }
                else
                {
                    likelihood = 15;
                }
            }
            else
            {
                //Making sure to take into account this standard price that changes due to weather conditions
                standardPricePerCup = .35;
                if (weather.goodWeatherConditions.Contains(weather.condition))
                {
                    likelihood = 5;
                }
                else if (weather.badWeatherConditions.Contains(weather.condition))
                {
                    likelihood = 8;
                }
                else
                {
                    likelihood = 10;
                }
            }

            return likelihood;
        }

        public void GenerateAmountOfCustomers()
        {
            int numberOfCustomers;
            int likelihood = GenerateLikelihood();

            if (likelihood <= 20 && likelihood >= 15)
            {
                numberOfCustomers = 20;
                GenerateCustomers(numberOfCustomers);
            }
            else if (likelihood < 15 && likelihood >= 10)
            {
                numberOfCustomers = 30;
                GenerateCustomers(numberOfCustomers);
            }
            else if (likelihood < 10 && likelihood >= 5)
            {
                numberOfCustomers = 50;
                GenerateCustomers(numberOfCustomers);
            }
        }

        public void GenerateCustomers(int numberOfCustomers)
        {
            for (int i = 0; i <= numberOfCustomers; i++)
            {
                customers.Add(new Customer($"{i}"));
            }
            GenerateCustomerChances();
        }

        public void GenerateCustomerChances()
        {
            foreach (Customer customer in customers)
            {
                customer.Chances = GenerateLikelihood();
            }
        }


    }
}
