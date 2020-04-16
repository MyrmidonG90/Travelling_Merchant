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
    public class CityMeny
    {
        Button inventoryButton;
        Button tradeButton;
        Button mapButton;

        public CityMeny()
        {
            inventoryButton = new Button(70, 920, 230, 120, TextureManager.texWhite);
            tradeButton = new Button(420, 920, 230, 120, TextureManager.texWhite);
            mapButton = new Button(1620, 920, 230, 120, TextureManager.texWhite);
        }

        public void Update(GameTime gameTime)
        {

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
        public void Draw(SpriteBatch spriteBatch)
        {

            inventoryButton.Draw(spriteBatch);
            tradeButton.Draw(spriteBatch);
            mapButton.Draw(spriteBatch);
            spriteBatch.DrawString(TextureManager.font, "CITY NAME: ", new Vector2(30, 50), Color.Black, 0, Vector2.Zero, 4, SpriteEffects.None, 1);
            spriteBatch.DrawString(TextureManager.font, "City Type: ", new Vector2(40, 120), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(TextureManager.font, "Population: ", new Vector2(40, 160), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(TextureManager.font, "Races: ", new Vector2(40, 200), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(TextureManager.font, "Kingdom: ", new Vector2(40, 240), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(TextureManager.font, "Resources: ", new Vector2(40, 280), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(TextureManager.font, "Inventory ", new Vector2(120, 962), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(TextureManager.font, "Trade ", new Vector2(495, 962), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(TextureManager.font, "Map ", new Vector2(1700, 962), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
        }
    }
}
