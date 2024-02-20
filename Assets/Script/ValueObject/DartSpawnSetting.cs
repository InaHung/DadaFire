using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartSpawnSetting : ScriptableObject
{
    public List<DartSetting> dartSettings = new List<DartSetting>();
   
}
[System.Serializable]
public class DartSetting
{
    public int level;
    public int damage;
    public int dartCount;
    public float spawnTime;
}

