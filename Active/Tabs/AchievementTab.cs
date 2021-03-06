﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Active
{
    class AchievementTab : TabClass
    {
        public AchievementTab()
        {
            name = "Achievement Tab";
            mainBox = new Rectangle(260, 150, 1400, 880);
        }
        public override void Draw(SpriteBatch sb)
        {

            sb.Draw(TextureManager.texMenuAchievement, mainBox, Color.White);
            int temp = 0;
            sb.DrawString(TextureManager.font48, "Achievements", new Vector2(300, 180 + temp), Color.Black);
            sb.DrawString(TextureManager.font32, "Description", new Vector2(880, 200 + temp), Color.Black);
            temp += 100;
            foreach (Achievement achievement in AchievementManager.achievements)
            {
                if (achievement.complete)
                {
                    sb.Draw(TextureManager.texAffirmative, new Rectangle(275, 180 + temp, 40, 40), Color.White);
                }
                else
                {
                    sb.Draw(TextureManager.texNegative, new Rectangle(275, 180 + temp, 40, 40), Color.White);
                }
                sb.DrawString(TextureManager.font24, achievement.name + "  " + achievement.progress, new Vector2(330, 180 + temp), Color.Black);
                sb.DrawString(TextureManager.font24, achievement.description, new Vector2(880, 180 + temp), Color.Black);
                temp += 60;
            }
        }

        public override void Update() // Behövs för inheritance
        {
            //please dont harm me i have a wife and three kids and have never hurt a single person in my entire life
        }
    }
}
