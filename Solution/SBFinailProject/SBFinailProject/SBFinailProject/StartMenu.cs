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
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class StartMenu : GameScene
    {
        public MenuComponent menu;


        public MenuComponent Menu
        {
            get { return menu; }
            set { menu = value; }
        }

        private SpriteBatch spriteBatch;
        private string[] menuchoices = { "One Player Game","Two Player Game", "How to Play", "Help", "About", "Quit" };
        private BackGround bg;


        public StartMenu(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here

            

            this.spriteBatch = spriteBatch;
            Vector2 bgpos = new Vector2(100, 0);
            bg = new BackGround(game, spriteBatch, game.Content.Load<Texture2D>("images/titlepic"), bgpos);
            this.Components.Add(bg);

            menu = new MenuComponent(game, spriteBatch, game.Content.Load<SpriteFont>("fonts/StandardMenuFont"),
                            game.Content.Load<SpriteFont>("fonts/HiLightedMenuFont"), menuchoices);
            this.Components.Add(menu);

            

             
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }
    }
}
