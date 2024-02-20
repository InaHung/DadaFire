using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DrillSpawner : WeaponSpawner
{
    public DrillSpawnSetting SpawnSetting;
    public List<Drill> curDrills = new List<Drill>();
    public float disposeTime;
    private Tween disposeTween;
    public float bulletSpeed = 1;
    public ObjectPool MaxDrillPool;
    private MaxDrill curMaxDrill;
    private void OnEnable()
    {
        DrillSpawn();
    }
    public void DrillSpawn()
    {
        for (int i = 0; i < SpawnSetting.drillLevelSettings[curLevel - 1].spawnCount-curDrills.Count; i++)
        {
            Drill drill = myPool.GetObject() as Drill;
            drill.transform.position = playerTransform.position;
            drill.MoveDirection = (getNearEnemy().transform.position - playerTransform.position).normalized;
            drill.moveSpeed = SpawnSetting.drillLevelSettings[curLevel - 1].moveSpeed*bulletSpeed;
            drill.damage = (int)Mathf.Round(SpawnSetting.drillLevelSettings[curLevel - 1].damage*plusDamage);
            curDrills.Add(drill);
            disposeTween = DOVirtual.DelayedCall(disposeTime, () =>
            {
                curDrills.Remove(drill);
                drill.Dispose();
                DrillSpawn();
           });
        }
   
    }
    public override void SetCurrentLevel()
    {
        base.SetCurrentLevel();
        if(curLevel<6)
        {
            DrillSpawn();
            foreach (var drill in curDrills)
            {
                drill.moveSpeed = SpawnSetting.drillLevelSettings[curLevel - 1].moveSpeed * bulletSpeed;
            }
        }
        if(curLevel==6)
        {
            foreach (var drill in curDrills)
            {
                drill.Dispose();
            }
            curDrills.Clear();
            disposeTween.Kill();
            MaxDrillSpawner();
        }

       
            
    }
    public override void TweenControl(bool pause)
    {
        curTween = disposeTween;

        if (pause)
        {
            curTween?.Pause();
            foreach (var drill in curDrills)
                drill.moveSpeed = 0;
            if (curMaxDrill != null)
            {
                curMaxDrill.moveSpeed = 0;
                curMaxDrill.Pause(true);
            }
        }
        else
        {
            curTween?.Play();
            foreach (var drill in curDrills)
            { 
                drill.moveSpeed = SpawnSetting.drillLevelSettings[curLevel - 1].moveSpeed * bulletSpeed;
            }
            if (curMaxDrill != null)
            {
                curMaxDrill.moveSpeed = SpawnSetting.drillLevelSettings[curLevel - 1].moveSpeed * bulletSpeed;
                curMaxDrill.Pause(false);
            }
        }
    }
    public override void IncreaseDamage(float percentage)
    {
        base.IncreaseDamage(percentage);
       
        foreach(var drills in curDrills)
        {
            drills.damage= (int)Mathf.Round(SpawnSetting.drillLevelSettings[curLevel - 1].damage * plusDamage);
        }
        if (curMaxDrill != null)
        {
            curMaxDrill.damage = (int)Mathf.Round(SpawnSetting.drillLevelSettings[curLevel - 1].damage * plusDamage);
        }
    }
    public void IncreaseMoveSpeed(float speed)
    {
        bulletSpeed = speed;
        foreach (var drills in curDrills)
        {
            drills.moveSpeed = SpawnSetting.drillLevelSettings[curLevel - 1].moveSpeed * bulletSpeed;
        }
        if(curMaxDrill!=null)
        {
            curMaxDrill.moveSpeed = SpawnSetting.drillLevelSettings[curLevel - 1].moveSpeed * bulletSpeed;
        }
        
    }
    public void MaxDrillSpawner()
    {
        MaxDrill maxDrill = MaxDrillPool.GetObject() as MaxDrill;
        maxDrill.transform.position = playerTransform.position;
        maxDrill.GetNearEnemy(getNearEnemy());
        maxDrill.moveSpeed = SpawnSetting.drillLevelSettings[curLevel - 1].moveSpeed*bulletSpeed;
        maxDrill.damage = (int)Mathf.Round(SpawnSetting.drillLevelSettings[curLevel - 1].damage * plusDamage);
        maxDrill.onGetNearEnemy=NearEnemy;
        curMaxDrill = maxDrill;
    }
    public Enemy NearEnemy()
    {
        return getNearEnemy();
    }
    private void OnDestroy()
    {
        disposeTween.Kill();
    }
}
