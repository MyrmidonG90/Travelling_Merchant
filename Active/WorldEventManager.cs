﻿using System;
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
        static int counter=0;
        static int oldCounter=0;

        static public void Init()
        {
            eventNameList = new List<string>();
            eventDesList = new List<string>();
            effectIDList = new List<bool[]>();
            effectValList = new List<int[]>();
            durationList = new List<int>();
            itemModifierList = new List<ItemModifierTemplate>();
            inventoryList = new List<InventoryTemplate>();
            activeEvents = new List<WorldEvent>();

            StreamReader sr = new StreamReader("./Data/WorldEvents.txt");

            stage = 0;
            while (!sr.EndOfStream)
            {
                if (stage == 0)
                {
                    LoadWorldEvents(sr);
                }
                else if (stage == 1)
                {
                    LoadItemMod(sr);
                }
                else if (stage == 2)
                {
                    LoadInv(sr);
                }

                if (sr.ReadLine() == "--------")
                {
                    stage++;
                }
            }
        }

        static public void EventFire(int id, string[] target)
        {
            WorldEvent newWorldEvent = GenerateEvent(id, target);

            if (newWorldEvent.EffectID[0])
            {
                foreach (ItemModifierTemplate newMod in itemModifierList)
                {
                    if (newMod.ID == newWorldEvent.EffectVal[0])
                    {
                        foreach (string cityTarget in newWorldEvent.Target)
                        {
                            foreach (City tempCity in WorldMapMenu.Cities)
                            {
                                if (cityTarget == tempCity.Name)
                                {
                                    for (int i = 0; i < newMod.ItemCategories.Count; i++)
                                    {
                                        ModifierManager.AddModifier(cityTarget, newMod.ItemCategories[i], newMod.ItemModifiers[i]);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            if (newWorldEvent.EffectID[1])
            {
                foreach (InventoryTemplate newInv in inventoryList)
                {
                    if (newInv.ID == newWorldEvent.EffectVal[1])
                    {
                        foreach (string cityTarget in newWorldEvent.Target)
                        {
                            foreach (City tempCity in WorldMapMenu.Cities)
                            {
                                if (cityTarget == tempCity.Name)
                                {
                                    newWorldEvent.OldTemplateInv = new Inventory(tempCity.TemplateInv);
                                    for (int i = 0; i < newInv.AmountNegorPos.Count; i++)
                                    {
                                        if (newInv.AmountNegorPos[i])
                                        {
                                            tempCity.TemplateInv.AddItem(newInv.Items[i]);
                                        }
                                        else
                                        {
                                            tempCity.TemplateInv.ReduceAmountOfItems(newInv.Items[i]);
                                            tempCity.Traded = true;
                                            tempCity.CheckDate();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            activeEvents.Add(newWorldEvent);
        }

        static private WorldEvent GenerateEvent(int id, string[] target)
        {
            WorldEvent newWorldEvent;
            newWorldEvent = new WorldEvent(eventNameList[id], eventDesList[id], id, target, effectIDList[id], effectValList[id], durationList[id]);
            return newWorldEvent;
        }

        static public bool Update()
        {
            counter = Calendar.TotalDays;
            if (counter != oldCounter)
            {
                
                int dif = counter - oldCounter;
                foreach (WorldEvent tempWorldEvent in activeEvents)
                {
                    if (tempWorldEvent.Countdown(dif))
                    {
                        CancelEvent(tempWorldEvent);
                        break;
                    }
                }
            }
            oldCounter = counter;
            if (activeEvents.Count > 0)
            {
                return true;
            }
            return false;
        }

        static private void CancelEvent(WorldEvent worldEvent)
        {
            if (worldEvent.EffectID[0])
            {
                foreach (ItemModifierTemplate newMod in itemModifierList)
                {
                    if (newMod.ID == worldEvent.EffectVal[0])
                    {
                        foreach (string cityTarget in worldEvent.Target)
                        {
                            foreach (City tempCity in WorldMapMenu.Cities)
                            {
                                if (cityTarget == tempCity.Name)
                                {
                                    for (int i = 0; i < newMod.ItemCategories.Count; i++)
                                    {
                                        float tempMod = -(newMod.ItemModifiers[i] * 2);
                                        ModifierManager.SetModifier(cityTarget, newMod.ItemCategories[i], tempMod);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (worldEvent.EffectID[1])
            {
                foreach (InventoryTemplate newInv in inventoryList)
                {
                    if (newInv.ID == worldEvent.EffectVal[1])
                    {
                        foreach (string cityTarget in worldEvent.Target)
                        {
                            foreach (City tempCity in WorldMapMenu.Cities)
                            {
                                if (cityTarget == tempCity.Name)
                                {
                                    tempCity.TemplateInv = worldEvent.OldTemplateInv;
                                    tempCity.Traded = true;
                                    tempCity.CheckDate();
                                }
                            }
                        }
                    }
                }
            }
            activeEvents.Remove(worldEvent);
        }

        static private void LoadWorldEvents(StreamReader sr)
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

        static private void LoadItemMod(StreamReader sr)
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

        static private void LoadInv(StreamReader sr)
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
    }
}
