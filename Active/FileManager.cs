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
        static string fileText, pathway;
        static List<string> readPerLine;

        public static string FileText { get => fileText; set => fileText = value; }
        public static List<string> ReadPerLine { get => readPerLine; set => readPerLine = value; }

        public static void ReadFile(string name)
        {
            FileText = null;
            if (File.Exists(name))
            {
                sr = new StreamReader(name);
                FileText = sr.ReadToEnd();
                sr.Close();
            }
        }
        public static string[] SplitText(char splitChar, string text)// Splits the text, Easier to read imo
        {
            return text.Split(splitChar);
        }

        public static void ReadFilePerLine(string pathway)
        {
            ReadPerLine = new List<string>();
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

        static void Reset()
        {
            FileText = null;
            splitter = null;
            secondSplitter = null;
            thirdSplitter = null;
            ReadPerLine = null;
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
