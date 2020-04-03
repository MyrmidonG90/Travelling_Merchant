using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Active
{
    class ItemCreator
    {

        public string[,] itemData;
        int length;

        public ItemCreator()
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
        }

        public Item createItem(int id, int amount)
        {

            Item newItem;
            if (id == 1)
            {
                id--;
                newItem = new Item(itemData[id, 0], int.Parse(itemData[id, 1]), TextureManager.texCarrot, id, int.Parse(itemData[id, 2]), amount, itemData[id, 3]);
            }
            else if (id == 2)
            {
                id--;
                newItem = new Item(itemData[id, 0], int.Parse(itemData[id, 1]), TextureManager.texPotato, id, int.Parse(itemData[id, 2]), amount, itemData[id, 3]);
            }
            else //id = 3
            {
                id--;
                newItem = new Item(itemData[id, 0], int.Parse(itemData[id, 1]), TextureManager.texIronIngot, id, int.Parse(itemData[id, 2]), amount, itemData[id, 3]);
            }
            return newItem;
        }
    }
}
