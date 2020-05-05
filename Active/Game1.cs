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
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        PlayerInventoryModule playerInventoryModule;
        CityMenu cityMenu;
        TravelMenu travelMenu;
        WorldMapMenu worldMapMenu;

        Inventory activeInv;
        Inventory activePlayerInv;

        bool options;

        enum GameState
        {
            Debug,
            CityMenu,
            MapMenu,
            TradeMenu,
            InventoryMenu,
            TravelMenu,
            MainMenu,
        }

        GameState previousGameState2;
        GameState previousGameState;
        GameState gameState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;
            //graphics.IsFullScreen = true;
            //väldigt svårt att debugga i fullscreen så rekomenderar att lämna det av förutom när det behövs /My
            graphics.ApplyChanges();
            IsMouseVisible = true;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.LoadContent(Content);
            ItemCreator.LoadItemData();
            Player.Init();
            MainMenuManager.Init();
            OptionsMenu.Init();
            CityManager.Initialize();
            previousGameState2 = GameState.Debug;
            previousGameState = GameState.Debug;
            gameState = GameState.MainMenu;

            options = false;
            activeInv = new Inventory(100);

            cityMenu = new CityMenu();
            worldMapMenu = new WorldMapMenu();
            worldMapMenu.LoadCities();
            playerInventoryModule = new PlayerInventoryModule();
            travelMenu = new TravelMenu();

            ModifierManager.LoadCityAndCategoryLists();
            Calendar.PrepareCalendar();
        }

        //========================================================================
        //OM DU SKA ÄNDRA GAMESTATE ANVÄND METODERNA SOM FINNS EFTER DRAW METODEN
        //========================================================================
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Back))
                Exit();

            KMReader.Update();
            Calendar.Update();

            if (gameState == GameState.CityMenu)
            {
                cityMenu.Update(gameTime);
                if (cityMenu.CheckInvButton())
                {
                    ChangeGameState(GameState.InventoryMenu);
                }
                if (cityMenu.CheckTradeButton())
                {
                    ChangeGameState(GameState.TradeMenu);
                    foreach (City tempCity in worldMapMenu.Cities)
                    {
                        if (tempCity.Name == Player.Location)
                        {
                            activeInv = tempCity.Inv;
                            activePlayerInv = Player.Inventory;
                        }
                    }
                    Trading.Initialize(activePlayerInv, activeInv);
                }
                if (cityMenu.CheckMapButton())
                {
                    ChangeGameState(GameState.MapMenu);
                }
            }
            else if (gameState == GameState.MapMenu)
            {
                worldMapMenu.Update(gameTime);

                string temp = worldMapMenu.CheckNewTravel();
                if (temp != null && travelMenu.TurnsLeft == 0)
                {
                    travelMenu.StartTravel(temp);
                    ChangeGameState(GameState.TravelMenu);
                }

                if (worldMapMenu.inventoryButton.Click())
                {
                    ChangeGameState(GameState.InventoryMenu);
                }

                if (worldMapMenu.returnButton.Click())
                {
                    RevertGameState();
                }
            }
            else if (gameState == GameState.TradeMenu)
            {
                if (Trading.Update(ref activePlayerInv, ref activeInv) == true)
                {
                    RevertGameState();
                    foreach (City tempCity in worldMapMenu.Cities)
                    {
                        if (tempCity.Name == Player.Location)
                        {
                            tempCity.Inv = activeInv;
                            Player.Inventory = activePlayerInv;
                            tempCity.Traded = true;
                        }
                    }
                }
            }
            else if (gameState == GameState.InventoryMenu)
            {
                playerInventoryModule.Update(gameTime);
                if (playerInventoryModule.CheckExit())
                {
                    RevertGameState();
                }
            }
            else if (gameState == GameState.TravelMenu)
            {
                if (travelMenu.Update(gameTime))
                {
                    Player.Location = travelMenu.Destination;
                    ChangeGameState(GameState.CityMenu);
                }
                if (travelMenu.CheckInvbutton())
                {
                    ChangeGameState(GameState.InventoryMenu);
                }
                if (travelMenu.CheckMapButton())
                {
                    ChangeGameState(GameState.MapMenu);
                }
            }
            else if (gameState == GameState.MainMenu)
            {
                if (MainMenuManager.CheckNewGame())
                {
                    ChangeGameState(GameState.CityMenu);
                }
                if (MainMenuManager.CheckLoadGame())
                {
                    LoadSave();
                }
                if (MainMenuManager.CheckExitGame())
                {
                    Exit();
                }
            }
            else if (gameState == GameState.Debug)
            {
                if (KMReader.prevKeyState.IsKeyUp(Keys.F1) && KMReader.keyState.IsKeyDown(Keys.F1))
                {
                    ChangeGameState(GameState.CityMenu);
                }
                if (KMReader.prevKeyState.IsKeyUp(Keys.F2) && KMReader.keyState.IsKeyDown(Keys.F2))
                {
                    ChangeGameState(GameState.MapMenu);
                }
                if (KMReader.prevKeyState.IsKeyUp(Keys.F3) && KMReader.keyState.IsKeyDown(Keys.F3))
                {
                    ChangeGameState(GameState.InventoryMenu);
                }
                if (KMReader.prevKeyState.IsKeyUp(Keys.F4) && KMReader.keyState.IsKeyDown(Keys.F4))
                {
                    ChangeGameState(GameState.TradeMenu);
                }
                if (KMReader.prevKeyState.IsKeyUp(Keys.F5) && KMReader.keyState.IsKeyDown(Keys.F5))
                {
                    ChangeGameState(GameState.TravelMenu);
                }
            }

            if (KMReader.prevKeyState.IsKeyUp(Keys.F6) && KMReader.keyState.IsKeyDown(Keys.F6))
            {
                ChangeGameState(GameState.Debug);
            }
            if (KMReader.prevKeyState.IsKeyUp(Keys.F11) && KMReader.keyState.IsKeyDown(Keys.F11))
            {
                LoadSave();
            }
            if (KMReader.prevKeyState.IsKeyUp(Keys.F12) && KMReader.keyState.IsKeyDown(Keys.F12))
            {
                SaveGame();
            }

            if (options)
            {
                if (OptionsMenu.CheckMainMenu())
                {
                    ChangeGameState(GameState.MainMenu);
                    options = false;
                }
                else if (OptionsMenu.CheckLoadGame())
                {
                    LoadSave();
                }
                else if (OptionsMenu.CheckSaveGame())
                {
                    SaveGame();
                }
                else if (OptionsMenu.CheckFullscreen()) //funkar inte framtida jag ska försöka fixa /My
                {
                    graphics.IsFullScreen = !graphics.IsFullScreen;
                    graphics.ApplyChanges();
                }
                if (OptionsMenu.Update())
                {
                    options = false;
                }
            }

            if (OptionsMenu.CheckMenuToggle())
            {
                options = !options;
            }

            Player.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (gameState == GameState.CityMenu)
            {
                cityMenu.Draw(spriteBatch, worldMapMenu.Cities);
            }
            else if (gameState == GameState.MapMenu)
            {
                worldMapMenu.Draw(spriteBatch);               
            }
            else if (gameState == GameState.TradeMenu)
            {
                Trading.Draw(spriteBatch);
            }
            else if (gameState == GameState.InventoryMenu)
            {
                playerInventoryModule.Draw(spriteBatch);
            }
            else if (gameState == GameState.TravelMenu)
            {
                travelMenu.Draw(spriteBatch);
            }
            else if (gameState == GameState.MainMenu)
            {
                MainMenuManager.Draw(spriteBatch);
            }
            else if (gameState == GameState.Debug)
            {
                spriteBatch.DrawString(TextureManager.fontInventory, "Press F1 for City Menu, F2 for Map Menu, F3 for Inv. Menu,\nF4 for Trading Menu or F5 for Travelling Menu \nand you can always press F6 to return here\nF11 loads and F12 saves", new Vector2(200, 200), Color.White);
            }
            if (gameState != GameState.MainMenu)
            {
                Calendar.Draw(spriteBatch);
            }
            OptionsMenu.Draw(spriteBatch, options);          

            spriteBatch.End();

            Window.Title = "Press F6 for debug menu          " + Player.SkillLevels[0].ToString() + Player.SkillLevels[1].ToString() + Player.SkillLevels[2].ToString() + "    " + OptionsMenu.selectedSkill;

            base.Draw(gameTime);
        }

        private void ChangeGameState(GameState newGameState)
        {
            previousGameState2 = previousGameState;
            previousGameState = gameState;
            gameState = newGameState;
        }

        private void CleanGameState(GameState newGameState)
        {
            previousGameState2 = newGameState;
            previousGameState = newGameState;
            gameState = newGameState;
        }

        private void RevertGameState()
        {
            gameState = previousGameState;
            previousGameState = previousGameState2;
        }

        private void SaveGame()
        {
            SaveModule.GenerateSave(Player.Inventory, Player.Location, travelMenu, gameState.ToString());
        }

        private void LoadSave()
        {
            string[] temp = SaveModule.LoadSave();

            if (temp == null)
            {
                return;
            }

            if (temp[0] == GameState.CityMenu.ToString())
            {
                CleanGameState(GameState.CityMenu);
            }
            else if (temp[0] == GameState.InventoryMenu.ToString())
            {
                CleanGameState(GameState.InventoryMenu);
            }
            else if (temp[0] == GameState.MapMenu.ToString())
            {
                CleanGameState(GameState.MapMenu);
            }
            else if (temp[0] == GameState.TravelMenu.ToString())
            {
                if (temp[1] != null)
                {
                    travelMenu.TurnsLeft = int.Parse(temp[1]);
                    travelMenu.Destination = temp[2];
                    CleanGameState(GameState.TravelMenu);
                }
                else
                {
                    CleanGameState(GameState.CityMenu);
                }
            }
            else
            {
                CleanGameState(GameState.CityMenu);
            }
        }
    }
}
