using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MonoGameLibrary.Util
{
    /// <summary>
    /// Makes GameComponent related things easier!
    /// </summary>
    static class GameUtil
    {
        
        /// <summary>
        /// Retrieve specified service instance. If service has not been created yet, then do so.
        /// </summary>
        /// <typeparam name="C">Concrete Class Type</typeparam>
        /// <typeparam name="I">Interface Type</typeparam>
        /// <param name="game">Reference to Game class</param>
        /// <returns>the service</returns>
        public static C GetService<C,I>(Game game)
            where C:IGameComponent
        {
            C service = (C)game.Services.GetService(typeof(I));
            
            if (service == null)
            {
                service = (C)Activator.CreateInstance(typeof(C), game);//God Mode
                game.Components.Add(service);
            }
            return service;
        }
        /// <summary>
        /// Retrieve specified service instance. If service has not been created yet, then do so.
        /// </summary>
        /// <typeparam name="C">Concrete Class Type</typeparam>
        /// <typeparam name="I">Interface Type</typeparam>
        /// <param name="game">Reference to Game class</param>
        /// <param name="parameters">Params to enter into the constructor. Is also used to find a constructor with the same params</param>
        /// <returns>the service</returns>
        public static C GetService<C, I>(Game game, params object[] parameters)
            where C : IGameComponent
        {
            C service = (C)game.Services.GetService(typeof(I));

            if (service == null)
            {

                service = (C)Activator.CreateInstance(typeof(C), NewObjectArray(game,parameters));//God Mode
                game.Components.Add(service);
            }
            return service;
        }
        /// <summary>
        /// Adds GameComponent object to the Game
        /// </summary>
        /// <typeparam name="T">An object that implements an IGameComponent interface</typeparam>
        /// <param name="game">Reference to the Game class</param>
        /// <return>the Game Component</return>
        public static T AddGameComponent<T>(Game game)
            where T:IGameComponent
        {
            T component = (T)Activator.CreateInstance(typeof(T),game);//God Mode
            game.Components.Add(component);
            return component;
        }
        /// <summary>
        /// Adds GameComponent object to the Game
        /// </summary>
        /// <typeparam name="T">An object that implements an IGameComponent interface</typeparam>
        /// <param name="game">Reference to the Game class</param>
        /// <param name="parameters">Params to enter into the constructor. Is also used to find a constructor with the same params</param>
        /// <return>the Game Component</return>
        public static T AddGameComponent<T>(Game game, params object[] parameters)
            where T : IGameComponent
        {
            
            T component = (T)Activator.CreateInstance(typeof(T), NewObjectArray(game,parameters));//God Mode
            game.Components.Add(component);
            return component;
        }
        /// <summary>
        /// Adds Game to the object array
        /// </summary>
        /// <param name="game">Reference to Game</param>
        /// <param name="parameters">Params to enter into the constructor.</param>
        /// <returns>an object array with Game included</returns>
        private static object[] NewObjectArray(Game game, object[] parameters)
        {
            object[] paramsWithGame = new object[parameters.Length + 1];
            paramsWithGame[0] = game;
            int count = 1;
            foreach (object p in parameters)
            {
                paramsWithGame[count] = p;
                count++;
            }
            return paramsWithGame;
        }
    }
}
