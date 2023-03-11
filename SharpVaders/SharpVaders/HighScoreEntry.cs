using System;

namespace SharpVaders
{
    public class HighScoreEntry
    {
        public string name { get; set; }
        public int score { get; set; }

        public HighScoreEntry(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
}

