using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.Android;

public class Request
{
    private readonly List<byte> data;
    public List<byte> Data
    {
        get => data;
    }

    public enum Type
    {
        INVALID,
        HELLO,
        USERNAME,
        MOVE,
        READY
    }

    public Request(List<byte> data)
    {
        this.data = data;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator ==(Request request, Type type)
    {
        return request.data[0] == (byte)type;
    }

    public static bool operator !=(Request request, Type type)
    {
        return request.data[0] != (byte)type;
    }
}