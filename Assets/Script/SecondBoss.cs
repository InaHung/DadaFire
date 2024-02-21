using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class SecondBoss : Boss
{
    public Fireball fireball;
    private Fireball curFireball;
    private Tween attackTween;
    private Tween firstAttackTween;
    public float attackTime;
    public float firstAttackTime;
    public Vector2 range;
   
    public override void StartAction()
    {
        base.StartAction();
        firstAttackTween = DOVirtual.DelayedCall(firstAttackTime, () =>
        {
            Attack();
        });
    }
    public void Attack()
    {
        Fireball ball = Instantiate(fireball, bossBorder.transform);
        curFireball = ball;
        curFireball.onDestroy = (() =>
        {
            curFireball = null;
        });
        float x = UnityEngine.Random.Range(-range.x, range.x);
        float y = UnityEngine.Random.Range(-range.y, range.y);
        ball.transform.localPosition = new Vector2(x, y);
        attackTween = DOVirtual.DelayedCall(attackTime, () =>
        {
            Attack();
        });
    }
    public override void Pause(bool pause)
    {
        base.Pause(pause);
        if(pause)
        {
            firstAttackTween.Pause();
            attackTween.Pause();
            curFireball?.StopAmimator(pause);
        }
        else
        {
            firstAttackTween.Play();
            attackTween.Play();
            curFireball?.StopAmimator(pause);
        }
    }

    protected override void EnemyDead()
    {
       
        if (currentHP <= 0)
        {
            currentHP = maxHP;
            if (onDisposeEnemy != null)
                onDisposeEnemy(this);
            onDisposeEnemy = null;
            Dispose();
            attackTween.Kill();
            
        }

    }
    private void OnDestroy()
    {
        firstAttackTween.Kill();
        attackTween.Kill();
    }
}
