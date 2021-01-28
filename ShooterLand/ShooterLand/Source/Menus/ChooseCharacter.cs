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
    public class ChooseCharacter
    {
        protected Basic2D background;
        protected Button2D chooseButton;
        protected Button2D previousButton;
        protected Button2D nextButton;

        public ChooseCharacter(PassObject _choose,PassObject _previous,PassObject _next)
        { 
     
            background = new Basic2D("2D\\UI\\Backgrounds\\mainMenu", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(Globals.screenWidth, Globals.screenHeight));
            chooseButton=new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(120, 50), "Fonts\\Arial16", "Choose", null, _choose);
            previousButton = new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(120, 50), "Fonts\\Arial16", "Previous", null, _previous);
            nextButton = new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(120, 50), "Fonts\\Arial16", "Next", null, _next);


        }

        public virtual void Update()
        {
              chooseButton.Update(new Vector2(Globals.screenWidth/2,Globals.screenHeight-200));
              previousButton.Update(new Vector2(200 , Globals.screenHeight/2));
              nextButton.Update(new Vector2(Globals.screenWidth - 200, Globals.screenHeight / 2));


        }

        public virtual void Draw(Character _character)
        {
            Character character = _character;
            character.SetDimentions(new Vector2(200,200));
            
            background.Draw(Vector2.Zero);
            character.Draw(new Vector2(0,-100));
            chooseButton.Draw(new Vector2(Globals.screenWidth / 2, Globals.screenHeight - 200));
            previousButton.Draw(new Vector2( 200, Globals.screenHeight / 2));
            nextButton.Draw(new Vector2(Globals.screenWidth - 200, Globals.screenHeight / 2));

            SpriteFont font= Globals.content.Load<SpriteFont>("Fonts\\Arial16");

            string tempStr = _character.GetName();
            Vector2 strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight / 2 -300), Color.Black);

            tempStr = _character.GetDescription();
            strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight / 2 + 50), Color.Black);

            tempStr = "Max Health: " + _character.GetHealthMax();
            strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight/2+100), Color.Black);

            tempStr = "Damage: " + _character.GetDamage();
            strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight / 2 + 150), Color.Black);

            tempStr = "Speed: " + _character.GetSpeed();
            strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight / 2 + 200), Color.Black);

            tempStr = "Range: " + _character.GetRange();
            strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, Globals.screenHeight / 2 + 250), Color.Black);

           

            
        }
    }
}
