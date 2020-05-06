using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Active
{
    class Encounter
    {
        int id;
        List<Rectangle> rectsOptions;
        Rectangle rectMessage;
        public int Id { get => id; set => id = value; }
        int counter, click;
        bool found;

        public void Update()
        {
            
            
        }
        int CheckClick()
        {
            click = -1;
            if (KMReader.MouseClick())
            {
                found = false;
                counter = 0;
                while (counter < rectsOptions.Count && found == false)
                {
                    if (KMReader.ClickRectangle(rectsOptions[counter]) == true)
                    {
                        found = true;
                        click = counter;
                    }
                    else
                    {
                        ++counter;
                    }
                }
            }
            return click;
        }

        

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(TextureManager.texBox,rectMessage,Color.LightGray);
            for (int i = 0; i < rectsOptions.Count; i++)
            {
                sb.Draw(TextureManager.texBox,rectsOptions[i],Color.Gray);
            }
            
        }
    }
}
