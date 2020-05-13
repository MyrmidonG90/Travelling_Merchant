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
        int surrogateKey;
        string surrogateKeyName;
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
            int index = FindIndexOf(itemID);
            int amountOfStacks = HowManyStacks(amount);            

            if (itemList.Count + amountOfStacks < invLimit)// The amount of stacks fit inside of the itemList
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
                StacksDoNotAddUp(amountOfStacks,index,itemID,amount);
            }
            return working;
        }
        void StacksDoNotAddUp(int amountOfStacks, int index, int itemID, int amount)
        {
            if (itemList.Count + amountOfStacks < invLimit + 1) // Does not exceed itemList+1
            {
                if (index != -1) // There are other stacks with same Id
                {
                    if (FindStackNotFull(itemID) != -1) // There are stacks not filled
                    {
                        FillStacks(itemID, ref amount);
                        AddStacks(itemID, amount);                        
                    }
                    else
                    {
                        AddStacks(itemID, amount);                        
                    }
                }
                else if (itemList.Count < invLimit)
                {
                    AddStacks(itemID, amount);                    
                }
            }
            else if (itemList.Count + amountOfStacks == invLimit+1)
            {
                if (index != -1) // There are other stacks with same Id
                {
                    if (FindStackNotFull(itemID) != -1) // There are stacks not filled
                    {
                        int amountToFill = stackLimit - itemList[FindStackNotFull(itemID)].Amount;
                        if (amount <= amountToFill)
                        {
                            FillStacks(itemID, ref amount);
                            // Nu bör amountOfStacks vara 25 istället för 25
                            AddStacks(itemID,amount);
                        }
                    }
                }
            }
        }
        void AddStacks(int itemID, int amount)
        {            
            if (amount > 0) // Egentligen onödig men ökar maintainability
            {
                int amountOfStacks = HowManyStacks(amount);
                while (amountOfStacks > 0)
                {
                    int tmp = stackLimit - amount;
                    if (tmp > 0)
                    {
                        itemList.Add(ItemCreator.CreateItem(itemID, amount));
                        
                        --amountOfStacks;
                    }
                    else if (tmp == 0)
                    {
                        itemList.Add(ItemCreator.CreateItem(itemID, stackLimit));
                        --amountOfStacks;
                    }
                    else
                    {
                        itemList.Add(ItemCreator.CreateItem(itemID, stackLimit));
                        amount -= 50;
                        --amountOfStacks;
                    }
                }
            }            
        }

        int HowManyStacks(int amount)
        {
            double amountOfStacks = (double)amount / stackLimit;

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
                    amountToFill = stackLimit - itemList[FindStackNotFull(itemID)].Amount;
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

        public int SurrogateKey { get => surrogateKey; set => surrogateKey = value; }
        public string SurrogateKeyName { get => surrogateKeyName; set => surrogateKeyName = value; }
    }
}
/*
            if (itemList.Count < invLimit) // itemlist not full
            {
                if (index != -1) // There are other stacks with the same ID
                {
                    if (itemList.Count < invLimit) // Om den får plats
                    {
                        if (itemList[index].Amount + amount <= stackLimit) // Om stacken som item:et är med i har plats
                        {
                            itemList[index].Amount += amount; // Ändrar summan av totala antalet av den item:et
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
                                        amount = itemList[FindStackNotFull(itemID)].Amount + amount - 50;
                                        itemList[FindStackNotFull(itemID)].Amount += amount;
                                        ItemList.Add(ItemCreator.CreateItem(itemID, amount));
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
                }
                else // There are no other stacks with the same ID
                {
                    if (itemList.Count < invLimit) //
                    {
                        ItemList.Add(ItemCreator.CreateItem(itemID, amount));
                        working = true;
                    }
                }
            }
            else // Iitemlist full
            {
                if (index != -1) // There are other stacks with the same ID
                {

                }
            }*/
/*if (itemList.Count + amountOfStacks < invLimit+1) // Does not exceed itemList+1
    {
        if (index != -1) // There are other stacks with same Id
        {
            if (FindStackNotFull(itemID)!= -1) // There are stacks not filled
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
        else if (itemList.Count < 25)
        {
            AddStacks(itemID, amount);
            working = true;
        }
    }
    else if (itemList.Count + amountOfStacks == 26) 
    {
        if (index != -1) // There are other stacks with same Id
        {
            if (FindStackNotFull(itemID) != -1) // There are stacks not filled
            {
                int amountToFill = 50 - itemList[FindStackNotFull(itemID)].Amount;
                if (amount <= amountToFill)
                {
                    FillStacks(itemID, ref amount);
                    working = true;
                }                            
            }
        }
    }*/
