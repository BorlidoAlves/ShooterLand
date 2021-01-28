using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class ServerHandle
    {   
        public static void WelcomeReceived(int _from ,Packet _packet)
        {
            int id = _packet.ReadInt();
            string username = _packet.ReadString();

            Console.WriteLine($"{username} connected sucessfully and is now player {_from}");
           
            if (id==2)
            {
                Console.WriteLine("Starting match...");
                ServerSend.ReadyToBeginPacket();
            }

            if (_from != id)
            {
                Console.WriteLine($"Player \"{username}\" (ID:{_from} has assummed the wrong client ID {id}!");
            }
        }

        public static void PlayerShoot(int _from,Packet _packet)
        {
            Vector2 position = _packet.ReadVector2();
            Vector2 target = _packet.ReadVector2();
            string type = _packet.ReadString();

            Console.WriteLine($"The client {_from} shot a {type} in the position {position}");
            ServerSend.ShootPacket(position,target,type, _from);
        }

        public static void PlayerMovement(int _from, Packet _packet)
        {
            Vector2 position = _packet.ReadVector2();
            float rotation = _packet.ReadFloat();

            //Console.WriteLine($"The client {_from} is in the position {position} with this rotation {rotation}");
            ServerSend.MovementPacket(position, rotation, _from);
        }

        public static void RivalHandle(int _from, Packet _packet)
        {
            Vector2 position = _packet.ReadVector2();
            string type = _packet.ReadString();


            Console.WriteLine($"The client {_from} position {position} type {type}");

            ServerSend.RivalPacket(position, type, _from);
        }
    }
}
