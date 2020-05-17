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
            int nrOfItems = FileManager.ReadPerLine.Count / 7;
            
            for (int i = 0; i < nrOfItems; i++)
            {
                //Vector2 vector = new Vector2(float.Parse(SplitText(';',readPerLine[2 + i * 6])[0]), float.Parse(SplitText(';', readPerLine[2 + i * 6])[1]));
                List<string> tmpNeighbors = new List<string>();
                List<Item> tmpItems = new List<Item>();
                List<Vector2> tmpModifiers = new List<Vector2>();

                for (int j = 0; j < info[(i*7 + 3)].Split(',').Length; j++) // Add neighbors
                {
                    tmpNeighbors.Add(info[(i*7 + 3)].Split(',')[j]);
                }

                cities.Add(new City(info[i*7], info[i*7 + 1], new Vector2(float.Parse(info[i*7 + 2].Split(',')[0]), float.Parse(info[i*7 + 2].Split(',')[1])), tmpNeighbors)); // Creates a new city based on name,description,map location and neighbors
                
                for (int j = 0; j < info[i + 5].Split(';').Length; j++) // Adding Inventory
                {
                    tmpItems.Add(ItemCreator.CreateItem(int.Parse(info[i*7 + 5].Split(';')[j].Split(',')[0]), int.Parse(info[i*7 + 5].Split(';')[j].Split(',')[1])));
                }
                cities[i].AddInventory(new Inventory(int.Parse(info[i*7 + 4]), tmpItems)); // Added Inventory

                for (int j = 0; j < info[i*7+6].Split(';').Length; j++) // Adding Modifiers
                {

                    tmpModifiers.Add(new Vector2(float.Parse(info[i*7+6].Split(';')[j].Split(':')[0]), float.Parse(info[i * 7 + 6].Split(';')[j].Split(':')[1])));
                }
                cities[i].AddModifiers(tmpModifiers); // Added Modifiers
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
