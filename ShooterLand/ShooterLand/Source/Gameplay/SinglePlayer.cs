using Microsoft.Xna.Framework;
using ShooterLand.Source.Engine;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using ShooterLand.Source.Menus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShooterLand.Source.Gameplay
{
    public class SinglePlayer :Gameplay
    {
        private GameOverMenu menu;
        private WorldSinglePlayer world;

        public SinglePlayer()
        {
            menu = new GameOverMenu(PlayAgainAsync, NotPlayAgain);
            choose = new ChooseCharacter(Choose, Previous, Next);

        }

        public virtual void Update()
        {
            if (choosing)
            {

                choose.Update();
            }

            else if (!world.GetCharacter().IsDead())
            {
                world.Update();
            }

            else
            {
                if (!scoreSaved)
                {
                    scoreSaved = true;
                    Communication.SaveStats(world.GetStatsManager().GetTotalKilled(), world.GetStatsManager().GetScore(),world.GetRoundManager().GetCurrentRound(), "SinglePlayer", Globals.loggedUser.Id);

                }
                menu.Update();
            }
        }

        public void Choose(object _info)
        {
            choosing = false;
            Character choosenCharacter = new Character(characters[currentCharacter]);
            choosenCharacter.SetDimentions(new Vector2(80, 80));
            world = new WorldSinglePlayer(choosenCharacter);
        }

        public void PlayAgainAsync(object _info)
        {  
            choosing = true;
        }

        public void NotPlayAgain(object _info)
        {
            ReturnToMenu(_info);
        }

        public virtual void Draw()
        {
            if (choosing)
            {
                choose.Draw(characters[currentCharacter]);

            }
            else if (!world.GetCharacter().IsDead())
            {
                world.Draw();

            }
            else
            {
                menu.DrawSinglePlayer(world.GetCharacter(), world.GetStatsManager().GetScore(), world.GetStatsManager().GetTotalKilled(), world.GetRoundManager().GetCurrentRound());

            }
        }

    }
}
