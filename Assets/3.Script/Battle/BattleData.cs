using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class BattleData : ScriptableObject
{
    [SerializeField]
    private List<Enemy> enemies;
    [SerializeField]
    private List<Vector3> spawnPos;

    public List<Enemy> Enemies => enemies;
    public List<Vector3> SpawnPos => spawnPos;
}
