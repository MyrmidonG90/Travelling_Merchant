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

namespace Active
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        PlayerInventoryModule playerInventoryModule;
        CityMeny cityMeny;
        TravelMenu travelMenu;

        WorldMapMenu worldMapMenu;

        enum GameState
        {
            Debug,
            CityMenu,
            MapMenu,
            TradeMenu,
            InventoryMenu,
            TravelMenu
        }

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

            previousGameState = GameState.Debug;
            gameState = GameState.Debug;

            cityMeny = new CityMeny();
            worldMapMenu = new WorldMapMenu();
            worldMapMenu.LoadCities();
            playerInventoryModule = new PlayerInventoryModule();         
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Back))
                Exit();

            KMReader.Update();

            if (gameState == GameState.CityMenu)
            {
                if (cityMeny.CheckInvButton())
                {
                    previousGameState = gameState;
                    gameState = GameState.InventoryMenu;
                }
                if (cityMeny.CheckTradeButton())
                {
                    previousGameState = gameState;
                    gameState = GameState.TradeMenu;
                }
                if (cityMeny.CheckMapButton())
                {
                    previousGameState = gameState;
                    gameState = GameState.MapMenu;
                }
            }
            else if (gameState == GameState.MapMenu)
            {
                worldMapMenu.Update(gameTime);
            }
            else if (gameState == GameState.TradeMenu)
            {

            }
            else if (gameState == GameState.InventoryMenu)
            {
                playerInventoryModule.Update(gameTime);
                if (playerInventoryModule.CheckExit())
                {
                    gameState = previousGameState;
                }
            }
            else if (gameState == GameState.TravelMenu)
            {
                travelMenu.Update(gameTime);
            }
            else if (gameState == GameState.Debug)
            {
                if (KMReader.prevKeyState.IsKeyUp(Keys.F1) && KMReader.keyState.IsKeyDown(Keys.F1))
                {
                    previousGameState = gameState;
                    gameState = GameState.CityMenu;
                }
                if (KMReader.prevKeyState.IsKeyUp(Keys.F2) && KMReader.keyState.IsKeyDown(Keys.F2))
                {
                    previousGameState = gameState;
                    gameState = GameState.MapMenu;
                }
                if (KMReader.prevKeyState.IsKeyUp(Keys.F3) && KMReader.keyState.IsKeyDown(Keys.F3))
                {
                    previousGameState = gameState;
                    gameState = GameState.InventoryMenu;
                }
                if (KMReader.prevKeyState.IsKeyUp(Keys.F4) && KMReader.keyState.IsKeyDown(Keys.F4))
                {
                    previousGameState = gameState;
                    gameState = GameState.TradeMenu;
                }
                if (KMReader.prevKeyState.IsKeyUp(Keys.F4) && KMReader.keyState.IsKeyDown(Keys.F5))
                {
                    gameState = GameState.TravelMenu;
                }
            }

            if (KMReader.prevKeyState.IsKeyUp(Keys.F5) && KMReader.keyState.IsKeyDown(Keys.F5))
            {
                previousGameState = gameState;
                gameState = GameState.Debug;
            }
            if (KMReader.prevKeyState.IsKeyUp(Keys.F11) && KMReader.keyState.IsKeyDown(Keys.F11))
            {
                playerInventoryModule.Inventory = SaveModule.LoadSave();
            }
            if (KMReader.prevKeyState.IsKeyUp(Keys.F12) && KMReader.keyState.IsKeyDown(Keys.F12))
            {
                SaveModule.GenerateSave(playerInventoryModule.Inventory);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (gameState == GameState.CityMenu)
            {
                cityMeny.Draw(spriteBatch);
            }
            else if (gameState == GameState.MapMenu)
            {

                worldMapMenu.Draw(spriteBatch);

            }
            else if (gameState == GameState.TradeMenu)
            {
                
            }
            else if (gameState == GameState.InventoryMenu)
            {
                playerInventoryModule.Draw(spriteBatch);
            }
            else if (gameState == GameState.TravelMenu)
            {
                travelMenu.Draw(spriteBatch);
            }
            else if (gameState == GameState.Debug)
            {
                spriteBatch.DrawString(TextureManager.fontInventory, "Press F1 for City Menu, F2 for Travel/Map Menu,\nF3 for Inv. Menu or F4 for Trading Menu and you can always press F5 to return here", new Vector2(200, 200), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
