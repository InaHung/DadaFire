using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpriteSetting :ScriptableObject
{
    public List<WeaponSprites> weaponSprite = new List<WeaponSprites>();
}
[System.Serializable]
public class WeaponSprites
{
    public WeaponType weaponType;
    public Sprite sprite;
}
