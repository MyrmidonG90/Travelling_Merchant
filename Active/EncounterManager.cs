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
    static class EncounterManager
    {
        static Random rand;
        static StreamReader sr;
        static List<TravelEvent> events;
        static List<Encounter> encounters;
        static bool eventOnGoing;
        static List<string> tmpString;
        static int tmpInt;
        static int counter;
        static bool found;
        static Encounter currentEncounter;
        internal static List<TravelEvent> Events { get => events;}


        static public void Initialize()
        {
            LoadEncountInfo();
            LoadEncounters();
            if (CheckPercentageValue() == false) // Bugg check
            {
                // Kommer att funka men det kommer inte vara rätt slump.
            }
        }

        static public bool Encountered()
        {
            if (eventOnGoing == false)
            {
                if (rand.Next(0, 9) == 0) // If Encountered
                {
                    eventOnGoing = true;
                    InitiateEncounter(FindEID(RandomiseEncounter()));
                }
            }

            return eventOnGoing;
        }

        static int FindEID(int id) // Hittar Encounter ID
        {
            found = false;
            counter = 0;
            while (found == false && events.Count > counter)
            {
                if (events[counter].Id == id)
                {
                    found = true;
                }
                else
                {
                    ++counter;
                }
            }

            return events[counter].EID;
        }

        static int FindEnocunterID(int eID)
        {
            found = false;
            counter = 0;
            while (found == false && events.Count > counter)
            {
                if (encounters[counter].Id == eID)
                {
                    found = true;
                }
                else
                {
                    ++counter;
                }
            }

            return events[counter].EID;
        }

        static void InitiateEncounter(int eID)
        {
            currentEncounter = encounters[FindEnocunterID(eID)];
        }

        static int RandomiseEncounter()
        {
            int chance = rand.Next(1,100);
            counter = 0;

            while (chance > 0)
            {

                chance -= events[counter].Percentage;

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

        static void LoadEncountInfo() // Laddar in alla events från en textfil
        {
            events = new List<TravelEvent>();
            
            FileManager.ReadFilePerLine("./Data/EncounterInfo.txt"); // Läser in filen där alla events är

            for (int i = 0; i < FileManager.ReadPerLine.Count/4; i++) //
            {
                
                int tmpID = int.Parse(FileManager.ReadPerLine[0 + i * 4]); // Får ID för eventet

                int tmpEID = int.Parse(FileManager.ReadPerLine[1 + i * 4]); // Får Event ID

                tmpString = new List<string>(); 

                FileManager.splitter = FileManager.SplitText('|',FileManager.ReadPerLine[2+i*4]);

                for (int j = 0; j < FileManager.splitter.Length; j++)
                {
                    tmpString.Add(FileManager.splitter[j]); // Lägger in event text
                }

                int tmpPercentage = int.Parse(FileManager.ReadPerLine[3+i*4]); // Får event procent

                Events.Add(new TravelEvent(tmpID,tmpEID,tmpString,tmpPercentage)); // Skapar events
            }

        }
        static void LoadEncounters()
        {
            encounters = new List<Encounter>();
            FileManager.ReadFilePerLine("./Data/Encounters.txt");

            for (int i = 0; i < FileManager.ReadPerLine.Count/2; i++)
            {
                int tmpID = int.Parse(FileManager.ReadPerLine[0+2*i]);
                tmpString = new List<string>();
                FileManager.splitter = FileManager.SplitText('|',FileManager.ReadPerLine[1+i*2]);
                for (int j = 0; j < FileManager.splitter.Length; j++)
                {
                    tmpString.Add(FileManager.splitter[j]);
                }
                encounters.Add(new Encounter(tmpID,tmpString));
            }
        }

        static void CreateEncounter() // Programmer tool
        {
            tmpString = new List<string>();
            encounters.Add(new Encounter(encounters.Count,tmpString));
            SaveEncounters();
        }
        static public void SaveEncounters() // Programmer tool
        {

        }
        static public void CreateTravelEvent() // Programmer tool
        {
            SaveTravelEvents();
        }
        static public void SaveTravelEvents() // Programmer tool
        {

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
            tmpInt = currentEncounter.Update();
            if (tmpInt != -1)
            {

            }
        }

        static void Draw(SpriteBatch sb) // Bör endast hända i TravelMenu
        {
            currentEncounter.Draw(sb);
        }
        
    }
}
