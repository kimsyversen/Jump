using Microsoft.Xna.Framework.Input;

namespace WindowsGame1WithPatterns.Classes.KeyboardConfiguration
{
    /// <summary>
    /// Used to create key mappings for players
    /// </summary>
    class KeyboardMapping
    {
        public Keys Left { get; set; }
        public Keys Right { get; set; }
        public Keys Jump { get; set; }

        public KeyboardMapping(Keys left, Keys right, Keys jump)
        {
            Left = left;
            Right = right;
            Jump = jump;
        }
    }
}
