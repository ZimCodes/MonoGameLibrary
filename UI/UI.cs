using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGameLibrary.UI
{
    /// <summary>
    /// The UI Abstract class
    /// </summary>
    public abstract class UI : IUI
    {
        protected Game game;
        protected string uiName, textureName;
        protected Vector2 position, origin;
        protected Color color;
        protected float rotation, scale, layerDepth;
        protected SpriteEffects effect;

        private Vector2 positionDefault, originDefault;
        private Color colorDefault;
        private string textureDefault;
        private float rotationDefault, scaleDefault, layerDepthDefault;
        private SpriteEffects effectDefault;
        public string UIName { get { return uiName; } }

        public bool isActive { get; private set; }

        public UI(Game game, string uiName, string textureName, Vector2 position, Color color, Vector2 origin, float rotateInDegrees = 0, float scale = 1, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0, bool isactive = true) {
            this.game = game;
            this.uiName = uiName;
            this.isActive = isactive;

            InitDefaults(textureName, position,color,origin,rotateInDegrees,scale,effects,layerDepth);
        }
        /// <summary>
        /// Set the default values from the ctor
        /// </summary>
        /// <param name="textureName">Name of texture</param>
        /// <param name="position">Location to set the element</param>
        /// <param name="color">Color of the element</param>
        /// <param name="origin">The rotation origin</param>
        /// <param name="rotateInDegrees">Rotation of element in degrees</param>
        /// <param name="scale">Multiplier size of the element</param>
        /// <param name="effects">The sprite effects</param>
        /// <param name="layerDepth">The layer to use</param>
        private void InitDefaults(string textureName, Vector2 position, Color color,  Vector2 origin, float rotateInDegrees = 0, float scale = 1, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0)
        {
            this.textureName = textureName;
            this.position = position;
            this.color = color;
            this.origin = origin;
            this.rotation = rotateInDegrees;
            this.scale = scale;
            this.effect = effects;
            this.layerDepth = layerDepth;

            this.textureDefault = textureName;
            this.positionDefault = position;
            this.colorDefault = color;
            this.originDefault = origin;
            this.rotationDefault = rotateInDegrees;
            this.scaleDefault = scale;
            this.effectDefault = effects;
            this.layerDepthDefault = layerDepth;
        }
        public virtual void ResetToDrawDefault()
        {
            this.textureName = this.textureDefault;
            this.position = this.positionDefault;
            this.color = this.colorDefault;
            this.origin = this.originDefault;
            this.rotation = this.rotationDefault;
            this.scale = this.scaleDefault;
            this.effect = this.effectDefault;
            this.layerDepth = this.layerDepthDefault;
            this.LoadContent();
        }
        public void Draw(SpriteBatch sb)
        {
            if (isActive)
            {
                this.SetDrawMethod(sb);
            }
        }
        /// <summary>
        /// Set the draw methods needed to be implemented in the Draw().
        /// </summary>
        /// <param name="sb">SpriteBatch reference</param>
        protected abstract void SetDrawMethod(SpriteBatch sb);
        public void Deactivate()
        {
            this.isActive = false;
        }
        public void Activate()
        {
            this.isActive = true;
        }

        public abstract void LoadContent();
        public void ReloadContent(string texture)
        {
            this.textureName = texture;
            this.LoadContent();
        }
        public void ReloadContent(Vector2 position,Color color)
        {
            this.position = position;
            this.color = color;
            this.LoadContent();
        }
        public void ReloadContent(string texture, Vector2 position, Color color)
        {
            this.textureName = texture;
            this.position = position;
            this.color = color;
            this.LoadContent();
        }
    }
}
