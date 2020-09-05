using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGameLibrary.Audio
{
    interface IMediaList<T>
    {
        int Count { get; }
        int Length { get; }
        void Add(params T[] mediaItems);
        void Remove(string mediaName);
        void RemoveAt(int index);
        bool Contains(string mediaName);
        void Clear();
        T GetMedia(int index);
        T GetMedia(string mediaName);
    }
}
