using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PassiveSkill : MonoBehaviour
{
    public int curLevel;
    public WeaponType weaponType;
    public Action<float, WeaponType> onSkillDeliverNumber;
    public PassiveSkillValueSetting valueSetting;
    public virtual void SkillLevelUP()
    {
        curLevel++;
        onSkillDeliverNumber(valueSetting.PassiveValueLevelSettings[curLevel - 1].percentage, weaponType);
    }
}
