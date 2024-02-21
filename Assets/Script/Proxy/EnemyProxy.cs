using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProxy : IProxy
{
    public List<Enemy> currentEnemies = new List<Enemy>();
    public Vector3 enemyPosition;
    public Vector3 damagePosition;
    public float damageText;
    [Regist]
    private DaimondProxy daimondProxy;
    [Regist]
    private PlayerProxy playerProxy;
    [Regist]
    private TimerProxy timerProxy;
    public void SetEnemy(Enemy enemy)
    { 
        if (!currentEnemies.Contains(enemy))
            currentEnemies.Add(enemy);
    }

    public void EnemyDead(Enemy enemy)
    {
        enemyPosition = enemy.transform.position;
        if (currentEnemies.Contains(enemy))
        {
            currentEnemies.Remove(enemy);
        }
        daimondProxy.SetEnemyInfo(enemyPosition, enemy.daimondType);
    }
    public void SetBoss(Enemy curBoss)
    {
        foreach (var enemy in currentEnemies)
        {
            enemy.Dispose();
        }
        currentEnemies.Clear();
        if(!currentEnemies.Contains(curBoss))
        {
            currentEnemies.Add(curBoss);
        }
        
        Broadcast(BossEvent.ON_BOSS_APPEAR);
    }
    public void BossDead(Enemy boss)
    {
        if (currentEnemies.Contains(boss))
        {
            currentEnemies.Remove(boss);
        }
       
        Broadcast(BossEvent.ON_BOSS_DEAD);
    }
    Vector2 nearDistance;
    public Enemy GetNearEnemy()
    {
        Enemy nearEnemy = null;
        Vector2 PlayerPosition=playerProxy.playerTransform.position;
        float lastNearDistance=100000000000;
        for (int i= 0;i<currentEnemies.Count;i++)
        {
            
            float nearDistance = Vector2.Distance(currentEnemies[i].transform.position,PlayerPosition);
            
            if(nearDistance<lastNearDistance)
            {
                lastNearDistance = nearDistance;
                nearEnemy = currentEnemies[i];
            }
        }
        return nearEnemy;
    }
    public void EnemyMove(bool canMove)
    {
        
        foreach (Enemy enemy in currentEnemies)
        {
            if (!canMove)
                enemy.moveSpeed = 0;
            else
                enemy.moveSpeed = enemy.originSpeed;

        }
    }
    public void ShowEnemyDamage(Vector3 textPosition, float damage)
    {
        damagePosition = textPosition;
        damageText= damage;
        Broadcast(EnemyTextEvent.ON_SHOW_ENEMY_DAMAGE);
    }
    public void Reset()
    {
        currentEnemies.Clear();
    }


}
