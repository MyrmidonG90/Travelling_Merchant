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
        Button test;
        Button InventoryButton;
        Button Trade;
        Button Map;
        int temp;
        //===============

        City city;
        City[] cities = new City[2];
        Button[] buttons = new Button[2];


        string temp;



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

            worldModule = new WorldModule();

            Inventory newInventory = new Inventory(100, new List<Item>());
            playerInventoryModule = new PlayerInventoryModule(newInventory);


            worldModule = new WorldModule();

            InventoryButton = new Button(70, 920, 230, 120);
            Trade = new Button(420, 920, 230, 120);
            Map = new Button(1620, 920, 230, 120);
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

            base.Update(gameTime);
            


            if (InventoryButton.Click())
            {
                temp++;
            }
            base.Update(gameTime);
            if (Trade.Click())
            {
                temp++;
            }
            base.Update(gameTime);
            if (Map.Click())

            foreach (City city in cities)

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
            Trade.Draw(spriteBatch);
            Map.Draw(spriteBatch);
            Window.Title = temp.ToString();


            foreach (Button button in buttons)
            {
                button.Draw(spriteBatch);
            }

            Window.Title = temp;


            spriteBatch.DrawString(font, "CITY NAME: ", new Vector2(30, 50), Color.Black, 0, Vector2.Zero, 4, SpriteEffects.None, 1);
            spriteBatch.DrawString(font, "City Type: ", new Vector2(40, 120), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(font, "Population: ", new Vector2(40, 160), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(font, "Races: ", new Vector2(40, 200), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(font, "Kingdom: ", new Vector2(40, 240), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(font, "Resources: ", new Vector2(40, 280), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(font, "Inventory ", new Vector2(120, 962), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(font, "Trade ", new Vector2(495, 962), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.DrawString(font, "Map ", new Vector2(1700, 962), Color.Black, 0, Vector2.Zero, 2, SpriteEffects.None, 1);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
