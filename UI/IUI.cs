using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGameLibrary.UI
{
    public interface IUI
    {
        /// <summary>
        /// Reset draw properties back to when it was first initialized 
        /// </summary>
        void ResetToDrawDefault();
        /// <summary>
        /// Load graphical resources to be drawn
        /// </summary>
        void LoadContent();
        /// <summary>
        /// Draws the UI element
        /// </summary>
        /// <param name="sb">SpriteBatch reference</param>
        void Draw(SpriteBatch sb);
        /// <summary>
        /// Prevents the UI Element from being drawn
        /// </summary>
        void Deactivate();
        /// <summary>
        /// Allows the UI Element to be drawn
        /// </summary>
        void Activate();
        /// <summary>
        /// Reloads graphical resources based on new properties
        /// </summary>
        /// <param name="texture">Name of the texture to use</param>
        void ReloadContent(string texture);
        /// <summary>
        /// Reloads graphical resources based on new properties
        /// </summary>
        /// <param name="position">New position to use</param>
        /// <param name="color">New color to use</param>
        void ReloadContent(Vector2 position, Color color);
        /// <summary>
        /// Reloads graphical resources based on new properties
        /// </summary>
        /// <param name="texture">Name of the texture to use</param>
        /// <param name="position">New position to use</param>
        /// <param name="color">New color to use</param>
        void ReloadContent(string texture, Vector2 position, Color color);
    }
}
