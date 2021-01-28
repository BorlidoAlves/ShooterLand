using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ShooterLand.Source.Engine;
using System;
using System.Threading.Tasks;

namespace ShooterLand
{

   //delegate allows to pass methods as argument
   public delegate void PassObject(object i);
   public delegate Task PassTask(object i);

     public class Globals
    {
        public static int screenHeight, screenWidth;
        public static ContentManager content;
        public static SpriteBatch spriteBatch;
        public static GameTime gameTime;
        public static Effect normalEffect, hitEffect;
        public static User loggedUser;
    }
}
