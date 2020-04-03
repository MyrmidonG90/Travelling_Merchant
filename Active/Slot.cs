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
        bool empty;
        Item item;
        int amountOfItem;
        Rectangle hitbox;

        public Slot(bool empty, int posX, int posY,int width, int height)
        {
            hitbox = new Rectangle(posX,posY,width,height);
            this.empty = empty;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(TextureManager.texBox,hitbox,Color.White);
            if (item != null)
            {
                item.Draw(sb, hitbox);
            }
        }

        public void Update()
        {
            
        }

        public bool Clicked()
        {
            if (KMReader.MouseClick())
            {
                if (hitbox.Contains(KMReader.GetMousePoint()))
                {
                    return true;
                }
            }
            return false;
        }
        public void AddItem(Item item, int amountOfItem)
        {
            this.item = item;
            this.amountOfItem = amountOfItem;
        }
        public int GetItemId()
        {
            return item.id;
        }


        public bool Empty { get => empty; set => empty = value; }
        public int AmountOfItem { get => amountOfItem; set => amountOfItem = value; }
        public void Reset()
        {
            item = null;
            empty = true;
            amountOfItem = 0;
        }
        
    }
}
