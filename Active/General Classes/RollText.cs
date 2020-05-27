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
        SpriteFont font;
        bool finished;
        bool onGoing;
        int velocity;
        bool timed;
        double timeAlive;
        double timeStart;
        double timer;
        Vector2 startPos;

        public bool Finished { get => finished;}
        public bool OnGoing { get => onGoing; }
        public SpriteFont Font { get => font; set => font = value; }

        public RollText(int velocity)
        {
            this.velocity = velocity;
            texts = new List<string>();
            textPos = new List<Vector2>();
            finished = false;
            timer = 0;
            timed = false;
            font = TextureManager.font48;
        }
        public RollText(int velocity, double timeAlive)
        {
            this.velocity = velocity;
            this.timeAlive = timeAlive;
            timeStart = timeAlive;
            texts = new List<string>();
            textPos = new List<Vector2>();
            onGoing = false;
            finished = false;
            timer = 0;
            timed = true;
            font = TextureManager.font48;
        }
        public void Start()
        {
            onGoing = true;
        }

        public void AddText(string text) // Börjar Längst ner
        {
            texts.Add(text);
            textPos.Add( new Vector2(1920 / 2 - font.MeasureString(texts[texts.Count - 1]).X/2, 1080 + textPos.Count * font.MeasureString("I").Y));
        }
        public void AddText(string text,Vector2 startPos) // Börjar vid start positionen
        {
            texts.Add(text);
            this.startPos = startPos;
            textPos.Add(startPos);
        }

        public bool MoveTextVertical(double timePassed)
        {
            timer -= timePassed;
            if (timer < 0)
            {
                if (timed)
                {
                    timeAlive -= timePassed;
                    if (timeAlive < 0)
                    {
                        finished = true;
                        onGoing = false;
                    }
                    else
                    {
                        for (int i = 0; i < textPos.Count; i++)
                        {
                            textPos[i] = new Vector2(textPos[i].X, textPos[i].Y - velocity);
                        }
                    }
                    timer = 15;
                }
                else
                {
                    if (textPos[textPos.Count - 1].Y + font.MeasureString("I").Y< 0)
                    {
                        finished = true;
                    }
                    else
                    {
                        for (int i = 0; i < textPos.Count; i++)
                        {
                            textPos[i] = new Vector2(textPos[i].X, textPos[i].Y - velocity);
                        }
                    }
                    timer = 15;
                }
            }
            
            return finished;
        }
        public void Reset()
        {
            finished = false;
            onGoing = false;
            textPos = new List<Vector2>();
            if (timed)
            {
                textPos.Add(startPos);
                timeAlive = timeStart;
            }
            else
            {
                for (int i = 0; i < texts.Count; i++)
                {
                    textPos.Add(new Vector2(1920 / 2 - font.MeasureString(texts[texts.Count - 1]).X / 2, 1080 + 5 + textPos.Count * font.MeasureString("I").Y));
                }
            }            
        }

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < texts.Count; i++)
            {
                sb.DrawString(font,texts[i],textPos[i], Color.White);
            }
        }
    }
}
