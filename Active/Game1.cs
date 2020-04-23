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
        CityMenu cityMenu;
        TravelMenu travelMenu;
        Inventory inv1, inv2;
        WorldMapMenu worldMapMenu;
        ModifierManager modifierManager;

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
            Player.Init();

            previousGameState = GameState.Debug;
            gameState = GameState.Debug;


            inv1 = new Inventory(100);
            inv2 = new Inventory(200);
            inv1.AddItem(ItemCreator.CreateItem(0,20));
            inv1.AddItem(ItemCreator.CreateItem(1, 20));
            inv2.AddItem(ItemCreator.CreateItem(2, 5));
            Trading.Initialize(inv1,inv2);
            cityMenu = new CityMenu();
            worldMapMenu = new WorldMapMenu();
            worldMapMenu.LoadCities();
            playerInventoryModule = new PlayerInventoryModule();
            travelMenu = new TravelMenu();
            modifierManager = new ModifierManager();
            modifierManager.LoadCityAndItemLists();

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Back))
                Exit();

            KMReader.Update();

            if (gameState == GameState.CityMenu)
            {
                if (cityMenu.CheckInvButton())
                {
                    previousGameState = gameState;
                    gameState = GameState.InventoryMenu;
                }
                if (cityMenu.CheckTradeButton())
                {
                    previousGameState = gameState;
                    gameState = GameState.TradeMenu;
                }
                if (cityMenu.CheckMapButton())
                {
                    previousGameState = gameState;
                    gameState = GameState.MapMenu;
                }
            }
            else if (gameState == GameState.MapMenu)
            {
                worldMapMenu.Update(gameTime);

                string temp = worldMapMenu.CheckNewTravel();
                if (temp != null)
                {
                    travelMenu.StartTravel(temp);
                    gameState = GameState.TravelMenu;
                }

                if (worldMapMenu.inventoryButton.Click())
                {
                    previousGameState = gameState;
                    gameState = GameState.InventoryMenu;
                }

                if (worldMapMenu.returnButton.Click())
                {
                    gameState = GameState.CityMenu;
                }
            }
            else if (gameState == GameState.TradeMenu)
            {
                if (Trading.Update(ref inv1, ref inv2) == true)
                {
                    gameState = GameState.Debug;
                }
                
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
                if (travelMenu.Update(gameTime))
                {
                    Player.Location = travelMenu.Destination;
                    gameState = GameState.CityMenu;
                }
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
                if (KMReader.prevKeyState.IsKeyUp(Keys.F5) && KMReader.keyState.IsKeyDown(Keys.F5))
                {
                    gameState = GameState.TravelMenu;
                }
            }

            if (KMReader.prevKeyState.IsKeyUp(Keys.F6) && KMReader.keyState.IsKeyDown(Keys.F6))
            {
                previousGameState = gameState;
                gameState = GameState.Debug;
            }
            if (KMReader.prevKeyState.IsKeyUp(Keys.F11) && KMReader.keyState.IsKeyDown(Keys.F11))
            {
                Player.Inventory = SaveModule.LoadSave();
            }
            if (KMReader.prevKeyState.IsKeyUp(Keys.F12) && KMReader.keyState.IsKeyDown(Keys.F12))
            {
                SaveModule.GenerateSave(Player.Inventory);
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
            else if (gameState == GameState.Debug)
            {
                spriteBatch.DrawString(TextureManager.fontInventory, "Press F1 for City Menu, F2 for Travel/Map Menu,\nF3 for Inv. Menu, F4 for Trading Menu or F5 for Travelling Menu \nand you can always press F6 to return here", new Vector2(200, 200), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
