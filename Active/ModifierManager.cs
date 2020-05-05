using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    static public class ModifierManager
    {
        static List<string> cities = new List<string>();
        static List<int> items = new List<int>();
        static int amountOfCities, amountOfCategories;
        /*
         0.Food
         1.Metal 
         2.Raw Materials
         3.Textiles
         4.Gear
         5.Magic
         6.Valuables
         7.Manufactured

             */

        static public double GetModifier(string city, int category)
        {
            StreamReader sr = new StreamReader("./Data/ItemPrices.txt");
            while (!sr.EndOfStream)
            {
                for (int i = 0; i < amountOfCities; i++)
                {
                    string tempCity = sr.ReadLine();
                    for (int j = 0; j < amountOfCategories; j++)
                    {
                        int tempCategoryNr = int.Parse(sr.ReadLine()); // Get Category
                        string tempModifier= sr.ReadLine(); // Get Category modifier

                        if (city == tempCity && tempCategoryNr == category) // Söker efter stadens kategori modifiers.
                        {
                            sr.Close();
                            try
                            {
                                return double.Parse(tempModifier); // Return modifier
                            }
                            catch (Exception)
                            {
                                tempModifier.Split('.');
                                double tmp = 0;
                                tmp += double.Parse(tempModifier.Split('.')[0]);
                                tmp += double.Parse(tempModifier.Split('.')[1])/10;
                                return tmp;
                                
                            }
                            
                        }
                    }
                }
            }

            sr.Close();
            return 1; // return modifier 1 in case of errors 
        }

        
        
        static public void LoadCityAndCategoryLists()
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

                int tempName = int.Parse(sr2.ReadLine());

                items.Add(tempName);

                amountOfCategories++;
            }
            sr2.Close();
        }

    }
}
