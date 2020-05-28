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
        }

        public override void Update() // Behövs pga inheritance
        {
            
        }
    }
}
