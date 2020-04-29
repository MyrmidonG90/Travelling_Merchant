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
    class WorldMapMenu
    {      
        City[] cities = new City[3];
        Button[] cityButtons = new Button[3];
        Button[] travelButtons = new Button[3];

        public Button inventoryButton = new Button(10, 20, 200, 50, TextureManager.texBox);
        public Button returnButton = new Button(240, 20, 200, 50, TextureManager.texBox);

        bool showText;
        
        string cityName;
        string cityInfo;
        Vector2 cityCords;

        public string CheckNewTravel()
        {
            int counter = 0;
            foreach (Button button in travelButtons)
            {
                if (button.Click())
                {
                    return cities[counter].Name;
                }
                counter++;
            }

            return null;
        }

        public void Update(GameTime gameTime)
        {
            bool temp = false;

            foreach (Button button in cityButtons)
            {
                if (button.Click())
                {
                    temp = true;
                }
            }

            if (!temp)
            {
                if (KMReader.MouseClick())
                {
                    showText = false;
                }
            }

            foreach (Button button in cityButtons)
            {
                foreach (City city in cities)
                {
                    if (button.Click() && button.Name == city.Name)
                    {
                        showText = true;
                        cityName = city.Name;
                        cityInfo = city.Information;

                        cityCords = city.Coordinates;

                    }
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
            
            inventoryButton.Draw(spriteBatch);
            returnButton.Draw(spriteBatch);
            spriteBatch.DrawString(TextureManager.fontInventory, "Return", new Vector2(280, 20), Color.Red);
            spriteBatch.DrawString(TextureManager.fontInventory, "Inventory", new Vector2(20, 20), Color.Red);

            if (showText)
            {
                spriteBatch.DrawString(TextureManager.fontInventory, cityName, new Vector2(cityCords.X + 80, cityCords.Y), Color.White);
                spriteBatch.DrawString(TextureManager.font, cityInfo, new Vector2(cityCords.X + 80, cityCords.Y + 40), Color.White);
            
                foreach (Button button in travelButtons)
                {
                    if(button.Name == cityName)
                    {
                        button.Draw(spriteBatch);
                    }
                }
            }          
        }

        public void LoadCities()
        {
            StreamReader sr = new StreamReader("./Data/cityInfo.txt");

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
                CityManager.CreateCity(tempName,tempInfo,cord); //
                counter++;
            }
            sr.Close();

            for (int i = 0; i < cities.Length; i++)
            {
                cities[i].Information = cities[i].Information.Replace(";", "\n");
            }


            CityManager.LoadCities();

            CityManager.SaveCities();

            foreach (City tempCity in cities)
            {
                tempCity.AddInventory(LoadCityMerchant(tempCity.Name));
                tempCity.InvInit();
            }
        }

        private Inventory LoadCityMerchant(string name)
        {
            string path = Path.Combine("./Data/", name + ".txt");
            StreamReader sr = new StreamReader(path);
            Inventory temp = new Inventory(int.Parse(sr.ReadLine()));
            int counter = int.Parse(sr.ReadLine());
            for (int i = 0; i < counter; i++)
            {
                string data = sr.ReadLine();
                string[] data2 = data.Split(';');
                Item newItem = ItemCreator.CreateItem(int.Parse(data2[0]), int.Parse(data2[1]));
                temp.AddItem(newItem);
            }

            return temp;
        }

        public City[] Cities
        {
            get
            {
                return cities;
            }
        }

        public string CityName
        {
            get
            {
                return cityName;
            }
        }
        public string CityInfo
        {
            get
            {
                return cityInfo;
            }
        }
    }
}
