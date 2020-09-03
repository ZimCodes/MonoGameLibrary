using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MonoGameLibrary.Util
{
    /// <summary>
    /// Makes retrieving and services easier!
    /// </summary>
    static class FetchService
    {
        /// <summary>
        /// Retrieve specified service instance. If service has not been created yet, then do so.
        /// </summary>
        /// <typeparam name="C">Concrete Class Type</typeparam>
        /// <typeparam name="I">Interface Type</typeparam>
        /// <param name="game">Reference to Game class</param>
        /// <returns>the service</returns>
        public static C Get<C,I>(Game game)
            where C:IGameComponent
        {
            C service = (C)game.Services.GetService(typeof(I));
            
            if (service == null)
            {
                service = (C)Activator.CreateInstance(typeof(C),game);//God Mode
                game.Components.Add(service);
            }
            return service;
        }
        /// <summary>
        /// Add GameComponent object to the Game
        /// </summary>
        /// <typeparam name="T">An object that implements an IGameComponent interface</typeparam>
        /// <param name="game">Reference to the Game class</param>
        public static void AddGameComponent<T>(Game game)
            where T:IGameComponent
        {
            T component = (T)Activator.CreateInstance(typeof(T),game);
            game.Components.Add(component);
        }
        /// <summary>
        /// Add GameComponent object to the Game
        /// </summary>
        /// <typeparam name="T">An object that implements an IGameComponent interface</typeparam>
        /// <param name="game">Reference to the Game class</param>
        /// <returns>the IGameComponent object created</returns>
        public static T GetGameComponent<T>(Game game)
            where T : IGameComponent
        {
            T component = (T)Activator.CreateInstance(typeof(T), game);
            game.Components.Add(component);
            return component;
        }

    }
}
