using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Engine.Basic2DObjects;
using ShooterLand.Source.Engine.Output;
using ShooterLand.Source.Gameplay.World.Units.Characters;

namespace ShooterLand.Source.Gameplay.World
{
    public class UI
    {
        protected SpriteFont font;
        protected HealthBar healtbar, healtbarRival;
        protected Basic2D pauseOverlay;
        private Basic2D shield;
        private Basic2D star;
        public UI()
        {
            pauseOverlay = new Basic2D("2D\\PauseOverlay", new Vector2(Globals.screenWidth/2, Globals.screenHeight/2), new Vector2(300, 300));
            font = Globals.content.Load<SpriteFont>("Fonts\\Arial16");
            healtbar = new HealthBar(new Vector2(120, 25), 2, Color.Red);
            healtbarRival = new HealthBar(new Vector2(120, 25), 2, Color.Red);
            shield = new Basic2D("2D\\Boosts\\Shield", new Vector2(0,0),new Vector2(30,30));
            star = new Basic2D("2D\\Boosts\\star", new Vector2(0, 0), new Vector2(45, 45));


        }
        public void Update(Character _character)
        {
            healtbar.Update(_character.GetHealth(), _character.GetHealthMax());

        }
        public void UpdateMultiplayer(Character _character, Character _character2)
        {
            healtbar.Update(_character.GetHealth(), _character.GetHealthMax());
            healtbarRival.Update(_character2.GetHealth(), _character2.GetHealthMax());
        }

        public void DrawSingleplayer(Character _character,int _score,int _totalMonstersKilled,int _currentRound)
        {
            string tempStr = "Score: " + _score;
            Vector2 strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, 20), Color.Black);

            tempStr = "Round: " + _currentRound;
            strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth- strDims.X / 2-100, 20), Color.Black);

            tempStr = "Monster killed: " + _totalMonstersKilled;
            strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth /2 - strDims.X / 2, Globals.screenHeight-100), Color.Black);


            healtbar.Draw(new Vector2(20,10));
            tempStr = _character.GetHealth()+"/"+ _character.GetHealthMax();
            strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(75 - strDims.X / 2, 10), Color.White);

            if (_character.HasShield())
            {
                shield.Draw(new Vector2(200,20));
            }

            if (_character.IsInvicible())
            {
                star.Draw(new Vector2(240, 20));
            }
           
        }

        public void DrawMultiplayer(Character _character, Character _character2)
        {
            healtbar.Draw(new Vector2(755, 35));
            string tempStr = _character.GetHealth() + "/" + _character.GetHealthMax();
            Vector2 strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(800 - strDims.X / 2, 35), Color.White);

            tempStr = "Your Life";
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(765, 10), Color.Black);

            healtbarRival.Draw(new Vector2(1075, 35));
            tempStr = _character2.GetHealth() + "/" + _character2.GetHealthMax();
            strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(1140 - strDims.X / 2, 35), Color.White);

            tempStr = "Rival Life";
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(1085, 10), Color.Black);
        }
    }


}
 