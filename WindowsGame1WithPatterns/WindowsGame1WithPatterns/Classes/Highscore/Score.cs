using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1WithPatterns.Classes.Highscores
{
    /// <summary>
    /// Used to set/get points and name for a player
    /// </summary>
    class Score
    {
        /// <summary>
        /// Get or set points
        /// </summary>
        public int Points { get; set; }

        /// <summary>
        /// Get or set name
        /// </summary>
        public string Name { get; set; }

        public Score(int newPoints, string newName)
        {
            Points = newPoints;
            Name = newName;
        }
    }
}
