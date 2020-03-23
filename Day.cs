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
        public int GenerateLikelihood(string condition, int temp)
        {
            int likelihood;
            if (temp <= 70)
            {
                //Making sure to take into account this standard price that changes due to weather conditions
                standardPricePerCup = .15;
                if (weather.goodWeatherConditions.Contains(condition))
                {
                    likelihood = 10;
                }
                else if (weather.badWeatherConditions.Contains(condition))
                {
                    likelihood = 15;
                }
                else
                {
                    likelihood = 20;
                }
            }
            else if (temp <= 90)
            {
                //Making sure to take into account this standard price that changes due to weather conditions
                standardPricePerCup = .25;
                if (weather.goodWeatherConditions.Contains(condition))
                {
                    likelihood = 5;
                }
                else if (weather.badWeatherConditions.Contains(condition))
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
                if (weather.goodWeatherConditions.Contains(condition))
                {
                    likelihood = 3;
                }
                else if (weather.badWeatherConditions.Contains(condition))
                {
                    likelihood = 5;
                }
                else
                {
                    likelihood = 10;
                }
            }

            return likelihood;
        }


    }
}
