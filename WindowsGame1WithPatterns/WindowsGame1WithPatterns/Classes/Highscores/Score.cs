using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1WithPatterns.Classes.Highscores
{
    class Score
    {
        private int points;
        private string name;

        public Score(int newPoints, string newName)
        {
            points = newPoints;
            name = newName;
        }

        public int Points
        {
            get { return points; }
        }

        public string Name
        {
            get { return name; }
        }
    }
}
