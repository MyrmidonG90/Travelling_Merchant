using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Active
{
    class Slot
    {
        protected Rectangle hitbox;
        protected Texture2D texture;
        public Slot(int posX, int posY, int width, int height)
        {
            hitbox = new Rectangle(posX, posY, width, height);
        }

        public void AddTexture(Texture2D texture)
        {
            this.texture = texture;
        }

        /// <summary>
        /// Doesn't check if mouse is clicked due to optimization
        /// </summary>
        /// <returns></returns>
        public bool LeftClicked()
        {
            return KMReader.OverRectangle(hitbox);
        }

        public bool RightClicked()
        {
            return KMReader.OverRectangle(hitbox);
        }

        public virtual void Draw(SpriteBatch sb)
        {
            if (texture != null)
            {
                sb.Draw( texture,hitbox, Color.White);
            }
        }

        public void DrawShadow(SpriteBatch sb)
        {
            if (texture != null)
            {
                sb.Draw(texture, hitbox, Color.Black);
            }
        }
    }
}
