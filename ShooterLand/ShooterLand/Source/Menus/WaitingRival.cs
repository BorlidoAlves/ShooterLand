using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterLand.Source.Engine.Basic2DObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShooterLand.Source.Menus
{
    class WaitingRival
    {
        private Basic2D background;
        private SpriteFont font;
        


        public WaitingRival()
        {
            background = new Basic2D("2D\\UI\\Backgrounds\\mainMenu", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(Globals.screenWidth, Globals.screenHeight));
            font = Globals.content.Load<SpriteFont>("Fonts\\Arial16");
        }

        public virtual void Draw()
        {
            background.Draw(Vector2.Zero);
            string tempStr = "Waiting for other player to join...";
            Vector2 strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, 100), Color.Black);
            


        }
    }
}
