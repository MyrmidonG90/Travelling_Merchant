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
    static class Trading
    {

        static Inventory invLeft, invRight, tradeLeft, tradeRight;
        static Slot[,] slotsLeft, slotsRight, tradeSlotsLeft, tradeSlotsRight;
        static Button accept, reset, back;
        enum Participant
        {
            Left,
            Right,
            None
        }
        static int counterCol, counterRow, tmpCounter, leftPrice, rightPrice, priceDifference;

        // Klart
        static public void Initialize(Inventory left, Inventory right)
        {
            invLeft = left;
            invRight = right;
            tradeLeft = new Inventory(left.Money);
            tradeRight = new Inventory(right.Money);
            CreateSlots();
            CreateButtons();
        }

        // Klar
        static void CreateSlots()
        {
            slotsLeft = new Slot[5, 5];
            slotsRight = new Slot[5, 5];
            tradeSlotsLeft = new Slot[3, 3];
            tradeSlotsRight = new Slot[3, 3];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    slotsLeft[i, j] = new Slot(50 + 60 * j, 100 + 60 * i, 50, 50);
                    slotsRight[i, j] = new Slot(1570 + 60 * j, 100 + 60 * i, 50, 50);
                }
            }
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    tradeSlotsLeft[i, j] = new Slot(1010 + 60 * j, 100 + 60 * i, 50, 50);
                    tradeSlotsRight[i, j] = new Slot(660 + 60 * j, 100 + 60 * i, 50, 50);
                }
            }
        }

        // Klar
        static void CreateButtons() // Initialiserar knapparna
        {
            accept = new Button(710, 630, 500, 200, TextureManager.texBox);
            reset = new Button(1260, 630, 500, 200, TextureManager.texBox);
            back = new Button(160, 630, 500, 200, TextureManager.texBox);
        }

        // Håller processen trading igång. När den returnerar false betyder det att transaktionen är ej över och vice versa när man returnerar true
        // Klar?
        static public bool Update(ref Inventory participantLeft, ref Inventory participantRight)
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
                    if (CheckInvSlotClick(slotsLeft, Participant.Left) == false)
                    {
                        CheckInvSlotClick(slotsRight, Participant.Right);
                    }
                    // glömde lägga till 
                }
            }

            return false;
        }

        //Lite till
        static bool CheckInvSlotClick(Slot[,] slots, Participant participant)
        {
            counterCol = 0;
            counterRow = 0;
            // Tittar om vänstra inventory:n har blivit klickat
            do // Observera om man behöver verkligen använda do-while-loop
            {
                do
                {
                    if (slots[counterCol, counterRow].Clicked() == false)
                    {
                        ++counterRow; // Ökar counterRow
                    }
                } while (counterRow < 4 && slots[counterCol, counterRow].Clicked() == false);

                if (slots[counterCol, counterRow].Clicked() == false)
                {
                    ++counterCol; // Ökar counterCol
                }
            } while (counterCol < 4 && slots[counterCol, counterRow].Clicked() == false);

            if (slots[counterCol, counterRow].Clicked() == true) // Om Slots:en som tillhör inventory:n har blivit klickat
            {
                if (participant == Participant.Left)
                {
                    tradeLeft.AddItem(slots[counterCol, counterRow].Item.ID, 1);// Lägger till item till det vänstra trade fältet   // Error Finns inte
                    invLeft.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, 1);
                }
                else if (participant == Participant.Right)
                {
                    tradeRight.AddItem(slots[counterCol, counterRow].Item.ID, 1); // Lägger till item till det högra trade fältet
                    invRight.ReduceAmountOfItems(slots[counterCol, counterRow].Item.ID, 1);
                }

                UpdateSlots();
                UpdatePrices();
                return true;
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
                        slotsLeft[i, j].Item = invLeft.ItemList[i * 5 + j];
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

        static void UpdateInventory()
        {
            for (int i = 0; i < 5; i++)
            {

            }
        }

        //Klar?
        static bool AcceptTrade(ref Inventory participantLeft, ref Inventory participantRight)
        {
            //If nothing is presented
            if (tradeLeft.ItemList.Count == 0 && tradeRight.ItemList.Count == 0)// !!!Något fel här!!!
            {
                return false;
            }
            //If player and Merchant doesn't enough space in inventory Advance.
            //Else prompt that the player doesn't have enough space
            if (invLeft.ItemList.Count < 24 || invRight.ItemList.Count < 24)
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
        static int CheckValue(Inventory inv)
        {
            int sum = 0;
            for (int i = 0; i < inv.ItemList.Count; i++)
            {
                sum += inv.ItemList[i].BasePrice * inv.ItemList[i].Amount;
            }
            return sum;
        }

        //Klar
        static void UpdatePrices()
        {
            leftPrice = CheckValue(tradeLeft);
            rightPrice = CheckValue(tradeRight);
            priceDifference = leftPrice - rightPrice;
        }

        //Klar
        static void ConstructSlots(Inventory inventory)
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

            tradeLeft = null;
            tradeRight = null;
        }

        // Ej klar, måste införa antal pengar och sånt
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
        }
    }
}
