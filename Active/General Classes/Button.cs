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
    public class Button
    {
        Rectangle hitBox;
        Texture2D tex;

        string name;
        string text;

        public Button(int x, int y, int xLength, int yLength, string name, string text, Texture2D tex)
        {
            hitBox = new Rectangle(x, y, xLength, yLength);

            this.tex = tex;
            this.name = name;
            this.text = text;
        }

        public Button(int x, int y, int xLength, int yLength, string name, Texture2D tex)
        {
            hitBox = new Rectangle(x, y, xLength, yLength);

            this.tex = tex;
            this.name = name;
        }

        public Button(int x, int y, int xLength, int yLength, Texture2D tex)
        {
            hitBox = new Rectangle(x, y, xLength, yLength);
            this.tex = tex;

        }

        public bool LeftClick()
        {
            if (KMReader.mouseState.LeftButton == ButtonState.Pressed && KMReader.prevMouseState.LeftButton == ButtonState.Released) //för att "hitta" exakt när kanppen trycks på //ONÖDIGT!!!
            {
                if (KMReader.LeftMouseClick()) // 
                {
                    if (hitBox.Contains(KMReader.GetMousePoint()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool RightClick()
        {
            if (KMReader.mouseState.RightButton == ButtonState.Pressed && KMReader.prevMouseState.RightButton == ButtonState.Released) //för att "hitta" exakt när kanppen trycks på
            {
                if (KMReader.RightMouseClick())
                {
                    if (hitBox.Contains(KMReader.GetMousePoint()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            if (text != null)
            {
                spriteBatch.Draw(tex, hitBox, Color.White);
                Vector2 temp = TextureManager.font24.MeasureString(text);
                Vector2 temp2 = new Vector2(hitBox.X + ((HitBox.Width - (int)temp.X)/2), hitBox.Y + ((HitBox.Height - (int)temp.Y) / 2));

                spriteBatch.DrawString(TextureManager.font24, text, temp2, Color.Black);
            }
            else
            {
                spriteBatch.Draw(tex, hitBox, Color.White);
            }
        }

        public Rectangle HitBox
        {
            get
            {
                return hitBox;
            }
            set
            {
                hitBox = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
    }
}
