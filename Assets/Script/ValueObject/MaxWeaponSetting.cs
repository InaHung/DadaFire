using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxWeaponSetting : ScriptableObject
{
    public List<CombineWeaponSetting> combineWeaponSettings = new List<CombineWeaponSetting>();
}
[System.Serializable]
public class CombineWeaponSetting
{
    public WeaponType activeWeapon;
    public WeaponType passiveWeapon;

}
