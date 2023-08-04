using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ERoomType
{
    Elite = 0,
    Enemy = 1,
    Merchant = 2,
    Rest = 3,
    Treasure = 4,
    Unknown = 5,
    Size = 6
}

public class MapGenerator : MonoBehaviour
{
    private const int _mapDefaultHeight = 16;

    private Room[,] _mapArray;

    [SerializeField] private StageData[] _stageData;

    [SerializeField] private Button _bossPrefab;
    [SerializeField] private Transform _stepPrefab;
    [SerializeField] private Room _roomPrefab;

    [SerializeField] private RectTransform _map;  // ������ ������Ʈ�� �θ� �� ������Ʈ

    [SerializeField] private float _mapXSize = 900;
    [SerializeField] private float _mapYSize = 2000;
    [SerializeField] private float _yOffset = 300;
    [SerializeField] private float _stepDistance = 20f;  // ���ڱ����� ����

    private RoomManager roomManager => ServiceLocator.Instance.GetService<RoomManager>();

    /// <summary>
    /// ���� �����ؼ� ��ȯ�ϴ� �޼ҵ��Դϴ�.
    /// ��� ���� ���� ũ��� �����մϴ�.
    /// </summary>
    /// <param name="height">���� ����</param>
    /// <param name="width">���� �ʺ�</param>
    /// <param name="pathCount">��� ����</param>
    /// <param name="isHeart">������̸� true, �ƴϸ� false </param>
    /// <returns></returns>
    public Room[,] GenerateMap(int width = 7, int pathCount = 5)
    {
        _mapArray = new Room[_mapDefaultHeight, width];

        for (int i = 0; i < pathCount; i++)
        {
            GenerateRoute();
        }

        DecideRoomType();
        VisualizeMap();

        return _mapArray;
    }

    // ��� ����
    private void GenerateRoute()
    {
        // 6�� �ݺ�
        int x = Random.Range(0, _mapArray.GetLength(1));
        List<int> xPosList = new List<int>();

        for (int y = 1; y < _mapArray.GetLength(0); y++)
        {
            // �����
            if (_mapArray[y, x] == null)
            {
                Vector3 pos = new Vector3(_mapXSize / (_mapArray.GetLength(1) - 1) * x - _mapXSize / 2, _mapYSize / (_mapArray.GetLength(0) - 1) * y - _mapYSize / 2 - _yOffset, 0);
                _mapArray[y, x] = Instantiate(_roomPrefab, _map);
                _mapArray[y, x].InitRoom(pos.x, pos.y);
            }

            xPosList.Add(x);

            // x ����
            if(x == 0)
            {
                x += Random.Range(0, 2);
            }
            else if(x == _mapArray.GetLength(1) - 1)
            {
                x += Random.Range(-1, 1);
            }
            else
            {
                x += Random.Range(-1, 2);
            }

        }
        MakeRoutine(xPosList);
    }

    // ��� �� �����߰�
    private void MakeRoutine(List<int> xPosList)
    {
        for (int i = xPosList.Count - 1; i >= 1; i--)
        {
            int x = xPosList[i];
            int y = i + 1;

            int prevX = xPosList[i - 1];
            int prevY = i;

            // ���� ������ �ٸ� ������ ũ�ν� �ϸ� �ٷ� �� �Ʒ��� ����

            // �� ����(��)
            if (x + 1 < _mapArray.GetLength(1) && _mapArray[y, x + 1] != null && _mapArray[y - 1, x] != null &&
                _mapArray[y - 1, x].connectedRooms.Contains(_mapArray[y, x + 1]) &&
                x + 1 == prevX)
            {
                _mapArray[y - 1, x].connectedRooms.Add(_mapArray[y, x]);
                _mapArray[prevY, prevX].connectedRooms.Add(_mapArray[y, x + 1]);
            }
            // �� ���� (��)
            else if (x - 1 >= 0 && _mapArray[y, x - 1] != null && _mapArray[y - 1, x] != null &&
                _mapArray[y - 1, x].connectedRooms.Contains(_mapArray[y, x - 1]) &&
                x - 1 == prevX)
            {
                _mapArray[y - 1, x].connectedRooms.Add(_mapArray[y, x]);
                _mapArray[prevY, prevX].connectedRooms.Add(_mapArray[y, x - 1]);
            }
            else
            {
                _mapArray[prevY, prevX].connectedRooms.Add(_mapArray[y, x]);
            }
        }
    }

