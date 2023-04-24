using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomData", menuName = "RoomData/BattleData")]
public class BattleData : ScriptableObject
{
    [SerializeField]
    private List<Enemy> enemies;
    [SerializeField]
    private List<Vector3> spawnPos;

    public int maxMoney;
    public int minMoney;
    public bool isRelics;

    public List<Enemy> Enemies => enemies;
    public List<Vector3> SpawnPos => spawnPos;
}
