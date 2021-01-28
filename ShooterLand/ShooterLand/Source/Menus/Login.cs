using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ShooterLand.Source.Engine.Basic2DObjects;
using ShooterLand.Source.Engine.Basic2DObjects.Buttons;
using ShooterLand.Source.Engine.Inputs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ShooterLand.Source.Menus
{
    
    public class Login
    {
        private AsyncButton loginButton;
        private Basic2D background, logo;
        private SpriteFont font;
        private Input inputUsername;
        private Input inputPassword;
        private bool emptyInfo;
        private bool wrongInfo;
        
        

        public Login(PassTask _login)
        {
           
            background= new Basic2D("2D\\UI\\Backgrounds\\background", new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2), new Vector2(Globals.screenWidth, Globals.screenHeight));
            logo = new Basic2D("2D\\UI\\logo", new Vector2(0, 0), new Vector2(600, 125));
            loginButton =new AsyncButton("2D\\Miscellaneous\\SimpleBtn", new Vector2(0, 0), new Vector2(96, 32), "Fonts\\Arial16", "Login", null, _login);
            font = Globals.content.Load<SpriteFont>("Fonts\\Arial16");
            inputUsername = new Input(new Vector2(0, 0), new Vector2(600, 50),false);
            inputPassword = new Input(new Vector2(0, 0), new Vector2(600, 50),true);
            emptyInfo = false;
            wrongInfo = false;
            
        }

        public Input GetUsernameInput()
        {
            return inputUsername;
        }

        public Input GetPasswordInput()
        {
            return inputPassword;
        }

        public void SetEmptyInfo(bool _info)
        {
            emptyInfo = _info;
        }

        public void SetWrongInfo(bool _info)
        {
            wrongInfo = _info;
        }

       
        public void Update()
        {
            inputUsername.Update(new Vector2(Globals.screenWidth / 2, 300), inputPassword);
            inputPassword.Update(new Vector2(Globals.screenWidth / 2, 450), inputUsername);
            loginButton.Update(new Vector2(Globals.screenWidth / 2 , 600));
           
        }

        
        public void Draw()
        {
            background.Draw(Vector2.Zero);
            logo.Draw(new Vector2(Globals.screenWidth / 2, Globals.screenHeight / 2 - 350));
            
            string tempStr = "Username: " ;
            Vector2 strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, 250), Color.Black);
            inputUsername.Draw(new Vector2(Globals.screenWidth / 2, 300));

            
            tempStr = "Password: ";
            strDims = font.MeasureString(tempStr);
            Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, 400), Color.Black);
            inputPassword.Draw(new Vector2(Globals.screenWidth / 2, 450));


            loginButton.Draw(new Vector2(Globals.screenWidth / 2, 600));

            if (emptyInfo)
            {
                tempStr = "Please provide both username and password";
                strDims = font.MeasureString(tempStr);
                Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, 700), Color.Black);
            }

            if (wrongInfo)
            {
                tempStr = "Password or username incorrect";
                strDims = font.MeasureString(tempStr);
                Globals.spriteBatch.DrawString(font, tempStr, new Vector2(Globals.screenWidth / 2 - strDims.X / 2, 700), Color.Black);
            }
           

        }
    }

}
