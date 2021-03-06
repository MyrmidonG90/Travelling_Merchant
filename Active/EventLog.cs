﻿using System;
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
            spritebatch.Draw(TextureManager.texEventLog, box, Color.White);
            int counter = 0;
            foreach (string tempstring in Player.EventCities)
            {
                spritebatch.DrawString(TextureManager.font24, tempstring + "====", new Vector2(730, 180 + (60 * counter)), Color.Black);
                counter++;
            }
            counter = 0;
            foreach (string tempstring in Player.EventNames)
            {
                if (tempstring == "Plague")
                {
                }
                else if (tempstring == "War")
                {

                }
                else if (tempstring == "Crop Failure")
                {

                }
                else if (tempstring == "Good Harvest")
                {
                    spritebatch.Draw(TextureManager.texIconEventGoodHarvest, new Rectangle(1040, 180 + (60 * counter), 60, 60), Color.White);

                }
                counter++;
            }
        }
    }
}
