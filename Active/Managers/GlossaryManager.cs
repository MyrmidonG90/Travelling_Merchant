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
            Item,
            TravelEncounters,
            WorldEvents
        }
        static Glossary currentGlossary;

        internal static List<Slot> Slots { get => slots; }
       

        static public void Initialize(string glossary)
        {
            LoadGlossary();            
            InitiateSlots((Glossary)GetGlossaryIndex(glossary));

        }

        static public void InitateGlossary(string glossary)
        {
            currentGlossary = (Glossary)GetGlossaryIndex(glossary);
        }

        static void InitiateSlots(Glossary glossary)
        {
            slots = new List<Slot>();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    slots.Add(new Slot(320 + 150 * j, 210 + 150 * i, 120, 120));
                }
            }

            if (glossary == Glossary.Item)
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
            while (found != false)
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
            for (int i = 0; i < 25; i++)
            {
                slots[i].AddTexture(ItemCreator.CreateItem(i,0).Tex);
            }
        }
        static void InitiateTravelEncountersSlots()
        {
            
        }
        static void InitiateWorlEventsSlots()
        {
            
        }

        static public void UpdateGlossary(string glossary)
        {
            int index = GetGlossaryIndex(glossary);
            if (finished[index/2] == false) // Change this into polymorphism!!!
            {
                if (index == 0)
                {
                    CheckAcceptedTrade();
                }
                else if (index == 2)
                {

                }
                else if (index == 4)
                {

                }
            }
        }

        static public void CheckAcceptedTrade()
        {
            Inventory inv = Trading.TradeRight;
            int counter = 0;
            while (counter < glossaries[1].Count)
            {
                if (inv.FindIndexOf(glossaries[1][counter]) != -1)
                {
                    int tmp = inv.FindIndexOf(glossaries[1][counter]);
                    glossaries[0].Add(inv.ItemList[tmp].ID);
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

        static public void CheckTravelEncounter()
        {
            
        }

        static public void CheckWorldEvents()
        {
            
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
            /*for (int i = 0; i < glossaries[(int)currentGlossary].Count; i++)
            {
                slots[glossaries[(int)currentGlossary][i]].Draw(sb);
            }
            for (int i = 0; i < glossaries[(int)currentGlossary+1].Count; i++)
            {
                slots[glossaries[(int)currentGlossary+1][i]].DrawShadow(sb);
            }*/
            for (int i = 0; i < glossaries[0].Count; i++)
            {
                slots[glossaries[0][i]].Draw(sb);
            }
            for (int i = 0; i < glossaries[1].Count; i++)
            {
                slots[glossaries[1][i]].DrawShadow(sb);
            }

        }
    }
}
