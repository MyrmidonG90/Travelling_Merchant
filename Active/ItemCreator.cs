using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Active
{
    static class ItemCreator
    {

        static public string[,] itemData;
        static int length;

        //This method is responsible for loading in data for all the items in the game the data file is structured as follows:
        //the first line only contains a number which has to be correspondent to the number of items that are currently present
        //in the data-file after that each line contains the data for one item with the following the following format: [NAME],[BASE PRICE],[CATEGORY],[DESCRIPTION]
        //the final bit of the method is used to replace the "stand-ins" we use for new-lines since it will not work if you try and read "\n" from a external file
        static public void LoadItemData()
        {
            StreamReader streamReader = new StreamReader("./Data/Items.txt");

            length = int.Parse(streamReader.ReadLine());
            itemData = new string[length, 4];
            int counter = 0;
            while (!streamReader.EndOfStream)
            {
                string tempData = streamReader.ReadLine();
                string[] tempData2 = tempData.Split(',');
                for (int j = 0; j < tempData2.Length; j++)
                {
                    itemData[counter, j] = tempData2[j];
                }
                counter++;
            }
            for (int i = 0; i < length; i++)
            {
                itemData[i, 3] = itemData[i, 3].Replace(";", "\n");
            }
            streamReader.Close();
        }

        static public Item CreateItem(int id, int amount)
        {

            Item newItem;
            if (id == 0)
            {
                newItem = new Item(itemData[id, 0], int.Parse(itemData[id, 1]), TextureManager.texCarrot, id, int.Parse(itemData[id, 2]), amount, itemData[id, 3]);
            }
            else if (id == 1)
            {
                newItem = new Item(itemData[id, 0], int.Parse(itemData[id, 1]), TextureManager.texPotato, id, int.Parse(itemData[id, 2]), amount, itemData[id, 3]);
            }
            else //id = 2
            {
                newItem = new Item(itemData[id, 0], int.Parse(itemData[id, 1]), TextureManager.texIronIngot, id, int.Parse(itemData[id, 2]), amount, itemData[id, 3]);
            }
            return newItem;
        }
    }
}
