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
    public class Car : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        public Vector2 position;
        private Vector2 startpos;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 speed;

        public void CarLeft()
        {
            position.X -= speed.X;

            if (position.X < 0)
            {
                position.X = 0;
            }
        }

        public void CarRight()
        {
            position.X += speed.X;

            if (position.X > Stage.size.X - tex.Width)
            {
                position.X = Stage.size.X - tex.Width;
            }
        }



        public Car(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, Vector2 speed)
            : base(game)
        {
            // TODO: Construct any child components here

            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            startpos = position;
            this.speed = speed;
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

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(tex, position, Color.White);

            spriteBatch.End();


            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y,
                tex.Width, tex.Height);
        }
    }
}
