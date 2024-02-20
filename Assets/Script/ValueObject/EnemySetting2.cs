using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetting2 : ScriptableObject
{
    public List<EnemySpawmSetting2> enemySpawmSettings = new List<EnemySpawmSetting2>();
}

[System.Serializable]
public class EnemySpawmSetting2
{
    public float timer;
    public Vector2 randomSpawntime;
    public List<SpawnSetting> spawnSettings = new List<SpawnSetting>();

}

[System.Serializable]
public class SpawnSetting
{
    public EnemyType enemyType;
    public int spawnCount;

}