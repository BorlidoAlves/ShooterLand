using Microsoft.Xna.Framework;
using ShooterLand.Source.Engine;
using ShooterLand.Source.Engine.Server;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using ShooterLand.Source.Menus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ShooterLand.Source.Gameplay
{
    public class MultiPlayer : Gameplay
    {
        private WorldMultiplayer world;
        private Client client;
        private WaitingRival waiting;
        private string gameResult;
        private GameOverMenu gameOver;
        

        public MultiPlayer(Client _client)
        {
            client = _client;
            choose = new ChooseCharacter(Choose,Previous,Next);
            waiting = new WaitingRival();
            gameOver = new GameOverMenu(PlayAgainAsync,NotPlayAgain);
            
        }

        public virtual void Update()
        {
            if (choosing)
            {
                choose.Update();
            }
            
            else if (ClientHandle.readyToStart && !world.GetCharacter().IsDead() && !world.GetRivalCharacter().IsDead() )
            {
                world.Update();
            }

            else if(world.GetCharacter().IsDead() || world.GetRivalCharacter().IsDead())
            {
                if (!world.GetCharacter().IsDead())
                {
                    gameResult = "Win";
                }
                else
                {
                    gameResult = "Defeat";
                }
                if (!scoreSaved  )
                {
                    scoreSaved = true;
                    Communication.SaveStatsMultiplayer(gameResult, "MultiPlayer", Globals.loggedUser.Id);
                    client.ServerDisconnect();
                    
                }
                gameOver.Update();
                
            }
        }

         public void Choose(object _info)
        {
            client.ConnectToServer();
            choosing = false;
            Character choosenCharacter = new Character(characters[currentCharacter]);
            choosenCharacter.SetDimentions(new Vector2(80, 80));
            world = new WorldMultiplayer(choosenCharacter);
        }

        public void PlayAgainAsync(object _info)
        {
            choosing = true;
            ClientHandle.readyToStart = false;
            scoreSaved = false;
        }

        public void  NotPlayAgain(object _info)
        {
            ReturnToMenu(_info);
            ClientHandle.readyToStart = false;


        }

        public virtual void Draw()
        {
            if (choosing )
            {
                choose.Draw(characters[currentCharacter]);

            }
            else if (!ClientHandle.readyToStart)
            {
                waiting.Draw();
            }
            else if (!world.GetCharacter().IsDead() && !world.GetRivalCharacter().IsDead())
            {
                world.Draw();

            }
            else if(world.GetCharacter().IsDead() || world.GetRivalCharacter().IsDead())
            {
                gameOver.DrawMultiplayer(world.GetCharacter(),gameResult);

            }
        }

        
    }
}
