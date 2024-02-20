using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class DartSpawner : WeaponSpawner
{
    public float impulseSpeed;
    public float interval = 0.05f;
    public DartSpawnSetting spawnSetting;
    private Tween spawnTween;
    private float originSpeed;
    private List<Tween> intervalTweenList = new List<Tween>();
    private List<Dart> curDarts = new List<Dart>();
    private void Awake()
    {
        originSpeed = impulseSpeed;
    }
    private void OnEnable()
    {
        DartSpawn();
    }


    public void DartSpawn()
    {

        spawnTween = DOVirtual.DelayedCall(spawnSetting.dartSettings[curLevel - 1].spawnTime * coolDownTime, () =>
               {
                   for (int i = 0; i < spawnSetting.dartSettings[curLevel - 1].dartCount; i++)
                   {
                       Tween intervalTween = null;
                       intervalTween = DOVirtual.DelayedCall(interval * i, () =>
                           {
                               Dart dart = myPool.GetObject() as Dart;
                               curDarts.Add(dart);
                               dart.transform.position = playerTransform.position;
                               dart.transform.rotation = playerTransform.rotation;
                               dart.damage = (int)Mathf.Round(spawnSetting.dartSettings[curLevel - 1].damage * plusDamage);
                               Vector3 forceDirection = (getNearEnemy().transform.position - playerTransform.position).normalized;
                               dart.DartMove(forceDirection);
                               dart.onDisposeDart = (() =>
                               {
                                   curDarts.Remove(dart);
                               });
                               intervalTweenList.Remove(intervalTween);
                           });
                       intervalTweenList.Add(intervalTween);
                   }
                   DartSpawn();

               });


    }
    public override void IncreaseDamage(float percentage)
    {
        base.IncreaseDamage(percentage);
        foreach (var dart in curDarts)
        {
            dart.damage = (int)Mathf.Round(spawnSetting.dartSettings[curLevel - 1].damage * plusDamage);
            
        }
    }

    public override void TweenControl(bool pause)
    {
        if (pause)
        {
            spawnTween.Pause();
            foreach (var tween in intervalTweenList)
            {
                tween.Pause();
            }
            foreach (var dart in curDarts)
            {
                dart.StopMoving(true);
            }
        }
        else
        {
            spawnTween.Play();
            foreach (var tween in intervalTweenList)
            {
                tween.Play();
            }
            foreach (var dart in curDarts)
            {
                dart.StopMoving(false);
            }
        }
    }
    private void OnDestroy()
    {
        spawnTween.Kill();
    }
}
