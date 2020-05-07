using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    static class TravelMenu
    {
        static int turnsToTravel;
        static int turnsLeft;
        static double turnTimer;
        static double timerLength;

        static string turnDisplay;

        static string destination;
        static bool paused;
        static bool test;

        static Button invButton;
        static Button pauseButton;
        static Button mapButton;

        static public void Init()
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

        static public void StartTravel(string newDestination)
        {
            destination = newDestination;
            EncounterManager.Initialize();
            paused = false;

            turnsToTravel = 5;
            turnsLeft = turnsToTravel;
            turnTimer = timerLength;
        }

        static public bool CheckInvbutton()
        {
            if (invButton.Click() && paused)
            {
                return true;
            }

            return false;
        }

        static public bool CheckMapButton()
        {
            if (mapButton.Click() && paused)
            {
                return true;
            }

            return false;
        }

        static public bool Update(GameTime gameTime)
        {
            turnDisplay = turnsLeft.ToString() + "/" + turnsToTravel.ToString();

            if (EncounterManager.EventOnGoing)
            {
                paused = true;
                EncounterManager.Update();
            }

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
                if (turnsLeft == 0)
                {
                    return true;
                }
                else
                {
                    EncounterManager.Encountered();
                }
            }            

            return false;
        }

        static public void Draw (SpriteBatch spriteBatch)
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
            if (EncounterManager.EventOnGoing)
            {
                EncounterManager.Draw(spriteBatch);
            }
        }

        static public string Destination
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

        static public int TurnsLeft
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
