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
    public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont regfont, hilightedfont;
        private List<string> menuItems;
        private int selectedIndex;

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }

        private Vector2 positition;
        private Color regularColor = Color.White;
        private Color highlightedColor = Color.Red;
        private KeyboardState oldKeyState;
     


        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont regfont, SpriteFont hilightedfont,
                    string[] menuthings)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.regfont = regfont;
            this.hilightedfont = hilightedfont;
            this.menuItems = new List<string>();
            

            for (int i = 0; i < menuthings.Length; i++)
            {
                menuItems.Add(menuthings[i]);
            }

            positition = new Vector2(Stage.size.X / 2 - 60, Stage.size.Y / 2-100);
            
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

            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Down) && oldKeyState.IsKeyUp(Keys.Down))
            {
                selectedIndex++;

                if (selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }

            if (ks.IsKeyDown(Keys.Up) && oldKeyState.IsKeyUp(Keys.Up))
            {
                selectedIndex--;

                if (selectedIndex == -1)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }

            oldKeyState = ks;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = positition;

            spriteBatch.Begin();

            for (int i = 0; i < menuItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    spriteBatch.DrawString(hilightedfont, menuItems[i], tempPos, highlightedColor);
                    tempPos.Y += hilightedfont.LineSpacing * 2;
                }
                else
                {
                    spriteBatch.DrawString(regfont, menuItems[i], tempPos, regularColor);
                    tempPos.Y += regfont.LineSpacing * 2;
                }
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
