using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    static class WorldEventManager
    {
        static List<string> eventNameList;
        static List<string> eventDesList;
        static List<bool[]> effectIDList;
        static List<int[]> effectValList;
        static List<int> durationList;
        static List<ItemModifierTemplate> itemModifierList;
        static List<InventoryTemplate> inventoryList;
        static List<WorldEvent> activeEvents;
        static int stage;

        static public void Init()
        {
            eventNameList = new List<string>();
            eventDesList = new List<string>();
            effectIDList = new List<bool[]>();
            effectValList = new List<int[]>();
            durationList = new List<int>();
            itemModifierList = new List<ItemModifierTemplate>();
            inventoryList = new List<InventoryTemplate>();

            StreamReader sr = new StreamReader("./Data/WorldEvents.txt");

            stage = 0;
            while (!sr.EndOfStream)
            {
                if (stage == 0)
                {
                    eventNameList.Add(sr.ReadLine());
                    eventDesList.Add(sr.ReadLine());

                    string temp = sr.ReadLine();
                    string[] temp2 = temp.Split(';');
                    bool[] data = new bool[4];

                    for (int i = 0; i < temp2.Length; i++)
                    {
                        if (temp2[i] == "true")
                        {
                            data[i] = true;
                        }
                        else
                        {
                            data[i] = false;
                        }
                    }
                    effectIDList.Add(data);

                    temp = sr.ReadLine();
                    temp2 = temp.Split(';');
                    int[] data2 = new int[4];

                    for (int i = 0; i < temp2.Length; i++)
                    {
                        data2[i] = int.Parse(temp2[i]);
                    }
                    effectValList.Add(data2);
                    durationList.Add(int.Parse(sr.ReadLine()));
                }
                else if (stage == 1)
                {
                    int id = int.Parse(sr.ReadLine());
                    int counter = int.Parse(sr.ReadLine());
                    List<int> itemCategories = new List<int>();
                    List<float> itemModifiers = new List<float>();
                    for (int i = 0; i < counter; i++)
                    {
                        itemCategories.Add(int.Parse(sr.ReadLine()));
                        itemModifiers.Add(float.Parse(sr.ReadLine()));
                    }
                    itemModifierList.Add(new ItemModifierTemplate(id, itemCategories, itemModifiers));
                }
                else if (stage == 2)
                {
                    int id = int.Parse(sr.ReadLine());
                    int counter = int.Parse(sr.ReadLine());
                    List<bool> amountNegOrPos = new List<bool>();
                    List<Item> items = new List<Item>();
                    for (int i = 0; i < counter; i++)
                    {
                        int itemID = int.Parse(sr.ReadLine());
                        int amount = int.Parse(sr.ReadLine());
                        items.Add(ItemCreator.CreateItem(itemID, amount));
                        if (sr.ReadLine() == "-")
                        {
                            amountNegOrPos.Add(false);
                        }
                        else
                        {
                            amountNegOrPos.Add(true);
                        }
                    }
                    inventoryList.Add(new InventoryTemplate(id, amountNegOrPos, items));
                }

                if (sr.ReadLine() == "--------")
                {
                    stage++;
                }
            }
        }

        static public void Update()
        {

        }
    }
}
