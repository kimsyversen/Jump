using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1WithPatterns.Classes.Highscores
{
    class Score
    {
        public int Points { get; set; }
        public string Name { get; set; }

        public Score(int newPoints, string newName)
        {
            Points = newPoints;
            Name = newName;
        }
    }
}
