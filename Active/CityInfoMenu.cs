using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    static class CityInfoMenu
    {
        static Rectangle mainBox;
        static Button exit;
        static Button go;
        static bool active;
        static string selected;
        static string desc;
        static Vector2 loreHeaderPos;
        static Vector2 lorePos;


        static public void Init()
        {
            mainBox = new Rectangle(460, 200, 1000, 600);
            exit = new Button(465, 205, 70, 70, TextureManager.texIconTrashCan);
            go = new Button(830, 820, 260, 120, TextureManager.texButtonGo);
            loreHeaderPos = new Vector2(1100, 290);
            lorePos = new Vector2(950, 360);
        }

        static public bool Update()
        {
            if (active)
            {
                if (exit.LeftClick())
                {
                    active = false;
                }
                foreach (City tempCity in WorldMapMenu.Cities)
                {
                    if (selected == tempCity.Name)
                    {
                        desc = tempCity.Information;
                    }
                }
                if (go.LeftClick() && !TravelMenu.travelling)
                {
                    if (TravelMenu.StartTravel(selected))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(TextureManager.texWhite, mainBox, Color.LightGray);
                exit.Draw(spriteBatch);
                go.Draw(spriteBatch);
                spriteBatch.DrawString(TextureManager.font32, "Lore", loreHeaderPos, Color.Black);
                spriteBatch.DrawString(TextureManager.font18, desc, lorePos, Color.Black);


                Vector2 temp =  TextureManager.font48.MeasureString(selected);
                Vector2 pos = new Vector2((1920 - temp.X) / 2, 210);
                spriteBatch.DrawString(TextureManager.font48, selected, pos, Color.Black);
            }
        }

        static public bool Active
        {
            get => active;
            set => active = value;
        }

        static public string Selected
        {
            get => selected;
            set => selected = value;
        }
    }
}
