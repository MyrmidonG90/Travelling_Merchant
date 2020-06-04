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
        static Vector2 infoPos;


        static public void Init()
        {
            mainBox = new Rectangle(460, 180, 1000, 600);
            exit = new Button(485, 205, 80, 80, TextureManager.texButtonReset);
            go = new Button(830, 820, 260, 120, TextureManager.texButtonGo);
            loreHeaderPos = new Vector2(1100, 290);
            lorePos = new Vector2(950, 360);
            infoPos = new Vector2(670, 290);
        }
        static List<string> Radbrytte(string text)
        {
            List<string> strings = new List<string>();
            string[] split = text.Split(';');

            for (int i = 0; i < split.Length; i++)
            {
                strings.Add(split[i]);
            }

            return strings;
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
                spriteBatch.Draw(TextureManager.texCityInfoMenu, mainBox, Color.White);
                exit.Draw(spriteBatch);
                if (selected != Player.Location)
                {
                    go.Draw(spriteBatch);
                }
                spriteBatch.DrawString(TextureManager.font32, "Lore", loreHeaderPos, Color.Black);
                int counter = 0;
                foreach (City city in WorldMapMenu.Cities)
                {
                    if (selected == city.Name)
                    {
                        if (Player.VisitedCities[counter])
                        {
                            List<string> radbrytte = Radbrytte(desc);
                            for (int i = 0; i < radbrytte.Count; i++)
                            {
                                spriteBatch.DrawString(TextureManager.font18, radbrytte[i], new Vector2(lorePos.X,lorePos.Y+ i*TextureManager.font18.MeasureString(radbrytte[i]).Y), Color.Black);
                            }
                            //spriteBatch.DrawString(TextureManager.font18, desc, lorePos, Color.Black);
                        }
                    }
                    counter++;
                }

                DrawCityDeals(spriteBatch);

                CheckAndDrawTravelTime(spriteBatch);

                Vector2 temp =  TextureManager.font48.MeasureString(selected);
                Vector2 pos = new Vector2((1920 - temp.X) / 2, 210);
                spriteBatch.DrawString(TextureManager.font48, selected, pos, Color.Black);
            }
        }

        static void DrawCityDeals(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.font32, "Info", infoPos, Color.Black);

            foreach (City tempCity in WorldMapMenu.Cities)
            {
                if (tempCity.Name == selected)
                {
                    spriteBatch.DrawString(TextureManager.font32, "Buy!", new Vector2(480, 380), Color.White);
                    spriteBatch.DrawString(TextureManager.font32, "Sell!", new Vector2(480, 540), Color.White);                  
                    try //expert felhantering
                    {
                        spriteBatch.Draw(TextureManager.texCategories[tempCity.GoodTrade[0]], new Rectangle(620, 360, 120, 120), Color.White);
                        spriteBatch.Draw(TextureManager.texCategories[tempCity.GoodTrade[1]], new Rectangle(780, 360, 120, 120), Color.White);
                    }
                    catch
                    {

                    }
                    try
                    {
                        spriteBatch.Draw(TextureManager.texCategories[tempCity.BadTrade[0]], new Rectangle(620, 520, 120, 120), Color.White);
                        spriteBatch.Draw(TextureManager.texCategories[tempCity.BadTrade[1]], new Rectangle(780, 520, 120, 120), Color.White);
                    }
                    catch
                    {

                    }
                    spriteBatch.Draw(TextureManager.texRaces[tempCity.Race], new Vector2(480, 650), Color.White);
                }
            }
        }

        static void CheckAndDrawTravelTime(SpriteBatch spriteBatch)
        {
            int travelTime = 0;
            for (int i = 0; i < 14; i++)
            {
                if (TravelMenu.RouteNames[i, 0] == Player.Location)
                {
                    if (TravelMenu.RouteNames[i, 1] == selected)
                    {
                        travelTime = TravelMenu.Routes[i];
                    }
                }
            }

            for (int i = 0; i < 14; i++)
            {
                if (TravelMenu.RouteNames[i, 0] == selected)
                {
                    if (TravelMenu.RouteNames[i, 1] == Player.Location)
                    {
                        travelTime = TravelMenu.Routes[i];
                    }
                }
            }

            if (travelTime != 0)
            {
                spriteBatch.DrawString(TextureManager.font18, "Travel time:\n   " + travelTime.ToString() + " days", new Vector2(720, 680), Color.Black);
                spriteBatch.Draw(TextureManager.texTravelTime, new Rectangle(600, 660, 100, 100), Color.White);
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