    // ���� Ÿ���� ���� �꿡 �°� ����
    private void DecideRoomType()
    {
        // dfs �˰���

        // ��, ����Ʈ�� �ּ� 6������ ����
        // �̺�Ʈ, �Ϲݸ��� ���޾� ���尡��
        // �� �濡�� �� �� �ִ� ���� ����� ���� Ÿ���� ���� �� ����
        for (int x = 0; x < _mapArray.GetLength(1); x++)
        {
            if(_mapArray[1, x] != null)
            {
                DfsMapTraversal(1, _mapArray[1, x]);
            }
        }
    }

    // mapArray ����ȭ
    private void VisualizeMap()
    {
        // ���(�̹���) �ð�ȭ
        for (int y = 1; y < _mapArray.GetLength(0) - 1; y++)
        {
            for (int x = 0; x < _mapArray.GetLength(1); x++)
            {
                if (_mapArray[y, x] != null)
                {
                    // ���ڱ�
                    Room currentRoom = _mapArray[y, x];
                    
                    // �ش� �濡 ����� ��� �� ���ڱ�(�̹���) �ð�ȭ
                    for (int i = 0; i < currentRoom.connectedRooms.Count; i++)
                    {
                        float currentPosX = currentRoom.PosX;
                        float currentPosY = currentRoom.PosY;

                        Room connectedRoom = currentRoom.connectedRooms[i];
                        float nextPosX = connectedRoom.PosX;
                        float nextPosY = connectedRoom.PosY;

                        Vector3 dir = new Vector3(nextPosX - currentPosX, nextPosY - currentPosY, 0).normalized;
                        float theta = -Mathf.Atan(dir.x / dir.y) * Mathf.Rad2Deg;

                        while (true)
                        {
                            if (currentPosY > nextPosY)
                                break;

                            Vector3 pos = new Vector3(currentPosX, currentPosY, 0) + dir * _stepDistance;
                            Transform step = Instantiate(_stepPrefab, _map);
                            step.transform.localPosition = pos;
                            step.transform.localEulerAngles = new Vector3(0, 0, theta);

                            currentPosX = pos.x;
                            currentPosY = pos.y;
                        }
                    }
                }
            }
        }

        // ������ ������������ ������ ���ڱ� �ð�ȭ
        for (int x = 0; x < _mapArray.GetLength(1); x++)
        {
            if (_mapArray[_mapArray.GetLength(0) - 1, x] != null)
            {
                // ���ڱ�
                Room currentRoom = _mapArray[_mapArray.GetLength(0) - 1, x];

                float currentPosX = currentRoom.PosX;
                float currentPosY = currentRoom.PosY;


                Vector3 dir = new Vector3(-currentPosX, 1070f - currentPosY, 0).normalized;
                float theta = -Mathf.Atan(dir.x / dir.y) * Mathf.Rad2Deg;

                while (true)
                {
                    if (currentPosY > 870f)
                        break;

                    Vector3 pos = new Vector3(currentPosX, currentPosY, 0) + dir * _stepDistance;
                    Transform step = Instantiate(_stepPrefab, _map);
                    step.transform.localPosition = pos;
                    step.transform.localEulerAngles = new Vector3(0, 0, theta);

                    currentPosX = pos.x;
                    currentPosY = pos.y;
                }
            }
        }


        // ��������(��ư) �ð�ȭ
        for (int y = 0; y < _mapArray.GetLength(0); y++)
        {
            for (int x = 0; x < _mapArray.GetLength(1); x++)
            {
                if (_mapArray[y, x] != null)
                {
                    _mapArray[y, x].Positioning();
                }
            }
        }

        // ���� �ð�ȭ
        Button bossStage = Instantiate(_bossPrefab, _map);
        bossStage.transform.localPosition = new Vector3(0, 1070f, 0);

        bossStage.onClick.AddListener(() => roomManager.OnEnterBossRoom());
    }


