using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Util.MathUtil;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.SceneManagement.Service
{
    /// <summary>
    /// Loads a texture to drawn with a Parallax Effect. This class only needs the name of the texture to run.
    /// </summary>
    public class SoloParallaxSceneLoader : SceneLoader
    {
        protected Vector2 Direction;
        protected float scrollSpeed;
        protected const float DEFAULT_SPEED = -0.5f;
        protected Vector2 DEFAULT_DIR = VectorUtil.Left;
        private Vector2 currentOffset;
        private Rectangle currentRect, nextRect, prevRect;
        private Rectangle prevTopRect, nextTopRect, currentTopRect;
        private Rectangle prevBottomRect, nextBottomRect, currentBottomRect;
        #region Constructors
        public SoloParallaxSceneLoader(Game game) : base(game)
        {
            this.Direction = DEFAULT_DIR;
            this.scrollSpeed = DEFAULT_SPEED;
        }
        public SoloParallaxSceneLoader(Game game, string _texture) : base(game,_texture)
        {
            this.Direction = DEFAULT_DIR;
            this.scrollSpeed = DEFAULT_SPEED;
        }
        public SoloParallaxSceneLoader(Game game, string _texture, Vector2 _dir,float _speed):base(game,_texture)
        {
            this.Direction = _dir;
            this.scrollSpeed = _speed;
        }
#endregion
        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            currentOffset = Vector2.Zero;
            DrawRectangles();
            base.LoadContent();
        }
        /// <summary>
        /// Loads a new parallax scene
        /// </summary>
        /// <param name="_texture">Name of scene texture to load</param>
        /// <param name="_dir">The direction the scene needs to move</param>
        /// <param name="_speed">How fast the scene should be moving</param>
        public void ReLoadContent(string _texture, Vector2 _dir, float _speed)
        {
            this.textureName = _texture;
            this.Direction = _dir;
            this.scrollSpeed = _speed;
            this.LoadContent();
        }
        /// <summary>
        /// Draw the textures for the parallax effect
        /// </summary>
        private void DrawRectangles()
        {
            
            this.currentBottomRect = new Rectangle((int)this.currentOffset.X, (int)this.currentOffset.Y, this.Game.GraphicsDevice.Viewport.Width, this.Game.GraphicsDevice.Viewport.Height);
            this.prevBottomRect = new Rectangle((int)this.currentOffset.X - this.Game.GraphicsDevice.Viewport.Width, (int)this.currentOffset.Y + this.Game.GraphicsDevice.Viewport.Height, this.Game.GraphicsDevice.Viewport.Width, this.Game.GraphicsDevice.Viewport.Height);
            this.nextBottomRect = new Rectangle((int)this.currentOffset.X + this.Game.GraphicsDevice.Viewport.Width, (int)this.currentOffset.Y + this.Game.GraphicsDevice.Viewport.Height, this.Game.GraphicsDevice.Viewport.Width, this.Game.GraphicsDevice.Viewport.Height);

            this.currentRect = new Rectangle((int)this.currentOffset.X, (int)this.currentOffset.Y, this.Game.GraphicsDevice.Viewport.Width, this.Game.GraphicsDevice.Viewport.Height);
            this.prevRect = new Rectangle((int)this.currentOffset.X - this.Game.GraphicsDevice.Viewport.Width, (int)this.currentOffset.Y, this.Game.GraphicsDevice.Viewport.Width, this.Game.GraphicsDevice.Viewport.Height);
            this.nextRect = new Rectangle((int)this.currentOffset.X + this.Game.GraphicsDevice.Viewport.Width, (int)this.currentOffset.Y, this.Game.GraphicsDevice.Viewport.Width, this.Game.GraphicsDevice.Viewport.Height);

            this.prevTopRect = new Rectangle((int)this.currentOffset.X - this.Game.GraphicsDevice.Viewport.Width, (int)this.currentOffset.Y - this.Game.GraphicsDevice.Viewport.Height, this.Game.GraphicsDevice.Viewport.Width, this.Game.GraphicsDevice.Viewport.Height);
            this.currentTopRect = new Rectangle((int)this.currentOffset.X, (int)this.currentOffset.Y - this.Game.GraphicsDevice.Viewport.Height, this.Game.GraphicsDevice.Viewport.Width, this.Game.GraphicsDevice.Viewport.Height);
            this.nextTopRect = new Rectangle((int)this.currentOffset.X + this.Game.GraphicsDevice.Viewport.Width, (int)this.currentOffset.Y - this.Game.GraphicsDevice.Viewport.Height, this.Game.GraphicsDevice.Viewport.Width, this.Game.GraphicsDevice.Viewport.Height);
        }
        public override void Update(GameTime gameTime)
        {
            DrawRectangles();
            MoveBackGround(gameTime);
            base.Update(gameTime);
        }
        /// <summary>
        /// Determine how the scene texture should move. Even when the texture has moved offscreen
        /// </summary>
        /// <param name="gameTime">GameTime reference for time</param>
        private void MoveBackGround(GameTime gameTime)
        {
            this.currentOffset = this.currentOffset + this.Direction * this.scrollSpeed * (gameTime.ElapsedGameTime.Milliseconds / 10);
            //Right Direction -->
            if (this.currentOffset.X > this.Game.GraphicsDevice.Viewport.Width)
            {
                this.currentOffset.X = 0;
            }
            //Left Direction <--
            if (this.currentOffset.X < -this.Game.GraphicsDevice.Viewport.Width)
            {
                this.currentOffset.X = 0;
            }
            if (this.currentOffset.Y > this.Game.GraphicsDevice.Viewport.Height)
            {
                this.currentOffset.Y = 0;
            }
            if (this.currentOffset.Y < -this.Game.GraphicsDevice.Viewport.Height)
            {
                this.currentOffset.Y = 0;
            }
        }
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(this.texture, prevRect, Color.White);
            sb.Draw(this.texture, currentRect, Color.White);
            sb.Draw(this.texture, nextRect, Color.White);

            sb.Draw(this.texture, prevTopRect, Color.White);
            sb.Draw(this.texture, currentTopRect, Color.White);
            sb.Draw(this.texture, nextTopRect, Color.White);

            sb.Draw(this.texture, prevBottomRect, Color.White);
            sb.Draw(this.texture, currentBottomRect, Color.White);
            sb.Draw(this.texture, nextBottomRect, Color.White);
            sb.End();
            
        }

    }
}
