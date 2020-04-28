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
        int turnsLeft;
        double turnTimer;

        string turnDisplay;

        string destination;
        bool paused;

        Button invButton;
        Button pauseButton;
        Button mapButton;

        public TravelMenu()
        {
            turnsToTravel = 5;
            turnsLeft = 0;
            turnTimer = 3;
            destination = "CarrotTown"; 

            turnDisplay = turnsLeft.ToString() + "/" + turnsToTravel.ToString();

            pauseButton = new Button(810, 900, 300, 100, "paused", "Pause/Unpause", TextureManager.texWhite);
            invButton = new Button(1310, 900, 300, 100, "inv", "Inventory", TextureManager.texWhite);
            mapButton = new Button(310, 900, 300, 100, "map", "Map", TextureManager.texWhite);

        }

        public void StartTravel(string destination)
        {
            this.destination = destination;

            paused = false;

            turnsToTravel = 5;
            turnsLeft = turnsToTravel;
            turnTimer = 3;
        }

        public bool CheckInvbutton()
        {
            if (invButton.Click() && paused)
            {
                return true;
            }

            return false;
        }

        public bool CheckMapButton()
        {
            if (mapButton.Click() && paused)
            {
                return true;
            }

            return false;
        }

        public bool Update(GameTime gameTime)
        {
            turnDisplay = turnsLeft.ToString() + "/" + turnsToTravel.ToString();

            if (pauseButton.Click())
            {
                paused = !paused; 
            }

            if(turnTimer > 0 && !paused)
            {
                turnTimer = turnTimer - gameTime.ElapsedGameTime.TotalSeconds;
            }

            if(turnTimer <= 0)
            {
                turnsLeft--;
                turnTimer = 3;
            }

            if (turnsLeft == 0)
            {
                return true;
            }

            return false;
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(TextureManager.font, turnDisplay, new Vector2(50, 50), Color.White);
            spriteBatch.DrawString(TextureManager.font, " " + turnTimer, new Vector2(50, 150), Color.White);
            pauseButton.Draw(spriteBatch);
            if (paused)
            {
                invButton.Draw(spriteBatch);
                mapButton.Draw(spriteBatch);
            }
        }

        public string Destination
        {
            get
            {
                return destination;
            }
            set
            {
                destination = value;
            }
        }

        public int TurnsLeft
        {
            get
            {
                return turnsLeft;
            }
            set
            {
                turnsLeft = value;
            }
        }
    }
}
