using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShieldSpawner : WeaponSpawner
{
    public ShieldSetting sheildSetting;
    private Shield curShield;
    public float stayTime;
    private List<Tween> curTweens = new List<Tween>();

   
    public void ShieldSpawn()
    {
        Shield shield = myPool.GetObject() as Shield;
        curShield = shield;
        shield.SetPlayerTransform(playerTransform);
        shield.damage = (int)Mathf.Round(sheildSetting.shieldLevelSettings[curLevel - 1].damage * plusDamage);
        shield.SheildCount(sheildSetting.shieldLevelSettings[curLevel - 1].count);
        shield.moveSpeed = sheildSetting.shieldLevelSettings[curLevel - 1].speed;
        if (stayTime < sheildSetting.shieldLevelSettings[curLevel - 1].time * coolDownTime)
        {

            Tween disposeTween = DOVirtual.DelayedCall(stayTime, () =>
            {
                shield.Dispose();
               

            });
            curTweens.Add(disposeTween);
            disposeTween.OnComplete(() =>
            {
                curTweens.Remove(disposeTween);
            });
            
            Tween spawnTween = DOVirtual.DelayedCall(sheildSetting.shieldLevelSettings[curLevel - 1].time * coolDownTime, () =>
            {
                ShieldSpawn();
               
            });
            curTweens.Add(spawnTween);
            spawnTween.OnComplete(() =>
            {
                curTweens.Remove(spawnTween);
            });
        } 
    }
    
    public override void SetCurrentLevel()
    {
        base.SetCurrentLevel();
       foreach(Tween tween in curTweens)
        {
            tween.Kill();
        }
        if (curLevel == 6)
        {
            curShield.damage = (int)Mathf.Round(sheildSetting.shieldLevelSettings[curLevel - 1].damage * plusDamage);
            curShield.moveSpeed=sheildSetting.shieldLevelSettings[curLevel - 1].speed;
            foreach(var circle in curShield.curCircle)
            {
                circle.gameObject.transform.GetComponent<SpriteRenderer>().color = Color.red;
            }
           
        }
        if(curLevel<6)
        {
            curShield?.Dispose();
            ShieldSpawn();
        }
        
    
     }
    public override void IncreaseDamage(float percentage)
    {
        base.IncreaseDamage(percentage);
        if(curShield!=null)
            curShield.damage = (int)Mathf.Round(sheildSetting.shieldLevelSettings[curLevel - 1].damage * plusDamage);

    }
    public override void TweenControl(bool pause)
    {
        if (curShield != null)
            curShield.moveSpeed = pause?0: sheildSetting.shieldLevelSettings[curLevel - 1].speed;

        foreach (var tween in curTweens)
        {
            curTween = tween;
            base.TweenControl(pause);
        }
    }
    private void OnDestroy()
    {
        foreach (var tween in curTweens)
        {
            tween.Kill();
        }

    }

}
