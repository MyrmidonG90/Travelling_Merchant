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
    class ItemSlot : Slot
    {
        Item item;
        Vector2 textPos;

        internal Item Item { get => item; set => item = value; }

        public ItemSlot(int posX, int posY, int width, int height): base(posX,posY,width,height)
        {
            textPos = new Vector2(posX+width/2,posY+height/2);
            item = null;
        }

        public override void Draw(SpriteBatch sb)
        {
            if (item != null)
            {
                item.Draw(sb, hitbox);
                sb.DrawString(TextureManager.font13,item.Amount.ToString(), textPos, Color.White);
            }
        }

        public void AddItem(Item item)
        {
            this.item = item;
            
        }

        public int GetItemId()
        {
            return item.ID;
        }

        public void Reset()
        {
            item = null;
        }
        
    }
}
