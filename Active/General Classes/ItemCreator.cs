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
            itemData = new string[length, 6];
            int counter = 0;
            while (!streamReader.EndOfStream)
            {
                string tempData = streamReader.ReadLine();
                string[] tempData2 = tempData.Split('|');
                for (int j = 0; j < tempData2.Length; j++)
                {
                    itemData[counter, j] = tempData2[j];
                }
                counter++;
            }
            streamReader.Close();

            for (int i = 0; i < length; i++)
            {
                itemData[i, 5] = itemData[i, 5].Replace(";", "\n");

                //for safekeeping /my

                //if (int.Parse(itemData[i, 3]) == 999)
                //{
                //    itemData[i, 3] = null;
                //}

                //if (int.Parse(itemData[i, 2]) == 999)
                //{
                //    itemData[i, 2] = null;
                //}

                //if (int.Parse(itemData[i, 1]) == 999)
                //{
                //    itemData[i, 1] = null;
                //}
            }        
        }

        static public Item CreateItem(int id, int amount)
        {
            Item newItem;

            newItem = new Item(itemData[id, 0], int.Parse(itemData[id, 1]), TextureManager.texItems[id], id, int.Parse(itemData[id, 2]), int.Parse(itemData[id, 3]), int.Parse(itemData[id, 4]), amount, itemData[id, 5]);
          
            return newItem;
        }
    }
}
