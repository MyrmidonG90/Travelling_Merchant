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
        static public List<City> Cities { get => cities;}

        /*
        Data structure for cities:

        String Name
        String Description
        Vector2 coordinates
        List<string> neighbors
        int money
        Item items
        Vector2 modifiers

        Example

        Example City
        Test text; Radbrytte; Slut
        500,600
        Exempel Byn, Alpha stad, Beta stad
        500
        0,50;1,50;0,50
        0,0.5;
        */
        static public void Initialize()
        {
            pathway = "./Data/CityStructure.txt";
           // LoadCities();
        }

        static void LoadCities()
        {
            cities = new List<City>();
            FileManager.ReadFilePerLine(pathway);
            List<string> info = FileManager.ReadPerLine ;
            int nrOfItems = FileManager.ReadPerLine.Count / 6;

            for (int i = 0; i < nrOfItems; i++)
            {
                //Vector2 vector = new Vector2(float.Parse(SplitText(';',readPerLine[2 + i * 6])[0]), float.Parse(SplitText(';', readPerLine[2 + i * 6])[1]));
                List<string> tmpNeighbors = new List<string>();
                List<Item> tmpItems = new List<Item>();
                for (int j = 0; j < info[(i + 5)].Split(',').Length; j++)
                {
                    tmpNeighbors.Add(info[(i + 5)].Split(',')[j]);
                }
                cities.Add(new City(info[i], info[i+1],new Vector2(float.Parse(info[i+2].Split(',')[0]),float.Parse(info[i+2].Split(',')[1])),tmpNeighbors));
                for (int j = 0; j < info[i+5].Split(';').Length; j++)
                {
                    tmpItems.Add(ItemCreator.CreateItem(int.Parse(info[i + 5].Split(';')[0]), int.Parse(info[i + 5].Split(';')[1])));
                }
                cities[i].AddInventory(new Inventory(int.Parse(info[i+4]),tmpItems));
            }
        }

        static int FindCityIndex(string cityName)
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
        
        static void EmptyFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.WriteAllText(fileName, String.Empty);
            }
        }
    }


}
