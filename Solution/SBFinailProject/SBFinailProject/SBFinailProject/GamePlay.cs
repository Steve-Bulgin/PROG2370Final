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
    public class GamePlay : GameScene
    {
        private SpriteBatch spriteBatch;
        private string scoreMsg;
        public string gameOverMsg;
        private ScoreString score;
        public ScoreString gameOver, pressSpaceBarOne;


        public ScoreString GameOver
        {
            get { return gameOver; }
            set { gameOver = value; }
        }
        private Car car;
        public GameCar car1 ,car2, car3;
        private Road road;
        private Vector2 roadspeed, boompos, carpos;
        private bool delaystart,  crash, gamestarted, enter;
        public bool enterpressed;
        private int delay;
        
        private float roadspeedfactor = 0.01f;
        public float maxroadspeed = 10f;
        private Texture2D creamcar, redcar, yellowcar, purplecar, greencar,
                          bluecar, orangecar, policecar, randomcar, boomtex;
        public Explosion boom;
        private SpriteFont gameOverFont;
        private SoundEffect horn, policesound, boomSound;
        public CollisionManager cm;

        public Road Road
        {
            get { return road; }
            set { road = value; }
        }

        public bool start;
        private int scorecount;

        public void gameOverReset()
        {
            gameOver.Message = "";
 
        }
            
        public void gameReset()
        {
            
            car1.score = 0;
            car1.position = new Vector2(-1, -120);
            car1.speed.Y = 0;
            start = false;
            car2.score = 0;
            car2.position = new Vector2(-1, -120);
            car2.speed.Y = 0;
            car3.score = 0;
            car3.position = new Vector2(-1, -120);
            car3.speed.Y = 0;
            scorecount = 0;
            
            
            
        }

       

        public void CarRandomizer()
        {
            Random randcar = new Random();
            int pick = randcar.Next(0, 8);

            if (pick == 0)
            {
                randomcar = creamcar;
            }
            else if (pick == 1)
            {
                randomcar = redcar;
            }
            else if (pick == 2)
            {
                randomcar = purplecar;
            }
            else if (pick == 3)
            {
                randomcar = yellowcar;
            }
            else if (pick == 4)
            {
                randomcar = greencar;
            }
            else if (pick == 5)
            {
                randomcar = bluecar;
            }
            else if (pick == 6)
            {
                randomcar = orangecar;
            }
            else if (pick == 7)
            {
                randomcar = policecar;
            }
        }
        
        public GamePlay(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here

            this.spriteBatch = spriteBatch;

            creamcar = game.Content.Load<Texture2D>("images/creamcar");
            purplecar = game.Content.Load<Texture2D>("images/purplecar");
            redcar = game.Content.Load<Texture2D>("images/redcar");
            yellowcar = game.Content.Load<Texture2D>("images/yellowcar");
            greencar = game.Content.Load<Texture2D>("images/greencar");
            bluecar = game.Content.Load<Texture2D>("images/bluecar");
            orangecar = game.Content.Load<Texture2D>("images/orangecar");
            policecar = game.Content.Load<Texture2D>("images/policecar");
            boomtex = game.Content.Load<Texture2D>("images/explosion.hasgraphics");
            horn = game.Content.Load<SoundEffect>("sounds/Horn");
            policesound = game.Content.Load<SoundEffect>("sounds/policesound");
            boomSound = game.Content.Load<SoundEffect>("sounds/expl06");

            Rectangle roadRect = new Rectangle(0, 100, 376, 478);
            Vector2 roadpos = new Vector2(100,0);
            roadspeed = new Vector2(0, 0);
            road = new Road(game, spriteBatch, game.Content.Load<Texture2D>("images/goodroad"), roadRect, roadpos, roadspeed);


            Vector2 opcar = new Vector2(-1, -105);
            Vector2 opcarspd = new Vector2(0, 0);
            int leftlimits = 130;
            int rightlimits = 389;
            car1 = new GameCar(game, spriteBatch, randomcar, opcar, leftlimits, rightlimits, 1,4);

            
            Vector2 opcar2 = new Vector2(-1, -105);
            Vector2 opcarspd2 = new Vector2(0,0);
            car2 = new GameCar(game, spriteBatch, randomcar, opcar2, leftlimits, rightlimits, 1,4);

            
            Vector2 opcar3 = new Vector2(-1, -105);
            Vector2 opcarspd3 = new Vector2(0, 0);
            car3 = new GameCar(game, spriteBatch, randomcar, opcar3, leftlimits, rightlimits, 1,4);

            carpos = new Vector2(260, 375);
            Vector2 carspd = new Vector2(5, 0);
            car = new Car(game, spriteBatch, game.Content.Load<Texture2D>("images/greencar"), carpos, carspd);

            boompos = new Vector2(100,100);
            boom = new Explosion(game, spriteBatch, boomtex, boompos, 1);
            

            cm = new CollisionManager(game, car, car1, car2, car3, Stage.size);
            this.Components.Add(cm);


            scoreMsg = "Score \n" + scorecount.ToString();
            Vector2 scorePos = new Vector2(0,0);
            score = new ScoreString(game, spriteBatch, scoreMsg, scorePos, Color.White, game.Content.Load<SpriteFont>("fonts/StandardMenuFont"));

            gameOverMsg = "";
            gameOverFont = game.Content.Load<SpriteFont>("fonts/bigfont");
            Vector2 gameOverPos = new Vector2(Stage.size.X/2 - gameOverFont.MeasureString(gameOverMsg).X -150, Stage.size.Y /2 - gameOverFont.LineSpacing);
            gameOver = new ScoreString(game, spriteBatch, gameOverMsg, gameOverPos, Color.White, gameOverFont);
            
            Vector2 spacePos = new Vector2(346, 230);
            pressSpaceBarOne = new ScoreString(game, spriteBatch, "Press SpaceBar to start", spacePos, Color.White, game.Content.Load<SpriteFont>("fonts/StandardMenuFont"));

            this.Components.Add(road);
            this.Components.Add(score);
            this.Components.Add(car1);
            this.Components.Add(car2);
            this.Components.Add(car3);
            this.Components.Add(car);
            this.Components.Add(boom);
            this.Components.Add(gameOver);
            this.Components.Add(pressSpaceBarOne);
            
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

            if (enterpressed == false )
            {
                if (ks.IsKeyDown(Keys.Enter))
                {
                    gameOverReset();
                    start = false;
                    enterpressed = true;
                    road.speed.Y = 0;
                    roadspeedfactor = 0 ;
                    car.Visible = true;
                    
                } 
            }
            
            
            {
                Console.WriteLine("mrs: " + maxroadspeed);
                Console.WriteLine("start: "+start);
                if (ks.IsKeyDown(Keys.Up))
                {
                    if (delaystart == false)
                    {
                        horn.Play();
                        delaystart = true;
                    }
                }

                if ((car1.position.Y >= 100) && (car1.tex == policecar) && (delaystart == false))
                {
                    policesound.Play();
                    delaystart = true;
                }

                if ((car2.position.Y >= 100) && (car2.tex == policecar) && (delaystart == false))
                {
                    policesound.Play();
                    delaystart = true;
                }

                if ((car3.position.Y > 100) && (car3.tex == policecar) && (delaystart == false))
                {
                    policesound.Play();
                    delaystart = true;
                }


                if (delaystart)
                {
                    delay++;
                    if (delay >= 140)
                    {
                        delaystart = false;
                        delay = 0;
                    }
                }
                if (ks.IsKeyDown(Keys.Right))
                {
                    car.CarRight();
                }

                if (ks.IsKeyDown(Keys.Left))
                {
                    car.CarLeft();
                }


                if (car.Position.X < road.Tex.Width - 247)
                {

                    car.position.X = road.Tex.Width - 247;


                }

                if (car.Position.X > road.Tex.Width + 12)
                {

                    car.position.X = road.Tex.Width + 12;


                }

                //if (start)
                {
                    if (car1.position.Y < -100)
                    {
                        CarRandomizer();
                        car1.tex = randomcar;

                        if (start)
                        {
                            car1.posRandomizer();
                            car1.spdRandomizer();
                        }

                    }

                    if (car2.position.Y < -100)
                    {
                        CarRandomizer();
                        car2.tex = randomcar;


                        if (start)
                        {
                            car2.posRandomizer();
                            car2.spdRandomizer();
                        }

                    }

                    if (car3.position.Y < -100)
                    {
                        CarRandomizer();
                        car3.tex = randomcar;

                        if (start)
                        {
                            car3.posRandomizer();
                            car3.spdRandomizer();
                        }
                    }
                }


                if (start)
                {
                    if (cm.Hit == true)
                    {
                        if (cm.Hitnum == 1)
                        {
                            CarRandomizer();
                            car1.tex = randomcar;
                            car1.posRandomizer();
                            car1.spdRandomizer();
                            cm.Hit = false;
                            cm.Hitnum = 0;
                        }

                        if (cm.Hitnum == 2)
                        {
                            CarRandomizer();
                            car2.tex = randomcar;
                            car2.posRandomizer();
                            car2.spdRandomizer();
                            cm.Hit = false;
                            cm.Hitnum = 0;
                        }

                        if (cm.Hitnum == 3)
                        {
                            CarRandomizer();
                            car3.tex = randomcar;
                            car3.posRandomizer();
                            car3.spdRandomizer();
                            cm.Hit = false;
                            cm.Hitnum = 0;
                        }
                    }

                }

                if (road.speed.Y < maxroadspeed)
                {
                    road.speed.Y += roadspeedfactor;
                }
                if (road.speed.Y >= maxroadspeed)
                {
                    start = true;
                }
                if (scorecount >= 10)
                {
                    maxroadspeed = 20;
                    car1.basespeed = 5;
                    car2.basespeed = 5;
                    car3.basespeed = 5;
                    car1.maxspeed = 8;
                    car2.maxspeed = 8;
                    car3.maxspeed = 8;
                }

                if (cm.carhit == true)
                {
                    
                    boom.Position = car.Position;
                    Console.WriteLine("carhit "+cm.carhit);
                    road.speed.Y = 0;
                    maxroadspeed = 0;
                    if (gamestarted == true)
                    {
                        car.Visible = false;
                        if (enterpressed)
                        {
                            gameOver.Message = "GAME OVER";
                            pressSpaceBarOne.Message = "Press SpaceBar to play again";
                        }

                        if (crash == false)
                        {
                            boomSound.Play();
                            boom.start();
                            crash = true;
                        } 
                    }
                    
                    
                        gameReset(); 
                    
                    
                } 
            }
            if (ks.IsKeyDown(Keys.Space) && road.speed.Y==0)
            {
                start = false;
                cm.carhit = false;
                pressSpaceBarOne.Message = "";
                car.Position = carpos;
                gamestarted = true;
                car.Visible = true;
                crash = false;
                gameOverReset();
                roadspeedfactor = 0.01f;
                maxroadspeed = 10;
            }
                
                


            scorecount = car1.score + car2.score + car3.score;
            
            score.Message = "Score \n" + scorecount.ToString();
            
                     

            base.Update(gameTime);
        }
    }
}
