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
        static ItemSlot[,] slotsLeft, slotsRight, tradeSlotsLeft, tradeSlotsRight;
        static Button accept, reset, back;
        static Item selected;
        static bool finished;
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

        internal static Inventory TradeRight { get => tradeRight; set => tradeRight = value; }

        static public void StartTrade(Inventory player, Inventory trader, string zone) 
        {
            invLeft = player;
            invRight = trader;
            finished = false;
            tradeLeft = new Inventory(player.Money,true);
            tradeRight = new Inventory(trader.Money,true);

            priceDifference = 0;
            origLeftInv = player;
            origRightInv = trader;
            CreateSlots();
            CreateButtons();
            
            zoneName = zone;
        }

        static void CreateSlots() // Skapar slots
        {
            slotsLeft = new ItemSlot[5, 5];
            slotsRight = new ItemSlot[5, 5];
            tradeSlotsLeft = new ItemSlot[3, 3];
            tradeSlotsRight = new ItemSlot[3, 3];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    slotsLeft[i, j] = new ItemSlot(245 + 60 * j, 200 + 60 * i, 50, 50);
                    slotsRight[i, j] = new ItemSlot(1375 + 60 * j, 200 + 60 * i, 50, 50);
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
                    tradeSlotsRight[i, j] = new ItemSlot(1020 + 60 * j, 334 + 60 * i, 50, 50);
                    tradeSlotsLeft[i, j] = new ItemSlot(720 + 60 * j, 334 + 60 * i, 50, 50);
                }
            }
        }

        static void CreateButtons() // Initialiserar knapparna
        {
            accept = new Button(1020, 200, 260, 120, "accept", "Accept Trade", TextureManager.texButton);
            reset = new Button(720, 200, 260, 120, "reset", "Reset Trade", TextureManager.texButton);
            back = new Button(20, 20, 80, 80, TextureManager.texBackArrow);
        }

        static public bool Update(ref Inventory participantLeft, ref Inventory participantRight)
        {
            //När ett mussklick händer
            if (KMReader.LeftMouseClick())
            {
                RightClickEvent(); //Jag lade in den här för jag tyckte det kändes bra att se info när man vänster klickar också
                finished = LeftClickEvent(ref participantLeft, ref participantRight);

            }
            else if (KMReader.RightMouseClick())
            {
                RightClickEvent();
            }
            return finished;
        }
       static bool LeftClickEvent(ref Inventory participantLeft, ref Inventory participantRight)
        {
            bool answer = false;
            // Om knappen accept klickas
            if (accept.LeftClick())
            {
                //AcceptTrade(ref participantLeft, ref participantRight);
                if (AcceptTrade(ref participantLeft, ref participantRight)) //Debugg
                {

                }
            }
            // Om knappen reset klickas
            else if (reset.LeftClick())
            {
                ResetTrade();
            }
            // Om knappen back klickas
            else if (back.LeftClick())
            {
                answer= Exit();
            }
            // Checkar om man har klickat på inventory:n
            else
            {
                if (CheckSlotClick(slotsLeft, Participant.Left) == false) // Checkar om man har klickat på den vänstra inventorium
                {
                    if (CheckSlotClick(slotsRight, Participant.Right) == false) //Checkar om man har klickat på den högra inventorium
                    {
                        if (CheckSlotClick(tradeSlotsLeft, Participant.TradeSlotsLeft) == false) // Checkar om man har klickat på den vänstra trade inventorium
                        {
                            CheckSlotClick(tradeSlotsRight, Participant.TradeSlotsRight); // Checkar om man har klickat på den högra trade inventorium
                        }
                    }
                }
            }
            return answer;
        }
        static void RightClickEvent()
        {
            foreach (ItemSlot tempSlot in slotsLeft)
            {
                if (tempSlot.RightClicked())
                {
                    selected = tempSlot.Item;
                }
            }

            foreach (ItemSlot tempSlot in slotsRight)
            {
                if (tempSlot.RightClicked())
                {
                    selected = tempSlot.Item;
                }
            }

            foreach (ItemSlot tempSlot in tradeSlotsLeft)
            {
                if (tempSlot.RightClicked())
                {
                    selected = tempSlot.Item;
                }
            }

            foreach (ItemSlot tempSlot in tradeSlotsRight)
            {
                if (tempSlot.RightClicked())
                {
                    selected = tempSlot.Item;
                }
            }
        }
        static void UpdateSlots() 
        {
            UpdateTradeSlots();
            UpdateInventorySlots();
            
        }
        static void UpdateTradeSlots()
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
        }
        static void UpdateInventorySlots()
        {
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

        static void UpdatePrices()
        {
            leftPrice = CheckValue(tradeLeft);
            rightPrice = CheckValue(tradeRight);
            priceDifference = leftPrice - rightPrice;
        }

        static bool CheckSlotClick(ItemSlot[,] slots, Participant participant)
        {
            counterCol = 0;
            counterRow = 0;
            
            while (counterCol <= slots.GetLength(1)-1 && slots[counterCol, counterRow].LeftClicked() == false) 
            {
                while (counterRow < slots.GetLength(1) - 1 && slots[counterCol, counterRow].LeftClicked() == false)
                {
                    ++counterRow;
                }

                if (slots[counterCol, counterRow].LeftClicked() == false)
                {
                    counterRow = 0;
                    ++counterCol;
                }
            }
            if (counterCol == slots.GetLength(1))
            {
                --counterCol;
            }
            if (slots[counterCol, counterRow].LeftClicked() == true && slots[counterCol, counterRow].Item != null) // Om Slots:en som tillhör inventory:n har blivit klickat
            {
                SlotClick(slots, participant);
                return true;
            }
            return false;
        }

        static void SlotClick(ItemSlot[,] slots, Participant participant)
        {
            if (CheckShiftClick())
            {
                ShiftAndMouseClick(slots, participant);
            }
            else
            {
                MouseSlotClick(slots, participant);
            }
            UpdateSlots();
            UpdatePrices();
        }

        static void MouseSlotClick(ItemSlot[,] slots, Participant participant)
        {
            if (participant == Participant.Left)
            {
                if (tradeLeft.AddItem(slots[counterCol, counterRow].Item.ID, 1))
                {
                    invLeft.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, 1);
                }                               
            }
            else if (participant == Participant.Right)
            {
                if (tradeRight.AddItem(slots[counterCol, counterRow].Item.ID, 1))
                {
                    invRight.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, 1);
                }                
            }
            else if (participant == Participant.TradeSlotsLeft)
            {
                if (invLeft.AddItem(slots[counterCol, counterRow].Item.ID, 1))
                {
                    tradeLeft.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, 1);
                }                
            }
            else if (participant == Participant.TradeSlotsRight)
            {
                if (invRight.AddItem(slots[counterCol, counterRow].Item.ID, 1))
                {
                    tradeRight.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, 1);
                }                
            }
        }

        static void ShiftAndMouseClick(ItemSlot[,] slots, Participant participant) // Too Long
        {
            if (participant == Participant.Left)
            {
                if (invLeft.ItemList[invLeft.FindIndexOf(slots[counterCol, counterRow].Item.ID)].Amount >= 5)
                {
                    tradeLeft.AddItem(slots[counterCol, counterRow].Item.ID, 5);// Lägger till item till det vänstra trade fältet
                    invLeft.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, 5);
                }
                else
                {
                    tradeLeft.AddItem(slots[counterCol, counterRow].Item.ID, invLeft.ItemList[invLeft.FindIndexOf(slots[counterCol, counterRow].Item.ID)].Amount);// Lägger till item till det vänstra trade fältet
                    invLeft.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, invLeft.ItemList[invLeft.FindIndexOf(slots[counterCol, counterRow].Item.ID)].Amount);
                }
            }
            else if (participant == Participant.Right)
            {
                if (invRight.ItemList[invRight.FindIndexOf(slots[counterCol, counterRow].Item.ID)].Amount >= 5)
                {
                    if (tradeRight.AddItem(slots[counterCol, counterRow].Item.ID, 5))
                    {
                        invRight.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, 5);
                    }
                   ;// Lägger till item till det vänstra trade fältet ///////////////////////////////////////////////////////////////!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    
                }
                else
                {
                    tradeRight.AddItem(slots[counterCol, counterRow].Item.ID, invRight.ItemList[invRight.FindIndexOf(slots[counterCol, counterRow].Item.ID)].Amount);// Lägger till item till det vänstra trade fältet
                    invRight.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, invRight.ItemList[invRight.FindIndexOf(slots[counterCol, counterRow].Item.ID)].Amount);
                }
            }
            else if (participant == Participant.TradeSlotsLeft)
            {
                if (tradeLeft.ItemList[tradeLeft.FindIndexOf(slots[counterCol, counterRow].Item.ID)].Amount >= 5)
                {
                    invLeft.AddItem(slots[counterCol, counterRow].Item.ID, 5);// Lägger till item till det vänstra trade fältet
                    tradeLeft.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, 5);
                }
                else
                {
                    invLeft.AddItem(slots[counterCol, counterRow].Item.ID, tradeLeft.ItemList[tradeLeft.FindIndexOf(slots[counterCol, counterRow].Item.ID)].Amount);// Lägger till item till det vänstra trade fältet
                    tradeLeft.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, tradeLeft.ItemList[tradeLeft.FindIndexOf(slots[counterCol, counterRow].Item.ID)].Amount);
                }
            }
            else if (participant == Participant.TradeSlotsRight)
            {
                if (tradeRight.ItemList[tradeRight.FindIndexOf(slots[counterCol, counterRow].Item.ID)].Amount >= 5)
                {
                    invRight.AddItem(slots[counterCol, counterRow].Item.ID, 5);// Lägger till item till det vänstra trade fältet
                    tradeRight.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, 5);
                }
                else
                {
                    invRight.AddItem(slots[counterCol, counterRow].Item.ID, tradeRight.ItemList[tradeRight.FindIndexOf(slots[counterCol, counterRow].Item.ID)].Amount);// Lägger till item till det vänstra trade fältet
                    tradeRight.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, tradeRight.ItemList[tradeRight.FindIndexOf(slots[counterCol, counterRow].Item.ID)].Amount);
                }
            }
        }
        
        static bool CheckShiftClick()
        {
            return KMReader.IsKeyDown(Keys.LeftShift);
        }

        static bool AcceptTrade(ref Inventory participantLeft, ref Inventory participantRight)
        {
            //EndCredits.Start();///////////////////////////////////////////////////////////////////////////////

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

            foreach (Item item in tradeRight.ItemList)
            {
                if(item.Name == "Carrot" && AchievementManager.achievements[0].complete == false)
                {
                    AchievementManager.boughtCarrots += item.Amount;
                    if (AchievementManager.boughtCarrots > 100)
                    {
                        AchievementManager.boughtCarrots = 100;
                    }
                }
            }

            ChangeInv();
            participantLeft = invLeft;
            participantRight = invRight;
            origLeftInv = invLeft;
            origRightInv = invRight;
            ResetTrade();
            return true;
        }

        static bool CheckInvFull()
        {
            bool full = false;
            Inventory left = new Inventory(invLeft.Money);
            Inventory right = new Inventory(invRight.Money);
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

        static void ChangeInv()
        {
            invLeft.Money += leftPrice - rightPrice;
            //GlossaryManager.UpdateGlossary("Item");
            invRight.Money += rightPrice - leftPrice;

            if(rightPrice - leftPrice >= 0)
            {
                AchievementManager.spentMoney += rightPrice - leftPrice;
            }

            if (leftPrice - rightPrice >= 0)
            {
                AchievementManager.totalCoinsEarned += leftPrice - rightPrice;
            }

            foreach (var item in tradeRight.ItemList)
            {
                invLeft.AddItem(item);
            }
            foreach (var item in tradeLeft.ItemList)
            {
                invRight.AddItem(item);
            }
            tradeLeft.ItemList.Clear();
            tradeRight.ItemList.Clear();


        }

        static int CheckValue(Inventory inv)
        {
            double sum = 0;
            for (int i = 0; i < inv.ItemList.Count; i++)
            {
                double temp = inv.ItemList[i].BasePrice *  SkillManager.ReturnSkillModifier(zoneName) * inv.ItemList[i].Amount;
                sum += (inv.ItemList[i].BasePrice*ModifierManager.GetModifier(zoneName, inv.ItemList[i].PrimaryCategory) * inv.ItemList[i].Amount) - temp;
            }
            return (int)sum; // Avrundas nedåt
        }
        
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
            //
            UpdateSlots();
            UpdatePrices();
        }

        static bool Exit()
        {
            ResetTrade();
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

        static public void Draw(SpriteBatch sb) 
        {
            sb.Draw(TextureManager.texTradeMenu, new Vector2(190, 144), Color.White);
            DrawSlots(sb);
            DrawButtons(sb);

            sb.DrawString(TextureManager.font24, invLeft.Money.ToString(), new Vector2(315, 530),Color.Black);
            sb.DrawString(TextureManager.font24, invRight.Money.ToString(), new Vector2(1520, 530), Color.Black);

            DrawCostIcon(sb);
            if (selected != null)
            {
                DrawSelected(sb);
            }

        }
        static void DrawButtons(SpriteBatch sb)
        {
            accept.Draw(sb);
            reset.Draw(sb);
            back.Draw(sb);
        }
        static void DrawSlots(SpriteBatch sb)
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
        }
        static void DrawCostIcon(SpriteBatch sb)
        {
            Vector2 arrowPos = new Vector2(867, 502);
            Vector2 moneyPos = new Vector2(920, 535);
            if (priceDifference < 0)
            {
                sb.Draw(TextureManager.texArrowMoneyRight, arrowPos, Color.White);
                sb.DrawString(TextureManager.font24, (priceDifference * -1).ToString(), moneyPos, Color.Black);
            }
            else if (priceDifference > 0)
            {
                sb.Draw(TextureManager.texArrowMoneyLeft, arrowPos, Color.White);
                sb.DrawString(TextureManager.font24, priceDifference.ToString(), moneyPos, Color.Black);
            }
            else if (priceDifference == 0)
            {
                sb.DrawString(TextureManager.font24, priceDifference.ToString(), moneyPos, Color.Black);
            }
        }
        static void DrawSelected(SpriteBatch sb)
        {
            Vector2 temp = TextureManager.font24.MeasureString(selected.Name);
            Vector2 namePos = new Vector2(((1920 - temp.X) / 2) - 5, 610);
            sb.DrawString(TextureManager.font24, selected.Name, namePos, Color.Black);
            if (selected.Rarity == 0)
            {
                sb.Draw(TextureManager.texIconCommon, new Rectangle(930, 650, 60, 60), Color.White);
            }
            if (selected.Rarity == 1)
            {
                sb.Draw(TextureManager.texIconUncommon, new Rectangle(930, 650, 60, 60), Color.White);
            }
            if (selected.Rarity == 2)
            {
                sb.Draw(TextureManager.texIconRare, new Rectangle(930, 650, 60, 60), Color.White);
            }
            sb.DrawString(TextureManager.font24, "Base Price: " + selected.BasePrice.ToString(), new Vector2(785, 710), Color.Black);
            sb.DrawString(TextureManager.font24, "Current Price: " + (selected.BasePrice * SkillManager.ReturnSkillModifier(zoneName)).ToString(), new Vector2(785, 750), Color.Black); //måste fixas
            if (selected.PrimaryCategory != 999)
            {
                sb.Draw(TextureManager.texCategories[selected.PrimaryCategory - 1], new Rectangle(800, 780, 80, 80), Color.White);
            }
            if (selected.SecondaryCategory != 999)
            {
                sb.Draw(TextureManager.texCategories[selected.SecondaryCategory - 1], new Rectangle(915, 780, 80, 80), Color.White);
            }
            if (selected.TertiaryCategory != 999)
            {
                sb.Draw(TextureManager.texCategories[selected.TertiaryCategory - 1], new Rectangle(1030, 780, 80, 80), Color.White);
            }
        }
    }
}