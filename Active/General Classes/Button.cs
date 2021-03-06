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

        List<string> rader;
        List<Vector2> textPos;

        string name;
        string text;

        public Button(int x, int y, int xLength, int yLength, string name, string text, Texture2D tex)
        {
            hitBox = new Rectangle(x, y, xLength, yLength);

            this.tex = tex;
            this.name = name;
            this.text = text;
            rader = Radbrytte(text);
            AdjustTextPos();
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

        List<string> Radbrytte(string text)
        {
            List<string> strings = new List<string>();
            string[] split = text.Split('/');

            for (int i = 0; i < split.Length; i++)
            {
                strings.Add(split[i]);
            }

            return strings;
        }

        void AdjustTextPos()
        {
            textPos = new List<Vector2>();
            for (int i = 0; i < rader.Count; i++)
            {
                Vector2 stringMeasures = TextureManager.font24.MeasureString(rader[i]);                
                int middleX = hitBox.X + hitBox.Width / 2 - (int)stringMeasures.X/2;
                int middleY = hitBox.Y + (hitBox.Height / rader.Count) * i + (hitBox.Height / (rader.Count*2))-(int)stringMeasures.Y/2;
                textPos.Add(new Vector2(middleX, middleY));
            }
        }



        public void Draw(SpriteBatch spriteBatch)
        {
            if (text != null)
            {
                spriteBatch.Draw(tex, hitBox, Color.White);

                for (int i = 0; i < rader.Count; i++)
                {
                    spriteBatch.DrawString(TextureManager.font24, rader[i], textPos[i], Color.Black);
                }

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
