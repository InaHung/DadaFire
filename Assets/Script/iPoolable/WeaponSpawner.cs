using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class WeaponSpawner : MonoBehaviour
{
    public ObjectPool myPool;
    protected Transform playerTransform;
    public Func<Enemy> getNearEnemy;
    public WeaponType weaponType;
    protected int curLevel;
    public float plusDamage=1;
    public float plusScale = 1;
    protected Tween curTween;
    public float coolDownTime = 1;
    private void Awake()
    {
        
    }
    public virtual void SetPlayerTransform(Transform transform)
    {
        playerTransform = transform;
    }
    public virtual void SetCurrentLevel()
    {
        curLevel++;
    }
    public virtual void IncreaseDamage(float percentage)
    {
        plusDamage = percentage;
    }
    public virtual void CoolDown(float percentage)
    {
        coolDownTime = percentage;
    }
    public virtual void Expand(float percentage)
    {
        plusScale = percentage;
    }

    public virtual void TweenControl(bool pause)
    {
       
        if (pause)
            curTween?.Pause();
        else
            curTween.Play();
    }

}
public enum WeaponType
{
    None = -1,
    dart,
    laser,
   forcefield,
    drill,
    shield,
    MoveSpeed,
    HighPower,
    reduceCD,
    expand,
    bulletSpeed,

}

