using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FirstBoss : Boss
{
    private Tween attackTween;
    private Tween firstAttackTween;
    public float attackTime;
    public float firstAttackTime;
    public Garnade garnade;
    private Garnade curGarnade;
    
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
        curGarnade = Instantiate(garnade, transform.position, Quaternion.identity);
        curGarnade.transform.SetParent(transform);
        Vector2 throwDirection = (playerTransform.position - transform.position).normalized;
        curGarnade.GarnadeMove(throwDirection);
        attackTween = DOVirtual.DelayedCall(firstAttackTime, () =>
        {
            Attack();
        });
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
    
    public override void Pause(bool pause)
    {
        base.Pause(pause);
        if (pause)
        {
            attackTween.Pause();
            firstAttackTween.Pause();
            curGarnade?.StopAction(pause);
        }
        else
        {
            attackTween.Play();
            firstAttackTween.Play();
            curGarnade?.StopAction(pause);
        }
    }
    private void OnDestroy()
    {
        attackTween.Kill();
        firstAttackTween.Kill();
    }
}
