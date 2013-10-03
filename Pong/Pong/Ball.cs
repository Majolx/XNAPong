using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    class Ball
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }
        public Texture2D Texture { get; private set; }

        private int X;    // background/2 - ball/2
        private int Y;

        private float speed { get; set; }
        private float speedMod = 0.01f;
        private int speedCount = 0;
        private int speedCycle = 20;

        private int[] direction = { 0, 0 };

        public Ball(Texture2D image, int posx, int posy)
        {
            this.Texture = image;
            this.PosX = posx;
            this.PosY = posy;
            X = posx;
            Y = posy;
            speed = 1;
        }

        public void Update()
        {
            speedCount++;
            if (speedCount == speedCycle)
            {
                speedCount = 0;
                speed += speedMod;
            }

            if (direction[0] == 0) moveLeft(); else moveRight();
            if (direction[1] == 0) moveUp(); else moveDown();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, new Vector2(PosX, PosY), Color.White);
            spriteBatch.End();
        }

        public void leftPaddleHit()  { direction[0] = 1; }
        public void rightPaddleHit() { direction[0] = 0; }
        public void topWallHit()     { direction[1] = 1; }
        public void bottomWallHit()  { direction[1] = 0; }
        public void leftBoundHit()   
        { 
            direction[0] = 1; 
            direction[1] = 1;
            Reset();
        }
        public void rightBoundHit()
        {
            direction[0] = 0;
            direction[1] = 0;
            Reset();
        }

        private void moveLeft()  { PosX -= (int)speed; }
        private void moveRight() { PosX += (int)speed; }
        private void moveUp()    { PosY -= (int)speed; }
        private void moveDown()  { PosY += (int)speed; }

        private void Reset()
        {
            PosX = X;
            PosY = Y;
            speed = 1.0f;
        }
    }
}
