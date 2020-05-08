﻿using System;
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

        public bool Click()
        {
            if (KMReader.mouseState.LeftButton == ButtonState.Pressed && KMReader.prevMouseState.LeftButton == ButtonState.Released) //för att "hitta" exakt när kanppen trycks på
            {
                if (KMReader.MouseClick())
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
                Vector2 temp = TextureManager.fontButton.MeasureString(text);
                Vector2 temp2 = new Vector2(hitBox.X + ((HitBox.Width - (int)temp.X)/2), hitBox.Y + ((HitBox.Height - (int)temp.Y) / 2));

                spriteBatch.DrawString(TextureManager.fontButton, text, temp2, Color.Black);
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