using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ServerPackets
{
    welcome = 1,
    udpTest
}

public enum ClientPackets
{
    welcomeReceived = 1,
    udpTestReceived
}

public class Packet : IDisposable
{
    private List<Byte> buffer;
    private byte[] readableBuffer;
    private int readPos;

    public Packet()
    {
        buffer = new List<byte>();
        readPos = 0;
    }

    public Packet(int _id)
    {
        buffer = new List<Byte>();
        readPos = 0;

        Write<int>(_id);
    }

    public void SetBytes(byte[] _data)
    {
        Write(_data);
        readableBuffer = buffer.ToArray();
    }


    public void Write<T>(T[] _data)
    {
        int size = System.Runtime.InteropServices.Marshal.SizeOf<T>();
        Byte[] byteArray = new byte[_data.Length * size];
        Buffer.BlockCopy(_data, 0, byteArray, 0, byteArray.Length);
        buffer.AddRange(byteArray);
    }
    public void Write<T>(T _data)
    {
        int size = System.Runtime.InteropServices.Marshal.SizeOf<T>();
        Byte[] byteArray = new byte[size];
        
        buffer.AddRange(byteArray);
    }

    public int ReadInt()
    {
        return 0;
    }

    void IDisposable.Dispose()
    {
        throw new NotImplementedException();
    }
}
