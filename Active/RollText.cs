using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Active
{
    class RollText
    {
        List<string> texts;
        List<Vector2> textPos;
        bool finished;
        int velocity;
        public RollText(int velocity)
        {
            this.velocity = velocity;
            texts = new List<string>();
            textPos = new List<Vector2>();
            finished = false;
        }
        public void AddText(string text)
        {
            texts.Add(text);
            textPos.Add( new Vector2(1920 / 2 - TextureManager.fontHeader.MeasureString(texts[texts.Count - 1]).X/2, 1080 + textPos.Count * TextureManager.fontHeader.MeasureString("I").Y));
        }
        public bool MoveTextVertical()
        {            
            if (textPos[textPos.Count-1].Y < 0)
            {
                finished = true;
            }
            else
            {
                for (int i = 0; i < textPos.Count; i++)
                {
                    textPos[i] = new Vector2(textPos[i].X , textPos[i].Y - velocity);
                }
            }
            return finished;
        }
        public void Reset()
        {
            finished = false;
            textPos = new List<Vector2>();
            for (int i = 0; i < texts.Count; i++)
            {
                textPos.Add(new Vector2(1920 / 2 - TextureManager.fontHeader.MeasureString(texts[texts.Count - 1]).X / 2, 1080 + 5 + textPos.Count * TextureManager.fontHeader.MeasureString("I").Y));
            }
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < texts.Count; i++)
            {
                sb.DrawString(TextureManager.fontHeader,texts[i],textPos[i], Color.White);
            }
        }
    }
}
