using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WeaponView : MonoBehaviour, IView
{

    public List<PassiveSkill> passiveSkills = new List<PassiveSkill>();
    public List<WeaponSpawner> weaponSpawners = new List<WeaponSpawner>();
    public Dictionary<WeaponType, WeaponSpawner> weaponSpawnerDic = new Dictionary<WeaponType, WeaponSpawner>();
    public Dictionary<WeaponType, PassiveSkill> passiveSkillDic = new Dictionary<WeaponType, PassiveSkill>();
    [Regist]
    private WeaponMediator mediator;
    private void Awake()
    {
        Communicator.Connect(this);
        mediator.Register(this);
        foreach (var spawner in weaponSpawners)
        {
            spawner.getNearEnemy = GetNearEnemy;
        }
        foreach (var skill in passiveSkills)
        {
            skill.onSkillDeliverNumber = SetPassiveSkill;
        }

    }
    private void Start()
    {
        SetPlayerTransform();
        SetWeaponSkill();
    }
    public void SetWeaponSkill()
    {
        Dictionary<WeaponType, int> weaponLevel = new Dictionary<WeaponType, int>();
        for (int i = 0; i < weaponSpawners.Count; i++)
        {
            weaponLevel.Add(weaponSpawners[i].weaponType, 0);
            weaponSpawnerDic.Add(weaponSpawners[i].weaponType, weaponSpawners[i]);
        }
        for (int i = 0; i < passiveSkills.Count; i++)
        {
            weaponLevel.Add(passiveSkills[i].weaponType, 0);
            passiveSkillDic.Add(passiveSkills[i].weaponType, passiveSkills[i]);
        }
        mediator.SetDictionary(weaponLevel);

    }
    public void SetPlayerTransform()
    {
        foreach (var WeaponSpawner in weaponSpawners)
        {
            WeaponSpawner.SetPlayerTransform(mediator.GetPlayerTransform());
        }

    }
    public Enemy GetNearEnemy()
    {
        return mediator.GetNearEnemy();

    }
    public void WeaponLevelUp(WeaponType weaponType)
    {
        if (weaponSpawnerDic.ContainsKey(weaponType))
        {
            weaponSpawnerDic[weaponType].SetCurrentLevel();
            if (!weaponSpawnerDic[weaponType].gameObject.activeInHierarchy)
            {
                weaponSpawnerDic[weaponType].gameObject.SetActive(true);
            }
        }
        else if (passiveSkillDic.ContainsKey(weaponType))
        {
            passiveSkillDic[weaponType].SkillLevelUP();
            if (!passiveSkillDic[weaponType].gameObject.activeInHierarchy)
            {
                passiveSkillDic[weaponType].gameObject.SetActive(true);
            }
        }

    }
    public void SetPassiveSkill(float value, WeaponType weaponType)
    {

        switch (weaponType)
        {
            case WeaponType.MoveSpeed:
                mediator.SetPlayerMoveSpeed(value);
                break;
            case WeaponType.HighPower:
                foreach (var spawner in weaponSpawners)
                {
                    spawner.IncreaseDamage(value);
                }
                break;
            case WeaponType.reduceCD:
                foreach (var spawner in weaponSpawners)
                {
                    spawner.CoolDown(value);
                }
                break;
            case WeaponType.bulletSpeed:
                DrillSpawner drillSpawner = weaponSpawnerDic[WeaponType.drill] as DrillSpawner;
                drillSpawner.IncreaseMoveSpeed(value);
                break;
            case WeaponType.expand:
                foreach(var spawner in weaponSpawners)
                {
                    spawner.Expand(value);
                }
                break;
        }
    }
    public void ControlTween(bool pause)
    {
        foreach (var spawner in weaponSpawners)
        {
            spawner.TweenControl(pause);
        }
    }


}
