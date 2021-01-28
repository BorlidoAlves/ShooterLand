using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

using System.Text;

namespace ShooterLand.Source.Engine.Server
{
    class ClientSend
    {

        private static void SendTCPData(Packet _packet)
        {
            _packet.WriteLength();
            Client.client.tcp.SendData(_packet);
        }

        //creates the packet to be sent to the server when the welcome packet is received
        public static void WelcomeReceived()
        {
            using(Packet _packet=new Packet((int)ClientPackets.welcomeReceived))
            {
                _packet.Write(Client.client.myId);
                _packet.Write(Globals.loggedUser.UserName);

                SendTCPData(_packet);
            }
        }

        public static void PlayerShoot(Vector2 _position, Vector2 _target,string _type)
        {
            using (Packet _packet = new Packet((int)ClientPackets.playerShoot))
            {
                _packet.Write(_position);
                _packet.Write(_target);
                _packet.Write(_type);

                SendTCPData(_packet);
            }
        }

        public static void PlayerMovement(Vector2 _position, float _rotation)
        {
            using (Packet _packet = new Packet((int)ClientPackets.playerMovement))
            {
                _packet.Write(_position);
                _packet.Write(_rotation);

                SendTCPData(_packet);
            }
        }

        public static void SendCharacter(Vector2 _position, string _type)
        {
            using (Packet _packet = new Packet((int)ClientPackets.sendCharacter))
            {
                _packet.Write(_position);
                _packet.Write(_type);

                SendTCPData(_packet);
            }
        } 
    }
}
