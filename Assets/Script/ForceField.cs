using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ForceField : Weapon
{
    public Transform playerTrasform;
    public float interval;
    private Dictionary<Enemy, Tween> attackEnemyTween = new Dictionary<Enemy, Tween>();
    private void Update()
    {
        if (playerTrasform != null)
            transform.position = playerTrasform.position;

    }
    public void SetPlayerTransform(Transform transform)
    {
        playerTrasform = transform;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
           

            Tween attackTween = DOVirtual.DelayedCall(interval, () =>
            {

                enemy.Injured(damage);

            });
            attackEnemyTween.Add(enemy, attackTween);
            attackTween.OnComplete(() =>
            {
                attackTween.Restart();
            });
            enemy.onDisposeEnemy += KillTween;
            enemy.Injured(damage);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {

            Enemy exitEnemey = collision.GetComponent<Enemy>();
            exitEnemey.onDisposeEnemy -= KillTween;
            KillTween(exitEnemey);
        }
    }

    private void KillTween(Enemy exitEnemey)
    {
        if (attackEnemyTween.ContainsKey(exitEnemey))
        {
            attackEnemyTween[exitEnemey]?.Kill();
            attackEnemyTween.Remove(exitEnemey);
        }
    }
    public void Pause(bool pause)
    {
        if(pause)
        {
            foreach(var tween in attackEnemyTween)
            {
                tween.Value.Pause();
            }
        }
        else
        {
            foreach (var tween in attackEnemyTween)
            {
                tween.Value.Play();
            }
        }
    }
    private void OnDestroy()
    {
        foreach(var tween in attackEnemyTween)
        {
            tween.Value.Kill();
        }
    }


}
