using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeFiledSpawner : WeaponSpawner
{
    public ForceFildSetting forceFildSetting;
    private ForceField currentField;
    private void OnEnable()
    {
        FiledSpawn();
    }
    public void FiledSpawn()
    {
        ForceField forceField = myPool.GetObject() as ForceField;
        currentField = forceField;
        forceField.SetPlayerTransform(playerTransform);
        float size = forceFildSetting.fieldLevelSettings[0].areaScale;
        forceField.transform.localScale = new Vector3(size, size, size) * plusScale;
        forceField.damage = (int)Mathf.Round(forceFildSetting.fieldLevelSettings[0].damage * plusDamage);
    }
    public override void SetCurrentLevel()
    {
        base.SetCurrentLevel();
        if (curLevel >= 2)
        {
            float size = forceFildSetting.fieldLevelSettings[curLevel - 1].areaScale;
            currentField.transform.localScale = new Vector3(size, size, size) * plusScale;
            currentField.damage = (int)Mathf.Round(forceFildSetting.fieldLevelSettings[curLevel - 1].damage * plusDamage);
            if (curLevel == 6)
                currentField.transform.GetComponent<SpriteRenderer>().color = Color.red;

        }
    }
    public override void IncreaseDamage(float percentage)
    {
        base.IncreaseDamage(percentage);
        if (currentField != null)
            currentField.damage = (int)Mathf.Round(forceFildSetting.fieldLevelSettings[curLevel - 1].damage * plusDamage);

    }
    public override void Expand(float percentage)
    {
        base.Expand(percentage);
        if (currentField != null)
            currentField.transform.localScale *= forceFildSetting.fieldLevelSettings[0].areaScale * plusScale;
    }
    public override void TweenControl(bool pause)
    {
    
            currentField?.Pause(pause);
        
    }

}
