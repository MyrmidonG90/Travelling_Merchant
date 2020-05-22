using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active.Menu
{
    static class GlossaryMenu
    {
        static Rectangle selectedSlot;
        static int indexOfSelectedSlot;
        enum Glossary
        {
            Items,
            TravelEncounters,
            WorldEvents
        }
        static Glossary currentGlossary;
        static void Start(string glossary)
        {
            currentGlossary = (Glossary)GlossaryManager.GetGlossaryIndex(glossary);
            GlossaryManager.InitateGlossary(glossary);
            indexOfSelectedSlot = -1;
        }
        static public void Update()
        {
            if (KMReader.LeftMouseClick())
            {                
                if (CheckSlotClick() != -1)
                {
                    int tmp = CheckSlotClick();
                    if (tmp != -1) // Om en slot blir klickad
                    {
                        if (tmp != indexOfSelectedSlot) // Om en slot som inte redan är iclickad blir clickad
                        {
                            indexOfSelectedSlot = tmp;
                            selectedSlot = new Rectangle(320 + 150 * indexOfSelectedSlot % 5, 210 + 150 * indexOfSelectedSlot / 5, 120, 120);
                        }
                    }
                }
            }            
        }
        static public void Draw(SpriteBatch sb)
        {
            GlossaryManager.Draw(sb);
            if (indexOfSelectedSlot != -1)
            {
                sb.Draw(TextureManager.texSelect, selectedSlot, Color.White);
            }
        }
        static int CheckSlotClick()
        {
            int counter = 0;
            bool found = false;
            while (found != false && counter < GlossaryManager.Slots.Count)
            {
                if (GlossaryManager.Slots[counter].LeftClicked())
                {
                    found = true;
                }
                else
                {
                    ++counter;
                }
            }
            if (found== false)
            {
                counter = -1;
            }
            return counter;
        }
        
        /*static int CheckButtonClick()
        {
            int count = 0;
            bool found = false;

            while (found != false && count < buttons.Count)
            {
                if (buttons[count].LeftClick())
                {
                    found = true;
                }
                else
                {
                    ++count;
                }
            }

            if (found == false)
            {
                count = -1;
            }

            return count;
        }*/
    }
}
