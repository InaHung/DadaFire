using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMediator : IMediator
{
    [Regist]
    private PlayerProxy playerProxy;
    [Regist]
    private EnemyProxy enemyProxy;
    [Regist]
    private TimerProxy timerProxy;
    private EnemyView enemyView;

    public override void Register(IView view)
    {
        enemyView = (EnemyView)view;
    }
    public Transform GetPlayerTransform()
    {
        return playerProxy.playerTransform;
    }
    public void SetEnemy(Enemy enemy)
    {
        enemyProxy.SetEnemy(enemy);
    }
    public void SetDeadEnemy(Enemy enemy)
    {
        enemyProxy.EnemyDead(enemy);
    }
    public void SetBoss(Enemy boss)
    {
        enemyProxy.SetBoss(boss);
    }
    public void SetDeadBoss(Enemy boss)
    {
        enemyProxy.BossDead(boss);
    }
    public int GetCurrentEnemyCount()
    {
        return enemyProxy.currentEnemies.Count;
    }
    [Listener(BossEvent.ON_BOSS_DEAD)]
    private void FinalBossCountDowm()
    {
        enemyView.FinalBossCountDowm();
    }

    [Listener(BossEvent.ON_BOSS_APPEAR)]
    private void OnBossAppear()
    {
        enemyView.KillEnemySpawn();
    }
    public float GetCurTime()
    {
        return timerProxy.curTime;
    }
    public void ShowEnemyDamamge(Vector3 textPosition,float damage)
    {
        enemyProxy.ShowEnemyDamage(textPosition,damage);
    }
    [Listener(BossEvent.ON_BOSS_APPEAR)]
    [Listener(SkillEvent.ON_RANDOM_SKILL_COMPLETE)]
    private void StopCounting()
    {
        enemyView.EnemyStopCounting();
    }
    [Listener(BossEvent.ON_BOSS_DEAD)]
    [Listener(SkillEvent.ON_SKILL_LEVELUP)]
    private void StartCounting()
    {
        enemyView.EnemyStartCounting();
    }
    [Listener(SkillEvent.ON_RANDOM_SKILL_COMPLETE)]
    private void BossStop()
    {
        enemyView.BossStop();
    }
    [Listener(SkillEvent.ON_SKILL_LEVELUP)]
    private void BossStart()
    {
        enemyView.BossStart();
    }
    public void OnFinalBossDead()
    {
        Broadcast(BossEvent.ON_FINAL_BOSS_DEAD);
    }
    public void ResetProxy()
    {
        enemyProxy.Reset();
    }

}
