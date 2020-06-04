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
        //OM DU ÄNDRAR VÄRDET HÄR SÅ BEHVÖER DU ÄNDRA SAMMA VÄRDE I PLAYER FÖR JAG SUGER PÅ PROGRAMMERING
        static int nrCities = 10;
        //OM DU ÄNDRAR VÄRDET HÄR SÅ BEHVÖER DU ÄNDRA SAMMA VÄRDE I PLAYER FÖR JAG SUGER PÅ PROGRAMMERING

        static City[] cities = new City[nrCities];
        static Button[] cityButtons = new Button[nrCities];
        static Button[] travelButtons = new Button[nrCities];
        static Button[] infoButtons = new Button[nrCities];

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
            string destination = "";
            int counter = 0;
            foreach (Button button in travelButtons)
            {
                if (button.LeftClick())
                {
                    destination = cities[counter].Name;
                }
                counter++;
            }
            return destination;
        }

        static public void Update(GameTime gameTime)
        {
            bool isCityClicked = false;
            //temp is used to check that a city has been pressed on and that it should display the associated 
            //buttons and info and once somewhere else has been pressed on the screen that info should disappear
            

            foreach (Button button in cityButtons)
            {
                if (button.LeftClick())
                {
                    isCityClicked = true;
                }
            }

            if (!isCityClicked)
            {
                if (KMReader.LeftMouseClick())
                {
                    showText = false;
                }
            }

            foreach (Button tempButton in infoButtons)
            {
                if (tempButton.LeftClick())
                {
                    CityInfoMenu.Active = true;
                    CityInfoMenu.Selected = tempButton.Name;
                }
            }

            foreach (Button button in cityButtons)
            {
                int counter = 0;
                foreach (City city in cities)
                {
                    if (button.LeftClick() && button.Name == city.Name)
                    {
                        showText = true;
                        cityName = city.Name;
                        cityCords = city.Coordinates;
                        cityInfo = " ";
                        if (Player.VisitedCities[counter])
                        {
                            cityInfo = city.Information;
                        }
                    }
                    counter++;
                }
            }
        }

        static public void CheckPlayerEventLog()
        {
            foreach (City tempCity in cities)
            {
                if (tempCity.Name == Player.Location)
                {
                    foreach (WorldEvent tempEvent in WorldEventManager.ActiveEvents)
                    {
                        foreach (string temptarget in tempEvent.Target)
                        {
                            if (temptarget == tempCity.Name)
                            {
                                Player.AddEventLogEntry(temptarget, tempEvent.EventName, tempEvent.InstanceID, tempEvent.DaysLeft);
                            }
                        }
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
            spriteBatch.Draw(TextureManager.texBGMap, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(TextureManager.texBGMapRoads, Vector2.Zero, Color.White);

            foreach (Button button in cityButtons)
            {
                button.Draw(spriteBatch);
                if (button.Name == Player.Location)
                {
                    Vector2 temp = new Vector2(button.HitBox.X, button.HitBox.Y - 110);
                    spriteBatch.Draw(TextureManager.texArrowDown, temp, Color.White);
                }
            }
            
            inventoryButton.Draw(spriteBatch);
            returnButton.Draw(spriteBatch);

            if (showText)
            {
                foreach (Button button in travelButtons)
                {
                    if(button.Name == cityName)
                    {
                        button.Draw(spriteBatch);
                    }
                }
                foreach (Button tempButton in infoButtons)
                {
                    if (tempButton.Name == cityName)
                    {
                        tempButton.Draw(spriteBatch);
                    }
                }
                spriteBatch.DrawString(TextureManager.font32, cityName, new Vector2(cityCords.X + 80, cityCords.Y - 5), Color.White);
                //spriteBatch.DrawString(TextureManager.font13, cityInfo, new Vector2(cityCords.X + 80, cityCords.Y + 40), Color.Black);
            }
        }

        static public void LoadCities()
        {
            ItemList();

            //StreamReader sr = new StreamReader("./Data/cityInfo.txt");
            ////posArrowDown = new Vector2(cities[0].Coordinates.X, cities[0].Coordinates.Y);

            //while (!sr.EndOfStream)
            //{

            //    string tempName = sr.ReadLine();
            //    string tempInfo = sr.ReadLine();
            //    string tempCord = sr.ReadLine();
            //    string tempNeigh = sr.ReadLine();
            //    string tempbad = sr.ReadLine();

            //    string[] tempCord2 = tempCord.Split(',');
            //    //string[] tempNeigh2 = tempNeigh.Split(',');
            //    //string[] tempbad2 = tempbad.Split(',');

            //    Vector2 cord = new Vector2(int.Parse(tempCord2[0]), int.Parse(tempCord2[1]));
            //    //List<int> good = new List<int>();
            //    //List<int> bad = new List<int>();

            //    //for (int i = 0; i < tempNeigh2.Length; i++)
            //    //{
            //    //    good.Add(int.Parse(tempNeigh2[i]));
            //    //}

            //    //for (int i = 0; i < tempbad2.Length; i++)
            //    //{
            //    //    bad.Add(int.Parse(tempbad2[i]));
            //    //}

            //    //cities[counter] = new City(tempName, tempInfo, cord, good, bad);

            //    //CityManager.CreateCity(tempName, tempInfo, cord, neighData);
            //}
            //sr.Close();

            //CityManager.SaveCities();
            //CityManager.LoadCities();
            /*CityManager.SaveCities();
            CityManager.LoadCities();*/

            //for (int i = 0; i < cities.Length; i++)
            //{
            //    cities[i].Information = cities[i].Information.Replace(";", "\n");
            //}

            cities = CityManager.Cities.ToArray();

            int counter = 0;
            foreach (City city in cities)
            {
                cityButtons[counter] = new Button((int)city.Coordinates.X, (int)city.Coordinates.Y, 68, 68, city.Name, TextureManager.texIconCity);
                travelButtons[counter] = new Button((int)city.Coordinates.X, (int)city.Coordinates.Y + 75, 68, 36, city.Name, TextureManager.texButtonGo);
                infoButtons[counter] = new Button((int)city.Coordinates.X + 80, (int)city.Coordinates.Y + 42, 30, 30, city.Name, TextureManager.texIconCityInfo);

                

                counter++;
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
