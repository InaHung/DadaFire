using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPSetting : ScriptableObject
{
    public List<SingleEXPSetting> singleEXPSettings = new List<SingleEXPSetting>();

    [ContextMenu("SetDefaultLevels")]
    public void SetDefaultLevels()
    {
        singleEXPSettings = new List<SingleEXPSetting>();
        for (int i = 1; i < 41; i++)
        {
            singleEXPSettings.Add(new SingleEXPSetting());
            singleEXPSettings[i - 1].level = i;
            singleEXPSettings[i - 1].maxScore = 20 + 5 * (i - 1);
        }
    }

}

[System.Serializable]
public class SingleEXPSetting
{
    public int level;
    public int maxScore;
}
