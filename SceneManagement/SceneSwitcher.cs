using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Text;

namespace MonoGameLibrary.SceneManagement
{
    /// <summary>
    /// Class for Iterating through a collection of Scene objects 
    /// </summary>
    public class SceneSwitcher
    {
        private List<IScene> scenes;
        private int currentIndex;
        public IScene CurrentScene { get { return this.scenes[this.currentIndex]; } }
        public SceneSwitcher()
        {
            this.scenes = new List<IScene>();
        }
        public SceneSwitcher(IEnumerable<IScene> items)
        {
            this.scenes = new List<IScene>();
            this.scenes.AddRange(items);
        }
        /// <summary>
        /// Adds an array of Scenes to the List 
        /// </summary>
        /// <param name="_scenes">Scenes to add</param>
        public void Add(params IScene[] _scenes)
        {
            foreach (IScene s in _scenes)
            {
                this.scenes.Add(s);
            }
        }
        /// <summary>
        /// Adds a Scene object to the List
        /// </summary>
        /// <param name="_scene">Scene to add</param>
        public void Add(IScene _scene)
        {
            this.scenes.Add(_scene);
        }
        /// <summary>
        /// Retrieve the next Scene object in the sequential order these were added
        /// </summary>
        /// <returns>The next Scene object in the List</returns>
        public IScene Next()
        {
            this.scenes[this.currentIndex].UnLoadContent();
            if (this.currentIndex + 1 >= this.scenes.Count)
            {
                this.currentIndex = 0;
            }
            else
            {
                this.currentIndex++;
            }
            return this.scenes[this.currentIndex];
        }
        /// <summary>
        /// Retrieve a specific Scene from the List
        /// </summary>
        /// <param name="_index">The index of the Scene</param>
        /// <returns>The specified Scene</returns>
        public IScene GetScene(int _index)
        {
            this.scenes[this.currentIndex].UnLoadContent();
            this.currentIndex = _index;
            return this.scenes[_index];
        }
        /// <summary>
        /// Retrieve a specific Scene from the List.
        /// </summary>
        /// <param name="_texturename">Name of texture to retrieve</param>
        /// <returns>The specified Scene</returns>
        public IScene GetScene(string _texturename)
        {
            this.scenes[this.currentIndex].UnLoadContent();
            int count = -1;
            IScene scene = this.scenes.Find((x) => { count++; return x.TextureName == _texturename; });
            this.currentIndex = count > -1 ? count : 0;
            return scene;
        }
        /// <summary>
        /// Checks if Scene is in the List
        /// </summary>
        /// <param name="_texturename">The texture name to look for</param>
        /// <returns>The Scene object</returns>
        public bool IsSceneInList(string _texturename)
        {
            return this.scenes.Contains(this.GetScene(_texturename));
        }
        /// <summary>
        /// Checks if Scene is in the List
        /// </summary>
        /// <param name="_scene">THe Scene object to look for</param>
        /// <returns>The Scene object</returns>
        public bool IsSceneInList(IScene _scene)
        {
            return this.scenes.Contains(_scene);
        }
        /// <summary>
        /// Clear all items in the List
        /// </summary>
        public void Reset()
        {
            this.currentIndex = 0;
            this.scenes.Clear();
        }
    }
}
