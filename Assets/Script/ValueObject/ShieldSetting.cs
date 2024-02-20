using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSetting :ScriptableObject
{
    public List<ShieldLevelSetting> shieldLevelSettings = new List<ShieldLevelSetting>();
}
[System.Serializable]
public class ShieldLevelSetting
{
    public int level;
    public int count;
    public float speed;
    public int damage;
    public float time;

}
