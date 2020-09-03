using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.SceneManagement.Service;
using MonoGameLibrary.Util;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MonoGameLibrary.SceneManagement
{
    public interface IScene
    {
        /// <summary>
        /// Content to be initialized
        /// </summary>
        void LoadContent();
        /// <summary>
        /// Content to free or disable temporary
        /// </summary>
        void UnLoadContent();
        /// <summary>
        /// Updates the scene 
        /// </summary>
        /// <param name="gameTime">GameTime reference</param>
        /// <param name="loader">The type of loader to use</param>
        /// <param name="switcher">SceneSwitcher reference</param>
        void Update(GameTime gameTime, ISceneSwitchLoad loader,SceneSwitcher switcher);
        /// <summary>
        /// Draws the scene
        /// </summary>
        /// <param name="sb"></param>
        void Draw(SpriteBatch sb);
        /// <summary>
        /// Name of the texture to load
        /// </summary>
        string TextureName { get; }
    }
    /// <summary>
    /// Use to build new Scene objects
    /// </summary>
    public abstract class Scene : IScene
    {
        protected string nextScene;
        protected string textureName;
        public string TextureName
        {
            get { return this.textureName; }
        }

        public Scene(Game game,string texture)
        {
            this.textureName = texture;
        }

        public abstract void LoadContent();
        public abstract void UnLoadContent();
        public abstract void Update(GameTime gameTime, ISceneSwitchLoad loader, SceneSwitcher switcher);
        public abstract void Draw(SpriteBatch sb);
    }
}
