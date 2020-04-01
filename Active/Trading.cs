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
         Inventory left, right;

             
             */
        Button[,] invLeft, invRight,tradeLeft,tradeRight;
        Button accept, enter,cancel;
        
        void Initialize(/*Entity left, Entity Right*/)
        {
            invLeft = new Button[5, 5];
            invRight = new Button[5, 5];
            tradeLeft = new Button[3, 3];
            tradeRight = new Button[3, 3];
            /*
             this.entLeft = left;
             this.entRight = right;
             */
            accept = new Button(5,5,100,100,TextureManager.box);
            enter = new Button(5, 5, 100, 100, TextureManager.box);
            cancel = new Button(5, 5, 100, 100, TextureManager.box);
        }
        
        bool Update()
        {
            if (KMReader.MouseClick())
            {

            }
            return true;
        }

        bool CheckBoxClicked()
        {

            return false;
        }

        void Draw(SpriteBatch sb)
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
            cancel.Draw(sb);

        }
    }
}
