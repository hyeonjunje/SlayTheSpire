using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{
    private Room[,] mapArray = new Room[15, 7];

    [SerializeField]
    private Button stageButton;
    [SerializeField]
    private Button bossButton;
    [SerializeField]
    private Transform stepImage;

    [SerializeField]
    private RectTransform map;

    [SerializeField]
    private float xx = 500;
    [SerializeField]
    private float yy = 400;
    [SerializeField]
    private float stepDistance = 20f;

    private void Awake()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        mapArray = new Room[15, 7];

        for(int i = 0; i < 5; i++)
        {
            GenerateRoute();
        }

        VisualizeMap();
    }

    private void GenerateRoute()
    {
        // 6�� �ݺ�
        int x = Random.Range(0, mapArray.GetLength(1));
        List<int> xPosList = new List<int>();
        for (int y = 1; y < mapArray.GetLength(0); y++)
        {
            xPosList.Add(x);
            if (mapArray[y, x] == null)
            {
                Vector3 pos = new Vector3(xx / (mapArray.GetLength(1) - 1) * x - xx / 2, yy / (mapArray.GetLength(0) - 1) * y - yy / 2 - 300, 0);
                mapArray[y, x] = new Room(pos.x, pos.y);
            }

            if(x == 0)
            {
                x += Random.Range(0, 2);
            }
            else if(x == mapArray.GetLength(1) - 1)
            {
                x += Random.Range(-1, 1);
            }
            else
            {
                x += Random.Range(-1, 2);
            }
        }
        MakeRoutine(xPosList);
        // VisualizeRoute(xPosList);
    }

    private void MakeRoutine(List<int> xPosList)
    {
        for(int i = xPosList.Count - 1; i >= 1; i++)
        {
            int x = xPosList[i];
            int y = i + 1;

            int prevX = xPosList[i - 1];
            int prevY = i;

            mapArray[prevY, prevX].connectedRooms.Add(mapArray[y, x]);
        }
    }

/*    // ���� �����
    private void MakeEdge()
    {
        for(int y = 0; y < mapArray.GetLength(0) - 1; y++)
        {
            // ���� ���ʿ� ���������� ���� ��
            if (mapArray[y, 0] != null)
            {
                // �ٷ� ���� ���������� �ִٸ� ������ ����
                if (mapArray[0, y + 1] != null)
                    mapArray[0, y].connectedRooms.Add(mapArray[0, y + 1]);

                // ������ �밢���� ���������� �ִٸ� 50% Ȯ���� ����
                if (mapArray[1, y + 1] != null && Random.Range(0, 2) == 0)
                    mapArray[0, y].connectedRooms.Add(mapArray[1, y + 1]);
            }

            // ���� �����ʿ� ���� ��
            if (mapArray[y, mapArray.GetLength(1) - 1] != null)
            {
                // �ٷ� ���� ���������� �ִٸ� ������ ����
                if (mapArray[mapArray.GetLength(1) - 1, y + 1] != null)
                    mapArray[mapArray.GetLength(1) - 1, y].connectedRooms.Add(mapArray[mapArray.GetLength(1) - 1, y + 1]);

                // ���� �밢���� ���������� �ִٸ� 50% Ȯ���� ����
                if (mapArray[mapArray.GetLength(1) - 2, y + 1] != null && Random.Range(0, 2) == 0)
                    mapArray[mapArray.GetLength(1) - 1, y].connectedRooms.Add(mapArray[mapArray.GetLength(1) - 2, y - 1]);
            }

            for (int x = 1; x < mapArray.GetLength(1) - 1; x++)
            {
                // �ش� ��ǥ�� stage�� ������ ����
                if (mapArray[y, x] == null)
                    continue;

                // ��, �� �ٷ� �� 
                
                // ���� ũ�ν��Ǹ� �������
            }
        }
    }*/

    private void VisualizeMap()
    {
        // ���(�̹���) �ð�ȭ
        for (int y = 0; y < mapArray.GetLength(0) - 1; y++)
        {
            for (int x = 0; x < mapArray.GetLength(1); x++)
            {
                if (mapArray[y, x] != null)
                {
                    // ���ڱ�
                    Room currentRoom = mapArray[y, x];
                    float currentPosX = currentRoom.PosX;
                    float currentPosY = currentRoom.PosY;

                    // �ش� �濡 ����� ��� �� ���ڱ�(�̹���) �ð�ȭ
                    for (int i = 0; i < currentRoom.connectedRooms.Count; i++)
                    {
                        Room connectedRoom = currentRoom.connectedRooms[i];
                        float nextPosX = connectedRoom.PosX;
                        float nextPosY = connectedRoom.PosY;

                        Vector3 dir = new Vector3(nextPosX - currentPosX, nextPosY - currentPosY, 0).normalized;
                        float theta = -Mathf.Atan(dir.x / dir.y) * Mathf.Rad2Deg;

                        while (true)
                        {
                            if (currentPosY > nextPosY)
                                break;

                            Vector3 pos = new Vector3(currentPosX, currentPosY, 0) + dir * stepDistance;
                            Transform step = Instantiate(stepImage, map);
                            step.transform.localPosition = pos;
                            step.transform.localEulerAngles = new Vector3(0, 0, theta);

                            currentPosX = pos.x;
                            currentPosY = pos.y;
                        }
                    }
                }
            }
        }


        // ��������(��ư) �ð�ȭ
        for (int y = 0; y < mapArray.GetLength(0); y++)
        {
            for(int x = 0; x < mapArray.GetLength(1); x++)
            {
                if(mapArray[y, x] != null)
                {
                    Vector3 pos = new Vector3(xx / (mapArray.GetLength(1) - 1) * x - xx / 2, yy / (mapArray.GetLength(0) - 1) * y - yy / 2 - 300, 0);
                    Button stage = Instantiate(stageButton, map);
                    stage.transform.localPosition = pos;
                }
            }
        }

        Button bossStage = Instantiate(bossButton, map);
        bossStage.transform.localPosition = new Vector3(0, 1070f, 0);
    }
}
