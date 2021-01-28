using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace ShooterLand.Source.Engine.Server
{
   public class Client
    {
        public static Client client;
        public static int dataBufferSize = 4096;
        public string ip = "127.0.0.1";     //ip for localhost
        public int port = 26950;            //same port setted up in server
        public int myId;
        public TCP tcp;
        public static Vector2 spawningPosition;

        private bool isConnected = false;
        private delegate void PacketHandler(Packet _packet);
        private static Dictionary<int, PacketHandler> packetHandlers;

        public Client()
        {
            if (client == null)
            {
                client = this;
            }
            tcp=new TCP();
        }

        public void ConnectToServer()
        {
            if (!isConnected) { 
                InitializeClientData();
                isConnected = true;
                tcp.Connect();
                
            }
        }

        public void ServerDisconnect()
        {
            Disconnect();
        }

        public class TCP
        {
            public TcpClient socket;
            public NetworkStream stream;
            private byte[] receiveBuffer;
            private Packet receivedData;

            public void Connect()
            {

                socket = new TcpClient()
                {
                    ReceiveBufferSize = dataBufferSize,
                    SendBufferSize = dataBufferSize
                };

                receiveBuffer = new byte[dataBufferSize];
                socket.BeginConnect(client.ip, client.port, ConnectionCallback, socket);
            }

            private void ConnectionCallback(IAsyncResult _result)
            {
                socket.EndConnect(_result);

                if (!socket.Connected)
                {
                    
                    return;
                }

                stream = socket.GetStream();
                receivedData = new Packet();
                
               
                stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
            }

            private void ReceiveCallback(IAsyncResult _result)
            {
                

                try
                {
                    int _byteLenght = stream.EndRead(_result);
                    if (_byteLenght <= 0)
                    {
                        client.Disconnect();
                        return;
                    }
                    byte[] _data = new byte[_byteLenght];
                    Array.Copy(receiveBuffer, _data, _byteLenght);

                    
                    receivedData.Reset(HandleData(_data));

                    stream.BeginRead(receiveBuffer, 0, dataBufferSize, ReceiveCallback, null);
                }
                catch (Exception _ex)
                {
                    Console.WriteLine($"Error receiving TCP data {_ex}");
                    Disconnect();
                }

                
            }
            private void Disconnect()
            {
                client.Disconnect();
                stream = null;
                receiveBuffer = null;
                receivedData = null;
            }
            public void SendData(Packet _packet)
            {
                try
                {
                    if (socket != null)
                    {
                        stream.BeginWrite(_packet.ToArray(), 0, _packet.Length(), null, null);
                    }
                }
                catch(Exception _ex)
                {
                    Debug.WriteLine($"Error sending data to server via TCP: {_ex}");
                }
            }

            private bool HandleData(byte[] _data)
            {
                int packetLenght = 0;

                receivedData.SetBytes(_data);

                if (receivedData.UnreadLength() >= 4)
                {
                    packetLenght = receivedData.ReadInt();
                    if (packetLenght <= 0)
                    {
                        return true;
                    }
                }
                while(packetLenght>0 && packetLenght <= receivedData.UnreadLength())
                {
                    byte[] packetBytes = receivedData.ReadBytes(packetLenght);
                    
                    ThreadManager.ExecuteOnMainThread(() =>
                    {
                        using (Packet packet = new Packet(packetBytes))
                        {
                            int packetId = packet.ReadInt();
                            packetHandlers[packetId](packet);
                        }
                    });
                    packetLenght = 0;

                    if (receivedData.UnreadLength() >= 4)
                    {
                        packetLenght = receivedData.ReadInt();
                        if (packetLenght <= 0)
                        {
                            return true;
                        }
                    }
                }
                if (packetLenght <= 1)
                {
                    return true;
                }

                return false;
            }

        }
       private void InitializeClientData()
        {
            packetHandlers = new Dictionary<int, PacketHandler>()
            {
                {(int)ServerPackets.welcome,ClientHandle.Welcome },
                {(int)ServerPackets.shoot,ClientHandle.Shoot },
                {(int)ServerPackets.bothPlayersReady,ClientHandle.StartMatch },
                {(int)ServerPackets.movement,ClientHandle.Movement },
                {(int)ServerPackets.rivalCharacter, ClientHandle.RivalCharacter }

            };
            Debug.WriteLine("Initialized data");
        }

        private void Disconnect()
        {
            if (isConnected)
            {
                isConnected = false;
                tcp.socket.Close();

                Debug.WriteLine("Disconnected from server");
            }
        }
    }
}
