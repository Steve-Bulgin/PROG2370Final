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
    public class Road : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        public Texture2D tex;

        public Texture2D Tex
        {
            get { return tex; }
            set { tex = value; }
        }
        private Rectangle roadRect;
        private Vector2 position1, position2;
        public Vector2 speed;

        public Road(Game game, SpriteBatch spriteBatch, Texture2D tex, Rectangle roadRect,
                Vector2 position, Vector2 speed)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.roadRect = roadRect;
            this.position1 = position;
            position2 = new Vector2(position.X, tex.Height - position.Y);
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

            position1 += speed;
            position2 += speed;
            //if (position2.Y > Stage.size.Y)
            //{
            //    position1.Y = position2.Y + tex.Height;
            //}
            //if (position1.Y > Stage.size.Y)
            //{
            //    position2.Y = position1.Y + tex.Height;
            //}

            if (position2.Y > +3 )
            {
                position1.Y = position2.Y - tex.Height ;
            }
            if (position1.Y > +3)
            {
                position2.Y = position1.Y - tex.Height;
            }


            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position1, roadRect, Color.White);
            spriteBatch.Draw(tex, position2, roadRect, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
