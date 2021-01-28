using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.World.SpawnPoints;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShooterLand.Source.Gameplay.Managers
{
    public class RoundManager
    {
        protected int roundKills;
        protected int roundSpawns;
        protected int currentRound;
        protected bool roundEnded;

        public RoundManager()
        {
            roundKills = 0;
            roundSpawns = 0;
            currentRound = 1;
            roundEnded = false;
            SpawnPoint.SetRoundSpawns(0);
        }

        public int GetRoundKills()
        {
            return roundKills;
        }

        public void SetRoundKills(int _roundKills)
        {
            roundKills = _roundKills;
        }

        public int GetRoundSpawns()
        {
            return roundSpawns;
        }

        public void SetRoundSpawns(int _roundSpawns)
        {
            roundSpawns = _roundSpawns;
        }

        public int GetCurrentRound()
        {
            return currentRound;
        }

        public void SetCurrentRound(int _currentRound)
        {
            currentRound = _currentRound;
        }

        public bool RoundEnded()
        {
            return roundEnded;
        }

        public virtual void NextRound()
        {
            roundEnded = false;
        }

        public bool ChangeRound(Character _character,PassObject _scroll)
        {
            if (roundKills == 10 + 5 * currentRound && !_character.IsDead() )
            {
                //ShooterLand.SetGameState(2);
                _character.SetPosition(new Vector2(900,900));
                _scroll(null);
                roundKills = 0;
                SpawnPoint.SetRoundSpawns(0);
                currentRound++;
                roundEnded = true;
                return true;
            }
            return false;
        }
    }

}
