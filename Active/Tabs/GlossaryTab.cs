using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class GlossaryTab : TabClass
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
        public GlossaryTab()
        {
            name = "Glossary Tab";
            mainBox = new Rectangle(260, 150, 1400, 880);
            GlossaryManager.Initialize("Item");
            //GlossaryManager.InitateGlossary("Item");
            currentGlossary = (Glossary)GlossaryManager.GetGlossaryIndex("Item");
        }
        static void Start(string glossary)
        {
            currentGlossary = (Glossary)GlossaryManager.GetGlossaryIndex(glossary);

            GlossaryManager.InitateGlossary(glossary);
            indexOfSelectedSlot = -1;
        }
        override public void Update()
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
        override public void Draw(SpriteBatch sb)
        {
            sb.Draw(TextureManager.texSkillMenu, mainBox, Color.White);
            GlossaryManager.Draw(sb);
            if (indexOfSelectedSlot != -1)
            {
                sb.Draw(TextureManager.texSelect, selectedSlot, Color.White);
            }
        }
        int CheckSlotClick()
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
        
    }
}
