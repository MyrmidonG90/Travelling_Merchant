using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Active
{
    class TravelEvent
    {
        int id;
        int eID;
        List<string> text;
        List<Vector2> textPos;
        int percentage;
        Rectangle rectMessage;
        public TravelEvent(int id, int eID, List<string> text, int percentage)
        {
            this.id = id;
            this.eID = eID;
            this.text = new List<string>(text);
            textPos = new List<Vector2>();
            for (int i = 0; i < text.Count; i++)
            {
                textPos.Add(new Vector2(450,250+i*50));
            }
            this.percentage = percentage;
            rectMessage = new Rectangle(400,120,1100,750);
        }

        public int Percentage { get => percentage;}
        public int EID { get => eID;}
        public int Id { get => id;}
        public List<string> Text { get => text;}

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(TextureManager.texBox, rectMessage, Color.LightGray);
            for (int i = 0; i < text.Count; i++)
            {
                sb.DrawString(TextureManager.font24,text[i],textPos[i],Color.Black);
            }            
        }
    }
}
