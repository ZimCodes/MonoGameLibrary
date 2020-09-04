using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGameLibrary.UI
{

    public class UIManager:DrawableGameComponent
    {
        List<UI> uiItems;
        SpriteBatch sb;
        public UIManager(Game game, IEnumerable<UI> uiElements):base(game)
        {
            this.DrawOrder = 2;
            uiItems = new List<UI>();
            uiItems.AddRange(uiElements);
        }
        public override void Initialize()
        {
            sb = new SpriteBatch(this.GraphicsDevice);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            uiItems.ForEach(x => x.LoadContent());
            base.LoadContent();
        }
        
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            uiItems.ForEach(x => x.Draw(sb));
            sb.End();
            base.Draw(gameTime);
        }
        public void ReloadContent(string uiName, string newTexture)
        {
            this.uiItems.Find(x => x.UIName == uiName).ReloadContent(newTexture);
        }
        public void ReloadContent(string uiName,Vector2 newPosition,Color newColor)
        {
            this.uiItems.Find(x => x.UIName == uiName).ReloadContent(newPosition, newColor);
        }
        public void ReloadContent(string uiName, string newTexture, Vector2 newPosition, Color newColor)
        {
            this.uiItems.Find(x => x.UIName == uiName).ReloadContent(newTexture, newPosition, newColor);
        }
        public void ResetToDefault(string uiName)
        {
            this.uiItems.Find(x=>x.UIName == uiName).ResetToDrawDefault();
        }
        public void ResetAllToDefault()
        {
            this.uiItems.ForEach(x => x.ResetToDrawDefault());
        }
        public void Deactivate(string uiName)
        {
            this.uiItems.Find(x => x.UIName == uiName).Deactivate();
        }
        public void DeactivateAll()
        {
            this.uiItems.ForEach(x => x.Deactivate());
        }
        public void Activate(string uiName)
        {
            this.uiItems.Find(x => x.UIName == uiName).Activate();
        }
        public void ActivateAll()
        {
            this.uiItems.ForEach(x => x.Activate());
        }
        public bool isUIActive(string uiName)
        {
            return this.uiItems.Find(x => x.UIName == uiName).isActive;
        }
        public void Clear()
        {
            this.uiItems.Clear();
        }
    }
}
