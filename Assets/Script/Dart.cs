using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Dart : Weapon
{

    private Tween disposeTween;
    public float disposeTime;
    public Rigidbody2D dartRigidbody;
    public float impulse;
    public Action onDisposeDart;
    private Vector3 curForceDirection;
    public Vector2 originVelocity;
   
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemy.Injured(damage);
            Dispose();
            disposeTween.Kill();
            onDisposeDart();
        }

    }
    public void DartMove(Vector3 forceDirection)
    {
        dartRigidbody.AddForce(forceDirection * impulse, ForceMode2D.Impulse);
        originVelocity = dartRigidbody.velocity;
        curForceDirection = forceDirection;
        disposeTween = DOVirtual.DelayedCall(disposeTime, () =>
        {
            Dispose();
            onDisposeDart();
        });
    }
    public void StopMoving(bool stop)
    {

        if (stop)
        {
            dartRigidbody.velocity = new Vector2(0, 0);
            disposeTween.Pause();
            
        }
        else
        {
            dartRigidbody.velocity = originVelocity;
            disposeTween.Play();
          
        }

    }
    private void OnDestroy()
    {
        disposeTween.Kill();
    }



}
