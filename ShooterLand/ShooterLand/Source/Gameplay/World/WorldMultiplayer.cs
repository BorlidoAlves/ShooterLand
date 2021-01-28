using Microsoft.Xna.Framework;
using ShooterLand.Source.Engine.Server;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ShooterLand.Source.Gameplay.World
{
    public class WorldMultiplayer:GameWorld
    {
        public static Character character2;
        private UI ui;
        

        public WorldMultiplayer(Character _character) :base(_character)
        {
            ClientHandle.myCharacter = character;
            ClientHandle.projectiles = projectiles;
            character2 = new Character();
            ui = new UI();

        }

        public Character GetRivalCharacter()
        {
            return character2;
        }
        public virtual void Update()
        {
            character.Update(offset,Scroll,map,projectiles,"multiplayer");

            //update projectiles
            for (int i = projectiles.Count - 1; i >= 0; i--)
            {
                projectiles[i].UpdateMultiplayer(offset, character, character2, map);
                if (projectiles[i].IsDone())
                {
                    projectiles.RemoveAt(i);
                }
            }

            ui.UpdateMultiplayer(character, character2);
        }
    
        public void Draw()
        {
            map.Draw(offset);
            character.Draw(offset);
            character2.Draw(offset);

            //draw the projectiles
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].Draw(offset);
            }

            ui.DrawMultiplayer(character, character2);
        }

    }
}
