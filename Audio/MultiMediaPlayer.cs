using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace MonoGameLibrary.Audio
{
    static class MultiMediaPlayer
    {
        private static SongList songList;
        private static int curSongIndex;
        /// <summary>
        /// Enable or Disable Looping of Songs being played
        /// </summary>
        public static bool isLooped { get { return MediaPlayer.IsRepeating; } set { MediaPlayer.IsRepeating = value; } }
        /// <summary>
        /// Mutes the volume of current Song
        /// </summary>
        public static bool isMuted { get { return MediaPlayer.IsMuted; } set { MediaPlayer.IsMuted = value; } }
        /// <summary>
        /// Change the volume. 0.0(silence) to 1.0(full) volume
        /// </summary>
        public static float Volume { get { return MediaPlayer.Volume; } set { MediaPlayer.Volume = value; } }
        /// <summary>
        /// Instance of the current Song
        /// </summary>
        public static Song CurrentSong { get { return songList.GetMedia(curSongIndex); } }
        static MultiMediaPlayer()
        {
            curSongIndex = 0;
            
            songList = new SongList();
        }
        /// <summary>
        /// Adds Song(s) to the Song list to be played
        /// </summary>
        /// <param name="mediaItems">Songs to play</param>
        public static void AddToQueue(params Song[] mediaItems)
        {
            songList.Add(mediaItems);
        }
        /// <summary>
        /// Adds Song(s) to MediaPlayer list to be played
        /// </summary>
        /// <param name="content">THe content manager to use in order to load the content</param>
        /// <param name="songs">Name of the songs to load into the Song list</param>
        public static void AddToQueue(ContentManager content,IEnumerable<string> songs)
        {
            songList.Add(content, songs);
        }
        /// <summary>
        /// Clear the Song list
        /// </summary>
        public static void ClearQueue()
        {
            curSongIndex = 0;
            songList.Clear();
        }
        /// <summary>
        /// Plays the next Song
        /// </summary>
        public static void Next()
        {
            curSongIndex = curSongIndex + 1 >= songList.Length ? 0 : curSongIndex + 1;
            Play();
        }
        /// <summary>
        /// Pauses the Song
        /// </summary>
        public static void Pause()
        {
            MediaPlayer.Pause();
        }
        /// <summary>
        /// Resume playing the current Song
        /// </summary>
        private static void Resume()
        {
            MediaPlayer.Resume();
        }
        /// <summary>
        /// Plays the current Song. If Song is paused, Resume it instead of starting over
        /// </summary>
        public static void Play()
        {
            if (MediaPlayer.State == MediaState.Paused)
            {
                Resume();
            }
            MediaPlayer.Play(songList.GetMedia(curSongIndex));
            
        }
        /// <summary>
        /// Plays a previous Song 
        /// </summary>
        public static void Previous()
        {
            curSongIndex = curSongIndex - 1 < 0 ? songList.Length - 1 : curSongIndex - 1;
            Play();
        }
        /// <summary>
        /// Removes a Song from the Song list
        /// </summary>
        /// <param name="mediaName">Name of the Song to remove</param>
        public static void RemoveFromQueue(string mediaName)
        {
            songList.Remove(mediaName);
        }
        /// <summary>
        /// Removes a Song from the Song list
        /// </summary>
        /// <param name="index">THe index of the Song to remove</param>
        public static void RemoveFromQueue(int index)
        {
            songList.RemoveAt(index);
        }
        /// <summary>
        /// Checks whether the Song is in the Song list
        /// </summary>
        /// <param name="mediaName">Name of the Song</param>
        /// <returns>True or False when Song is in the list</returns>
        public static bool HasSongInList(string mediaName)
        {
            return songList.Contains(mediaName);
        }
        /// <summary>
        /// Stops Song from playing, restarting the Song from the beginning.
        /// </summary>
        public static void Stop()
        {
            MediaPlayer.Stop();
        }
        /// <summary>
        /// Check if the current Song has finished playing
        /// </summary>
        /// <returns>True or False if song is finished</returns>
        public static bool IsSongFinished()
        {
            return MediaPlayer.PlayPosition >= CurrentSong.Duration;
        }
        /// <summary>
        /// The amount of time remaining in the song
        /// </summary>
        /// <returns>The elapsed time remaining of the song</returns>
        public static TimeSpan ElapsedSongTime()
        {
            return CurrentSong.Duration - MediaPlayer.PlayPosition;
        }
        /// <summary>
        /// Shuffles the Song list using cryptographic algorithm
        /// </summary>
        public static void ShuffleSongs()
        {
            songList.shuffleSongs();
        }
        
    }
}
