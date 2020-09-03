using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Spawn
{
    interface ISpawner
    {
        GameComponent Instance { get; set; }
        void Spawn();
    }
    public class Spawner : GameComponent, ISpawner
    {
        private GameComponent instance;
        public GameComponent Instance {
            get {
                if (instance == null)
                {
                    instance = new GameComponent(this.Game);
                    instance.Initialize();
                }
                return instance;
            }
            set { instance = value; }
        }
        public Spawner(Game game):base(game)
        {

        }
        public virtual void Spawn()
        {
            if (instance != null)
            {
                this.Game.Components.Add(instance);
            }
        }
    }
}
