
using System.Collections.Generic;
using System.Linq;


namespace PD_1_Project_FrostyGame.PD1.General
{
    public class PlayerScore
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public int Distance { get; set; }
        public int Coins { get; set; }
        public PlayerScore(string name, int score, int distance, int coins)
        {
            Name = name;
            Score = score;
            Distance = distance;
            Coins = coins;
        }
        public override string ToString()
        {
            return $"{Name} : Score {Score} , Distance {Distance} , Coins {Coins}";
        }
        public static List<PlayerScore> AddScoreToList(PlayerScore newScore)
        {
            if (GameSettings.ListOfScores == null)
            {
                GameSettings.ListOfScores = new List<PlayerScore>() { newScore };
            }
            else
            {
                if (newScore.Name == null)
                {
                    newScore.Name = "";
                }
                if (GameSettings.ListOfScores.Exists(n => n.Name.Equals(newScore.Name) && newScore.Score > n.Score))
                {
                    GameSettings.ListOfScores.RemoveAll(n => n.Name.Equals(newScore.Name));
                    GameSettings.ListOfScores.Add(newScore);
                }
                else
                {
                    GameSettings.ListOfScores.Add(newScore);
                }
            }
            return GameSettings.ListOfScores;
        }
        public static List<PlayerScore> OrderScore()
        {
            return GameSettings.ListOfScores.OrderByDescending(s => s.Score).ToList();
        }
    }
}
