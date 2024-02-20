using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class InitCmd : ICommand
{
    [Regist]
    private ScoreProxy scoreProxy;
    [Regist]
    private SkillProxy skillProxy;
    public EXPSetting expSetting;
    public MaxWeaponSetting weaponSetting;
    public WeaponType weaponType;
    public float delaySetFirstWeaponTime;
    public override void InitComplete()
    {

        scoreProxy.expSetting = expSetting;
        scoreProxy.SetInitLevel();
        skillProxy.maxWeaponSetting = weaponSetting;
        DOVirtual.DelayedCall(delaySetFirstWeaponTime, () =>
        {
            skillProxy.SetWeaponLevel(weaponType);
        });

    }



}
