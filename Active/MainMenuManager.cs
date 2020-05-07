using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Active
{
    static class MainMenuManager
    {
        static Button newGame;
        static Button loadGame;
        static Button exitGame;

        static Rectangle logo;

        static public void Init()
        {
            newGame = new Button(430, 900, 260, 120, "new", "New Game", TextureManager.texButton);
            loadGame = new Button(830, 900, 260, 120, "load", "Load Game", TextureManager.texButton);
            exitGame = new Button(1230, 900, 260, 120, "exit", "Exit Game", TextureManager.texButton);
            logo = new Rectangle(460, 150, 1000, 600);
        }

        static public bool CheckNewGame()
        {
            if (newGame.Click())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool CheckLoadGame()
        {
            if (loadGame.Click())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool CheckExitGame()
        {
            if (exitGame.Click())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public void Draw(SpriteBatch spritebatch)
        {
            newGame.Draw(spritebatch);
            loadGame.Draw(spritebatch);
            exitGame.Draw(spritebatch);

            spritebatch.Draw(TextureManager.texWhite, logo, Color.Red);
        }
    }
}
