using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Active
{
    static class CharCreationMenu
    {
        static int playerAvatar;

        

        static Button nextAvatar;
        static Button prevAvatar;
        static Button backToMain;
        static Button nextSkill;
        static Button prevSkill;
        static Button startGame;

        static Rectangle creationBox;

        static Vector2 characterPos = new Vector2(430, 200);
        

        static public void Init()
        {
            playerAvatar = 3;

            startGame = new Button(830, 900, 260, 120, "start", "Start Game", TextureManager.texButton);
            nextAvatar = new Button(570, 650, 150, 150, "next", "Next", TextureManager.texWhite);
            prevAvatar = new Button(370, 650, 150, 150, "prev", "Prev", TextureManager.texWhite);
            nextSkill = new Button(1400, 650, 150, 150, "next", "Next", TextureManager.texWhite);
            prevSkill = new Button(1200, 650, 150, 150, "next", "Next", TextureManager.texWhite);

            creationBox = new Rectangle(310, 150, 1300, 700);
        }


        static public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.texWhite, creationBox, Color.Gray);
            startGame.Draw(spriteBatch);
            nextAvatar.Draw(spriteBatch);
            prevAvatar.Draw(spriteBatch);
            nextSkill.Draw(spriteBatch);
            prevSkill.Draw(spriteBatch);

            if(playerAvatar == 1)
            {
                spriteBatch.Draw(TextureManager.texPCavatar1, characterPos, Color.White);
            }
            else if (playerAvatar == 2)
            {
                spriteBatch.Draw(TextureManager.texPCavatar2, characterPos, Color.White);
            }
            else if (playerAvatar == 3)
            {
                spriteBatch.Draw(TextureManager.texPCavatar3, characterPos, Color.White);
            }
        }

        static public bool CheckStartGame()
        {
            if (startGame.LeftClick())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public bool CheckNextAvatar()
        {
            if (nextAvatar.LeftClick())
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        static public bool CheckPrevAvatar()
        {
            if (prevAvatar.LeftClick())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
