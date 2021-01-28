using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{

    //All the methods to create the various packets that will be sent will be defined here
    class ServerSend
    {

        public static void WelcomePacket(int _toClient,string _message)     //_toClient is the client who will receive the packet
        {
            Vector2 position;

            if (_toClient == 1)
            {
                position = new Vector2(500,300);
            }

            else
            {
                position = new Vector2(1500, 800);
            }
            //Packet inherits from IDisposable so it has to be disposed when it's done.
            //This action could be done by calling Dispose method instead of defining the Packet instance inside a using block
            //This approach was choosen because it disposes the packet automatically and it´s cleaner
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                Console.WriteLine($"ID DO CLIENTE : {_toClient}");
                _packet.Write(_message);
                _packet.Write(_toClient);
                _packet.Write(position);
                

                SendTCPData(_toClient, _packet);
            }
        }

        //shooter is the player who made the shot. This way, everyone will receive the package except him
        public static void ShootPacket(Vector2 _position, Vector2 _target, string _type,int _shooter)
        {
            using (Packet _packet = new Packet((int)ServerPackets.shoot))
            {
                _packet.Write(_position);
                _packet.Write(_target);
                _packet.Write(_type);

                SendTCPDataToAll(_shooter, _packet);
            }
            
        }

        public static void ReadyToBeginPacket()
        {
            using (Packet _packet = new Packet((int)ServerPackets.readyToPlay))
            {
                string message = "Both players ready to begin the match";
                _packet.Write(message);

                SendTCPDataToAll(_packet);
            }
        }

        public static void MovementPacket(Vector2 _position, float _rotation, int _from)
        {
            using (Packet _packet = new Packet((int)ServerPackets.movement))
            {
                _packet.Write(_position);
                _packet.Write(_rotation);

                SendTCPDataToAll(_from, _packet);
            }
        }

        public static void RivalPacket(Vector2 _position, string _type, int _from)
        {
            using (Packet _packet = new Packet((int)ServerPackets.sendRival))
            {
                _packet.Write(_position);
                _packet.Write(_type);

                SendTCPDataToAll(_from, _packet);
            }
        }

        private static void SendTCPData(int _toClient,Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }
        
        //sends TCP data to all the connected clients
        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for(int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }

        //sends TCP data to all the connected clients except the one specified in _except
        private static void SendTCPDataToAll(int _except,Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _except)
                {
                    Server.clients[i].tcp.SendData(_packet);
                    
                }
                
            }
        }
    }
}
