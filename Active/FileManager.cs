using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
namespace Active
{
    static class FileManager
    {
        static public StreamReader sr;
        static public StreamWriter sw;
        static public FileStream fs;
        static public string[] splitter, secondSplitter, thirdSplitter, fourthSplitter;
        static public string fileText;
        static public List<string> readFilePerLine;
        static public List<City> cities;

        public static string[] SplitText(char splitChar, string text)
        {
            return text.Split(splitChar);
        }
        public static void ReadFile(string name)
        {
            fileText = null;
            if (File.Exists(name))
            {
                sr = new StreamReader(name);
                sr.ReadToEnd();
            }
        }
        public static void ReWriteFile(string name, string text)
        {
            if (File.Exists(name))
            {
                EmptyFile(name);
                sw = new StreamWriter(name);
                sw.Write(text);
                sw.Close();
            }
        }
        public static void ReWriteFile(string name, List<string> text)
        {
            if (File.Exists(name))
            {
                EmptyFile(name);
                sw = new StreamWriter(name);
                foreach (var item in text)
                {
                    sw.WriteLine(item);
                }
                sw.Close();
            }
        }
        public static void LoadCities()
        {
            ReadFile("");
            Inventory inv;
            splitter = SplitText('|', fileText); // Splits the text per object
            
            for (int i = 0; i < splitter.Length; i++)
            {
                secondSplitter = SplitText(';', splitter[i]); // Splits the text per data structure
                cities.Add(new City(secondSplitter[0], secondSplitter[1], new Vector2(float.Parse(secondSplitter[2]), float.Parse(secondSplitter[3]))));
                inv = new Inventory(int.Parse(secondSplitter[4])); 
                thirdSplitter = SplitText(',',secondSplitter[5]); // Splits data structure inventory
                for (int j = 0; j < thirdSplitter.Length; j++)
                {
                    fourthSplitter = SplitText(':',thirdSplitter[j]); // Splits the data structure inside of inventory
                    inv.AddItem(int.Parse(fourthSplitter[0]),int.Parse(fourthSplitter[1]));
                }
                cities[i].AddInventory(inv);
            }
            Reset();
        }
        public static void CreateCity()
        {

        }
        public static void SaveCities()
        {
            if (File.Exists(""))
            {
                for (int i = 0; i < cities.Count; i++)
                {
                    fileText += cities[i].ToString();
                }
                EmptyFile("");
                sw = new StreamWriter("");
                sw.Write(fileText);
                sw.Close();
            }

            Reset();
        }
        public static void UpdateCities()
        {

        }
        
        static void Reset()
        {
            fileText = null;
            splitter = null;
            secondSplitter = null;
            thirdSplitter = null;
        }

        public static void EmptyFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                File.WriteAllText(fileName, String.Empty);
            }
        }
    }


}
