using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Active
{
    class Slot
    {
        Item item;
        
        Rectangle hitbox;
        Vector2 textPos;
        public Slot(int posX, int posY,int width, int height)
        {
            hitbox = new Rectangle(posX,posY,width,height);
            textPos = new Vector2(posX+width/2,posY+height/2);
            item = null;
        }

        public void Draw(SpriteBatch sb)
        {
            //sb.Draw(TextureManager.texBox,hitbox,Color.White);
            if (Item != null)
            {
                Item.Draw(sb, hitbox);
                sb.DrawString(TextureManager.font13,item.Amount.ToString(), textPos, Color.White);
            }
        }

        public bool LeftClicked()
        {
            if (KMReader.LeftMouseClick())
            {
                if (hitbox.Contains(KMReader.GetMousePoint()))
                {
                    return true;
                }
            }
            return false;
        }

        public bool RightClicked()
        {
            if (KMReader.RightMouseClick())
            {
                if (hitbox.Contains(KMReader.GetMousePoint()))
                {
                    return true;
                }
            }
            return false;
        }

        public void AddItem(Item item)
        {
            Item = item;

        }

        public int GetItemId()
        {
            return item.ID;
        }
        internal Item Item { get => item; set => item = value; }

        public void Reset()
        {
            Item = null;
        }
        
    }
}
