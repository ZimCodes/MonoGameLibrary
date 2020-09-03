using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;

namespace MonoGameLibrary.SceneManagement.Service
{
    public interface ISceneLoader
    {
        /// <summary>
        /// Loads a new scene texture to be drawn
        /// </summary>
        /// <param name="_texture">The texture name to be drawn</param>
        void ReLoadContent(string _texture);
        /// <summary>
        /// Disables Update()
        /// </summary>
        void Pause();
        /// <summary>
        /// Enables Update() 
        /// </summary>
        void Play();
        /// <summary>
        /// Prevent Update() and Draw() methods from being called
        /// </summary>
        void Deactivate();
        /// <summary>
        /// Allow Update() and Draw() methods to be called
        /// </summary>
        void Activate();
    }
    public class SceneLoader : DrawableGameComponent, ISceneLoader
    {
        protected SpriteBatch sb;
        protected Texture2D texture;
        protected string textureName;
        
        public SceneLoader(Game game):base(game)
        {
            // Adds loader class as a service.
            game.Services.AddService(typeof(ISceneLoader), this);
        }
        public SceneLoader(Game game, string texture) : base(game)
        {
            this.textureName = texture;
            // Adds loader class as a service.
            game.Services.AddService(typeof(ISceneLoader), this);
        }
        public override void Initialize()
        {
            //Create spritebatch
            this.sb = new SpriteBatch(this.Game.GraphicsDevice);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            this.texture = this.Game.Content.Load<Texture2D>(textureName);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        
        public virtual void ReLoadContent(string _texture)
        {
            this.textureName = _texture;
            this.LoadContent();
        }
        
        public virtual void Pause()
        {
            this.Enabled = false;
        }
        
        public virtual void Play()
        {
            this.Enabled = true;
        }
        
        public virtual void Activate()
        {
            this.Enabled = this.Visible = true;
        }
        
        public virtual void Deactivate()
        {
            this.Enabled = this.Visible = false;
        }
        
    }
}
