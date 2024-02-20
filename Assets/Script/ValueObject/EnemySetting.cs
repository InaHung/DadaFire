using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetting : ScriptableObject
{
    public List<EnemySpawmSetting> enemySpawmSettings = new List<EnemySpawmSetting>();

    public EnemySpawmSetting GetSettingByTime(float currentTime)
    {

        EnemySpawmSetting enemySpawmSetting = null;
        for (int i = 0; i < enemySpawmSettings.Count; i++)
        {
            if (i == enemySpawmSettings.Count - 1)
                return enemySpawmSettings[i];

            if (currentTime <= enemySpawmSettings[i + 1].timer)
            {
                enemySpawmSetting = enemySpawmSettings[i];
                break;
            }
        }
        return enemySpawmSetting;
    }
}
[System.Serializable]
public class EnemySpawmSetting
{
    public EnemyType enemyType;
    public float timer;
    public Vector2 randomSpawntime;
    public int spawnCount;
  
}

