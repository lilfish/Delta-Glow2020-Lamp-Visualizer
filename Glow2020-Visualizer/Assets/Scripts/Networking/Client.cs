using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Client
{
    public static int dataBufferSize = 4096;

    public int id;
    public UDP udp;

    public Client(int _clientID)
    {
        id = _clientID;
        udp = new UDP(1);
    }



}
