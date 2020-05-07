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

        static List<string> itemCategories = new List<string>();
        static List<float> itemModifiers = new List<float>();
        static int amountOfCities, amountOfCategories;
        
        //0.Food
        //1.Metal 
        //2.Raw Materials
        //3.Textiles
        //4.Gear
        //5.Magic
        //6.Valuables
        //7.Manufactured

        //om du inte fattar vafan som händer här inne så ändrades det senast av My 6 maj
                

            
        


        static public double GetModifier(string city, int category)

        {
            category--;
            int counter = 0;
            for (int i = 0; i < amountOfCities; i++)
            {
                for (int j = 0; j < amountOfCategories; j++)
                {
                    if (city == cities[i] && j == category) // Söker efter stadens kategori modifiers.
                    {
                        return itemModifiers[counter]; // Return modifier
                    }
                    counter++;
                }
            }         
            return 1; // return modifier 1 in case of errors 
        }

        static public void SetModifier(string city, int category, float modifier)
        {
            int counter = 0;
            for (int i = 0; i < amountOfCities; i++)
            {
                for (int j = 0; j < amountOfCategories; j++)
                {
                    category--;
                    if (city == cities[i] && j == category) // Söker efter stadens kategori modifiers.
                    {
                        itemModifiers[counter] = modifier;
                    }
                    counter++;
                }
            }
        }

        static public void AddModifier(string city, int category, float modifier)
        {
            int counter = 0;
            for (int i = 0; i < amountOfCities; i++)
            {
                for (int j = 0; j < amountOfCategories; j++)
                {
                    category--;
                    if (city == cities[i] && itemCategories[j] == category) // Söker efter stadens kategori modifiers.
                    {
                        itemModifiers[counter] += modifier;
                    }
                    counter++;
                }
            }
        }

        static public void LoadItemModifiers()
        {
            StreamReader sr = new StreamReader("./Data/ItemPrices.txt");
            while (!sr.EndOfStream)
            {
                for (int i = 0; i < amountOfCities; i++)
                {
                    sr.ReadLine();
                    for (int j = 0; j < amountOfCategories; j++)
                    {

                        string temp = sr.ReadLine();
                        itemCategories.Add(temp);
                        temp = sr.ReadLine();
                        itemModifiers.Add(float.Parse(temp));                        

                    }
                }
            }
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
