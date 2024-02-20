using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazerSetting : ScriptableObject
{
    public List<LazorLevelSetting> lazorLevelSettings = new List<LazorLevelSetting>();
    
}
[System.Serializable]
public class LazorLevelSetting
{
    public int level;
    public int damage;
    public string animatoinName;

        

}
