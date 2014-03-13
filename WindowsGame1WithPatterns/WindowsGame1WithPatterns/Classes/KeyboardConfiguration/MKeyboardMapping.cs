using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using WindowsGame1WithPatterns.FileIO;
using System.Xml.Serialization;
using System.Diagnostics;
using System.IO;

namespace WindowsGame1WithPatterns.Classes.KeyboardConfiguration
{
    class MKeyboardMapping
    {
        protected SerializableDictionary<GameKey, Keys> _keyboardMaps = new SerializableDictionary<GameKey, Keys>();
        protected string _keyboardMapFilePath;

        public MKeyboardMapping(string keyboardMapFilePath)
        {
            _keyboardMapFilePath = Path.Combine(Environment.CurrentDirectory, keyboardMapFilePath);
            _keyboardMapFilePath = @"C:\Users\Magnus Sandgren\Documents\Visual Studio 2010\Projects\Jump\WindowsGame1WithPatterns\WindowsGame1WithPatterns\bin\x86\Debug\KeyboardMappings.xml";
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
            return InputManager.IsKeyPressed(_keyboardMaps[key]);
        }

        /// <summary>
        /// Check if the key has been released between the current frame 
        /// and the previus frame
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if the key has been released between frames, else false</returns>
        public bool IsKeyReleased(GameKey key)
        {
            return InputManager.IsKeyReleased(_keyboardMaps[key]);
        }


        public void Load()
        {
            try
            {
                _keyboardMaps =
                    Serializer.Deserialize<SerializableDictionary<GameKey, Keys>>(_keyboardMapFilePath);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Could not load keyboard maping file (dous it exist?). Exception message:");
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
                Serializer.Serialize<SerializableDictionary<GameKey, Keys>>(_keyboardMaps, _keyboardMapFilePath);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Could not save keyboard layout, continuing. Exception message:");
                Debug.WriteLine(e.Message);
            }
        }

        protected void ResetToDefaultMap()
        {
            _keyboardMaps.Add(GameKey.Up, Keys.W);
            _keyboardMaps.Add(GameKey.Down, Keys.S);
            _keyboardMaps.Add(GameKey.Left, Keys.A);
            _keyboardMaps.Add(GameKey.Right, Keys.D);
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
