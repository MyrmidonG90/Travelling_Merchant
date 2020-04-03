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
        ItemCreator itemCreator;
        Button test;


        WorldMapMenu worldMapMeny;


        #region pending removal to CityMeny
        public Button InventoryButton;
        public Button TradeButton;
        public Button MapButton;
        #endregion


        enum GameState
        {
            Debug,
            CityMenu,
            MapMenu,
            TradeMenu,
            InventoryMenu,
        }

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
            itemCreator = new ItemCreator();


            gameState = GameState.Debug;


            cityMeny = new CityMeny();
            worldMapMeny = new WorldMapMenu();
            worldMapMeny.LoadCities();

            playerInventoryModule = new PlayerInventoryModule(new Inventory(100, new List<Item>()), itemCreator);


            cityMeny.InventoryButton = new Button(70, 920, 230, 120, TextureManager.WhiteTex);
            cityMeny.TradeButton = new Button(420, 920, 230, 120, TextureManager.WhiteTex);
            cityMeny.MapButton = new Button(1620, 920, 230, 120, TextureManager.WhiteTex);


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KMReader.Update();

            if (gameState == GameState.CityMenu)
            {
                if (cityMeny.CheckInvButton())
                {
                    gameState = GameState.InventoryMenu;
                }
                if (cityMeny.CheckTradeButton())
                {
                    gameState = GameState.TradeMenu;
                }
                if (cityMeny.CheckMapButton())
                {
                    gameState = GameState.MapMenu;
                }
            }
            else if (gameState == GameState.MapMenu)
            {
                worldMapMeny.Update(gameTime);
            }
            else if (gameState == GameState.TradeMenu)
            {

            }
            else if (gameState == GameState.InventoryMenu)
            {
                playerInventoryModule.Update(gameTime, itemCreator);
            }
            else if (gameState == GameState.Debug)
            {
                if (KMReader.prevKeyState.IsKeyUp(Keys.F1) && KMReader.keyState.IsKeyDown(Keys.F1))
                {
                    gameState = GameState.CityMenu;
                }
                if (KMReader.prevKeyState.IsKeyUp(Keys.F2) && KMReader.keyState.IsKeyDown(Keys.F2))
                {
                    gameState = GameState.MapMenu;
                }
                if (KMReader.prevKeyState.IsKeyUp(Keys.F3) && KMReader.keyState.IsKeyDown(Keys.F3))
                {
                    gameState = GameState.InventoryMenu;
                }
                if (KMReader.prevKeyState.IsKeyUp(Keys.F4) && KMReader.keyState.IsKeyDown(Keys.F4))
                {
                    gameState = GameState.TradeMenu;
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            if (gameState == GameState.CityMenu)
            {
                InventoryButton.Draw(spriteBatch);
                TradeButton.Draw(spriteBatch);
                MapButton.Draw(spriteBatch);

                cityMeny.Draw(spriteBatch);
            }
            else if (gameState == GameState.MapMenu)
            {

                worldMapMeny.Draw(spriteBatch);

            }
            else if (gameState == GameState.TradeMenu)
            {
                
            }
            else if (gameState == GameState.InventoryMenu)
            {
                playerInventoryModule.Draw(spriteBatch);
            }
            else if (gameState == GameState.Debug)
            {
                spriteBatch.DrawString(TextureManager.fontInventory, "Press F1 for City Menu, F2 for Travel/Map Menu,\nF3 for Inv. Menu or F4 for Trading Menu", new Vector2(200, 200), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
