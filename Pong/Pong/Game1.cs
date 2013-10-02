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
        Texture2D ball;

        SoundEffect beep1;
        SoundEffect beep2;
        SoundEffect beep3;

        SpriteFont score;

        int score1;
        int score2;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
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
            score1 = 0;
            score2 = 0;

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
            ball = Content.Load<Texture2D>("res/img/ball");
            paddle1 = new Paddle(Content.Load<Texture2D>("res/img/paddle"), 20, 200, Keys.W, Keys.S);
            paddle2 = new Paddle(Content.Load<Texture2D>("res/img/paddle"), 450, 200, Keys.Up, Keys.Down);
            score = Content.Load<SpriteFont>("res/fnt/Score");
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
            spriteBatch.DrawString(score, "" + score1, new Vector2(182, 5), Color.Green);
            spriteBatch.DrawString(score, "" + score2, new Vector2(250, 5), Color.Green);

            spriteBatch.End();

            paddle1.Draw(spriteBatch);
            paddle2.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
