using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using MonoGameLibrary.Sprite.Service;

namespace MonoGameLibrary.Sprite
{
    /// <summary>
    /// This is an Extension of Drawable sprite. Each Sprite has
    /// an animation adapter that can manage animations
    /// </summary>
    public class DrawableAnimatableSprite : DrawableSprite
    {

        protected SpriteAnimationAdapter spriteAnimationAdapter;
        Rectangle currentTextureRect;

        public DrawableAnimatableSprite(Game game)
            : base(game)
        {
            spriteAnimationAdapter = new SpriteAnimationAdapter(game, this);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteAnimationAdapter.LoadContent();
            base.LoadContent();
        }

        
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //Elapsed time since last update
            lastUpdateTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            
            SpriteEffects = SpriteEffects.None;       //Default Sprite Effects
            if(this.spriteAnimationAdapter.HasAnimations)
                this.spriteTexture = this.spriteAnimationAdapter.CurrentTexture;        //update texture for collision
               
            base.Update(gameTime);

            currentTextureRect = spriteAnimationAdapter.GetCurrentDrawRect(lastUpdateTime, this.scale);
            SetTranformAndRect();
            //HACK
            this.SpriteTextureData = new Color[this.spriteAnimationAdapter.CurrentTexture.Width * this.spriteAnimationAdapter.CurrentTexture.Height];
            this.spriteAnimationAdapter.CurrentTexture.GetData(this.SpriteTextureData);

        }

