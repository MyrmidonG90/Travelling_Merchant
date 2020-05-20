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
        
        public int Id { get => id; set => id = value; }
        public bool OccuredDuringTravel { get => occuredDuringTravel; set => occuredDuringTravel = value; }

        int counter, click;
        bool found;
        bool occuredDuringTravel;
        /* Encounters.txt
         int id
         string Val1|string Val2|string Val3
             
             */

        public Encounter(int id, List<string> optionTexts)
        {
            this.id = id;
            btnOptions = new List<Button>();
            for (int i = 0; i < optionTexts.Count; i++)
            {
                btnOptions.Add(new Button(560+270*i,700,260,120,"something",optionTexts[i],TextureManager.texBox));                
            }
            occuredDuringTravel = false;
        }

        public int Update()
        {
            return CheckClick();
        }

        int CheckClick()
        {
            click = -1;
            if (KMReader.LeftMouseClick())
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
            for (int i = 0; i < btnOptions.Count; i++)
            {                
                foreach (var item in btnOptions)
                {
                    item.Draw(sb);
                }
            }            
        }
    }
}
