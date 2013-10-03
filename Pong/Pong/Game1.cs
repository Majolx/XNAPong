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

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D field;
        Paddle paddle1;
        Paddle paddle2;
        Ball ball;

        SoundController soundController;

        SpriteFont score;
        int textBuffer = 10;

        int score1;
        int score2;

        int BOUND = 17;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 320;
            graphics.PreferredBackBufferWidth = 480;
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
            // Start at 2 until the issue with the font is fixed..
            score1 = 2;
            score2 = 2;

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

            field = Content.Load<Texture2D>("res/img/background");
            ball = new Ball(
                Content.Load<Texture2D>("res/img/ball"), 
                field.Width/2 - 8,
                field.Height/2 - 8);
            paddle1 = new Paddle(Content.Load<Texture2D>("res/img/paddle"), 20, 200, Keys.W, Keys.S);
            paddle2 = new Paddle(Content.Load<Texture2D>("res/img/paddle"), 450, 200, Keys.Up, Keys.Down);
            score = Content.Load<SpriteFont>("res/fnt/PongFont");
            soundController = new SoundController(
                Content.Load<SoundEffect>("res/snd/plop"),
                Content.Load<SoundEffect>("res/snd/peeep"),
                Content.Load<SoundEffect>("res/snd/beeep"));
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

            paddle1.Update();
            paddle2.Update();
            ball.Update();

            checkIfBallHit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(field, new Rectangle(0, 0, 480, 320), Color.White);
            spriteBatch.DrawString(score, 
                "" + score1, 
                new Vector2(field.Width/2 - score.MeasureString("" + score1).X - BOUND, BOUND + textBuffer), 
                Color.Green);
            spriteBatch.DrawString(score, 
                "" + score2, 
                new Vector2(field.Width/2 + BOUND, BOUND + textBuffer), 
                Color.Green);

            spriteBatch.End();

            paddle1.Draw(spriteBatch);
            paddle2.Draw(spriteBatch);
            ball.Draw(spriteBatch);

            base.Draw(gameTime);
        }

        private void checkIfBallHit()
        {
            // If left paddle is hit..
            if (ball.PosX <= paddle1.PosX + paddle1.Texture.Width
                && ball.PosY >= paddle1.PosY
                && ball.PosY <= paddle1.PosY + paddle1.Texture.Height)
            {
                ball.leftPaddleHit();
                soundController.Play(3);
            }
            // If right paddle is hit..
            if (ball.PosX + ball.Texture.Width >= paddle2.PosX
                && ball.PosY >= paddle2.PosY
                && ball.PosY <= paddle2.PosY + paddle2.Texture.Height)
            {
                ball.rightPaddleHit();
                soundController.Play(3);
            }
            // If upper wall is hit..
            if (ball.PosY <= BOUND)
            {
                ball.topWallHit();
                soundController.Play(1);
            }
            // If lower wall is hit..
            if (ball.PosY + ball.Texture.Height >= field.Height - BOUND)
            {
                ball.bottomWallHit();
                soundController.Play(1);
            }
            // If past left bounds..
            if (ball.PosX <= paddle1.PosX + paddle1.Texture.Width
                && (ball.PosY <= paddle1.PosY - ball.Texture.Height
                || ball.PosY >= paddle1.PosY + paddle1.Texture.Height))
            {
                ball.leftBoundHit();
                score2++;
                soundController.Play(2);
            }
            // If past right bounds..
            if (ball.PosX + ball.Texture.Width >= paddle2.PosX
                && (ball.PosY <= paddle2.PosY - ball.Texture.Height
                || ball.PosY >= paddle2.PosY + paddle2.Texture.Height))
            {
                ball.rightBoundHit();
                score1++;
                soundController.Play(2);
            }
        }
    }
}
