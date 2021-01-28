using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using ShooterLand.Source.Engine;
using ShooterLand.Source.Engine.Server;
using ShooterLand.Source.Gameplay;
using ShooterLand.Source.Menus;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

namespace ShooterLand
{
    public class ShooterLand : Game
    {
        private GraphicsDeviceManager _graphics;
        private SinglePlayer single;
        private MultiPlayer multi;
        private MainMenu mainMenu;
        private Instructions instructions;
        private int userChoice;
        private CultureInfo culture;
        private Login loginMenu;
        private Client client;
        

        public ShooterLand()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

             Globals.screenWidth = 1920;
             Globals.screenHeight = 1080;
          
            userChoice = 0;

            _graphics.PreferredBackBufferWidth = Globals.screenWidth;
            _graphics.PreferredBackBufferHeight = Globals.screenHeight;
            _graphics.ApplyChanges();

            Communication.Initialize();
            
            client = new Client();

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            Globals.content = this.Content;
            Globals.spriteBatch = new SpriteBatch(GraphicsDevice);
            single = new SinglePlayer();
            multi = new MultiPlayer(client);
            instructions = new Instructions(ChangeUserChoice);
            Globals.normalEffect = Globals.content.Load<Effect>("2D\\Effects\\NormalFlat");
           // Globals.hitEffect = Globals.content.Load<Effect>("2D\\Effects\\Throb");
            culture = new System.Globalization.CultureInfo("en-US");
            mainMenu = new MainMenu(PlaySingle,PlayMulti,ExitGame,ChangeUserChoice);
            loginMenu = new Login(Login);
            
        }

        protected override void Update(GameTime _gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Globals.gameTime = _gameTime;
            if(single.Return())
            {
                userChoice = 1;
                single.SetReturn(false);
            }

            if (multi.Return())
            {
                userChoice = 1;
                multi.SetReturn(false);
            }

            //when the user is in the login
            if (userChoice == 0)
            {
                loginMenu.Update();
            }

            //when the user is in the menu
            if (userChoice == 1)
            {            
                mainMenu.Update();
            }

            //user choose to play singleplayer
            else if(userChoice==2)
            {
                single.Update();
            }
            else if(userChoice == 3)
            {
                instructions.Update();
            }

            else if (userChoice == 4)
            {
                multi.Update();
            }
            

            ThreadManager.Update();

            base.Update(_gameTime);
        }

        public virtual void ChangeUserChoice(object _info)
        {
            userChoice = Convert.ToInt32(_info, culture);
        }

        public virtual void ExitGame(object _info)
        {
            Environment.Exit(0);
        }

        public virtual async Task Login(object _info)
        {
            string tempUsername = loginMenu.GetUsernameInput().GetInputText();
            string tempPassword = loginMenu.GetPasswordInput().GetInputText();
            if (tempUsername.Equals("") || tempPassword.Equals(""))
            {
                // no login with missing credentials
                loginMenu.SetWrongInfo(false);
                loginMenu.SetEmptyInfo(true);
            }

            else
            {
                 User tempUser = await Communication.Login(tempUsername, tempPassword);
                 if (tempUser!=null){
                    Globals.loggedUser = tempUser;
                    Debug.WriteLine("Login Successuul");
                    ChangeUserChoice(1);
                 }

                 else
                 {
                     loginMenu.SetEmptyInfo(false);
                     loginMenu.SetWrongInfo(true);
                 }
            }
        }

        public virtual void PlaySingle(object _info)
        {
            single = new SinglePlayer();
            ChangeUserChoice(2);
        }

        public void PlayMulti(object _info)
        {
            multi = new MultiPlayer(client);
            ChangeUserChoice(4);

            
        }

        


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Globals.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            if (userChoice == 0)
            {
                loginMenu.Draw();
            }
            if (userChoice == 1)
            {
                mainMenu.Draw();
            }
            else if (userChoice == 2)
            {
                single.Draw();
            }
            else if (userChoice == 3)
            {
                instructions.Draw();
            }
           
            else if (userChoice == 4)
            {
                multi.Draw();
            }

            Globals.spriteBatch.End();

            
            base.Draw(gameTime);
        }
    }

    public static class Program
    {
        static void Main()
        {
            using (var game = new ShooterLand())
                game.Run();
        }
    }
}
