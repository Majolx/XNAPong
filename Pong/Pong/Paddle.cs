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
        private int posx { get; set; }
        private int posy { get; set; }
        private Texture2D image { get; set; }

        private static KeyboardState keyboard;
        private Keys up { get; set; }
        private Keys down { get; set; }

        public Paddle(Texture2D image, int posx, int posy, Keys up, Keys down)
        {
            this.image = image;
            this.posx = posx;
            this.posy = posy;
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
            spriteBatch.Draw(image, new Vector2(posx, posy), Color.White);
            spriteBatch.End();
        }

        private void moveUp()
        {
            if (posy > 10)
                posy--;
        }

        private void moveDown()
        {
            if (posy < 250)
                posy++;
        }
    }
}
