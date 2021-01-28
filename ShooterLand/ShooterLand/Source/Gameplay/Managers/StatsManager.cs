using System;
using System.Collections.Generic;
using System.Text;

namespace ShooterLand.Source.Gameplay.Managers
{
    public class StatsManager
    {
        protected int score;
        protected int totalKilled;

        public StatsManager()
        {
            score = 0;
            totalKilled = 0;
        }

        public int GetScore()
        {
            return score;
        }

        public void SetScore(int _score)
        {
            score = _score;
        }

        public int GetTotalKilled()
        {
            return totalKilled;
        }

        public void SetTotalKilled(int _kills)
        {
            totalKilled=_kills;
        }
    }

    
}
