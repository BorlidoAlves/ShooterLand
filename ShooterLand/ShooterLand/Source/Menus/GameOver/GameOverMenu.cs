using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterLand.Source.Engine.Basic2DObjects;
using ShooterLand.Source.Engine.Basic2DObjects.Buttons;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShooterLand.Source.Menus
{
    public class GameOverMenu
    {
        protected Basic2D background;
        protected Button2D againButton;
        protected Button2D menuButton;
        protected SpriteFont font;
        //protected int score;

        public GameOverMenu(PassObject _again, PassObject _menu)
        {

            font = Globals.content.Load<SpriteFont>("Fonts\\Arial16");
            background = new Basic2D("2D\\UI\\Backgrounds\\mainMenu", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(Globals.screenWidth, Globals.screenHeight));
            againButton = new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(120, 50), "Fonts\\Arial16", "Play Again", null, _again);
            menuButton = new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(120, 50), "Fonts\\Arial16", "Main Menu", null, _menu);


        }

        public virtual void Update()
        {
            againButton.Update(new Vector2(Globals.screenWidth/2,Globals.screenHeight-300));
            menuButton.Update(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 200));
              
        }

        public virtual void DrawSinglePlayer(Character _character, int _score, int _totalMonstersKilled, int _currentRound)
        {

            Character character = _character;
            character.SetDimentions(new Vector2(200, 200));
            character.SetRotation(0);
            character.SetPosition(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2));

            background.Draw(Vector2.Zero);
            character.Draw(new Vector2(0, -100));
            string tempStr = "Final score: " + _score + "\nRound: " + _currentRound + "\nMonster killed: " + _totalMonstersKilled;
            Vector2 strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, 20), Color.Black);
            againButton.Draw(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 300));
            menuButton.Draw(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 200));
  
        }

        public virtual void DrawMultiplayer(Character _character,string _result)
        {
            Character character = _character;
            character.SetDimentions(new Vector2(200, 200));
            character.SetRotation(0);
            character.SetPosition(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2));

            background.Draw(Vector2.Zero);
            character.Draw(new Vector2(0, -100));

            string tempStr = "";
            if (_result != null)
            {
                if (_result.Equals("Win"))
                {
                    tempStr = "You slayed your rival! You are the winner";
                }
                else
                {
                    tempStr = "You died! LOSER";
                }
            }
           
            Vector2 strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, 20), Color.Black);
            againButton.Draw(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 300));
            menuButton.Draw(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 200));
        }
    }
}
