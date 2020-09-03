using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGameLibrary.UI
{
    sealed class TextUI : UI
    {
        SpriteFont font;
        private string text, textDefault;
        
        public TextUI(Game game,string uiName, string fontTextureName, string text, Vector2 position, Color color, Vector2 origin, float rotateInDegrees = 0, float scale = 1, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0,bool isactive = true) 
            :base(game,uiName,fontTextureName,position,color,origin,rotateInDegrees,scale,effects,layerDepth,isactive)
        {
            this.text = text;
            this.textDefault = text;
        }
        public override void LoadContent()
        {
            font = game.Content.Load<SpriteFont>(this.textureName);
        }
        protected override void SetDrawMethod(SpriteBatch sb)
        {
            sb.DrawString(this.font, this.text, this.position, this.color, this.rotation, this.origin, this.scale, this.effect, this.layerDepth);
        }
        public override void ResetToDrawDefault()
        {
            this.text = this.textDefault;
            base.ResetToDrawDefault();
        }


    }
}
