using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame1WithPatterns.Classes.KeyboardConfiguration
{
    //Sorce: http://xboxforums.create.msdn.com/forums/p/37393/217007.aspx
    /// <summary>
    /// The Keyboard.GetState() and Mouse.GetState() are expensive to run.
    /// This class is to minimize the use of the GetState method and to help 
    /// in checking for keystrokes between frames.
    /// </summary>
    class InputManager
    {
        /// <summary>
        /// Sorce: http://csharpindepth.com/Articles/General/Singleton.aspx
        /// Singleton implementation 
        /// </summary>
        #region Singleton
        private static readonly Lazy<InputManager> lazy =
        new Lazy<InputManager>(() => new InputManager());
    
        public static InputManager Instance { get { return lazy.Value; } }

        private InputManager()
        {
        }
        #endregion

        /// <summary>
        /// Will hold the keyboard state of the current frame
        /// </summary>
        private KeyboardState _currentKeyboardState;

        /// <summary>
        /// Will hold the keyboard state of the previous frame
        /// </summary>
        private KeyboardState _previousKeyboardState;

        /// <summary>
        /// Will hold the mouse state of the current frame
        /// </summary>
        private MouseState _currentMouseState;

        /// <summary>
        /// Will hold the mouse state of the previous frame
        /// </summary>
        private MouseState _previousMouseState;

        /// <summary>
        /// Get the current MouseState for the frame
        /// </summary>
        public MouseState CurrentMouseState
        {
            get { return _currentMouseState; }
        }

        /// <summary>
        /// Get the MouseState for the previus frame
        /// </summary>
        public MouseState PreviousMouseState
        {
            get { return _previousMouseState; }
        }

        /// <summary>
        /// Check if the key has been pressed between the current frame 
        /// and the previus frame
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if the key has been pressed between frames, else false</returns>
        public bool IsKeyPressed(Keys key)  
        {
            return _currentKeyboardState.IsKeyDown(key) && _previousKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Check if the key has been released between the current frame 
        /// and the previus frame
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if the key has been released between frames, else false</returns>
        public bool IsKeyReleased(Keys key)
        {
            return _previousKeyboardState.IsKeyDown(key) && _currentKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Check if the key is down this frame
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if the key is down, else false</returns>
        public bool IsKeyDown(Keys key)
        {
            return _currentKeyboardState.IsKeyDown(key);
        }

        /// <summary>
        /// Check if the key is up this frame
        /// </summary>
        /// <param name="key">The key to check</param>
        /// <returns>True if the key is up this frame, else false</returns>
        public bool IsKeyUp(Keys key)
        {
            return _currentKeyboardState.IsKeyUp(key);
        }

        /// <summary>
        /// Concatinates the keyboard stream on to the string and
        /// returns it
        /// </summary>
        /// <param name="inputString">The string to concatinate the keyboard input to</param>
        /// <returns>The input string concatinated with the keyboard stream</returns>
        public string KeyboardStream(string inputString)
        {
            bool shift = _currentKeyboardState.IsKeyDown(Keys.LeftShift) || _currentKeyboardState.IsKeyDown(Keys.RightShift);

            foreach (var key in _currentKeyboardState.GetPressedKeys())
                if (IsKeyPressed(key))
                {
                    string convertedKey = ConvertKeyToChar(key, shift);
                    if (convertedKey == "\b")
                    {
                        if (inputString != string.Empty)
                            inputString = inputString.Remove(inputString.Length - 1);
                    }
                    else
                        inputString = string.Concat(inputString, convertedKey);
                }

            return inputString;
        }

        //Sorce: http://xboxforums.create.msdn.com/forums/p/36983/214768.aspx
        /// <summary> 
        /// Convert a key to it's respective character or escape sequence. 
        /// </summary> 
        /// <param name="key">The key to convert.</param> 
        /// <param name="shift">Is the shift key pressed or caps lock down.</param> 
        /// <returns>The char for the key that was pressed or string.Empty if it doesn't have a char representation.</returns> 
        private string ConvertKeyToChar(Keys key, bool shift)
        {
            switch (key)
            {
                case Keys.Space: return " ";

                // Escape Sequences 
                case Keys.Enter: return "\n";                         // Create a new line 
                case Keys.Tab: return "\t";                           // Tab to the right 

                // D-Numerics (strip above the alphabet) 
                case Keys.D0: return shift ? ")" : "0";
                case Keys.D1: return shift ? "!" : "1";
                case Keys.D2: return shift ? "@" : "2";
                case Keys.D3: return shift ? "#" : "3";
                case Keys.D4: return shift ? "$" : "4";
                case Keys.D5: return shift ? "%" : "5";
                case Keys.D6: return shift ? "^" : "6";
                case Keys.D7: return shift ? "&" : "7";
                case Keys.D8: return shift ? "*" : "8";
                case Keys.D9: return shift ? "(" : "9";

                // Numpad 
                case Keys.NumPad0: return "0";
                case Keys.NumPad1: return "1";
                case Keys.NumPad2: return "2";
                case Keys.NumPad3: return "3";
                case Keys.NumPad4: return "4";
                case Keys.NumPad5: return "5";
                case Keys.NumPad6: return "6";
                case Keys.NumPad7: return "7";
                case Keys.NumPad8: return "8";
                case Keys.NumPad9: return "9";
                case Keys.Add: return "+";
                case Keys.Subtract: return "-";
                case Keys.Multiply: return "*";
                case Keys.Divide: return "/";
                case Keys.Decimal: return ".";

                // Alphabet 
                case Keys.A: return shift ? "A" : "a";
                case Keys.B: return shift ? "B" : "b";
                case Keys.C: return shift ? "C" : "c";
                case Keys.D: return shift ? "D" : "d";
                case Keys.E: return shift ? "E" : "e";
                case Keys.F: return shift ? "F" : "f";
                case Keys.G: return shift ? "G" : "g";
                case Keys.H: return shift ? "H" : "h";
                case Keys.I: return shift ? "I" : "i";
                case Keys.J: return shift ? "J" : "j";
                case Keys.K: return shift ? "K" : "k";
                case Keys.L: return shift ? "L" : "l";
                case Keys.M: return shift ? "M" : "m";
                case Keys.N: return shift ? "N" : "n";
                case Keys.O: return shift ? "O" : "o";
                case Keys.P: return shift ? "P" : "p";
                case Keys.Q: return shift ? "Q" : "q";
                case Keys.R: return shift ? "R" : "r";
                case Keys.S: return shift ? "S" : "s";
                case Keys.T: return shift ? "T" : "t";
                case Keys.U: return shift ? "U" : "u";
                case Keys.V: return shift ? "V" : "v";
                case Keys.W: return shift ? "W" : "w";
                case Keys.X: return shift ? "X" : "x";
                case Keys.Y: return shift ? "Y" : "y";
                case Keys.Z: return shift ? "Z" : "z";

                // Oem 
                case Keys.OemOpenBrackets: return shift ? "{" : "[";
                case Keys.OemCloseBrackets: return shift ? "}" : "]";
                case Keys.OemComma: return shift ? "<" : ",";
                case Keys.OemPeriod: return shift ? ">" : ".";
                case Keys.OemMinus: return shift ? "_" : "-";
                case Keys.OemPlus: return shift ? "+" : "=";
                case Keys.OemQuestion: return shift ? "?" : "/";
                case Keys.OemSemicolon: return shift ? ":" : ";";
                case Keys.OemQuotes: return shift ? "\"" : "'";
                case Keys.OemPipe: return shift ? "|" : "\\";
                case Keys.OemTilde: return shift ? "~" : "`";

                // Backspace
                case Keys.Back: return "\b";
            }

            return string.Empty;
        } 

        /// <summary>
        /// Method to get the current input States of keyboard and mouse
        /// This method should be the first method to run in SingleplayerManager.Update.
        /// </summary>
        public void Begin()  
        {
            _currentKeyboardState = Keyboard.GetState();
            _currentMouseState = Mouse.GetState();
        }  
 
        /// <summary>
        /// Method to remember the current input States of keyboard and mouse to the next frame.
        /// This method should be the last method to run in SingleplayerManager.Update.
        /// </summary>
        public void End()  
        {
            _previousKeyboardState = _currentKeyboardState;
            _previousMouseState = _currentMouseState;
        } 
    }
}
