using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.Managers;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Gameplay.World.Boosts;
using ShooterLand.Source.Gameplay.World.Projectiles;
using ShooterLand.Source.Gameplay.World.SpawnPoints;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using ShooterLand.Source.Gameplay.World.Units.Enemies;
using ShooterLand.Source.Menus;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ShooterLand.Source.Gameplay.World
{
   public class GameWorld
    {
        protected Character character;
        protected Vector2 offset;
        protected List<Projectile> projectiles;
        protected MapManager map;
        
        
        
        public GameWorld(Character _character)
        {
            map = new MapManager();
            projectiles = new List<Projectile>();
            character = _character;
         
            offset = new Vector2(0, 0);
        }

        public Character GetCharacter()
        {
            return character;
        }

        public virtual void Scroll(object _info)
        {
            Vector2 tempPosition = (Vector2)_info;

            if (tempPosition.X < -offset.X + (Globals.screenWidth * .4f) && offset.X < 30)
            {
                offset = new Vector2(offset.X + character.GetSpeed() * 2, offset.Y);

            }

            if (tempPosition.X > -offset.X + (Globals.screenWidth * .6f) && offset.X > 31)
            {
                offset = new Vector2(offset.X - character.GetSpeed() * 2, offset.Y);

            }

            if (tempPosition.Y < -offset.Y + (Globals.screenHeight * .4f) && offset.Y < 33)
            {
                offset = new Vector2(offset.X, offset.Y + character.GetSpeed() * 2);

            }

            if (tempPosition.Y > -offset.Y + (Globals.screenHeight * .6f) && offset.Y > -843)
            {
                offset = new Vector2(offset.X, offset.Y - character.GetSpeed() * 2);

            }
        }

        public virtual void ResetScroll(object _info)
        {
            offset = new Vector2(0, 0);
        }



    }
}
