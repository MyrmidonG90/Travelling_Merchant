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
        static public string[] charSplitter;
        static public string[] secondCharSplitter;
        static public string[] thirdCharSplitter;
        static public string fileText;
        static public List<string> readFilePerLine;
        static public List<City> cities;

        public static string[] SplitText(char splitChar, string text)
        {
            string[] splitter;
            splitter = text.Split(splitChar);
            return splitter;
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
        public static void CreateCities()
        {
            int nrOfTerms = 5;
            ReadFile("");
            
            charSplitter = SplitText(';', fileText);
            
            for (int i = 0; i < charSplitter.Length/nrOfTerms; i++)
            {
                cities.Add(new City(charSplitter[nrOfTerms*i%nrOfTerms],charSplitter[nrOfTerms*(i+1)%nrOfTerms],new Vector2(float.Parse(charSplitter[nrOfTerms * (i + 2) % nrOfTerms]), float.Parse(charSplitter[nrOfTerms * (i + 3) % nrOfTerms]))));
                secondCharSplitter = SplitText(',', charSplitter[nrOfTerms*(5+i)%nrOfTerms]);
                Inventory inv = new Inventory(int.Parse(charSplitter[nrOfTerms*(4+i)%nrOfTerms]));
                for (int j = 0; j < secondCharSplitter.Length; j++)
                {
                    thirdCharSplitter = SplitText(':', secondCharSplitter[j]);
                    inv.AddItem(int.Parse(thirdCharSplitter[0]), int.Parse(thirdCharSplitter[1]));
                    
                }
                cities[i / nrOfTerms].AddInventory(inv);
            }
        }
        static void Reset()
        {
            fileText = null;
            charSplitter = null;
            secondCharSplitter = null;
            thirdCharSplitter = null;
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
