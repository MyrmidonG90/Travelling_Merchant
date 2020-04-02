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

        WorldMapMeny worldMapMeny;

        CityMeny cityMeny;

        #region pending removal to CityMeny
        public Button InventoryButton;
        public Button TradeButton;
        public Button MapButton;
        #endregion

        enum GameState
        {
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

            gameState = GameState.MapMenu;

            cityMeny = new CityMeny();
            worldMapMeny = new WorldMapMeny();
            worldMapMeny.LoadCities();

            playerInventoryModule = new PlayerInventoryModule(new Inventory(100, new List<Item>()));

            InventoryButton = new Button(70, 920, 230, 120, TextureManager.WhiteTex);
            TradeButton = new Button(420, 920, 230, 120, TextureManager.WhiteTex);
            MapButton = new Button(1620, 920, 230, 120, TextureManager.WhiteTex);
           
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KMReader.Update();

            if (gameState == GameState.CityMenu)
            {

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
                playerInventoryModule.Update(gameTime);
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

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
