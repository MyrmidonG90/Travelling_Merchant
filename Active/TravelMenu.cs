using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
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
        static string oldLocation;  // Never declared and never used?
        static bool paused;
        static bool test;
        static public bool travelling;

        static Button invButton;
        static Button pauseButton;
        static Button mapButton;

        static string[,] routeNames;
        static int[] routes;

        static public void Init()
        {
            turnsToTravel = 5;
            turnsLeft = 0;
            turnTimer = timerLength;
            timerLength = 1;
            destination = Player.Location;

            turnDisplay = turnsLeft.ToString() + "/" + turnsToTravel.ToString();

            pauseButton = new Button(830, 900, 260, 120, "paused", "Pause/Unpause", TextureManager.texButton);
            invButton = new Button(330, 900, 260, 120, "inv", "Inventory", TextureManager.texButton);
            mapButton = new Button(1330, 900, 260, 120, "map", "Map", TextureManager.texButton);
            test = false;

            LoadRoutes();
            int i = 0;
        }

        static public void StartTravel(string newDestination)
        {
            for (int i = 0; i < 14; i++)
            {
                if (routeNames[i, 0] == Player.Location)
                {
                    if (routeNames[i, 1] == newDestination)
                    {
<<<<<<< HEAD
                        if (routeNames[j, 1] == newDestination)
                        {
                            destination = newDestination;
                            paused = false;
                            travelling = true;
                            float tmp= Vector2.Distance(CityManager.Cities[CityManager.FindCityIndex(Player.Location)].Coordinates, CityManager.Cities[CityManager.FindCityIndex(newDestination)].Coordinates);
                            
                            turnsToTravel = 5;
                            turnsLeft = turnsToTravel;
                            turnTimer = timerLength;
=======
                        destination = newDestination;
                        paused = false;
                        travelling = true;

                        turnsToTravel = routes[i];
                        turnsLeft = turnsToTravel;
                        turnTimer = timerLength;
>>>>>>> master

                        EncounterManager.NewTrip();
                    }                   
                }
            }
            for (int i = 0; i < 14; i++)
            {
                if (routeNames[i, 0] == newDestination)
                {
                    if (routeNames[i, 1] == Player.Location)
                    {
                        destination = newDestination;
                            paused = false;
                            travelling = true;

                            turnsToTravel = routes[i];
                            turnsLeft = turnsToTravel;
                            turnTimer = timerLength;

                            EncounterManager.NewTrip();
                        
                    }
                }
            }
        }

        static public void AbortTravel()
        {
            destination = Player.Location;
            paused = false;

            turnsToTravel = 5;
            turnsLeft = turnsToTravel - turnsLeft;
            turnTimer = timerLength;

            EncounterManager.NewTrip();
        }

        static public bool CheckInvbutton()
        {
            if (invButton.LeftClick() && paused)
            {
                return true;
            }

            return false;
        }

        static public bool CheckMapButton()
        {
            if (mapButton.LeftClick() && paused)
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
                if (EncounterManager.Update())
                {
                    paused = false;
                }
            }

            if (pauseButton.LeftClick())
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
                    travelling = false;
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

        static private void LoadRoutes()
        {
            StreamReader streamReader;
            streamReader = new StreamReader("./Data/routeInfo.txt");
            routes = new int[14];
            routeNames = new string[14, 2];

            int counter = 0;
            while (!streamReader.EndOfStream)
            {
                string temp = streamReader.ReadLine();
                string[] data = temp.Split('|');

                routeNames[counter, 0] = data[0];
                routeNames[counter, 1] = data[1];
                routes[counter] = int.Parse(data[2]);
                counter++;
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

        static public string OldLocation
        {
            get => oldLocation;
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
