using Microsoft.Xna.Framework;
using ShooterLand.Source.Engine.Basic2DObjects;
using ShooterLand.Source.Engine.Basic2DObjects.Buttons;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShooterLand.Source.Menus
{
    public class MainMenu
    {

        protected Basic2D background, logo;
        protected List<Button2D> buttons;

        public MainMenu(PassObject _playSingle,PassObject _playMulti,PassObject _exitClick, PassObject _intructionsClick)
        {
            buttons = new List<Button2D>();
            background = new Basic2D("2D\\UI\\Backgrounds\\background",new Vector2(Globals.screenWidth/2,Globals.screenHeight/2),new Vector2(Globals.screenWidth , Globals.screenHeight));
            logo = new Basic2D("2D\\UI\\logo", new Vector2(0,0), new Vector2(600,125));
            buttons.Add(new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(130, 32),"Fonts\\Arial16", "SinglePlayer", null, _playSingle));
            buttons.Add(new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(120, 32), "Fonts\\Arial16", "Multiplayer", null, _playMulti));
            buttons.Add(new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(120, 32), "Fonts\\Arial16", "Instructions", 3, _intructionsClick));
            buttons.Add(new MethodButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(96, 32), "Fonts\\Arial16", "Quit", null,_exitClick));

        }

        public virtual void Update()
        {
            for(int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Update(new Vector2(Globals.screenWidth/2, Globals.screenHeight / 2 - 100 + 45 * i));
            }
        }

        public virtual void Draw()
        {
            background.Draw(Vector2.Zero);
            logo.Draw(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 - 350));
            
            for (int i = 0; i < buttons.Count; i++)
            {
                buttons[i].Draw(new Vector2(Globals.screenWidth/2, Globals.screenHeight/ 2 - 100 + 45 * i));
            }
        }
    }
}
