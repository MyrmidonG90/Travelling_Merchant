﻿using System;
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

        public static void SaveFile()
        {

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