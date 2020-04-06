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
        int counterCol, counterRow,tmpCounter,leftPrice,rightPrice,priceDifference;

        // Oklart
        void Initialize(Inventory left, Inventory right)
        {
            this.invLeft = left;
            this.invRight = right;
        }

        // Behöver lite arbete till
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
                    slotsLeft[i, j] = new Slot(50+60*j,100+60*i,50,50);
                    slotsRight[i, j] = new Slot(1570+60*j,100+60*i,50,50);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // Placera ut dem på rätt plats
                    tradeSlotsLeft[i,j] = new Slot(1010+60*j,100+60*i,50,50);
                    tradeSlotsRight[i, j] = new Slot(660+60*j,100+60*i,50,50);
                }
            }
        }

        // Lite arbete kvar. Justera värderna
        void CreateButtons()
        {
            
            accept = new Button(5, 5, 100, 100, TextureManager.texBox);
            reset = new Button(5, 5, 100, 100, TextureManager.texBox);
            back = new Button(5, 5, 100, 100, TextureManager.texBox);
        }
        
        // Håller processen trading igång. När den returnerar false betyder det att transaktionen är ej över och vice versa när man returnerar true
        // Behöver mycket arbete!!!
        bool Update(ref Inventory participantLeft, ref Inventory participantRight)
        {
            //När ett mussklick händer
            if (KMReader.MouseClick())
            {
                // Om knappen accept klickas
                if (accept.Click())
                {
                    if (AcceptTrade(ref participantLeft, ref participantRight))
                    {
                        return true;
                    }
                }
                // Om knappen reset klickas
                else if (reset.Click())
                {
                    ResetTrade();
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
            UpdatePrices();
            return false;
        }

        //Lite till
        bool CheckInvClick()
        {
            counterCol = 0;
            counterRow = 0;
            // Tittar om vänstra inventory:n har blivit klickat
            ////////////////////////////////
            do
            {
                do
                {
                    if (slotsLeft[counterCol, counterRow].Clicked() == false)
                    {                        
                        ++counterRow;
                    }
                    
                } while (counterRow < 5 && slotsLeft[counterCol, counterRow].Clicked() == false);

                if (slotsLeft[counterCol, counterRow].Clicked() == false)
                {
                    
                    ++counterCol;
                }
                
            } while (counterCol < 5 && slotsLeft[counterCol, counterRow].Clicked() == false);

            if (slotsLeft[counterCol, counterRow].Clicked() == true) // Om det vänstra inventory:n har blivit klickat
            {
                tradeLeft.AddItem(slotsLeft[counterCol,counterRow].Item);// Lägger till item till det vänstra trade fältet   
                // Måste ta bort från inventory;n också!!!
                UpdateSlots();
                return true;
            }
            ////////////////////////////////
            counterCol = 0;
            counterRow = 0;
            ////////////////////////////////
            do
            {
                do
                {
                    if (slotsRight[counterCol, counterRow].Clicked() == false)
                    {
                        ++counterRow;
                    }

                } while (counterRow < 5 && slotsRight[counterCol, counterRow].Clicked() == false);

                if (slotsRight[counterCol, counterRow].Clicked() == false)
                {

                    ++counterCol;
                }
            } while (counterCol < 5 && slotsRight[counterCol, counterRow].Clicked() == false);

            if (slotsRight[counterCol, counterRow].Clicked() == true) // Om det högra inventory:n har blivit klickat
            {
                tradeRight.AddItem(slotsLeft[counterCol, counterRow].Item); // Lägger till item till det högra trade fältet
                // Måste ta bort från inventory;n också!!!
                UpdateSlots();
                return true;
            }
            ////////////////////////////////

            /* while (counterCol < 5 && slotsLeft[counterCol, counterRow].Clicked() == false)
             {
                 while (counterRow < 5 && slotsLeft[counterCol, counterRow].Clicked())
                 {
                     ++counterRow;
                 }
                 if (slotsLeft[counterCol, counterRow].Clicked())
                 {
                     //Add item to the trade inventory on the left
                     //AddItem(Participant.Left,/*itemId,*/ /*counterRow, counterCol);
                     return true;
                 }
                 ++counterCol;
             }*/


            // Tittar om högra inventory:n har blivit klickat
            /* while (counterCol < 5 && slotsLeft[counterCol, counterRow].Clicked())
             {
                 while (counterRow < 5 && slotsLeft[counterCol, counterRow].Clicked())
                 {
                     ++counterRow;
                 }
                 if (slotsLeft[counterCol, counterRow].Clicked())
                 {
                     // Add item to the trade inventory on the right
                     AddItem(Participant.Right,/*itemId,*/ /*counterRow, counterCol);
                     return true;
                 }
                 ++counterCol;
             }*/

            return false;
        }

        // Klar
        //Uppdaterar Slots
        void UpdateSlots()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    try
                    {
                        tradeSlotsLeft[i, j].Item = tradeLeft.ItemList[i * 3 + j];
                    }
                    catch (Exception)
                    {

                        tradeSlotsLeft[i, j].Item = null;
                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    try
                    {
                        tradeSlotsRight[i, j].Item = tradeRight.ItemList[i * 3 + j];
                    }
                    catch (Exception) // Ifall det finns tomma rutor
                    {

                        tradeSlotsRight[i, j].Item = null;
                    }
                    
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    try
                    {
                        slotsLeft[i, j].Item = invRight.ItemList[i * 5 + j];
                    }
                    catch (Exception)
                    {
                        slotsLeft[i, j].Item = null;
                    }
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    try
                    {
                        slotsRight[i, j].Item = invRight.ItemList[i * 5 + j];
                    }
                    catch (Exception)
                    {
                        slotsRight[i, j].Item = null;
                    }
                    
                }
            }
        }

        void UpdateInventory()
        {
            for (int i = 0; i < 5; i++)
            {

            }
        }
        //Medium Arbete Kvar!!
        bool AcceptTrade(ref Inventory participantLeft, ref Inventory participantRight)
        {
            //If nothing is presented
            if (tradeLeft.ItemList.Count == 0 && tradeRight.ItemList.Count==0)
            {
                return false;
            }
            //If player and Merchant doesn't enough space in inventory Advance.
            //Else prompt that the player doesn't have enough space
            if (invLeft.ItemList.Count < 24 && invRight.ItemList.Count < 24)
            {
                return false;
            }
            
            //If player doesn't have enough coin and merchant neither.
            //Else prompt that you do not have enough gold
            if (priceDifference != 0)
            {
                return false;
            }

            // Update both player and merchant's inventory            
            participantLeft = invLeft;
            participantRight = invRight;
            return true;
        }

        // Klar men Behöver ändra BasePrice till något mer verkligt
        int CheckValue(Inventory inv)
        {
            int sum = 0;
            for (int i = 0; i < inv.ItemList.Count; i++)
            {
                sum += inv.ItemList[i].BasePrice * inv.ItemList[i].Amount;
            }
            return sum;
        }        

        //Klar
        void UpdatePrices()
        {
            leftPrice = CheckValue(tradeLeft);
            rightPrice = CheckValue(tradeRight);
            priceDifference = leftPrice - rightPrice;
        }

        // Gammal
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

        //Klar
        void ConstructSlots(Inventory inventory)
        {
            // Konstrurerar inventory slots
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    try
                    {
                        slotsLeft[j, i].Item = invLeft.ItemList[i * 5 + j];                        
                    }
                    catch (Exception)
                    {
                        slotsLeft[j, i].Item = null;
                    }
                    try
                    {
                        slotsRight[j, i].Item = invRight.ItemList[i * 5 + j];
                    }
                    catch (Exception)
                    {
                        slotsRight[j, i].Item = null;
                    }
                    
                }
            }

            // Konstruerar trade rutorna
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tradeSlotsLeft[j, i].Item = null;
                    tradeSlotsRight[j, i].Item = null;
                }
            }
        }

            // Klar
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
            foreach (var item in slotsLeft)
            {
                item.Reset();
            }
            foreach (var item in slotsRight)
            {
                item.Reset();
            }
            invLeft = null;
            invRight = null;
            tradeLeft = null;
            tradeRight = null;
        }

        // Oklart
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
