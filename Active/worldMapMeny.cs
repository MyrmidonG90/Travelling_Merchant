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
    class WorldMapMeny
    {
        
        City[] cities = new City[3];
        Button[] cityButtons = new Button[3];
        Button[] travelButtons = new Button[3];
        bool showText;
        string cityName;
        string cityInfo;
        Vector2 cityCords;




        public void Update(GameTime gameTime)
        {
            foreach (Button button in cityButtons)
            {
                foreach (City city in cities)
                {
                    if (button.Click() && button.name == city.name)
                    {
                        showText = true;
                        cityName = city.name;
                        cityInfo = city.information;
                        cityCords = city.Coordinates;
                    }
                    //else if (!button.Click() && KMReader.MouseClick()) // Den här koden fungerar bara för en av städerna.
                    //{                                                  // Jag har ingen aning om varför. Feel free to help.
                    //    showText = false;
                    //    cityName = null;
                    //}
                }
            }

            foreach (Button button in travelButtons)
            {
                if (button.Click())
                {


                    
                }
            }



            }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.texMap, new Vector2(0, 0), Color.White);

            foreach (Button button in cityButtons)
            {
                button.Draw(spriteBatch);
            }

            

            if (showText)
            {
                spriteBatch.DrawString(TextureManager.fontInventory, cityName, new Vector2(cityCords.X + 80, cityCords.Y), Color.White);
                spriteBatch.DrawString(TextureManager.font, "Carrot Town is the great city of carrots." +
                    "\r\nIts agricultural roots can be traced \r\nthousands of years! There is no better " +
                    "\r\nplace to buy fruits, vegetables \r\nand, of course, carrots.", new Vector2(cityCords.X + 80, cityCords.Y + 40), Color.White);

                //Funkar inte 
                //spriteBatch.DrawString(TextureManager.font, cityInfo, new Vector2(cityCords.X + 80, cityCords.Y + 40), Color.White); 

                foreach (Button button in travelButtons)
                {
                    if(button.name == cityName)
                    {
                        button.Draw(spriteBatch);
                    }
                }
            }
            

        }

        public void LoadCities()
        {

            StreamReader sr = new StreamReader("cityInfo.txt");

            int counter = 0;
            while (!sr.EndOfStream)
            {
 
                string tempName = sr.ReadLine();
                string tempInfo = sr.ReadLine();
                string tempCord = sr.ReadLine();

                string[] tempCord2 = tempCord.Split(',');

                Vector2 cord = new Vector2(int.Parse(tempCord2[0]), int.Parse(tempCord2[1]));

                cities[counter] = new City(tempName, tempInfo, cord);
                cityButtons[counter] = new Button((int)cord.X, (int)cord.Y, 75, 75, tempName, TextureManager.texBox);
                travelButtons[counter] = new Button((int)cord.X, (int)cord.Y + 80, 75, 30, tempName, TextureManager.texBox);
                counter++;
            }
        }

    }
}
