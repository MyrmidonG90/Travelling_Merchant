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
        static StreamReader sr;
        static StreamWriter sw;
        static public string[] splitter, secondSplitter, thirdSplitter, fourthSplitter;
        static string fileText;
        static List<string> readPerLine;

        public static string FileText { get => fileText;}
        public static List<string> ReadPerLine { get => readPerLine;}

        
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
                    ReadPerLine.Add(sr.ReadLine());
                }
                sr.Close();
            }
        }

        public static void SaveCityFile(string pathway) // Funkar EJ som intended!!! Ändra på city.ToStringArray() och lägg till Mys int!!!
        {
            if (File.Exists(pathway))
            {
                EmptyFile(pathway);
                sw = new StreamWriter(pathway);
                for (int i = 0; i < CityManager.Cities.Count; i++)
                {
                    for (int j = 0; j < CityManager.Cities[i].ToStringArray().Length; j++)
                    {
                        sw.WriteLine(CityManager.Cities[i].ToStringArray()[j]);
                    }
                }
                sw.Close();
            }
        }
        /// <summary>
        /// Glossary structure
        /// itemsDiscovered
        /// itemsLeft
        /// travelEncountersOccured
        /// travelEncountersLeft
        /// worldEventsOccured
        /// worldEventsLeft
        /// </summary>
        /// 

        public static List<List<int>> LoadGlossary()
        {
            List<List<int>> glossaries = new List<List<int>>();
            ReadFilePerLine("./Data/Glossary.txt");

            for (int i = 0; i < readPerLine.Count; i++)
            {
                glossaries.Add(new List<int>());
            }

            for (int i = 0; i < readPerLine.Count; i++)
            {
                for (int j = 0; j < readPerLine[i].Split(',').Length; j++)
                {
                    glossaries[i].Add(int.Parse(readPerLine[i].Split(',')[j]));
                }
            }
            return glossaries;
        }
        public static void SaveGlossaries(List<List<int>> glossaries)
        {
            if (File.Exists("./Data/Glossary.txt"))
            {
                EmptyFile("./Data/Glossary.txt");
                sw = new StreamWriter("./Data/Glossary.txt");
                for (int i = 0; i < glossaries.Count; i++)
                {
                    if (glossaries[i] != null)
                    {
                        string tmp = "";
                        for (int j = 0; j < glossaries[i].Count; j++)
                        {
                            if (j != 0)
                            {
                                tmp += ','+glossaries[i][j].ToString() ;
                            }
                            else
                            {
                                tmp = glossaries[i][j].ToString();
                            }
                        }
                        sw.WriteLine(tmp);
                    }
                    else
                    {
                        sw.WriteLine(-1);
                    }                    
                }
                sw.Close();
            }
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
