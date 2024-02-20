using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSetting : ScriptableObject
{
    public List<BossSpawnSetting> bossSpawnSettings=new List<BossSpawnSetting>();

   
}
[System.Serializable]
public class BossSpawnSetting
{
    public EnemyType enemyType;
    public float countDownTime;
    
}
