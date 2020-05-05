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
        int amountOfCities, amountOfCategories;

       


        public double CheckModifiers(string city, string category)
        {
            StreamReader sr = new StreamReader("./Data/ItemPrices.txt");
            while (!sr.EndOfStream)
            {
                for (int i = 0; i < amountOfCities; i++)
                {
                    string tempCity = sr.ReadLine();
                    for (int j = 0; j < amountOfCategories; j++)
                    {
                        string tempCategoryName = sr.ReadLine();
                        string tempModifier= sr.ReadLine();

                        if (city == tempCity && tempCategoryName == category)
                        {
                            sr.Close();
                            return double.Parse(tempModifier);
                        }
                    }
                }
            }

            sr.Close();
            return 1;
        }



        public void LoadCityAndCategoryLists()
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

            StreamReader sr2 = new StreamReader("./Data/CategoryList.txt");
            amountOfCategories = 0;
            while (!sr2.EndOfStream)
            {

                string tempName = sr2.ReadLine();

                items.Add(tempName);

                amountOfCategories++;
            }
            sr2.Close();
        }

    }
}
