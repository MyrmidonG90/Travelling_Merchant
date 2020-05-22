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
        static List<Slot> slots;
        static List<Button> buttons;
        static public void Update()
        {
            if (KMReader.LeftMouseClick())
            {
                if (CheckButtonClick() != -1)
                {

                }
                else if (CheckSlotClick() != -1)
                {

                }
            }
            else if (KMReader.RightMouseClick())
            {

            }
        }
        static public void Draw(SpriteBatch sb)
        {
            GlossaryManager.Draw(sb);
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
        static int CheckButtonClick()
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
        }
    }
}
