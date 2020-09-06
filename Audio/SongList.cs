using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MonoGameLibrary.Audio
{
    class SongList : IMediaList<Song>
    {
        /// <summary>
        /// List of songs
        /// </summary>
        private List<Song> songs;
        /// <summary>
        /// The amount of Songs in the list
        /// </summary>
        public int Count { get { return this.songs.Count; } }
        /// <summary>
        /// The amount of Songs in the list
        /// </summary>
        public int Length { get { return this.songs.Count; } }
        public SongList()
        {
            this.songs = new List<Song>();
        }
        /// <summary>
        /// Adds the Song to the list
        /// </summary>
        /// <param name="mediaItems">Songs to add</param>
        public void Add(params Song[] mediaItems)
        {
            this.songs.AddRange(mediaItems);
        }
        /// <summary>
        /// Adds the Song to the list
        /// </summary>
        /// <param name="content">ContentManager reference to load Song</param>
        /// <param name="mediaNames">Names of the songs to add to the list</param>
        public void Add(ContentManager content, IEnumerable<string> mediaNames)
        {
            foreach (string name in mediaNames)
            {
                Song s = content.Load<Song>(name);
                this.songs.Add(s);
            }
        }
        /// <summary>
        /// Clears the song list
        /// </summary>
        public void Clear()
        {
            this.songs.Clear();
        }
        /// <summary>
        /// Check to see if list contains the Song
        /// </summary>
        /// <param name="mediaName"></param>
        /// <returns>True or false if Song is in list</returns>
        public bool Contains(string mediaName)
        {
            Song s = this.songs.Find(x => x.Name == mediaName);
            return this.songs.Contains(s);
        }
        /// <summary>
        /// Retrieve a specified Song
        /// </summary>
        /// <param name="index">Index of the song</param>
        /// <returns>THe Song reference</returns>
        public Song GetMedia(int index)
        {
            
            return this.songs[index];
        }
        /// <summary>
        /// Retrieve a specified Song
        /// </summary>
        /// <param name="mediaName">Name of the Song to retrieve</param>
        /// <returns>The Song reference</returns>
        public Song GetMedia(string mediaName)
        {
            return this.songs.Find(x=>x.Name == mediaName);
        }
        /// <summary>
        /// Removes a Song from the list
        /// </summary>
        /// <param name="mediaName">Song to remove</param>
        public void Remove(string mediaName)
        {
            Song s = this.songs.Find(x=>x.Name == mediaName);
            this.songs.Remove(s);
        }
        /// <summary>
        /// Removes a Song from the list
        /// </summary>
        /// <param name="index">Index of the Song to remove</param>
        public void RemoveAt(int index)
        {
            this.songs.RemoveAt(index);
        }
        /// <summary>
        /// Shuffles the list of Songs
        /// </summary>
        public void shuffleSongs()
        {
            ListUtil.Shuffle(this.songs);
        }
    }
}
