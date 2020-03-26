using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand_3DayStarter
{
    class Day
    {
        //Member Variables
        //public Weather weather = new Weather();
        public CurrentWeather weatherTest;
        public Weather weather = new Weather();
        HttpClient client = new HttpClient();
        public List<Customer> customers;
        public double standardPricePerCup;
        public double profits;
        public int cupsSold;


        //Constructor
        public Day()
        {
            weather.condition = weather.GenerateRandomCondition();
            weather.temp = weather.GenerateRandomTemp();
            DeserializeAPICall();
            customers = new List<Customer>();
        }
        //Member Methods
        public async void DeserializeAPICall()
        {
            string cityName = "Muskego";
            string apiKey = "c6f64df958a8c87acc09cebfa8a7d040";
            string url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&units=imperial&appid={apiKey}";
            HttpResponseMessage response = await client.GetAsync(url);
            string jsonResult = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                weatherTest = JsonConvert.DeserializeObject<CurrentWeather>(jsonResult);
            }
        }
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

        public void GenerateAmountOfCustomers(Player player)
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
