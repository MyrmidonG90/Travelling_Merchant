using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class Inventory
    {
        int money, stackLimit, invLimit;
        List<Item> itemList;

        public Inventory(int m, List<Item> iL)
        {
            money = m;
            itemList = iL;
            Initialize();
        }

        public Inventory(int m)
        {
            money = m;
            itemList = new List<Item>();
            Initialize();
        }

        //skum jag vet men fixade problem med referenser som pekar på samma objekt i minnet
        public Inventory(Inventory inv)
        {
            money = inv.Money;

            itemList = new List<Item>();
            foreach (Item item in inv.ItemList)
            {
                itemList.Add(ItemCreator.CreateItem(item.ID, item.Amount));
            }

            Initialize();
        }

        void Initialize()
        {
            stackLimit = 50;
            invLimit = 25;
        }

        public bool AddItem(Item newItem)
        {
            return AddItem(newItem.ID, newItem.Amount);
        }

        public bool AddItem(int itemID, int amount)
        {
            bool working = false;
            if (itemList.Count <= invLimit) // Om det finns plats i listan för en ny item
            {
                if (FindIndexOf(itemID) != -1) // Om den redan finns i listan
                {
                    if (itemList[FindIndexOf(itemID)].Amount + amount <= stackLimit) // Om stacken som item:et är med i har plats
                    {
                        itemList[FindIndexOf(itemID)].Amount += amount; // Ändrar summan av totala antalet av den item:et
                        working = true;
                    }
                    else // Om stacken som item:et är med i inte har plats
                    {
                        if (itemList.Count < invLimit) // Om det finns plats för att göra en ny stack
                        {
                            if (FindStackNotFull(itemID) != -1)
                            {
                                if (itemList[FindStackNotFull(itemID)].Amount + amount <= stackLimit)
                                {
                                    itemList[FindStackNotFull(itemID)].Amount += amount; // Ändrar summan av totala antalet av den item:et
                                    working = true;
                                }
                                else
                                {
                                    int tmp = itemList[FindStackNotFull(itemID)].Amount + amount - stackLimit;
                                    itemList[FindStackNotFull(itemID)].Amount += amount - tmp;
                                    ItemList.Add(ItemCreator.CreateItem(itemID, tmp));
                                    working = true;
                                }
                            }
                            else
                            {
                                ItemList.Add(ItemCreator.CreateItem(itemID, amount));
                                working = true;
                            }
                        }
                        else
                        {
                            if (FindStackNotFull(itemID) != -1)
                            {
                                if (itemList[FindStackNotFull(itemID)].Amount + amount <= stackLimit) // Om stacken som item:et är med i har plats
                                {
                                    itemList[FindStackNotFull(itemID)].Amount += amount; // Ändrar summan av totala antalet av den item:et
                                    working = true;
                                }
                            }
                        }
                    }
                }
                else // Om item:et inte redan finns i inventory:n.
                {
                    if (itemList.Count < invLimit)
                    {
                        ItemList.Add(ItemCreator.CreateItem(itemID, amount));
                        working = true;
                    }
                }
            }
            return working;
            //här ska det läggas till någon prompt att det inte går att lägga till pga plats
        }

        public void ReduceAmountOfItems(Item reduceItem)
        {
            ReduceAmountOfItems(reduceItem.ID, reduceItem.Amount);
        }

        public void ReduceAmountOfItems(int itemID, int amount)
        {
            try
            {
                int amountLeft = itemList[FindIndexOf(itemID)].Amount - amount;
                if (amountLeft > 0) // Tar bort antalet varor från stacken
                {
                    itemList[FindIndexOf(itemID)].Amount -= amount;

                }
                else if (amountLeft == 0) // Om det tar bort exakt lika mycket som finns av den stacken
                {
                    itemList.RemoveAt(FindIndexOf(itemID));
                }
                else if (amountLeft < 0)
                {
                    amountLeft *= -1;
                    ReduceAmountOfItems(itemID, amountLeft);
                }

                /*
                int amountLeft = itemList[FindIndexOf(itemID)].Amount - amount;
                //itemList[FindIndexOf(itemID)].Amount -= amount; // Reducerar antalet man har sålt.
                if (amountLeft > 0) // Tar bort antalet varor från stacken
                {
                    itemList[FindIndexOf(itemID)].Amount -= amount;
                }
                else if (amountLeft == 0) // Om det tar bort exakt lika mycket som finns av den stacken
                {
                    itemList.RemoveAt(FindIndexOf(itemID));
                }
                else if (amountLeft < 0) // 
                {
                    int tmp;
                    amountLeft *= -1;
                    itemList.RemoveAt(FindIndexOf(itemID));
                    while (amountLeft != 0)
                    {
                        tmp = itemList[FindIndexOf(itemID)].Amount -= amountLeft;
                        if (tmp > 0)
                        {
                            itemList[FindIndexOf(itemID)].Amount -= amount;
                        }
                        else if (tmp == 0)
                        {
                            itemList.RemoveAt(FindIndexOf(itemID));
                        }
                        else if (tmp < 0)
                        {
                        }
                    }
                }*/
            }
            catch { }
        }

        int FindIndexOf(int itemID)
        {
            bool found = false;
            int tmpCounter = 0;
            int answer = -1;

            while (found == false && tmpCounter < itemList.Count)
            {
                if (itemList[tmpCounter].ID == itemID)
                {
                    found = true;
                    answer = tmpCounter;
                }
                else
                {
                    ++tmpCounter;
                }
            }
            return answer;
        }

        int FindStackNotFull(int itemID)
        {
            bool found = false;
            int tmpCounter = 0;
            int answer = -1;
            while (found == false && tmpCounter < itemList.Count)
            {
                if (itemList[tmpCounter].ID == itemID)
                {
                    if (itemList[tmpCounter].Amount != stackLimit)
                    {
                        found = true;
                        answer = tmpCounter;
                    }
                    else
                    {
                        ++tmpCounter;
                    }
                }
                else
                {
                    ++tmpCounter;
                }
            }
            return answer;
        }

        public int Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
            }
        }

        public List<Item> ItemList
        {
            get
            {
                return itemList;
            }
            set
            {
                itemList = value;
            }
        }
    }
}