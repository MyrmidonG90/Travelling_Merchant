using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class Inventory
    {
        int money, stackLimit;
        List<Item> itemList;
        
        public Inventory(int m, List<Item> iL)
        {
            money = m;
            itemList = iL;
            stackLimit = 50;
        }
        public Inventory(int m)
        {
            money = m;
            itemList = new List<Item>();
        }

        public bool AddItem(Item newItem)
        {
            return AddItem(newItem.ID,newItem.Amount);
        }

        public bool AddItem(int itemID, int amount)
        {
            if (itemList.Count <= 24) // Om det finns plats i listan för en ny item
            {
                if (FindIndexOf(itemID) != -1) // Om den redan finns i listan
                {
                    if (itemList[FindIndexOf(itemID)].Amount + amount <= stackLimit ) // Om stacken som item:et är med i har plats
                    {
                        itemList[FindIndexOf(itemID)].Amount += amount; // Ändrar summan av totala antalet av den item:et
                        return true;
                    }
                    else // Om stacken som item:et är med i inte har plats
                    {
                        if (itemList.Count < 24) // Om det finns plats för att göra en ny stack
                        {
                            int tmp = itemList[FindIndexOf(itemID)].Amount + amount - stackLimit;
                            itemList[FindIndexOf(itemID)].Amount += amount - tmp;
                            ItemList.Add(ItemCreator.CreateItem(itemID, tmp));
                            return true;
                        }
                        else // Om det inte finns plats för stacken
                        {
                            return false;
                        }                        
                    }
                }
                else // Om item:et inte redan finns i inventory:n.
                {
                    ItemList.Add(ItemCreator.CreateItem(itemID, amount));
                    return true;
                }
            }
            return false;
            //här ska det läggas till någon prompt att det inte går att lägga till pga plats
        }

        public void ReduceAmountOfItems(Item reduceItem)
        {
            ReduceAmountOfItems(reduceItem.ID,reduceItem.Amount);
        }
        public void ReduceAmountOfItems(int itemID, int amount)
        {
            itemList[FindIndexOf(itemID)].Amount -= amount; // Reducerar antalet man har sålt.
            if (itemList[FindIndexOf(itemID)].Amount == 0) // Har ej tillgång till item:et längre
            {
                itemList.RemoveAt(FindIndexOf(itemID));
            }
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
