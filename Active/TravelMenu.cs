using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class TravelMenu
    {
        int turnsToTravel;
        int currentTurnTickDown;
        double turnTimer;

        string turnDisplay;

        public TravelMenu()
        {
            turnsToTravel = 5;
            currentTurnTickDown = turnsToTravel;
            turnTimer = 3;
        }

        public void Update(GameTime gameTime)
        {
            turnDisplay = currentTurnTickDown.ToString() + "/" + turnsToTravel.ToString();

            if(turnTimer >= 1)
            {
                turnTimer += gameTime.ElapsedGameTime.TotalSeconds;
            }

            if(turnTimer <= 0)
            {
                turnTimer = 3;
            }
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.font, turnDisplay, new Vector2(50, 50), Color.White);
        }
    }
}
