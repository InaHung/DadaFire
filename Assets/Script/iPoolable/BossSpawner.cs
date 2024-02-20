using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class BossSpawner : MonoBehaviour
{
    public BossSetting bossSetting;
    public Dictionary<EnemyType, ObjectPool> bossPool = new Dictionary<EnemyType, ObjectPool>();
    private Transform playerTransform;
    public Action<Enemy> onCreateBoss;
    public Action<Enemy> onBossDead;
    public Action<Vector3, float> onInjured;
    private Tween BossTween;
    private int i = 0;
    private Boss curBoss;

    public GameObject bossBorder;

    public void CreateBoss()
    {
        if (i >= bossSetting.bossSpawnSettings.Count)
            return;

        BossTween = DOVirtual.DelayedCall(bossSetting.bossSpawnSettings[i].countDownTime, () =>
        {
            Boss boss = bossPool[bossSetting.bossSpawnSettings[i].enemyType].GetObject() as Boss;
            curBoss = boss;
            boss.transform.position = playerTransform.position + new Vector3(0, 4f, 0);
            boss.transform.SetParent(transform);
            boss.SetPlayerTransform(playerTransform);
            boss.StartAction();
            onCreateBoss(boss);
            GameObject border = Instantiate(bossBorder, playerTransform.position, Quaternion.identity);
            boss.SetBorder(border);
            boss.onDisposeEnemy += (boss2) =>
            {
                onBossDead(boss2);
                Destroy(border);
                curBoss = null;
            };
            boss.onInjured = onInjured;
            i++;
        });


    }
    public void SetMyPool(Dictionary<EnemyType, ObjectPool> objPool)
    {
        bossPool = objPool;
    }
    public void SetPlayerTransform(Transform transform)
    {
        playerTransform = transform;
        CreateBoss();
    }
    public void TweenControl(bool pause)
    {
        if (pause)
        {
            BossTween.Pause();
            curBoss?.Pause(pause);
        }
        else
        {
            BossTween?.Play();
            curBoss?.Pause(pause);
        }
    }
    private void OnDestroy()
    {
        BossTween.Kill();
    }
}
