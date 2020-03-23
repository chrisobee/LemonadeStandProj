using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand_3DayStarter
{
    class Weather
    {
        //Member Variables
        public string condition;
        public int temp;
        Random rand = new Random();
        List<string> weatherConditions;
        public List<string> badWeatherConditions = new List<string>() { "Overcast", "Rainy" };
        public List<string> goodWeatherConditions = new List<string>() { "Hazy", "Clear and Sunny" };

        //Constructor
        public Weather()
        {
            
        }
        //Member Methods
        public string GenerateRandomCondition()
        {
            weatherConditions = new List<string>() { "Hazy", "Clear and Sunny", "Overcast", "Rainy", "Thunderstorm" };
            condition = weatherConditions[rand.Next(weatherConditions.Count)];
            return condition;
        }

        public int GenerateRandomTemp()
        {
            temp = rand.Next(50, 100);
            return temp;
        }

        
    }
}
