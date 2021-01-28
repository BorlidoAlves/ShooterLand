using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace GameServer
{
    class Server
    {
        public static int MaxPlayers{get;private set;}
        public static int Port { get; private set; }
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();
        private static TcpListener TcpListener;
        public delegate void PackeHandler(int _from, Packet _packet);
        public static Dictionary<int, PackeHandler> packetHandlers;

        public static void Start(int _maxPlayers,int _port)
        {
            MaxPlayers = _maxPlayers;
            Port = _port;
            InitializeServerData();

            Console.WriteLine($"Starting server...");

            TcpListener = new TcpListener(IPAddress.Any, Port);
            TcpListener.Start();
            TcpListener.BeginAcceptTcpClient(new AsyncCallback(TcpConnectCallback), null);

            Console.WriteLine($"Server started on port {Port}");

        }

        private static void TcpConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = TcpListener.EndAcceptTcpClient(_result);
            TcpListener.BeginAcceptTcpClient(new AsyncCallback(TcpConnectCallback), null);

            Console.WriteLine($"Incoming connection from {_client.Client.RemoteEndPoint}");
            for (int i = 1; i <= MaxPlayers; i++)
            {
                if (clients[i].tcp.socket == null)      //if this slot is empty
                {
                    clients[i].tcp.Connect(_client);
                    return;
                }
            }
            Console.WriteLine($"{_client.Client.RemoteEndPoint} failed to connect...The server is full");
        }
        
        private static void InitializeServerData()
        {
            for(int i = 1; i <= MaxPlayers; i++)
            {
                clients.Add(i,new Client(i));
            }

            packetHandlers = new Dictionary<int, PackeHandler>()
            {
                {(int)ClientPackets.welcomeReceived,ServerHandle.WelcomeReceived },
                {(int)ClientPackets.playerShoot,ServerHandle.PlayerShoot },
                {(int)ClientPackets.playerMovement, ServerHandle.PlayerMovement },
                {(int)ClientPackets.rivalCharacter, ServerHandle.RivalHandle }
            };

            Console.WriteLine("Initialized packets");
        }



    }
}
