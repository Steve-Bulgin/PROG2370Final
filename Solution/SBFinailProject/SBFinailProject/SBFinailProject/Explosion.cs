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
    public class Explosion : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector2 dimension;
        private List<Rectangle> frames;
        private int frameindex = -1;
        private int delay;
        private int delaycount;


        public void stop()
        {

            this.Enabled = false;
            this.Visible = false;
        }
        public void start()
        {

            this.Enabled = true;
            this.Visible = true;
        }

        private void createFrames()
        {
            frames = new List<Rectangle>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    int x = j * (int)dimension.X;
                    int y = i * (int)dimension.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimension.X,
                        (int)dimension.Y);
                    frames.Add(r);

                }
            }

        }

        public Explosion(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, int delay)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            this.delay = delay;
            dimension = new Vector2(100, 100);
            stop();
            createFrames();
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
            delaycount++;
            if (delaycount > delay)
            {
                frameindex++;
                if (frameindex > 80)
                {
                    frameindex = -1;
                    stop();
                }
                delaycount = 0;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (frameindex >= 0)
            {
                spriteBatch.Draw(tex, position,
                    frames.ElementAt<Rectangle>(frameindex),
                    Color.White);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
