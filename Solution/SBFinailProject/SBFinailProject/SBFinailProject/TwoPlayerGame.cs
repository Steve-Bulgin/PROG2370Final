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
    public class TwoPlayerGame : GameScene 
    {
        private SpriteBatch spriteBatch;
        private string leftScoreMsg, rightScoreMsg;
        public string gameOverTwoPayerMsg;
        private int leftscorecount, rightscorecount, leftcarleftbounds, leftcarrightbounds, rightcarleftbounds,
                    rightcarrightbounds, lefthornDelay, righthornDelay;
        private ScoreString leftscore, rightscore;
        public ScoreString gameOverTwoPlayer;
        public ScoreString pressSpaceBar, winString;
        private SpriteFont gameOverFont;
        private Vector2 leftcarstartpos, rightcarstartpos, spacePos, winVec;
        private Car leftcar, rightcar;
        private GameCar leftc1, leftc2, leftc3, rightc1, rightc2, rightc3;
        private Road leftroad, rightroad;
        private Texture2D creamcar, redcar, yellowcar, purplecar, greencar,
                          bluecar, orangecar, policecar, leftrandomcar, rightrandomcar, boomtex;
        private CollisionManager leftcm, rightcm;
        private Explosion explosion;
        private bool start, lefthornPlay, righthornPlay;
        private int baseCarSpd, maxCarSpd;
        private SoundEffect lefthorn, righthorn, exploSound;
        public void leftCarRandomizer()
        {
            Random leftrandcar = new Random();
            int pick = leftrandcar.Next(0, 8);

            if (pick == 0)
            {
                leftrandomcar = creamcar;
            }
            else if (pick == 1)
            {
                leftrandomcar = redcar;
            }
            else if (pick == 2)
            {
                leftrandomcar = purplecar;
            }
            else if (pick == 3)
            {
                leftrandomcar = yellowcar;
            }
            else if (pick == 4)
            {
                leftrandomcar = greencar;
            }
            else if (pick == 5)
            {
                leftrandomcar = bluecar;
            }
            else if (pick == 6)
            {
                leftrandomcar = orangecar;
            }
            else if (pick == 7)
            {
                leftrandomcar = policecar;
            }
        }

        public void rightCarRandomizer()
        {
            Random rightrandcar = new Random();
            int pick = rightrandcar.Next(0, 8);

            if (pick == 0)
            {
                rightrandomcar = creamcar;
            }
            else if (pick == 1)
            {
                rightrandomcar = redcar;
            }
            else if (pick == 2)
            {
                rightrandomcar = purplecar;
            }
            else if (pick == 3)
            {
                rightrandomcar = yellowcar;
            }
            else if (pick == 4)
            {
                rightrandomcar = greencar;
            }
            else if (pick == 5)
            {
                rightrandomcar = bluecar;
            }
            else if (pick == 6)
            {
                rightrandomcar = orangecar;
            }
            else if (pick == 7)
            {
                rightrandomcar = policecar;
            }
        }

        public void gameReset()
        {
            rightc1.position = new Vector2(-1, -120);
            rightc1.speed.Y = 0;
            rightc2.position = new Vector2(-1, -120);
            rightc2.speed.Y = 0;
            rightc3.position = new Vector2(-1, -120);
            rightc3.speed.Y = 0;

            leftc1.position = new Vector2(-1, -120);
            leftc1.speed.Y = 0;
            leftc2.position = new Vector2(-1, -120);
            leftc2.speed.Y = 0;
            leftc3.position = new Vector2(-1, -120);
            leftc3.speed.Y = 0;

            rightroad.speed.Y = 0;
            leftroad.speed.Y = 0;


            start = false;
        }

        public void scoreAndMsgReset()
        {
            rightc1.score = 0;
            rightc2.score = 0;
            rightc3.score = 0;

            leftc1.score = 0;
            leftc2.score = 0;
            leftc3.score = 0;

            leftcm.carhit = false;
            rightcm.carhit = false;

            rightcar.Visible = true;
            leftcar.Visible = true;


            leftcar.Position = leftcarstartpos;
            rightcar.Position = rightcarstartpos;

            gameOverTwoPlayer.Message = "";

        }

        private void speedSetter()
        {
            //Right base
            rightc1.basespeed = baseCarSpd;
            rightc2.basespeed = baseCarSpd;
            rightc3.basespeed = baseCarSpd;

            //Left base
            leftc1.basespeed = baseCarSpd;
            leftc2.basespeed = baseCarSpd;
            leftc3.basespeed = baseCarSpd;

            //Right max

            rightc1.maxspeed = maxCarSpd;
            rightc2.maxspeed = maxCarSpd;
            rightc3.maxspeed = maxCarSpd;

            //Left Max
            leftc1.maxspeed = maxCarSpd;
            leftc2.maxspeed = maxCarSpd;
            leftc3.maxspeed = maxCarSpd;
        }
        
        public TwoPlayerGame(Game game, SpriteBatch spriteBatch)
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
            lefthorn = game.Content.Load<SoundEffect>("sounds/Horn");
            righthorn = game.Content.Load<SoundEffect>("sounds/Horn");
            exploSound = game.Content.Load<SoundEffect>("sounds/expl06");
            boomtex = game.Content.Load<Texture2D>("images/explosion.hasgraphics");

            Rectangle leftroadRect = new Rectangle(0, 100, 376, 478);
            Vector2 leftRoadPos = new Vector2(100, 0);
            Rectangle rightroadRect = new Rectangle(0, 100, 376, 478);
            Vector2 rightRoadPos = new Vector2(475, 0);

            leftroad = new Road(game, spriteBatch, game.Content.Load<Texture2D>("images/goodroad"), leftroadRect, leftRoadPos, new Vector2(0, 0));
            rightroad = new Road(game, spriteBatch, game.Content.Load<Texture2D>("images/goodroad"), rightroadRect, rightRoadPos, new Vector2(0, 0));

            //Player cars

            leftcarstartpos = new Vector2(260, 375);
            Vector2 leftcarspd = new Vector2(5,0);
            leftcar = new Car(game, spriteBatch, greencar, leftcarstartpos, leftcarspd);

            rightcarstartpos = new Vector2(635, 375);
            Vector2 rightcarspd = new Vector2(5, 0);
            rightcar = new Car(game, spriteBatch, redcar, rightcarstartpos, rightcarspd);

            //Player cars//

            // Computer cars

            //Left Cars
            leftcarleftbounds = 130;
            leftcarrightbounds = 389;

            Vector2 leftc1pos = new Vector2(-1, -105);
            Vector2 leftc1spd = new Vector2(0, +8);
            leftc1 = new GameCar(game, spriteBatch, leftrandomcar, leftc1pos, leftcarleftbounds, leftcarrightbounds,
                    1, 4);

            Vector2 leftc2pos = new Vector2(-1, -105);
            Vector2 leftc2spd = new Vector2(0, +2);
            leftc2 = new GameCar(game, spriteBatch, leftrandomcar, leftc2pos, leftcarleftbounds, leftcarrightbounds,
                     1, 4);

            Vector2 leftc3pos = new Vector2(-1, -105);
            Vector2 leftc3spd = new Vector2(0, +4);
            leftc3 = new GameCar(game, spriteBatch, leftrandomcar, leftc3pos, leftcarleftbounds, leftcarrightbounds,
                    1, 4);

            //Right Cars

            rightcarleftbounds = 503;
            rightcarrightbounds = 762;

            Vector2 rightc1pos = new Vector2(-1, -105);
            Vector2 rightc1spd = new Vector2(0, +2);
            rightc1 = new GameCar(game, spriteBatch, rightrandomcar, rightc1pos, rightcarleftbounds, rightcarrightbounds,
                    1, 4);

            Vector2 rightc2pos = new Vector2(-1, -105);
            Vector2 rightc2spd = new Vector2(0, +2);
            rightc2 = new GameCar(game, spriteBatch, rightrandomcar, rightc2pos, rightcarleftbounds, rightcarrightbounds,
                    1, 4);

            Vector2 rightc3pos = new Vector2(-1, -105);
            Vector2 rightc3spd = new Vector2(0, +2);
            rightc3 = new GameCar(game, spriteBatch, rightrandomcar, rightc3pos, rightcarleftbounds, rightcarrightbounds,
                    1, 4);
 
            //Computer Cars//

            Vector2 exppos = new Vector2(0, 0);
            explosion = new Explosion(game, spriteBatch, boomtex, exppos, 1);

            rightcm = new CollisionManager(game, rightcar, rightc1, rightc2, rightc3, Stage.size);
            this.Components.Add(rightcm);

            leftcm = new CollisionManager(game, leftcar, leftc1, leftc2, leftc3, Stage.size);
            this.Components.Add(leftcm);

            Vector2 leftscorePos = new Vector2(0,0);
            leftScoreMsg = "Left Score\n" + leftscorecount.ToString();
            leftscore = new ScoreString(game, spriteBatch, leftScoreMsg, leftscorePos, Color.White, game.Content.Load<SpriteFont>("fonts/StandardMenuFont"));


            rightScoreMsg = "Right Score\n" + rightscorecount.ToString();
            Vector2 rightscorePos = new Vector2(Stage.size.X - 140, 0);
            
            rightscore = new ScoreString(game, spriteBatch, rightScoreMsg, rightscorePos, Color.White, game.Content.Load<SpriteFont>("fonts/StandardMenuFont"));


            gameOverTwoPayerMsg = "";
            gameOverFont = game.Content.Load<SpriteFont>("fonts/bigfont");
            Vector2 gameOverPos = new Vector2(Stage.size.X / 2 - gameOverFont.MeasureString(gameOverTwoPayerMsg).X - 150, Stage.size.Y / 2 - gameOverFont.LineSpacing);
            gameOverTwoPlayer = new ScoreString(game, spriteBatch, gameOverTwoPayerMsg, gameOverPos, Color.White, gameOverFont);

            winVec = new Vector2(315, 230);
            winString = new ScoreString(game, spriteBatch, "", winVec, Color.Red, game.Content.Load<SpriteFont>("fonts/winner"));

            spacePos = new Vector2(346, 230);
            pressSpaceBar = new ScoreString(game, spriteBatch, "Press SpaceBar to start", spacePos, Color.White, game.Content.Load<SpriteFont>("fonts/StandardMenuFont"));



            this.Components.Add(leftroad);
            this.Components.Add(rightroad);
            this.Components.Add(leftcar);
            this.Components.Add(rightcar);
            this.Components.Add(leftc1);
            this.Components.Add(leftc2);
            this.Components.Add(leftc3);
            this.Components.Add(rightc1);
            this.Components.Add(rightc2);
            this.Components.Add(rightc3);
            this.Components.Add(explosion);
            this.Components.Add(leftscore);
            this.Components.Add(rightscore);
            this.Components.Add(gameOverTwoPlayer);
            this.Components.Add(pressSpaceBar);
            this.Components.Add(winString);

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

            //Left car
            if (ks.IsKeyDown(Keys.Right))
            {
               rightcar.CarRight();
            }

            if (ks.IsKeyDown(Keys.Left))
            {
                rightcar.CarLeft();
            }


            //Right car
            if (ks.IsKeyDown(Keys.S))
            {
                leftcar.CarRight();
            }

            if (ks.IsKeyDown(Keys.A))
            {
                leftcar.CarLeft();
            }


            //Left movement
            if (leftcar.Position.X < 130)
            {
                leftcar.position.X = 130;
            }

            if (leftcar.Position.X > 389)
            {
                leftcar.position.X = 389;
            }

            //Right movement
            if (rightcar.Position.X < 503)
            {
                rightcar.position.X = 503;
            }
            if (rightcar.Position.X > 762)
            {
                rightcar.position.X = 762;
            }

            if (start)
            {
                rightroad.speed.Y = 10;
                leftroad.speed.Y = 10;
            }

            //Set cars
            //Left

            
            if (leftc1.position.Y < -100)
            {
                leftCarRandomizer();
                leftc1.tex = leftrandomcar;
                if (start)
                {
                    leftc1.posRandomizer();
                    leftc1.spdRandomizer(); 
                }
            }
            
            if (leftc2.position.Y < -100)
            {
                leftCarRandomizer();
                leftc2.tex = leftrandomcar;
                if (start)
                {
                    leftc2.posRandomizer();
                    leftc2.spdRandomizer(); 
                }
            }

            if (leftc3.position.Y < -100)
            {
                leftCarRandomizer();
                leftc3.tex = leftrandomcar;
                if (start)
                {
                    leftc3.posRandomizer();
                    leftc3.spdRandomizer(); 
                }
            }

            //Right

            if (rightc1.position.Y < -100)
            {
                rightCarRandomizer();
                rightc1.tex =rightrandomcar;
                if (start)
                {
                    rightc1.posRandomizer();
                    rightc1.spdRandomizer(); 
                }
            }

            if (rightc2.position.Y < -100)
            {
                rightCarRandomizer();
                rightc2.tex = rightrandomcar;
                if (start)
                {
                    rightc2.posRandomizer();
                    rightc2.spdRandomizer(); 
                }
            }
            if (rightc3.position.Y < -100)
            {
                rightCarRandomizer();
                rightc3.tex = rightrandomcar;
                if (start)
                {
                    rightc3.posRandomizer();
                    rightc3.spdRandomizer(); 
                }
            }

            Console.WriteLine("leftcar" + leftcm.carhit);
            Console.WriteLine("rightcar" + rightcm.carhit);
            //Speed up
            if ((leftscorecount >= 10 && rightscorecount >= 10) && 
                (leftcm.carhit == false && rightcm.carhit == false))
            {
                //Roads
                leftroad.speed.Y = 20;
                rightroad.speed.Y = 20;

                baseCarSpd = 5;
                maxCarSpd = 10;
                speedSetter();
            }

            if (leftcm.Hit == true)
            {
                if (leftcm.Hitnum == 1)
                {
                    leftCarRandomizer();
                    leftc1.tex = leftrandomcar;
                    leftcm.Hit = false;
                    leftcm.Hitnum = 0;
                }

                if (leftcm.Hitnum == 2)
                {
                    leftCarRandomizer();
                    leftc2.tex = leftrandomcar;
                    leftcm.Hit = false;
                    leftcm.Hitnum = 0;
                }

                if (leftcm.Hitnum == 3)
                {
                    leftCarRandomizer();
                    leftc3.tex = leftrandomcar;
                    leftcm.Hit = false;
                    leftcm.Hitnum = 0;
                }

                if (leftcm.carhit == true)
                {
                    rightroad.speed.Y = 0;
                    leftroad.speed.Y = 0;
                    leftcar.Visible = false;
                    winString.Position = new Vector2(315, 230);
                    winString.Color = Color.Red;
                    winString.Message = "Red Wins!";
                    explosion.Position = leftcar.Position;
                    explosion.start();
                    exploSound.Play();
                    gameReset();
                }
            }

            if (rightcm.Hit == true)
            {
                if (rightcm.Hitnum == 1)
                {
                    rightCarRandomizer();
                    rightc1.tex = leftrandomcar;
                    rightcm.Hit = false;
                    rightcm.Hitnum = 0;
                }

                if (rightcm.Hitnum == 2)
                {
                    rightCarRandomizer();
                    rightc2.tex = leftrandomcar;
                    rightcm.Hit = false;
                    rightcm.Hitnum = 0;
                }

                if (rightcm.Hitnum == 3)
                {
                    rightCarRandomizer();
                    rightc3.tex = leftrandomcar;
                    rightcm.Hit = false;
                    rightcm.Hitnum = 0;
                }

                if (rightcm.carhit == true)
                {
                    rightroad.speed.Y = 0;
                    leftroad.speed.Y = 0;
                    rightcar.Visible = false;
                    winString.Position = new Vector2(275, 230);
                    winString.Color = Color.Green;
                    winString.Message = "Green Wins!";
                    explosion.Position = rightcar.Position;
                    explosion.start();
                    exploSound.Play();
                    gameReset();
                }
            }

            if (leftcm.carhit == true || rightcm.carhit == true)
            {
                gameOverTwoPlayer.Message = "Game Over";
                pressSpaceBar.Position = new Vector2(346, 270);
                pressSpaceBar.Message = "Press SpaceBar to play again";
            }

            if (ks.IsKeyDown(Keys.Space))
            {
                if (rightroad.speed.Y==0 && leftroad.speed.Y == 0)
                {
                    start = true;
                    scoreAndMsgReset();
                    baseCarSpd = 1;
                    maxCarSpd = 4;
                    speedSetter();
                    pressSpaceBar.Message = "";
                    winString.Message = "";
                }
            }

            if (ks.IsKeyDown(Keys.W))
            {
                if (lefthornPlay == false)
                {
                    lefthorn.Play();
                    lefthornPlay = true;
                }
            }


            //Horn
            if (lefthornPlay == true )
            {
                lefthornDelay++;

                if (lefthornDelay >= 140)
                {
                    lefthornPlay = false;
                    lefthornDelay = 0;
                }
            }

            if (ks.IsKeyDown(Keys.Up))
            {
                if (righthornPlay == false)
                {
                    righthorn.Play();
                    righthornPlay = true;
                }
            }

            if (righthornPlay == true)
            {
                righthornDelay++;
                if (righthornDelay>= 140)
                {
                    righthornPlay = false;
                    righthornDelay = 0;
                }
            }

            leftscorecount = leftc1.score + leftc2.score + leftc3.score;
            leftscore.Message = "Left Score\n" + leftscorecount.ToString();

            rightscorecount = rightc1.score + rightc2.score + rightc3.score;
            rightscore.Message = "Right Score\n" + rightscorecount.ToString();
            
            base.Update(gameTime);
        }

        
    }
}
