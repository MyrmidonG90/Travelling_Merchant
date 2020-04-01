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
    class Button
    {
        private Rectangle hitBox;
        private Texture2D tex;
        public string name;

        public Button(int x, int y, int xLength, int yLength, string name)
        {
            hitBox = new Rectangle(x, y, xLength, yLength);
            tex = TextureManager.texMap;
            this.name = name;
        }

        public bool Click(MouseState newMouseState, MouseState oldMouseState)
        {
            if (newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released) //för att "hitta" exakt när kanppen trycks på
            {
                if (hitBox.Contains(newMouseState.Position))
                {
                    return true;
                }
            }
            return false;
        }

        public void Update(GameTime gameTime)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, hitBox, Color.Red);
        }
    }
}