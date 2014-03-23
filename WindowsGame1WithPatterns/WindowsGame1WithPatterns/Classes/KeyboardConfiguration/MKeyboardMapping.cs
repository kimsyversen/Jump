using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WindowsGame1WithPatterns.Classes.FileIO;
using Microsoft.Xna.Framework.Input;
using System.Xml.Serialization;
using System.Diagnostics;
using System.IO;

namespace WindowsGame1WithPatterns.Classes.KeyboardConfiguration
{
    //TODO: Tenke mer på mapping mellom virituelle vs ordentlige keys og lage et system for det
    //TODO: Bruker vi denne?
    /// <summary>
    /// Class is still under development... (Magnus)
    /// </summary>
    public abstract class MKeyboardMapping
    {
        protected SerializableDictionary<GameKey, Keys> KeyboardMaps;
        protected string KeyboardMapFilePath;

        public MKeyboardMapping(string keyboardMapFilePath)
        {
            KeyboardMaps = new SerializableDictionary<GameKey, Keys>();
            KeyboardMapFilePath = keyboardMapFilePath;
            Load();
        }

        /// <summary>
        /// Check if the key has been pressed between the current frame 
        /// and the previus frame
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if the key has been pressed between frames, else false</returns>
        public bool IsKeyPressed(GameKey key)
        {
            return InputManager.Instance.IsKeyPressed(KeyboardMaps[key]);
        }

        /// <summary>
        /// Check if the key has been released between the current frame 
        /// and the previus frame
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if the key has been released between frames, else false</returns>
        public bool IsKeyReleased(GameKey key)
        {
            return InputManager.Instance.IsKeyReleased(KeyboardMaps[key]);
        }


        public void Load()
        {
            try
            {
                KeyboardMaps =
                    Serializer.Deserialize<SerializableDictionary<GameKey, Keys>>(KeyboardMapFilePath);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Could not load keyboard maping file (does it exist and is the class calling serialize public?). Exception message:");
                Debug.WriteLine(e.Message);
                Debug.WriteLine("Reseting keyboard layout and saving it to the xmlSaveFile.");
                ResetToDefaultMap();
                Save();
            }
        }

        public void Save()
        {
            try
            {
                Serializer.Serialize<SerializableDictionary<GameKey, Keys>>(KeyboardMaps, KeyboardMapFilePath);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Could not save keyboard layout, continuing. Exception message:");
                Debug.WriteLine(e.Message);
            }
        }

        protected void ResetToDefaultMap()
        {
            KeyboardMaps.Add(GameKey.Up, Keys.W);
            KeyboardMaps.Add(GameKey.Down, Keys.S);
            KeyboardMaps.Add(GameKey.Left, Keys.A);
            KeyboardMaps.Add(GameKey.Right, Keys.D);
        }

        public enum GameKey
        {
            Up, 
            Down, 
            Left, 
            Right,
        }
    }
}