    /// <summary>
    /// dfs�˰������� ���� ���鼭 �ش� ���� ������ �����մϴ�.
    /// </summary>
    /// <param name="height">���� ����(��)</param>
    /// <param name="room">�ش� ��</param>
    /// <param name="possibleRoomType">���� �Լ����� ������ �� ����</param>
    private Room DfsMapTraversal(int height, Room room, List<int> possibleRoomType = null)
    {
        // �̹� ������ ���̶�� return;
        if (room.IsGenerate)
            return room;

        // ���� �Լ��� possibleRoomType�� �Ѱ��� ����Ʈ
        // 4�� ��������. ���� ����Ʈ�� ���� ����
        List<int> originPossibleRoomTypes = new List<int>() { 0, 1, 2, 3, 5 };

        if(possibleRoomType?.Count <= 0)
        {
            Debug.Log("�װ��� �� �� �ִ� ���� �����ϴ�.");
        }

        // 1�� �Ϲ� ��
        if (height == 1)
        {
            room.SetStageType(_stageData[(int)ERoomType.Enemy], ERoomType.Enemy);
        }
        // 9�� ���� ��
        else if (height == 9)
        {
            room.SetStageType(_stageData[(int)ERoomType.Treasure], ERoomType.Treasure);
        }
        // 15�� �޽� ��
        else if (height == 15)
        {
            room.SetStageType(_stageData[(int)ERoomType.Rest], ERoomType.Rest);
            originPossibleRoomTypes.Remove((int)ERoomType.Rest);
        }
        else
        {
            // 15���� ������ �޽� ���̹Ƿ� 14������ �޽Ĺ��� ���ܽ�Ų��.
            if (height == 14)
            {
                possibleRoomType.Remove((int)ERoomType.Rest);
            }


            // ���� ���Ϸ��� ��� �̾��� ���߿� �̹� ������ ���� �ִٸ� �� ���� Ÿ�Ե� ����(�� �Ϲݸ�, ������ ���޾� �����ϱ� ������ ���� ����)
            for (int i = 0; i < room.connectedRooms.Count; i++)
            {
                Room nextRoom = room.connectedRooms[i];
                if (nextRoom.IsGenerate && nextRoom.RoomType != ERoomType.Enemy && nextRoom.RoomType != ERoomType.Unknown)
                {
                    possibleRoomType.Remove((int)nextRoom.RoomType);
                }
            }

            // �� ���� Ȯ���� ����Ͽ� ���� �� �ε���
            int randomRoomType = SelectRoomWeightRandom(possibleRoomType);

            room.SetStageType(_stageData[randomRoomType], (ERoomType)randomRoomType);

            // ������ �� ���� (���� ���޾� ������ �ȵ�. �� �Ϲ����� ������ ����)
            if (randomRoomType != (int)ERoomType.Enemy && randomRoomType != (int)ERoomType.Unknown)
                originPossibleRoomTypes.Remove(randomRoomType);
        }

        

        for(int i = 0; i < room.connectedRooms.Count; i++)
        {
            Room nextRoom = room.connectedRooms[i];

            // 6������ �ؿ� ������ ����Ʈ, �޽� �� �� ����
            if(height < 6)
            {
                originPossibleRoomTypes.Remove((int)ERoomType.Elite);
                originPossibleRoomTypes.Remove((int)ERoomType.Rest);
            }

            // ����Լ��� ��ȯ������ �ش� ��� ����� ��(���õ� ��)�� ������.
            // ��ȯ�� ���� null�� �ƴ϶�� originPossibleRoomTypes���� ����.
            // �� �濡�� �� �� �ִ� ���� ����� ���� Ÿ���� ���� �� ����.
            Room returnedNextRoom = DfsMapTraversal(height + 1, nextRoom, originPossibleRoomTypes);
            if(returnedNextRoom != null)
            {
                originPossibleRoomTypes.Remove((int)returnedNextRoom.RoomType);
            }
        }

        return room;
    }

    /// <summary>
    /// ����ġ ���� �̱�
    /// </summary>
    /// <param name="possibleRoomType">�� ����Ʈ �߿��� ����ġ ������ ����</param>
    /// <returns>����ġ �������� ���� ���� ��</returns>
    private int SelectRoomWeightRandom(List<int> possibleRoomType)
    {

        int allPercentage = 0;   // possibleRoomType�� ��� Ȯ�� ���� ��
        int currentPercentageSum = 0; // ���ʴ�� Ȯ������ �����Ͽ� ���� ��
        int selectedRoomIndex = 0; // ��ȯ�� ��

        foreach (int roomIndex in possibleRoomType)
        {
            allPercentage += _stageData[roomIndex].percentage;
        }

        int percentage = Random.Range(0, allPercentage + 1);  // �������� ���� Ȯ�� ��

        foreach (int roomIndex in possibleRoomType)
        {
            currentPercentageSum += _stageData[roomIndex].percentage;
            if (percentage <= currentPercentageSum)
            {
                selectedRoomIndex = roomIndex;
                break;
            }
        }

        return selectedRoomIndex;
    }
}

