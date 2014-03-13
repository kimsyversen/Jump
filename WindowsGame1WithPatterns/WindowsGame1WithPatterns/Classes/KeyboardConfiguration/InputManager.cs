using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1WithPatterns.Classes.KeyboardConfiguration
{
    //Sorce: http://xboxforums.create.msdn.com/forums/p/37393/217007.aspx
    /// <summary>
    /// The Keyboard.GetState() and Mouse.GetState() are expencive to run.
    /// This class is to minimize the use of the GetState method and to help 
    /// in checking for keystrokes between frames.
    /// </summary>
    static class InputManager
    {
        //TODO: Burde denne kanskje gjøres om til singleton? 
        private static KeyboardState _currentKeyboardState;
        private static KeyboardState _previousKeyboardState;

        private static MouseState _currentMouseState;
        private static MouseState _previousMouseState;

        /// <summary>
        /// Get the current MouseState for the frame
        /// </summary>
        public static MouseState CurrentMouseState
        {
            get { return _currentMouseState; }
        }

        /// <summary>
        /// Get the MouseState for the previus frame
        /// </summary>
        public static MouseState PreviousMouseState
        {
            get { return _previousMouseState; }
        }

        /// <summary>
        /// Check if the key has been pressed between the current frame 
        /// and the previus frame
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if the key has been pressed between frames, else false</returns>
        public static bool IsKeyPressed(Keys key)  
        {
            return _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Check if the key has been released between the current frame 
        /// and the previus frame
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if the key has been released between frames, else false</returns>
        public static bool IsKeyReleased(Keys key)
        {
            return _previousKeyboardState.IsKeyDown(key) && _currentKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Method to get the current input States of keyboard and mouse
        /// This method should be the first method to run in Game1.Update.
        /// </summary>
        public static void Begin()  
        {
            _currentKeyboardState = Keyboard.GetState();
            _currentMouseState = Mouse.GetState();
        }  
 
        /// <summary>
        /// Method to remember the current input States of keyboard and mouse to the next frame.
        /// This method should be the last method to run in Game1.Update.
        /// </summary>
        public static void End()  
        {
            _previousKeyboardState = _currentKeyboardState;
            _previousMouseState = _currentMouseState;
        } 
    }
}
