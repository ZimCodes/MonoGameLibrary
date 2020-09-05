using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace MonoGameLibrary.Audio
{
    static class MultiMediaPlayer
    {
        private static SongList songList;
        private static int curSongIndex;
        /// <summary>
        /// Loops the current Song playing
        /// </summary>
        public static bool isLooped { get { return MediaPlayer.IsRepeating; } set { MediaPlayer.IsRepeating = value; } }
        public static bool isMuted { get { return MediaPlayer.IsMuted; } set { MediaPlayer.IsMuted = value; } }
        public static float Volume { get { return MediaPlayer.Volume; } set { MediaPlayer.Volume = value; } }

        public static Song CurrentSong { get { return songList.GetMedia(curSongIndex); } }
        static MultiMediaPlayer()
        {
            curSongIndex = 0;
            
            songList = new SongList();
        }
        public static void AddToQueue(params Song[] mediaItems)
        {
            songList.Add(mediaItems);
        }
        public static void AddToQueue(ContentManager content,IEnumerable<string> songs)
        {
            songList.Add(content, songs);
        }
        public static void ClearQueue()
        {
            curSongIndex = 0;
            songList.Clear();
        }

        public static void Next()
        {
            curSongIndex = curSongIndex + 1 >= songList.Length ? 0 : curSongIndex + 1;
            Play();
        }

        public static void Pause()
        {
            MediaPlayer.Pause();
        }
        private static void Resume()
        {
            MediaPlayer.Resume();
        }
        public static void Play()
        {
            if (MediaPlayer.State == MediaState.Paused)
            {
                Resume();
            }
            MediaPlayer.Play(songList.GetMedia(curSongIndex));
            
        }

        public static void Previous()
        {
            curSongIndex = curSongIndex - 1 < 0 ? songList.Length - 1 : curSongIndex - 1;
            Play();
        }

        public static void RemoveFromQueue(string mediaName)
        {
            songList.Remove(mediaName);
        }

        public static void RemoveFromQueue(int index)
        {
            songList.RemoveAt(index);
        }
        public static bool HasSongInList(string mediaName)
        {
            return songList.Contains(mediaName);
        }
        public static void Stop()
        {
            MediaPlayer.Stop();
        }
        public static bool IsSongFinished()
        {
            return MediaPlayer.PlayPosition >= CurrentSong.Duration;
        }
        public static void ShuffleSongs()
        {
            songList.shuffleSongs();
        }
        
    }
}
