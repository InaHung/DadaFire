using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaimondMediator : IMediator
{
    [Regist]
    private DaimondProxy daimondProxy;
    [Regist]
    private EnemyProxy enemyProxy;
    private DaimondView daimondView;
    public override void Register(IView view)
    {
        daimondView= view as DaimondView;
    }

    public void SetDaimond(Daimond daimond)
    {
        daimondProxy.SetDaimond(daimond);
    }
    [Listener(EnemyEvent.ON_GET_ENEMY_INFO)]
    private void SetEnemyPosition()
    {
        Vector3 enemyPosition = daimondProxy.enemyPosition;
        DaimondType daimondType = daimondProxy.daimondType1;
        daimondView.SetEnemyInfo(enemyPosition,daimondType);
    }
    public void EatDaimond(Daimond daimond)
    {
        daimondProxy.EatDaimond(daimond);

    }
    /*[Listener(SkillEvent.ON_RANDOM_SKILL_COMPLETE)]
   private void StopTween()
    {
        daimondProxy.StopTween(true);
        
    }
    [Listener(SkillEvent.ON_SKILL_LEVELUP)]
    private void StartTween()
    {
        daimondProxy.StopTween(false);
    }*/
}
