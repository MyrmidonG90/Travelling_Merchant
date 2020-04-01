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
using System.IO;

namespace Active
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public SpriteFont font;
        WorldModule worldModule;

        
        MouseState newMouseState;
        MouseState oldMouseState;

        //Button Proof-of-Concept (kan tas bort utan risk)
        City[] cities = new City[3];
        Button[] buttons = new Button[3];

        PlayerInventoryModule playerInventoryModule;

        //Button Proof-of-Concept (kan tas bort utan risk)

        CityMeny cityMeny;

        Button test;
        public Button InventoryButton;
        public Button TradeButton;
        public Button MapButton;
        int temp;
        //===============

        //City city;
        //City[] cities = new City[2];
        //Button[] buttons = new Button[2];



        //string temp;



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



            InventoryButton = new Button(70, 920, 230, 120, TextureManager.WhiteTex);
            TradeButton = new Button(420, 920, 230, 120, TextureManager.WhiteTex);
            MapButton = new Button(1620, 920, 230, 120, TextureManager.WhiteTex);

            Inventory newInventory = new Inventory(100, new List<Item>());
            playerInventoryModule = new PlayerInventoryModule(newInventory);


            worldModule = new WorldModule();


            font = Content.Load<SpriteFont>("File");

            City city = new City("test", "hej", new Vector2(2,2));


            //StreamReader sr = new StreamReader("cityNames.txt");
            //int counter = 0;
            //while (!sr.EndOfStream)
            //{

            //    string tempName = sr.ReadLine();
            //    string tempInfo = sr.ReadLine();
            //    string tempCord = sr.ReadLine();

            //    string[] tempCord2 = tempCord.Split(',');

            //    Vector2 cord = new Vector2(int.Parse(tempCord2[0]), int.Parse(tempCord2[1]));

            //    //cities[counter] = new City(tempName, tempInfo, cord);
            //    //buttons[counter] = new Button((int)cord.X, (int)cord.Y, 100, 100, tempName, TextureManager.texBox);
            //    counter++;

            //}
            
            

            



        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            
            oldMouseState = newMouseState;
            newMouseState = Mouse.GetState();
            
            worldModule.Update(gameTime);
            
            KMReader.Update();

            

            //foreach (City city in cities)

            //{
            //    foreach (Button button in buttons)
            //    {
            //        if (button.Click() && button.name == city.name)
            //        {
            //            temp = city.name;
            //        }
            //    }
            //}

            base.Update(gameTime);


        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            worldModule.Draw(spriteBatch);
            //InventoryButton.Draw(spriteBatch);
            //TradeButton.Draw(spriteBatch);
            //MapButton.Draw(spriteBatch);
            //Window.Title = temp.ToString();


            //foreach (Button button in buttons)
            //{
            //    button.Draw(spriteBatch);
            //}

            //Window.Title = temp;

            playerInventoryModule.Draw(spriteBatch);
            //cityMeny.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
