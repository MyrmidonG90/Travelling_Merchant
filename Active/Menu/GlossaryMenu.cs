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

            }
            else if (KMReader.RightMouseClick())
            {

            }
        }
        static public void Draw(SpriteBatch sb)
        {
            
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
            return c9

        }
    }
}
