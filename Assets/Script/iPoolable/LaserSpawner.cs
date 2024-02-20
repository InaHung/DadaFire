using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LaserSpawner : WeaponSpawner
{
    public ObjectPool MaxLaserPool;
    public lazerSetting lazerSetting;
    public Vector3 randomPosition;
    public Vector3 spawnPosition;
    public float spawnTime;
    public float disposeTime;
    private Laser curLaser;
    List<Tween> curTweens = new List<Tween>();
    public void LaserSpawn()
    { 
        Laser laser = myPool.GetObject() as Laser;
        curLaser = laser;
        spawnPosition.x = Random.Range(-randomPosition.x, randomPosition.x);
        spawnPosition.y = Random.Range(-randomPosition.y, randomPosition.y);
        laser.transform.position = spawnPosition + playerTransform.position;
        laser.damage = (int)Mathf.Round(lazerSetting.lazorLevelSettings[curLevel - 1].damage * plusDamage);
        laser.StartLaser(lazerSetting.lazorLevelSettings[curLevel - 1].animatoinName);
        Tween disposeTween = DOVirtual.DelayedCall(disposeTime, () =>
        {
            laser.Dispose();
           
        });
        curTweens.Add(disposeTween);
        disposeTween.OnComplete(() =>
        {
            curTweens.Remove(disposeTween);
        });


        Tween spawnTween = DOVirtual.DelayedCall(spawnTime * coolDownTime, () =>
          {
             
              LaserSpawn();
          });
        curTweens.Add(spawnTween);
        spawnTween.OnComplete(() =>
        {
            curTweens.Remove(spawnTween);
        });
    }
    public void MaxLaser()
    {
        Laser laser = MaxLaserPool.GetObject() as Laser;
        curLaser = laser;
        laser.transform.position = playerTransform.position;
        laser.damage = (int)Mathf.Round(lazerSetting.lazorLevelSettings[curLevel - 1].damage * plusDamage);
        Tween disposeTween = DOVirtual.DelayedCall(2f, () =>
        {
            laser.Dispose();
        });
        curTweens.Add(disposeTween);
        disposeTween.OnComplete(() =>
        {
            curTweens.Remove(disposeTween);
        });

        Tween spawnTween = DOVirtual.DelayedCall(spawnTime * coolDownTime, () =>
        {
            MaxLaser();

        });
        curTweens.Add(spawnTween);
        spawnTween.OnComplete(() =>
        {
            curTweens.Remove(spawnTween);
        });
    }
    public override void SetCurrentLevel()
    {
        gameObject.SetActive(true);
        base.SetCurrentLevel();
        if (curLevel == 1)
            LaserSpawn();
        if (curLevel == 6)
        {
            curLaser.Dispose();
            foreach(Tween tween in curTweens)
            {
                tween.Kill();
            }
            curTweens.Clear();
            MaxLaser();

        }

    }
    public override void TweenControl(bool pause)
    {
        if (curLaser != null)
            curLaser.lazerAnimator.speed = pause ? 0 : 1;

        foreach (Tween tween in curTweens)
        {
            curTween = tween;
            base.TweenControl(pause);
        }
        
    }
    public override void IncreaseDamage(float percentage)
    {
        base.IncreaseDamage(percentage);
        if (curLaser != null)
            curLaser.damage = (int)Mathf.Round(lazerSetting.lazorLevelSettings[curLevel-1].damage * plusDamage);

    }
    private void OnDestroy()
    {
        foreach (var tween in curTweens)
        {
            tween.Kill();
        }

    }

}
