using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class Inventory
    {
        int money;
        List<Item> itemList;

        public Inventory(int m, List<Item> iL)
        {
            money = m;
            itemList = iL;
        }
        public Inventory(int m)
        {
            money = m;
            itemList = new List<Item>();
        }

        public bool AddItem(Item newItem)
        {
            if (FindIndexOf(newItem.ID) != -1) // Om den redan finns i listan
            {
                itemList[FindIndexOf(newItem.ID)].Amount += newItem.Amount; // Ändrar summan av totala antalet av den item:et
                return true;
            }
            else
            {
                if (itemList.Count <= 24) // Om det finns plats i listan för en ny item
                {
                    itemList.Add(newItem);
                    return true;
                }
            }
            
            return false;
            //här ska det läggas till någon prompt att det inte går att lägga till pga plats
        }
        public bool AddItem(int itemID, int amount)
        {
            if (FindIndexOf(itemID) != -1) // Om den redan finns i listan
            {
                itemList[FindIndexOf(itemID)].Amount += amount; // Ändrar summan av totala antalet av den item:et
                return true;
            }
            else
            {
                if (itemList.Count <= 24) // Om det finns plats i listan för en ny item
                {
                    ItemList.Add(ItemCreator.CreateItem(itemID,amount));                    
                    return true;
                }
            }

            return false;
            //här ska det läggas till någon prompt att det inte går att lägga till pga plats
        }

        public void ReduceAmountOfItems(Item reduceItem)
        {
            itemList[FindIndexOf(reduceItem.ID)].Amount -= reduceItem.Amount; // Reducerar antalet man har sålt.
            if (itemList[FindIndexOf(reduceItem.ID)].Amount == 0) // Har ej tillgång till item:et längre
            {
                itemList.RemoveAt(FindIndexOf(reduceItem.ID));
            }
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
