﻿using System;
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
        static int playerAvatarSelect;
        static int startingSkill;
        static int confirmedAvatar;

        static Button nextAvatar;
        static Button prevAvatar;
        static Button backToMain;
        static Button nextSkill;
        static Button prevSkill;
        static Button startGame;

        static Rectangle creationBox;

        static Vector2 characterPos = new Vector2(430, 200);
        static Vector2 skillPos = new Vector2(1315, 350);


        static public void Init()
        {
            playerAvatarSelect = 1;
            startingSkill = 1;
            confirmedAvatar = 1;

            startGame = new Button(830, 900, 260, 120, "start", "Start Game", TextureManager.texButton);
            nextAvatar = new Button(570, 650, 150, 150, "next", "Next", TextureManager.texWhite);
            prevAvatar = new Button(370, 650, 150, 150, "prev", "Prev", TextureManager.texWhite);
            nextSkill = new Button(1400, 650, 150, 150, "next", "Next", TextureManager.texWhite);
            prevSkill = new Button(1200, 650, 150, 150, "next", "Next", TextureManager.texWhite);
            backToMain = new Button(430, 900, 260, 120, "back", "Back", TextureManager.texButton);

            creationBox = new Rectangle(310, 150, 1300, 700);
        }

        static public void Update()
        {
            if (nextAvatar.LeftClick())
            {
                playerAvatarSelect++;
            }
            if (prevAvatar.LeftClick())
            {
                playerAvatarSelect--;
            }
            if (nextSkill.LeftClick())
            {
                startingSkill++;
            }
            if (prevSkill.LeftClick())
            {
                startingSkill--;
            }

            if (playerAvatarSelect <= 0)
            {
                playerAvatarSelect = 3;
            }
            else if (playerAvatarSelect >= 4)
            {
                playerAvatarSelect = 1;
            }

            if (startingSkill <= 0)
            {
                startingSkill = 3;
            }
            else if (startingSkill >= 4)
            {
                startingSkill = 1;
            }

        }


        static public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(TextureManager.texWhite, creationBox, Color.Gray);
            startGame.Draw(spriteBatch);
            nextAvatar.Draw(spriteBatch);
            prevAvatar.Draw(spriteBatch);
            nextSkill.Draw(spriteBatch);
            prevSkill.Draw(spriteBatch);
            backToMain.Draw(spriteBatch);

            if(playerAvatarSelect == 1)
            {
                spriteBatch.Draw(TextureManager.texPCavatar1, characterPos, Color.White);
            }
            else if (playerAvatarSelect == 2)
            {
                spriteBatch.Draw(TextureManager.texPCavatar2, characterPos, Color.White);
            }
            else if (playerAvatarSelect == 3)
            {
                spriteBatch.Draw(TextureManager.texPCavatar3, characterPos, Color.White);
            }

            if (startingSkill == 1)
            {
                spriteBatch.Draw(TextureManager.texIconWisdom, skillPos, Color.White);
            }
            else if (startingSkill == 2)
            {
                spriteBatch.Draw(TextureManager.texIconIntimidation, skillPos, Color.White);
            }
            else if (startingSkill == 3)
            {
                spriteBatch.Draw(TextureManager.texIconPersuasion, skillPos, Color.White);
            }
        }

        static public bool CheckStartGame()
        {
            if (startGame.LeftClick())
            {
                if(startingSkill == 1)
                {
                    Player.AddXP("Wisdom", 100);
                }
                else if (startingSkill == 2)
                {
                    Player.AddXP("Intimidation", 100);
                }
                else if (startingSkill == 3)
                {
                    Player.AddXP("Persuasion", 100);
                }

                confirmedAvatar = playerAvatarSelect;
                playerAvatarSelect = 1;
                startingSkill = 1;

                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool CheckBack()
        {
            if (backToMain.LeftClick())
            {
                playerAvatarSelect = 1;
                startingSkill = 1;
                return true;
            }
            else
            {
                return false;
            }
        }

        static public int ConfirmedAvatar
        {
            get => confirmedAvatar;
            set => confirmedAvatar = value;
        }

    }
}