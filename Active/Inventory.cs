using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    class Inventory
    {
        private int money;
        private List<Item> itemList;
        public Inventory(int m, List<Item> iL)
        {
            money = m;
            itemList = iL;
        }

        public bool AddItem(Item newItem)
        {
            if (itemList.Count <= 24)
            {
                if (FindIndexOf(newItem.ID) != -1)
                {
                    itemList[FindIndexOf(newItem.ID)].Amount += newItem.Amount;
                }
                else
                {
                    itemList.Add(newItem);
                }
                return true;
            }
            return false;
            //här ska det läggas till någon prompt att det inte går att lägga till pga plats
        }
        
        private int FindIndexOf(int itemID)
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
