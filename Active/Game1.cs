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
        public SpriteFont font;
        WorldModule worldModule;
        PlayerInventoryModule playerInventoryModule;

        //Button Proof-of-Concept (kan tas bort utan risk)

        CityMeny cityMeny;

<<<<<<< Updated upstream
        Button test;
        public Button InventoryButton;
        public Button TradeButton;
        public Button MapButton;
        int temp;
        //===============

        City city;
        City[] cities = new City[2];
        Button[] buttons = new Button[2];


        string temp;


=======
        #region pending removal to CityMeny

        #endregion

        public enum GameState
        {
            CityMenu,
            MapMenu,
            TradeMenu,
            InventoryMenu,
        }

       public GameState gameState;
>>>>>>> Stashed changes

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

<<<<<<< Updated upstream
            worldModule = new WorldModule();
=======
            gameState = GameState.CityMenu;
>>>>>>> Stashed changes


            cityMeny.InventoryButton = new Button(70, 920, 230, 120, TextureManager.WhiteTex);
            cityMeny.TradeButton = new Button(420, 920, 230, 120, TextureManager.WhiteTex);
            cityMeny.MapButton = new Button(1620, 920, 230, 120, TextureManager.WhiteTex);

            Inventory newInventory = new Inventory(100, new List<Item>());
            playerInventoryModule = new PlayerInventoryModule(newInventory);


            worldModule = new WorldModule();


            font = Content.Load<SpriteFont>("File");

            City city = new City("test", "hej", new Vector2(2,2));


            buttons[0] = new Button((int)city.carrotTownCords.X, (int)city.carrotTownCords.Y, 100, 100, "Carrot Town");
            buttons[1] = new Button((int)city.steelVilleCords.X, (int)city.steelVilleCords.Y, 100, 100, "Steelville");


            cities[0] = new City("Carrot Town", city.carrotTownInfo, city.carrotTownCords);
            cities[1] = new City("Steelville", city.steelVilleInfo, city.steelVilleCords);



        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            oldMouseState = newMouseState;
            newMouseState = Mouse.GetState();
            
            worldModule.Update(gameTime);
            
            KMReader.Update();

<<<<<<< Updated upstream
            base.Update(gameTime);
            
            foreach (City city in cities)
=======
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
>>>>>>> Stashed changes

            {
                foreach (Button button in buttons)
                {
                    if (button.Click(newMouseState, oldMouseState) && button.name == city.name)
                    {
                        temp = city.name;
                    }
                }
            }

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            worldModule.Draw(spriteBatch);
            InventoryButton.Draw(spriteBatch);
            TradeButton.Draw(spriteBatch);
            MapButton.Draw(spriteBatch);
            Window.Title = temp.ToString();

<<<<<<< Updated upstream

            foreach (Button button in buttons)
=======
            if (gameState == GameState.CityMenu)
            {
                cityMeny.Draw(spriteBatch);
            }
            else if (gameState == GameState.MapMenu)
>>>>>>> Stashed changes
            {
                button.Draw(spriteBatch);
            }

            Window.Title = temp;


            cityMeny.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
