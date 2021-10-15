﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Diagnostics;


namespace fpsCounter
{
    public class SimpleFps
    {
        private double frames = 0;
        private double updates = 0;
        private double elapsed = 0;
        private double last = 0;
        private double now = 0;
        public double msgFrequency = 1.0f;
        public string msg = "";

        /// <summary>
        /// The msgFrequency here is the reporting time to update the message.
        /// </summary>
        public void Update(GameTime gameTime)
        {
            now = gameTime.TotalGameTime.TotalSeconds;
            elapsed = (double)(now - last);
            if (elapsed > msgFrequency)
            {
                msg = " Fps: " + (frames / elapsed).ToString() + "\n Elapsed time: " + elapsed.ToString() + "\n Updates: " + updates.ToString() + "\n Frames: " + frames.ToString();
                elapsed = 0;
                frames = 0;
                updates = 0;
                last = now;
            }
            updates++;
        }
    }
}
