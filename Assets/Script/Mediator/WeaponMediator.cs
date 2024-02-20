using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMediator : IMediator
{
   
    [Regist]
    private PlayerProxy playerProxy;
    [Regist]
    private EnemyProxy enemyProxy;
    [Regist]
    private SkillProxy skillProxy;
    private WeaponView weaponView;
    public override void Register(IView view)
    {
        weaponView = (WeaponView)view;
    }
    public Transform GetPlayerTransform()
    {
        Transform transform = playerProxy.playerTransform;
        return transform;
    }
    public void SetPlayerMoveSpeed(float value)
    {
        playerProxy.SetPlayerSpeed(value);
    }
    public Enemy GetNearEnemy()
    {
        return enemyProxy.GetNearEnemy();
    }
    public void SetDictionary(Dictionary<WeaponType, int> weaponSpawnerDic)
    {
        skillProxy.weaponLevel = weaponSpawnerDic;
    }
   [Listener(SkillEvent.ON_SKILL_LEVELUP)]
   private void WeaponLevelUp()
    {
        weaponView.WeaponLevelUp(skillProxy.weaponType);
    }
    [Listener(SkillEvent.ON_RANDOM_SKILL_COMPLETE)]
    private void StopTween()
    {
        weaponView.ControlTween(true);
    }
    [Listener(SkillEvent.ON_SKILL_LEVELUP)]
    private void StartTween()
    {
        weaponView.ControlTween(false);
    }
    


}
