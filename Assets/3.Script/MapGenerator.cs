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
        // 6번 반복
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
        VisualizeRoute(xPosList);
    }

    private void VisualizeRoute(List<int> xPosList)
    {
        for(int i = 0; i < xPosList.Count; i++)
        {
            int x = xPosList[i];
            int y = i + 1;

            // 발자국 UI(Image) 시각화
            if(i != 0)
            {
                int prevX = xPosList[i - 1];
                int prevY = i;

                // 현재와 이전의 스테이지가 이미 있던 경우라면 발자국 없음(중복 제거)
/*                if (mapArray[y, x].IsGenerate && mapArray[prevY, prevX].IsGenerate)
                {
                    continue;
                }
                else*/
                {
                    // 발자국
                    float currentPosX = mapArray[prevY, prevX].PosX;
                    float currentPosY = mapArray[prevY, prevX].PosY;

                    Vector3 dir = new Vector3(mapArray[y, x].PosX - mapArray[prevY, prevX].PosX,
                        mapArray[y, x].PosY - mapArray[prevY, prevX].PosY,
                        0).normalized;

                    float theta = Mathf.Atan(dir.x / dir.y) * Mathf.Rad2Deg;

                    if (x != prevX)
                        theta += 90;

                    while(true)
                    {
                        if(currentPosY > mapArray[y, x].PosY)
                        {
                            break;
                        }

                        Vector3 pos = new Vector3(currentPosX, currentPosY, 0) + dir * stepDistance;
                        Transform step = Instantiate(stepImage, map);
                        step.transform.localPosition = pos;

                        step.transform.localEulerAngles = new Vector3(0, 0, theta);

                        currentPosX = pos.x;
                        currentPosY = pos.y;
                    }
                }
            }

            // 현재 좌표의 스테이지가 생성되지 않았다면 UI 생성(시각화)
            if(!mapArray[y, x].IsGenerate)
            {
                mapArray[y, x].IsGenerate = true;

                Vector3 pos = new Vector3(xx / (mapArray.GetLength(1) - 1) * x - xx / 2, yy / (mapArray.GetLength(0) - 1) * y - yy / 2 - 300, 0);
                Button stage = Instantiate(stageButton, map);
                stage.transform.localPosition = pos;
            }
        }
    }

    private void VisualizeMap()
    {
/*        for(int y = 0; y < mapArray.GetLength(0); y++)
        {
            for(int x = 0; x < mapArray.GetLength(1); x++)
            {
                if (mapArray[y, x] == 1)
                {
                    Vector3 pos = new Vector3(xx / (mapArray.GetLength(1) - 1) * x - xx / 2, yy / (mapArray.GetLength(0) - 1) * y - yy / 2 - 300, 0);
                    Button stage = Instantiate(stageButton, map);
                    stage.transform.localPosition = pos;
                }
            }
        }*/

        Button bossStage = Instantiate(bossButton, map);
        bossStage.transform.localPosition = new Vector3(0, 1070f, 0);
    }
}
