using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Util
{
   
    public class RandomManager
    {
        
        static double lastvalue;
        static double value;
        static Random r = new Random((int)DateTime.Now.Millisecond);
        public RandomManager(){}
        /// <summary>
        /// (Inclusive) psuedo Random Generator
        /// </summary>
        /// <param name="min">Minimum Number</param>
        /// <param name="max">Maximum Number</param>
        /// <returns>random number between min(inclusive) and max(inclusive) </returns>
        public static double getRandom(double min, double max)
        {
            double value;

            min = Math.Ceiling(min);
            max = Math.Floor(max);

            value = Math.Floor(r.NextDouble() * (max - min + 1)) + min;
            return value;
        }

        /// <summary>
        /// (Exclusive) Psuedo Random Generator
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>random number between 0(inclusive) and max(exclusive) </returns>
        public static int getRandomint(int min, int max)
        {
            int value;
            value = r.Next(min, max);
            return value;
        }
        /// <summary>
        /// Delays the return of a random value for set periods of time 
        /// </summary>
        /// <param name="min"> Minimum Number (inclusive)</param>
        /// <param name="max">Max Number (inclusive)</param>
        /// <param name="gameTime">Needs GameTime</param>
        /// <param name="timer">Depends on Timer Service</param>
        /// <returns>Random Number</returns>
        public static double getRandomDelay(double min, double max, int delaytimeinseconds, GameTime gameTime, Timer timer)
        {
            
            timer.PlayTimerContinuous(delaytimeinseconds, gameTime);
            if (timer.IsTimeUp)
            {
                value = getRandom(min, max);
                lastvalue = value;
                return value;
            }
            return lastvalue;
        }
        
    }

    
}
