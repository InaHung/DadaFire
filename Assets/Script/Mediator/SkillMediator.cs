using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillMediator : IMediator
{
    [Regist]
    private SkillProxy skillProxy;
    private SkillView skillView;
    public override void Register(IView view)
    {
        skillView = (SkillView)view;
    }

   
    

    [Listener(SkillEvent.ON_RANDOM_SKILL_COMPLETE)]
    private void SetRandomSkill()
    {
        skillView.ShowWeaponChoice(skillProxy.selectSkills);
    }
    public void ChooseWeapon(WeaponType weaponType)
    {
        skillProxy.SetWeaponLevel(weaponType);
    }

}
