using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonadeStand_3DayStarter
{
    class Inventory
    {
        // member variables (HAS A)
        public List<Lemon> lemons;
        public List<SugarCube> sugarCubes;
        public List<IceCube> iceCubes;
        public List<Cup> cups;

        // constructor (SPAWNER)
        public Inventory()
        {
            lemons = new List<Lemon>();
            sugarCubes = new List<SugarCube>();
            iceCubes = new List<IceCube>();
            cups = new List<Cup>();
        }

        // member methods (CAN DO)
        public void AddLemonsToInventory(int numberOfLemons)
        {
            for(int i = 0; i < numberOfLemons; i++)
            {
                Lemon lemon = new Lemon();
                lemons.Add(lemon);
            }
        }

        public void AddSugarCubesToInventory(int numberOfSugarCubes)
        {
            for(int i = 0; i < numberOfSugarCubes; i++)
            {
                SugarCube sugarCube = new SugarCube();
                sugarCubes.Add(sugarCube);
            }
        }

        public void AddIceCubesToInventory(int numberOfIceCubes)
        {
            for(int i = 0; i < numberOfIceCubes; i++)
            {
                IceCube iceCube = new IceCube();
                iceCubes.Add(iceCube);
            }
        }

        public void AddCupsToInventory(int numberOfCups)
        {
            for(int i = 0; i < numberOfCups; i++)
            {
                Cup cup = new Cup();
                cups.Add(cup);
            }
        }

        public void RemoveLemonsFromInventory(int numberOfLemons)
        {
            int lowerLimit = lemons.Count - numberOfLemons;
            for (int i = lemons.Count - 1; i >= lowerLimit; i--)
            {
                lemons.Remove(lemons[i]);
            }
        }

        public void RemoveSugarCubesFromInventory(int numberOfSugarCubes)
        {
            int lowerLimit = sugarCubes.Count - numberOfSugarCubes;
            for (int i = sugarCubes.Count - 1; i >= lowerLimit; i--)
            {
                sugarCubes.Remove(sugarCubes[i]);
            }
        }

        public void RemoveIceCubesFromInventory(int numberOfIceCubes)
        {
            int lowerLimit = iceCubes.Count - numberOfIceCubes;
            for (int i = iceCubes.Count - 1; i >= lowerLimit; i--)
            {
                iceCubes.Remove(iceCubes[i]);
            }
        }

        public void RemoveCupsFromInventory(int numberOfCups)
        {
            int lowerLimit = cups.Count - numberOfCups;
            for (int i = cups.Count - 1; i >= lowerLimit; i--)
            {
                cups.Remove(cups[i]);
            }
        }
    }
}
