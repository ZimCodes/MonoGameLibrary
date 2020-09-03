using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Util
{
    enum TimerState {Play,Stop,Reset};
    public interface ITimer
    {
        void ResetTimer(GameTime gameTime);
        void PlayTimerContinuous(float timerinmlliseconds, GameTime gameTime);
        void PlayTimerOnce(float timerinmilliseconds, GameTime gameTime);
    }
    public class Timer
    {
        /// <summary>
        /// Holds all Timer Instances 
        /// </summary>
        public List<Timer> Timers = new List<Timer>();

        private float CurrentTime;
        private float SecondsTimer;

        /// <summary>
        /// Tells you when time is up
        /// </summary>
        public bool IsTimeUp;
        public Timer()
        {
            CurrentTime = 0;
            SecondsTimer = 0;
        }
        
        /// <summary>
        /// Activate different States of a timer
        /// </summary>
        /// <param name="timerState">Current State of Timer</param>
        /// <param name="gameTime">Needs GameTime</param>
        private void ActivateTimerType(TimerState timerState,GameTime gameTime)
        {
            switch (timerState)
            {
                case TimerState.Play:
                    CurrentTime += (float)gameTime.ElapsedGameTime.Milliseconds/1000;
                    break;
                case TimerState.Stop:
                    IsTimeUp = true;
                    break;
                case TimerState.Reset:
                    CurrentTime = 0;
                    IsTimeUp = false;
                    break;
            }
        }
        public void ResetTimer(GameTime gameTime)
        {
            ActivateTimerType(TimerState.Reset,gameTime);
        }
        /// <summary>
        /// A Timer that plays only once
        /// </summary>
        /// <param name="seconds">seconds before timer ends</param>
        /// <param name="gameTime">needs GameTime</param>
        public void PlayTimerOnce(float seconds,GameTime gameTime)
        {
            SecondsTimer = seconds;
            if (CurrentTime < SecondsTimer)
            {
                ActivateTimerType(TimerState.Play, gameTime);
            }
            else
            {
                ActivateTimerType(TimerState.Stop, gameTime);
            }
        }
        /// <summary>
        /// Continuous Timer 
        /// </summary>
        /// <param name="seconds">seconds before timer ends</param>
        /// <param name="gameTime">needs GameTime</param>
        public void PlayTimerContinuous(float seconds, GameTime gameTime)
        {
            SecondsTimer = seconds;
            if (CurrentTime < SecondsTimer)
            {
                ActivateTimerType(TimerState.Play, gameTime);
            }
            else if (!IsTimeUp && CurrentTime >= SecondsTimer)
            {
                IsTimeUp = true;
            }
            else if (IsTimeUp && CurrentTime >= SecondsTimer)
            {
                ActivateTimerType(TimerState.Reset, gameTime);
                ActivateTimerType(TimerState.Play, gameTime);
            }
        }
        /// <summary>
        /// Creates multiple instances of Timers (use Timers to access them) ->place in init
        /// </summary>
        /// <param name="numoftimers">Number of timers instances to make</param>
        public void NumberofTimers(int numoftimers)
        {
            Timer t;
            for (int i = 0; i < numoftimers; i++)
            {
                t = new Timer();
                Timers.Add(t);
            }
        }
        public override string ToString()
        {
            return String.Format("Current Time: {0}", CurrentTime);
        }
    }
}
