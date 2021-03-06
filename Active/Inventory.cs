﻿using System;
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
        int surrogateKey; // används inte
        string surrogateKeyName; // Andvänds inte
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

        public Inventory(int money,bool trading)
        {
            this.money = money;
            itemList = new List<Item>();
            stackLimit = 20;
            invLimit = 9;
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
            stackLimit = 20;
            invLimit = 25;
        }        

        public bool AddItem(Item newItem)
        {
            return AddItem(newItem.ID, newItem.Amount);
        }

        public bool AddItem(int itemID, int amount)
        {
            bool working = false;
            int index = FindIndexOf(itemID);
            int amountOfStacks = HowManyStacks(amount);            

            if (itemList.Count + amountOfStacks < InvLimit)// The amount of stacks fit inside of the itemList
            {
                if (index != -1) // // There are other stacks with same Id
                {
                    if (FindStackNotFull(itemID) != -1) // There are stacks that are not full
                    {
                        FillStacks(itemID, ref amount);
                        AddStacks(itemID, amount);
                        working = true;
                    }
                    else
                    {
                        AddStacks(itemID, amount);
                        working = true;
                    }
                }
                else // Creates enough stacks so that amount == 0
                {
                    AddStacks(itemID,amount);
                    working = true;
                }
            }
            else // The amount of stacks does not fit inside of the itemList
            {
                working = StacksDoNotAddUp(amountOfStacks,index,itemID,amount);
            }
            return working;
        }
        bool StacksDoNotAddUp(int amountOfStacks, int index, int itemID, int amount) // Two rows too long
        {
            bool answer = false;
            if (itemList.Count + amountOfStacks < InvLimit + 1) // Does not exceed itemList+1
            {
                if (index != -1) // There are other stacks with same Id
                {
                    if (FindStackNotFull(itemID) != -1) // There are stacks not filled
                    {
                        FillStacks(itemID, ref amount);
                        AddStacks(itemID, amount);
                        answer = true;
                    }
                    else
                    {
                        AddStacks(itemID, amount);
                        answer = true;
                    }
                }
                else if (itemList.Count < InvLimit)
                {
                    AddStacks(itemID, amount);
                    answer = true;
                }
            }
            else if (itemList.Count + amountOfStacks == InvLimit+1)
            {
                if (index != -1) // There are other stacks with same Id
                {
                    if (FindStackNotFull(itemID) != -1) // There are stacks not filled
                    {
                        int amountToFill = StackLimit - itemList[FindStackNotFull(itemID)].Amount;
                        if (amount <= amountToFill)
                        {
                            FillStacks(itemID, ref amount);
                            // Nu bör amountOfStacks vara 25 istället för 25
                            AddStacks(itemID,amount);
                            answer = true;
                        }
                    }
                }
            }
            return answer;
        }
        void AddStacks(int itemID, int amount)
        {            
            if (amount > 0) // Egentligen onödig men ökar maintainability
            {
                int amountOfStacks = HowManyStacks(amount);
                while (amountOfStacks > 0)
                {
                    int tmp = StackLimit - amount;
                    if (tmp > 0)
                    {
                        itemList.Add(ItemCreator.CreateItem(itemID, amount));
                        
                        --amountOfStacks;
                    }
                    else if (tmp == 0)
                    {
                        itemList.Add(ItemCreator.CreateItem(itemID, StackLimit));
                        --amountOfStacks;
                    }
                    else
                    {
                        itemList.Add(ItemCreator.CreateItem(itemID, StackLimit));
                        amount -= 50;
                        --amountOfStacks;
                    }
                }
            }            
        }

        int HowManyStacks(int amount)
        {
            double amountOfStacks = (double)amount / StackLimit;

            if ((amountOfStacks % 1.0) > 0) // Calculate how many stacks it will create
            {
                amountOfStacks = (int)(amountOfStacks + 1);
            }
            return (int)amountOfStacks;
        }

        bool FillStacks(int itemID, ref int amount) // Fill Stacks until they're full or amount == 0
        {
            bool answer = false;
            int amountToFill;
            if (FindStackNotFull(itemID) == -1)
            {
                answer = true;
            }
            else
            {
                while (FindStackNotFull(itemID) != -1 && amount != 0)
                {
                    amountToFill = StackLimit - itemList[FindStackNotFull(itemID)].Amount;
                    if (amount - amountToFill >= 0)
                    {
                        amount -= amountToFill;
                        itemList[FindStackNotFull(itemID)].Amount += amountToFill;
                    }
                    else
                    {
                        itemList[FindStackNotFull(itemID)].Amount += amount;
                        amount -= amount;
                    }
                }
            }
            return answer;
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
                    itemList.RemoveAt(FindIndexOf(itemID));
                    amountLeft *= -1;
                    ReduceAmountOfItems(itemID, amountLeft);
                }
            }
            catch { }
        }

        public int FindIndexOf(int itemID)
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
                    if (itemList[tmpCounter].Amount != StackLimit)
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
                if (money<0)
                {
                    money = 0;
                }
                
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

        public int SurrogateKey { get => surrogateKey; set => surrogateKey = value; } // Används inte
        public string SurrogateKeyName { get => surrogateKeyName; set => surrogateKeyName = value; } // Används inte
        public int StackLimit { get => stackLimit;}
        public int InvLimit { get => invLimit;}
    }
}

