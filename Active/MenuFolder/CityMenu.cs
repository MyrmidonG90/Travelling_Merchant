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

namespace Active
{
    class CityMenu
    {
        Button inventoryButton;
        Button tradeButton;
        Button mapButton;
        Button nextTurnButton;
        Button infoButton;

        string currentCity;
        string currentCityInfo;

        bool activeEvent;
        string eventDes;

        RollText loseMoneyText;

        int loseMoneyPerTurn;
        int date;
        int oldDate;

        public CityMenu()
        {
            inventoryButton = new Button(60, 920, 260, 120, "inv", "Inventory", TextureManager.texButton);
            tradeButton = new Button(400, 920, 260, 120, "trade", "Trade", TextureManager.texButton);
            nextTurnButton = new Button(1260, 920, 260, 120, "turn", "Next Turn", TextureManager.texButton);
            mapButton = new Button(1600, 920, 260, 120, "map", "Map", TextureManager.texButton);
            infoButton = new Button(830, 920, 260, 120, "info", "Info", TextureManager.texButton);
            loseMoneyText = new RollText(5,500);
            loseMoneyPerTurn = 5;
            string inputLoseMoneyText = "-" + loseMoneyPerTurn + "GP"; 
            loseMoneyText.AddText(inputLoseMoneyText, new Vector2(1260+tradeButton.HitBox.Width/2-loseMoneyText.Font.MeasureString(inputLoseMoneyText).X/2,920 - loseMoneyText.Font.MeasureString(inputLoseMoneyText).Y));

            
        }

        public void Update(GameTime gameTime)
        {
            if (nextTurnButton.LeftClick())
            {                
                Calendar.AddDays(1);
                Player.Inventory.Money -= 5;
                loseMoneyText.Reset();
                loseMoneyText.Start();
            }

            date = Calendar.TotalDays;
            if (loseMoneyText.OnGoing)
            {
                loseMoneyText.MoveTextVertical(gameTime.ElapsedGameTime.TotalMilliseconds);
            }

            bool check = false;
            foreach (WorldEvent tempEvent in WorldEventManager.ActiveEvents)
            {
                foreach (string tempTarget in tempEvent.Target)
                {
                    if (tempTarget == currentCity)
                    {
                        activeEvent = true;
                        eventDes = tempEvent.EventDes;
                        check = true;
                    }
                }
            }
            if (!check)
            {
                activeEvent = false;
                eventDes = "";
            }
            if (infoButton.LeftClick())
            {
                CityInfoMenu.Active = !CityInfoMenu.Active;
                CityInfoMenu.Selected = currentCity;
            }
        }

        public bool CheckInvButton()
        {
            if (inventoryButton.LeftClick())
            {
                return true;
            }
            return false;
        }
        public bool CheckTradeButton()
        {
            if (tradeButton.LeftClick())
            {
                return true;
            }
            return false;
        }
        public bool CheckMapButton()
        {
            if (mapButton.LeftClick())
            {
                return true;
            }
            return false;
        }
        public void Draw(SpriteBatch spriteBatch, City[] cities)
        {
            if (currentCity != Player.Location)
            {
                foreach (City tempCity in cities)
                {
                    if (tempCity.Name == Player.Location)
                    {
                        currentCity = tempCity.Name;
                        currentCityInfo = tempCity.Information;
                    }
                }
            }

            inventoryButton.Draw(spriteBatch);
            tradeButton.Draw(spriteBatch);
            mapButton.Draw(spriteBatch);
            nextTurnButton.Draw(spriteBatch);
            infoButton.Draw(spriteBatch);
            spriteBatch.DrawString(TextureManager.font64, currentCity, new Vector2(30, 100), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            //spriteBatch.DrawString(TextureManager.font32, currentCityInfo, new Vector2(40, 180), Color.Black, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

            if (activeEvent)
            {
                spriteBatch.DrawString(TextureManager.font32, eventDes, new Vector2(1300, 180), Color.Black);
            }
            if (loseMoneyText.OnGoing)
            {
                loseMoneyText.Draw(spriteBatch);
            }
        }
    }
}
