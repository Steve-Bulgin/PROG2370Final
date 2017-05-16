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
    public class GameCar : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        public Texture2D tex;
        public Vector2 position, oldpos, startpos;
        public int leftbounds, rightbounds, basespeed, maxspeed;
        public Vector2 speed;
        
        public int score;


        

        public void posRandomizer()
        {
            //if (position.Y == -tex.Height -1)
            //if(position.Y < 0 - 95)
            {
                Random randpos = new Random();
                position.X = randpos.Next(leftbounds, rightbounds);
                oldpos.X = position.X;

                if (position.X==oldpos.X)
                {
                    position.X = randpos.Next(leftbounds, rightbounds);
                }

                Console.WriteLine(position.X);
            }
       
        }

        public void spdRandomizer()
        {
            //if (position.Y == -tex.Height - 1)
            {
                Random randspd = new Random();
                speed.Y = randspd.Next(basespeed , maxspeed);
            }
        }


        public GameCar(Game game, SpriteBatch spriteBatch, Texture2D tex, Vector2 position, int leftbounds, int rightbounds, int basespeed, int maxspeed)
            : base(game)
        {
            // TODO: Construct any child components here

            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.position = position;
            startpos = position;
            this.leftbounds = leftbounds;
            this.rightbounds = rightbounds;
            this.basespeed = basespeed;
            this.maxspeed = maxspeed;
            
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

            position.Y += speed.Y;

            if (position.Y >= Stage.size.Y + tex.Height)
            {                   
                    score++;
                    position = startpos;
                            
            }

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
