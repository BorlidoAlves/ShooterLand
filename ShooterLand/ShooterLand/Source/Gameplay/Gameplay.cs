using Microsoft.Xna.Framework;
using ShooterLand.Source.Engine;
using ShooterLand.Source.Engine.Server;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Gameplay.World.Projectiles;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using ShooterLand.Source.Menus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ShooterLand.Source.Gameplay
{
    public class Gameplay
    {
        protected bool choosing;
        protected bool returnToMenu;
        protected ChooseCharacter choose;
        //private GameOverMenu menu;
        protected List<Character> characters;
        protected int currentCharacter;
        protected bool scoreSaved;
        //private Client client; 

        public Gameplay()
        {
            choosing = true;
            returnToMenu = false;
            //menu = new GameOverMenu(PlayAgainAsync, NotPlayAgain);
            characters = new List<Character>();
            characters.Add( new Archer(new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), 1));
            characters.Add((new Mage(new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), 1)));
            characters.Add((new IceMage(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), 1)));
            currentCharacter = 0;
            scoreSaved = false;
           
        }

        

       /* public void Choose(object _info)
        {
            
            //client.ConnectToServer();
            choosing = false;
           // characters[currentCharacter].SetDimentions(new Vector2(80,80));
            Character choosenCharacter = new Character(characters[currentCharacter]);
            choosenCharacter.SetDimentions(new Vector2(80, 80));
            // world = new World(characters[currentCharacter]);
            world = new GameWorld(choosenCharacter);
        } */

        public void Previous(object _info)
        {
            currentCharacter--;
            if (currentCharacter < 0)
            {
                currentCharacter += characters.Count;
            }
            Debug.WriteLine(currentCharacter);
        }
        
        public void Next(object _info)
        {
            currentCharacter = (currentCharacter + 1) % characters.Count;
            Debug.WriteLine(currentCharacter);
        }

       /* public async Task PlayAgainAsync(object _info)
        {
           //For now we will store GameMode as singleplayer. When multiplayer is created this will be dinamic
            await Communication.SaveStats(world.GetStatsManager().GetTotalKilled(), world.GetStatsManager().GetScore(),
            world.GetRoundManager().GetCurrentRound(), "SinglePlayer", Globals.loggedUser.Id);
            choosing = true;
            characters[currentCharacter].SetLifeStatus(false);
            characters[currentCharacter].Revive();
            characters[currentCharacter].SetPosition(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2));

        }*/

        /*public async Task NotPlayAgain(object _info)
        {
            await Communication.SaveStats(world.GetStatsManager().GetTotalKilled(), world.GetStatsManager().GetScore(),
            world.GetRoundManager().GetCurrentRound(), "SinglePlayer", Globals.loggedUser.Id);
            client.ServerDisconnect();
            ReturnToMenu(_info);
        }*/

        public void ReturnToMenu(object _info)
        {
            returnToMenu = true;

        }

        public bool Return()
        {
            return returnToMenu;
        }

        public void SetReturn(bool _return)
        {
            returnToMenu = _return;
        }

    }
}
