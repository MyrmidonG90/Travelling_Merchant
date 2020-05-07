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
        int percentage;
        Rectangle rectMessage;
        public TravelEvent(int id, int eID, List<string> text, int percentage)
        {
            this.id = id;
            this.eID = eID;
            this.text = new List<string>(text);
            this.percentage = percentage;
            rectMessage = new Rectangle(0,0,500,500);
        }

        public int Percentage { get => percentage;}
        public int EID { get => eID;}
        public int Id { get => id;}
        public List<string> Text { get => text;}

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(TextureManager.texBox, rectMessage, Color.LightGray);
        }
    }
}
