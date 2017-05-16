using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace SBFinailProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Color grass = new Color(94, 157, 30);

        StartMenu startscreen;
        GamePlay gameplay;
        TwoPlayerGame twoplayergame;
        HowToPlay howtoplay;
        Help help;
        About about;


        private void hideAllScenes()
        {
            GameScene gs = null;
            foreach (GameComponent item in Components)
            {
                if (item is GameScene)
                {
                    gs = (GameScene)item;
                    gs.hide();
                }
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 980;
            Content.RootDirectory = "Content";
        }
        

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            Stage.size = new Vector2(graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            startscreen = new StartMenu(this, spriteBatch);
            this.Components.Add(startscreen);

            gameplay = new GamePlay(this, spriteBatch);
            this.Components.Add(gameplay);

            twoplayergame = new TwoPlayerGame(this, spriteBatch);
            this.Components.Add(twoplayergame);

            howtoplay = new HowToPlay(this, spriteBatch);
            this.Components.Add(howtoplay);            

            help = new Help(this, spriteBatch);
            this.Components.Add(help);

            about = new About(this, spriteBatch);
            this.Components.Add(about);

            startscreen.show();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            int selectedIndex = 0;

            KeyboardState ks = Keyboard.GetState();

            if (startscreen.Enabled)
            {
                selectedIndex = startscreen.Menu.SelectedIndex;

                if (selectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    gameplay.show();
                }

                if (selectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    twoplayergame.show();
                }

                if (selectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    howtoplay.show();
                }

                if (selectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    help.show();
                }

                if (selectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    hideAllScenes();
                    about.show();
                }

                if (selectedIndex == 5 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }

            if (startscreen.Enabled || help.Enabled || gameplay.Enabled || twoplayergame.Enabled 
                 || howtoplay.Enabled || about.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    if (gameplay.Enabled)
                    {
                        
                        gameplay.enterpressed = false;
                        gameplay.cm.carhit = false;
                        gameplay.start = false;
                        gameplay.gameReset();
                        gameplay.car1.score = 0;
                        gameplay.car1.position = new Vector2(-1, -120);
                        gameplay.car1.speed.Y = 0;
                        gameplay.car2.score = 0;
                        gameplay.car2.position = new Vector2(-1, -120);
                        gameplay.car2.speed.Y = 0;
                        gameplay.car3.score = 0;
                        gameplay.car3.position = new Vector2(-1, -120);
                        gameplay.car3.speed.Y = 0;
                        gameplay.maxroadspeed = 10f;
                        gameplay.pressSpaceBarOne.Message = "Press SpaceBar to start";
                    }

                    if (twoplayergame.Enabled )
                    {
                        twoplayergame.gameReset();
                        twoplayergame.scoreAndMsgReset();
                        twoplayergame.pressSpaceBar.Message = "Press SpaceBar to start";
                        twoplayergame.winString.Message = "";
                    }

                    hideAllScenes();
                    
                    startscreen.show();
                }
            }


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            if (gameplay.Enabled || twoplayergame.Enabled)
            {
                
                GraphicsDevice.Clear(grass);
            }
            else
            {
                GraphicsDevice.Clear(Color.Black);
            }


            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
