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

        static void Start(int skill)
        {
            
            imagePos = new Vector2(0-currentTexture.Width,1080/2-currentTexture.Height/2);
            hitBox = new Rectangle();
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
        }
        static void Update(double timePassed)
        {
            if (onGoing)
            {
                if (imagePos.X > 1980)
                {
                    onGoing = false;
                    
                }
                imagePos.X += 1;
            }
        }
        static public void Draw(SpriteBatch sb)
        {
            sb.Draw(currentTexture,imagePos,Color.White);
        }
    }
}
