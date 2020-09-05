using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MonoGameLibrary.Audio
{
    /// <summary>
    /// An Wrapper class to combine SoundEffect & SoundEffectInstance 
    /// </summary>
    public class SFX
    {
        private SoundEffectInstance sfxInstance;
        private SoundEffect sfx;
        /// <summary>
        /// Gets the name of the asset.
        /// </summary>
        public string Name { get { return this.sfx.Name; } }
        /// <summary>
        /// Gets the duration of the sound effect.
        /// </summary>
        public TimeSpan Duration { 
            get { 
                return this.sfx.Duration; 
            } 
        }
        /// <summary>
        /// Change the volume of all sound effect instances.
        /// </summary>
        public static float MasterVolume
        {
            get{return SoundEffect.MasterVolume;}
            set
            {
                SoundEffect.MasterVolume = value;
            }
        }
        /// <summary>
        /// Enables or Disables whether the sound effect should repeat.
        /// </summary>
        public bool isLooping { get { return this.sfxInstance.IsLooped; } set { this.sfxInstance.IsLooped = value; } }
        /// <summary>
        /// Gets or sets the pan, or speaker balance.
        /// </summary>
        public float Pan { get { return this.sfxInstance.Pan; } set { this.sfxInstance.Pan = value; } }
        /// <summary>
        /// Gets or sets the pitch adjustment.
        /// </summary>
        public float Pitch { get { return this.sfxInstance.Pitch; } set { this.sfxInstance.Pitch = value; } }
        /// <summary>
        /// Gets the sound effect's current playback state.
        /// </summary>
        public SoundState State { get { return this.sfxInstance.State; } }
        /// <summary>
        /// Gets or sets the volume of the sound effect.
        /// </summary>
        public float Volume { get { return this.sfxInstance.Volume; } set { this.sfxInstance.Volume = value; } }

        public SFX(SoundEffect _sfx)
        {
            this.sfx = _sfx;
            this.Init();
        }
        public SFX(ContentManager content, string mediaName)
        {
            this.sfx = content.Load<SoundEffect>(mediaName);
            this.Init();
        }
        private void Init()
        {
            this.sfxInstance = this.sfx.CreateInstance();
        }
        /// <summary>
        /// Pauses playback of the sound effect
        /// </summary>
        public void Pause()
        {
            this.sfxInstance.Pause();
        }
        /// <summary>
        /// Plays or resumes the sound effect
        /// </summary>
        public void Play()
        {
            this.sfxInstance.Play();
        }
        /// <summary>
        /// Immediately stops playing the sound effect
        /// </summary>
        public void Stop()
        {
            this.sfxInstance.Stop();
        }
        
    }
}
