using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

using System.Diagnostics;

namespace Pong
{
    class SoundController
    {
        private SoundEffect WallHit;
        private SoundEffect GoalHit;
        private SoundEffect PaddleHit;

        public SoundController(SoundEffect wallHit, SoundEffect goalHit, SoundEffect paddleHit)
        {
            this.WallHit = wallHit;
            this.GoalHit = goalHit;
            this.PaddleHit = paddleHit;
        }

        public void Play(int sound)
        {
            switch (sound)
            {
                case 1:
                    WallHit.Play();
                    break;
                case 2:
                    GoalHit.Play();
                    break;
                case 3:
                    PaddleHit.Play();
                    break;
                default:
                    Debug.WriteLine("No audio to play by this code!");
                    break;
            }
        }
    }
}
