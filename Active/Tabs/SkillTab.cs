using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Active
{
    class SkillTab : TabClass
    {
        public SkillTab()
        {
            name = "Skill Tab";
            mainBox = new Rectangle(260, 150, 1400, 880);

        }
        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(TextureManager.texSkillMenu, mainBox, Color.White);
            sb.DrawString(TextureManager.font32, "Wisdom: " + Player.ReturnSkillLevel("Wisdom"), new Vector2(400, 300), Color.Black);
            sb.DrawString(TextureManager.font32, "Intimidation: " + Player.ReturnSkillLevel("Intimidation"), new Vector2(400, 400), Color.Black);
            sb.DrawString(TextureManager.font32, "Persuasion: " + Player.ReturnSkillLevel("Persuasion"), new Vector2(400, 500), Color.Black);



            
            if(Player.skillLevels[0] == 0)
            {
                sb.DrawString(TextureManager.font32, Player.skillXP[0].ToString() + "/1", new Vector2(1100, 300), Color.Black);
            }
            else if((Player.skillLevels[0] * Player.skillLevels[0] * 100) < 2500)
            {
                sb.DrawString(TextureManager.font32, Player.skillXP[0] + "/" + Player.skillLevels[0] * Player.skillLevels[0] * 100, new Vector2(1100, 300), Color.Black);
            }
            else
            {
                sb.DrawString(TextureManager.font32, "MAX LEVEL", new Vector2(1100, 300), Color.Black);
            }


            if (Player.skillLevels[1] == 0)
            {
                sb.DrawString(TextureManager.font32, Player.skillXP[1].ToString() + "/1", new Vector2(1100, 400), Color.Black);
            }
            else if((Player.skillLevels[1] * Player.skillLevels[1] * 100) < 2500)
            {
                sb.DrawString(TextureManager.font32, Player.skillXP[1].ToString() + "/" + Player.skillLevels[1] * Player.skillLevels[1] * 100, new Vector2(1100, 400), Color.Black);
            }
            else
            {
                sb.DrawString(TextureManager.font32, "MAX LEVEL", new Vector2(1100, 400), Color.Black);
            }


            if (Player.skillLevels[2] == 0)
            {
                sb.DrawString(TextureManager.font32, Player.skillXP[2].ToString() + "/1", new Vector2(1100, 500), Color.Black);
            }
            else if((Player.skillLevels[2] * Player.skillLevels[2] * 100) < 2500)
            {
                sb.DrawString(TextureManager.font32, Player.skillXP[2].ToString() + "/" + Player.skillLevels[2] * Player.skillLevels[2] * 100, new Vector2(1100, 500), Color.Black);
            }
            else
            {
                sb.DrawString(TextureManager.font32, "MAX LEVEL", new Vector2(1100, 500), Color.Black);
            }







        }

        public override void Update() // Behövs pga inheritance
        {
            //please dont harm me i have a wife and three kids and have never hurt a single person in my entire life
        }
    }
}
