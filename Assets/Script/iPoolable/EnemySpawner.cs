using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class EnemySpawner : MonoBehaviour
{
    private Transform playerTransform;
    public Action<Enemy> OnCreateEnemy;
    public Vector3 spawnerPosition;
    public float randomX;
    public float randomY;
    public Action<Enemy> onEnemyDead;
    public EnemySetting enemySetting;
    private int lastEnemyCount;
    private bool isWaitingToSpawn;
    private Tween countingTween;
    public Dictionary<EnemyType, ObjectPool> enemyPool = new Dictionary<EnemyType, ObjectPool>();
    public Func<float> GetCurTime;
    public Action<Vector3,float> onInjured;
    public void SetLastEnemyCount(int count)
    {
        lastEnemyCount = count;
        if (isWaitingToSpawn)
            EnemySpawn();
    }

    public void SetMyPool(Dictionary<EnemyType, ObjectPool> objectPool)
    {
        enemyPool = objectPool;
    }

    public void GetPlayerTransform(Transform transform)
    {

        playerTransform = transform;
        EnemySpawn();
    }

    public void EnemySpawn()
    {
        float currentTime = GetCurTime();
        EnemySpawmSetting currentSetting = enemySetting.GetSettingByTime(currentTime);
        if (lastEnemyCount < currentSetting.spawnCount)
        {
            isWaitingToSpawn = true;
            return;
        }
        isWaitingToSpawn = false;
        float randomTime = UnityEngine.Random.Range(currentSetting.randomSpawntime.x, currentSetting.randomSpawntime.y);

        if (lastEnemyCount >= currentSetting.spawnCount)
        {
            for (int i = 0; i < currentSetting.spawnCount; i++)
            {
                Enemy enemy = enemyPool[currentSetting.enemyType].GetObject() as Enemy;
                enemy.isDead = false;
                float x = UnityEngine.Random.Range(-randomX, randomX);
                float y = UnityEngine.Random.Range(-randomY, randomY);
                enemy.gameObject.transform.position = playerTransform.position + spawnerPosition + new Vector3(x, y, 0);
                enemy.transform.SetParent(transform);
                enemy.SetPlayerTransform(playerTransform);
                OnCreateEnemy(enemy);
                enemy.onDisposeEnemy += (enemy2) =>
                {
                    onEnemyDead(enemy2);
                };
                enemy.onInjured = onInjured;

            }
        }

        countingTween = DOVirtual.DelayedCall(randomTime, () =>
       {

           EnemySpawn();
       });
    }

    public void KillTween()
    {
        if (countingTween != null)
            countingTween.Kill();
        isWaitingToSpawn = false;
    }
    public void PauseEnemySpawn(bool pause)
    {
        if (pause)
        {
            countingTween?.Pause();
        }

        else
            countingTween.Play();
    }
    private void OnDestroy()
    {
        countingTween.Kill();
    }



}
