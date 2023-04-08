using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    private float _posX;
    private float _posY;
    private bool _isGenerate;

    public float PosX => _posX;
    public float PosY => _posY;
    public bool IsGenerate { get { return _isGenerate; } set { _isGenerate = value; } }

    public Room(float posX, float posY)
    {
        _posX = posX;
        _posY = posY;

        _isGenerate = false;
    }
}
