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
        static List<Slot> slots;
        static bool itemFinished, travelEncountersFinished, worldEventsFinished;
        static int amountOfGlossaries;
        enum Glossary
        {
            Item,
            TravelEncounters,
            WorldEvents
        }
        static Glossary currentGlossary;

        internal static List<Slot> Slots { get => slots; }

        static public void Initialize()
        {
            LoadGlossary();
            amountOfGlossaries = glossaries.Count/2;
        }

        static public void ChangeGlossary(string glossary)
        {
            bool found = false;
            while (found != false)
            {
                if (currentGlossary.ToString()==glossary)
                {
                    found = true;
                }
                else
                {
                    currentGlossary = (Glossary)((int)(currentGlossary+1) % amountOfGlossaries);
                }
            }
        }



        static public void CheckAcceptedTrade(Inventory inv)
        {
            if (!itemFinished)
            {
                int counter = 0;
                while (counter < glossaries[0].Count)
                {
                    if (inv.FindIndexOf(glossaries[0][counter]) != -1)
                    {
                        glossaries[1].Add(inv.FindIndexOf(glossaries[0][counter]));
                        glossaries[0].RemoveAt(counter);
                    }
                    else
                    {
                        ++counter;
                    }
                }
                if (glossaries[0].Count == 0)
                {
                    itemFinished = true;
                    glossaries[0].Add(-1);
                }
            }
        }

        static public void CheckTravelEncounter()
        {
            if (!travelEncountersFinished)
            {
                if (glossaries[2].Count == 0)
                {
                    itemFinished = true;
                    glossaries[2].Add(-1);
                }
            }
        }

        static public void CheckWorldEvents()
        {
            if (!worldEventsFinished)
            {
                if (glossaries[4].Count == 0)
                {
                    itemFinished = true;
                    glossaries[4].Add(-1);
                }
            }
        }

        static void LoadGlossary()
        {
            glossaries = FileManager.LoadGlossary();
            for (int i = 0; i < glossaries.Count; i++)
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
            for (int i = 0; i < glossaries[(int)currentGlossary].Count; i++)
            {
                slots[glossaries[(int)currentGlossary][i]].Draw(sb);
            }
            for (int i = 0; i < glossaries[(int)currentGlossary+1].Count; i++)
            {
                slots[glossaries[(int)currentGlossary][i]].DrawShadow(sb);
            }
        }
    }
}
