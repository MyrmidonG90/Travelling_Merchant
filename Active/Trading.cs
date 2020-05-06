using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Active
{
    static class Trading
    {

        static Inventory invLeft, invRight, tradeLeft, tradeRight, origLeftInv, origRightInv, tmpInv;
        static Slot[,] slotsLeft, slotsRight, tradeSlotsLeft, tradeSlotsRight;
        static Button accept, reset, back;
        static string zoneName;
        enum Participant
        {
            Left,
            Right,
            TradeSlotsLeft,
            TradeSlotsRight,
            None
        }

        static int counterCol, counterRow, leftPrice, rightPrice, priceDifference;

        // Klart
        static public void StartTrade(Inventory player, Inventory trader, string zone) 
        {
            invLeft = player;
            invRight = trader;

            tradeLeft = new Inventory(player.Money);
            tradeRight = new Inventory(trader.Money);

            priceDifference = 0;
            origLeftInv = player;
            origRightInv = trader;
            CreateSlots();
            CreateButtons();
            
            zoneName = zone;
        }

        // Klar
        static void CreateSlots() // Skapar slots
        {
            slotsLeft = new Slot[5, 5];
            slotsRight = new Slot[5, 5];
            tradeSlotsLeft = new Slot[3, 3];
            tradeSlotsRight = new Slot[3, 3];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    slotsLeft[i, j] = new Slot(245 + 60 * j, 200 + 60 * i, 50, 50);
                    slotsRight[i, j] = new Slot(1385 + 60 * j, 200 + 60 * i, 50, 50);
                    try
                    {
                        slotsLeft[i, j].AddItem(invLeft.ItemList[i * 5 + j]);
                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        slotsRight[i, j].AddItem(invRight.ItemList[i * 5 + j]);
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tradeSlotsRight[i, j] = new Slot(1045 + 60 * j, 320 + 60 * i, 50, 50);
                    tradeSlotsLeft[i, j] = new Slot(705 + 60 * j, 320 + 60 * i, 50, 50);
                }
            }
        }

        // Klar
        static void CreateButtons() // Initialiserar knapparna
        {
            accept = new Button(830, 600, 260, 120, "accept", "Accept Trade", TextureManager.texBox);
            reset = new Button(830, 740, 260, 120, "reset", "Reset Trade", TextureManager.texBox);
            back = new Button(20, 20, 80, 80, TextureManager.texBackArrow);
        }

        // Klar
        static public bool Update(ref Inventory participantLeft, ref Inventory participantRight)
        {
            //När ett mussklick händer
            if (KMReader.MouseClick())
            {
                // Om knappen accept klickas
                if (accept.Click())
                {
                    //AcceptTrade(ref participantLeft, ref participantRight);
                    if (AcceptTrade(ref participantLeft, ref participantRight)) //Debuff
                    {
                        
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
                    return Exit();
                }
                // Checkar om man har klickat på inventory:n
                else
                {
                    if (CheckSlotClick(slotsLeft, Participant.Left) == false)
                    {
                        if (CheckSlotClick(slotsRight, Participant.Right) == false)
                        {
                            if (CheckSlotClick(tradeSlotsLeft, Participant.TradeSlotsLeft) == false)
                            {
                                CheckSlotClick(tradeSlotsRight, Participant.TradeSlotsRight);
                            }
                        }
                    }                    
                }
            }

            return false;
        }
       
        // Klar
        //Uppdaterar Slots
        static void UpdateSlots()
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
                        slotsLeft[i, j].Item = invLeft.ItemList[i * 5 + j];
                    }
                    catch (Exception)
                    {
                        slotsLeft[i, j].Item = null;
                    }
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

        //Klar
        static void UpdatePrices()
        {
            leftPrice = CheckValue(tradeLeft);
            rightPrice = CheckValue(tradeRight);
            priceDifference = leftPrice - rightPrice;
        }

        //Klar
        static bool CheckSlotClick(Slot[,] slots, Participant participant)
        {
            counterCol = 0;
            counterRow = 0;         

            while (counterCol <= slots.GetLength(1)-1 && slots[counterCol, counterRow].Clicked() == false) 
            {
                while (counterRow < slots.GetLength(1) - 1 && slots[counterCol, counterRow].Clicked() == false)
                {
                    ++counterRow;
                }

                if (slots[counterCol, counterRow].Clicked() == false)
                {
                    counterRow = 0;
                    ++counterCol;
                }
            }
            if (counterCol == slots.GetLength(1))
            {
                --counterCol;
            }
            if (slots[counterCol, counterRow].Clicked() == true && slots[counterCol, counterRow].Item != null) // Om Slots:en som tillhör inventory:n har blivit klickat
            {
                if (participant == Participant.Left) // Inventory to the left aka Player
                {
                    tradeLeft.AddItem(slots[counterCol, counterRow].Item.ID, 1);// Lägger till item till det vänstra trade fältet   // Error Finns inte
                    invLeft.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, 1);
                }
                else if (participant == Participant.Right) // Inventoty to the right aka Merchant
                {
                    tradeRight.AddItem(slots[counterCol, counterRow].Item.ID, 1); // Lägger till item till det högra trade fältet
                    invRight.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, 1);
                }
                else if (participant == Participant.TradeSlotsLeft)
                {
                    tradeLeft.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, 1);
                    invLeft.AddItem(slots[counterCol, counterRow].Item.ID, 1);
                }
                else if (participant == Participant.TradeSlotsRight)
                {
                     tradeRight.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, 1);
                    invRight.AddItem(slots[counterCol, counterRow].Item.ID, 1);
                }
                UpdateSlots();
                UpdatePrices();
                return true;
            }
            return false;
        }
        
        //Klar
        static bool AcceptTrade(ref Inventory participantLeft, ref Inventory participantRight)
        {
            //If nothing is presented
            if (tradeLeft.ItemList.Count == 0 && tradeRight.ItemList.Count == 0)
            {
                return false;
            }
            //If player and Merchant doesn't have enough space in inventory.
            if (CheckInvFull())
            {
                return false;
            }
            //If player doesn't have enough coin and merchant neither.
            if (invLeft.Money+leftPrice-rightPrice < 0)
            {
                return false;
            }
            if (invRight.Money + rightPrice-leftPrice < 0)
            {
                return false;
            }

            // Update both player and merchant's inventory
            ChangeInv();
            participantLeft = invLeft;
            participantRight = invRight;
            origLeftInv = invLeft;
            origRightInv = invRight;
            ResetTrade();
            //Exit();
            return true;
        }

        //Klar
        static bool CheckInvFull()
        {
            bool full = false;
            Inventory left = new Inventory(invLeft.Money);
            Inventory right = new Inventory(invLeft.Money);
            foreach (var item in invLeft.ItemList)
            {
                left.AddItem(item);
            }
            foreach (var item in invRight.ItemList)
            {
                right.AddItem(item);
            }
            foreach (var item in tradeRight.ItemList)
            {
                if (left.AddItem(item) == false)
                {
                    full = true;
                }
            }
            foreach (var item in tradeLeft.ItemList)
            {
                if (right.AddItem(item) == false)
                {
                    full = true;
                }
            }

            return full;
        }

        //Klar
        static void ChangeInv()
        {
            invLeft.Money += leftPrice - rightPrice;
            invRight.Money += rightPrice - leftPrice;
            foreach (var item in tradeRight.ItemList)
            {
                invLeft.AddItem(item);
            }
            foreach (var item in tradeLeft.ItemList)
            {
                invRight.AddItem(item);
            }
        }

        // Klar
        static int CheckValue(Inventory inv)
        {
            double sum = 0;
            for (int i = 0; i < inv.ItemList.Count; i++)
            {
                sum += inv.ItemList[i].BasePrice*ModifierManager.GetModifier(zoneName, inv.ItemList[i].PrimaryCategory) * inv.ItemList[i].Amount;
            }
            return (int)sum; // Avrundas nedåt
        }
        
        // Klar
        static void ResetTrade()
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
            
            invLeft = origLeftInv;
            invRight = origRightInv;
            foreach (var item in tradeLeft.ItemList) 
            {
                invLeft.AddItem(item);
            }
            foreach (var item in tradeRight.ItemList)
            {
                invRight.AddItem(item);
            }
            tradeLeft.ItemList.Clear();
            tradeRight.ItemList.Clear();
            UpdateSlots();
            UpdatePrices();
        }

        //Klar
        static bool Exit()
        {
            invLeft = null;
            invRight = null;
            tradeLeft = null;
            tradeRight = null;
            origLeftInv = null;
            origRightInv = null;            
            tradeSlotsLeft = null;
            tradeSlotsRight = null;
            slotsLeft = null;
            slotsRight = null;
            zoneName = null;

            return true;
        }

        // Klar
        static public void Draw(SpriteBatch sb)
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
            sb.DrawString(TextureManager.fontButton,"Player money: " + invLeft.Money, new Vector2(245, 520),Color.White);
            sb.DrawString(TextureManager.fontButton, "Merchant money: " + invRight.Money, new Vector2(1385, 520), Color.White);
            if (priceDifference < 0)
            {
                sb.DrawString(TextureManager.fontButton, "Cost: " + priceDifference*-1, new Vector2(900, 520), Color.White);
            }
            else
            {
                sb.DrawString(TextureManager.fontButton, "Gain: " + priceDifference, new Vector2(900, 520), Color.White);
            }
            
            
        }
    }
}
