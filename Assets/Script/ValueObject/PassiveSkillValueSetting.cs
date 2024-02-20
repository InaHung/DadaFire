using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillValueSetting :ScriptableObject
{
    public List<PassiveValueLevelSetting> PassiveValueLevelSettings = new List<PassiveValueLevelSetting>();

}
[System.Serializable]
public class PassiveValueLevelSetting
{
    public int level;
    public float percentage;
}
