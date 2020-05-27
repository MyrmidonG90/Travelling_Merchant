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
        static List<int> durationModList;
        static List<ItemModifierTemplate> itemModifierList;
        static List<InventoryTemplate> inventoryList;
        static List<WorldEvent> activeEvents;
        static int stage;
        static int counter = 0;
        static int oldCounter = 0;
        static int eventCounter = 0;

        static float chance = 4f;

        static public void Init()
        {
            eventNameList = new List<string>();
            eventDesList = new List<string>();
            effectIDList = new List<bool[]>();
            effectValList = new List<int[]>();
            durationList = new List<int>();
            durationModList = new List<int>();
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

            for (int i = 0; i < eventDesList.Count; i++)
            {
                eventDesList[i] = eventDesList[i].Replace(";", "\n");
            }
        }

        static public void EventFire(int id, string[] target, Random rnd)
        {
            WorldEvent newWorldEvent = GenerateEvent(id, target, rnd);

            if (newWorldEvent.EffectID[0])
            {
                ApplyItemModifier(newWorldEvent);
            }

            if (newWorldEvent.EffectID[1])
            {
                ApplyInventory(newWorldEvent);
            }
            activeEvents.Add(newWorldEvent);
        }

        static private WorldEvent GenerateEvent(int id, string[] target, Random rnd)
        {
            WorldEvent newWorldEvent;
            newWorldEvent = new WorldEvent(eventNameList[id], eventDesList[id], id, eventCounter, target, effectIDList[id], effectValList[id], durationList[id]);
            eventCounter++;
            int mod = rnd.Next(0, durationModList[id] + 1);
            if (rnd.Next(0, 2) == 1)
            {
                newWorldEvent.DaysLeft += mod;
            }
            else
            {
                newWorldEvent.DaysLeft -= mod;
            }
            return newWorldEvent;
        }

        static public bool Update(Random rnd)
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

            CheckNewWorldEvent(rnd);

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
                UnApplyItemModifier(worldEvent);
            }
            if (worldEvent.EffectID[1])
            {
                UnApplyInventory(worldEvent);
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
            durationModList.Add(int.Parse(sr.ReadLine()));
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
                string protoData = sr.ReadLine();
                string[] data = protoData.Split(';');
                int itemID = int.Parse(data[0]);
                int amount = int.Parse(data[1]);
                items.Add(ItemCreator.CreateItem(itemID, amount));
                if (data[2] == "-")
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

        static private void ApplyItemModifier(WorldEvent newWorldEvent)
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

        static private void ApplyInventory(WorldEvent newWorldEvent) // Too long
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
                                Inventory newTemplateInv = new Inventory(tempCity.TemplateInv);
                                for (int i = 0; i < newInv.AmountNegorPos.Count; i++)
                                {
                                    Item fixedItem = newInv.Items[i];
                                    int counter = 0;

                                    for (int j = 0; j < tempCity.TemplateInv.ItemList.Count; j++)
                                    {
                                        if (fixedItem.ID == tempCity.TemplateInv.ItemList[j].ID)
                                        {
                                            counter += tempCity.TemplateInv.ItemList[j].Amount;
                                        }
                                    }

                                    float temp = (fixedItem.Amount / 100f) * counter;

                                    fixedItem.Amount = (int)(temp + 0.5f);

                                    if (fixedItem.Amount > 0)
                                    {
                                        if (newInv.AmountNegorPos[i])
                                        {
                                            newTemplateInv.AddItem(fixedItem);
                                        }
                                        else
                                        {
                                            newTemplateInv.ReduceAmountOfItems(fixedItem);
                                        }
                                    }
                                }
                                int ih = 0;
                                tempCity.TemplateInv = new Inventory(newTemplateInv);
                                tempCity.Traded = true;
                                tempCity.CheckDate();
                            }
                        }
                    }
                }
            }
        }

        static private void UnApplyItemModifier(WorldEvent worldEvent)
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

        static private void UnApplyInventory(WorldEvent worldEvent)
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

        static private void CheckNewWorldEvent(Random rnd)
        {
            if (rnd.Next(1, 101) <= chance && counter != oldCounter)
            {
                int id = rnd.Next(0, eventDesList.Count);

                int number = rnd.Next(1, WorldMapMenu.Cities.Length);
                int[] protoTargets = new int[number];

                for (int i = 0; i < number; i++)
                {
                    protoTargets[i] = rnd.Next(0, WorldMapMenu.Cities.Length);
                }

                for (int i = 1; i < number; i++)
                {
                    if (protoTargets[i - 1] == protoTargets[i] && protoTargets[i] != WorldMapMenu.Cities.Length - 1)
                    {
                        protoTargets[i]++;
                    }
                    else if (protoTargets[i - 1] == protoTargets[i])
                    {
                        protoTargets[i]--;
                    }
                }

                List<string> targets = new List<string>();
                for (int i = 0; i < protoTargets.Length; i++)
                {
                    targets.Add(WorldMapMenu.Cities[protoTargets[i]].Name);
                }

                targets = targets.Distinct().ToList();

                CheckNotNeighbors(targets); //doesnt work as intended men har inte tid eller ork att fixa atm /my

                EventFire(id, targets.ToArray(), rnd);
            }
        }

        static private void CheckNotNeighbors(List<string> targets) // Too long
        {
            bool exit = false;
            bool exitCheck = true;
            while (exit)
            {
                exitCheck = true;
                foreach (string tempTarget in targets)
                {
                    bool check = false;
                    foreach (City tempCity in WorldMapMenu.Cities)
                    {
                        if (tempCity.Name == tempTarget)
                        {
                            for (int i = 0; i < 14; i++)
                            {
                                if (TravelMenu.RouteNames[i, 0] == tempCity.Name)
                                {
                                    if (TravelMenu.RouteNames[i, 1] == tempTarget)
                                    {
                                        check = true;
                                        exitCheck = false;
                                    }
                                }
                            }

                            for (int i = 0; i < 14; i++)
                            {
                                if (TravelMenu.RouteNames[i, 0] == tempTarget)
                                {
                                    if (TravelMenu.RouteNames[i, 1] == tempCity.Name)
                                    {
                                        check = true;
                                        exitCheck = false;
                                    }
                                }
                            }

                            if (!check)
                            {
                                targets.Remove(tempTarget);
                                exitCheck = false;
                                break;
                            }
                        }
                    }
                    if (check)
                    {
                        exitCheck = false;
                        break;
                    }
                }
                if (exitCheck)
                {
                    exit = true;
                }
            }
        }

        static public List<string> EventDesList
        {
            get => eventDesList;           
        }

        static public List<WorldEvent> ActiveEvents
        {
            get => activeEvents;
        }
    }
}
