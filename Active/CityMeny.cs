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
        Game1 game1;


        public void Update(GameTime gameTime)
        {
            if (game1.InventoryButton.Click())
            {

            }

            if (game1.TradeButton.Click())
            {

            }

            if (game1.MapButton.Click())
            {

            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(game1.font, "CITY NAME: ", new Vector2(30, 50), Color.Black, 0, Vector2.Zero, 4, SpriteEffects.None, 1);
            spriteBatch.DrawString(game1.font, "City Type: ", new Vector2(40, 120), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(game1.font, "Population: ", new Vector2(40, 160), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(game1.font, "Races: ", new Vector2(40, 200), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(game1.font, "Kingdom: ", new Vector2(40, 240), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(game1.font, "Resources: ", new Vector2(40, 280), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(game1.font, "Inventory ", new Vector2(120, 962), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(game1.font, "Trade ", new Vector2(495, 962), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(game1.font, "Map ", new Vector2(1700, 962), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
        }
    }
}
