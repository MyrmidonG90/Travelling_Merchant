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

        public ItemCreator()
        {
            StreamReader streamReader = new StreamReader("Items.txt");


            itemData = new string[int.Parse(streamReader.ReadLine()), 4];
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
            foreach (string tempstring in itemData)
            {
                tempstring.Replace("\n", Environment.NewLine);
            }
        }
    }
}
