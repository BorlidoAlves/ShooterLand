using Microsoft.Xna.Framework;
using ShooterLand.Source.Gameplay.World;
using ShooterLand.Source.Gameplay.World.Projectiles;
using ShooterLand.Source.Gameplay.World.Units.Characters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ShooterLand.Source.Engine.Server
{
    class ClientHandle
    {
        public static bool readyToStart = false;
        private static Vector2 spawningPosition=new Vector2();
        public static Character myCharacter;
        public static List<Projectile> projectiles;
        

        public static void Welcome(Packet _packet)
        {
            
            string message = _packet.ReadString();
            int id = _packet.ReadInt();
            spawningPosition = _packet.ReadVector2();
            Debug.WriteLine($"Message from the server :{message} ");
            Debug.WriteLine($"Spawning position: {spawningPosition}");
            Client.client.myId = id;
            ClientSend.WelcomeReceived();
            
        }

        public static void Shoot(Packet _packet)
        {
            Vector2 position = _packet.ReadVector2();
            Vector2 target = _packet.ReadVector2();
            string type = _packet.ReadString();
            Projectile tempProjectile;

            if (type == "arrow")
            {
                tempProjectile = new Arrow(position, WorldMultiplayer.character2, target);
            }
            else if (type == "fire")
            {
                tempProjectile= new Fireball(position, WorldMultiplayer.character2, target);
            }
            else
            {
                tempProjectile= new IceShard(position, WorldMultiplayer.character2, target);
            }
            projectiles.Add(tempProjectile);
            //create a projectile of the specified type and add it to the list of projectiles

            //Debug.WriteLine($"Message from the server : A {type} was shot from the position {position} with targer {target} ");
        }

        public static void StartMatch(Packet _packet)
        {
            string message = _packet.ReadString();
            Debug.WriteLine($"Message from the server : {message} ");
            readyToStart = true;
            myCharacter.SetPosition(spawningPosition);
            myCharacter.SetMaxHealth(500);
            myCharacter.SetHealth(500);
            ClientSend.SendCharacter(spawningPosition, myCharacter.GetName());

        }

        public static void Movement(Packet _packet)
        {
            Vector2 position = _packet.ReadVector2();
            float rotation = _packet.ReadFloat();

            WorldMultiplayer.character2.SetPosition(position);
            WorldMultiplayer.character2.SetRotation(rotation);

            //Debug.WriteLine($"Message from the server: Enemy player in position {position} with rotation {rotation} ");
        }

        public static void RivalCharacter(Packet _packet)
        {
            Vector2 position = _packet.ReadVector2();
            string type = _packet.ReadString();

            if(type == "The Archer")
            {
                WorldMultiplayer.character2 = new Archer(position,2);
            }
            else if(type == "The Ice Mage")
            {
                WorldMultiplayer.character2 = new IceMage(position, 2);
            }
            else
            {
                WorldMultiplayer.character2 = new Mage(position, 2);
            }
            WorldMultiplayer.character2.SetMaxHealth(500);
            WorldMultiplayer.character2.SetDimentions(new Vector2(80, 80));
            WorldMultiplayer.character2.SetHealth(500);

            Debug.WriteLine($"Message from the server: Enemy player in position {position} type {type} ");
        }
    }
}
