using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace The_Lost_Vikings.Engine
{
    /// <summary>
    /// A simple Timer for a usage
    /// </summary>
    class Timer
    {
        public float timer, targetTime;
        public bool IsActive;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="time">target time</param>
        public Timer(float time)
        {
            targetTime = time;
            IsActive = true;
        }

        /// <summary>
        
        /// If timer currenttime >= targettime -> return true
        /// </summary>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public bool Update(GameTime gameTime)
        {
            if (IsActive)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (timer >= targetTime)
            { 
                return true;
            }
            return false;
        }
        
        public void Reset()
        {
            timer = 0;
        }
    }
}
