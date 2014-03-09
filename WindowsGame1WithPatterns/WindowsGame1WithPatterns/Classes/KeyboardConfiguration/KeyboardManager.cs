using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1WithPatterns.Classes.KeyboardConfiguration
{
    //Easy reuseable,Input handlig&managing is at one place, globally accessible

    //TODO: Singleton?
    public static class KeyboardManager
    {
        private static KeyboardState _currentState;
        private static KeyboardState _previousState;
        private static void RefreshPreviousKeyState()
        {
            _previousState = _currentState;
        }
        public static void RefreshCurrentKeyState()
        {
            _currentState = Keyboard.GetState();
        }

        public static bool IsKeyDown(Keys key)
        {
            return _currentState.IsKeyDown(key);
        }

        public static bool IsKeyUp(Keys key)
        {
            return _currentState.IsKeyUp(key);
        }

        /// <summary>
        /// Need to to this because updating of frames happens faster than a user releases a key
        /// Without, it would just switch fast between menu and game.
        /// Check if a key is down now, and was not down before, it's just pressed
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyJustPressed(Keys key)
        {
            var value = _currentState.IsKeyDown(key) && !_previousState.IsKeyDown(key);
            RefreshPreviousKeyState();
            return value;
        }
        /// <summary>
        /// Check if a key is up and was not up before 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool KeyJustReleased(Keys key)
        {
            var value = _currentState.IsKeyUp(key) && !_previousState.IsKeyUp(key);
            RefreshPreviousKeyState();
            return value;
        }
    }
}
