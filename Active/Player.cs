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

        static public void Init()
        {
            inventory = new Inventory(100);
            test = true;
            location = "CarrotTown";

            skillNames = new string[3];
            skillNames[0] = "Wisdom";
            skillNames[1] = "Intimidation";
            skillNames[2] = "Persuasion";

            skillLevels = new int[3];
            skillLevels[0] = 3;
            skillLevels[1] = 3;
            skillLevels[2] = 3;
        }


        static public void Update()
        {
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
            }
            return 50;
        }

        static public bool SetSkillLevel(string skillName, float level)
        {
            int counter = 0;
            foreach (string tempString in skillNames)
            {
                if (skillName == tempString)
                {
                    if (level == 0)
                    {
                        skillLevels[counter]++;
                    }
                    else
                    {
                        if (level < 0)
                        {
                            while (level != 0)
                            {
                                skillLevels[counter]--;
                                level++;
                            }
                        }
                        else
                        {
                            skillLevels[counter] += (int)level;
                        }
                    }
                }
                counter++;
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
    }
}
