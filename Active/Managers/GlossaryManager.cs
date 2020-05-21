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
        static int amountOfGlossaries;
        enum Glossary
        {
            Item,
            TravelEncounters,
            WorldEvents
        }
        static Glossary currentGlossary;

        static public void Initialize()
        {
            amountOfGlossaries = 3;
            glossaries = new List<List<int>>();
            for (int i = 0; i < amountOfGlossaries; i++)
            {
                glossaries.Add(new List<int>()); // Discovered/Encountered
                glossaries.Add(new List<int>()); // Left
            }           
        }
        static public void ChangeGlossary()
        {

        }

        static public void CheckAcceptedTrade(Inventory inv)
        {

        }

        static public void CheckTravelEncounter()
        {

        }

        static void LoadGlossary()
        {
            glossaries = FileManager.LoadGlossary();            
        }

        static public void Draw(SpriteBatch sb)
        {
            // Draws all Item slots
            for (int i = 0; i < itemsDiscovered.Count; i++)
            {
                slots[itemsDiscovered[i]].Draw(sb);
            }
            for (int i = 0; i < itemsLeft.Count; i++)
            {
                slots[itemsLeft[i]].DrawShadow(sb);
            }
        }
    }
}
