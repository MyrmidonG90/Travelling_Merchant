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
    class CityMenu
    {
        Button inventoryButton;
        Button tradeButton;
        Button mapButton;
        Button nextTurnButton;

        string currentCity;
        string currentCityInfo;

        public CityMenu()
        {
            inventoryButton = new Button(60, 920, 260, 120, "inv", "Inventory", TextureManager.texButton);
            tradeButton = new Button(400, 920, 260, 120, "trade", "Trade", TextureManager.texButton);
            nextTurnButton = new Button(1260, 920, 260, 120, "turn", "Next Turn", TextureManager.texButton);
            mapButton = new Button(1600, 920, 260, 120, "map", "Map", TextureManager.texButton);
        }

        public void Update(GameTime gameTime)
        {
            if (nextTurnButton.Click())
            {
                Calendar.AddDays(1);
            }
        }

        public bool CheckInvButton()
        {
            if (inventoryButton.Click())
            {
                return true;
            }
            return false;
        }
        public bool CheckTradeButton()
        {
            if (tradeButton.Click())
            {
                return true;
            }
            return false;
        }
        public bool CheckMapButton()
        {
            if (mapButton.Click())
            {
                return true;
            }
            return false;
        }
        public void Draw(SpriteBatch spriteBatch, City[] cities)
        {
            if (currentCity != Player.Location)
            {
                foreach (City tempCity in cities)
                {
                    if (tempCity.Name == Player.Location)
                    {
                        currentCity = tempCity.Name;
                        currentCityInfo = tempCity.Information;
                    }
                }
            }

            inventoryButton.Draw(spriteBatch);
            tradeButton.Draw(spriteBatch);
            mapButton.Draw(spriteBatch);
            nextTurnButton.Draw(spriteBatch);
            spriteBatch.DrawString(TextureManager.fontHeader, currentCity, new Vector2(30, 100), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            spriteBatch.DrawString(TextureManager.fontInventory, currentCityInfo, new Vector2(40, 180), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            //spriteBatch.DrawString(TextureManager.font, "Population: ", new Vector2(40, 160), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            //spriteBatch.DrawString(TextureManager.font, "Races: ", new Vector2(40, 200), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            //spriteBatch.DrawString(TextureManager.font, "Kingdom: ", new Vector2(40, 240), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            //spriteBatch.DrawString(TextureManager.font, "Resources: ", new Vector2(40, 280), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
        }
    }
}
