using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MonoGameLibrary.Audio
{
    class SongList : IMediaList<Song>
    {
        private List<Song> songs;
        public int Count { get { return this.songs.Count; } }

        public int Length { get { return this.songs.Count; } }
        public SongList()
        {
            this.songs = new List<Song>();
        }
        public void Add(params Song[] mediaItems)
        {
            this.songs.AddRange(mediaItems);
        }

        public void Add(ContentManager content, IEnumerable<string> mediaNames)
        {
            foreach (string name in mediaNames)
            {
                Song s = content.Load<Song>(name);
                this.songs.Add(s);
            }
        }

        public void Clear()
        {
            this.songs.Clear();
        }

        public bool Contains(string mediaName)
        {
            Song s = this.songs.Find(x => x.Name == mediaName);
            return this.songs.Contains(s);
        }

        public Song GetMedia(int index)
        {
            
            return this.songs[index];
        }
        
        public Song GetMedia(string mediaName)
        {
            return this.songs.Find(x=>x.Name == mediaName);
        }

        public void Remove(string mediaName)
        {
            Song s = this.songs.Find(x=>x.Name == mediaName);
            this.songs.Remove(s);
        }

        public void RemoveAt(int index)
        {
            this.songs.RemoveAt(index);
        }
        public void shuffleSongs()
        {
            MultiMediaPlayer.Shuffle(this.songs);
        }
    }
}
