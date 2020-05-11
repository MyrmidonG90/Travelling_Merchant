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
        static StreamReader sr;
        static StreamWriter sw;
        static public string[] splitter, secondSplitter, thirdSplitter, fourthSplitter;
        static string fileText,pathway;
        static List<string> readPerLine;
        static public List<City> cities;
        
        static public void Initialize()
        {
            cities = new List<City>();
        }

        public static string[] SplitText(char splitChar, string text)// Splits the text, Easier to read imo
        {
            return text.Split(splitChar);
        }

        public static void ReadFile(string name)
        {
            fileText = null;
            if (File.Exists(name))
            {
                sr = new StreamReader(name);
                fileText = sr.ReadToEnd();
                sr.Close();
            }
        }
        public static void ReadFilePerLine(string pathway)
        {
            readPerLine = new List<string>();
            if (File.Exists(pathway))
            {
                sr = new StreamReader(pathway);
                while (!sr.EndOfStream)
                {                    
                    readPerLine.Add(sr.ReadLine());                    
                }
                sr.Close();
            }            
        }



        public static void LoadCities()
        {
            pathway = "./Data/test.txt";
            ReadFile(pathway);
            Inventory inv;
            cities = new List<City>();
            splitter = SplitText('|', fileText); // Splits the text per object
            
            for (int i = 0; i < splitter.Length-1; i++)
            {
                secondSplitter = SplitText(';', splitter[i]); // Splits the text per data structure
                //cities.Add(new City(secondSplitter[0], secondSplitter[1], new Vector2(float.Parse(secondSplitter[2]), float.Parse(secondSplitter[3]))));// Error
                inv = new Inventory(int.Parse(secondSplitter[4])); 
                thirdSplitter = SplitText(',',secondSplitter[5]); // Splits data structure inventory
                if (thirdSplitter[0] != "")
                {
                    for (int j = 0; j < thirdSplitter.Length; j++)
                    {
                        fourthSplitter = SplitText(':', thirdSplitter[j]); // Splits the data structure inside of inventory
                        inv.AddItem(int.Parse(fourthSplitter[0]), int.Parse(fourthSplitter[1]));
                    }
                }               
                
                cities[i].AddInventory(inv);
            }
            Reset();
        }
        public static void LoadCities(int o)
        {
            cities = new List<City>();
            pathway = "./Data/test.txt";
            ReadFilePerLine(pathway);
            Inventory inv;
            int nrOfItems = readPerLine.Count / 6;
            for (int i = 0; i < nrOfItems; i++)
            {
                Vector2 vector = new Vector2(float.Parse(SplitText(';',readPerLine[2 + i * 6])[0]), float.Parse(SplitText(';', readPerLine[2 + i * 6])[1]));
                //cities.Add(new City(readPerLine[i*6], readPerLine[1+i*6], vector));

            }

        }
        public static void CreateCity(string name, string information, Vector2 coordinates) 
        {
            //cities.Add(new City(name,information, coordinates));
        }
        public static void CreateCity(string name, string information, Vector2 coordinates,Inventory inv)
        {
            //cities.Add(new City(name, information, coordinates));
            cities[cities.Count-1].AddInventory(inv);
        }

        public static void SaveCities()
        {
            pathway = "./Data/test.txt";
            if (File.Exists(pathway))
            {

                for (int i = 0; i < cities.Count; i++)
                {
                    fileText += cities[i].ToString();
                }
                
                EmptyFile(pathway);
                sw = new StreamWriter(pathway);
                
                sw.Write(fileText);
                sw.Close();
            }
            Reset();
        }
        
        static void Reset()
        {
            fileText = null;
            splitter = null;
            secondSplitter = null;
            thirdSplitter = null;
            readPerLine = null;
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
