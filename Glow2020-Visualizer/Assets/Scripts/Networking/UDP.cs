using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using UnityEngine;

public class UDP
{
    public IPEndPoint endPoint;

    private int id;

    public UDP(int _id)
    {
        id = _id;
    }

    public void Connect(IPEndPoint _endPoint)
    {
        endPoint = _endPoint;
        //ServerSend.UDPTest();
    }

    public void SendPacket(Packet _packet)
    {

    }

}
