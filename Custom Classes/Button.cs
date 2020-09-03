using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using MonoGameLibrary.Util.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGameLibrary.Custom_Classes
{
    /// <summary>
    /// Sprite class for building buttons
    /// </summary>
    public class Button : DrawableAnimatableSprite
    {
        InputHandler input;
        public Button(Game game):base(game)
        {
            input = GameCompUtil.GetService<InputHandler, IInputHandler>(game);
        }
        /// <summary>
        /// Instantiate SpriteAnimation objects here
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }
        /// <summary>
        ///  Tells you if Button was clicked
        /// </summary>
        /// <param name="button">The button you want to read</param>
        /// <returns>returns whether or not Button was clicked</returns>
        protected bool ButtonClick(MouseButtons button)
        {
            
            if (input.MouseState.WasMouseBtnPressed(button) && WithinHotSpot())
            {
                
                return true;
            }
            
            return false;
        }
        /// <summary>
        /// Tells if the user is holding a mouse button on the button texture 
        /// </summary>
        /// <param name="button">The button you want to read</param>
        /// <returns>returns whether or not Button is being held down</returns>
        protected bool ButtonHolding(MouseButtons button)
        {

            if (input.MouseState.IsMouseDown(button) && WithinHotSpot())
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Generates a hotspot on a button texture
        /// </summary>
        /// <returns>Whether mouse is within the hotspot of the button</returns>
        private bool WithinHotSpot()
        {
            Vector2 starthotspot = new Vector2(this.Location.X, this.Location.Y);
            Vector2 endhotspot = new Vector2(this.Location.X+ this.spriteAnimationAdapter.CurrentLocationRect.Width, this.Location.Y+ this.spriteAnimationAdapter.CurrentLocationRect.Height);
            if (input.MouseState.mouseState.Position.X >= starthotspot.X && input.MouseState.mouseState.Position.X <= endhotspot.X
                && input.MouseState.mouseState.Position.Y >= starthotspot.Y && input.MouseState.mouseState.Position.Y <= endhotspot.Y)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Tells whether or not the mouse if hovering over a button texture
        /// </summary>
        /// <returns>whether mouse is hovering over button</returns>
        protected bool Hover()
        {
            return WithinHotSpot();
        }
    }
}
