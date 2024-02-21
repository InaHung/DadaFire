using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyView : MonoBehaviour, IView
{
    public List<EnemySpawner> enemySpawners = new List<EnemySpawner>();
    public BossSpawner bossSpawner;
    public List<Enemy> enemyList = new List<Enemy>();
    public Dictionary<EnemyType, ObjectPool> enemyPool = new Dictionary<EnemyType, ObjectPool>();
    public int MaxEnemyCount;
    int currentCount;
    [Regist]
    private EnemyMediator mediator;
    Transform playerTransform;

    private void Awake()
    {

        Communicator.Connect(this);
        mediator.Register(this);
        foreach (var enemyspawner in enemySpawners)
        {
            enemyspawner.OnCreateEnemy += OnEnemyCreate;
            enemyspawner.onEnemyDead += OnEnemyDead;
            enemyspawner.GetCurTime = GetCurTime;
            enemyspawner.onInjured = OnEnemyDamage;
        }
        bossSpawner.onCreateBoss += OnBossCreate;
        bossSpawner.onBossDead += OnBossDead;
        bossSpawner.onInjured = OnEnemyDamage;
        CreateEnemyPool();
        mediator.ResetProxy();
    }

    private void CreateEnemyPool()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            UnityEngine.GameObject poolObject = new UnityEngine.GameObject();
            poolObject.transform.SetParent(transform);
            poolObject.name = enemyList[i].name + "Pool";
            ObjectPool pool = poolObject.AddComponent<ObjectPool>();
            pool.SetPrefab(enemyList[i]);
            pool.InitPool();
            enemyPool.Add(enemyList[i].enemyType, pool);
        }

        foreach (var spawner in enemySpawners)
        {
            spawner.SetMyPool(enemyPool);
        }
        bossSpawner.SetMyPool(enemyPool);
    }



    private void Start()
    {
        playerTransform = mediator.GetPlayerTransform();
        foreach (var enemyspawner in enemySpawners)
        {
            enemyspawner.GetPlayerTransform(playerTransform);
            enemyspawner.SetLastEnemyCount(MaxEnemyCount);
        }
        bossSpawner.SetPlayerTransform(playerTransform);

    }


    public void OnEnemyCreate(Enemy enemy)
    {
        mediator.SetEnemy(enemy);
        CheckEnemyCount();
    }
    public void OnEnemyDead(Enemy enemy)
    {
        mediator.SetDeadEnemy(enemy);
        CheckEnemyCount();
    }
    public void OnBossCreate(Enemy boss)
    {
        mediator.SetBoss(boss);
    }
    public void OnBossDead(Enemy boss)
    {
        mediator.SetDeadBoss(boss);
        if(boss.enemyType==EnemyType.finalBoss)
        {
            mediator.OnFinalBossDead();
        }
    }
    public void OnEnemyDamage(Vector3 textPosition,float damage)
    {
        mediator.ShowEnemyDamamge(textPosition,damage);
    }
    public void CheckEnemyCount()
    {
        
        
        foreach (var enemySpawn in enemySpawners)
        {
            currentCount = mediator.GetCurrentEnemyCount();
            int canSpawnCount = MaxEnemyCount - currentCount;
            enemySpawn.SetLastEnemyCount(canSpawnCount);
        }
    }
    public void FinalBossCountDowm()
    {
    
        bossSpawner.CreateBoss();
        CheckEnemyCount();
        foreach (var enemySpawner in enemySpawners)
        {
            enemySpawner.EnemySpawn();
        }
    }

    public void KillEnemySpawn()
    {
        foreach (var enemySpawner in enemySpawners)
        {
            enemySpawner.KillTween();
        }

    }
    public float GetCurTime()
    {
        return mediator.GetCurTime();
    }
    public void EnemyStopCounting()
    {
       
        foreach (var enemySpawner in enemySpawners)
        {
            enemySpawner.PauseEnemySpawn(true);
        }
    }
    public void BossStop()
    {
        bossSpawner.TweenControl(true);
    }
    public void EnemyStartCounting()
    {
        foreach (var enemySpawner in enemySpawners)
        {
            enemySpawner.PauseEnemySpawn(false);
        }
    }
    public void BossStart()
    {
        bossSpawner.TweenControl(false);
    }
    
}

