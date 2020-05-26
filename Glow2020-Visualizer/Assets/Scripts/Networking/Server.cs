using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using UnityEngine;

public static class Server
{
    public static int maxClients { get; private set; }
    public static int port { get; private set; }
    public static Dictionary<int, Client> clients = new Dictionary<int, Client>();
    public delegate void PacketHandler(int clientID, PacketHandler packet);
    public static Dictionary<int, PacketHandler> packetHandlers;

    private static UdpClient udpListener;

    public static void start( int _maxClients,int _port)
    {
        maxClients = _maxClients;
        port = _port;




        udpListener = new UdpClient(Server.port);
        udpListener.BeginReceive(UDPReceiveCallback, null);

        Debug.Log("Server started.");
    }

    private static void InitializeServer()
    {
        for(int i = 1; i <= maxClients; i++)
        {
            clients.Add(i, new Client(i));
        }

        //packetHandlers = new Dictionary<int, PacketHandler>();
        //{
        //    { (int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
        //    { (int)ClientPackets.udpTestReceived, ServerHandle.UDPTestReceived }
        //};
        Debug.Log("Server initialized");

    }

    private static void UDPReceiveCallback(IAsyncResult _result)
    {
        try
        {
            IPEndPoint clientEndpoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = udpListener.EndReceive(_result, ref clientEndpoint);
            udpListener.BeginReceive(UDPReceiveCallback, null);

            if(data.Length < 4)
            {
                Debug.LogWarning("Warning data of udp packet was too short: " + data.ToString());
                return;
            }

            using(Packet packet = new Packet())
            {
                int clientID = packet.ReadInt();

                if(clientID == 0)
                {
                    Debug.LogError("Error 0 clientID given.");
                    return;
                }
                
                //if(clients[clientID].udp.endpoint == null)
                //{
                //    clients[clientID].udp.Connect(clientEndpoint);
                //    return;
                //}

                //if (clients[clientID].udp.endPoint.ToString() == clientEndpoint.ToString())
                //{
                //    clients[clientID].udp.HandleData(packet);
                //}

            }

        }
        catch(Exception _ex)
        {
            Debug.LogError($"Error while receiving UDP data: {_ex}");
        }
    }

    private static void SendUDPData(IPEndPoint _clientEndPoint, Packet _packet)
    {
        try
        {
            if(_clientEndPoint != null)
            {
                //udpListener.BeginSend(_packet.ToArray(), _packet.Length(), _clientEndPoint, null, null);
            }
        }
        catch(Exception _ex)
        {
            Debug.LogError($"Error sending data to {_clientEndPoint} via UDP: {_ex}");
        }
    }

}
