using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEx
{
    private Room[,] _mapArray = new Room[16, 7];
    private Room _selectedRoom; // 터치한 방
    private Room _currentRoom; // 현재 위치한 방

    public Room SelectedRoom
    {
        get { return _selectedRoom; }
        set
        {
            if (_selectedRoom != null)
                _selectedRoom.IsBigger = false;

            _selectedRoom = value;

            _selectedRoom.IsBigger = true;
        }
    }

    public Room CurrentRoom
    {
        get { return _currentRoom; }
        set
        {
            _currentRoom = value;

            for (int y = 0; y < _mapArray.GetLength(0); y++)
            {
                for (int x = 0; x < _mapArray.GetLength(1); x++)
                {
                    if (_mapArray[y, x] != null)
                    {
                        _mapArray[y, x].IsGoable = false;
                    }
                }
            }
        }
    }


    public void Init()
    {

    }

    public void SetMapArray(Room[,] mapArray)
    {
        _mapArray = mapArray;
    }


    public void StartMap()
    {
        for (int x = 0; x < _mapArray.GetLength(1); x++)
        {
            if (_mapArray[1, x] != null)
            {
                _mapArray[1, x].IsGoable = true;
            }
        }
    }

    public void ShowRoomWithType(ERoomType roomType)
    {
        for (int y = 0; y < _mapArray.GetLength(0); y++)
        {
            for (int x = 0; x < _mapArray.GetLength(1); x++)
            {
                if (_mapArray[y, x] != null)
                {
                    if(_mapArray[y, x].RoomType == roomType)
                    {
                        _mapArray[y, x].IsHighlight = true;
                    }
                    else
                    {
                        _mapArray[y, x].IsHighlight = false;
                    }
                }
            }
        }
    }
}
