using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    static class Player
    {
        static Inventory inventory;
        static bool test;
        static string location;
        static string[] skillNames;
        static int[] skillLevels;
        static int[] skillXP;
        static List<string> eventCities;
        static List<string> eventNames;
        static List<int> activeEventID;
        static List<int> eventDaysLeft;
        static int date;
        static int oldDate;

        static public void Init()
        {
            inventory = new Inventory(1000);
            test = true;
            location = "Carrot Town";

            skillNames = new string[3];
            skillNames[0] = "Wisdom";
            skillNames[1] = "Intimidation";
            skillNames[2] = "Persuasion";

            skillLevels = new int[3];
            skillLevels[0] = 1;
            skillLevels[1] = 1;
            skillLevels[2] = 1;

            skillXP = new int[3];
            skillXP[0] = 0;
            skillXP[1] = 0;
            skillXP[2] = 0;


            eventCities = new List<string>();
            eventNames = new List<string>();
            activeEventID = new List<int>();
            eventDaysLeft = new List<int>();
        }

        static public void Update()
        {

            AchievementManager.currentCoins = inventory.Money;            

            //debug funktioner
            if (test)
            {
                if (KMReader.prevKeyState.IsKeyUp(Keys.A) && KMReader.keyState.IsKeyDown(Keys.A))
                {
                    inventory.AddItem(ItemCreator.CreateItem(0, 20));
                }
                if (KMReader.prevKeyState.IsKeyUp(Keys.B) && KMReader.keyState.IsKeyDown(Keys.B))
                {
                    inventory.AddItem(ItemCreator.CreateItem(1, 30));
                }
                if (KMReader.prevKeyState.IsKeyUp(Keys.C) && KMReader.keyState.IsKeyDown(Keys.C))
                {
                    inventory.AddItem(ItemCreator.CreateItem(2, 10));
                }
            }

            date = Calendar.TotalDays;
            int counter = 0;
            bool check = false;
            if (date != oldDate)
            {
                for (int i = 0; i < eventDaysLeft.Count; i++)
                {
                    eventDaysLeft[i] -= date - oldDate;
                }

                foreach (string tempTarget in eventCities)
                {
                    if (eventDaysLeft[counter] <= 0)
                    {
                        if (location == tempTarget)
                        {
                            eventCities.RemoveAt(counter);
                            eventNames.RemoveAt(counter);
                            activeEventID.RemoveAt(counter);
                            check = true;
                        }                        
                    }
                    counter++;
                    if (check)
                    {
                        break;
                    }
                }
            }
            oldDate = date;
        }

        static public void AddEventLogEntry(string target, string type, int id, int days)
        {
            bool check = false;
            foreach (string tempName in eventCities)
            {
                foreach (int tempID in activeEventID)
                {
                    if (tempName == target && tempID == id)
                    {
                        check = true;
                    }
                }
            }
            if (!check)
            {
                eventCities.Add(target);
                eventNames.Add(type);
                activeEventID.Add(id);
                eventDaysLeft.Add(days);
            }
        }
       
        static public int ReturnSkillLevel(string skillName)
        {
            int counter = 0;
            foreach (string tempString in skillNames)
            {
                if (skillName == tempString)
                {
                    return skillLevels[counter];
                }
                counter++;
            }
            return 50;
        }

        static public bool AddXP(string skillName, int xp)
        {            
            for (int i = 0; i < skillNames.Length; i++)
            {
                if (skillName == skillNames[i])
                {
                    skillXP[i] += xp;
                }
            }

            for (int i = 0; i < skillNames.Length; i++)
            {
                if (skillXP[i] < 100)
                {
                    skillLevels[i] = 1;
                    LevelUp.Start(skillNames[i]);
                }
                else if (skillXP[i] >= 100 && skillXP[i] < 200)
                {
                    skillLevels[i] = 2;
                    LevelUp.Start(skillNames[i]);
                }
                else if (skillXP[i] >= 200)
                {
                    skillLevels[i] = 3;
                    LevelUp.Start(skillNames[i]);
                }
            }
            return false;
        }



        static public Inventory Inventory
        {
            get
            {
                return inventory;
            }
            set
            {
                inventory = value;
            }
        }

        static public string Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        static public int[] SkillLevels
        {
            get
            {
                return skillLevels;
            }
            set
            {
                skillLevels = value;
            }
        }

        static public List<string> EventCities
        {
            get => eventCities;
        }

        static public List<string> EventNames
        {
            get => eventNames;
        }
    }
}
