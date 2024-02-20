using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Enemy : IPoolable
{
    public EnemyType enemyType;
    public DaimondType daimondType;
    public float moveSpeed;
    protected Transform playerTransform;
    PlayerView playerView;
    public Action<Enemy> onDisposeEnemy;
    public Action<Vector3, float> onInjured;
    public int damage;
    protected int currentHP;
    public int maxHP;
    public float originSpeed;
    public bool isDead;
    public bool isBoss;
    private bool pauseAction;
    protected virtual void Awake()
    {
        currentHP = maxHP;
        originSpeed = moveSpeed;
        isDead = false;

    }
    private void Update()
    {

        EnemyMove();


        if (playerView != null&&pauseAction==false)
        {
            playerView.PlayerDamage(damage);
        }

    }
    public virtual void Injured(int damage)
    {
        currentHP -= damage;
        onInjured(transform.position, damage);
        EnemyDead();
    }

    public void SetPlayerTransform(Transform transform)
    {
        playerTransform = transform;
    }
    protected virtual void EnemyMove()
    {
        if (playerTransform != null)
            transform.position = Vector2.MoveTowards(transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "player")
        {
            playerView = collision.transform.GetComponent<PlayerView>();
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        playerView = null;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "MainCamera" && collision.transform.GetComponent<CameraView>().isBomb && isBoss == false)
        {
            onDisposeEnemy(this);
            onDisposeEnemy = null;
            Dispose();

        }

    }
    protected virtual void EnemyDead()
    {
        if (currentHP <= 0)
        {
            currentHP = maxHP;
            if (onDisposeEnemy != null)
                onDisposeEnemy(this);
            onDisposeEnemy = null;
            Dispose();



        }
    }
    public virtual void Pause(bool pause)
    {
        pauseAction = pause;
       
    }
    public override void Dispose()
    {

        currentHP = maxHP;
        base.Dispose();
        isDead = true;

    }
}
public enum EnemyType
{
    flyEnemy,
    oneEyeEnemy,
    glassEnemy,
    firstBoss,
    finalBoss,
}


