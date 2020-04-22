using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class ModifierManager
    {
        List<string> cities = new List<string>();
        List<string> items = new List<string>();
        int amountOfCities, amountOfItems;

        string activeCity, categoryName; //temporary variables that will later be replaced with variables from another-
        int sellPrice, buyPrice;    //class that store the current city and the current item that should be checked.


        public void CheckModifiers()
        {
            StreamReader sr = new StreamReader("./Data/ItemPrices.txt");
            while (!sr.EndOfStream)
            {
                for (int i = 0; i < amountOfCities; i++)
                {
                    string tempCity = sr.ReadLine();
                    for (int j = 0; j < amountOfItems; j++)
                    {
                        string tempCategoryName = sr.ReadLine();
                        string tempBuyModifier= sr.ReadLine();
                        string tempSellModifier = sr.ReadLine();

                        if (activeCity == tempCity && tempCategoryName == categoryName)
                        {
                            sellPrice = int.Parse(tempSellModifier);
                            buyPrice = int.Parse(tempBuyModifier);
                        }
                    }
                }
            }

            sr.Close();
        }



        public void LoadCityAndItemLists()
        {
            StreamReader sr = new StreamReader("./Data/cityInfo.txt");
            amountOfCities = 0;
            while (!sr.EndOfStream)
            {

                string tempName = sr.ReadLine();
                string skip1 = sr.ReadLine();
                string skip2 = sr.ReadLine();

                cities.Add(tempName);

                amountOfCities++;
            }
            sr.Close();

            StreamReader sr2 = new StreamReader("./Data/ItemList.txt");
            amountOfItems = 0;
            while (!sr2.EndOfStream)
            {

                string tempName = sr2.ReadLine();

                items.Add(tempName);

                amountOfItems++;
            }
            sr2.Close();
        }

    }
}
