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

        static public void Start(int skill)
        {
            if ((Skill)skill ==  Skill.Intimidation)
            {
                currentTexture = TextureManager.texRankUpIntimidation;
            }
            else if ((Skill)skill == Skill.Persuasion)
            {
                currentTexture = TextureManager.texRankUpPersuasion;
            }
            else if ((Skill)skill == Skill.Wisdom)
            {
                currentTexture = TextureManager.texRankUpWisdom;
            }

            onGoing = true;
            imagePos = new Vector2(0 - currentTexture.Width, 1080 / 2 - currentTexture.Height / 2);
            hitBox = new Rectangle((int)imagePos.X + currentTexture.Width / 2, (int)imagePos.Y + currentTexture.Height / 2, 30, 30);
        }
        static public void Start(string skillName)
        {
            if (skillName == "Intimidation")
            {
                currentTexture = TextureManager.texRankUpIntimidation;
            }
            else if (skillName == "Persuasion")
            {
                currentTexture = TextureManager.texRankUpPersuasion;
            }
            else if (skillName == "Wisdom")
            {
                currentTexture = TextureManager.texRankUpWisdom;
            }

            onGoing = true;
            midWayStop = true;
            imagePos = new Vector2(0 - currentTexture.Width, 1080 / 2 - currentTexture.Height / 2);
            hitBox = new Rectangle((int)imagePos.X + currentTexture.Width / 2, (int)imagePos.Y + currentTexture.Height / 2, 30, 30);
        }
        static int FindSkill(string skillName)
        {
            int answer = -1;
            if (skillName == "Intimidation")
            {
                answer = 0;
            }
            else if (skillName == "Persuasion")
            {
                answer = 1;
            }
            else if (skillName == "Wisdom")
            {
                answer = 2;
            }
            return answer;
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
