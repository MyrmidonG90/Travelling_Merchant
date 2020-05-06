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
        static List<Event> events;
        static bool eventOnGoing;
        internal static List<Event> Events { get => events;}

        static public void Initialize()
        {
            LoadEvents();
            if (CheckPercentageValue() == false) // Bugg check
            {
                // Kommer att funka men det kommer inte vara rätt slump.
            }
        }

        static public bool Encountered()
        {
            if (rand.Next(0, 9) == 0)
            {
                eventOnGoing = true;                
            }

            return eventOnGoing;
        }

        static int RandomiseEncounter()
        {
            int chance = rand.Next(1,100);
            int counter = 0;

            while (chance > 0)
            {

                chance -= Events[counter].Percentage;

                if (chance > 0)
                {
                    ++counter;
                }

            }

            return counter;
        }

        /*TextFil Events.txt
             ID
             Händelse ID
             Text|text
             Procent
        */

        static public void LoadEvents() // Laddar in alla events från en textfil
        {
            events = new List<Event>();
            FileManager.ReadFilePerLine("./Data/Events.txt"); // Läser in filen där alla events är

            for (int i = 0; i < FileManager.ReadPerLine.Count/4; i++) //
            {
                
                int tmpID = int.Parse(FileManager.ReadPerLine[0 + i * 4]); // Får ID för eventet

                int tmpEID = int.Parse(FileManager.ReadPerLine[1 + i * 4]); // Får Event ID

                List<string> tmpString = new List<string>(); 

                FileManager.splitter = FileManager.SplitText('|',FileManager.ReadPerLine[2+i*4]);

                for (int j = 0; j < FileManager.splitter.Length; j++)
                {
                    tmpString.Add(FileManager.splitter[j]); // Lägger in event text
                }

                int tmpPercentage = int.Parse(FileManager.ReadPerLine[3+i*4]); // Får event procent

                Events.Add(new Event(tmpID,tmpEID,tmpString,tmpPercentage)); // Skapar events
            }

        }

        static bool CheckPercentageValue() // Debugg funktion
        {
            bool answer = false;
            int sum = 0;

            for (int i = 0; i < Events.Count; i++)
            {
                sum +=Events[i].Percentage;
            }

            if (sum == 100)
            {
                answer = true;
            }

            return answer;            
        }

        static void Update() // Bör endast hända i TravelMenu
        {
            
        }

        static void Draw(SpriteBatch sb) // Bör endast hända i TravelMenu
        {
            
        }
        
    }
}
