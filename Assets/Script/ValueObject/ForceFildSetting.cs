using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFildSetting :ScriptableObject
{
    public List<fieldLevelSetting> fieldLevelSettings = new List<fieldLevelSetting>();
}
[System.Serializable]
public class fieldLevelSetting
{
    public int level;
    public int damage;
    public float areaScale;



}