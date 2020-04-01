﻿using System;
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
            if (itemList.Count <= 25)
            {
                newItem.ItemID = itemList.Count;
                itemList.Add(newItem);
                return true;
            }
            return false;
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
        }
    }
}