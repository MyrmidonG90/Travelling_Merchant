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
        List<Button> btnOptions;
        Rectangle rectMessage;
        public int Id { get => id; set => id = value; }
        int counter, click;
        bool found;
        /* Encounters.txt
         int id
         string Val1|string Val2|string Val3
             
             */
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
                while (counter < btnOptions.Count && found == false)
                {
                    if (KMReader.ClickRectangle(btnOptions[counter].HitBox) == true)
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
            for (int i = 0; i < btnOptions.Count; i++)
            {
                sb.Draw(TextureManager.texBox, btnOptions[i].HitBox,Color.Gray);
            }
            
        }
    }
}
