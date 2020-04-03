using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Active
{
    class Trading
    {
        /*
         Enitity entLeft,entRight;*/
         Inventory invLeft, invRight,tradeLeft,tradeRight;

             
             
        Slot[,] slotsLeft, slotsRight,tradeSlotsLeft,tradeSlotsRight;
        
        Button accept, reset,back;
        enum Participant
        {
            Left,
            Right,
            None
        }
        int counterCol, counterRow,tmpCounter,leftPrice,rightPrice,totalPrice;


        void Initialize(Inventory left, Inventory right)
        {

            this.invLeft = left;
            this.invRight = right;
            
        }
        void CreateSlots()
        {
            slotsLeft = new Slot[5, 5];
            slotsRight = new Slot[5, 5];
            tradeSlotsLeft = new Slot[3, 3];
            tradeSlotsRight = new Slot[3, 3];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    // Placera ut dem på rätt plats
                    //slotsLeft[i, j] = new Slot();
                    //slotsRight[i, j] = new Slot();
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Placera ut dem på rätt plats
                    //tradeSlotsLeft[i,j] = new Slot();
                    //tradeSlotsRight[i, j] = new Slot();
                }
            }
        }
        void CreateButtons()
        {
            
            accept = new Button(5, 5, 100, 100, TextureManager.texBox);
            reset = new Button(5, 5, 100, 100, TextureManager.texBox);
            back = new Button(5, 5, 100, 100, TextureManager.texBox);
        }
        
        // Håller processen trading igång. När den returnerar false betyder det att transaktionen är ej över och vice versa när man returnerar true
        bool Update()
        {
            //När ett mussklick händer
            if (KMReader.MouseClick())
            {
                // Om knappen accept klickas
                if (accept.Click())
                {
                    if (AcceptTrade())
                    {
                        return true;
                    }
                    else
                    {

                    }
                    
                }
                // Om knappen reset klickas
                else if (reset.Click())
                {
                    
                }
                // Om knappen back klickas
                else if (back.Click())
                {
                    return true;
                }
                // Checkar om man har klickat på inventory:n
                else
                {
                    CheckInvClick();
                }
            }
            return false;
        }


        bool CheckInvClick()
        {
            counterCol = 0;
            counterRow = 0;
            // Tittar om vänstra inventory:n har blivit klickat
            while (counterCol < 5 && slotsLeft[counterCol, counterRow].Clicked())
            {
                while (counterRow < 5 && slotsLeft[counterCol, counterRow].Clicked())
                {
                    ++counterRow;
                }
                if (slotsLeft[counterCol, counterRow].Clicked())
                {
                    //Add item to the trade inventory on the left
                    AddItem(Participant.Left,/*itemId,*/ counterRow, counterCol);
                    return true;
                }
                ++counterCol;
            }

            counterCol = 0;
            counterRow = 0;
            // Tittar om högra inventory:n har blivit klickat
            while (counterCol < 5 && slotsLeft[counterCol, counterRow].Clicked())
            {
                while (counterRow < 5 && slotsLeft[counterCol, counterRow].Clicked())
                {
                    ++counterRow;
                }
                if (slotsLeft[counterCol, counterRow].Clicked())
                {
                    // Add item to the trade inventory on the right
                    AddItem(Participant.Right,/*itemId,*/ counterRow,counterCol);
                    return true;
                }
                ++counterCol;
            }

            return false;
        }
        void AddItem(Participant participant, /*, int itemId*/int posRow, int posCol)
        {
            bool finding = false;
            counterCol = 0;
            counterRow = 0;
            if (participant == Participant.Left)
            {
                while (finding == false && counterCol < 3)
                {
                    while (counterRow < 3)
                    {
                        // Jämnför itemId som ska läggas till med itemId i denna Slot.
                        if (true/*itemId == invLeft[].itemId*/)
                        {
                            finding = true;
                        }
                        ++counterRow;
                    }
                    if (finding == false)
                    {
                        ++counterCol;
                    }
                }
            }
            else if (participant == Participant.Right)
            {

            }
            
            // Search if it's already inside the trade 
            //      If inside add another
            //      If not Check if there's a free slot
            //              If there is add that item to that free slot
            //              If not, Nothing happens

            
        }
        bool AcceptTrade(ref Inventory invLeft, ref Inventory invRight)
        {
            //If nothing is presented
            if (tradeLeft.ItemList.Count == 0 && tradeRight.ItemList.Count==0)
            {
                return false;
            }
            //If player and Merchant doesn't enough space in inventory Advance.
            //Else prompt that the player doesn't have enough space
            if (false)
            {
                return false;
            }
            
            //If player doesn't have enough coin and merchant neither.
            //Else prompt that you do not have enough gold
            if (false)
            {
                return false;
            }
            
            // Update both player and merchant's inventory
            UpdateInventories(/*enitityLeft,entityRight*/);
            // Return true;
            return true;
        }

        int CheckValue(Inventory inv)
        {
            
            return 0;
        }
        void UpdateInventories(/*ref entityLeft, ref entityRight*/)
        {            
            //entityLeft.inventory = invLeft;
            // enittyRight.inventory = invRight;
        }

        
        void ConstructInventory(Participant participant, Inventory inventory)
        {
            if (participant == Participant.Left)
            {
                if (inventory.ItemList.Count <= 25)
                {
                    tmpCounter = 0;
                    counterCol = 0;
                    counterRow = 0;

                    while (tmpCounter <= inventory.ItemList.Count && counterCol < 5 )
                    {
                        while (tmpCounter <= inventory.ItemList.Count && counterRow < 5)
                        {
                            slotsLeft[counterCol, counterRow].AddItem(inventory.ItemList[tmpCounter],inventory.ItemList[tmpCounter].Amount);
                            ++tmpCounter;
                            ++counterRow;
                        }
                        if (tmpCounter <= inventory.ItemList.Count)
                        {
                            ++counterCol;
                        }                        
                    }
                }
                else
                {
                    while (tmpCounter < inventory.ItemList.Count)
                    {

                    }
                }
            }
            else
            {
                if (inventory.ItemList.Count <= 25)
                {
                    tmpCounter = 0;
                    counterCol = 0;
                    counterRow = 0;

                    while (tmpCounter <= inventory.ItemList.Count && counterCol < 5)
                    {
                        while (tmpCounter <= inventory.ItemList.Count && counterRow < 5)
                        {
                            slotsRight[counterCol, counterRow].AddItem(inventory.ItemList[tmpCounter], inventory.ItemList[tmpCounter].Amount);
                            ++tmpCounter;
                            ++counterRow;
                        }
                        if (tmpCounter <= inventory.ItemList.Count)
                        {
                            ++counterCol;
                        }
                    }
                }
                else
                {
                    while (tmpCounter < inventory.ItemList.Count)
                    {

                    }
                }
            }
        }
        // Cleans slots of items 
        void ResetTrade()
        {
            foreach (var item in tradeSlotsLeft)
            {
                item.Reset();
            }
            foreach (var item in tradeSlotsRight)
            {
                item.Reset();
            }
            //invLeft = entity.inventory
            //invRight = entity.inventory

        }
        void Draw(SpriteBatch sb)
        {
            foreach (var item in slotsLeft)
            {
                item.Draw(sb);
            }
            foreach (var item in slotsRight)
            {
                item.Draw(sb);
            }
            foreach (var item in tradeSlotsLeft)
            {
                item.Draw(sb);
            }
            foreach (var item in tradeSlotsRight)
            {
                item.Draw(sb);
            }
            accept.Draw(sb);
            reset.Draw(sb);
            back.Draw(sb);
        }
    }     
}
