using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand_3DayStarter
{
    class Recipe
    {
        //member variables
        public int amountOfLemons;
        public int amountOfSugarCubes;
        public int amountOfIceCubes;
        public double pricePerCup;

        //constructor
        public Recipe()
        {
            amountOfLemons = 2;
            amountOfSugarCubes = 3;
            amountOfIceCubes = 3;
            pricePerCup = .25;
        }
        //member methods
    }
}