        public override void SetTranformAndRect()
        {
            try
            {
                // Build the block's transform
                spriteTransform =
                    Matrix.CreateTranslation(new Vector3(this.Origin * -1, 0.0f)) *
                    Matrix.CreateScale(this.Scale) *
                    Matrix.CreateRotationZ(0.0f) *
                    Matrix.CreateTranslation(new Vector3(this.Location, 0.0f));

                // Calculate the bounding rectangle of this block in world space
                this.locationRect = CalculateBoundingRectangle(
                         new Rectangle(0, 0, this.currentTextureRect.Width,
                             this.currentTextureRect.Height),
                         spriteTransform);
            }
            catch (NullReferenceException nu)
            {
                //nothing
                if (this.spriteTexture == null)
                {
                    //first time this will fail because load content hasn't been called yet
                }
                else
                {
                    throw nu;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(spriteAnimationAdapter.CurrentTexture,
                new Rectangle((int)Location.X, (int)Location.Y,
                (int)(currentTextureRect.Width * this.scale),
                (int)(currentTextureRect.Height* this.scale)),
                currentTextureRect,
                Color.White,
                MathHelper.ToRadians(Rotate),
                this.Origin,
                SpriteEffects,
                0);
            

            base.DrawMarkers(spriteBatch);

            spriteBatch.End();
        }

        /// <summary>
        /// Checks if this sprites pixels intersect with another sprite
        /// This is more painful than checking rectangles
        /// </summary>
        /// <param name="OtherSprite"></param>
        /// <returns></returns>
        public override bool PerPixelCollision2(Sprite OtherSprite)
        {
            return IntersectPixels(this.spriteTransform, 
                this.currentTextureRect.Width,
                this.currentTextureRect.Height, 
                this.SpriteTextureData,
                OtherSprite.spriteTransform,
                OtherSprite.SpriteTexture.Width,
                OtherSprite.SpriteTexture.Height,
                OtherSprite.SpriteTextureData);
        }
    }

    public class SpriteAnimationAdapter
    {
        List<SpriteAnimation> spriteAnimations;
        protected SpriteAnimation currentAnimation;
        protected CelAnimationManager celAnimationManager;

        protected Texture2D defaultTexture; //only used if no sprite animations are set
        protected Sprite parent;
        public bool HasAnimations
        {
            get
            {
                if (currentAnimation != null)
                    return true;
                return false;
            }
        }

        public Rectangle CurrentLocationRect
        {
            get
            {
                return this.GetCurrentDrawRect();
            }
        }

        public CelAnimationManager CelAnimationManager { get { return celAnimationManager;}}
        public SpriteAnimation CurrentAnimation {
            get { return currentAnimation; }
            set {
                    if(!(spriteAnimations.Contains(value)))
                    {
                        this.spriteAnimations.Add(value);
                    }
                        this.currentAnimation = value;
            }
        }
              
        public SpriteAnimationAdapter(Game game, Sprite sprite)
        {
            this.parent = sprite;
            spriteAnimations = new List<SpriteAnimation>();
            
            celAnimationManager = (CelAnimationManager)game.Services.GetService(typeof(ICelAnimationManager));
            if (celAnimationManager == null)
            {
                //throw new Exception("To use a DrawableAnimatedSprite you must a CelAnimationManager to the game as a service!");
                celAnimationManager = new CelAnimationManager(game);
                game.Components.Add(celAnimationManager);
            }   
        }

        public void LoadContent()
        {
            this.defaultTexture = parent.SpriteTexture;
            if (parent.SpriteTexture == null)
            {
                parent.Initialize();
                this.defaultTexture = parent.SpriteTexture;
            }
        }

        public Texture2D CurrentTexture
        {
            get {
                if(currentAnimation == null)
                {
                    return this.defaultTexture;
                }
                return celAnimationManager.GetTexture(currentAnimation.TextureName); }
        }
        /// <summary>
        /// Adds the sprite animation to the List
        /// </summary>
        /// <param name="s">SpriteAnimation reference</param>
        public void AddAnimation(SpriteAnimation s)
        {
            this.spriteAnimations.Add(s);
            this.celAnimationManager.AddAnimation(s.AnimationName, s.TextureName, s.CellCount, s.FPS);
            this.celAnimationManager.ToggleAnimation(s.AnimationName, false);
            if (spriteAnimations.Count == 1)
            {
                currentAnimation = s;
            }
        }
        /// <summary>
        /// Adds the sprite animation to the List
        /// </summary>
        /// <param name="animationName">Name of the animation</param>
        /// <param name="textureName">Name of the texture to load</param>
        /// <param name="fps">Frames per second the animation should play</param>
        /// <param name="numberOfCols">Number of columns on spritesheet</param>
        /// <param name="numberOfRows">Number of rows on spritesheet</param>
        public void AddAnimation(string animationName, string textureName,
            int fps, int numberOfCols, int numberOfRows)
        {
            SpriteAnimation s = new SpriteAnimation(animationName,textureName,fps,numberOfCols,numberOfRows);
            this.spriteAnimations.Add(s);
            this.celAnimationManager.AddAnimation(s.AnimationName, s.TextureName, s.CellCount, s.FPS);
            this.celAnimationManager.ToggleAnimation(s.AnimationName, false);
            if (spriteAnimations.Count == 1)
            {
                currentAnimation = s;
            }
        }
        /// <summary>
        /// Resets the animation of the Sprite
        /// </summary>
        /// <param name="s">SpriteAnimation reference</param>
        public void ResetAnimation(SpriteAnimation s)
        {

            this.celAnimationManager.ResetAnimation(s.AnimationName);
        }
        /// <summary>
        /// Reset the animation of the Sprite
        /// </summary>
        /// <param name="_name">Name of animation to reset</param>
        public void ResetAnimation(string _name)
        {
            this.celAnimationManager.ResetAnimation(_name);
        }
        /// <summary>
        /// Removes the sprite animation from the List
        /// </summary>
        /// <param name="s">SpriteAnimation reference</param>
        public void RemoveAnimation(SpriteAnimation s)
        {
            this.spriteAnimations.Remove(s);
            this.celAnimationManager.Animations.Remove(s.AnimationName);
        }
        /// <summary>
        /// Removes the sprite animation from the List
        /// </summary>
        /// <param name="_name">Name of animation to remove</param>
        public void RemoveAnimation(string _name)
        {
            SpriteAnimation anim = this.spriteAnimations.Find(x => x.AnimationName == _name);
            this.spriteAnimations.Remove(anim);
            this.celAnimationManager.Animations.Remove(anim.AnimationName);
        }
        /// <summary>
        /// Pauses the animation of the sprite
        /// </summary>
        /// <param name="s">SpriteAnimation reference</param>
        public void PauseAnimation(SpriteAnimation s)
        {
            this.celAnimationManager.ToggleAnimation(s.AnimationName, true);
        }
        /// <summary>
        /// Pauses the animation of the sprite
        /// </summary>
        /// <param name="_name">Name of animation to pause</param>
        public void PauseAnimation(string _name)
        {
            this.celAnimationManager.ToggleAnimation(_name, true);
        }
        /// <summary>
        /// Go to a specific frame in the animation
        /// </summary>
        /// <param name="s">SpriteAnimation reference</param>
        /// <param name="frameindex">Frame to show. Index starts at 0</param>
        public void GoToFrame(SpriteAnimation s, int frameindex)
        {
            this.celAnimationManager.GoToFrame(s.AnimationName, frameindex);
        }
        /// <summary>
        /// Go to a specific frame in the animation
        /// </summary>
        /// <param name="_name">Name of the sprite animation</param>
        /// <param name="frameindex">Frame to show. Index starts at 0</param>
        public void GoToFrame(string _name, int frameindex)
        {
            this.celAnimationManager.GoToFrame(_name, frameindex);
        }
        /// <summary>
        /// Resumes the animation of the sprite
        /// </summary>
        /// <param name="s">SpriteAnimation reference</param>
        public void ResumeAnimation(SpriteAnimation s)
        {
            this.celAnimationManager.ToggleAnimation(s.AnimationName, false);
        }
        /// <summary>
        /// Resumes the animation of the sprite
        /// </summary>
        /// <param name="_name">Name of animation to resume</param>
        public void ResumeAnimation(string _name)
        {
            this.celAnimationManager.ToggleAnimation(_name, false);
        }
        public Rectangle GetCurrentDrawRect(float elapsedTime, float scale)
        {
            Rectangle drawRect;
            if (this.HasAnimations)
                drawRect = this.CelAnimationManager.GetCurrentDrawRect(elapsedTime, currentAnimation.AnimationName, scale);
            else
                drawRect = defaultTexture.Bounds;
            return drawRect;
        }

        public Rectangle GetCurrentDrawRect(float elapsedTime)
        {
            return GetCurrentDrawRect(0.0f, 0.0f);
        }

        public Rectangle GetCurrentDrawRect()
        {
            return GetCurrentDrawRect(0.0f);
        }
        /// <summary>
        /// Retrieve the amount of times the animation has been looped through
        /// </summary>
        /// <returns>Number of times animation has been looped through</returns>
        public int GetLoopCount()
        {
            return this.celAnimationManager.Animations[currentAnimation.AnimationName].LoopCount;
        }

        
    }

    public class SpriteAnimation 
    {

        public string AnimationName;
        public int FPS;
        public string TextureName;
        public CelCount CellCount;

        protected bool isPaused;
        public bool IsPaused { get { return isPaused;} set { isPaused = value;} }

        public SpriteAnimation(string animationName, string textureName,
            int fps,  int numberOfCols, int numberOfRows  )
        {
            this.AnimationName = animationName;
            this.FPS = fps;
            this.TextureName = textureName;
            this.CellCount = new CelCount(numberOfCols,numberOfRows);
            isPaused = true;
        }

    }
}