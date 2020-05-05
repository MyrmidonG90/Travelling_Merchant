using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Active
{
    static class EventManager
    {
        static Random rand;
        static StreamReader sr;
        static public List<Event> events;
        static public bool Encountered()
        {
            /*TextFil Events.txt
             ID
             Händelse ID
             Text|text
             Procent
             */
             
            

            if (rand.Next(0, 9) == 0)
            {
                return true;
            }

            return false;
        }

        static int RandomiseEncouonter()
        {
            return 0;
        }
        static public int GetEventID()
        {
            return 1;
        }
        static public void LoadEvents()
        {
            FileManager.ReadFilePerLine("");
            for (int i = 0; i < FileManager.ReadPerLine.Count/4; i++)
            {
                List<string> tmp;
                int tmpID = int.Parse(FileManager.ReadPerLine[0 + i * 4]);
                int tmpEID = int.Parse(FileManager.ReadPerLine[1 + i * 4]);
                
                int tmpPercentage = int.Parse(FileManager.ReadPerLine[3+i*4]);
                events.Add(,));
            }
            FileManager.ReadPerLine

        }

        static bool CheckPercentageValue()
        {
            bool answer = false;
            int sum = 0;
            for (int i = 0; i < events.Count; i++)
            {
                sum +=events[i].Percentage;
            }
            if (sum == 100)
            {
                answer = true;
            }

            return answer;            
        }
        
    }
}
