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

    [SerializeField] private RectTransform _map;  // 생성할 오브젝트의 부모가 될 오브젝트

    [SerializeField] private float _mapXSize = 900;
    [SerializeField] private float _mapYSize = 2000;
    [SerializeField] private float _yOffset = 300;
    [SerializeField] private float _stepDistance = 20f;  // 발자국간의 간격

    private RoomManager roomManager => ServiceLocator.Instance.GetService<RoomManager>();

    /// <summary>
    /// 맵을 생성해서 반환하는 메소드입니다.
    /// 모든 씬의 맵의 크기는 동일합니다.
    /// </summary>
    /// <param name="height">맵의 높이</param>
    /// <param name="width">맵의 너비</param>
    /// <param name="pathCount">경로 개수</param>
    /// <param name="isHeart">심장방이면 true, 아니면 false </param>
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

    // 경로 생성
    private void GenerateRoute()
    {
        // 6번 반복
        int x = Random.Range(0, _mapArray.GetLength(1));
        List<int> xPosList = new List<int>();

        for (int y = 1; y < _mapArray.GetLength(0); y++)
        {
            // 방생성
            if (_mapArray[y, x] == null)
            {
                Vector3 pos = new Vector3(_mapXSize / (_mapArray.GetLength(1) - 1) * x - _mapXSize / 2, _mapYSize / (_mapArray.GetLength(0) - 1) * y - _mapYSize / 2 - _yOffset, 0);
                _mapArray[y, x] = Instantiate(_roomPrefab, _map);
                _mapArray[y, x].InitRoom(pos.x, pos.y);
            }

            xPosList.Add(x);

            // x 갱신
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

    // 경로 간 간선추가
    private void MakeRoutine(List<int> xPosList)
    {
        for (int i = xPosList.Count - 1; i >= 1; i--)
        {
            int x = xPosList[i];
            int y = i + 1;

            int prevX = xPosList[i - 1];
            int prevY = i;

            // 이을 간선이 다른 간선에 크로스 하면 바로 위 아래와 연결

            // 위 기준(↘)
            if (x + 1 < _mapArray.GetLength(1) && _mapArray[y, x + 1] != null && _mapArray[y - 1, x] != null &&
                _mapArray[y - 1, x].connectedRooms.Contains(_mapArray[y, x + 1]) &&
                x + 1 == prevX)
            {
                _mapArray[y - 1, x].connectedRooms.Add(_mapArray[y, x]);
                _mapArray[prevY, prevX].connectedRooms.Add(_mapArray[y, x + 1]);
            }
            // 위 기준 (↙)
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

    // 방의 타입을 게임 룰에 맞게 결정
    private void DecideRoomType()
    {
        // dfs 알고리즘

        // 불, 엘리트는 최소 6층부터 등장
        // 이벤트, 일반몹만 연달아 등장가능
        // 한 방에서 고를 수 있는 다음 방들은 같은 타입을 가질 수 없음
        for (int x = 0; x < _mapArray.GetLength(1); x++)
        {
            if(_mapArray[1, x] != null)
            {
                DfsMapTraversal(1, _mapArray[1, x]);
            }
        }
    }

    // mapArray 가시화
    private void VisualizeMap()
    {
        // 경로(이미지) 시각화
        for (int y = 1; y < _mapArray.GetLength(0) - 1; y++)
        {
            for (int x = 0; x < _mapArray.GetLength(1); x++)
            {
                if (_mapArray[y, x] != null)
                {
                    // 발자국
                    Room currentRoom = _mapArray[y, x];
                    
                    // 해당 방에 연결된 모든 방 발자국(이미지) 시각화
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

        // 마지막 스테이지에서 보스방 발자국 시각화
        for (int x = 0; x < _mapArray.GetLength(1); x++)
        {
            if (_mapArray[_mapArray.GetLength(0) - 1, x] != null)
            {
                // 발자국
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


        // 스테이지(버튼) 시각화
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

        // 보스 시각화
        Button bossStage = Instantiate(_bossPrefab, _map);
        bossStage.transform.localPosition = new Vector3(0, 1070f, 0);

        bossStage.onClick.AddListener(() => roomManager.OnEnterBossRoom());
    }


    /// <summary>
    /// dfs알고리즘으로 맵을 돌면서 해당 방의 종류를 갱신합니다.
    /// </summary>
    /// <param name="height">방의 높이(층)</param>
    /// <param name="room">해당 방</param>
    /// <param name="possibleRoomType">현재 함수에서 가능한 방 종류</param>
    private Room DfsMapTraversal(int height, Room room, List<int> possibleRoomType = null)
    {
        // 이미 생성된 방이라면 return;
        if (room.IsGenerate)
            return room;

        // 다음 함수에 possibleRoomType로 넘겨줄 리스트
        // 4는 보물방임. 따라서 리스트에 넣지 않음
        List<int> originPossibleRoomTypes = new List<int>() { 0, 1, 2, 3, 5 };

        if(possibleRoomType?.Count <= 0)
        {
            Debug.Log("그곳에 들어갈 수 있는 방이 없습니다.");
        }

        // 1층 일반 적
        if (height == 1)
        {
            room.SetStageType(_stageData[(int)ERoomType.Enemy], ERoomType.Enemy);
        }
        // 9층 보물 방
        else if (height == 9)
        {
            room.SetStageType(_stageData[(int)ERoomType.Treasure], ERoomType.Treasure);
        }
        // 15층 휴식 방
        else if (height == 15)
        {
            room.SetStageType(_stageData[(int)ERoomType.Rest], ERoomType.Rest);
            originPossibleRoomTypes.Remove((int)ERoomType.Rest);
        }
        else
        {
            // 15층은 무조건 휴식 방이므로 14층에는 휴식방을 제외시킨다.
            if (height == 14)
            {
                possibleRoomType.Remove((int)ERoomType.Rest);
            }


            // 만약 정하려는 방과 이어진 방중에 이미 정해진 방이 있다면 그 방의 타입도 제외(단 일반몹, 미지는 연달아 가능하기 때문에 빼지 않음)
            for (int i = 0; i < room.connectedRooms.Count; i++)
            {
                Room nextRoom = room.connectedRooms[i];
                if (nextRoom.IsGenerate && nextRoom.RoomType != ERoomType.Enemy && nextRoom.RoomType != ERoomType.Unknown)
                {
                    possibleRoomType.Remove((int)nextRoom.RoomType);
                }
            }

            // 각 방의 확률을 계산하여 나온 방 인덱스
            int randomRoomType = SelectRoomWeightRandom(possibleRoomType);

            room.SetStageType(_stageData[randomRoomType], (ERoomType)randomRoomType);

            // 가능한 방 갱신 (방은 연달아 나오면 안됨. 단 일반적과 랜덤방 제외)
            if (randomRoomType != (int)ERoomType.Enemy && randomRoomType != (int)ERoomType.Unknown)
                originPossibleRoomTypes.Remove(randomRoomType);
        }

        

        for(int i = 0; i < room.connectedRooms.Count; i++)
        {
            Room nextRoom = room.connectedRooms[i];

            // 6층보다 밑에 있으면 엘리트, 휴식 방 안 나옴
            if(height < 6)
            {
                originPossibleRoomTypes.Remove((int)ERoomType.Elite);
                originPossibleRoomTypes.Remove((int)ERoomType.Rest);
            }

            // 재귀함수의 반환값으로 해당 방과 연결된 방(세팅된 방)을 가져옴.
            // 반환된 방이 null이 아니라면 originPossibleRoomTypes에서 제외.
            // 한 방에서 고를 수 있는 다음 방들은 같은 타입을 가질 수 없음.
            Room returnedNextRoom = DfsMapTraversal(height + 1, nextRoom, originPossibleRoomTypes);
            if(returnedNextRoom != null)
            {
                originPossibleRoomTypes.Remove((int)returnedNextRoom.RoomType);
            }
        }

        return room;
    }

    /// <summary>
    /// 가중치 랜덤 뽑기
    /// </summary>
    /// <param name="possibleRoomType">이 리스트 중에서 가중치 랜덤을 뽑음</param>
    /// <returns>가중치 랜덤으로 인해 나온 값</returns>
    private int SelectRoomWeightRandom(List<int> possibleRoomType)
    {

        int allPercentage = 0;   // possibleRoomType의 모든 확률 더한 값
        int currentPercentageSum = 0; // 차례대로 확률값을 누적하여 더한 값
        int selectedRoomIndex = 0; // 반환할 값

        foreach (int roomIndex in possibleRoomType)
        {
            allPercentage += _stageData[roomIndex].percentage;
        }

        int percentage = Random.Range(0, allPercentage + 1);  // 랜덤으로 뽑은 확률 값

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

