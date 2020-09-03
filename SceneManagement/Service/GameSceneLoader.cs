using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGameLibrary.SceneManagement.Service
{
    public interface ISceneSwitchLoad
    {
        /// <summary>
        /// Set the next Scene to load
        /// </summary>
        /// <param name="_newscene">The scene to load</param>
        void NextScene(IScene _newscene);
        /// <summary>
        /// Change to a different SceneSwitcher
        /// </summary>
        /// <param name="switcher">The SceneSwitcher to use</param>
        void ChangeSceneSwitcher(SceneSwitcher switcher);
    }
    /// <summary>
    /// Specifically loads Scene objects 
    /// </summary>
    public class GameSceneLoader:SceneLoader,ISceneSwitchLoad
    {
        private SceneSwitcher switcher;
        public GameSceneLoader(Game game, SceneSwitcher _switch) : base(game)
        {
            this.switcher =  _switch;
            this.textureName = this.switcher.CurrentScene.TextureName;
        }
        public override void Update(GameTime gameTime)
        {
            this.switcher.CurrentScene.Update(gameTime,this,this.switcher);
            base.Update(gameTime);
        }
        
        public override void Draw(GameTime gameTime)
        {
            this.switcher.CurrentScene.Draw(this.sb);
            sb.Begin();
            sb.Draw(this.texture, new Rectangle(this.Game.GraphicsDevice.Viewport.X, this.Game.GraphicsDevice.Viewport.Y, this.Game.GraphicsDevice.Viewport.Width, this.Game.GraphicsDevice.Viewport.Height), Color.White);
            sb.End();
            base.Draw(gameTime);
        }
        
        public void NextScene(IScene _newscene)
        {
            IScene newscene = _newscene;
            this.ReLoadContent(newscene.TextureName);
            newscene.LoadContent();
        }
        public void ChangeSceneSwitcher(SceneSwitcher _switcher)
        {
            this.switcher = _switcher;
        }
    }
}
