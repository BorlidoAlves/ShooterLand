using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterLand.Source.Engine.Basic2DObjects;
using ShooterLand.Source.Engine.Basic2DObjects.Buttons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShooterLand.Source.Menus
{
    public class Instructions
    {
        private Button2D buttonBackMenu;
        private Basic2D mouseImage, keyboardImage, background;
        private List<string> stringList;
        private SpriteFont font, fontTitle;
        public Instructions(PassObject _menuClick)
        {
            stringList = new List<string>();

            background = new Basic2D("2D\\UI\\Backgrounds\\mainMenu", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(Globals.screenWidth, Globals.screenHeight));
            font = Globals.content.Load<SpriteFont>("Fonts\\Arial16");
            fontTitle = Globals.content.Load<SpriteFont>("Fonts\\Arial24");
            buttonBackMenu = new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(150, 32), "Fonts\\Arial16", "Back to Menu", 1, _menuClick);
            mouseImage = new Basic2D("2D\\UI\\mouse-left-click", new Vector2(0, 0), new Vector2(200, 200));
            keyboardImage = new Basic2D("2D\\UI\\keyboard", new Vector2(0, 0), new Vector2(240, 200));
            
            stringList.Add("Controls");
            stringList.Add("W - Up ");
            stringList.Add("A - Left ");
            stringList.Add("S - Down ");
            stringList.Add("D - Right ");
            stringList.Add("Mouse Left Click - Fire ");
            stringList.Add("Mouse Cursor - Aim ");
        }
        public virtual void Update()
        {
            buttonBackMenu.Update(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 + 400));
        }
        public virtual void Draw()
        { 
            int count = 1;

            background.Draw(Vector2.Zero);

            Globals.spriteBatch.DrawString(fontTitle, stringList[0], new Vector2(Globals.screenWidth / 2 - fontTitle.MeasureString(stringList[0]).X / 2, 75), Color.Black);
         
            buttonBackMenu.Draw(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 + 400));
            
            mouseImage.Draw(new Vector2(Globals.screenWidth / 3 ,Globals.screenHeight / 2 - 150));

            for (int i = 5; i < stringList.Count; i++)
            {
                
                Globals.spriteBatch.DrawString(font, stringList[i], new Vector2(Globals.screenWidth / 3 - 100, Globals.screenHeight / 2 + 50 * count), Color.Black);
                count++;
            }

            keyboardImage.Draw(new Vector2(2 * Globals.screenWidth / 3 , Globals.screenHeight / 2 - 150));

            for (int i = 1; i < 5; i++)
            {
                Globals.spriteBatch.DrawString(font, stringList[i], new Vector2(2 * Globals.screenWidth / 3 - 50, Globals.screenHeight / 2 + 50 * i), Color.Black);
            }
        }
    }
}
