using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicGenerator : MonoBehaviour, IRegisterable
{
    [SerializeField]
    private List<RelicData> relicDataList;
    [SerializeField]
    private RelicIcon relicPrefab;
    [SerializeField]
    private Transform relicParent;

    private List<RelicData> relicRandomDataList;

    public void Init()
    {
        relicRandomDataList = new List<RelicData>();

        relicDataList.ForEach(relic => relicRandomDataList.Add(relic));
        relicRandomDataList.ShuffleList();
    }

    public RelicIcon GenerateRelic(ERelic relic)
    {
        RelicIcon relicObj = Instantiate(relicPrefab, relicParent);
        RelicData relicData = GetRelicData(relic);
        relicRandomDataList.Remove(relicData);
        relicObj.Init(relicData);
        return relicObj;
    }

    public RelicData GenerateRandomRelicData()
    {
        RelicData relicData = relicRandomDataList[0];
        relicRandomDataList.RemoveAt(0);
        return relicData;
    }

    private RelicData GetRelicData(ERelic relic)
    {
        for(int i = 0; i < relicDataList.Count; i++)
        {
            if (relic == relicDataList[i].relic)
                return relicDataList[i];
        }
        Debug.LogWarning("그런 유물은 없습니다.");
        return null;
    }
}
