using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillProxy : IProxy
{
    public WeaponType weaponType;
    public Dictionary<WeaponType, int> weaponLevel = new Dictionary<WeaponType, int>();
    public SelectSkills[] selectSkills;
    public MaxWeaponSetting maxWeaponSetting;
    [Regist]
    private DaimondProxy daimondProxy;
    [Regist]
    private TimerProxy timerProxy;
    [Regist]
    private EnemyProxy enemyProxy;
    public void GiveSkills()
    {
        List<int> randomIndex = new List<int>();
        int selectCount = weaponLevel.Count >= 3 ? 3 : weaponLevel.Count;
        selectSkills = new SelectSkills[selectCount];
        for (int i = 0; i < selectCount; i++)
        {
            bool hasSelect = false;
            while (!hasSelect)
            {
                int index = Random.Range(0, weaponLevel.Count);
                if (!randomIndex.Contains(index) && weaponLevel.ElementAt(index).Value < 5)
                {
                    randomIndex.Add(index);
                    hasSelect = true;
                }
                if (!randomIndex.Contains(index) && weaponLevel.ElementAt(index).Value == 5)
                {
                    WeaponType passiveSkill = GetWeapon(weaponLevel.ElementAt(index).Key);
                    if (passiveSkill == WeaponType.None)
                        continue;
                    if (weaponLevel[passiveSkill] >= 1)
                    {
                        randomIndex.Add(index);
                        hasSelect = true;
                    }


                }
            }
        }
        for (int i = 0; i < randomIndex.Count; i++)
        {

            selectSkills[i] = new SelectSkills();
            selectSkills[i].weaponType = weaponLevel.ElementAt(randomIndex[i]).Key;
            selectSkills[i].level = weaponLevel.ElementAt(randomIndex[i]).Value + 1;
        }
        StopAllAction();
    }
    public void StopAllAction()
    {
        Broadcast(SkillEvent.ON_RANDOM_SKILL_COMPLETE);
        foreach (var daimond in daimondProxy.currentDaimond)
        {
            daimond.moveSpeed = 0;
        }
        enemyProxy.EnemyMove(false);
    }
    public WeaponType GetWeapon(WeaponType type)
    {
        for (int i = 0; i < maxWeaponSetting.combineWeaponSettings.Count; i++)
        {
            if (maxWeaponSetting.combineWeaponSettings[i].activeWeapon == type)
                return maxWeaponSetting.combineWeaponSettings[i].passiveWeapon;
        }
        return WeaponType.None;
    }
    public void SetWeaponLevel(WeaponType Type)
    {
        weaponLevel[Type]++;
        weaponType = Type;
        if (weaponLevel[Type] >= 6)
            weaponLevel.Remove(Type);
        StartAllAction();
    }
    public void StartAllAction()
    {
        Broadcast(SkillEvent.ON_SKILL_LEVELUP);
        foreach (var daimond in daimondProxy.currentDaimond)
        {
            daimond.moveSpeed = daimond.originSpeed;
        }
        enemyProxy.EnemyMove(true);
    }
    public void Reset()
    {
        weaponLevel.Clear();
    }
}

public class SelectSkills
{
    public WeaponType weaponType;
    public int level;
}

