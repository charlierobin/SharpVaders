using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SharpVaders
{
    public class HighScores
    {
        private string highScoresFileName = "com.charlierobin.sharpvaders.high-scores.json";

        private int maximumNumberOfHighScores = 10;

        public List<HighScoreEntry> entries;

        public HighScores()
        {
            string json = "[]";

            string path = this.prefsFile();

            if (File.Exists(path))
            {
                StreamReader reader = new StreamReader(path, true);

                json = reader.ReadToEnd();

                reader.Close();
            }

            this.entries = JsonSerializer.Deserialize<List<HighScoreEntry>>(json);
        }

        private string prefsFile()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            path = Path.Combine(path, "Library/Preferences");

            path = Path.Combine(path, this.highScoresFileName);

            return path;
        }

        public void write()
        {
            string json = JsonSerializer.Serialize(this.entries);

            string path = this.prefsFile();

            StreamWriter writer = new StreamWriter(path, false);

            writer.Write(json);

            writer.Flush();
            writer.Close();
        }

        public bool isNewHighScore(int score)
        {
            if (score > 0)
            {
                if (this.entries.Count < this.maximumNumberOfHighScores) return true;

                int pos = this.entries.FindIndex(delegate (HighScoreEntry entry) { return entry.score < score; });

                if (pos > -1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        public void add(string name, int score)
        {
            HighScoreEntry entry = new HighScoreEntry(name, score);

            this.entries.Add(entry);

            this.entries.Sort(delegate (HighScoreEntry x, HighScoreEntry y)
            {
                if (x.score > y.score) return -1;
                else if (y.score > x.score) return 1;
                else return 0;
            });

            this.entries = this.entries.GetRange(0, Math.Min(this.maximumNumberOfHighScores, this.entries.Count));
        }
    }
}
