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
        double timerLength;

        string turnDisplay;

        string destination;
        bool paused;
        bool test;

        Button invButton;
        Button pauseButton;
        Button mapButton;

        public TravelMenu()
        {
            turnsToTravel = 5;
            turnsLeft = 0;
            turnTimer = timerLength;
            timerLength = 1;
            destination = "CarrotTown"; 

            turnDisplay = turnsLeft.ToString() + "/" + turnsToTravel.ToString();

            pauseButton = new Button(830, 900, 260, 120, "paused", "Pause/Unpause", TextureManager.texWhite);
            invButton = new Button(330, 900, 260, 120, "inv", "Inventory", TextureManager.texWhite);
            mapButton = new Button(1330, 900, 260, 120, "map", "Map", TextureManager.texWhite);

            test = false;
        }

        public void StartTravel(string destination)
        {
            this.destination = destination;

            paused = false;

            turnsToTravel = 5;
            turnsLeft = turnsToTravel;
            turnTimer = timerLength;
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
                Calendar.AddDays(1);
                turnsLeft--;
                turnTimer = timerLength;
            }

            if (turnsLeft == 0)
            {
                return true;
            }

            return false;
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            Vector2 temp = TextureManager.fontHeader.MeasureString(turnDisplay);
            spriteBatch.DrawString(TextureManager.fontHeader, turnDisplay, new Vector2((1920 - temp.X)/2, 200), Color.White);
            //for debugging i guess
            if (test)
            {
                spriteBatch.DrawString(TextureManager.font, " " + turnTimer, new Vector2(50, 150), Color.White);
            }
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
