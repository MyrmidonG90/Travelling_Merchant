using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
namespace Active
{
    static class CityManager
    {
        static string pathway;
        static List<City> cities;

        internal static List<City> Cities { get => cities; set => cities = value; }


        /*
         
        Data structure for cities:

        0 String Name
        1 String Description
        2 Vector2 coordinates
        3 Good Deals
        4 Bad Deals
        5 int money
        6 Item items
        7 Vector2 modifiers

        Example

        Example City
        Test text; Radbrytte; Slut
        500,600
        1,5
        2,3
        500
        0,50;1,50;0,50
        0:0,5;

            When Changing data structure

        */

        /* Old Structure
         0 String Name
        1 String Description
        2 Vector2 coordinates
        3 List<string> neighbors
        4 int money
        5 Item items
        6 Vector2 modifiers

        Example

        Example City
        Test text; Radbrytte; Slut
        500,600
        Exempel Byn, Alpha stad, Beta stad
        500
        0,50;1,50;0,50
        0:0,5;
             */

        static public void Initialize()
        {
            pathway = "./Data/CityStructure.txt";
            
            LoadCities();
            int checkpoint = 0; // används bara vid debug
        }

        static void LoadCities()
        {
            cities = new List<City>();
            FileManager.ReadFilePerLine(pathway);
            List<string> info = FileManager.ReadPerLine ;

            List<int> mysInt = new List<int>();

            int nrOfItems = 9; // Change This if you add more items

            int nrOfCities = FileManager.ReadPerLine.Count / nrOfItems;
            
            for (int i = 0; i < nrOfCities; i++)
            {
                //Vector2 vector = new Vector2(float.Parse(SplitText(';',readPerLine[2 + i * 6])[0]), float.Parse(SplitText(';', readPerLine[2 + i * 6])[1]));
                int start = 0;
                string cityName;
                string description;
                Vector2 coordinates;
                List<int> goodDeals = new List<int>();
                List<int> badDeals = new List<int>();
                //List<string> tmpNeighbors = new List<string>();
                int money;
                List<Item> tmpItems = new List<Item>();
                List<Vector2> tmpModifiers = new List<Vector2>();
                int index = i * nrOfItems;


                cityName = info[index]; // City Name
                ++start; // start == 1 Description
                description = info[index + start]; // City Description
                ++start; // Start == 2 Coordinates
                coordinates = new Vector2(float.Parse(info[index + start].Split(',')[0]),float.Parse( info[index + start].Split(',')[1])); // Coordinates
                ++start; // Start == 3 Good Deals
                for (int j = 0; j < info[index + start].Split(',').Length; j++) // Good Deals
                {
                    goodDeals.Add(int.Parse(info[index + start].Split(',')[j]));
                }
                ++start; // Start == 4 Bad Deals
                for (int j = 0; j < info[index + start].Split(',').Length; j++)
                {
                    badDeals.Add(int.Parse(info[index + start].Split(',')[j]));
                }
                cities.Add(new City(cityName,description,coordinates,goodDeals,badDeals)); // Creates the Cíty
                ++start;// Start == 5 Money for Inventory
                money = int.Parse(info[index + start]);
                ++start; // Start == 6 Items for inventory

                /*for (int j = 0; j < info[(i* nrOfItems + 3)].Split(',').Length; j++) // Add neighbors
                {
                    tmpNeighbors.Add(info[(i*7 + 3)].Split(',')[j]);
                }
                for (int j = 0; j < info[(i * 7 + 4)].Split(',').Length; j++) // Add neighbors
                {
                    tmpNeighbors.Add(info[(i * 7 + 4)].Split(',')[j]);
                }*/

                //cities.Add(new City(info[i*7], info[i*7 + 1], new Vector2(float.Parse(info[i*7 + 2].Split(',')[0]), float.Parse(info[i*7 + 2].Split(',')[1])), tmpNeighbors)); // Creates a new city based on name,description,map location and neighbors
                
                for (int j = 0; j < info[index + start].Split(';').Length; j++) // Adding Inventory
                {
                    tmpItems.Add(ItemCreator.CreateItem(int.Parse(info[index + start].Split(';')[j].Split(',')[0]), int.Parse(info[index + start].Split(';')[j].Split(',')[1])));
                }
                cities[i].AddInventory(new Inventory(money, tmpItems)); // Added Inventory
                ++start; // Start == 7 modifers
                for (int j = 0; j < info[index + start].Split(';').Length; j++) // Adding Modifiers
                {
                    tmpModifiers.Add(new Vector2(float.Parse(info[index + start].Split(';')[j].Split(':')[0]), float.Parse(info[index + start].Split(';')[j].Split(':')[1])));
                }
                cities[i].AddModifiers(tmpModifiers); // Added Modifiers
                ++start; // Mys int
                mysInt.Add(int.Parse(info[index+start]));
                
            }
        }
        static public int FindCityIndex(string cityName)
        {
            bool found = false;
            int counter = 0;
            while (found == false && counter < cities.Count) // Kommer returnera error om man har skrivit fel namn
            {
                if (cities[counter].Name == cityName)
                {
                    found = true;                    
                }
                else
                {
                    ++counter;
                }
            }
            return counter;
        }
    }


}
