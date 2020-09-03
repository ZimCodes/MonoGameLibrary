using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Spawn
{
    /// <summary>
    /// Spawn Objects continuously every 'x' seconds
    /// </summary>
    public class TimedSpawn:Spawner
    {
        private Timer timer;
        protected float SpawnEverySecond = 3;
        public TimedSpawn(Game game):base(game)
        {
            timer = new Timer();
        }
        public TimedSpawn(Game game,float _spawninseconds):base(game)
        {
            timer = new Timer();
            this.SpawnEverySecond = _spawninseconds;
        }
        public override void Update(GameTime gameTime)
        {
            SpawnTimer(gameTime);
            base.Update(gameTime);
        }
        public virtual void SpawnTimer(GameTime gameTime)
        {
            timer.PlayTimerContinuous(SpawnEverySecond, gameTime);
            if (timer.IsTimeUp)
            {
                this.Spawn();
            }
        }
    }
}
