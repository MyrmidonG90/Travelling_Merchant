using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Active
{
    static class LevelUp
    {
        static bool onGoing;
        static Texture2D currentTexture;
        static Vector2 imagePos;
        static double timer;
        static bool midWayStop;
        static Rectangle hitBox;
        enum Skill
        {
            Intimidation,
            Persuasion,
            Wisdom
        }
        static Skill skill;

        static public void Start(int skillNr)
        {
            FindTexture(skillNr);

            onGoing = true;
            imagePos = new Vector2(0 - currentTexture.Width, 1080 / 2 - currentTexture.Height / 2);
            hitBox = new Rectangle((int)imagePos.X + currentTexture.Width / 2, (int)imagePos.Y + currentTexture.Height / 2, 30, 30);
        }
        static public void Start(string skillName)
        {
            bool found = false;
            skill = 0;
            while (found == false) // Stuck in loop == input is wrong!
            {
                if (skill.ToString() == skillName)
                {
                    found = true;
                    FindTexture((int)skill);
                }
                else
                {
                    ++skill;
                }
            }

            onGoing = true;
            midWayStop = true;
            imagePos = new Vector2(0 - currentTexture.Width, 1080 / 2 - currentTexture.Height / 2);
            hitBox = new Rectangle((int)imagePos.X + currentTexture.Width / 2, (int)imagePos.Y + currentTexture.Height / 2, 30, 30);
        }
        static void FindTexture(int index)
        {
            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(TextureManager.texRankUpIntimidation);
            textures.Add(TextureManager.texRankUpPersuasion);
            textures.Add(TextureManager.texRankUpWisdom);


            currentTexture = textures[index];
        }

        static public void Update(double timePassed)
        {
            if (onGoing)
            {
                timer -= timePassed;
                if (timer < 0)
                {
                    if (imagePos.X > 1980) 
                    {
                        onGoing = false;
                    }
                    else
                    {
                        if (hitBox.Contains(960, 540) != true)
                        {
                            MoveNormally();
                        }
                        else
                        {
                            if (midWayStop != true)
                            {
                                MoveNormally();
                            }
                            else
                            {
                                timer = 1000;
                                midWayStop = false;
                            }
                        }
                    }
                }                
            }
        }
        static void MoveNormally()
        {
            imagePos.X += 30;
            hitBox.X += 30;
            timer = 10;
        }
        static public void Draw(SpriteBatch sb)
        {
            if (onGoing)
            {
                sb.Draw(currentTexture, imagePos, Color.White);
            }            
        }
    }
}
