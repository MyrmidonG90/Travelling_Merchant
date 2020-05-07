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
    static class WorldMapMenu
    {
        static int nrCities = 10;
        static City[] cities = new City[nrCities];
        static Button[] cityButtons = new Button[nrCities];
        static Button[] travelButtons = new Button[nrCities];

        static public Button inventoryButton = new Button(70, 920, 260, 120, "inv", "Inventory", TextureManager.texButton);
        static public Button returnButton = new Button(20, 20, 80, 80, TextureManager.texBackArrow);
      
        static List<string> itemList = new List<string>();
      
        static bool showText;

        static string cityName;
        static string cityInfo;
        static Vector2 cityCords;

      static void ItemList()
        {
            StreamReader sr = new StreamReader("./Data/ItemList.txt");

            while (!sr.EndOfStream)
            {

                string tempName = sr.ReadLine();
                itemList.Add(tempName);

            }
            sr.Close();
        }
      
        static public string CheckNewTravel()

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

        static public void Update(GameTime gameTime)
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

        static public void UpdateCities()
        {
            foreach (City tempCity in cities)
            {
                tempCity.Update();
            }
        }

        static public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.texMap, new Vector2(0, 0), Color.White);

            foreach (Button button in cityButtons)
            {
                button.Draw(spriteBatch);
            }
            
            inventoryButton.Draw(spriteBatch);
            returnButton.Draw(spriteBatch);

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

        static public void LoadCities()
        {
            ItemList();
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
                //CityManager.CreateCity(tempName,tempInfo,cord);//
                counter++;
            }
            sr.Close();

            //CityManager.SaveCities();
            CityManager.LoadCities();
            /*CityManager.SaveCities();
            CityManager.LoadCities();*/

            for (int i = 0; i < cities.Length; i++)
            {
                cities[i].Information = cities[i].Information.Replace(";", "\n");
            }

            foreach (City tempCity in cities)
            {
                tempCity.AddInventory(LoadCityMerchant(tempCity.Name));
                tempCity.InvInit();
            }
        }

        static private Inventory LoadCityMerchant(string name)
        {
            string path = Path.Combine("./Data/City_Data/", name + ".txt");
            StreamReader sr = new StreamReader(path);
            Inventory temp = new Inventory(int.Parse(sr.ReadLine()));
            int counter = int.Parse(sr.ReadLine());
            for (int i = 0; i < counter; i++)
            {
                string data = sr.ReadLine();
                string[] data2 = data.Split(';');

                int placeCounter = 0;
                int tempNr = 0;
                foreach (string item in itemList)
                {
                    if (item == data2[0])
                    {
                        tempNr = placeCounter;
                    }
                    placeCounter++;
                }
                
                Item newItem = ItemCreator.CreateItem(tempNr, int.Parse(data2[1]));
                temp.AddItem(newItem);
            }

            return temp;
        }

        static public City[] Cities
        {
            get
            {
                return cities;
            }
        }

        static public string CityName
        {
            get
            {
                return cityName;
            }
        }
        static public string CityInfo
        {
            get
            {
                return cityInfo;
            }
        }
    }
}
