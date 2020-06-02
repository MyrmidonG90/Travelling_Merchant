using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    static class GlossaryManager
    {
        static List<List<int>> glossaries;
        static List<GlossaryTab> glossaryTabs;

        /* Glossary text file structure
         0. Items
         1. Items Left
         2. Travel Events Occured
         3. Travel Events Left
         4. World Events Occured
         5. World Events Left
             */

        static List<Slot> slots;
        static List<bool> finished;
        static int amountOfGlossaries;
        enum Glossary
        {
            Items,
            TravelEncounters,
            WorldEvents
        }
        static Glossary currentGlossary;

        internal static List<Slot> Slots { get => slots; }
        internal static List<GlossaryTab> GlossaryTabs { get => glossaryTabs; set => glossaryTabs = value; }
        public static int AmountOfGlossaries { get => amountOfGlossaries; }

        static public void Initialize(string glossary)
        {
            LoadGlossary();
            Glossary glossaryIndex = (Glossary)GetGlossaryIndex(glossary);
            InitiateSlots(glossaryIndex);
            currentGlossary = glossaryIndex;
        }

        static public void InitateGlossary(string glossary)
        {
            currentGlossary = (Glossary)GetGlossaryIndex(glossary);
            InitiateSlots((Glossary)GetGlossaryIndex(glossary));
        }

        static void InitiateSlots(Glossary glossary)
        {
            slots = new List<Slot>();
            if (glossary == Glossary.Items)
            {
                InitiateItemSlots();
            }
            else if (glossary == Glossary.TravelEncounters)
            {
                InitiateTravelEncountersSlots();
            }
            else if (glossary == Glossary.WorldEvents)
            {
                InitiateWorlEventsSlots();
            }
        }

        static public int GetGlossaryIndex(string glossary)
        {
            bool found = false;
            
            Glossary tmp = 0;
            while (found == false)
            {
                if (tmp.ToString() == glossary)
                {
                    found = true;
                }
                else
                {
                    ++tmp;
                }
            }
            return (int)tmp;
        }

        static void InitiateItemSlots()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    slots.Add(new Slot(320 + 150 * j, 210 + 150 * i, 120, 120));
                }
            }
            for (int i = 0; i < 25; i++)
            {
                slots[i].AddTexture(ItemCreator.CreateItem(i,0).Tex);
            }
        }
        static void InitiateTravelEncountersSlots()
        {
            for (int i = 0; i < 5; i++)
            {
                slots.Add(new Slot(320 + 150 * i, 210, 120, 120));
                slots[i].AddTexture(TextureManager.encounterIcons[i]);
            }            
        }
        static void InitiateWorlEventsSlots()
        {            
            for (int i = 0; i < 3; i++)
            {
                slots.Add(new Slot(320 + 150 * i, 210, 120, 120));
                slots[i].AddTexture(TextureManager.texWorldEventIcons[i]);
            }
        }

        static public void CheckAcceptedTrade(Inventory tradeInv)
        {
            int counter = 0;
            while (counter < glossaries[1].Count)
            {
                if (tradeInv.FindIndexOf(glossaries[1][counter]) != -1)
                {
                    int tmp = tradeInv.FindIndexOf(glossaries[1][counter]);
                    glossaries[0].Add(tradeInv.ItemList[tmp].ID);
                    glossaries[1].RemoveAt(counter);
                }
                else
                {
                    ++counter;
                }
            }
            if (glossaries[1].Count == 0)
            {
                finished[0] = true;
                glossaries[0].Clear();
            }
        }

        static public void CheckTravelEncounter(int encounterID)
        {
            if (finished[1] == false)
            {
                EventOccured("TravelEncounters", encounterID);
            }
        }

        static public void CheckWorldEvents(int worldEID)
        {
            if (finished[2] == false)
            {
                EventOccured("WorldEvents", worldEID);
            }
        }
        static public int IfIndexIn(string glossary, bool existsInOccured, int find)
        {
            int counter = 0;
            bool found = false;
            int parameter = GetGlossaryIndex(glossary);
            if (existsInOccured)
            {
                while (found == false && counter < glossaries[parameter * 2].Count)
                {
                    if (find == glossaries[parameter*2][counter])
                    {
                        found = true;
                    }
                    else
                    {
                        ++counter;
                    }
                }
            }
            else
            {
                while (found == false && counter < glossaries[parameter * 2+1].Count)
                {
                    if (find == glossaries[parameter * 2+1][counter])
                    {
                        found = true;
                    }
                    else
                    {
                        ++counter;
                    }
                }
            }
            if (found == false)
            {
                counter = -1;
            }
            
            return counter;
        }
        static bool EventOccured(string glossary,int id)
        {
            int parameter = GetGlossaryIndex(glossary);
            bool found = false;
            int index = IfIndexIn(glossary, false, id);
            if (index != -1)
            {
                glossaries[parameter * 2].Add(id);
                glossaries[parameter * 2 + 1].RemoveAt(index);
                found = true;

                if (glossaries[parameter * 2 + 1].Count == 0)
                {
                    finished[parameter] = true;
                }
            }
          
            return found;
        }
        

        static void LoadGlossary()
        {
            finished = new List<bool>();            
            glossaries = FileManager.LoadGlossary();
            amountOfGlossaries = glossaries.Count / 2;
            for (int i = 0; i < amountOfGlossaries; i++) // To Add a list of bools for optimization
            {
                if (glossaries[1 + i * 2][0] == -1) // If they've seen all items/events
                {
                    finished.Add(true);
                }
                else // If they haven't seen all items/events
                {
                    finished.Add(false);
                }
            }

            for (int i = 0; i < glossaries.Count; i++) // Removing unecessary data
            {
                if (glossaries[i][0] == -1)
                {
                    glossaries[i].Clear();
                }
            }            
        }

        static public void Draw(SpriteBatch sb)
        {
            // Draws all Item slots
            for (int i = 0; i < glossaries[(int)currentGlossary*2].Count; i++)
            {
                slots[glossaries[(int)currentGlossary*2][i]].Draw(sb);
            }
            for (int i = 0; i < glossaries[(int)currentGlossary*2+1].Count; i++)
            {
                slots[glossaries[(int)currentGlossary*2+1][i]].DrawShadow(sb);
            }
        }
    }
}
