using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    class Paddle
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        public Texture2D Texture { get; private set; }

        private static KeyboardState keyboard;
        private Keys up { get; set; }
        private Keys down { get; set; }

        private float speedMod = 2.75f;

        public Paddle(Texture2D image, int posx, int posy, Keys up, Keys down)
        {
            this.Texture = image;
            this.PosX = posx;
            this.PosY = posy;
            this.up = up;
            this.down = down;
        }

        public void Update()
        {
            keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(up)) 
            {
                moveUp();
            }
            else if (keyboard.IsKeyDown(down))
            {
                moveDown();
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, new Vector2(PosX, PosY), Color.White);
            spriteBatch.End();
        }

        private void moveUp()
        {
            if (PosY > 17)
                PosY -= (int)(1*speedMod);
        }

        private void moveDown()
        {
            if (PosY < 303 - Texture.Height)
                PosY += (int)(1*speedMod);
        }
    }
}
