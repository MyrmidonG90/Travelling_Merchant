using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Active
{
    static class EventLog
    {
        static Rectangle box;

        static public void Init()
        {
            box = new Rectangle(710, 100, 500, 600);
        }

        static public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(TextureManager.texWhite, box, Color.White);
            int counter = 0;
            foreach (string tempstring in Player.EventCities)
            {
                spritebatch.DrawString(TextureManager.fontButton, tempstring, new Vector2(720, 120 + (44 * counter)), Color.Black);
                counter++;
            }
            counter = 0;
            foreach (string tempstring in Player.EventNames)
            {
                spritebatch.DrawString(TextureManager.fontButton, tempstring, new Vector2(970, 120 + (44 * counter)), Color.Black);
                counter++;
            }
        }
    }
}
