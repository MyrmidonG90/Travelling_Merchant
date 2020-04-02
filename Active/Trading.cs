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
         Enitity entLeft,entRight;
         Inventory left, right,tradeLeft,tradeRight;

             
             */
        Button[,] invLeft, invRight,tradeLeft,tradeRight;
        Button accept, enter,back;
        enum Participant
        {
            Left,
            Right
        }
        int counter1, counter2;


        void Initialize(/*Entity left, Entity Right*/)
        {
            
            /*
             this.entLeft = left;
             this.entRight = right;
             */
            
        }

        void CreateButtons()
        {
            invLeft = new Button[5, 5];
            invRight = new Button[5, 5];
            tradeLeft = new Button[3, 3];
            tradeRight = new Button[3, 3];
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    //invLeft[i, j] = new Button();
                    //invRight[i, j] = new Button();
                }
            }
            accept = new Button(5, 5, 100, 100, TextureManager.texBox);
            enter = new Button(5, 5, 100, 100, TextureManager.texBox);
            back = new Button(5, 5, 100, 100, TextureManager.texBox);
        }
        
        bool Update()
        {
            if (KMReader.MouseClick())
            {
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
                else if (enter.Click())
                {

                }
                else if (back.Click())
                {
                    return true;
                }
                else if (true)
                {

                }
            }
            return false;
        }
        bool CheckInvClick(Participant participant)
        {
            counter1 = 0;
            counter2 = 0;
            if (participant == Participant.Left)
            {                
                while (counter1 < 5 && invLeft[counter1, counter2].Click())
                {
                    while (counter2 < 5 && invLeft[counter1, counter2].Click())
                    {
                        ++counter2;
                    }
                    if (!invLeft[counter1, counter2].Click())
                    {
                        ++counter1;
                    }
                }
            }
            else if (participant == Participant.Right)
            {
                
                while (counter1 < 5 && invLeft[counter1, counter2].Click())
                {
                    while (counter2 < 5 && invLeft[counter1, counter2].Click())
                    {
                        ++counter2;
                    }
                    if (!invLeft[counter1, counter2].Click())
                    {
                        ++counter1;
                    }
                }
            }
            return false;
        }
        void AddItem(Participant participant, int posRow, int posCol)
        {
            // Search if it's already inside the trade 
            //      If inside add another
            //      If not Check if there's a free slot
            //              If there is add that item to that free slot
            //              If not, Nothing happens

            while (true)
            {

            }
        }
        bool AcceptTrade()
        {
            //If player has enough coin and merchant has enough gold Advance
            //Else return false with a popup error
            // Update both player and merchant's inventory
            // Return true;
            return false;
        }
        
        void Draw(SpriteBatch sb)
        {
            DrawBoxes(sb);
            DrawItems(sb);
        }
        void DrawBoxes(SpriteBatch sb)
        {
            foreach (var item in invLeft)
            {
                item.Draw(sb);
            }
            foreach (var item in invRight)
            {
                item.Draw(sb);
            }
            foreach (var item in tradeLeft)
            {
                item.Draw(sb);
            }
            foreach (var item in tradeRight)
            {
                item.Draw(sb);
            }
            accept.Draw(sb);
            enter.Draw(sb);
            back.Draw(sb);
        }
        void DrawItems(SpriteBatch sb)
        {
            // Draw both inventories in a 5x5 grid
        }
    }
}
