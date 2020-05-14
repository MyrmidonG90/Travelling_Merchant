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
        static List<TravelEvent> travelEvents;
        static List<Encounter> encounters;
        static bool eventOnGoing;
        static List<string> tmpString;
        static int tmpInt;
        static int counter;
        static bool found;
        static bool boolUpdate;
        static Encounter currentEncounter;
        static TravelEvent currentTravelEvent;
        internal static List<TravelEvent> Events { get => travelEvents;}
        public static bool EventOnGoing { get => eventOnGoing;}

        static public void Initialize() // Only needed to called once
        {
            LoadEncountInfo();
            LoadEncounters();
            
            rand = new Random();
            if (CheckPercentageValue() == false) // Bugg check
            {
                // Kommer att funka men det kommer inte vara rätt slump.
            }
        }

        static public void NewTrip()
        {
            foreach (var item in encounters)
            {
                item.OccuredDuringTravel = false;
            }
            currentEncounter = null;
        } // Called when a new trip is made

        static public bool Encountered()
        {
            if (currentEncounter == null)
            {
                if (eventOnGoing == false)
                {
                    if (rand.Next(0, 1) == 0) // If Encountered //Change this one to increase/decrease encounters!!!
                    {
                        eventOnGoing = true;
                        InitiateEncounter(FindID(RandomiseEncounter()));
                    }
                }
            }          
            return eventOnGoing;
        } 

        static int FindID(int id) // Hittar travel events id
        {
            found = false;
            counter = 0;
            while (found == false && travelEvents.Count > counter)
            {
                if (travelEvents[counter].Id == id)
                {
                    found = true;
                }
                else
                {
                    ++counter;
                }
            }

            return travelEvents[counter].Id;
        }

        static int FindEncounterID(int eID) // Hittar Encounter ID
        {
            found = false;
            counter = 0;
            while (found == false && travelEvents.Count > counter)
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

            return travelEvents[counter].EID;
        }

        static int FindIndex(int id)
        {
            found = false;
            counter = 0;
            while (found == false && travelEvents.Count > counter)
            {
                if (travelEvents[counter].Id == id)
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
        static void InitiateEncounter(int id)
        {
            currentEncounter = encounters[FindEncounterID(id)];
            currentTravelEvent = travelEvents[FindIndex(id)];
            currentEncounter.OccuredDuringTravel = true;
            boolUpdate = false;

        }        

        // Works fine
        static int RandomiseEncounter()
        {
            int chance = rand.Next(1,100);
            counter = 0;

            while (chance > 0)
            {

                chance -= travelEvents[counter].Percentage;

                if (chance >= 0)
                {
                    ++counter;
                }
            }
            
            // Ser till att under denna färden inte kommer upprepa samma encounter
            
            if (encounters[FindEncounterID(FindID(counter))].OccuredDuringTravel) // Stackoverflow Farlig kod!!! //buggen då inte hemsidan /My
            {
                counter = RandomiseEncounter();
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
            travelEvents = new List<TravelEvent>();
            
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

                int counter = 0;
                List<string> newTmpString = new List<string>();
                foreach (string tempString in tmpString)
                {
                    newTmpString.Add(tempString.Replace("|", "\n"));
                    counter++;
                }

                int tmpPercentage = int.Parse(FileManager.ReadPerLine[3+i*4]); // Får event procent

                Events.Add(new TravelEvent(tmpID,tmpEID,newTmpString,tmpPercentage)); // Skapar events
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

        static public bool Update() // Bör endast hända i TravelMenu
        {            
            tmpInt = currentEncounter.Update();
            if (tmpInt != -1)
            {
                if (Scenarios(currentEncounter.Id, tmpInt))
                {
                    eventOnGoing = false;
                    boolUpdate = true;
                }             
            }
            return boolUpdate;

        }

        static bool Scenarios(int eID, int answer)
        {
            switch (eID)
            {
                case 0:
                    if (answer == 0)
                    {
                        Player.Inventory.Money -= 20;
                        return true;
                    }
                    else if (answer == 1 && Player.ReturnSkillLevel("Persuasion") >= 2)
                    {
                        Player.Inventory.Money -= 10;
                        return true;
                    }
                    else if (answer == 2)
                    {
                        TravelMenu.AbortTravel();
                        return true;
                    }
                    break;
                case 1:
                    if (answer == 0)
                    {
                        Player.Inventory.Money -= (int)((Player.Inventory.Money * 0.3f) + 0.5f);
                        return true;
                    }
                    else if (answer == 1)
                    {
                        foreach (Item tempitem in Player.Inventory.ItemList)
                        {
                            if (tempitem.Rarity == 2)
                            {
                                Player.Inventory.ReduceAmountOfItems(tempitem.ID, 1);
                                return true;
                            }
                        }
                        return false;
                    }
                    else if (answer == 2 && Player.ReturnSkillLevel("Intimidation") >= 3)
                    {
                        return true;
                    }
                    break;
                case 2:
                    if (answer == 0)
                    {
                        TravelMenu.TurnsLeft += 3;
                        return true;
                    }
                    else if (answer == 1)
                    {
                        return true;
                    }
                    else if (answer == 2)
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        static public void Draw(SpriteBatch sb) // Bör endast hända i TravelMenu
        {
            currentTravelEvent.Draw(sb);
            currentEncounter.Draw(sb);
            
        }
        
    }
}
