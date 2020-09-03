using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGameLibrary.UI
{
    class ImageUI : UI
    {
        Texture2D image;
        Rectangle rectangle;
        public ImageUI(Game game, string uiName, string imageTextureName, Vector2 position, Color color, Vector2 origin, float rotateInDegrees = 0, float scale = 1, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0, bool isactive = true)
            : base(game, uiName, imageTextureName,position, color, origin, rotateInDegrees, scale, effects, layerDepth, isactive)
        {
            
        }
        public override void LoadContent()
        {
            this.image = this.game.Content.Load<Texture2D>(this.textureName);
            this.LoadRectForDrawing();
        }
        protected override void SetDrawMethod(SpriteBatch sb)
        {
            sb.Draw(this.image,this.rectangle,null,this.color,this.rotation,this.origin,this.effect,this.layerDepth);
        }
        /// <summary>
        /// Load the drawing rectangle for the image element
        /// </summary>
        private void LoadRectForDrawing()
        {
            this.rectangle.X = (int)this.position.X;
            this.rectangle.Y = (int)this.position.Y;
            this.rectangle.Width = (int)(this.image.Width * this.scale);
            this.rectangle.Height = (int)(this.image.Height * this.scale);
        }
    }
}
