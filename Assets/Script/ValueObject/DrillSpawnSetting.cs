using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillSpawnSetting : ScriptableObject
{
    public List<DrillLevelSetting> drillLevelSettings = new List<DrillLevelSetting>();
}
[System.Serializable]
public class DrillLevelSetting
{
    public int level;
    public float moveSpeed;
    public int spawnCount;
    public int damage;
  
}