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
    public class CollisionManager : Microsoft.Xna.Framework.GameComponent
    {
        private Car car;
        private GameCar car1, car2, car3;
        private Vector2 stage;
        public bool carhit;
        private int hitnum;


        public int Hitnum
        {
            get { return hitnum; }
            set { hitnum = value; }
        }
        private bool hit;

        public bool Hit
        {
            get { return hit; }
            set { hit = value; }
        }

       

        
        public CollisionManager(Game game, Car car, GameCar car1, GameCar car2, GameCar car3, Vector2 stage)
            : base(game)
        {
            // TODO: Construct any child components here
            this.car = car;
            this.car1 = car1;
            this.car2 = car2;
            this.car3 = car3;
            this.stage = stage;
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

            Rectangle recCar = car.getBounds();
            Rectangle recGameCar = car1.getBounds();
            Rectangle recGameCar2 = car2.getBounds();
            Rectangle recGameCar3 = car3.getBounds();

            if (recCar.Intersects(recGameCar))
            {
                car1.position.Y = -car1.tex.Height -1;
                //car1.posRandomizer();
                //car1.spdRandomizer();
                hit = true;
                carhit = true;
                hitnum = 1;
                
                
            }

            if (recCar.Intersects(recGameCar2))
            {
                car2.position.Y = -car2.tex.Height - 1;
                //car2.posRandomizer();
                //car2.spdRandomizer();
                hit = true;
                carhit = true;
                hitnum = 2;

            }
            else if (recCar.Intersects(recGameCar3))
            {
                car3.position.Y = -car3.tex.Height - 1;
                //car3.posRandomizer();
                //car3.spdRandomizer();
                hit = true;
                carhit = true;
                hitnum = 3;
            }

            if (recGameCar.Intersects(recGameCar2))
            {
                car2.position.Y = -car2.tex.Height - 1;
                //car2.posRandomizer();
                //car2.spdRandomizer();
            }

            if (recGameCar3.Intersects(recGameCar2) || recGameCar3.Intersects(recGameCar))
            {
                car3.position.Y = -car2.tex.Height - 1;
                //car3.posRandomizer();
                //car3.spdRandomizer();
            }

            

            base.Update(gameTime);
        }
    }
}
