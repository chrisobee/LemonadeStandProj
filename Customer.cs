using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand_3DayStarter
{
    class Customer
    {
        //Member Variables
        public string name;
        Random rand = new Random();
        public List<string> names = new List<string>();
        private int chances;
        public int Chances
        {
            get
            {
                return chances;
            }
            set
            {
                if(value < 0)
                {
                    chances = 0;
                }
                else
                {
                    chances = value;
                }
            }
        }
        public bool willBuy;
        
        //Constructor
        public Customer(string name)
        {
            this.name = name;
            names.Add(name);
        }
        //Member Methods

        public void ChangeChancesDueToPrice(double pricePerCup, double standardPricePerCup)
        {
            double incrementCheck;
            incrementCheck = (Math.Abs(pricePerCup - standardPricePerCup)) * 100;

            if (pricePerCup > standardPricePerCup)
            {
                for (int i = 0; i <= incrementCheck; i++)
                {
                    DecreaseChances();
                }
            }
            else if (pricePerCup < standardPricePerCup)
            {
                for (int i = 0; i <= incrementCheck; i++)
                {
                    IncreaseChances();
                }
            }
        }
        public bool BuyLemonade(int buyCheck)
        {
            int willBuyNumber = 3;
            if(buyCheck <= willBuyNumber)
            {
                willBuy = true;
            }
            else
            {
                willBuy = false;
            }
            return willBuy;
        }

        public void DecreaseChances()
        {
            Chances++;
        }

        public void IncreaseChances()
        {
            Chances--;
        }
        
    }
}
