using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGameLibrary.SceneManagement.Service
{
    /// <summary>
    /// Specifically loads Scene objects to be drawn with the Parallax Effect
    /// </summary>
    public class ParallaxSceneLoader: SoloParallaxSceneLoader, ISceneSwitchLoad
    {
        SceneSwitcher switcher;
        public ParallaxSceneLoader(Game game, SceneSwitcher _switcher) : base(game)
        {
            this.switcher = _switcher;
            this.textureName = this.switcher.CurrentScene.TextureName;
        }
        public ParallaxSceneLoader(Game game, SceneSwitcher _switcher, Vector2 _dir, float _speed):base(game) 
        {
            this.switcher = _switcher;
            this.Direction = _dir;
            this.scrollSpeed = _speed;
            this.textureName = this.switcher.CurrentScene.TextureName;
        }
        public override void Update(GameTime gameTime)
        {
            this.switcher.CurrentScene.Update(gameTime, this, this.switcher);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            this.switcher.CurrentScene.Draw(this.sb);
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
