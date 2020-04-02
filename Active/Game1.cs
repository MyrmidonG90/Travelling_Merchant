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

        #region pending removal to mapModule
        City[] cities = new City[3];
        Button[] buttons = new Button[3];
        #endregion

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
            itemCreator = new ItemCreator();

            gameState = GameState.InventoryMenu;

            cityMeny = new CityMeny();

            playerInventoryModule = new PlayerInventoryModule(new Inventory(100, new List<Item>()), itemCreator);

            cityMeny.InventoryButton = new Button(70, 920, 230, 120, TextureManager.WhiteTex);
            cityMeny.TradeButton = new Button(420, 920, 230, 120, TextureManager.WhiteTex);
            cityMeny.MapButton = new Button(1620, 920, 230, 120, TextureManager.WhiteTex);

            #region detta ska flyttas in i en egen manager (mapModule förslagsvis)
            //funkar inte då data filen inte har följt med i commiten

            //StreamReader sr = new StreamReader("cityNames.txt");
            //int counter = 0;
            //while (!sr.EndOfStream)
            //{

            //    string tempName = sr.ReadLine();
            //    string tempInfo = sr.ReadLine();
            //    string tempCord = sr.ReadLine();

            //    string[] tempCord2 = tempCord.Split(',');

            //    Vector2 cord = new Vector2(int.Parse(tempCord2[0]), int.Parse(tempCord2[1]));

            //    cities[counter] = new City(tempName, tempInfo, cord);
            //    buttons[counter] = new Button((int)cord.X, (int)cord.Y, 100, 100, tempName, TextureManager.texBox);
            //    counter++;
            //}
            #endregion
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KMReader.Update();


            base.Update(gameTime);

            foreach (City city in cities)

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


            foreach (Button button in buttons)



            if (gameState == GameState.CityMenu)
            {
                InventoryButton.Draw(spriteBatch);
                TradeButton.Draw(spriteBatch);
                MapButton.Draw(spriteBatch);

                cityMeny.Draw(spriteBatch);
            }
            else if (gameState == GameState.MapMenu)
            {

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
